using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace BatzUtils.Content.Projectiles;

public class SplittingArrow : ModProjectile
{
    public bool hasSplit;
    
    public override void SetStaticDefaults() {
        DisplayName.SetDefault("MultiArrow");
    }
    public override void SetDefaults()
    {
        Projectile.CloneDefaults(ProjectileID.WoodenArrowFriendly);
        AIType = ProjectileID.WoodenArrowFriendly;
        hasSplit = false;
        

    }

    public override void AI()
    {
        Projectile.ai[1]++;
        float maxDetectRadius = 250f; 
        float projSpeed = 5f;

        if (Projectile.ai[1] < 30 || !hasSplit)
        {
            return;
        }

        NPC closestNpc = Helpers.FindClosestNpcProjectile(maxDetectRadius, Projectile);
            if (closestNpc == null)
                return;

        Projectile.velocity = (closestNpc.Center - Projectile.Center).SafeNormalize(Vector2.Zero) * projSpeed;
        Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.PiOver2;
        Lighting.AddLight(Projectile.Center, 0.9f, 0.1f, 0.3f);
        }
    
    public override void Kill(int timeLeft) {
        if(Projectile.owner != Main.myPlayer)
            return;
        Vector2 launchVelocity = new Vector2(-5, 0); 
        for (int i = 0; i < 8; i++) {
            launchVelocity = launchVelocity.RotatedBy(MathHelper.PiOver4/2);
            if (!hasSplit)
            {
                int index =Projectile.NewProjectile(Projectile.InheritSource(Projectile), Projectile.Center, launchVelocity, ModContent.ProjectileType<SplittingArrow>(), Projectile.damage, Projectile.knockBack, Projectile.owner);
                if (Main.projectile[index].ModProjectile is SplittingArrow modProjectile)
                {
                    modProjectile.hasSplit = true;
                    
                }
            }
        }
    }
}
