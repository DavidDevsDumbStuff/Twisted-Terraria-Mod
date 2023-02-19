using TwistedTerraria.Content.Dusts;
using TwistedTerraria.Content.Projectiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.GameContent.Creative;
using Terraria.ModLoader;


namespace TwistedTerraria.Content.Items.Weapons
{
    public class OrbOfLight : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("will add tooltip later this is just for testing");

            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 32;
            Item.height = 32;

            Item.useStyle = ItemUseStyleID.Swing;
            Item.useTime = 20;
            Item.useAnimation = 20;
            Item.autoReuse = false;

            Item.DamageType = DamageClass.Melee;
            Item.damage = 10;
            Item.knockBack = 4;
            Item.crit = 4;

            Item.value = Item.buyPrice(gold: 15);
            Item.rare = ItemRarityID.Pink;
            Item.UseSound = SoundID.Item1;

            //Item.shoot = ModContent.ProjectileType<>();
            //Item.shootSpeed = 8f;
        }
    }
}
