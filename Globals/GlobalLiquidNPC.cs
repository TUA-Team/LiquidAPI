using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LiquidAPI.ID;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace LiquidAPI.Globals
{
    class GlobalLiquidNPC : GlobalNPC
    {
        public override bool InstancePerEntity => true;

        public Dictionary<int, bool> npcWet = new Dictionary<int, bool>();

        public override void ResetEffects(NPC npc)
        {
            foreach (int npcWetKey in npcWet.Keys)
            {
                npcWet[npcWetKey] = false;
            }
        }

        public void PostCollisionUpdate()
        {
            
        }

        public override bool PreAI(NPC npc)
        {
            
            return base.PreAI(npc);
        }

        public bool LavaWet()
        {
            return npcWet[LiquidID.Lava];
        }

        public bool HoneyWet()
        {
            return npcWet[LiquidID.Honey];
        }

        public bool WaterWet()
        {
            return npcWet[LiquidID.Water];
        }

        public bool ModdedLiquidWet(ModLiquid liquid)
        {
            return npcWet[liquid.Type];
        }

        public void SetLiquidWetState(int liquidID, bool state)
        {
            npcWet[liquidID] = state;
        }

        public void SpawnDust(int liquidID)
        {
            Color color = LiquidRegistry.GetLiquid(liquidID).LiquidColor;
            
        }
    }
}
