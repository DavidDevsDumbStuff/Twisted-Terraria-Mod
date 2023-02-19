using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ItemID = On.Terraria.ID.ItemID;

namespace TwistedTerraria.Content.Projectiles;

public class StoneCupProjectile : ModProjectile
{

    public override void SetDefaults()
    {
        Projectile.CloneDefaults(ProjectileID.Grenade);
        AIType = ProjectileID.Grenade;
        Projectile.width = 15;  
        Projectile.height = 15;
        DrawOffsetX =  -(30-Projectile.width)/2 ;
        DrawOriginOffsetY = -(30 - Projectile.height) / 2;
    }
    public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
    {
        if (!target.friendly)
        {
            target.AddBuff(BuffID.Ichor,Helpers.Seconds(10));
        }
        Projectile.Kill();
    }

    public override void AI()
    {
        Projectile.velocity.Y += 0.4f; 
        if (Projectile.velocity.Y > 16f)
        {
            Projectile.velocity.Y = 16f;
        }
        base.AI();
    }
    public override bool OnTileCollide(Vector2 oldVelocity)
    {
        Projectile.Kill();
        return false;
    }

    public override void Kill(int timeLeft) {
        if(Projectile.owner != Main.myPlayer)
            return;
        Vector2 launchVelocity = new Vector2(-5, 0); 
        for (int i = 0; i < 8; i++) {
            
            launchVelocity = launchVelocity.RotatedBy(MathHelper.PiOver4/2 + MathHelper.ToRadians(Main._rand.NextFloat(-30,30)) );
            { 
                Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center, launchVelocity, ModContent.ProjectileType<StoneProjectile>(), Projectile.damage/2, Projectile.knockBack, Projectile.owner);
            }
        }
    }
}
