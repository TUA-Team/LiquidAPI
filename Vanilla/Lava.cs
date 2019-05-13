using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ObjectData;

namespace LiquidAPI.Vanilla
{
	public class Lava : ModLiquid
	{
		public override string Name => "Lava";
		public override Color LiquidColor => new Color(253, 62, 3);
		//public override float LiquidOpacity=>0.5f;

		public override byte WaterfallLength=>3;
		public override float DefaultOpacity=>0.95f;
		public override byte WaveMaskStrength=>0;
		public override byte ViscosityMask=>200;

		public override bool Autoload(ref string name)=>false;

		public override bool CanKillTile(int x,int y)=>TileObjectData.CheckLavaDeath(Main.tile[x,y]);
	}
}
