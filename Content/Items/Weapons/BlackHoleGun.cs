using TwistedTerraria.Content.Projectiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TwistedTerraria.Content.Items.Weapons;

public class BlackHoleGun : ModItem
{
    public override void SetDefaults() {
        // Common Properties
        Item.scale = 1.2f;
        // Use Properties
        Item.useTime = 30;
        Item.useAnimation = 15; 
        Item.useStyle = ItemUseStyleID.Shoot; 
   
        // Weapon Properties
        Item.DamageType = DamageClass.Ranged; 
        Item.damage = 20; 
        Item.knockBack = 5f; 
        Item.noMelee = true; 
        // Gun Properties
        Item.shoot = ModContent.ProjectileType<BlackHoleProjectile>();
        Item.shootSpeed = 16f; 
        //Item.useAmmo = AmmoID.Bullet;
    }

    public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage,
        ref float knockback)
    {
        position = player.Center + new Vector2(player.width + 15f * player.direction, -5f);
        base.ModifyShootStats(player, ref position, ref velocity, ref type, ref damage, ref knockback);
    }
}
