using Terraria.ModLoader;

namespace BatzUtils.Content.Projectiles;

public class StoneProjectile : ModProjectile
{
    public override void SetDefaults()
    {
        
        Projectile.CloneDefaults(1);
        Projectile.penetrate = 1;
        Projectile.width = 10;
        Projectile.height = 10;
    }
}
