using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.Enums;
using Terraria.GameContent;
using Terraria.ModLoader;

namespace TwistedTerraria.Content.Projectiles;

public class LaserBeam : ModProjectile
{
    Player player;
    public override void OnSpawn(IEntitySource source)
    {
        player = Main.player[Projectile.owner];
    }

    // The maximum charge value
    private const float MAX_CHARGE = 50f;
    //The distance charge particle from the player center
    private const float MOVE_DISTANCE = 60f;

    // The actual distance is stored in the ai0 field
    // By making a property to handle this it makes our life easier, and the accessibility more readable
    public float Distance {
        get => Projectile.ai[0];
        set => Projectile.ai[0] = value;
    }

    // The actual charge value is stored in the localAI0 field
    public float Charge {
        get => Projectile.localAI[0];
        set => Projectile.localAI[0] = value;
    }

    // Are we at max charge? With c#6 you can simply use => which indicates this is a get only property
    public bool IsAtMaxCharge => Charge >= MAX_CHARGE;
    
    public override void SetDefaults() {
        Projectile.friendly = true;
        Projectile.penetrate = -1;
        Projectile.tileCollide = false;
        //Projectile.hide = true;
    }

    public override bool PreDraw(ref Color lightColor)
    {
        if (IsAtMaxCharge)
        {
            DrawLaser(TextureAssets.Projectile[Projectile.type].Value, Main.player[Projectile.owner].Center + new Vector2(12f * player.direction,-2),
                Projectile.velocity, 10, Projectile.damage, -1.57f, 1f, 1000f, Color.White, (int)MOVE_DISTANCE);
        }
        return false;
    }

    public void DrawLaser(Texture2D texture, Vector2 start, Vector2 unit, float step, int damage, float rotation = 0f, float scale = 1f, float maxDist = 2000f, Color color = default(Color), int transDist = 50) {
        float r = unit.ToRotation() + rotation;

        // Draws the laser 'body'
        for (float i = transDist; i <= Distance; i += step) {
            Color c = Color.White;
            var origin = start + i * unit;
            Main.EntitySpriteDraw(texture, origin - Main.screenPosition,
                new Rectangle(0, 26, 28, 26), i < transDist ? Color.Transparent : c, r,
                new Vector2(28 * .5f, 26 * .5f), scale, 0, 0);
        }
			
        // Draws the laser 'tail'
        Main.EntitySpriteDraw(texture, start + unit * (transDist - step) - Main.screenPosition,
            new Rectangle(0, 0, 28, 26), Color.White, r, new Vector2(28 * .5f, 26 * .5f), scale, 0, 0);
			
        // Draws the laser 'head'
        Main.EntitySpriteDraw(texture, start + (Distance + step) * unit - Main.screenPosition,
            new Rectangle(0, 52, 28, 26), Color.White, r, new Vector2(28 * .5f, 26 * .5f), scale, 0, 0);
    }
    
    public override void OnHitNPC(NPC target, int damage, float knockback, bool crit) {
        Projectile.localNPCHitCooldown = 5;
    }
    
    public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox) {
        // We can only collide if we are at max charge, which is when the laser is actually fired
        if (!IsAtMaxCharge) return false;

        
        Vector2 unit = Projectile.velocity;
        float point = 0f;
        // Run an AABB versus Line check to look for collisions, look up AABB collision first to see how it works
        // It will look for collisions on the given line using AABB
        return Collision.CheckAABBvLineCollision(targetHitbox.TopLeft(), targetHitbox.Size(), player.Center,
            player.Center + unit * Distance, 22, ref point);
    }
    
    public override void AI() {
        player = Main.player[Projectile.owner];
        
        

        Projectile.position = player.Center + Projectile.velocity * MOVE_DISTANCE;
        Projectile.timeLeft = 2;

        // By separating large AI into methods it becomes very easy to see the flow of the AI in a broader sense
        // First we update player variables that are needed to channel the laser
        // Then we run our charging laser logic
        // If we are fully charged, we proceed to update the laser's position
        // Finally we spawn some effects like dusts and light

        UpdatePlayer();
        ChargeLaser();
        SpawnCollisionDust();
        // If laser is not charged yet, stop the AI here.
        if (Charge < MAX_CHARGE) return;

        SetLaserPosition(player);
    }
    void UpdatePlayer()
    {
        // Multiplayer support here, only run this code if the client running it is the owner of the projectile
        if (Projectile.owner == Main.myPlayer) {
            Vector2 diff = Main.MouseWorld - player.Center;
            diff.Normalize();
            Projectile.velocity = diff;
        Projectile.direction = Main.MouseWorld.X > player.position.X ? 1 : -1;
        Projectile.netUpdate = true;
        }
        int dir = Projectile.direction;
        player.ChangeDir(dir); // Set player direction to where we are shooting
        player.heldProj = Projectile.whoAmI; // Update player's held projectile
        player.itemTime = 2; // Set item time to 2 frames while we are used
        player.itemAnimation = 2; // Set item animation time to 2 frames while we are used
        player.itemRotation = (float)Math.Atan2(Projectile.velocity.Y * dir, Projectile.velocity.X * dir); // Set the item rotation to where we are shooting
    }
    
    void ChargeLaser()
    {
        // Kill the projectile if the player stops channeling
        if (!player.channel) {
            Projectile.Kill();
        }
        else {
            Vector2 offset = Projectile.velocity;
            offset *= MOVE_DISTANCE - 20;
            Vector2 pos = player.Center + offset - new Vector2(10, 10);
            if (Charge < MAX_CHARGE) {
                player.GetModPlayer<TTModPlayer>().ScreenShake = player.channel;
                Charge++;
            }
            else
            {
                player.GetModPlayer<TTModPlayer>().ScreenShake = false;
            }
            int chargeFact = (int)(Charge / 20f);
             Vector2 dustVelocity = Vector2.UnitX * 18f;
             dustVelocity = dustVelocity.RotatedBy(Projectile.rotation - 1.57f);
             Vector2 spawnPos = Projectile.Center + dustVelocity;
             for (int k = 0; k < chargeFact + 1; k++) {
                 Vector2 spawn = spawnPos + ((float)Main.rand.NextDouble() * 6.28f).ToRotationVector2() * (12f - chargeFact * 2);
                Dust dust = Main.dust[Dust.NewDust(pos, 20, 20, 226, Projectile.velocity.X / 2f, Projectile.velocity.Y / 2f)];
                dust.velocity = Vector2.Normalize(spawnPos - spawn) * 1.5f * (10f - chargeFact * 2f) / 10f;
                dust.noGravity = true;
                dust.scale = Main.rand.Next(10, 20) * 0.05f;
            }
        }
    }

    void SetLaserPosition(Player player)
    {
        for (Distance = MOVE_DISTANCE; Distance <= 2200f; Distance += 5f)
        {
            var start = player.Center + Projectile.velocity * Distance;
            if (!Collision.CanHitLine(player.Center, 1, 1, start, 1, 1)) {
                Distance -= 5f;
                break;
            }
        }
    }
    
    public override void CutTiles() {
        DelegateMethods.tilecut_0 = TileCuttingContext.AttackProjectile;
        Vector2 unit = Projectile.velocity;
        Utils.PlotTileLine(Projectile.Center, Projectile.Center + unit * Distance, (Projectile.width + 16) * Projectile.scale, DelegateMethods.CutTiles);
    }
    
    void SpawnCollisionDust()
    {
        Vector2 dustPos = player.Center + Projectile.velocity * Distance;
        if(Distance < 1)
            return;
        for (int i = 0; i < 2; ++i) {
            float num1 = Projectile.velocity.ToRotation() + (Main.rand.NextBool(2)? -1.0f : 1.0f) * 1.57f;
            float num2 = (float)(Main.rand.NextDouble() * 0.8f + 1.0f);
            Vector2 dustVel = new Vector2((float)Math.Cos(num1) * num2, (float)Math.Sin(num1) * num2);
            Dust dust = Main.dust[Dust.NewDust(dustPos + new Vector2(17f*player.direction,-6f), 0, 0, 226, dustVel.X, dustVel.Y)];
            dust.noGravity = true;
            dust.scale = 1.2f;
        }
    }
}
