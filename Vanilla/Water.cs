using Microsoft.Xna.Framework;
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
			LiquidColor = new Color(51, 107, 249);
			WaterfallLength = 10;
			DefaultOpacity = 0.6f;
			WaveMaskStrength = 0;
			ViscosityMask = 0;
		}

		public override bool CanKillTile(int x,int y) => TileObjectData.CheckWaterDeath(Main.tile[x,y]);
	}
}
