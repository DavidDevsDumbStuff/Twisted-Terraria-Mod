using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TwistedTerraria.Content.Items.Weapons;

public class BambooShoot : ModItem
{
    public override void SetStaticDefaults()
    {
        Tooltip.SetDefault("A fast hitting javelin that steals hp");
    }
    public override void SetDefaults()
    {
        Item.CloneDefaults(ItemID.Javelin);
        Item.useStyle = ItemUseStyleID.Swing;
        Item.shoot =  ModContent.ProjectileType<Projectiles.BambooShootProjectile>();
        Item.useTime = 48;
        Item.useAnimation = 48;
        Item.maxStack = 1;
        Item.consumable = false;
        Item.autoReuse = true;
        Item.DamageType = DamageClass.Ranged;
        Item.damage = 14;
        Item.knockBack = 4;
        Item.crit = 2;
        

    }
}
