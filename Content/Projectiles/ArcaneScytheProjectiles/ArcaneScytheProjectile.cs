using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;

namespace BatzUtils.Content.Projectiles.ArcaneScytheProjectiles;

public class ArcaneScytheProjectile : ModProjectile
{
    Player _player;
    float _overrideScale;
    int _overrideHeight;
    int _overrideWidth;
    bool hasScaled = false;
    bool hasCenetred = false;

    public void ScaleProjectile(int width, int height, float scale)
    {
        _overrideWidth = width;
        _overrideHeight = height;
        _overrideScale = scale;
        hasScaled = true;
    }
    public override void SetDefaults()
    {
        Projectile.friendly = true;
        Projectile.penetrate = -1;
        Projectile.tileCollide = false;
        Projectile.height = 76;
        Projectile.width = 66;
    }
    public override void OnSpawn(IEntitySource source)
    {
        _player = Main.player[Projectile.owner];
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
        Projectile.AdjustHitboxSize(new Vector2(_overrideWidth, _overrideHeight));
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
        if (hasScaled && !hasCenetred)
        {
            Projectile.Center = _player.Center + new Vector2((_player.width + 20 * _overrideScale) * _player.direction,
                -Projectile.height / 4f + 20 * _overrideScale);
            hasCenetred = true;
        }
        Main.EntitySpriteDraw(texture,
            Projectile.Center - Main.screenPosition + new Vector2(0f, Projectile.gfxOffY),
            sourceRectangle, drawColor, Projectile.rotation, origin, _overrideScale, spriteEffects, 0);
        // It's important to return false, otherwise we also draw the original texture.
        Lighting.AddLight(Projectile.Center, 1f, 1f, 1f);
        return false;
    }

    public override void AI()
    {
        if (Main.LocalPlayer.whoAmI == Projectile.owner)
        {
            Projectile.ai[0]++;
            if (Projectile.ai[0] <= Helpers.Seconds(0.5f))
            {
                Projectile.velocity = new Vector2(5f * 1.25f,0);
            }
            else
            {
                Projectile.velocity = new Vector2(5f,0);
            }
        }
    }
}
