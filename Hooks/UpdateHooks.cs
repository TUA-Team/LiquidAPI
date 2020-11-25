using System;
using Terraria;
using Terraria.ID;

namespace LiquidAPI.Hooks
{
    internal static class UpdateHooks
	{
		public static void Init()
		{
			On.Terraria.Liquid.Update += Update;
		}

		private static void Update(On.Terraria.Liquid.orig_Update orig, Liquid self)
		{
			Main.tileSolid[TileID.Bubble] = true;

			Tile tileLeft = Main.tile[self.x - 1, self.y];
			Tile tileRight = Main.tile[self.x + 1, self.y];
			Tile tileUp = Main.tile[self.x, self.y - 1];
			Tile tileDown = Main.tile[self.x, self.y + 1];
			Tile tileSelf = Main.tile[self.x, self.y];

			// If a full block has been placed that isn't something like a platform,
			// kill the liquid
			if (tileSelf.nactive() && Main.tileSolid[tileSelf.type] && !Main.tileSolidTop[tileSelf.type])
			{
				self.kill = 999;
				return;
			}

			byte liquidAmount = tileSelf.liquid;

			// If the liquid is in the underworld and is a water, evaporate
            if (tileSelf.liquidType() == 0 && self.y > Main.UnderworldLayer)
            {
                byte b = 2;
                if (tileSelf.liquid < b)
                {
                    b = tileSelf.liquid;
                }
                tileSelf.liquid -= b;
            }

			// If the liquid has completely disappeared for either of the above reason,
			// remove itself from the world
            if (tileSelf.liquid <= 0)
			{
				self.kill = 999;
				return;
			}

			if (tileSelf.lava())
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
				if (tileLeft.lava())
				{
					Liquid.AddWater(self.x - 1, self.y);
				}
				if (tileRight.lava())
				{
					Liquid.AddWater(self.x + 1, self.y);
				}
				if (tileUp.lava())
				{
					Liquid.AddWater(self.x, self.y - 1);
				}
				if (tileDown.lava())
				{
					Liquid.AddWater(self.x, self.y + 1);
				}
				if (tileSelf.honey())
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
					if (tileLeft.honey())
					{
						Liquid.AddWater(self.x - 1, self.y);
					}
					if (tileRight.honey())
					{
						Liquid.AddWater(self.x + 1, self.y);
					}
					if (tileUp.honey())
					{
						Liquid.AddWater(self.x, self.y - 1);
					}
					if (tileDown.honey())
					{
						Liquid.AddWater(self.x, self.y + 1);
					}
				}
			}

            float num;

			/* Conditions defined below:
			 *	1) The tile below the liquid is valid if
			 *		a) It does not exist
			 *		b) It is not solid
			 *		c) It only has a solid top
			 *	2) The below liquid is not full and has the same type as the current liquid
			 *		OR has no liquid at all
			*/
            if ((!tileDown.nactive() || !Main.tileSolid[tileDown.type] || Main.tileSolidTop[tileDown.type]) && (tileDown.liquid <= 0 || tileDown.liquidType() == tileSelf.liquidType()) && tileDown.liquid < byte.MaxValue)
            {
                // In this entire if block, `num` represents the amount of liquid required for
                // the liquid below the current one to be filled
                num = 255 - tileDown.liquid;
				// Cap it at the current liquid's amount
                if (num > tileSelf.liquid)
                    num = tileSelf.liquid;

                // `flag` represents if the tile below is almsot full and the current liquid is full
                bool flag = num == 1f && tileSelf.liquid == byte.MaxValue;

				// If they aren't nearly full, then remove the amount dumped from the current liquid
				// This is probably to prevent rounding errors of some kind
                if (!flag)
                    tileSelf.liquid -= (byte)num;

				// Dump all the liquid into the tile below it
                tileDown.liquid += (byte)num;

				// Quickly force the bottom liquid to have the same type as this one
				// This happens in the case that there was no liquid at all
				// LiquidType(byte) is incredibly fast, so we don't really have to check
                tileDown.liquidType(tileSelf.liquidType());

                Liquid.AddWater(self.x, self.y + 1);

                tileDown.skipLiquid(skipLiquid: true);
                tileSelf.skipLiquid(skipLiquid: true);

                if (Liquid.quickSettle && tileSelf.liquid > 250)
                    tileSelf.liquid = byte.MaxValue;
                
                else if (!flag)
                {
                    Liquid.AddWater(self.x - 1, self.y);
                    Liquid.AddWater(self.x + 1, self.y);
                }
            }
            if (tileSelf.liquid > 0)
			{
				bool flag2 = true;
				bool flag3 = true;
				bool flag4 = true;
				bool flag5 = true;
				if (tileLeft.nactive() && Main.tileSolid[tileLeft.type] && !Main.tileSolidTop[tileLeft.type])
				{
					flag2 = false;
				}
				else if (tileLeft.liquid > 0 && tileLeft.liquidType() != tileSelf.liquidType())
				{
					flag2 = false;
				}
				else if (Main.tile[self.x - 2, self.y].nactive() && Main.tileSolid[Main.tile[self.x - 2, self.y].type] && !Main.tileSolidTop[Main.tile[self.x - 2, self.y].type])
				{
					flag4 = false;
				}
				else if (Main.tile[self.x - 2, self.y].liquid == 0)
				{
					flag4 = false;
				}
				else if (Main.tile[self.x - 2, self.y].liquid > 0 && Main.tile[self.x - 2, self.y].liquidType() != tileSelf.liquidType())
				{
					flag4 = false;
				}
				if (tileRight.nactive() && Main.tileSolid[tileRight.type] && !Main.tileSolidTop[tileRight.type])
				{
					flag3 = false;
				}
				else if (tileRight.liquid > 0 && tileRight.liquidType() != tileSelf.liquidType())
				{
					flag3 = false;
				}
				else if (Main.tile[self.x + 2, self.y].nactive() && Main.tileSolid[Main.tile[self.x + 2, self.y].type] && !Main.tileSolidTop[Main.tile[self.x + 2, self.y].type])
				{
					flag5 = false;
				}
				else if (Main.tile[self.x + 2, self.y].liquid == 0)
				{
					flag5 = false;
				}
				else if (Main.tile[self.x + 2, self.y].liquid > 0 && Main.tile[self.x + 2, self.y].liquidType() != tileSelf.liquidType())
				{
					flag5 = false;
				}
				int num2 = 0;
				if (tileSelf.liquid < 3)
				{
					num2 = -1;
				}
				if (tileSelf.liquid > 250)
				{
					flag4 = false;
					flag5 = false;
				}
				if (flag2 && flag3)
				{
					if (flag4 && flag5)
					{
						bool flag6 = true;
						bool flag7 = true;
						if (Main.tile[self.x - 3, self.y].nactive() && Main.tileSolid[Main.tile[self.x - 3, self.y].type] && !Main.tileSolidTop[Main.tile[self.x - 3, self.y].type])
						{
							flag6 = false;
						}
						else if (Main.tile[self.x - 3, self.y].liquid == 0)
						{
							flag6 = false;
						}
						else if (Main.tile[self.x - 3, self.y].liquidType() != tileSelf.liquidType())
						{
							flag6 = false;
						}
						if (Main.tile[self.x + 3, self.y].nactive() && Main.tileSolid[Main.tile[self.x + 3, self.y].type] && !Main.tileSolidTop[Main.tile[self.x + 3, self.y].type])
						{
							flag7 = false;
						}
						else if (Main.tile[self.x + 3, self.y].liquid == 0)
						{
							flag7 = false;
						}
						else if (Main.tile[self.x + 3, self.y].liquidType() != tileSelf.liquidType())
						{
							flag7 = false;
						}
						if (flag6 && flag7)
						{
							num = (float)Math.Round((float)(tileLeft.liquid + tileRight.liquid + Main.tile[self.x - 2, self.y].liquid + Main.tile[self.x + 2, self.y].liquid + Main.tile[self.x - 3, self.y].liquid + Main.tile[self.x + 3, self.y].liquid + tileSelf.liquid + num2) / 7f);
							int num3 = 0;
							tileLeft.liquidType(tileSelf.liquidType());
							if (tileLeft.liquid != (byte)num)
							{
								tileLeft.liquid = (byte)num;
								Liquid.AddWater(self.x - 1, self.y);
							}
							else
							{
								num3++;
							}
							tileRight.liquidType(tileSelf.liquidType());
							if (tileRight.liquid != (byte)num)
							{
								tileRight.liquid = (byte)num;
								Liquid.AddWater(self.x + 1, self.y);
							}
							else
							{
								num3++;
							}
							Main.tile[self.x - 2, self.y].liquidType(tileSelf.liquidType());
							if (Main.tile[self.x - 2, self.y].liquid != (byte)num)
							{
								Main.tile[self.x - 2, self.y].liquid = (byte)num;
								Liquid.AddWater(self.x - 2, self.y);
							}
							else
							{
								num3++;
							}
							Main.tile[self.x + 2, self.y].liquidType(tileSelf.liquidType());
							if (Main.tile[self.x + 2, self.y].liquid != (byte)num)
							{
								Main.tile[self.x + 2, self.y].liquid = (byte)num;
								Liquid.AddWater(self.x + 2, self.y);
							}
							else
							{
								num3++;
							}
							Main.tile[self.x - 3, self.y].liquidType(tileSelf.liquidType());
							if (Main.tile[self.x - 3, self.y].liquid != (byte)num)
							{
								Main.tile[self.x - 3, self.y].liquid = (byte)num;
								Liquid.AddWater(self.x - 3, self.y);
							}
							else
							{
								num3++;
							}
							Main.tile[self.x + 3, self.y].liquidType(tileSelf.liquidType());
							if (Main.tile[self.x + 3, self.y].liquid != (byte)num)
							{
								Main.tile[self.x + 3, self.y].liquid = (byte)num;
								Liquid.AddWater(self.x + 3, self.y);
							}
							else
							{
								num3++;
							}
							if (tileLeft.liquid != (byte)num || tileSelf.liquid != (byte)num)
							{
								Liquid.AddWater(self.x - 1, self.y);
							}
							if (tileRight.liquid != (byte)num || tileSelf.liquid != (byte)num)
							{
								Liquid.AddWater(self.x + 1, self.y);
							}
							if (Main.tile[self.x - 2, self.y].liquid != (byte)num || tileSelf.liquid != (byte)num)
							{
								Liquid.AddWater(self.x - 2, self.y);
							}
							if (Main.tile[self.x + 2, self.y].liquid != (byte)num || tileSelf.liquid != (byte)num)
							{
								Liquid.AddWater(self.x + 2, self.y);
							}
							if (Main.tile[self.x - 3, self.y].liquid != (byte)num || tileSelf.liquid != (byte)num)
							{
								Liquid.AddWater(self.x - 3, self.y);
							}
							if (Main.tile[self.x + 3, self.y].liquid != (byte)num || tileSelf.liquid != (byte)num)
							{
								Liquid.AddWater(self.x + 3, self.y);
							}
							if (num3 != 6 || tileUp.liquid <= 0)
							{
								tileSelf.liquid = (byte)num;
							}
						}
						else
						{
							int num4 = 0;
							num = (float)Math.Round((float)(tileLeft.liquid + tileRight.liquid + Main.tile[self.x - 2, self.y].liquid + Main.tile[self.x + 2, self.y].liquid + tileSelf.liquid + num2) / 5f);
							tileLeft.liquidType(tileSelf.liquidType());
							if (tileLeft.liquid != (byte)num)
							{
								tileLeft.liquid = (byte)num;
								Liquid.AddWater(self.x - 1, self.y);
							}
							else
							{
								num4++;
							}
							tileRight.liquidType(tileSelf.liquidType());
							if (tileRight.liquid != (byte)num)
							{
								tileRight.liquid = (byte)num;
								Liquid.AddWater(self.x + 1, self.y);
							}
							else
							{
								num4++;
							}
							Main.tile[self.x - 2, self.y].liquidType(tileSelf.liquidType());
							if (Main.tile[self.x - 2, self.y].liquid != (byte)num)
							{
								Main.tile[self.x - 2, self.y].liquid = (byte)num;
								Liquid.AddWater(self.x - 2, self.y);
							}
							else
							{
								num4++;
							}
							Main.tile[self.x + 2, self.y].liquidType(tileSelf.liquidType());
							if (Main.tile[self.x + 2, self.y].liquid != (byte)num)
							{
								Main.tile[self.x + 2, self.y].liquid = (byte)num;
								Liquid.AddWater(self.x + 2, self.y);
							}
							else
							{
								num4++;
							}
							if (tileLeft.liquid != (byte)num || tileSelf.liquid != (byte)num)
							{
								Liquid.AddWater(self.x - 1, self.y);
							}
							if (tileRight.liquid != (byte)num || tileSelf.liquid != (byte)num)
							{
								Liquid.AddWater(self.x + 1, self.y);
							}
							if (Main.tile[self.x - 2, self.y].liquid != (byte)num || tileSelf.liquid != (byte)num)
							{
								Liquid.AddWater(self.x - 2, self.y);
							}
							if (Main.tile[self.x + 2, self.y].liquid != (byte)num || tileSelf.liquid != (byte)num)
							{
								Liquid.AddWater(self.x + 2, self.y);
							}
							if (num4 != 4 || tileUp.liquid <= 0)
							{
								tileSelf.liquid = (byte)num;
							}
						}
					}
					else if (flag4)
					{
						num = (float)Math.Round((float)(tileLeft.liquid + tileRight.liquid + Main.tile[self.x - 2, self.y].liquid + tileSelf.liquid + num2) / 4f);
						tileLeft.liquidType(tileSelf.liquidType());
						if (tileLeft.liquid != (byte)num || tileSelf.liquid != (byte)num)
						{
							tileLeft.liquid = (byte)num;
							Liquid.AddWater(self.x - 1, self.y);
						}
						tileRight.liquidType(tileSelf.liquidType());
						if (tileRight.liquid != (byte)num || tileSelf.liquid != (byte)num)
						{
							tileRight.liquid = (byte)num;
							Liquid.AddWater(self.x + 1, self.y);
						}
						Main.tile[self.x - 2, self.y].liquidType(tileSelf.liquidType());
						if (Main.tile[self.x - 2, self.y].liquid != (byte)num || tileSelf.liquid != (byte)num)
						{
							Main.tile[self.x - 2, self.y].liquid = (byte)num;
							Liquid.AddWater(self.x - 2, self.y);
						}
						tileSelf.liquid = (byte)num;
					}
					else if (flag5)
					{
						num = (float)Math.Round((float)(tileLeft.liquid + tileRight.liquid + Main.tile[self.x + 2, self.y].liquid + tileSelf.liquid + num2) / 4f);
						tileLeft.liquidType(tileSelf.liquidType());
						if (tileLeft.liquid != (byte)num || tileSelf.liquid != (byte)num)
						{
							tileLeft.liquid = (byte)num;
							Liquid.AddWater(self.x - 1, self.y);
						}
						tileRight.liquidType(tileSelf.liquidType());
						if (tileRight.liquid != (byte)num || tileSelf.liquid != (byte)num)
						{
							tileRight.liquid = (byte)num;
							Liquid.AddWater(self.x + 1, self.y);
						}
						Main.tile[self.x + 2, self.y].liquidType(tileSelf.liquidType());
						if (Main.tile[self.x + 2, self.y].liquid != (byte)num || tileSelf.liquid != (byte)num)
						{
							Main.tile[self.x + 2, self.y].liquid = (byte)num;
							Liquid.AddWater(self.x + 2, self.y);
						}
						tileSelf.liquid = (byte)num;
					}
					else
					{
						num = (float)Math.Round((float)(tileLeft.liquid + tileRight.liquid + tileSelf.liquid + num2) / 3f);
						if (num == 254f && WorldGen.genRand.Next(30) == 0)
						{
							num = 255f;
						}
						tileLeft.liquidType(tileSelf.liquidType());
						if (tileLeft.liquid != (byte)num)
						{
							tileLeft.liquid = (byte)num;
							Liquid.AddWater(self.x - 1, self.y);
						}
						tileRight.liquidType(tileSelf.liquidType());
						if (tileRight.liquid != (byte)num)
						{
							tileRight.liquid = (byte)num;
							Liquid.AddWater(self.x + 1, self.y);
						}
						tileSelf.liquid = (byte)num;
					}
				}
				else if (flag2)
				{
					num = (float)Math.Round((float)(tileLeft.liquid + tileSelf.liquid + num2) / 2f);
					if (tileLeft.liquid != (byte)num)
					{
						tileLeft.liquid = (byte)num;
					}
					tileLeft.liquidType(tileSelf.liquidType());
					if (tileSelf.liquid != (byte)num || tileLeft.liquid != (byte)num)
					{
						Liquid.AddWater(self.x - 1, self.y);
					}
					tileSelf.liquid = (byte)num;
				}
				else if (flag3)
				{
					num = (float)Math.Round((float)(tileRight.liquid + tileSelf.liquid + num2) / 2f);
					if (tileRight.liquid != (byte)num)
					{
						tileRight.liquid = (byte)num;
					}
					tileRight.liquidType(tileSelf.liquidType());
					if (tileSelf.liquid != (byte)num || tileRight.liquid != (byte)num)
					{
						Liquid.AddWater(self.x + 1, self.y);
					}
					tileSelf.liquid = (byte)num;
				}
			}
			if (tileSelf.liquid != liquidAmount)
			{
				if (tileSelf.liquid == 254 && liquidAmount == byte.MaxValue)
				{
					if (Liquid.quickSettle)
					{
						tileSelf.liquid = byte.MaxValue;
						self.kill++;
					}
					else
					{
						self.kill++;
					}
				}
				else
				{
					Liquid.AddWater(self.x, self.y - 1);
					self.kill = 0;
				}
			}
			else
			{
				self.kill++;
			}
		}
	}
}