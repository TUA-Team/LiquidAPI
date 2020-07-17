using LiquidAPI.ID;
using Terraria.ID;
using LiquidAPI.LiquidMod;
using LiquidAPI.Test;
using Microsoft.Xna.Framework;

namespace LiquidAPI.Vanilla
{
	public class Honey : ModLiquid
	{
		public override bool Autoload(ref string name,ref string texture, ref string fancyTexture)=>false;

        public override Color LiquidColor => new Color(215, 131, 8);

        public override void SetDefaults()
		{
			DisplayName.SetDefault("Honey");
            WaterfallLength = 2;
			DefaultOpacity = 0.95f;
			WaveMaskStrength = 0;
			ViscosityMask = 240;
            Name = "Honey";

            LiquidAPI.interactionResult[Type, LiquidID.Water] = TileID.HoneyBlock;
            LiquidAPI.interactionResult[Type, LiquidID.Lava] = TileID.CrispyHoneyBlock;
        }
    }
}
