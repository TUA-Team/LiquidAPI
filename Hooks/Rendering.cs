using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LiquidAPI.ID;
using LiquidAPI.LiquidMod;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent.Liquid;
using Terraria.ModLoader;

namespace LiquidAPI.Hooks
{
	internal static partial class LiquidHooks
	{
		private static void MainOnOldDrawWater(On.Terraria.Main.orig_oldDrawWater orig, Terraria.Main self,
			bool bg = false, int Style = 0, float Alpha = 1f)
		{
			float num = 0f;
			float num2 = 99999f;
			float num3 = 99999f;
			int num4 = -1;
			int num5 = -1;
			Vector2 zero = new Vector2((float) Main.offScreenRange, (float) Main.offScreenRange);
			if (Main.drawToScreen)
			{
				zero = Vector2.Zero;
			}

			int num6 = (int) (255f * (1f - Main.gfxQuality) + 40f * Main.gfxQuality);
			float arg_5D_0 = Main.gfxQuality;
			float arg_63_0 = Main.gfxQuality;
			int num7 = (int) ((Main.screenPosition.X - zero.X) / 16f - 1f);
			int num8 = (int) ((Main.screenPosition.X + (float) Main.screenWidth + zero.X) / 16f) + 2;
			int num9 = (int) ((Main.screenPosition.Y - zero.Y) / 16f - 1f);
			int num10 = (int) ((Main.screenPosition.Y + (float) Main.screenHeight + zero.Y) / 16f) + 5;
			if (num7 < 5)
			{
				num7 = 5;
			}

			if (num8 > Main.maxTilesX - 5)
			{
				num8 = Main.maxTilesX - 5;
			}

			if (num9 < 5)
			{
				num9 = 5;
			}

			if (num10 > Main.maxTilesY - 5)
			{
				num10 = Main.maxTilesY - 5;
			}

			for (int i = num9; i < num10 + 4; i++)
			{
				for (int j = num7 - 2; j < num8 + 2; j++)
				{
					LiquidRef liquid = LiquidCore.grid[j, i];
					LiquidRef liquidUp = LiquidCore.grid[j, i + 1];
					LiquidRef liquidDown = LiquidCore.grid[j, i - 1];

					if (liquid.Amount > 0 &&
					    (!liquid.Tile.nactive() || !Main.tileSolid[liquid.Tile.type] ||
					     Main.tileSolidTop[liquid.Tile.type]) && (Lighting.Brightness(j, i) > 0f || bg))
					{
						Microsoft.Xna.Framework.Color color = Lighting.GetColor(j, i);
						float num11 = (float) (256 - (int) liquid.Amount);
						num11 /= 32f;
						int num12 = 0;
						if (liquid.Type == LiquidID.lava)
						{
							if (Main.drewLava)
							{
								goto IL_E7F;
							}

							float num13 = Math.Abs((float) (j * 16 + 8) -
							                       (Main.screenPosition.X + (float) (Main.screenWidth / 2)));
							float num14 = Math.Abs((float) (i * 16 + 8) -
							                       (Main.screenPosition.Y + (float) (Main.screenHeight / 2)));
							if (num13 < (float) (Main.screenWidth * 2) && num14 < (float) (Main.screenHeight * 2))
							{
								float num15 = (float) Math.Sqrt((double) (num13 * num13 + num14 * num14));
								float num16 = 1f - num15 / ((float) Main.screenWidth * 0.75f);
								if (num16 > 0f)
								{
									num += num16;
								}
							}

							if (num13 < num2)
							{
								num2 = num13;
								num4 = j * 16 + 8;
							}

							if (num14 < num3)
							{
								num3 = num13;
								num5 = i * 16 + 8;
							}

							num12 = 1;
						}
						else if (liquid.Type == LiquidID.honey)
						{
							num12 = 11;
						}

						if (num12 == 0)
						{
							num12 = Style;
						}

						if ((num12 != 1 && num12 != 11) || !Main.drewLava)
						{
							float num17 = 0.5f;
							if (bg)
							{
								num17 = 1f;
							}

							if (num12 != 1 && num12 != 11)
							{
								num17 *= Alpha;
							}

							Vector2 value = new Vector2((float) (j * 16), (float) (i * 16 + (int) num11 * 2));
							Microsoft.Xna.Framework.Rectangle value2 =
								new Microsoft.Xna.Framework.Rectangle(0, 0, 16, 16 - (int) num11 * 2);

							if (liquidUp.Amount < 245 &&
							    (!liquidUp.Tile.nactive() || !Main.tileSolid[liquidUp.Tile.type] ||
							     Main.tileSolidTop[liquidUp.Tile.type]))
							{
								float num18 = (float) (256 - (int) liquidUp.Amount);
								num18 /= 32f;
								num17 = 0.5f * (8f - num11) / 4f;
								if ((double) num17 > 0.55)
								{
									num17 = 0.55f;
								}

								if ((double) num17 < 0.35)
								{
									num17 = 0.35f;
								}

								float num19 = num11 / 2f;
								if (liquidUp.Amount < 200)
								{
									if (bg)
									{
										goto IL_E7F;
									}

									if (liquidDown.Amount > 0 && liquidDown.Amount > 0)
									{
										value2 = new Microsoft.Xna.Framework.Rectangle(0, 4, 16, 16);
										num17 = 0.5f;
									}
									else if (liquidDown.Amount > 0)
									{
										value = new Vector2((float) (j * 16), (float) (i * 16 + 4));
										value2 = new Microsoft.Xna.Framework.Rectangle(0, 4, 16, 12);
										num17 = 0.5f;
									}
									else if (liquidUp.Amount > 0)
									{
										value = new Vector2((float) (j * 16),
											(float) (i * 16 + (int) num11 * 2 + (int) num18 * 2));
										value2 = new Microsoft.Xna.Framework.Rectangle(0, 4, 16, 16 - (int) num11 * 2);
									}
									else
									{
										value = new Vector2((float) (j * 16 + (int) num19),
											(float) (i * 16 + (int) num19 * 2 + (int) num18 * 2));
										value2 = new Microsoft.Xna.Framework.Rectangle(0, 4, 16 - (int) num19 * 2,
											16 - (int) num19 * 2);
									}
								}
								else
								{
									num17 = 0.5f;
									value2 = new Microsoft.Xna.Framework.Rectangle(0, 4, 16,
										16 - (int) num11 * 2 + (int) num18 * 2);
								}
							}
							else if (liquidDown.Amount > 32)
							{
								value2 = new Microsoft.Xna.Framework.Rectangle(0, 4, value2.Width, value2.Height);
							}
							else if (num11 < 1f && liquidDown.Tile.nactive() &&
							         Main.tileSolid[liquidDown.Tile.type] &&
							         !Main.tileSolidTop[liquidDown.Tile.type])
							{
								value = new Vector2((float) (j * 16), (float) (i * 16));
								value2 = new Microsoft.Xna.Framework.Rectangle(0, 4, 16, 16);
							}
							else
							{
								bool flag = true;
								int num20 = i + 1;
								while (num20 < i + 6 &&
								       (!LiquidCore.grid[j, num20].Tile.nactive() ||
								        !Main.tileSolid[LiquidCore.grid[j, num20].Tile.type] ||
								        Main.tileSolidTop[LiquidCore.grid[j, num20].Tile.type]))
								{
									if (LiquidCore.grid[j, num20].Amount < 200)
									{
										flag = false;
										break;
									}

									num20++;
								}

								if (!flag)
								{
									num17 = 0.5f;
									value2 = new Microsoft.Xna.Framework.Rectangle(0, 4, 16, 16);
								}
								else if (liquidDown.HasLiquid())
								{
									value2 = new Microsoft.Xna.Framework.Rectangle(0, 2, value2.Width, value2.Height);
								}
							}

							if ((color.R > 20 || color.B > 20 || color.G > 20) && value2.Y < 4)
							{
								int num21 = (int) color.R;
								if ((int) color.G > num21)
								{
									num21 = (int) color.G;
								}

								if ((int) color.B > num21)
								{
									num21 = (int) color.B;
								}

								num21 /= 30;
								if (Main.rand.Next(20000) < num21)
								{
									Microsoft.Xna.Framework.Color newColor =
										new Microsoft.Xna.Framework.Color(255, 255, 255);
									if (liquid.Type == LiquidID.honey)
									{
										newColor = new Microsoft.Xna.Framework.Color(255, 255, 50);
									}

									int num22 = Dust.NewDust(new Vector2((float) (j * 16), value.Y - 2f), 16, 8, 43, 0f,
										0f, 254, newColor, 0.75f);
									Main.dust[num22].velocity *= 0f;
								}
							}

							if (liquid.Type == LiquidID.honey)
							{
								num17 *= 1.6f;
								if (num17 > 1f)
								{
									num17 = 1f;
								}
							}

							if (liquid.Type == LiquidID.lava)
							{
								num17 *= 1.8f;
								if (num17 > 1f)
								{
									num17 = 1f;
								}

								if (self.IsActive && !Main.gamePaused && Dust.lavaBubbles < 200)
								{
									if (liquid.Amount > 200 && Main.rand.Next(700) == 0)
									{
										Dust.NewDust(new Vector2((float) (j * 16), (float) (i * 16)), 16, 16, 35, 0f,
											0f, 0, default(Microsoft.Xna.Framework.Color), 1f);
									}

									if (value2.Y == 0 && Main.rand.Next(350) == 0)
									{
										int num23 = Dust.NewDust(
											new Vector2((float) (j * 16), (float) (i * 16) + num11 * 2f - 8f), 16, 8,
											35, 0f, 0f, 50, default(Microsoft.Xna.Framework.Color), 1.5f);
										Main.dust[num23].velocity *= 0.8f;
										Dust expr_9CA_cp_0 = Main.dust[num23];
										expr_9CA_cp_0.velocity.X = expr_9CA_cp_0.velocity.X * 2f;
										Dust expr_9E8_cp_0 = Main.dust[num23];
										expr_9E8_cp_0.velocity.Y =
											expr_9E8_cp_0.velocity.Y - (float) Main.rand.Next(1, 7) * 0.1f;
										if (Main.rand.Next(10) == 0)
										{
											Dust expr_A22_cp_0 = Main.dust[num23];
											expr_A22_cp_0.velocity.Y =
												expr_A22_cp_0.velocity.Y * (float) Main.rand.Next(2, 5);
										}

										Main.dust[num23].noGravity = true;
									}
								}
							}

							float num24 = (float) color.R * num17;
							float num25 = (float) color.G * num17;
							float num26 = (float) color.B * num17;
							float num27 = (float) color.A * num17;
							color = new Microsoft.Xna.Framework.Color((int) ((byte) num24), (int) ((byte) num25),
								(int) ((byte) num26), (int) ((byte) num27));
							if (Lighting.NotRetro && !bg)
							{
								Microsoft.Xna.Framework.Color color2 = color;
								if (num12 != 1 && ((double) color2.R > (double) num6 * 0.6 ||
								                   (double) color2.G > (double) num6 * 0.65 ||
								                   (double) color2.B > (double) num6 * 0.7))
								{
									for (int k = 0; k < 4; k++)
									{
										int num28 = 0;
										int num29 = 0;
										int width = 8;
										int height = 8;
										Microsoft.Xna.Framework.Color color3 = color2;
										Microsoft.Xna.Framework.Color color4 = Lighting.GetColor(j, i);
										if (k == 0)
										{
											color4 = Lighting.GetColor(j - 1, i - 1);
											if (value2.Height < 8)
											{
												height = value2.Height;
											}
										}

										if (k == 1)
										{
											color4 = Lighting.GetColor(j + 1, i - 1);
											num28 = 8;
											if (value2.Height < 8)
											{
												height = value2.Height;
											}
										}

										if (k == 2)
										{
											color4 = Lighting.GetColor(j - 1, i + 1);
											num29 = 8;
											height = 8 - (16 - value2.Height);
										}

										if (k == 3)
										{
											color4 = Lighting.GetColor(j + 1, i + 1);
											num28 = 8;
											num29 = 8;
											height = 8 - (16 - value2.Height);
										}

										num24 = (float) color4.R * num17;
										num25 = (float) color4.G * num17;
										num26 = (float) color4.B * num17;
										num27 = (float) color4.A * num17;
										color4 = new Microsoft.Xna.Framework.Color((int) ((byte) num24),
											(int) ((byte) num25), (int) ((byte) num26), (int) ((byte) num27));
										color3.R = (byte) ((color2.R * 3 + color4.R * 2) / 5);
										color3.G = (byte) ((color2.G * 3 + color4.G * 2) / 5);
										color3.B = (byte) ((color2.B * 3 + color4.B * 2) / 5);
										color3.A = (byte) ((color2.A * 3 + color4.A * 2) / 5);
										Main.spriteBatch.Draw(Main.liquidTexture[num12],
											value - Main.screenPosition + new Vector2((float) num28, (float) num29) +
											zero,
											new Microsoft.Xna.Framework.Rectangle?(
												new Microsoft.Xna.Framework.Rectangle(value2.X + num28,
													value2.Y + num29, width, height)), color3, 0f, default(Vector2), 1f,
											SpriteEffects.None, 0f);
									}
								}
								else
								{
									Main.spriteBatch.Draw(Main.liquidTexture[num12], value - Main.screenPosition + zero,
										new Microsoft.Xna.Framework.Rectangle?(value2), color, 0f, default(Vector2), 1f,
										SpriteEffects.None, 0f);
								}
							}
							else
							{
								if (value2.Y < 4)
								{
									value2.X += (int) (Main.wFrame * 18f);
								}

								Main.spriteBatch.Draw(Main.liquidTexture[num12], value - Main.screenPosition + zero,
									new Microsoft.Xna.Framework.Rectangle?(value2), color, 0f, default(Vector2), 1f,
									SpriteEffects.None, 0f);
							}

							if (liquidUp.Tile.halfBrick())
							{
								color = Lighting.GetColor(j, i + 1);
								num24 = (float) color.R * num17;
								num25 = (float) color.G * num17;
								num26 = (float) color.B * num17;
								num27 = (float) color.A * num17;
								color = new Microsoft.Xna.Framework.Color((int) ((byte) num24), (int) ((byte) num25),
									(int) ((byte) num26), (int) ((byte) num27));
								value = new Vector2((float) (j * 16), (float) (i * 16 + 16));
								Main.spriteBatch.Draw(Main.liquidTexture[num12], value - Main.screenPosition + zero,
									new Microsoft.Xna.Framework.Rectangle?(
										new Microsoft.Xna.Framework.Rectangle(0, 4, 16, 8)), color, 0f,
									default(Vector2), 1f, SpriteEffects.None, 0f);
							}
						}
					}

					IL_E7F: ;
				}
			}

			if (!Main.drewLava)
			{
				Main.ambientLavaX = (float) num4;
				Main.ambientLavaY = (float) num5;
				Main.ambientLavaStrength = num;
			}

			Main.drewLava = true;
		}
	}
}