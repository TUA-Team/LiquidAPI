using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace LiquidAPI.LiquidMod
{
	public class LiquidPlayer : ModPlayer
	{
		public override void PostUpdate()
		{
			bool[] liquidCollision = ModdedWetCollision(player.Center, player.width, player.height);
			/*for (byte i = 0; i < LiquidRegistry.liquidList.; i++)
			{
				if (liquidCollision[i])
				{
					LiquidRegistry.PlayerInteraction(i, player);
				}
			}*/
		}

		private static bool[] ModdedWetCollision(Vector2 _position, int _width, int _height)
		{
			bool[] moddedLiquid = new bool[256];

			for (int i = 0; i < moddedLiquid.Length; i++)
				moddedLiquid[i] = false;

			Vector2 vector = new Vector2(_position.X + (float) (_width / 2), _position.Y + (float) (_height / 2));
			int largestWidth = 10;
			int largestHeight = _height / 2;

			if (largestWidth > _width)
				largestWidth = _width;

			if (largestHeight > _height)
				largestHeight = _height;

			vector = new Vector2(vector.X - (float) (largestWidth / 2), vector.Y - (float) (largestHeight / 2));

			int num3 = Utils.Clamp<int>((int) (_position.X / 16f) - 1, 0, Main.maxTilesX - 1);
			int num4 = Utils.Clamp<int>((int) ((_position.X + (float) _width) / 16f) + 2, 0, Main.maxTilesX - 1);
            int num5 = Utils.Clamp<int>((int) (_position.Y / 16f) - 1, 0, Main.maxTilesY - 1);
			int num6 = Utils.Clamp<int>((int) ((_position.Y + (float) _height) / 16f) + 2, 0, Main.maxTilesY - 1);

			for (int x = num3; x < num4; x++)
			{
				for (int y = num5; y < num6; y++)
				{
					if (Main.tile[x, y] != null)
					{
						LiquidRef liquid = LiquidCore.grid[x, y];

						if (liquid.HasLiquid())
						{
							Vector2 vector2 = new Vector2(x * 16, y * 16);

							int num7 = 16;
							float num8 = (float) (256 - (int) Main.tile[x, y].liquid);

						    num8 /= 32f;
							vector2.Y += num8 * 2f;
							num7 -= (int) (num8 * 2f);

							if (vector.X + (float) largestWidth > vector2.X && vector.X < vector2.X + 16f && vector.Y + (float) largestHeight > vector2.Y && vector.Y < vector2.Y + (float) num7)
								for (byte b = 3; b < 2 + LiquidRegistry.liquidList.Count; b++)
									if (LiquidCore.liquidGrid[x, y].data == b)
										moddedLiquid[b] = true;
						}
						else if (Main.tile[x, y].active() && Main.tile[x, y].slope() != 0 && y > 0 && Main.tile[x, y - 1] != null && Main.tile[x, y - 1].liquid > 0)
						{
							liquid = LiquidCore.grid[x, y - 1];
							Vector2 vector2 = new Vector2(x * 16, y * 16);
							int num9 = 16;

							if (vector.X + (float) largestWidth > vector2.X && vector.X < vector2.X + 16f && vector.Y + (float) largestHeight > vector2.Y && vector.Y < vector2.Y + (float) num9)
									for (byte b = 3; b < 2 + LiquidRegistry.liquidList.Count; b++)
										if (LiquidCore.liquidGrid[x, y].data == b)
											moddedLiquid[b] = true;
						}
					}
				}
			}

			return moddedLiquid;
		}
	}
}