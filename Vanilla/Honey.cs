using Microsoft.Xna.Framework;

namespace LiquidAPI.Vanilla
{
	public class Honey : ModLiquid
	{
		public override string Name => "Honey";
		public override Color LiquidColor => new Color(215, 131, 8);
		//public override float LiquidOpacity=>0.5f;

		public override byte WaterfallLength=>2;
		public override float DefaultOpacity=>0.95f;
		public override byte WaveMaskStrength=>0;
		public override byte ViscosityMask=>240;

		public override bool Autoload(ref string name)=>false;
	}
}
