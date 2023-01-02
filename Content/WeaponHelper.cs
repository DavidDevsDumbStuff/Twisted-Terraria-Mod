using Microsoft.Xna.Framework;
using Terraria;

namespace BatzUtils.Content;

public static class WeaponHelper
{
    public static void ApplyKickBackOmnidirectional(Player player, float pushBackAmount)
    {
        if (player.whoAmI == Main.myPlayer)
        {
            Vector2 dir = ( player.Center - Main.MouseWorld).SafeNormalize(Vector2.Zero);
            player.velocity += new Vector2(dir.X * pushBackAmount, dir.Y * pushBackAmount);
        }
    }
}
