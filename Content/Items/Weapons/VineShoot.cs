using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace BatzUtils.Content.Items.Weapons;

public class VineShoot : ModItem
{
    public override void SetStaticDefaults()
    {
        Tooltip.SetDefault("A fast hitting javelin that steals hp");
    }
    public override void SetDefaults()
    {
        Item.CloneDefaults(ItemID.Javelin);
        Item.useStyle = ItemUseStyleID.Swing;
        Item.shoot =  ModContent.ProjectileType<Projectiles.VineShootProjectile>();
        Item.maxStack = 1;
        Item.consumable = false;
        Item.autoReuse = true;
        Item.DamageType = DamageClass.Ranged;
        Item.damage = 6;
        Item.knockBack = 1;
        Item.crit = 2;
   


    }
}
