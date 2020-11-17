﻿using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;

namespace LiquidAPI.Test
{
    class PlutonicWaste : ModLiquid
    {
        public override bool Autoload(ref string name, ref string texture, ref string fancyTexture)
        {
#if DEBUG
            return true;
#else
            return false;
#endif
        }

        public override Color LiquidColor => Color.GreenYellow;



        public override void SetDefaults()
        {
            DisplayName.SetDefault("Liquid Waste");
            DefaultOpacity = 0.5f;
            customDelay = 50;
            LiquidDust = new LiquidDust(DustID.AmberBolt, 20, 1f, 2.5f, 1.3f, 100, true);

            RegisterMapColorCode(new Color(0, 255, 0));
        }

        public override void PreDrawValueSet(ref bool bg, ref int style, ref float Alpha)
        {
            style = 12;
            Alpha = 0.2f;
        }

        public override void NPCInteraction(NPC target)
        {
            if (target.type == NPCID.GreenSlime || target.type == NPCID.BlueSlime || target.type == NPCID.PurpleSlime)
            {
                Vector2 position = target.Center;
                target.active = false;
                NPC.NewNPC((int)position.X, (int)position.Y, NPCID.MoonLordCore);
            }
        }

    }
}
