using System;
using LiquidAPI.LiquidMod;
using LiquidAPI.Swap;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using LiquidAPI.Swap;
using Microsoft.Xna.Framework;
using Terraria.ObjectData;

namespace LiquidAPI
{
	internal static class LiquidID
	{
		public const int Water = 0;
		public const int Lava = 1;
		public const int Honey = 2;
	}

	internal static class LiquidExtension
	{
		public static void MethodSwap()
		{
			On.Terraria.Liquid.Update += ModdedLiquidUpdate;
			On.Terraria.Liquid.AddWater += AddWater;
			On.Terraria.LiquidBuffer.AddBuffer += LiquidBufferOnAddBuffer;
			On.Terraria.Liquid.LavaCheck += LiquidOnLavaCheck;
			On.Terraria.Liquid.HoneyCheck += LiquidOnHoneyCheck;
		}

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

		public static void ModdedLiquidUpdate(On.Terraria.Liquid.orig_Update orig, Liquid self)
		{
			Main.tileSolid[TileID.Bubble] = true;
			LiquidRef liquidLeft = LiquidCore.grid[self.x - 1, self.y];
			LiquidRef liquidRight = LiquidCore.grid[self.x + 1, self.y];
			LiquidRef liquidUp = LiquidCore.grid[self.x, self.y - 1];
			LiquidRef liquidDown = LiquidCore.grid[self.x, self.y + 1];
			LiquidRef liquidSelf = LiquidCore.grid[self.x, self.y];

			byte liquidTypeLeft() => LiquidCore.liquidGrid[self.x - 1, self.y].data;
			byte liquidTypeRight() => LiquidCore.liquidGrid[self.x + 1, self.y].data;
			byte liquidTypeUp() => LiquidCore.liquidGrid[self.x, self.y - 1].data;
			byte liquidTypeDown() => LiquidCore.liquidGrid[self.x, self.y + 1].data;
			byte liquidTypeSelf() => LiquidCore.liquidGrid[self.x, self.y].data;

			void setLiquidTypeLeft(byte type) => LiquidCore.liquidGrid[self.x - 1, self.y].data = type;
			void setLiquidTypeRight(byte type) => LiquidCore.liquidGrid[self.x + 1, self.y].data = type;
			void setLiquidTypeUp(byte type) => LiquidCore.liquidGrid[self.x, self.y - 1].data = type;
			void setLiquidTypeDown(byte type) => LiquidCore.liquidGrid[self.x, self.y + 1].data = type;
			void setLiquidTypeSelf(byte type) => LiquidCore.liquidGrid[self.x, self.y].data = type;

			if (liquidSelf.Tile.nactive() && Main.tileSolid[(int)liquidSelf.Tile.type] &&
				!Main.tileSolidTop[(int)liquidSelf.Tile.type])
			{
				self.kill = 9;
				return;
			}

			byte liquid = liquidSelf.Amount;
			if (self.y > Main.maxTilesY - 200 && liquidTypeSelf() == 0 && liquidSelf.Amount > 0)
			{
				byte b = 2;
				if (liquidSelf.Amount < b)
				{
					b = liquidSelf.Amount;
				}

				liquidSelf.Amount -= b;
			}

			if (liquidSelf.Amount == 0)
			{
				self.kill = 9;
				return;
			}

			if (liquidTypeSelf() == LiquidID.Lava)
			{
				Liquid.LavaCheck(self.x, self.y);
				if (!Liquid.quickFall)
				{
					if (self.delay < 5)
					{
						self.delay++;
						return;
					}

					self.delay = 0;
				}
			}
			else
			{
				if (liquidTypeLeft() == LiquidID.Lava)
				{
					Liquid.AddWater(self.x - 1, self.y);
				}

				if (liquidTypeRight() == LiquidID.Lava)
				{
					Liquid.AddWater(self.x + 1, self.y);
				}

				if (liquidTypeUp() == LiquidID.Lava)
				{
					Liquid.AddWater(self.x, self.y - 1);
				}

				if (liquidTypeDown() == LiquidID.Lava)
				{
					Liquid.AddWater(self.x, self.y + 1);
				}

				if (liquidTypeSelf() == LiquidID.Honey)
				{
					Liquid.HoneyCheck(self.x, self.y);
					if (!Liquid.quickFall)
					{
						if (self.delay < 10)
						{
							self.delay++;
							return;
						}

						self.delay = 0;
					}
				}
				else
				{
					if (liquidTypeLeft() == LiquidID.Honey)
					{
						Liquid.AddWater(self.x - 1, self.y);
					}

					if (liquidTypeRight() == LiquidID.Honey)
					{
						Liquid.AddWater(self.x + 1, self.y);
					}

					if (liquidTypeUp() == LiquidID.Honey)
					{
						Liquid.AddWater(self.x, self.y - 1);
					}

					if (liquidTypeDown() == LiquidID.Honey)
					{
						Liquid.AddWater(self.x, self.y + 1);
					}
				}
			}

			if ((!liquidDown.Tile.nactive() || !Main.tileSolid[(int)liquidDown.Tile.type] ||
				 Main.tileSolidTop[(int)liquidDown.Tile.type]) &&
				(liquidDown.Amount <= 0 || liquidTypeDown() == liquidTypeSelf()) && liquidDown.Amount < 255)
			{
				float num = (float)(255 - liquidDown.Amount);
				if (num > (float)liquidSelf.Amount)
				{
					num = (float)liquidSelf.Amount;
				}

				liquidSelf.Amount -= (byte)num;
				liquidDown.Amount += (byte)num;
				setLiquidTypeDown(liquidTypeSelf());
				Liquid.AddWater(self.x, self.y + 1);
				liquidDown.SetSkipLiquid(true);
				liquidSelf.SetSkipLiquid(true);
				if (liquidSelf.Amount > 250)
				{
					liquidSelf.Amount = 255;
				}
				else
				{
					Liquid.AddWater(self.x - 1, self.y);
					Liquid.AddWater(self.x + 1, self.y);
				}
			}

			if (liquidSelf.Amount > 0)
			{
				bool flag = true;
				bool flag2 = true;
				bool flag3 = true;
				bool flag4 = true;
				if (liquidLeft.Tile.nactive() && Main.tileSolid[(int)liquidLeft.Tile.type] &&
					!Main.tileSolidTop[(int)liquidLeft.Tile.type])
				{
					flag = false;
				}
				else if (liquidLeft.Amount > 0 && liquidTypeLeft() != liquidTypeSelf())
				{
					flag = false;
				}
				else if (Main.tile[self.x - 2, self.y].nactive() &&
						 Main.tileSolid[(int)Main.tile[self.x - 2, self.y].type] &&
						 !Main.tileSolidTop[(int)Main.tile[self.x - 2, self.y].type])
				{
					flag3 = false;
				}
				else if (Main.tile[self.x - 2, self.y].liquid == 0)
				{
					flag3 = false;
				}
				else if (Main.tile[self.x - 2, self.y].liquid > 0 &&
						 LiquidCore.liquidGrid[self.x - 2, self.y].data != liquidTypeSelf())
				{
					flag3 = false;
				}

				if (liquidRight.Tile.nactive() && Main.tileSolid[(int)liquidRight.Tile.type] &&
					!Main.tileSolidTop[(int)liquidRight.Tile.type])
				{
					flag2 = false;
				}
				else if (liquidRight.Amount > 0 && liquidTypeRight() != liquidTypeSelf())
				{
					flag2 = false;
				}
				else if (Main.tile[self.x + 2, self.y].nactive() &&
						 Main.tileSolid[(int)Main.tile[self.x + 2, self.y].type] &&
						 !Main.tileSolidTop[(int)Main.tile[self.x + 2, self.y].type])
				{
					flag4 = false;
				}
				else if (Main.tile[self.x + 2, self.y].liquid == 0)
				{
					flag4 = false;
				}
				else if (Main.tile[self.x + 2, self.y].liquid > 0 &&
						 LiquidCore.liquidGrid[self.x + 2, self.y].data != liquidTypeSelf())
				{
					flag4 = false;
				}

				int num2 = 0;
				if (liquidSelf.Amount < 3)
				{
					num2 = -1;
				}

				if (flag && flag2)
				{
					if (flag3 && flag4)
					{
						bool flag5 = true;
						bool flag6 = true;
						if (Main.tile[self.x - 3, self.y].nactive() &&
							Main.tileSolid[(int)Main.tile[self.x - 3, self.y].type] &&
							!Main.tileSolidTop[(int)Main.tile[self.x - 3, self.y].type])
						{
							flag5 = false;
						}
						else if (Main.tile[self.x - 3, self.y].liquid == 0)
						{
							flag5 = false;
						}
						else if (LiquidCore.liquidGrid[self.x - 3, self.y].data != liquidTypeSelf())
						{
							flag5 = false;
						}

						if (Main.tile[self.x + 3, self.y].nactive() &&
							Main.tileSolid[(int)Main.tile[self.x + 3, self.y].type] &&
							!Main.tileSolidTop[(int)Main.tile[self.x + 3, self.y].type])
						{
							flag6 = false;
						}
						else if (Main.tile[self.x + 3, self.y].liquid == 0)
						{
							flag6 = false;
						}
						else if (LiquidCore.liquidGrid[self.x + 3, self.y].data != liquidTypeSelf())
						{
							flag6 = false;
						}

						if (flag5 && flag6)
						{
							float num = (float)((int)(liquidLeft.Amount + liquidRight.Amount +
														Main.tile[self.x - 2, self.y].liquid +
														Main.tile[self.x + 2, self.y].liquid +
														Main.tile[self.x - 3, self.y].liquid +
														Main.tile[self.x + 3, self.y].liquid + liquidSelf.Amount) +
												 num2);
							num = (float)Math.Round((double)(num / 7f));
							int num3 = 0;
							setLiquidTypeLeft(liquidTypeSelf());
							if (liquidLeft.Amount != (byte)num)
							{
								liquidLeft.Amount = (byte)num;
								Liquid.AddWater(self.x - 1, self.y);
							}
							else
							{
								num3++;
							}

							setLiquidTypeRight(liquidTypeSelf());
							if (liquidRight.Amount != (byte)num)
							{
								liquidRight.Amount = (byte)num;
								Liquid.AddWater(self.x + 1, self.y);
							}
							else
							{
								num3++;
							}

							LiquidCore.liquidGrid[self.x - 2, self.y].data = liquidTypeSelf();
							if (Main.tile[self.x - 2, self.y].liquid != (byte)num)
							{
								Main.tile[self.x - 2, self.y].liquid = (byte)num;
								Liquid.AddWater(self.x - 2, self.y);
							}
							else
							{
								num3++;
							}

							LiquidCore.liquidGrid[self.x + 2, self.y].data = liquidTypeSelf();
							if (Main.tile[self.x + 2, self.y].liquid != (byte)num)
							{
								Main.tile[self.x + 2, self.y].liquid = (byte)num;
								Liquid.AddWater(self.x + 2, self.y);
							}
							else
							{
								num3++;
							}

							LiquidCore.liquidGrid[self.x - 3, self.y].data = liquidTypeSelf();
							if (Main.tile[self.x - 3, self.y].liquid != (byte)num)
							{
								Main.tile[self.x - 3, self.y].liquid = (byte)num;
								Liquid.AddWater(self.x - 3, self.y);
							}
							else
							{
								num3++;
							}

							LiquidCore.liquidGrid[self.x + 3, self.y].data = liquidTypeSelf();
							if (Main.tile[self.x + 3, self.y].liquid != (byte)num)
							{
								Main.tile[self.x + 3, self.y].liquid = (byte)num;
								Liquid.AddWater(self.x + 3, self.y);
							}
							else
							{
								num3++;
							}

							if (liquidLeft.Amount != (byte)num || liquidSelf.Amount != (byte)num)
							{
								Liquid.AddWater(self.x - 1, self.y);
							}

							if (liquidRight.Amount != (byte)num || liquidSelf.Amount != (byte)num)
							{
								Liquid.AddWater(self.x + 1, self.y);
							}

							if (Main.tile[self.x - 2, self.y].liquid != (byte)num ||
								liquidSelf.Amount != (byte)num)
							{
								Liquid.AddWater(self.x - 2, self.y);
							}

							if (Main.tile[self.x + 2, self.y].liquid != (byte)num ||
								liquidSelf.Amount != (byte)num)
							{
								Liquid.AddWater(self.x + 2, self.y);
							}

							if (Main.tile[self.x - 3, self.y].liquid != (byte)num ||
								liquidSelf.Amount != (byte)num)
							{
								Liquid.AddWater(self.x - 3, self.y);
							}

							if (Main.tile[self.x + 3, self.y].liquid != (byte)num ||
								liquidSelf.Amount != (byte)num)
							{
								Liquid.AddWater(self.x + 3, self.y);
							}

							if (num3 != 6 || liquidUp.Amount <= 0)
							{
								liquidSelf.Amount = (byte)num;
							}
						}
						else
						{
							int num4 = 0;
							float num = (float)((int)(liquidLeft.Amount + liquidRight.Amount +
														Main.tile[self.x - 2, self.y].liquid +
														Main.tile[self.x + 2, self.y].liquid + liquidSelf.Amount) +
												 num2);
							num = (float)Math.Round((double)(num / 5f));
							setLiquidTypeLeft(liquidTypeSelf());
							if (liquidLeft.Amount != (byte)num)
							{
								liquidLeft.Amount = (byte)num;
								Liquid.AddWater(self.x - 1, self.y);
							}
							else
							{
								num4++;
							}

							setLiquidTypeRight(liquidTypeSelf());
							if (liquidRight.Amount != (byte)num)
							{
								liquidRight.Amount = (byte)num;
								Liquid.AddWater(self.x + 1, self.y);
							}
							else
							{
								num4++;
							}

							LiquidCore.liquidGrid[self.x - 2, self.y].data = liquidTypeSelf();
							if (Main.tile[self.x - 2, self.y].liquid != (byte)num)
							{
								Main.tile[self.x - 2, self.y].liquid = (byte)num;
								Liquid.AddWater(self.x - 2, self.y);
							}
							else
							{
								num4++;
							}

							LiquidCore.liquidGrid[self.x + 2, self.y].data = liquidTypeSelf();
							if (Main.tile[self.x + 2, self.y].liquid != (byte)num)
							{
								Main.tile[self.x + 2, self.y].liquid = (byte)num;
								Liquid.AddWater(self.x + 2, self.y);
							}
							else
							{
								num4++;
							}

							if (liquidLeft.Amount != (byte)num || liquidSelf.Amount != (byte)num)
							{
								Liquid.AddWater(self.x - 1, self.y);
							}

							if (liquidRight.Amount != (byte)num || liquidSelf.Amount != (byte)num)
							{
								Liquid.AddWater(self.x + 1, self.y);
							}

							if (Main.tile[self.x - 2, self.y].liquid != (byte)num ||
								liquidSelf.Amount != (byte)num)
							{
								Liquid.AddWater(self.x - 2, self.y);
							}

							if (Main.tile[self.x + 2, self.y].liquid != (byte)num ||
								liquidSelf.Amount != (byte)num)
							{
								Liquid.AddWater(self.x + 2, self.y);
							}

							if (num4 != 4 || liquidUp.Amount <= 0)
							{
								liquidSelf.Amount = (byte)num;
							}
						}
					}
					else if (flag3)
					{
						float num = (float)((int)(liquidLeft.Amount + liquidRight.Amount +
													Main.tile[self.x - 2, self.y].liquid +
													liquidSelf.Amount) + num2);
						num = (float)Math.Round((double)(num / 4f) + 0.001);
						setLiquidTypeLeft(liquidTypeSelf());
						if (liquidLeft.Amount != (byte)num || liquidSelf.Amount != (byte)num)
						{
							liquidLeft.Amount = (byte)num;
							Liquid.AddWater(self.x - 1, self.y);
						}

						setLiquidTypeRight(liquidTypeSelf());
						if (liquidRight.Amount != (byte)num || liquidSelf.Amount != (byte)num)
						{
							liquidRight.Amount = (byte)num;
							Liquid.AddWater(self.x + 1, self.y);
						}

						LiquidCore.liquidGrid[self.x - 2, self.y].data = liquidTypeSelf();
						if (Main.tile[self.x - 2, self.y].liquid != (byte)num || liquidSelf.Amount != (byte)num)
						{
							Main.tile[self.x - 2, self.y].liquid = (byte)num;
							Liquid.AddWater(self.x - 2, self.y);
						}

						liquidSelf.Amount = (byte)num;
					}
					else if (flag4)
					{
						float num = (float)((int)(liquidLeft.Amount + liquidRight.Amount +
													Main.tile[self.x + 2, self.y].liquid +
													liquidSelf.Amount) + num2);
						num = (float)Math.Round((double)(num / 4f) + 0.001);
						setLiquidTypeLeft(liquidTypeSelf());
						if (liquidLeft.Amount != (byte)num || liquidSelf.Amount != (byte)num)
						{
							liquidLeft.Amount = (byte)num;
							Liquid.AddWater(self.x - 1, self.y);
						}

						setLiquidTypeRight(liquidTypeSelf());
						if (liquidRight.Amount != (byte)num || liquidSelf.Amount != (byte)num)
						{
							liquidRight.Amount = (byte)num;
							Liquid.AddWater(self.x + 1, self.y);
						}

						LiquidCore.liquidGrid[self.x + 2, self.y].data = liquidTypeSelf();
						if (Main.tile[self.x + 2, self.y].liquid != (byte)num || liquidSelf.Amount != (byte)num)
						{
							Main.tile[self.x + 2, self.y].liquid = (byte)num;
							Liquid.AddWater(self.x + 2, self.y);
						}

						liquidSelf.Amount = (byte)num;
					}
					else
					{
						float num = (float)((int)(liquidLeft.Amount + liquidRight.Amount +
													liquidSelf.Amount) + num2);
						num = (float)Math.Round((double)(num / 3f) + 0.001);
						setLiquidTypeLeft(liquidTypeSelf());
						liquidLeft.Amount = (byte)num;

						if (liquidSelf.Amount != (byte)num || liquidLeft.Amount != (byte)num)
						{
							Liquid.AddWater(self.x - 1, self.y);
						}

						setLiquidTypeRight(liquidTypeSelf());
						liquidRight.Amount = (byte)num;

						if (liquidSelf.Amount != (byte)num || liquidRight.Amount != (byte)num)
						{
							Liquid.AddWater(self.x + 1, self.y);
						}

						liquidSelf.Amount = (byte)num;
					}
				}
				else if (flag)
				{
					float num = (float)((int)(liquidLeft.Amount + liquidSelf.Amount) + num2);
					num = (float)Math.Round((double)(num / 2f) + 0.001);
					liquidLeft.Amount = (byte)num;

					setLiquidTypeLeft(liquidTypeSelf());
					if (liquidSelf.Amount != (byte)num || liquidLeft.Amount != (byte)num)
					{
						Liquid.AddWater(self.x - 1, self.y);
					}

					liquidSelf.Amount = (byte)num;
				}
				else if (flag2)
				{
					float num = (float)((int)(liquidRight.Amount + liquidSelf.Amount) + num2);
					num = (float)Math.Round((double)(num / 2f) + 0.001);
					liquidRight.Amount = (byte)num;

					setLiquidTypeRight(liquidTypeSelf());
					if (liquidSelf.Amount != (byte)num || liquidRight.Amount != (byte)num)
					{
						Liquid.AddWater(self.x + 1, self.y);
					}

					liquidSelf.Amount = (byte)num;
				}
			}

			if (liquidSelf.Amount == liquid)
			{
				self.kill++;
				return;
			}

			if (liquidSelf.Amount == 254 && liquid == 255)
			{
				liquidSelf.Amount = 255;
				self.kill++;
				return;
			}

			Liquid.AddWater(self.x, self.y - 1);
			self.kill = 0;
		}

		public static void AddWater(On.Terraria.Liquid.orig_AddWater org, int x, int y)
		{
			LiquidRef liquid = LiquidCore.grid[x, y];
			if (liquid?.Tile == null ||
				liquid.CheckingLiquid() ||
				x >= Main.maxTilesX - 5 ||
				y >= Main.maxTilesY - 5 ||
				x < 5 || y < 5 ||
				liquid.Amount == 0)
			{
				return;
			}

			if (Liquid.numLiquid >= Liquid.maxLiquid - 1)
			{
				LiquidBuffer.AddBuffer(x, y);
				return;
			}

			liquid.SetCheckingLiquid(true);
			Main.liquid[Liquid.numLiquid].kill = 0;
			Main.liquid[Liquid.numLiquid].x = x;
			Main.liquid[Liquid.numLiquid].y = y;
			Main.liquid[Liquid.numLiquid].delay = 0;
			liquid.SetSkipLiquid(false);
			Liquid.numLiquid++;
			if (Main.netMode == 2)
			{
				Liquid.NetSendLiquid(x, y);
			}

			if (!liquid.Tile.active() || WorldGen.gen)
			{
				return;
			}

			bool flag = false;
			if (LiquidCore.liquidGrid[x, y].data == LiquidID.Lava)
			{
				if (TileObjectData.CheckLavaDeath(liquid.Tile))
				{
					flag = true;
				}
			}
			else if (TileObjectData.CheckWaterDeath(liquid.Tile))
			{
				flag = true;
			}

			if (flag)
			{
				WorldGen.KillTile(x, y, false, false, false);
				if (Main.netMode == 2)
				{
					NetMessage.SendData(17, -1, -1, null, 0, (float)x, (float)y, 0f, 0, 0, 0);
				}
			}
		}

		private static void LiquidBufferOnAddBuffer(On.Terraria.LiquidBuffer.orig_AddBuffer orig, int x, int y)
		{
			if (LiquidBuffer.numLiquidBuffer == 9999 || LiquidCore.grid[x, y].CheckingLiquid())
				return;
			LiquidCore.grid[x, y].SetCheckingLiquid(true);
			Main.liquidBuffer[LiquidBuffer.numLiquidBuffer].x = x;
			Main.liquidBuffer[LiquidBuffer.numLiquidBuffer].y = y;
			++LiquidBuffer.numLiquidBuffer;
		}
	}
}