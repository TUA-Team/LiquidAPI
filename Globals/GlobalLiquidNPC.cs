using System;
using System.Collections.Concurrent;
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

        

        public ConcurrentDictionary<int, bool> npcWet = new ConcurrentDictionary<int, bool>()
        {
            [0] = false,
            [1] = false,
            [2] = false
        };

        public GlobalLiquidNPC()
        {
            if (npcWet.Count != LiquidRegistry.liquidList.Count)
            {
                for (int i = 3; i < LiquidRegistry.liquidList.Count; i++)
                {
                    npcWet.TryAdd(i, false);
                }
            }
        }

        public override void ResetEffects(NPC npc)
        {
            foreach (int npcWetKey in npcWet.Keys)
            {
                npcWet[npcWetKey] = false;
            }
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

        public int GetFirstLiquidWet()
        {
            foreach (int npcWetKey in npcWet.Keys)
            {
                if(npcWetKey < 2)
                    continue;
                if (npcWet[npcWetKey])
                {
                    return npcWetKey;
                }
            }
            return 0;
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
