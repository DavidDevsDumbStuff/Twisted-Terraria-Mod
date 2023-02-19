using Microsoft.Xna.Framework;
using Terraria;

namespace TwistedTerraria;

public static class ProjectileExtension
{
    public  static void AdjustHitboxSize(this Projectile projectile,int amount)
    {
        projectile.position.Y += projectile.height;
        projectile.height = amount;
        projectile.position.Y -= projectile.height;

        projectile.position.X += projectile.width / 2f;
        projectile.width = amount;
        projectile.position.X -= projectile.width / 2f;
    }
    public static void AdjustHitboxSize(this Projectile projectile,float amount)
    {
        projectile.position.Y += projectile.height;
        projectile.height = (int)amount;
        projectile.position.Y -= projectile.height;

        projectile.position.X += projectile.width / 2f;
        projectile.width = (int)amount;
        projectile.position.X -= projectile.width / 2f;
    }
    public static void AdjustHitboxSize(this Projectile projectile,Vector2 amount)
    {
        projectile.position.Y += projectile.height;
        projectile.height = (int)amount.Y;
        projectile.position.Y -= projectile.height;

        projectile.position.X += projectile.width / 2f;
        projectile.width = (int)amount.X;
        projectile.position.X -= projectile.width / 2f;
    }
}
