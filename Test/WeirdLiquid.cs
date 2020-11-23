/*using Microsoft.Xna.Framework;
using Terraria;

namespace LiquidAPI.Test
{
	class WeirdLiquid : ModLiquid
	{
		public override void SetDefaults()
		{
			DisplayName.SetDefault("Test liquid");
			liquidColor = Color.White;
			defaultOpacity = 1f;
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
*/