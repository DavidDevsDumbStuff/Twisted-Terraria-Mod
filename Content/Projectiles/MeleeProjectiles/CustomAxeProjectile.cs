using System;
using System.Collections.Generic;
using BatzUtils.Content.Items.Tools;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace BatzUtils.Content.Projectiles.MeleeProjectiles;

public class CustomAxeProjectile : ModProjectile
{
    Player player;
    bool SwingDown = true;
    public override void SetDefaults()
    {
        Projectile.width = 0;
        Projectile.height = 0;
        Projectile.friendly = true;
        Projectile.penetrate = -1;
        Projectile.tileCollide = false;
        Projectile.DamageType = DamageClass.Melee;
        Projectile.ignoreWater = true;
        Projectile.usesLocalNPCImmunity = true;
        Projectile.ownerHitCheck = true;
        Projectile.timeLeft = 2;
        Projectile.hide = false;
        
    }

    public override void DrawBehind(int index, List<int> behindNPCsAndTiles, List<int> behindNPCs, List<int> behindProjectiles, List<int> overPlayers,
        List<int> overWiresUI)
    {
       // overPlayers.Add(index);
        base.DrawBehind(index, behindNPCsAndTiles, behindNPCs, behindProjectiles, overPlayers, overWiresUI);
    }
    
    
    public override void ModifyDamageHitbox(ref Rectangle hitbox)
    {
        hitbox.Width = 30;
        hitbox.Height = 30;
        int newX = (int)(Projectile.Center.X - (hitbox.Width ) * Math.Cos(Projectile.rotation +MathHelper.ToRadians(90)));
        int newY = (int)(Projectile.Center.Y - hitbox.Height * Math.Sin(Projectile.rotation + MathHelper.ToRadians(90)));
         //hitbox.Location= new Point((int)Projectile.position.X, (int)Projectile.position.Y - Projectile.height);
         hitbox.Location = new Point(newX - hitbox.Width/2,newY - hitbox.Height/2) ;
    }
    public override bool PreDraw(ref Color lightColor)
    {
         // SpriteEffects helps to flip texture horizontally and vertically
        SpriteEffects spriteEffects = SpriteEffects.None;
        if (Projectile.spriteDirection == 1 && !SwingDown || Projectile.spriteDirection == -1 && SwingDown)
            spriteEffects = SpriteEffects.FlipHorizontally;
        // Getting texture of projectile
        Texture2D texture = (Texture2D) ModContent.Request<Texture2D>(Texture);
        // Calculating frameHeight and current Y pos dependence of frame
        // If texture without animation frameHeight is always texture.Height and startY is always 0
        int frameHeight = texture.Height / Main.projFrames[Projectile.type];
        int startY = frameHeight * Projectile.frame;
        // Get this frame on texture
        Rectangle sourceRectangle = new Rectangle(0, startY, texture.Width, frameHeight);
        // Alternatively, you can skip defining frameHeight and startY and use this:
        // Rectangle sourceRectangle = texture.Frame(1, Main.projFrames[Projectile.type], frameY: Projectile.frame);
        Vector2 origin = sourceRectangle.BottomLeft();
        // If image isn't centered or symmetrical you can specify origin of the sprite
        // (0,0) for the upper-left corner
        float offsetX = 25f;
        origin.X = (float) (Projectile.spriteDirection == 1 ? sourceRectangle.Width - offsetX : offsetX);
        // If sprite is vertical
        // float offsetY = 20f;
        // origin.Y = (float)(Projectile.spriteDirection == 1 ? sourceRectangle.Height - offsetY : offsetY);
        // Applying lighting and draw current frame
        Color drawColor = Projectile.GetAlpha(lightColor);
        //Projectile.Center = player.Center + new Vector2(player.width/2f -3f,0);
        //Projectile.rotation+= MathHelper.ToRadians(90)/20f * player.direction;
        Main.EntitySpriteDraw(texture,
            Projectile.Center - Main.screenPosition + new Vector2(0f, Projectile.gfxOffY),
            sourceRectangle, drawColor, Projectile.rotation, origin, Projectile.scale, spriteEffects, 0);
        // It's important to return false, otherwise we also draw the original texture.
        return false;
    }

    public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
    {
        Projectile.localNPCHitCooldown = 30;
        
    }

    public override void OnSpawn(IEntitySource source)
    {
        player = Main.player[Projectile.owner];
        Projectile.rotation = 0; //MathHelper.ToRadians(15) * player.direction;
    }
    //TODO allow change directions + do damage only at end of swings
    //TODO Sync arm movement
    public override bool PreAI()
    {
        if (player.channel && player.HeldItem.type == ModContent.ItemType<CustomAxe>() || player.noItems || player.CCed)
        {
            Projectile.spriteDirection = player.direction;
            Projectile.Center = player.Center;
            Projectile.timeLeft = 5;
            Projectile.ai[0] += 1f;
            
            if (SwingDown)
            {
                Projectile.rotation =  MathHelper.Lerp(Projectile.rotation, MathHelper.ToRadians(165f* player.direction)  ,MathHelper.ToRadians(Projectile.ai[0] * 5));
            }
            else
            {
              
                Projectile.rotation =  MathHelper.Lerp(Projectile.rotation , MathHelper.ToRadians(15 * player.direction) ,MathHelper.ToRadians(Projectile.ai[0] * 5));
            }
            if (Projectile.ai[0] >= 30f)
            {
                SwingDown = !SwingDown;
                Projectile.ai[0] = 0f;
                Projectile.netUpdate = true;
            }
           // player.SetCompositeArmFront(true, Player.CompositeArmStretchAmount.Full, base.Projectile.velocity.ToRotation() * player.gravDir - 1.5707964f);
            //player.SetCompositeArmBack(true, Player.CompositeArmStretchAmount.Full, base.Projectile.velocity.ToRotation() * player.gravDir - 1.5707964f - 0.3926991f * player.direction);
        }
        return false;
    }
}
