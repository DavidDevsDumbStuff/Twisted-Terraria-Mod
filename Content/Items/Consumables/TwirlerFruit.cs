using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace BatzUtils.Content.Items.Consumables;

public class TwirlerFruit: ModItem
{
    public const int MaxTwirlerFruitCount = 10;
    
    public override void SetStaticDefaults() {
        Tooltip.SetDefault("Permanently increases movespeed by 5% up to 50%");
    }
    public override void SetDefaults()
    {
        Item.CloneDefaults(ItemID.LifeCrystal);
    }
    
    public override bool CanUseItem(Player player) {
        // This check prevents this item from being used before vanilla health upgrades are maxed out.
        return player.GetModPlayer<TTModPlayer>().TwirlerFruitCount < 10;
    }

    public override bool? UseItem(Player player)
    {
        player.GetModPlayer<TTModPlayer>().TwirlerFruitCount++;
        return true;
    }
}
