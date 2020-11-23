/*using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ObjectData;

namespace LiquidAPI.Vanilla
{
	public class Lava : ModLiquid
	{
		public override bool Autoload(ref string name,ref string texture)=>false;

		public override void SetDefaults()
		{
			DisplayName.SetDefault("Lava");
			liquidColor = new Color(253, 62, 3);
			waterfallLength = 3;
			defaultOpacity = 0.95f;
			waveMaskStrength = 0;
			viscosityMask = 200;
		}

		public override bool CanKillWater(int x,int y)=>TileObjectData.CheckLavaDeath(Main.tile[x,y]);
	}
}
*/