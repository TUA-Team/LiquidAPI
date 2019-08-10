using System;
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
            LiquidRef liquidLeft = LiquidWorld.grid[self.x - 1, self.y];
            LiquidRef liquidRight = LiquidWorld.grid[self.x + 1, self.y];
            LiquidRef liquidUp = LiquidWorld.grid[self.x, self.y - 1];
            LiquidRef liquidDown = LiquidWorld.grid[self.x, self.y + 1];
            LiquidRef liquidSelf = LiquidWorld.grid[self.x, self.y];

            if (liquidSelf.Tile.nactive() && Main.tileSolid[liquidSelf.Tile.type] && !Main.tileSolidTop[liquidSelf.Tile.type])
            {
                self.kill = 9;
                return;
            }

            byte liquid = liquidSelf.Amount;
            if (self.y > Main.maxTilesY - 200 && liquidSelf.TypeID == 0 && liquidSelf.Amount > 0)
            {
                if (liquidSelf.Amount >= 2)
                {
                    liquidSelf.Amount -= 2;
                }
            }

            if (liquidSelf.Amount == 0)
            {
                self.kill = 9;
                return;
            }

            if (liquidSelf.TypeID == LiquidID.Lava)
            {
                Liquid.LavaCheck(self.x, self.y);
                if (!Liquid.quickFall)
                {
                    if (self.delay > 0)
                    {
                        self.delay--;
                        return;
                    }

                    self.delay = 5;
                }
            }
            else
            {
                if (liquidLeft.TypeID == LiquidID.Lava)
                {
                    Liquid.AddWater(self.x - 1, self.y);
                }

                if (liquidRight.TypeID == LiquidID.Lava)
                {
                    Liquid.AddWater(self.x + 1, self.y);
                }

                if (liquidUp.TypeID == LiquidID.Lava)
                {
                    Liquid.AddWater(self.x, self.y - 1);
                }

                if (liquidDown.TypeID == LiquidID.Lava)
                {
                    Liquid.AddWater(self.x, self.y + 1);
                }

                if (liquidSelf.TypeID == LiquidID.Honey)
                {
                    Liquid.HoneyCheck(self.x, self.y);
                    if (!Liquid.quickFall)
                    {
                        if (self.delay > 0)
                        {
                            self.delay--;
                            return;
                        }

                        self.delay = 10;
                    }
                }
                else
                {
                    if (liquidLeft.TypeID == LiquidID.Honey)
                    {
                        Liquid.AddWater(self.x - 1, self.y);
                    }

                    if (liquidRight.TypeID == LiquidID.Honey)
                    {
                        Liquid.AddWater(self.x + 1, self.y);
                    }

                    if (liquidUp.TypeID == LiquidID.Honey)
                    {
                        Liquid.AddWater(self.x, self.y - 1);
                    }

                    if (liquidDown.TypeID == LiquidID.Honey)
                    {
                        Liquid.AddWater(self.x, self.y + 1);
                    }
                }
            }

            if ((!liquidDown.Tile.nactive() || !Main.tileSolid[(int)liquidDown.Tile.type] ||
                 Main.tileSolidTop[(int)liquidDown.Tile.type]) &&
                (liquidDown.Amount <= 0 || liquidDown.TypeID == liquidSelf.TypeID) && liquidDown.Amount < 255)
            {
                float num = (float)(255 - liquidDown.Amount);
                if (num > (float)liquidSelf.Amount)
                {
                    num = (float)liquidSelf.Amount;
                }

                liquidSelf.Amount -= (byte)num;
                liquidDown.Amount += (byte)num;
                liquidDown.TypeID = liquidSelf.TypeID;
                Liquid.AddWater(self.x, self.y + 1);
                liquidDown.SkipLiquid = true;
                liquidSelf.SkipLiquid = true;
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
                else if (liquidLeft.Amount > 0 && liquidLeft.TypeID != liquidSelf.TypeID)
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
                         LiquidWorld.liquidGrid[self.x - 2, self.y].data != liquidSelf.TypeID)
                {
                    flag3 = false;
                }

                if (liquidRight.Tile.nactive() && Main.tileSolid[(int)liquidRight.Tile.type] &&
                    !Main.tileSolidTop[(int)liquidRight.Tile.type])
                {
                    flag2 = false;
                }
                else if (liquidRight.Amount > 0 && liquidRight.TypeID != liquidSelf.TypeID)
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
                         LiquidWorld.liquidGrid[self.x + 2, self.y].data != liquidSelf.TypeID)
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
                        else if (LiquidWorld.liquidGrid[self.x - 3, self.y].data != liquidSelf.TypeID)
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
                        else if (LiquidWorld.liquidGrid[self.x + 3, self.y].data != liquidSelf.TypeID)
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
                            liquidLeft.TypeID = liquidSelf.TypeID;
                            if (liquidLeft.Amount != (byte)num)
                            {
                                liquidLeft.Amount = (byte)num;
                                Liquid.AddWater(self.x - 1, self.y);
                            }
                            else
                            {
                                num3++;
                            }

                            liquidRight.TypeID = liquidSelf.TypeID;
                            if (liquidRight.Amount != (byte)num)
                            {
                                liquidRight.Amount = (byte)num;
                                Liquid.AddWater(self.x + 1, self.y);
                            }
                            else
                            {
                                num3++;
                            }

                            LiquidWorld.liquidGrid[self.x - 2, self.y].data = liquidSelf.TypeID;
                            if (Main.tile[self.x - 2, self.y].liquid != (byte)num)
                            {
                                Main.tile[self.x - 2, self.y].liquid = (byte)num;
                                Liquid.AddWater(self.x - 2, self.y);
                            }
                            else
                            {
                                num3++;
                            }

                            LiquidWorld.liquidGrid[self.x + 2, self.y].data = liquidSelf.TypeID;
                            if (Main.tile[self.x + 2, self.y].liquid != (byte)num)
                            {
                                Main.tile[self.x + 2, self.y].liquid = (byte)num;
                                Liquid.AddWater(self.x + 2, self.y);
                            }
                            else
                            {
                                num3++;
                            }

                            LiquidWorld.liquidGrid[self.x - 3, self.y].data = liquidSelf.TypeID;
                            if (Main.tile[self.x - 3, self.y].liquid != (byte)num)
                            {
                                Main.tile[self.x - 3, self.y].liquid = (byte)num;
                                Liquid.AddWater(self.x - 3, self.y);
                            }
                            else
                            {
                                num3++;
                            }

                            LiquidWorld.liquidGrid[self.x + 3, self.y].data = liquidSelf.TypeID;
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
                            liquidLeft.TypeID = liquidSelf.TypeID;
                            if (liquidLeft.Amount != (byte)num)
                            {
                                liquidLeft.Amount = (byte)num;
                                Liquid.AddWater(self.x - 1, self.y);
                            }
                            else
                            {
                                num4++;
                            }

                            liquidRight.TypeID = liquidSelf.TypeID;
                            if (liquidRight.Amount != (byte)num)
                            {
                                liquidRight.Amount = (byte)num;
                                Liquid.AddWater(self.x + 1, self.y);
                            }
                            else
                            {
                                num4++;
                            }

                            LiquidWorld.liquidGrid[self.x - 2, self.y].data = liquidSelf.TypeID;
                            if (Main.tile[self.x - 2, self.y].liquid != (byte)num)
                            {
                                Main.tile[self.x - 2, self.y].liquid = (byte)num;
                                Liquid.AddWater(self.x - 2, self.y);
                            }
                            else
                            {
                                num4++;
                            }

                            LiquidWorld.liquidGrid[self.x + 2, self.y].data = liquidSelf.TypeID;
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
                        liquidLeft.TypeID = liquidSelf.TypeID;
                        if (liquidLeft.Amount != (byte)num || liquidSelf.Amount != (byte)num)
                        {
                            liquidLeft.Amount = (byte)num;
                            Liquid.AddWater(self.x - 1, self.y);
                        }

                        liquidRight.TypeID = liquidSelf.TypeID;
                        if (liquidRight.Amount != (byte)num || liquidSelf.Amount != (byte)num)
                        {
                            liquidRight.Amount = (byte)num;
                            Liquid.AddWater(self.x + 1, self.y);
                        }

                        LiquidWorld.liquidGrid[self.x - 2, self.y].data = liquidSelf.TypeID;
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
                        liquidLeft.TypeID = liquidSelf.TypeID;
                        if (liquidLeft.Amount != (byte)num || liquidSelf.Amount != (byte)num)
                        {
                            liquidLeft.Amount = (byte)num;
                            Liquid.AddWater(self.x - 1, self.y);
                        }

                        liquidRight.TypeID = liquidSelf.TypeID;
                        if (liquidRight.Amount != (byte)num || liquidSelf.Amount != (byte)num)
                        {
                            liquidRight.Amount = (byte)num;
                            Liquid.AddWater(self.x + 1, self.y);
                        }

                        LiquidWorld.liquidGrid[self.x + 2, self.y].data = liquidSelf.TypeID;
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
                        liquidLeft.TypeID = liquidSelf.TypeID;
                        liquidLeft.Amount = (byte)num;

                        if (liquidSelf.Amount != (byte)num || liquidLeft.Amount != (byte)num)
                        {
                            Liquid.AddWater(self.x - 1, self.y);
                        }

                        liquidRight.TypeID = liquidSelf.TypeID;
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

                    liquidLeft.TypeID = liquidSelf.TypeID;
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

                    liquidRight.TypeID = liquidSelf.TypeID;
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
    }
}