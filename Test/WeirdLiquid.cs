using LiquidAPI.LiquidMod;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;

namespace LiquidAPI.Test
{
	class WeirdLiquid : ModLiquid
	{
		public override void SetDefaults()
		{
			DisplayName.SetDefault("Test liquid");
            DefaultOpacity = 1f;
            LiquidDust = new LiquidDust(DustID.Marble, 20, 1f, 2.5f, 1.3f, 100, true);
		}

        public override int LiquidInteraction(LiquidRef liquidUp, LiquidRef liquidDown, LiquidRef liquidLeft, LiquidRef liquidRight, int x, int y)
        {
            return TileID.IceBlock;
        }

        public override void PreDrawValueSet(ref bool bg, ref int style, ref float Alpha)
		{
			style = 12;
			Alpha = 0.2f;
		}

        /// <summary>
        /// Not implemented yet
        /// </summary>
        /// <param name="target"></param>
        public override void PlayerInteraction(Player target)
        {
            Main.NewText("Touched test liquid");
        }

        /// <summary>
        /// Not implemented yet
        /// </summary>
        /// <param name="target"></param>
        public override void ItemInteraction(Item target)
        {
            Main.NewText("Item in weird liquid : " + target.Name);
        }
    }
}
