using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;

namespace BatzUtils.Content.Projectiles;


public class BlackHoleProjectile : ModProjectile
{
    NPC[] npcsToPull;
    public override void OnSpawn(IEntitySource source)
    {
        npcsToPull = null;
    }
    public override void SetDefaults()
    {
        Projectile.timeLeft = Helpers.Seconds(5);
        Projectile.width = 42;
        Projectile.height = 42;
        Projectile.penetrate = -1;
        Projectile.scale = 1f;
        Projectile.friendly = true;
    }

    public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
    {
        Projectile.velocity = Vector2.Zero;
    }
    public override void OnHitPlayer(Player target, int damage, bool crit)
    {
        Projectile.velocity = Vector2.Zero;
    }
    public override bool OnTileCollide(Vector2 oldVelocity)
    {
        Projectile.velocity = Vector2.Zero;
        return false;
    }


    public override void AI()
    {
        Projectile.ai[1]++;
        if (Projectile.velocity == Vector2.Zero)
        {
            Projectile.ai[0]++;
            Projectile.rotation = MathHelper.ToRadians(Projectile.ai[0] * 12f);
            npcsToPull = Helpers.FindClosestNpcsProjectile(350f, Projectile);
            if (npcsToPull != null)
            {
                foreach (var npc in npcsToPull)
                {
                    if (npc == null || npc.boss)
                        continue;
                    Vector2 pullDir = (Projectile.position - npc.position).SafeNormalize(Vector2.Zero);
                    if (npc.Distance(Projectile.Center) < 20f)
                    {
                        npc.velocity = Vector2.Zero;
                    }
                    else
                    {
                        npc.velocity = pullDir * 3f;
                    }
                    
                }
            }
        }
        else
        {
            if (Projectile.ai[1] >= 20)
            {
                Projectile.velocity = Vector2.Zero;
                Projectile.ai[1] = 0;
            }
        }

    }
}
