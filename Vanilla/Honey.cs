using Terraria.ID;
using LiquidAPI.LiquidMod;
using LiquidAPI.Test;
using Microsoft.Xna.Framework;

namespace LiquidAPI.Vanilla
{
	public class Honey : ModLiquid
	{
		public override bool Autoload(ref string name,ref string texture)=>false;

        public override Color LiquidColor => new Color(215, 131, 8);

        public override void SetDefaults()
		{
			DisplayName.SetDefault("Honey");
            WaterfallLength = 2;
			DefaultOpacity = 0.95f;
			WaveMaskStrength = 0;
			ViscosityMask = 240;
		}

        public override int LiquidInteraction(LiquidRef liquidUp, LiquidRef liquidDown, LiquidRef liquidLeft, LiquidRef liquidRight, int x, int y)
        {
            if (liquidLeft.Type is Lava || liquidRight.Type is Lava || liquidDown.Type is Lava)
            {
                return TileID.CrispyHoneyBlock;
            } 
            else if (liquidLeft.Type is PlutonicWaste || liquidRight.Type is PlutonicWaste || liquidDown.Type is PlutonicWaste)
            {
                return TileID.LunarOre;
            }
                
            return base.LiquidInteraction(liquidUp, liquidDown, liquidLeft, liquidRight, x, y);
        }
    }
}
