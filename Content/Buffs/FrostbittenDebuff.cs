using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TwistedTerraria.Content.Buffs;

public class FrostbittenDebuff : ModBuff
{
    public override void SetStaticDefaults() {
        DisplayName.SetDefault("Hypothermia"); // Buff display name
        Description.SetDefault("Slowing freezing to death"); // Buff description
        Main.debuff[Type] = true;  // Is it a debuff?
        Main.pvpBuff[Type] = true; // Players can give other players buffs, which are listed as pvpBuff
        Main.buffNoSave[Type] = true; // Causes this buff not to persist when exiting and rejoining the world
      
    }
    
    // Allows you to make this buff give certain effects to the given player
    public override void Update(NPC npc, ref int buffIndex)
    {
        
        Dust.NewDust(npc.Center, npc.width, npc.height, DustID.Frost);
        npc.GetGlobalNPC<FrostbittenNPCDebuff>().Frostbitten = true;
    }
}

public class FrostbittenNPCDebuff : GlobalNPC
{
    public override bool InstancePerEntity => true;
    public bool Frostbitten = false;

    public override void AI(NPC npc)
    {
        if (Frostbitten && !npc.boss)
        {
            npc.velocity.X *= 0.75f;
        }
    }
    public override void ResetEffects(NPC npc)
    {
        Frostbitten = false;
    }
    

    public override void UpdateLifeRegen(NPC npc, ref int damage)
    {
        npc.stepSpeed = 0;
        if (Frostbitten)
        {
            npc.lifeRegen = -8;
        }
        
    }
}


