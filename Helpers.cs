using System;
using System.Collections.Generic;
using System.Reflection;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Chat;
using Terraria.Localization;
using Terraria.ID;



namespace BatzUtils;

public static class Helpers
{

    public static int Seconds(float seconds)
    {
        return (int) (seconds * 60f);
    }
    public static void PrintText(string text, Color color)
    {
        if (Main.netMode == NetmodeID.SinglePlayer)
        {
            Main.NewText(text, new Color?(color));
            return;
        }
        if (Main.netMode == NetmodeID.Server)
        {
            ChatHelper.BroadcastChatMessage(NetworkText.FromLiteral(text), color, -1);
        }
    }
    public static void ClearText()
    {
        List<Tuple<string, Color>> cachedMessages =  (List<Tuple<string, Color>>) typeof(ChatHelper).GetField("_cachedMessages",BindingFlags.Static|BindingFlags.NonPublic)?.GetValue(null);
        if (cachedMessages != null)
        {
            cachedMessages.Clear();
            typeof(ChatHelper).GetField("_cachedMessages",BindingFlags.Static|BindingFlags.NonPublic)?.SetValue(null,cachedMessages);
            PrintText("Messages Cleared", Color.Purple);
        }
        else
        {
            PrintText("CachedMessages not found", Color.Purple);
        }
    }
    
    
    //Credit to Example mod
    
    public static NPC[] FindClosestNpcsProjectile(float maxDetectDistance,Projectile projectile) {
        NPC[] NPCsInRange = new NPC[Main.maxNPCs];

        // Using squared values in distance checks will let us skip square root calculations, drastically improving this method's speed.
        float sqrMaxDetectDistance = maxDetectDistance * maxDetectDistance;

        // Loop through all NPCs(max always 200)
        for (int k = 0; k < Main.maxNPCs; k++) {
            NPC target = Main.npc[k];
            // Check if NPC able to be targeted. It means that NPC is
            // 1. active (alive)
            // 2. chaseable (e.g. not a cultist archer)
            // 3. max life bigger than 5 (e.g. not a critter)
            // 4. can take damage (e.g. moonlord core after all it's parts are downed)
            // 5. hostile (!friendly)
            // 6. not immortal (e.g. not a target dummy)
            if (target.CanBeChasedBy()) {
                // The DistanceSquared function returns a squared distance between 2 points, skipping relatively expensive square root calculations
                float sqrDistanceToTarget = Vector2.DistanceSquared(target.Center, projectile.Center);
                // Check if it is within the radius
                if (sqrDistanceToTarget < sqrMaxDetectDistance) {
                    sqrMaxDetectDistance = sqrDistanceToTarget;
                    NPCsInRange[k] = target;
                }
            }
        }
        return NPCsInRange;
    }
    public static NPC FindClosestNpcProjectile(float maxDetectDistance,Projectile projectile) {
        NPC closestNPC = null;

        // Using squared values in distance checks will let us skip square root calculations, drastically improving this method's speed.
        float sqrMaxDetectDistance = maxDetectDistance * maxDetectDistance;

        // Loop through all NPCs(max always 200)
        for (int k = 0; k < Main.maxNPCs; k++) {
            NPC target = Main.npc[k];
            // Check if NPC able to be targeted. It means that NPC is
            // 1. active (alive)
            // 2. chaseable (e.g. not a cultist archer)
            // 3. max life bigger than 5 (e.g. not a critter)
            // 4. can take damage (e.g. moonlord core after all it's parts are downed)
            // 5. hostile (!friendly)
            // 6. not immortal (e.g. not a target dummy)
            if (target.CanBeChasedBy()) {
                // The DistanceSquared function returns a squared distance between 2 points, skipping relatively expensive square root calculations
                float sqrDistanceToTarget = Vector2.DistanceSquared(target.Center, projectile.Center);

                // Check if it is within the radius
                if (sqrDistanceToTarget < sqrMaxDetectDistance) {
                    sqrMaxDetectDistance = sqrDistanceToTarget;
                    closestNPC = target;
                }
            }
        }
        return closestNPC;
    }
    public static NPC FindClosestNpcPlayer(float maxDetectDistance) {
        NPC closestNPC = null;

        // Using squared values in distance checks will let us skip square root calculations, drastically improving this method's speed.
        float sqrMaxDetectDistance = maxDetectDistance * maxDetectDistance;

        // Loop through all NPCs(max always 200)
        for (int k = 0; k < Main.maxNPCs; k++) {
            NPC target = Main.npc[k];
            // Check if NPC able to be targeted. It means that NPC is
            // 1. active (alive)
            // 2. chaseable (e.g. not a cultist archer)
            // 3. max life bigger than 5 (e.g. not a critter)
            // 4. can take damage (e.g. moonlord core after all it's parts are downed)
            // 5. hostile (!friendly)
            // 6. not immortal (e.g. not a target dummy)
            if (target.active && target.CanBeChasedBy()) {
                // The DistanceSquared function returns a squared distance between 2 points, skipping relatively expensive square root calculations
                float sqrDistanceToTarget = Vector2.DistanceSquared(target.Center, Main.LocalPlayer.Center);

                // Check if it is within the radius
                if (sqrDistanceToTarget < sqrMaxDetectDistance) {
                    sqrMaxDetectDistance = sqrDistanceToTarget;
                    closestNPC = target;
                }
            }
        }
        return closestNPC;
    }
    public static Player FindClosestPlayer(float maxDetectDistance, Entity entity) {
        Player closestPlayer = null;

        // Using squared values in distance checks will let us skip square root calculations, drastically improving this method's speed.
        float sqrMaxDetectDistance = maxDetectDistance * maxDetectDistance;

        // Loop through all NPCs(max always 200)
        for (int k = 0; k < Main.player.Length; k++) {
            Player target = Main.player[k];
            // Check if NPC able to be targeted. It means that NPC is
            // 1. active (alive)
            // 2. chaseable (e.g. not a cultist archer)
            // 3. max life bigger than 5 (e.g. not a critter)
            // 4. can take damage (e.g. moonlord core after all it's parts are downed)
            // 5. hostile (!friendly)
            // 6. not immortal (e.g. not a target dummy)
            // The DistanceSquared function returns a squared distance between 2 points, skipping relatively expensive square root calculations
            if (!target.dead && !target.immune)
            {
                float sqrDistanceToTarget = Vector2.DistanceSquared(target.Center, entity.Center);
                // Check if it is within the radius
                if (sqrDistanceToTarget < sqrMaxDetectDistance) {
                    sqrMaxDetectDistance = sqrDistanceToTarget;
                    closestPlayer = target;
                }
            }
            
        }
        return closestPlayer;
    }
}
