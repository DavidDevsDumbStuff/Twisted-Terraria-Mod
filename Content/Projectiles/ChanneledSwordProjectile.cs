using System;
using TwistedTerraria.Content.Items.Weapons;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Enums;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace TwistedTerraria.Content.Projectiles;

public class ChanneledSwordProjectile : ModProjectile
{

    float projectileSpeed = 8;
    public override void SetStaticDefaults()
    {
        ProjectileID.Sets.NeedsUUID[Projectile.type] = true;

    }
    public override void SetDefaults()
    {
        Projectile.width = 128;
        Projectile.height = 128;
        Projectile.friendly = true;
        Projectile.penetrate = -1;
        Projectile.tileCollide = false;
        Projectile.ignoreWater = true;

    }

    public override void AI()
    {
        Player player = Main.player[Projectile.owner];
        if (Main.LocalPlayer.whoAmI == Projectile.owner)
        {

            //check distance between projectile and player for despawn
            var distance = Projectile.Distance(player.Center);
            if (!player.channel && distance < 50 || player.channel &&  player.HeldItem.type != ModContent.ItemType<ChanneledSword>() && distance < 50 || player.noItems || player.CCed)
            {
                //Kill projectile
                 Projectile.Kill();
            }
            else if (player.channel && player.HeldItem.type == ModContent.ItemType<ChanneledSword>())
            {
                //Move towards mouse
                projectileSpeed += 0.1f;
                Projectile.velocity =
                    (Main.MouseWorld - Projectile.Center).SafeNormalize(Vector2.Zero) * projectileSpeed;

                //Reset acceleration if close to destination
                if (Projectile.Distance(Main.MouseWorld) < 30)
                {
                    projectileSpeed = 8;
                }
            }
            else
            {
               
                //Adds some acceleration
                projectileSpeed += 0.1f;
                //Move towards Player
                Projectile.velocity = (player.Center - Projectile.Center).SafeNormalize(Vector2.Zero) * projectileSpeed;
                if (Projectile.Distance(player.Center) < 30)
                {
                    projectileSpeed = 8;

                }
            }
        }

        //Make sure projectile doesnt time out
        Projectile.timeLeft = Helpers.Seconds(10);
        //Spin the projectile in the direction the player is facing
        Projectile.rotation += 0.5f * player.direction;
        //Update the sprite direction to the player direction
        Projectile.spriteDirection = player.direction;

        // reset rotation after a full rotation in eithier direction;
        if (Projectile.rotation > MathHelper.TwoPi)
        {
            Projectile.rotation -= MathHelper.TwoPi;
        }
        else if (Projectile.rotation < 0)
        {
            Projectile.rotation += MathHelper.TwoPi;
        }
    }



}
