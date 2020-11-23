/*using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace LiquidAPI.Test
{
	class PlutonicWaste : ModLiquid
	{
		public override void SetDefaults()
		{
			DisplayName.SetDefault("Liquid Waste");
			defaultOpacity=0.5f;
			liquidColor=Color.GreenYellow;
		}

		public override void PreDrawValueSet(ref bool bg, ref int style, ref float Alpha)
		{
			style = 12;
			Alpha = 0.2f;
		}
		

		public override void PlayerInteraction(Player target)
		{
			Main.NewText("This is liquid waste");
		}

		public override void NPCInteraction(NPC target)
		{
			if (target.type == NPCID.GreenSlime || target.type == NPCID.BlueSlime || target.type == NPCID.PurpleSlime)
			{
				Vector2 position = target.Center;
				target.active = false;
				NPC.NewNPC((int) position.X, (int) position.Y, ModLoader.GetMod("TUA").NPCType("MutatedSludge"));
			}
		}
	}
}
*/