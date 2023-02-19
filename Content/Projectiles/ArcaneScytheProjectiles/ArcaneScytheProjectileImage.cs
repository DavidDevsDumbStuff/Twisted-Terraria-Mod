using TwistedTerraria.Content.Dusts;
using TwistedTerraria.Content.Items.Weapons;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;

namespace TwistedTerraria.Content.Projectiles.ArcaneScytheProjectiles;

public class ArcaneScytheProjectileImage : ModProjectile
{
    bool _hasReleased;
    Player _player;
    int _manaUsed;
    int xOffset;
    public float OverrideScale { get; private set; }
    public int OverrideHeight { get; private set; }
    public int OverrideWidth { get; private set; }
    int _startDirection;
    
    public override void SetDefaults()
    {
        Projectile.tileCollide = false;
        Projectile.penetrate = -1;
        Projectile.height = 80;
        Projectile.width = 48;
        Projectile.scale = 1f;
    }   
    public override void OnSpawn(IEntitySource source)
    {
        
        _player = Main.player[Projectile.owner];
        _startDirection = _player.direction;
        Projectile.Center = _player.Center;
    }

  
    public override void AI()
    {
        Projectile.alpha -= 40;
        if (Projectile.alpha < 0)
        {
            Projectile.alpha = 0;
        }
        _player = Main.player[Projectile.owner];
        if (Main.LocalPlayer.whoAmI == Projectile.owner)
        {
            _player.direction = _startDirection;
            int manaUsage = _player.statManaMax2 / 60;
            
            //if Channeling with the correct item, and You haven't channeled more than your max mana and you can afford to keep channeling
            if (_player.channel && _player.HeldItem.type == ModContent.ItemType<ArcaneScythe>() &&
                _manaUsed <= _player.statManaMax2 && manaUsage <= _player.statMana)
            {
                for (int i = 0; i < 3; i++)
                {   Vector2 spawnLocation = Main.rand.NextVector2Circular(200f, 200f);
                    Dust dust= Dust.NewDustPerfect(Projectile.Center - spawnLocation, ModContent.DustType<ArcaneDust>(), Vector2.Zero, Scale: 1.5f);
                }
                //Stop mana delay for 2 seconds
                _player.manaRegenDelay = 120;
                //Spend mana
                _player.statMana -= manaUsage;
                //Allow mana flower interaction
                if (_player.manaFlower && _player.statMana <= manaUsage * 2)
                {
                    _player.QuickMana();
                }
                _manaUsed += manaUsage;
                
                //Scale size and hitbox based on charge length (manaUsed)
                Projectile.timeLeft = Helpers.Seconds(10);
                OverrideScale  = _manaUsed / 80f >= 0.1f ? _manaUsed/80f : 0.1f;
                OverrideHeight = (int) (80 * OverrideScale);
                OverrideWidth = (int) (48 * OverrideScale);

                Lighting.AddLight(Projectile.Center, 1.1f * Projectile.scale, 1.1f *Projectile.scale, 0.8f * Projectile.scale);
                //Update projectile Position & rotation
                if (_player.direction < 0)
                {
                    Projectile.rotation = -MathHelper.Pi;
                }
                
               
               
            }
            else
            {
                //Destroy image and shoot real projectile
                Projectile.Kill();
                ActivateProjectile();
                
            }

        }
    }

    public override bool PreDraw(ref Color lightColor)
    {
        // SpriteEffects helps to flip texture horizontally and vertically
        SpriteEffects spriteEffects = SpriteEffects.None;
        if (Projectile.spriteDirection == -1)
            spriteEffects = SpriteEffects.FlipHorizontally;

        // Getting texture of projectile
        Texture2D texture = (Texture2D) ModContent.Request<Texture2D>(Texture);

        // Calculating frameHeight and current Y pos dependence of frame
        
        // If texture without animation frameHeight is always texture.Height and startY is always 0
        int frameHeight = texture.Height / Main.projFrames[Projectile.type];
        int startY = frameHeight * Projectile.frame;

        // Get this frame on texture
        Rectangle sourceRectangle = new Rectangle(0, startY, texture.Width, frameHeight);
        Projectile.AdjustHitboxSize(new Vector2(OverrideWidth,OverrideHeight));
        // Alternatively, you can skip defining frameHeight and startY and use this:
        // Rectangle sourceRectangle = texture.Frame(1, Main.projFrames[Projectile.type], frameY: Projectile.frame);

        Vector2 origin = sourceRectangle.Size() / 2f;

        // If image isn't centered or symmetrical you can specify origin of the sprite
        // (0,0) for the upper-left corner
        float offsetX = 25f;
        origin.X = (float) (Projectile.spriteDirection == 1 ? sourceRectangle.Width - offsetX : offsetX);

        // If sprite is vertical
        // float offsetY = 20f;
        // origin.Y = (float)(Projectile.spriteDirection == 1 ? sourceRectangle.Height - offsetY : offsetY);


        // Applying lighting and draw current frame
        Color drawColor = Projectile.GetAlpha(lightColor);
        Projectile.Center = _player.Center +new Vector2((_player.width + 20 * OverrideScale) * _player.direction, 0);
        Main.EntitySpriteDraw(texture,
            Projectile.Center - Main.screenPosition + new Vector2(0f, Projectile.gfxOffY),
            sourceRectangle, drawColor, Projectile.rotation, origin, OverrideScale, spriteEffects, 0);

        // It's important to return false, otherwise we also draw the original texture.
        return false;
    }

    public override Color? GetAlpha(Color color)
   {
       return new Color(255, 255, 255, 255) * ((255f - (float)Projectile.alpha) / 255f);
   }

   void ActivateProjectile()
    {
        if (!_hasReleased)
        {
            _player.SetDummyItemTime(0);
            _hasReleased = true;
            // Spawn projectile with data based of charged duration
            int index = Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center,
                new Vector2(5f * _startDirection, 0), ModContent.ProjectileType<ArcaneScytheProjectile>(), (int)(_manaUsed * 2 *  (1 -_player.manaSickReduction)), 0, _player.whoAmI);
            var projectile = Main.projectile[index];
            if (_player.direction < 0)
            {
                projectile.rotation =-MathHelper.Pi;
            }

            if (projectile.ModProjectile is ArcaneScytheProjectile arcaneScytheProjectile)
                arcaneScytheProjectile.ScaleProjectile(OverrideWidth, OverrideHeight, OverrideScale);
            projectile.timeLeft = Helpers.Seconds(OverrideScale * 2f);
        }

    }
  
}
