using BatzUtils.Content.Buffs;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace BatzUtils.Content.Items.Weapons;

public class WindTalons : ModItem
{

    public override void SetDefaults()
    {
        Item.CloneDefaults(ItemID.AdamantiteSword);
        Item.damage = 6;
    }

    //move this to the custome arkhalis projectile
    public override void OnHitNPC(Player player, NPC target, int damage, float knockBack, bool crit)
    {
        
        target.AddBuff(ModContent.BuffType<FrostbittenDebuff>(),Helpers.Seconds(3));
        for (int i = 0; i < 10; i++)
        {
            Dust.NewDust(target.Center, target.width, target.height, DustID.Frost);
        }

    }
}
