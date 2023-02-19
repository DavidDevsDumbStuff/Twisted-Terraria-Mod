using TwistedTerraria.Content.Dusts;
using TwistedTerraria.Content.Projectiles;
using TwistedTerraria.Content.Projectiles.ArcaneScytheProjectiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using ItemID = On.Terraria.ID.ItemID;

namespace TwistedTerraria.Content.Items.Weapons;

public class ArcaneScythe : ModItem
{
    int manaUsed;
    float overrideScale;
    int overrideHeight;
    
    public override void SetStaticDefaults()
    {
        Tooltip.SetDefault("A ever hungry demonic Scythe");
        Tooltip.SetDefault("Uses all your mana, Damage size and duration scales with mana used");
    }

    public override void SetDefaults()
    {
        Item.DamageType = DamageClass.Magic;
        Item.noMelee = true;
        Item.channel = true;
        Item.shoot = ModContent.ProjectileType<ArcaneScytheSwingProjectile>();
        Item.scale = 1f;
        Item.crit = -4;
        Item.useTime = 300;
        Item.useAnimation = 300;
        Item.knockBack = 0;
        Item.shootSpeed = 0;
        Item.noUseGraphic = true;
        Item.useStyle = 5; //
        Item.autoReuse = false;

    }

    public override bool? UseItem(Player player)
    {
        if (player.whoAmI == Main.myPlayer)
        {
            if (player.channel)
            {
                return true;
            }
            
        }

        return null;
    }

    public override Color? GetAlpha(Color lightColor)
    {
        return new Color(255, 255, 255);
    }

    public override Vector2? HoldoutOffset()
    {
        return new Vector2(-13, -3);
    }
    public override bool CanUseItem(Player player)
    {
        if (player.statMana < 20)   
        {
            return false;
        }

        return true;
    }
    public override bool CanShoot(Player player)
    {
        if (player.statMana < 20)   
        {
            return false;
        }

        return true;
    }


}
