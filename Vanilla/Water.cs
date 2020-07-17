using LiquidAPI.ID;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
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

			
            LiquidAPI.interactionResult[Type, LiquidID.Honey] = TileID.HoneyBlock;
            LiquidAPI.interactionResult[Type, LiquidID.Lava] = TileID.Obsidian;
            Name = "Water";
		}

		public override bool CanKillTile(int x,int y) => TileObjectData.CheckWaterDeath(Main.tile[x,y]);
	}
}
