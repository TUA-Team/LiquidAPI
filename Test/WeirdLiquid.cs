using Terraria;
using Terraria.ID;

namespace LiquidAPI.Test
{
    class WeirdLiquid : ModLiquid
    {
        public override bool Autoload(ref string name, ref string texture, ref string fancyTexture)
        {
#if DEBUG
            return true;
#else
            return false;
#endif
        }

        public override void SetDefaults()
        {
            DisplayName.SetDefault("Test liquid");
            DefaultOpacity = 1f;
            LiquidDust = new LiquidDust(DustID.Marble, 20, 1f, 2.5f, 1.3f, 100, true);
        }

        public override void PreDrawValueSet(ref bool bg, ref int style, ref float Alpha)
        {
            style = 12;
            Alpha = 0.2f;
        }

        /// <summary>
        /// Not implemented yet
        /// </summary>
        /// <param name="target"></param>
        public override void PlayerInteraction(Player target)
        {
            Main.NewText("Touched test liquid");
        }

        /// <summary>
        /// Not implemented yet
        /// </summary>
        /// <param name="target"></param>
        public override void ItemInteraction(Item target)
        {
            Main.NewText("Item in weird liquid : " + target.Name);
        }
    }
}
