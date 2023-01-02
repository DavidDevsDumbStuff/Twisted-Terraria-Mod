
using BatzUtils.Content.Projectiles;
using Terraria.ID;
using Terraria.ModLoader;

namespace BatzUtils.Content.Items.Weapons;

public class GelatinousGrenadier : ModItem
{
    public override void SetDefaults()
    {
        Item.CloneDefaults(ItemID.ThrowingKnife);
        Item.useAnimation = Helpers.Seconds(0.75f);
        Item.useTime = Helpers.Seconds(0.75f);
        Item.DamageType = DamageClass.Ranged;
        Item.autoReuse = true;
        Item.damage = 44;
        Item.noMelee = true;
        Item.consumable = true;
        Item.knockBack = 10f;
        Item.shoot = ModContent.ProjectileType<GelatinousGrenadierProjectile>();
    }

    public override void AddRecipes()
    {
        CreateRecipe(50)
            .AddIngredient(ItemID.ThrowingKnife, 50)
            //to add explosive gel
            .AddTile(TileID.Solidifier)
            .Register();
    }
}
