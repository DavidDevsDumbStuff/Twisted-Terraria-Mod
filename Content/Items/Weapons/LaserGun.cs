using System.Collections.Generic;
using BatzUtils.Content.Dusts;
using BatzUtils.Content.Projectiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace BatzUtils.Content.Items.Weapons;

public class LaserGun : ModItem
{
    public override void SetStaticDefaults()
    {
        base.SetStaticDefaults();
    }
    public override void SetDefaults()
    {
        
        Item.shoot = ModContent.ProjectileType<Projectiles.LaserBeam>();
        Item.damage = 125;
        Item.useTime = 20;
        Item.useAnimation = 20;
        Item.shootSpeed = 14f;
        Item.width = 28;
        Item.height = 30;
        Item.channel = true;
        Item.noMelee = true;
        Item.useStyle = 5;
        Item.shootSpeed = 14f;
        

    }

    public override void HoldStyle(Player player, Rectangle heldItemFrame)
    {
        if (player.channel)
        {
            Vector2 spawnLocation = Main.rand.NextVector2Circular(200f, 200f);
            Dust.NewDustPerfect(player.Center - spawnLocation, ModContent.DustType<ArcaneDust>(), Vector2.Zero, Scale: 1.5f);
           
        }
      
        
    }

    

   
  


}