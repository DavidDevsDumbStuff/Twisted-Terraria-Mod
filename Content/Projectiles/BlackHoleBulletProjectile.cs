using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TwistedTerraria.Content.Projectiles;

public class BlackHoleBulletProjectile : ModProjectile
{
    public override void SetDefaults()
    {
        Projectile.CloneDefaults(ProjectileID.Bullet);
        Projectile.scale = 4f;
        Projectile.friendly = true;
    }

    public override void Kill(int timeLeft)
    {
        if (Projectile.owner == Main.myPlayer)
        {
            Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center, Vector2.Zero,
                ModContent.ProjectileType<BlackHoleProjectile>(), 40, 1f);
        }
        
    }
}
