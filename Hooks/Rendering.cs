using System;
using System.Collections.Generic;
using LiquidAPI.ID;
using LiquidAPI.LiquidMod;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;

namespace LiquidAPI.Hooks
{
	internal static partial class LiquidHooks
	{
	    public static Texture2D OldHoneyTexture;
	    public static Texture2D OldLavaTexture;
	    public static List<Texture2D> OldWaterTexture; 
    
		private static void MainOnOldDrawWater(On.Terraria.Main.orig_oldDrawWater orig, Main self, bool bg = false, int Style = 0, float Alpha = 1f)
		{
		    Texture2D liquidTexture = OldWaterTexture[Style];
			float num = 0f;
			float num2 = 99999f;
			float num3 = 99999f;
			int num4 = -1;
			int num5 = -1;
			Vector2 zero = Main.drawToScreen?Vector2.Zero:new Vector2(Main.offScreenRange,Main.offScreenRange);

			int num6 = (int) (255f * (1f - Main.gfxQuality) + 40f * Main.gfxQuality);
			int num7 = (int) ((Main.screenPosition.X - zero.X) / 16f - 1f);
			int num8 = (int) ((Main.screenPosition.X +  Main.screenWidth + zero.X) / 16f) + 2;
			int num9 = (int) ((Main.screenPosition.Y - zero.Y) / 16f - 1f);
			int num10 = (int) ((Main.screenPosition.Y +  Main.screenHeight + zero.Y) / 16f) + 5;
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

                    if (liquid.Amount > 0 && (!liquid.Tile.nactive() || !Main.tileSolid[liquid.Tile.type] || Main.tileSolidTop[liquid.Tile.type]) && (Lighting.Brightness(j, i) > 0f || bg))
					{
						Color color = Lighting.GetColor(j, i);
						float num11 = 256 - liquid.Amount;
						num11 /= 32f;
						int num12 = 0;
						if (liquid.TypeID == LiquidID.Lava)
						{
							if (Main.drewLava)
							{
								goto IL_E7F;
							}

							float num13 = Math.Abs(j * 16 + 8 - (Main.screenPosition.X + (Main.screenWidth / 2)));
							float num14 = Math.Abs(i * 16 + 8 - (Main.screenPosition.Y + (Main.screenHeight / 2)));
							if (num13 <  (Main.screenWidth * 2) && num14 <  (Main.screenHeight * 2))
							{
								float num15 = (float) Math.Sqrt(num13 * num13 + num14 * num14);
								float num16 = 1f - num15 / ( Main.screenWidth * 0.75f);
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

						    liquidTexture = OldLavaTexture;
						}
						else if (liquid.TypeID == LiquidID.Honey)
						{
						    liquidTexture = OldHoneyTexture;
						}
					    else if(liquid.TypeID != LiquidID.Water)
						{
						    liquidTexture = LiquidRegistry.getInstance()[liquid.TypeID].Texture;
						}

					    if (num12 == 0)
						{
							num12 = Style;
						}

						if ((num12 != 1 && num12 <= 11) || !Main.drewLava)
						{
							float num17 = 0.5f;
							if (bg)
							{
								num17 = 1f;
							}

							if (num12 != 1 && num12 <= 11)
							{
								num17 *= Alpha;
							}

							Vector2 value = new Vector2(j * 16,i * 16 + (int) num11 * 2);
							Rectangle value2 = new Rectangle(0, 0, 16, 16 - (int) num11 * 2);

							if (liquidUp.Amount < 245 && (!liquidUp.Tile.nactive() || !Main.tileSolid[liquidUp.Tile.type] || Main.tileSolidTop[liquidUp.Tile.type]))
							{
								float num18 = 256 - liquidUp.Amount;
								num18 /= 32f;
								num17 = 0.5f * (8f - num11) / 4f;
								if (num17 > 0.55f)
								{
									num17 = 0.55f;
								}

								if (num17 < 0.35f)
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
										value2 = new Rectangle(0, 4, 16, 16);
										num17 = 0.5f;
									}
									else if (liquidDown.Amount > 0)
									{
										value = new Vector2(j * 16,i * 16 + 4);
										value2 = new Rectangle(0, 4, 16, 12);
										num17 = 0.5f;
									}
									else if (liquidUp.Amount > 0)
									{
										value = new Vector2(j * 16,i * 16 + (int) num11 * 2 + (int) num18 * 2);
										value2 = new Rectangle(0, 4, 16, 16 - (int) num11 * 2);
									}
									else
									{
										value = new Vector2(j * 16 + (int) num19,i * 16 + (int) num19 * 2 + (int) num18 * 2);
										value2 = new Rectangle(0, 4, 16 - (int) num19 * 2, 16 - (int) num19 * 2);
									}
								}
								else
								{
									num17 = 0.5f;
									value2 = new Rectangle(0, 4, 16, 16 - (int) num11 * 2 + (int) num18 * 2);
								}
							}
							else if (liquidDown.Amount > 32)
							{
								value2 = new Rectangle(0, 4, value2.Width, value2.Height);
							}
							else if (num11 < 1f && liquidDown.Tile.nactive() &&
							         Main.tileSolid[liquidDown.Tile.type] &&
							         !Main.tileSolidTop[liquidDown.Tile.type])
							{
								value = new Vector2(j * 16,i * 16);
								value2 = new Rectangle(0, 4, 16, 16);
							}
							else
							{
								bool flag = true;
								int num20 = i + 1;
								while (num20 < i + 6 && (!LiquidCore.grid[j, num20].Tile.nactive() || !Main.tileSolid[LiquidCore.grid[j, num20].Tile.type] || Main.tileSolidTop[LiquidCore.grid[j, num20].Tile.type]))
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
									value2 = new Rectangle(0, 4, 16, 16);
								}
								else if (liquidDown.HasLiquid)
								{
									value2 = new Rectangle(0, 2, value2.Width, value2.Height);
								}
							}

							if ((color.R > 20 || color.B > 20 || color.G > 20) && value2.Y < 4)
							{
								int num21 = color.R;
								if (color.G > num21)
								{
									num21 = color.G;
								}

								if (color.B > num21)
								{
									num21 = color.B;
								}

								num21 /= 30;
								if (Main.rand.Next(20000) < num21)
								{
									Color newColor = new Color(255, 255, 255);
									if (liquid.TypeID == LiquidID.Honey)
									{
										newColor = new Color(255, 255, 50);
									}

									int num22 = Dust.NewDust(new Vector2(j * 16, value.Y - 2f), 16, 8, 43, 0f, 0f, 254, newColor, 0.75f);
									Main.dust[num22].velocity *= 0f;
								}
							}

							if (liquid.TypeID == LiquidID.Honey)
							{
								num17 *= 1.6f;
								if (num17 > 1f)
								{
									num17 = 1f;
								}
							}

							if (liquid.TypeID == LiquidID.Lava)
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
										Dust.NewDust(new Vector2(j * 16,i * 16), 16, 16, 35, 0f, 0f, 0, default(Color), 1f);
									}

									if (value2.Y == 0 && Main.rand.Next(350) == 0)
									{
										Dust dust=Main.dust[Dust.NewDust(new Vector2(j * 16,(i * 16) + num11 * 2f - 8f), 16, 8, 35, 0f, 0f, 50, default(Color), 1.5f)];
										dust.velocity *= 0.8f;
										dust.velocity.X *= 2f;
										dust.velocity.Y -= Main.rand.Next(1, 7) * 0.1f;
										if (Main.rand.Next(10) == 0)
										{
											dust.velocity.Y *= Main.rand.Next(2, 5);
										}
										dust.noGravity = true;
									}
								}
							}
							color *= num17;
							if (Lighting.NotRetro && !bg)
							{
								Color color2 = color;
								if (num12 != 1 && (color2.R >  num6 * 0.6f || color2.G >  num6 * 0.65f || color2.B >  num6 * 0.7f))
								{
									for (int k = 0; k < 4; k++)
									{
										int num28 = 0;
										int num29 = 0;
										int width = 8;
										int height = 8;
										Color color4;
										switch(k)
										{
											case 0:
											{
												color4 = Lighting.GetColor(j - 1, i - 1);
												if (value2.Height < 8)
												{
													height = value2.Height;
												}
												break;
											}

											case 1:
											{
												color4 = Lighting.GetColor(j + 1, i - 1);
												num28 = 8;
												if (value2.Height < 8)
												{
													height = value2.Height;
												}
												break;
											}

											case 2:
											{
												color4 = Lighting.GetColor(j - 1, i + 1);
												num29 = 8;
												height = 8 - (16 - value2.Height);
												break;
											}

											case 3:
											{
												color4 = Lighting.GetColor(j + 1, i + 1);
												num28 = 8;
												num29 = 8;
												height = 8 - (16 - value2.Height);
												break;
											}
											default:continue;
										}

										Color color3 = Color.Lerp(color2,color4 * num17,0.4f);
										Main.spriteBatch.Draw(Main.liquidTexture[num12],value - Main.screenPosition + new Vector2(num28, num29) +zero,new Rectangle(value2.X + num28,value2.Y + num29, width, height), color3, 0f, default(Vector2), 1f,SpriteEffects.None, 0f);
									}
								}
								else
								{
									Main.spriteBatch.Draw(liquidTexture, value - Main.screenPosition + zero,value2, color, 0f, default(Vector2), 1f,SpriteEffects.None, 0f);
								}
							}
							else
							{
								if (value2.Y < 4)
								{
									value2.X += (int) (Main.wFrame * 18f);
								}

								Main.spriteBatch.Draw(liquidTexture, value - Main.screenPosition + zero,value2, color, 0f, default(Vector2), 1f,SpriteEffects.None, 0f);
							}

							if (liquidUp.Tile.halfBrick())
							{
								color = Lighting.GetColor(j, i + 1) * num17;
								value = new Vector2(j * 16,i * 16 + 16);
								Main.spriteBatch.Draw(liquidTexture, value - Main.screenPosition + zero,new Rectangle(0, 4, 16, 8), color, 0f,default(Vector2), 1f, SpriteEffects.None, 0f);
							}
						}
					}

					IL_E7F:;
				}
			}

			if (!Main.drewLava)
			{
				Main.ambientLavaX = num4;
				Main.ambientLavaY = num5;
				Main.ambientLavaStrength = num;
			}

			Main.drewLava = true;
		}
	}
}