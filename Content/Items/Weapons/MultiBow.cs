using TwistedTerraria.Content.Projectiles;
using IL.Terraria;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Player = Terraria.Player;

namespace TwistedTerraria.Content.Items.Weapons;

public class MultiBow : ModItem
{
    public override void SetStaticDefaults()
    {
        Tooltip.SetDefault("Makes wooden arrow shoot in lots of little ones");
    }
    public override void SetDefaults()
    {
        Item.CloneDefaults(ItemID.PlatinumBow);
        Item.damage = 20;
    }
    
    public override Vector2? HoldoutOffset()
    {
        return new Vector2(-8f, 0f);
    }
    
    public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage,
        ref float knockback)
    {
        if (type == ProjectileID.WoodenArrowFriendly)
        {
            type = ModContent.ProjectileType<SplittingArrow>();
        }
    }
}
