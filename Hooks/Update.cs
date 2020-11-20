using LiquidAPI.Caches;
using LiquidAPI.ID;
using LiquidAPI.LiquidMod;
using LiquidAPI.Vanilla;
using System;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using static Terraria.Liquid;

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
            if (self.y > Main.maxTilesY - 200 && liquidSelf.LiquidType is Water && liquidSelf.Amount > 0)
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
                    if (liquidLeft.TypeID != 0 && liquidLeft.TypeID == liquidSelf.TypeID)
                    {
                        Liquid.AddWater(self.x - 1, self.y);
                    }

                    if (liquidRight.TypeID != 0 && liquidRight.TypeID == liquidSelf.TypeID)
                    {
                        Liquid.AddWater(self.x + 1, self.y);
                    }

                    if (liquidUp.TypeID != 0 && liquidUp.TypeID == liquidSelf.TypeID)
                    {
                        Liquid.AddWater(self.x, self.y - 1);
                    }

                    if (liquidDown.TypeID != 0 && liquidDown.TypeID == liquidSelf.TypeID)
                    {
                        Liquid.AddWater(self.x, self.y + 1);
                    }
                }

                if (LiquidRegistry.liquidList.ContainsValue(liquidSelf.LiquidType) && !(liquidSelf.LiquidType is Water))
                {
                    LiquidRegistry.ModLiquidCheck(liquidSelf.LiquidType, self.x, self.y);
                    if (!Liquid.quickFall)
                    {
                        if (self.delay > 0)
                        {
                            self.delay--;
                            return;
                        }

                        self.delay = 10;
                    }
                    else
                    {
                        if (liquidLeft.TypeID == liquidSelf.TypeID)
                        {
                            Liquid.AddWater(self.x - 1, self.y);
                        }

                        if (liquidRight.TypeID == liquidSelf.TypeID)
                        {
                            Liquid.AddWater(self.x + 1, self.y);
                        }

                        if (liquidUp.TypeID == liquidSelf.TypeID)
                        {
                            Liquid.AddWater(self.x, self.y - 1);
                        }

                        if (liquidDown.TypeID == liquidSelf.TypeID)
                        {
                            Liquid.AddWater(self.x, self.y + 1);
                        }
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

        public static double QuickWater(On.Terraria.Liquid.orig_QuickWater orig, int verbose = 0, int minY = -1, int maxY = -1)
        {
            Main.tileSolid[379] = true;
            int num = 0;
            if (minY == -1)
                minY = 3;

            if (maxY == -1)
                maxY = Main.maxTilesY - 3;

            for (int num2 = maxY; num2 >= minY; num2--)
            {
                if (verbose > 0)
                {
                    float num3 = (float)(maxY - num2) / (float)(maxY - minY + 1);
                    num3 /= (float)verbose;
                    Main.statusText = Lang.gen[27].Value + " " + (int)(num3 * 100f + 1f) + "%";
                }
                else if (verbose < 0)
                {
                    float num4 = (float)(maxY - num2) / (float)(maxY - minY + 1);
                    num4 /= (float)(-verbose);
                    Main.statusText = Lang.gen[18].Value + " " + (int)(num4 * 100f + 1f) + "%";
                }

                for (int i = 0; i < 2; i++)
                {
                    int num5 = 2;
                    int num6 = Main.maxTilesX - 2;
                    int num7 = 1;
                    if (i == 1)
                    {
                        num5 = Main.maxTilesX - 2;
                        num6 = 2;
                        num7 = -1;
                    }

                    for (int j = num5; j != num6; j += num7)
                    {
                        LiquidRef tile = new LiquidRef(j, num2);
                        if (tile.Amount <= 0)
                            continue;

                        int num8 = -num7;
                        bool flag = false;
                        int num9 = j;
                        int num10 = num2;
                        byte b = (byte)tile.LiquidType.Type;
                        bool isLava = tile.LiquidType is Lava;
                        bool isHoney = tile.LiquidType is Honey;
                        byte liquidAmount = tile.Amount;
                        tile.Amount = 0;
                        bool flag4 = true;
                        int num11 = 0;
                        while (flag4 && num9 > 3 && num9 < Main.maxTilesX - 4 && num10 < Main.maxTilesY - 4)
                        {
                            flag4 = false;
                            while (LiquidWorld.grid[num9, num10 + 1].Amount == 0 && num10 < Main.maxTilesY - 5 && (!Main.tile[num9, num10 + 1].nactive() || !Main.tileSolid[Main.tile[num9, num10 + 1].type] || Main.tileSolidTop[Main.tile[num9, num10 + 1].type]))
                            {
                                flag = true;
                                num8 = num7;
                                num11 = 0;
                                flag4 = true;
                                num10++;
                                if (num10 > WorldGen.waterLine && WorldGen.gen && !isHoney)
                                    b = 1;
                            }

                            if (LiquidWorld.grid[num9, num10 + 1].Amount > 0 && LiquidWorld.grid[num9, num10 + 1].Amount < byte.MaxValue && Main.tile[num9, num10 + 1].liquidType() == b)
                            {
                                int num12 = 255 - LiquidWorld.grid[num9, num10 + 1].Amount;
                                if (num12 > liquidAmount)
                                    num12 = liquidAmount;

                                LiquidWorld.grid[num9, num10 + 1].Amount += (byte)num12;
                                liquidAmount = (byte)(liquidAmount - (byte)num12);
                                if (liquidAmount == 0)
                                {
                                    num++;
                                    break;
                                }
                            }

                            if (num11 == 0)
                            {
                                if (LiquidWorld.grid[num9 + num8, num10].Amount == 0 && (!Main.tile[num9 + num8, num10].nactive() || !Main.tileSolid[Main.tile[num9 + num8, num10].type] || Main.tileSolidTop[Main.tile[num9 + num8, num10].type]))
                                    num11 = num8;
                                else if (LiquidWorld.grid[num9 - num8, num10].Amount == 0 && (!Main.tile[num9 - num8, num10].nactive() || !Main.tileSolid[Main.tile[num9 - num8, num10].type] || Main.tileSolidTop[Main.tile[num9 - num8, num10].type]))
                                    num11 = -num8;
                            }

                            if (num11 != 0 && LiquidWorld.grid[num9 + num11, num10].Amount == 0 && (!Main.tile[num9 + num11, num10].nactive() || !Main.tileSolid[Main.tile[num9 + num11, num10].type] || Main.tileSolidTop[Main.tile[num9 + num11, num10].type]))
                            {
                                flag4 = true;
                                num9 += num11;
                            }

                            if (flag && !flag4)
                            {
                                flag = false;
                                flag4 = true;
                                num8 = -num7;
                                num11 = 0;
                            }
                        }

                        if (j != num9 && num2 != num10)
                            num++;

                        LiquidWorld.grid[num9, num10].Amount = liquidAmount;
                        LiquidRef liquidRef = LiquidWorld.grid[num9, num10];
                        liquidRef.LiquidType = LiquidRegistry.GetLiquid(b);
                        if (LiquidWorld.grid[num9 - 1, num10].Amount > 0 && !(LiquidWorld.grid[num9 - 1, num10].LiquidType is Lava))
                        {
                            if (isLava)
                                LavaCheck(num9, num10);
                            else
                                LavaCheck(num9 - 1, num10);
                        }
                        else if (LiquidWorld.grid[num9 + 1, num10].Amount > 0 && !(LiquidWorld.grid[num9 + 1, num10].LiquidType is Lava))
                        {
                            if (isLava)
                                LavaCheck(num9, num10);
                            else
                                LavaCheck(num9 + 1, num10);
                        }
                        else if (LiquidWorld.grid[num9, num10 - 1].Amount > 0 && !(LiquidWorld.grid[num9, num10 - 1].LiquidType is Lava))
                        {
                            if (isLava)
                                LavaCheck(num9, num10);
                            else
                                LavaCheck(num9, num10 - 1);
                        }
                        else if (LiquidWorld.grid[num9, num10 + 1].Amount > 0 && !(LiquidWorld.grid[num9, num10 + 1].LiquidType is Lava))
                        {
                            if (isLava)
                                LavaCheck(num9, num10);
                            else
                                LavaCheck(num9, num10 + 1);
                        }

                        if (LiquidWorld.grid[num9, num10].Amount <= 0)
                            continue;

                        if (LiquidWorld.grid[num9 - 1, num10].Amount > 0 && !(LiquidWorld.grid[num9 - 1, num10].LiquidType is Honey))
                        {
                            if (isHoney)
                                HoneyCheck(num9, num10);
                            else
                                HoneyCheck(num9 - 1, num10);
                        }
                        else if (LiquidWorld.grid[num9 + 1, num10].Amount > 0 && !(LiquidWorld.grid[num9 + 1, num10].LiquidType is Honey))
                        {
                            if (isHoney)
                                HoneyCheck(num9, num10);
                            else
                                HoneyCheck(num9 + 1, num10);
                        }
                        else if (LiquidWorld.grid[num9, num10 - 1].Amount > 0 && !(LiquidWorld.grid[num9, num10 - 1].LiquidType is Honey))
                        {
                            if (isHoney)
                                HoneyCheck(num9, num10);
                            else
                                HoneyCheck(num9, num10 - 1);
                        }
                        else if (LiquidWorld.grid[num9, num10 + 1].Amount > 0 && !(LiquidWorld.grid[num9, num10 + 1].LiquidType is Honey))
                        {
                            if (isHoney)
                                HoneyCheck(num9, num10);
                            else
                                HoneyCheck(num9, num10 + 1);
                        }
                    }
                }
            }

            return num;
        }

        public static void UpdateLiquid(On.Terraria.Liquid.orig_UpdateLiquid orig)
        {
            int wetCounter = (int)ReflectionCaches.fieldCache[typeof(Liquid)]["wetCounter"].GetValue(null);

            if (!WorldGen.gen)
            {
                if (!panicMode)
                {
                    if (numLiquid + LiquidBuffer.numLiquidBuffer > 4000)
                    {
                        panicCounter++;
                        if (panicCounter > 1800 || numLiquid + LiquidBuffer.numLiquidBuffer > 13500)
                            StartPanic();
                    }
                    else
                    {
                        panicCounter = 0;
                    }
                }
                //This is what is ruining the entire settling code
                if (panicMode)
                {
                    int num = 0;
                    while (panicY >= 3 && num < 5)
                    {
                        num++;
                        Liquid.QuickWater(0, panicY, panicY);
                        panicY--;
                        if (panicY >= 3)
                            continue;

                        Console.WriteLine(Language.GetTextValue("Misc.WaterSettled"));
                        panicCounter = 0;
                        panicMode = false;
                        WorldGen.WaterCheck();
                        if (Main.netMode != 2)
                            continue;

                        for (int i = 0; i < 255; i++)
                        {
                            for (int j = 0; j < Main.maxSectionsX; j++)
                            {
                                for (int k = 0; k < Main.maxSectionsY; k++)
                                {
                                    Netplay.Clients[i].TileSections[j, k] = false;
                                }
                            }
                        }
                    }

                    return;
                }
            }

            if (quickSettle || numLiquid > 2000)
                quickFall = true;
            else
                quickFall = false;

            wetCounter++;
            int num2 = maxLiquid / cycles;
            int num3 = num2 * (wetCounter - 1);
            int num4 = num2 * wetCounter;
            if (wetCounter == cycles)
                num4 = numLiquid;

            if (num4 > numLiquid)
            {
                num4 = numLiquid;
                _ = Main.netMode;
                wetCounter = cycles;
            }

            if (quickFall)
            {
                for (int l = num3; l < num4; l++)
                {
                    Main.liquid[l].delay = 10;
                    Main.liquid[l].Update();
                    Main.tile[Main.liquid[l].x, Main.liquid[l].y].skipLiquid(skipLiquid: false);
                }
            }
            else
            {
                for (int m = num3; m < num4; m++)
                {
                    if (!Main.tile[Main.liquid[m].x, Main.liquid[m].y].skipLiquid())
                        Main.liquid[m].Update();
                    else
                        Main.tile[Main.liquid[m].x, Main.liquid[m].y].skipLiquid(skipLiquid: false);
                }
            }

            if (wetCounter >= cycles)
            {
                wetCounter = 0;
                for (int num5 = numLiquid - 1; num5 >= 0; num5--)
                {
                    if (Main.liquid[num5].kill > 4)
                        DelWater(num5);
                }

                int num6 = maxLiquid - (maxLiquid - numLiquid);
                if (num6 > LiquidBuffer.numLiquidBuffer)
                    num6 = LiquidBuffer.numLiquidBuffer;

                for (int n = 0; n < num6; n++)
                {
                    Main.tile[Main.liquidBuffer[0].x, Main.liquidBuffer[0].y].checkingLiquid(checkingLiquid: false);
                    Liquid.AddWater(Main.liquidBuffer[0].x, Main.liquidBuffer[0].y);
                    LiquidBuffer.DelBuffer(0);
                }

                if (numLiquid > 0 && numLiquid > stuckAmount - 50 && numLiquid < stuckAmount + 50)
                {
                    stuckCount++;
                    if (stuckCount >= 10000)
                    {
                        stuck = true;
                        for (int num7 = numLiquid - 1; num7 >= 0; num7--)
                        {
                            DelWater(num7);
                        }

                        stuck = false;
                        stuckCount = 0;
                    }
                }
                else
                {
                    stuckCount = 0;
                    stuckAmount = numLiquid;
                }
            }

            //if (!WorldGen.gen && Main.netMode == 2 && _netChangeSet.Count > 0) {
            //Utils.Swap(ref _netChangeSet, ref _swapNetChangeSet);
            //NetManager.Instance.Broadcast(NetLiquidModule.Serialize(_swapNetChangeSet));
            //_swapNetChangeSet.Clear();
            //}

        }

    }
}