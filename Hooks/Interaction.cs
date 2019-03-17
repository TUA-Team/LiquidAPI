using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.Localization;

namespace LiquidAPI.Hooks
{
	internal static partial class LiquidHooks
	{
		private static void LiquidOnLavaCheck(On.Terraria.Liquid.orig_LavaCheck orig, int x, int y)
		{
			// TODO: Fix Later
			return;

			Tile tile1 = Main.tile[x - 1, y];
			Tile tile2 = Main.tile[x + 1, y];
			Tile tile3 = Main.tile[x, y - 1];
			Tile tile4 = Main.tile[x, y + 1];
			Tile tile5 = Main.tile[x, y];
			if (tile1.liquid > (byte)0 && !tile1.lava() || tile2.liquid > (byte)0 && !tile2.lava() ||
				tile3.liquid > (byte)0 && !tile3.lava())
			{
				int num = 0;
				int type = 56;
				if (!tile1.lava())
				{
					num += (int)tile1.liquid;
					tile1.liquid = (byte)0;
				}

				if (!tile2.lava())
				{
					num += (int)tile2.liquid;
					tile2.liquid = (byte)0;
				}

				if (!tile3.lava())
				{
					num += (int)tile3.liquid;
					tile3.liquid = (byte)0;
				}

				if (tile1.honey() || tile2.honey() || tile3.honey())
					type = 230;
				if (num < 24)
					return;
				if (tile5.active() && Main.tileObsidianKill[(int)tile5.type])
				{
					WorldGen.KillTile(x, y, false, false, false);
					if (Main.netMode == 2)
						NetMessage.SendData(17, -1, -1, (NetworkText)null, 0, (float)x, (float)y, 0.0f, 0, 0, 0);
				}

				if (tile5.active())
					return;
				tile5.liquid = (byte)0;
				tile5.lava(false);
				if (type == 56)
					Main.PlaySound(SoundID.LiquidsWaterLava, new Vector2((float)(x * 16 + 8), (float)(y * 16 + 8)));
				else
					Main.PlaySound(SoundID.LiquidsHoneyLava, new Vector2((float)(x * 16 + 8), (float)(y * 16 + 8)));
				WorldGen.PlaceTile(x, y, type, true, true, -1, 0);
				WorldGen.SquareTileFrame(x, y, true);
				if (Main.netMode != 2)
					return;
				NetMessage.SendTileSquare(-1, x - 1, y - 1, 3,
					type == 56 ? TileChangeType.LavaWater : TileChangeType.HoneyLava);
			}
			else
			{
				if (tile4.liquid <= (byte)0 || tile4.lava())
					return;
				bool flag = false;
				if (tile5.active() && TileID.Sets.ForceObsidianKill[(int)tile5.type] &&
					!TileID.Sets.ForceObsidianKill[(int)tile4.type])
					flag = true;
				if (Main.tileCut[(int)tile4.type])
				{
					WorldGen.KillTile(x, y + 1, false, false, false);
					if (Main.netMode == 2)
						NetMessage.SendData(17, -1, -1, (NetworkText)null, 0, (float)x, (float)(y + 1), 0.0f, 0, 0,
							0);
				}
				else if (tile4.active() && Main.tileObsidianKill[(int)tile4.type])
				{
					WorldGen.KillTile(x, y + 1, false, false, false);
					if (Main.netMode == 2)
						NetMessage.SendData(17, -1, -1, (NetworkText)null, 0, (float)x, (float)(y + 1), 0.0f, 0, 0,
							0);
				}

				if (!(!tile4.active() | flag))
					return;
				if (tile5.liquid < (byte)24)
				{
					tile5.liquid = (byte)0;
					tile5.liquidType(0);
					if (Main.netMode != 2)
						return;
					NetMessage.SendTileSquare(-1, x - 1, y, 3, TileChangeType.None);
				}
				else
				{
					int type = 56;
					if (tile4.honey())
						type = 230;
					tile5.liquid = (byte)0;
					tile5.lava(false);
					tile4.liquid = (byte)0;
					if (type == 56)
						Main.PlaySound(SoundID.LiquidsWaterLava,
							new Vector2((float)(x * 16 + 8), (float)(y * 16 + 8)));
					else
						Main.PlaySound(SoundID.LiquidsHoneyLava,
							new Vector2((float)(x * 16 + 8), (float)(y * 16 + 8)));
					WorldGen.PlaceTile(x, y + 1, type, true, true, -1, 0);
					WorldGen.SquareTileFrame(x, y + 1, true);
					if (Main.netMode != 2)
						return;
					NetMessage.SendTileSquare(-1, x - 1, y, 3,
						type == 56 ? TileChangeType.LavaWater : TileChangeType.HoneyLava);
				}
			}
		}

		private static void LiquidOnHoneyCheck(On.Terraria.Liquid.orig_HoneyCheck orig, int x, int y)
		{
			// TODO: Fix Later
			return;

			/*Tile tile1 = Main.Tile[x - 1, y];
			Tile tile2 = Main.Tile[x + 1, y];
			Tile tile3 = Main.Tile[x, y - 1];
			Tile tile4 = Main.Tile[x, y + 1];
			Tile tile5 = Main.Tile[x, y];
			bool flag = false;
			if (tile1.liquid > (byte) 0 && tile1.liquidTypeX() == (byte) 0 ||
			    tile2.liquid > (byte) 0 && tile2.liquidTypeX() == (byte) 0 ||
			    tile3.liquid > (byte) 0 && tile3.liquidTypeX() == (byte) 0)
			{
				int num = 0;
				if (tile1.liquidTypeX() == (byte) 0)
				{
					num += (int) tile1.liquid;
					tile1.liquid = (byte) 0;
				}

				if (tile2.liquidTypeX() == (byte) 0)
				{
					num += (int) tile2.liquid;
					tile2.liquid = (byte) 0;
				}

				if (tile3.liquidTypeX() == (byte) 0)
				{
					num += (int) tile3.liquid;
					tile3.liquid = (byte) 0;
				}

				if (tile1.lava() || tile2.lava() || tile3.lava())
					flag = true;
				if (num < 32)
					return;
				if (tile5.active() && Main.tileObsidianKill[(int) tile5.type])
				{
					WorldGen.KillTile(x, y, false, false, false);
					if (Main.netMode == 2)
						NetMessage.SendData(17, -1, -1, (NetworkText) null, 0, (float) x, (float) y, 0.0f, 0, 0, 0);
				}

				if (tile5.active())
					return;
				tile5.liquid = (byte) 0;
				tile5.liquidType(0);
				WorldGen.PlaceTile(x, y, 229, true, true, -1, 0);
				if (flag)
					Main.PlaySound(SoundID.LiquidsHoneyLava, new Vector2((float) (x * 16 + 8), (float) (y * 16 + 8)));
				else
					Main.PlaySound(SoundID.LiquidsHoneyWater, new Vector2((float) (x * 16 + 8), (float) (y * 16 + 8)));
				WorldGen.SquareTileFrame(x, y, true);
				if (Main.netMode != 2)
					return;
				NetMessage.SendTileSquare(-1, x - 1, y - 1, 3,
					flag ? TileChangeType.HoneyLava : TileChangeType.HoneyWater);
			}
			else
			{
				if (tile4.liquid <= (byte) 0 || tile4.liquidTypeX() != (byte) 0)
					return;
				if (Main.tileCut[(int) tile4.type])
				{
					WorldGen.KillTile(x, y + 1, false, false, false);
					if (Main.netMode == 2)
						NetMessage.SendData(17, -1, -1, (NetworkText) null, 0, (float) x, (float) (y + 1), 0.0f, 0, 0,
							0);
				}
				else if (tile4.active() && Main.tileObsidianKill[(int) tile4.type])
				{
					WorldGen.KillTile(x, y + 1, false, false, false);
					if (Main.netMode == 2)
						NetMessage.SendData(17, -1, -1, (NetworkText) null, 0, (float) x, (float) (y + 1), 0.0f, 0, 0,
							0);
				}

				if (tile4.active())
					return;
				if (tile5.liquid < (byte) 32)
				{
					tile5.liquid = (byte) 0;
					tile5.liquidType(0);
					if (Main.netMode != 2)
						return;
					NetMessage.SendTileSquare(-1, x - 1, y, 3, TileChangeType.None);
				}
				else
				{
					if (tile4.lava())
						flag = true;
					tile5.liquid = (byte) 0;
					tile5.liquidType(0);
					tile4.liquid = (byte) 0;
					tile4.liquidType(0);
					if (flag)
						Main.PlaySound(SoundID.LiquidsHoneyLava,
							new Vector2((float) (x * 16 + 8), (float) (y * 16 + 8)));
					else
						Main.PlaySound(SoundID.LiquidsHoneyWater,
							new Vector2((float) (x * 16 + 8), (float) (y * 16 + 8)));
					WorldGen.PlaceTile(x, y + 1, 229, true, true, -1, 0);
					WorldGen.SquareTileFrame(x, y + 1, true);
					if (Main.netMode != 2)
						return;
					NetMessage.SendTileSquare(-1, x - 1, y, 3,
						flag ? TileChangeType.HoneyLava : TileChangeType.HoneyWater);
				}
			}*/
		}
	}
}
