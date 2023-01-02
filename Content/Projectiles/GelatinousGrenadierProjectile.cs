using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace BatzUtils.Content.Projectiles;

public class GelatinousGrenadierProjectile : ModProjectile
{
    bool shouldExplode;
    float velocityX;
    float velocityY;
    public override void SetDefaults()
    {
        Projectile.CloneDefaults(ProjectileID.ThrowingKnife);
        
        Projectile.penetrate = -1;
        Projectile.knockBack = 10f;
    }

    
    public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
    {
        velocityX = Projectile.velocity.X;
        velocityY = Projectile.velocity.Y;
        shouldExplode = true;
        
    }
    public override bool OnTileCollide(Vector2 oldVelocity)
    {
        velocityX = Projectile.velocity.X;
        velocityY = Projectile.velocity.Y;
        //oldVelocity.X = 0;
        //oldVelocity.Y = 0;
        //Projectile.velocity = new Vector2(0, 0);
        shouldExplode = true;
        return false;
    }
    
    public override void AI()
    {
        if (shouldExplode)
        {
            Projectile.timeLeft = 0;
        }
        if (Projectile.owner == Main.myPlayer && Projectile.timeLeft == 0)
        {
            Projectile.tileCollide = false;
            Projectile.alpha = 255;
            Projectile.position = Projectile.Center;
            Projectile.width = 125;
            Projectile.height = 125;
            Projectile.Center = Projectile.position;
            Projectile.friendly = true;
            Projectile.hostile = true;
            Projectile.damage = 44;
            Projectile.knockBack = 10f;
        }
    }


    public override void Kill(int timeLeft)
    {

        // Play explosion sound
        // Smoke Dust spawn
        for (int i = 0; i < 80; i++)
        {
            int dustIndex = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width,
                Projectile.height, DustID.Smoke, 0f, 0f, 100, default, 2f);
            Main.dust[dustIndex].velocity *= 1.4f;
        }

        // Fire Dust spawn
        for (int i = 0; i < 140; i++)
        {
            int dustIndex = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width,
                Projectile.height, DustID.Torch, 0f, 0f, 100, default, 3f);
            Main.dust[dustIndex].noGravity = true;
            Main.dust[dustIndex].velocity *= 5f;
            dustIndex = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width,
                Projectile.height, DustID.Torch, 0f, 0f, 100, default, 2f);
            Main.dust[dustIndex].velocity *= 3f;
        }

        /*// Large Smoke Gore spawn
        for (int g = 0; g < 2; g++)
        {
            var projectileSource = Projectile.GetSource_FromThis();
            int goreIndex = GoreID.Smoke1;
            Gore.NewGore(projectileSource, Projectile.position,
                new Vector2(Main.rand.Next(61, 64), Main.rand.Next(61, 64)), goreIndex);
            Main.gore[goreIndex].scale = 1.5f;
            Main.gore[goreIndex].velocity.X = Main.gore[goreIndex].velocity.X + 1.5f;
            Main.gore[goreIndex].velocity.Y = Main.gore[goreIndex].velocity.Y + 1.5f;
            goreIndex = GoreID.Smoke2;
            Gore.NewGore(projectileSource, Projectile.position,
                new Vector2(Main.rand.Next(61, 64), Main.rand.Next(61, 64)), goreIndex);
            Main.gore[goreIndex].scale = 1.5f;
            Main.gore[goreIndex].velocity.X = Main.gore[goreIndex].velocity.X - 1.5f;
            Main.gore[goreIndex].velocity.Y = Main.gore[goreIndex].velocity.Y + 1.5f;
            goreIndex = GoreID.Smoke3;
            Gore.NewGore(projectileSource, Projectile.position,
                new Vector2(Main.rand.Next(61, 64), Main.rand.Next(61, 64)), goreIndex);
            Main.gore[goreIndex].scale = 1.5f;
            Main.gore[goreIndex].velocity.X = Main.gore[goreIndex].velocity.X + 1.5f;
            Main.gore[goreIndex].velocity.Y = Main.gore[goreIndex].velocity.Y - 1.5f;
            goreIndex = GoreID.Smoke1;
            Gore.NewGore(projectileSource, Projectile.position,
                new Vector2(Main.rand.Next(61, 64), Main.rand.Next(61, 64)), goreIndex);
            Main.gore[goreIndex].scale = 1.5f;
            Main.gore[goreIndex].velocity.X = Main.gore[goreIndex].velocity.X - 1.5f;
            Main.gore[goreIndex].velocity.Y = Main.gore[goreIndex].velocity.Y - 1.5f;
        }*/

        Projectile.position.X = Projectile.position.X + (float)(Projectile.width / 2f);
        Projectile.position.Y = Projectile.position.Y + (float)(Projectile.height / 2f);
        Projectile.width = 10;
        Projectile.height = 10; 
        Projectile.position.X = Projectile.position.X - (float)(Projectile.width / 2f);
        Projectile.position.Y = Projectile.position.Y - (float)(Projectile.height / 2f);

    }

    public override void OnHitPlayer(Player target, int damage, bool crit)
    {
        if (target == Main.player[Projectile.owner])
        {
            target.velocity *= 0.5f;
            target.velocity += new Vector2(-Math.Clamp(velocityX * 3, -10f, 10f), -Math.Clamp(velocityY * 3, -20f, 20f));
        }
    }
    public override bool CanHitPlayer(Player target)
    {
        if (target.statLife < 100)
        {
            target.velocity *= 0.5f;
            target.velocity += new Vector2(-Math.Clamp(velocityX * 3, -10f, 10f), -Math.Clamp(velocityY * 3, -20f, 20f));
            return false;
        }
        return true;
    }
}
