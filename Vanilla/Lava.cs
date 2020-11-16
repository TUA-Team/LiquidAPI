using LiquidAPI.ID;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ObjectData;

namespace LiquidAPI.Vanilla
{
    public class Lava : ModLiquid
    {
        public override bool Autoload(ref string name, ref string texture, ref string fancyTexture) => false;

        public override Color LiquidColor => new Color(253, 62, 3);

        public override void SetDefaults()
        {
            DisplayName.SetDefault("Lava");
            WaterfallLength = 3;
            DefaultOpacity = 0.95f;
            WaveMaskStrength = 0;
            ViscosityMask = 200;
            Name = "Lava";

            LiquidAPI.interactionResult[Type, LiquidID.Honey] = TileID.CrispyHoneyBlock;
            LiquidAPI.interactionResult[Type, LiquidID.Water] = TileID.Obsidian;
            RegisterMapColorCode(new Color(253, 32, 3));
        }

        public override bool CanKillTile(int x, int y) => TileObjectData.CheckLavaDeath(Main.tile[x, y]);
    }
}
