using System;
using BatzUtils.Content.Projectiles.MeleeProjectiles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace BatzUtils.Content.Items.Tools;

public class CustomAxe : ModItem
{
    public override void SetDefaults()
    {
        Item.width = 0;
        Item.height = 0;
        Item.useStyle = 5;
        Item.useTime = 1;
        Item.useAnimation = 1;
        Item.channel = true;
        Item.autoReuse = true;
        Item.damage = 50;
        Item.noUseGraphic = true;
        Item.noMelee = true;
        Item.shoot = ModContent.ProjectileType<CustomAxeProjectile>();
        
    }


    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type,
        int damage, float knockback)
    {
        if (player.ownedProjectileCounts[ModContent.ProjectileType<CustomAxeProjectile>()] > 0)
        {
            return false;
        }
        return true;
        
    }


   
}
