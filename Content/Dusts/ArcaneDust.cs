using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace TwistedTerraria.Content.Dusts;

public class ArcaneDust : ModDust
{
    Player _player;
    public Entity Target;
    
    int speed = 5;
    public override void OnSpawn(Dust dust) {
   
        _player = Main.LocalPlayer;
        dust.noGravity = true;
        dust.velocity = (_player.Center - dust.position).SafeNormalize(Vector2.Zero) * speed;
    }

    public override bool Update(Dust dust)
    {
        if (Target == null)
        {
            Target = _player;
        }
        dust.velocity = ((Target.Center - dust.position).SafeNormalize(Vector2.Zero) * speed )+ Target.velocity;
        dust.rotation = dust.velocity.ToRotation();
        dust.position += dust.velocity;
        if (Target.Distance(dust.position) < 30)
        {
            dust.active = false;
        }
        return false;
    }
    public override Color? GetAlpha(Dust dust, Color lightColor)
    {
        return new Color(lightColor.R, lightColor.G, lightColor.B, 25);
    }
}

