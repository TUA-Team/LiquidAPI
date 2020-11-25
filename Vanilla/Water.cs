using LiquidAPI.LiquidMod;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace LiquidAPI.Vanilla
{
	[Autoload(false)]
	public sealed class Water : ModLiquid
    {
		internal Water()
        {
        }

        public override void SetStaticDefaults()
        {
			DisplayName.SetDefault("Water");
			liquidColor = new Color(51, 107, 249);
			waterfallLength = 10;
			defaultOpacity = 0.6f;
			waveMaskStrength = 0;
			viscosityMask = 0;
		}

        public override void BeginUpdate(LiquidRef liquid)
        {
			if (liquid.Y > Main.UnderworldLayer)
			{
				byte b = 2;
				if (liquid.Amount < b)
				{
					b = liquid.Amount;
				}
				liquid.Amount -= b;
			}
		}
    }
}

/*using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ObjectData;

namespace LiquidAPI.Vanilla
{
	public class Water : ModLiquid
	{
		public override bool Autoload(ref string name,ref string texture)=>false;

		public override void SetDefaults()
		{
			DisplayName.SetDefault("Water");
			liquidColor = new Color(51, 107, 249);
			waterfallLength = 10;
			defaultOpacity = 0.6f;
			waveMaskStrength = 0;
			viscosityMask = 0;
		}

		public override bool CanKillWater(int x,int y) => TileObjectData.CheckWaterDeath(Main.tile[x,y]);
	}
}
*/