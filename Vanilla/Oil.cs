using LiquidAPI.LiquidMod;
using LiquidAPI.Test;
using Microsoft.Xna.Framework;
using Terraria.ID;

namespace LiquidAPI.Vanilla
{
    class Oil : ModLiquid
    {
        public override bool Autoload(ref string name,ref string texture, ref string fancyTexture)=>false;

        public override Color LiquidColor => Color.Black;

        public override void SetDefaults()
        {
            DisplayName.SetDefault("Oil");
            DefaultOpacity=1f;
            customDelay = 50;
            LiquidDust = new LiquidDust(DustID.Blood, 20, 1f, 2.5f, 1.3f, 100, true);
        }

        public override void PreDrawValueSet(ref bool bg, ref int style, ref float Alpha)
        {
            style = 12;
            Alpha = 0.2f;
        }


        public override int LiquidInteraction(LiquidRef liquidUp, LiquidRef liquidDown, LiquidRef liquidLeft, LiquidRef liquidRight, int x, int y)
        {
            if (liquidLeft.Type is Lava || liquidRight.Type is Lava || liquidDown.Type is Lava)
            {
                return TileID.Diamond;
            } else if (liquidLeft.Type is Water || liquidRight.Type is Water || liquidDown.Type is Water)
            {
                return TileID.Hellstone;
            } else if (liquidLeft.Type is WeirdLiquid || liquidRight.Type is WeirdLiquid || liquidDown.Type is WeirdLiquid)
            {
                return TileID.AdamantiteBeam;
            }

            return base.LiquidInteraction(liquidUp, liquidDown, liquidLeft, liquidRight, x, y);
        }
    }
}
