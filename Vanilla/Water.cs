using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ObjectData;

namespace LiquidAPI.Vanilla
{
	public class Water : ModLiquid
	{
		public override string Name => "Water";
		public override Color LiquidColor => new Color(51, 107, 249);
		//public override float LiquidOpacity=>0.5f;

		public override byte WaterfallLength=>10;
		public override float DefaultOpacity=>0.6f;
		public override byte WaveMaskStrength=>0;
		public override byte ViscosityMask=>0;

		public override bool Autoload(ref string name)=>false;

		public override bool CanKillTile(int x,int y) => TileObjectData.CheckWaterDeath(Main.tile[x,y]);
	}
}
