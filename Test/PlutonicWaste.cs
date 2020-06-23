using LiquidAPI.LiquidMod;
using LiquidAPI.Vanilla;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace LiquidAPI.Test
{
	class PlutonicWaste : ModLiquid
	{
        public override Color LiquidColor => Color.GreenYellow;

        public override void SetDefaults()
		{
			DisplayName.SetDefault("Liquid Waste");
			DefaultOpacity=0.5f;
            customDelay = 50;
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
				NPC.NewNPC((int) position.X, (int) position.Y, NPCID.MoonLordCore);
			}
		}

        public override int LiquidInteraction(LiquidRef liquidUp, LiquidRef liquidDown, LiquidRef liquidLeft, LiquidRef liquidRight, int x, int y)
        {
            if (liquidLeft.Type is Lava || liquidRight.Type is Lava || liquidDown.Type is Lava)
            {
                return TileID.Diamond;
            } else if (liquidLeft.Type is Water || liquidRight.Type is Water || liquidDown.Type is Water)
            {
                return TileID.Hellstone;
            } else if (liquidLeft.Type is WeirdLiquid || liquidRight.Type is WeirdLiquid || liquidDown.Type is WeirdLiquid)
            {
                return TileID.AdamantiteBeam;
            }

            return base.LiquidInteraction(liquidUp, liquidDown, liquidLeft, liquidRight, x, y);
        }
    }
}
