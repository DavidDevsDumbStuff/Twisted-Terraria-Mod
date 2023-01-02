using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;

namespace BatzUtils.Content.Projectiles.ArcaneScytheProjectiles;

public class ArcaneScytheSwingProjectile : ModProjectile
{
    Player _player;
    int _startDirection;
    public override void SetDefaults()
    {
        Projectile.tileCollide = false;
        Projectile.penetrate = -1;
        Projectile.height = 54;
        Projectile.width = 76;
        Projectile.scale = 1f;
    }  
    
    public override void OnSpawn(IEntitySource source)
    {
        
        _player = Main.player[Projectile.owner];
        _startDirection = _player.direction;
        Projectile.Center = _player.Center;
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
        //Projectile.AdjustHitboxSize(new Vector2(OverrideWidth,OverrideHeight));
        
        // Alternatively, you can skip defining frameHeight and startY and use this:
        //Rectangle sourceRectangle = texture.Frame(1, Main.projFrames[Projectile.type], frameY: Projectile.frame);

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
        Projectile.Center = _player.Center +new Vector2((_player.width + 20) * _player.direction, 0);
        Main.EntitySpriteDraw(texture,
            Projectile.Center - Main.screenPosition + new Vector2(0f, Projectile.gfxOffY),
            sourceRectangle, drawColor, Projectile.rotation * _player.direction, origin, 1, spriteEffects, 0);

        // It's important to return false, otherwise we also draw the original texture.
        return false;
    }
}
