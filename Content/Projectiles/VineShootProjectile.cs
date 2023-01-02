using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace BatzUtils.Content.Projectiles;

public class VineShootProjectile : ModProjectile
{
    
    public override void SetDefaults()
    {
        Projectile.CloneDefaults(ProjectileID.JavelinFriendly);
        AIType = ProjectileID.JavelinFriendly;
        Projectile.damage = 0;
        Projectile.penetrate = 1;
        
    }
    public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
    {
        if (!target.CountsAsACritter && !target.immortal)
        {
            Main.player[Projectile.owner].HealEffect(1);
        }
            
    }
}
