﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LiquidAPI.ID;
using LiquidAPI.LiquidMod;
using Terraria;
using Terraria.ID;

namespace LiquidAPI.Hooks
{
	internal static partial class LiquidHooks
	{
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

			if (liquidSelf.Tile.nactive() && Main.tileSolid[(int) liquidSelf.Tile.type] &&
			    !Main.tileSolidTop[(int) liquidSelf.Tile.type])
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

			if (liquidTypeSelf() == LiquidID.lava)
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
				if (liquidTypeLeft() == LiquidID.lava)
				{
					Liquid.AddWater(self.x - 1, self.y);
				}

				if (liquidTypeRight() == LiquidID.lava)
				{
					Liquid.AddWater(self.x + 1, self.y);
				}

				if (liquidTypeUp() == LiquidID.lava)
				{
					Liquid.AddWater(self.x, self.y - 1);
				}

				if (liquidTypeDown() == LiquidID.lava)
				{
					Liquid.AddWater(self.x, self.y + 1);
				}

				if (liquidTypeSelf() == LiquidID.honey)
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
					if (liquidTypeLeft() == LiquidID.honey)
					{
						Liquid.AddWater(self.x - 1, self.y);
					}

					if (liquidTypeRight() == LiquidID.honey)
					{
						Liquid.AddWater(self.x + 1, self.y);
					}

					if (liquidTypeUp() == LiquidID.honey)
					{
						Liquid.AddWater(self.x, self.y - 1);
					}

					if (liquidTypeDown() == LiquidID.honey)
					{
						Liquid.AddWater(self.x, self.y + 1);
					}
				}
			}

			if ((!liquidDown.Tile.nactive() || !Main.tileSolid[(int) liquidDown.Tile.type] ||
			     Main.tileSolidTop[(int) liquidDown.Tile.type]) &&
			    (liquidDown.Amount <= 0 || liquidTypeDown() == liquidTypeSelf()) && liquidDown.Amount < 255)
			{
				float num = (float) (255 - liquidDown.Amount);
				if (num > (float) liquidSelf.Amount)
				{
					num = (float) liquidSelf.Amount;
				}

				liquidSelf.Amount -= (byte) num;
				liquidDown.Amount += (byte) num;
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
				if (liquidLeft.Tile.nactive() && Main.tileSolid[(int) liquidLeft.Tile.type] &&
				    !Main.tileSolidTop[(int) liquidLeft.Tile.type])
				{
					flag = false;
				}
				else if (liquidLeft.Amount > 0 && liquidTypeLeft() != liquidTypeSelf())
				{
					flag = false;
				}
				else if (Main.tile[self.x - 2, self.y].nactive() &&
				         Main.tileSolid[(int) Main.tile[self.x - 2, self.y].type] &&
				         !Main.tileSolidTop[(int) Main.tile[self.x - 2, self.y].type])
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

				if (liquidRight.Tile.nactive() && Main.tileSolid[(int) liquidRight.Tile.type] &&
				    !Main.tileSolidTop[(int) liquidRight.Tile.type])
				{
					flag2 = false;
				}
				else if (liquidRight.Amount > 0 && liquidTypeRight() != liquidTypeSelf())
				{
					flag2 = false;
				}
				else if (Main.tile[self.x + 2, self.y].nactive() &&
				         Main.tileSolid[(int) Main.tile[self.x + 2, self.y].type] &&
				         !Main.tileSolidTop[(int) Main.tile[self.x + 2, self.y].type])
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
						    Main.tileSolid[(int) Main.tile[self.x - 3, self.y].type] &&
						    !Main.tileSolidTop[(int) Main.tile[self.x - 3, self.y].type])
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
						    Main.tileSolid[(int) Main.tile[self.x + 3, self.y].type] &&
						    !Main.tileSolidTop[(int) Main.tile[self.x + 3, self.y].type])
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
							float num = (float) ((int) (liquidLeft.Amount + liquidRight.Amount +
							                            Main.tile[self.x - 2, self.y].liquid +
							                            Main.tile[self.x + 2, self.y].liquid +
							                            Main.tile[self.x - 3, self.y].liquid +
							                            Main.tile[self.x + 3, self.y].liquid + liquidSelf.Amount) +
							                     num2);
							num = (float) Math.Round((double) (num / 7f));
							int num3 = 0;
							setLiquidTypeLeft(liquidTypeSelf());
							if (liquidLeft.Amount != (byte) num)
							{
								liquidLeft.Amount = (byte) num;
								Liquid.AddWater(self.x - 1, self.y);
							}
							else
							{
								num3++;
							}

							setLiquidTypeRight(liquidTypeSelf());
							if (liquidRight.Amount != (byte) num)
							{
								liquidRight.Amount = (byte) num;
								Liquid.AddWater(self.x + 1, self.y);
							}
							else
							{
								num3++;
							}

							LiquidCore.liquidGrid[self.x - 2, self.y].data = liquidTypeSelf();
							if (Main.tile[self.x - 2, self.y].liquid != (byte) num)
							{
								Main.tile[self.x - 2, self.y].liquid = (byte) num;
								Liquid.AddWater(self.x - 2, self.y);
							}
							else
							{
								num3++;
							}

							LiquidCore.liquidGrid[self.x + 2, self.y].data = liquidTypeSelf();
							if (Main.tile[self.x + 2, self.y].liquid != (byte) num)
							{
								Main.tile[self.x + 2, self.y].liquid = (byte) num;
								Liquid.AddWater(self.x + 2, self.y);
							}
							else
							{
								num3++;
							}

							LiquidCore.liquidGrid[self.x - 3, self.y].data = liquidTypeSelf();
							if (Main.tile[self.x - 3, self.y].liquid != (byte) num)
							{
								Main.tile[self.x - 3, self.y].liquid = (byte) num;
								Liquid.AddWater(self.x - 3, self.y);
							}
							else
							{
								num3++;
							}

							LiquidCore.liquidGrid[self.x + 3, self.y].data = liquidTypeSelf();
							if (Main.tile[self.x + 3, self.y].liquid != (byte) num)
							{
								Main.tile[self.x + 3, self.y].liquid = (byte) num;
								Liquid.AddWater(self.x + 3, self.y);
							}
							else
							{
								num3++;
							}

							if (liquidLeft.Amount != (byte) num || liquidSelf.Amount != (byte) num)
							{
								Liquid.AddWater(self.x - 1, self.y);
							}

							if (liquidRight.Amount != (byte) num || liquidSelf.Amount != (byte) num)
							{
								Liquid.AddWater(self.x + 1, self.y);
							}

							if (Main.tile[self.x - 2, self.y].liquid != (byte) num ||
							    liquidSelf.Amount != (byte) num)
							{
								Liquid.AddWater(self.x - 2, self.y);
							}

							if (Main.tile[self.x + 2, self.y].liquid != (byte) num ||
							    liquidSelf.Amount != (byte) num)
							{
								Liquid.AddWater(self.x + 2, self.y);
							}

							if (Main.tile[self.x - 3, self.y].liquid != (byte) num ||
							    liquidSelf.Amount != (byte) num)
							{
								Liquid.AddWater(self.x - 3, self.y);
							}

							if (Main.tile[self.x + 3, self.y].liquid != (byte) num ||
							    liquidSelf.Amount != (byte) num)
							{
								Liquid.AddWater(self.x + 3, self.y);
							}

							if (num3 != 6 || liquidUp.Amount <= 0)
							{
								liquidSelf.Amount = (byte) num;
							}
						}
						else
						{
							int num4 = 0;
							float num = (float) ((int) (liquidLeft.Amount + liquidRight.Amount +
							                            Main.tile[self.x - 2, self.y].liquid +
							                            Main.tile[self.x + 2, self.y].liquid + liquidSelf.Amount) +
							                     num2);
							num = (float) Math.Round((double) (num / 5f));
							setLiquidTypeLeft(liquidTypeSelf());
							if (liquidLeft.Amount != (byte) num)
							{
								liquidLeft.Amount = (byte) num;
								Liquid.AddWater(self.x - 1, self.y);
							}
							else
							{
								num4++;
							}

							setLiquidTypeRight(liquidTypeSelf());
							if (liquidRight.Amount != (byte) num)
							{
								liquidRight.Amount = (byte) num;
								Liquid.AddWater(self.x + 1, self.y);
							}
							else
							{
								num4++;
							}

							LiquidCore.liquidGrid[self.x - 2, self.y].data = liquidTypeSelf();
							if (Main.tile[self.x - 2, self.y].liquid != (byte) num)
							{
								Main.tile[self.x - 2, self.y].liquid = (byte) num;
								Liquid.AddWater(self.x - 2, self.y);
							}
							else
							{
								num4++;
							}

							LiquidCore.liquidGrid[self.x + 2, self.y].data = liquidTypeSelf();
							if (Main.tile[self.x + 2, self.y].liquid != (byte) num)
							{
								Main.tile[self.x + 2, self.y].liquid = (byte) num;
								Liquid.AddWater(self.x + 2, self.y);
							}
							else
							{
								num4++;
							}

							if (liquidLeft.Amount != (byte) num || liquidSelf.Amount != (byte) num)
							{
								Liquid.AddWater(self.x - 1, self.y);
							}

							if (liquidRight.Amount != (byte) num || liquidSelf.Amount != (byte) num)
							{
								Liquid.AddWater(self.x + 1, self.y);
							}

							if (Main.tile[self.x - 2, self.y].liquid != (byte) num ||
							    liquidSelf.Amount != (byte) num)
							{
								Liquid.AddWater(self.x - 2, self.y);
							}

							if (Main.tile[self.x + 2, self.y].liquid != (byte) num ||
							    liquidSelf.Amount != (byte) num)
							{
								Liquid.AddWater(self.x + 2, self.y);
							}

							if (num4 != 4 || liquidUp.Amount <= 0)
							{
								liquidSelf.Amount = (byte) num;
							}
						}
					}
					else if (flag3)
					{
						float num = (float) ((int) (liquidLeft.Amount + liquidRight.Amount +
						                            Main.tile[self.x - 2, self.y].liquid +
						                            liquidSelf.Amount) + num2);
						num = (float) Math.Round((double) (num / 4f) + 0.001);
						setLiquidTypeLeft(liquidTypeSelf());
						if (liquidLeft.Amount != (byte) num || liquidSelf.Amount != (byte) num)
						{
							liquidLeft.Amount = (byte) num;
							Liquid.AddWater(self.x - 1, self.y);
						}

						setLiquidTypeRight(liquidTypeSelf());
						if (liquidRight.Amount != (byte) num || liquidSelf.Amount != (byte) num)
						{
							liquidRight.Amount = (byte) num;
							Liquid.AddWater(self.x + 1, self.y);
						}

						LiquidCore.liquidGrid[self.x - 2, self.y].data = liquidTypeSelf();
						if (Main.tile[self.x - 2, self.y].liquid != (byte) num || liquidSelf.Amount != (byte) num)
						{
							Main.tile[self.x - 2, self.y].liquid = (byte) num;
							Liquid.AddWater(self.x - 2, self.y);
						}

						liquidSelf.Amount = (byte) num;
					}
					else if (flag4)
					{
						float num = (float) ((int) (liquidLeft.Amount + liquidRight.Amount +
						                            Main.tile[self.x + 2, self.y].liquid +
						                            liquidSelf.Amount) + num2);
						num = (float) Math.Round((double) (num / 4f) + 0.001);
						setLiquidTypeLeft(liquidTypeSelf());
						if (liquidLeft.Amount != (byte) num || liquidSelf.Amount != (byte) num)
						{
							liquidLeft.Amount = (byte) num;
							Liquid.AddWater(self.x - 1, self.y);
						}

						setLiquidTypeRight(liquidTypeSelf());
						if (liquidRight.Amount != (byte) num || liquidSelf.Amount != (byte) num)
						{
							liquidRight.Amount = (byte) num;
							Liquid.AddWater(self.x + 1, self.y);
						}

						LiquidCore.liquidGrid[self.x + 2, self.y].data = liquidTypeSelf();
						if (Main.tile[self.x + 2, self.y].liquid != (byte) num || liquidSelf.Amount != (byte) num)
						{
							Main.tile[self.x + 2, self.y].liquid = (byte) num;
							Liquid.AddWater(self.x + 2, self.y);
						}

						liquidSelf.Amount = (byte) num;
					}
					else
					{
						float num = (float) ((int) (liquidLeft.Amount + liquidRight.Amount +
						                            liquidSelf.Amount) + num2);
						num = (float) Math.Round((double) (num / 3f) + 0.001);
						setLiquidTypeLeft(liquidTypeSelf());
						liquidLeft.Amount = (byte) num;

						if (liquidSelf.Amount != (byte) num || liquidLeft.Amount != (byte) num)
						{
							Liquid.AddWater(self.x - 1, self.y);
						}

						setLiquidTypeRight(liquidTypeSelf());
						liquidRight.Amount = (byte) num;

						if (liquidSelf.Amount != (byte) num || liquidRight.Amount != (byte) num)
						{
							Liquid.AddWater(self.x + 1, self.y);
						}

						liquidSelf.Amount = (byte) num;
					}
				}
				else if (flag)
				{
					float num = (float) ((int) (liquidLeft.Amount + liquidSelf.Amount) + num2);
					num = (float) Math.Round((double) (num / 2f) + 0.001);
					liquidLeft.Amount = (byte) num;

					setLiquidTypeLeft(liquidTypeSelf());
					if (liquidSelf.Amount != (byte) num || liquidLeft.Amount != (byte) num)
					{
						Liquid.AddWater(self.x - 1, self.y);
					}

					liquidSelf.Amount = (byte) num;
				}
				else if (flag2)
				{
					float num = (float) ((int) (liquidRight.Amount + liquidSelf.Amount) + num2);
					num = (float) Math.Round((double) (num / 2f) + 0.001);
					liquidRight.Amount = (byte) num;

					setLiquidTypeRight(liquidTypeSelf());
					if (liquidSelf.Amount != (byte) num || liquidRight.Amount != (byte) num)
					{
						Liquid.AddWater(self.x + 1, self.y);
					}

					liquidSelf.Amount = (byte) num;
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
	}
}