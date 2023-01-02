using System.Collections.Generic;
using BatzUtils.Content.Dusts;
using BatzUtils.Content.Projectiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace BatzUtils.Content.Items.Weapons;

public class ChanneledSword : ModItem
{

    public override void SetDefaults()
    {
        Item.damage = 50;
        Item.width = 64;
        Item.height = 64;
        Item.useTime = 0;
        Item.useAnimation = 0;
        Item.channel = true;
        Item.noUseGraphic = true;
        Item.noMelee = true;
        Item.useStyle = 5;
        Item.autoReuse = false;
        Item.shoot = ModContent.ProjectileType<ChanneledSwordProjectile>();
        

    }

    


    public override bool CanUseItem(Player player)
    {
        if (player.ownedProjectileCounts[ModContent.ProjectileType<Projectiles.ChanneledSwordProjectile>()] > 0)
        {
            Helpers.PrintText("You already own this projectile", Color.Bisque);
            return false;
        }
        return true;

    }

   
  


}