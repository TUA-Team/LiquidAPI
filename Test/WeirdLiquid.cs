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


        public override void PlayerInteraction(Player target)
        {
            Main.NewText("Touched test liquid");
        }

        public override void ItemInteraction(Item target)
        {
            Main.NewText("Item in weird liquid : " + target.Name);
        }
    }
}
