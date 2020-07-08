using LiquidAPI.LiquidMod;
using LiquidAPI.Test;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ObjectData;

namespace LiquidAPI.Vanilla
{
	public class Lava : ModLiquid
	{
		public override bool Autoload(ref string name,ref string texture, ref string fancyTexture)=>false;

        public override Color LiquidColor => new Color(253, 62, 3);

        public override void SetDefaults()
		{
			DisplayName.SetDefault("Lava");
            WaterfallLength = 3;
			DefaultOpacity = 0.95f;
			WaveMaskStrength = 0;
			ViscosityMask = 200;
		}

		public override bool CanKillTile(int x,int y)=>TileObjectData.CheckLavaDeath(Main.tile[x,y]);

        public override int LiquidInteraction(LiquidRef liquidUp, LiquidRef liquidDown, LiquidRef liquidLeft, LiquidRef liquidRight, int x, int y)
        {
            if (liquidLeft.Type is Honey || liquidRight.Type is Honey || liquidDown.Type is Honey)
            {
                return TileID.CrispyHoneyBlock;
            }
            else if (liquidLeft.Type is PlutonicWaste || liquidRight.Type is PlutonicWaste || liquidDown.Type is PlutonicWaste)
            {
                return TileID.Diamond;
            } 
            else if (liquidLeft.Type is WeirdLiquid || liquidRight.Type is WeirdLiquid || liquidDown.Type is WeirdLiquid)
            {
                return TileID.Emerald;
            }
                
            return base.LiquidInteraction(liquidUp, liquidDown, liquidLeft, liquidRight, x, y);
        }
    }
}
