using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ObjectData;

namespace LiquidAPI.Vanilla
{
	public class Water : ModLiquid
	{
		public override bool Autoload(ref string name,ref string texture, ref string fancyTexture)=>false;

        public override Color LiquidColor => new Color(51, 107, 249);

        public override void SetDefaults()
		{
			DisplayName.SetDefault("Water");
            WaterfallLength = 10;
			DefaultOpacity = 0.6f;
			WaveMaskStrength = 0;
			ViscosityMask = 0;
		}

		public override bool CanKillTile(int x,int y) => TileObjectData.CheckWaterDeath(Main.tile[x,y]);
	}
}
