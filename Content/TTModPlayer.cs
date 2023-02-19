using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace TwistedTerraria.Content;

public class TTModPlayer : ModPlayer
{
    
    //Todo Multiplayer Networking
    
    public bool ScreenShake;
    public int TwirlerFruitCount;
    public int PlasmaPepperCount;
    public int GravAppleCount;
    public override void ModifyScreenPosition()
    {
        if (ScreenShake)
        {
            Main.screenPosition += Main.rand.NextVector2Circular(20, 15);
        }
        
    }
    public override void SaveData(TagCompound tag) {
        tag["TwirlerFruitCount"] = TwirlerFruitCount;
        tag["PlasmaPepperCount"] = PlasmaPepperCount;
        tag["GravAppleCount"] = GravAppleCount;
    }

    public override void LoadData(TagCompound tag) {
        TwirlerFruitCount = tag.GetInt("TwirlerFruitCount");
        PlasmaPepperCount = tag.GetInt("PlasmaPepperCount");
        GravAppleCount = tag.GetInt("GravAppleCount");
    }

    public override void PostUpdateMiscEffects()
    {
        
    }

    public override void PostUpdateEquips()
    {
        
    }
    public override void PostUpdateRunSpeeds()
    {
        if (!Player.mount._active)
        {
            Player.accRunSpeed *= 1 + (0.5f * TwirlerFruitCount/10) + (0.5f * PlasmaPepperCount/10);
            Player.runAcceleration *= 1 + (0.5f * TwirlerFruitCount/10) + (0.5f * PlasmaPepperCount/10);
            Player.moveSpeed*= 1 + (0.5f * TwirlerFruitCount/10) + (0.5f * PlasmaPepperCount/10);
            Player.maxRunSpeed*= 1 + (0.5f * TwirlerFruitCount/10) + (0.5f * PlasmaPepperCount/10);
        }

        Player.jumpSpeed += (0.5f * GravAppleCount/10);
        Player.jumpHeight += (5 * GravAppleCount/10);
        Player.wingTime += 0.5f * GravAppleCount/10;
        
        /*
        Helpers.PrintText($" max run speed: {Player.maxRunSpeed}",Color.Pink);
        Helpers.PrintText($" Move speed: {Player.moveSpeed}",Color.Pink);
        Helpers.PrintText($" run acc speed: {Player.runAcceleration}",Color.Pink);
        Helpers.PrintText($" acc run speed speed: {Player.accRunSpeed}",Color.Pink);
        */
        

       
    }
}
