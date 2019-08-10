using Microsoft.Xna.Framework;

namespace LiquidAPI.Vanilla
{
	public class Honey : ModLiquid
	{
		public override bool Autoload(ref string name,ref string texture)=>false;

		public override void SetDefaults()
		{
			DisplayName.SetDefault("Honey");
			LiquidColor = new Color(215, 131, 8);
			WaterfallLength = 2;
			DefaultOpacity = 0.95f;
			WaveMaskStrength = 0;
			ViscosityMask = 240;
		}
	}
}
