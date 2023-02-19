using TwistedTerraria.Content.Projectiles;
using Terraria.ID;
using Terraria.ModLoader;

namespace TwistedTerraria.Content.Items.Weapons;

public class StoneCup : ModItem
{

    public override void SetStaticDefaults()
    {
        Tooltip.SetDefault("Inelegant yet effective \n splits into rocks if it hits an enemy");
    }
    public override void SetDefaults()
    {
        Item.CloneDefaults(ItemID.Grenade);
        Item.shoot =  ModContent.ProjectileType<StoneCupProjectile>();
        Item.shootSpeed *= 3.25f;
        Item.useTime = 30;
        Item.useAnimation = 30;
        Item.maxStack = 9999;
        Item.consumable = true;
        Item.autoReuse = true;
        Item.DamageType = DamageClass.Ranged;
        Item.damage = 75;
        Item.knockBack = 10;
        Item.width = 30;
        Item.height = 30;
    }
}
