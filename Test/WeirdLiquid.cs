using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;

namespace LiquidAPI.Test
{
    class WeirdLiquid : ModLiquid
    {
        public override Texture2D texture
        {
            get { return mod.GetTexture("Texture/water/BestWater2"); }
        }

        public override string name => "Test liquid";

        public override Color liquidColor => Color.White;

        public override bool Autoload(ref string name)
        {
            return true;
        }

        public override void PreDrawValueSet(ref bool bg, ref int style, ref float Alpha)
        {
            style = 12;
            Alpha = 0.2f;
        }

        public override float SetLiquidOpacity()
        {
            return 1f;
        }

        public override void PlayerInteraction(Player target)
        {
            Main.NewText("Touched test liquid");
        }

        public override void ItemInteraction(Item target)
        {
            Main.NewText("Item in weird liquid : " + target.Name);
        }
    }
}
