using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ObjectData;

namespace LiquidAPI.Vanilla
{
	public class Lava : ModLiquid
	{
		public override bool Autoload(ref string name,ref string texture)=>false;

		public override void SetDefaults()
		{
			DisplayName.SetDefault("Lava");
			LiquidColor = new Color(253, 62, 3);
			WaterfallLength = 3;
			DefaultOpacity = 0.95f;
			WaveMaskStrength = 0;
			ViscosityMask = 200;
		}

		public override bool CanKillTile(int x,int y)=>TileObjectData.CheckLavaDeath(Main.tile[x,y]);
	}
}
