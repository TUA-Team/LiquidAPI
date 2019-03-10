using System;
using LiquidAPI.LiquidMod;
using LiquidAPI.Swap;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using LiquidAPI.Swap;

namespace LiquidAPI
{
    internal static class LiquidExtension
    {

        public static void MethodSwap()
        {
            On.Terraria.Liquid.Update += ModdedLiquidUpdate;
            On.Terraria.Liquid.AddWater += AddWater;
        }

        /*public static void UpdateLiquid(On.Terraria.Liquid.orig_Update orig)
        {
            FieldInfo info = typeof(Liquid).GetField("wetCounter", BindingFlags.Static | BindingFlags.NonPublic);
            int wetCounter = (int)info.GetValue(null);
            int arg_07_0 = Main.netMode;
            if (!WorldGen.gen)
            {
                if (!Liquid.panicMode)
                {
                    if (Liquid.numLiquid + LiquidBuffer.numLiquidBuffer > 4000)
                    {
                        Liquid.panicCounter++;
                        if (Liquid.panicCounter > 1800 || Liquid.numLiquid + LiquidBuffer.numLiquidBuffer > 13500)
                        {
                            Liquid.StartPanic();
                        }
                    }
                    else
                    {
                        Liquid.panicCounter = 0;
                    }
                }
                if (Liquid.panicMode)
                {
                    int num = 0;
                    while (Liquid.panicY >= 3 && num < 5)
                    {
                        num++;
                        QuickWater(0, Liquid.panicY, Liquid.panicY);
                        Liquid.panicY--;
                        if (Liquid.panicY < 3)
                        {
                            Console.WriteLine(Language.GetTextValue("Misc.WaterSettled"));
                            Liquid.panicCounter = 0;
                            Liquid.panicMode = false;
                            WorldGen.WaterCheck();
                            if (Main.netMode == 2)
                            {
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
                        }
                    }
                    return;
                }
            }
            if (Liquid.quickSettle || Liquid.numLiquid > 2000)
            {
                Liquid.quickFall = true;
            }
            else
            {
                Liquid.quickFall = false;
            }
            wetCounter++;
            int num2 = Liquid.maxLiquid / Liquid.cycles;
            int num3 = num2 * (wetCounter - 1);
            int num4 = num2 * wetCounter;
            if (wetCounter == Liquid.cycles)
            {
                num4 = Liquid.numLiquid;
            }
            if (num4 > Liquid.numLiquid)
            {
                num4 = Liquid.numLiquid;
                int arg_190_0 = Main.netMode;
                wetCounter = Liquid.cycles;
            }
            if (Liquid.quickFall)
            {
                for (int l = num3; l < num4; l++)
                {
                    Main.liquid[l].delay = 10;
                    Main.liquid[l].ModdedLiquidUpdate();
                    Main.tile[Main.liquid[l].x, Main.liquid[l].y].skipLiquid(false);
                }
            }
            else
            {
                for (int m = num3; m < num4; m++)
                {
                    if (!Main.tile[Main.liquid[m].x, Main.liquid[m].y].skipLiquid())
                    {
                        Main.liquid[m].ModdedLiquidUpdate();
                    }
                    else
                    {
                        Main.tile[Main.liquid[m].x, Main.liquid[m].y].skipLiquid(false);
                    }
                }
            }
            if (wetCounter >= Liquid.cycles)
            {
                wetCounter = 0;
                for (int n = Liquid.numLiquid - 1; n >= 0; n--)
                {
                    if (Main.liquid[n].kill > 4)
                    {
                        Liquid.DelWater(n);
                    }
                }
                int num5 = Liquid.maxLiquid - (Liquid.maxLiquid - Liquid.numLiquid);
                if (num5 > LiquidBuffer.numLiquidBuffer)
                {
                    num5 = LiquidBuffer.numLiquidBuffer;
                }
                for (int num6 = 0; num6 < num5; num6++)
                {
                    Main.tile[Main.liquidBuffer[0].x, Main.liquidBuffer[0].y].checkingLiquid(false);
                    AddModdedLiquidAround(Main.liquidBuffer[0].x, Main.liquidBuffer[0].y);
                    LiquidBuffer.DelBuffer(0);
                }
                if (Liquid.numLiquid > 0 && Liquid.numLiquid > Liquid.stuckAmount - 50 && Liquid.numLiquid < Liquid.stuckAmount + 50)
                {
                    Liquid.stuckCount++;
                    if (Liquid.stuckCount >= 10000)
                    {
                        Liquid.stuck = true;
                        for (int num7 = Liquid.numLiquid - 1; num7 >= 0; num7--)
                        {
                            Liquid.DelWater(num7);
                        }
                        Liquid.stuck = false;
                        Liquid.stuckCount = 0;
                    }
                }
                else
                {
                    Liquid.stuckCount = 0;
                    Liquid.stuckAmount = Liquid.numLiquid;
                }
            }
            
            if (!WorldGen.gen && Main.netMode == 2 && Liquid._netChangeSet.Count > 0)
            {
                Utils.Swap<HashSet<int>>(ref Liquid._netChangeSet, ref Liquid._swapNetChangeSet);
                NetManager.Instance.Broadcast(NetLiquidModule.Serialize(Liquid._swapNetChangeSet), -1);
                Liquid._swapNetChangeSet.Clear();
            }
        }*/

        /// <summary>
        /// tile.nactive() mean check if the tile is actuated
        /// </summary>
        /// <param name="self"></param>

        public static void ModdedLiquidUpdate(On.Terraria.Liquid.orig_Update orig, Liquid self)
        {
            if (self.x < 5 || self.x > Main.maxTilesX - 5 || self.y < 5 || self.y > Main.maxTilesY - 5)
            {
                return;
            }

            Main.tileSolid[TileID.Bubble] = true; //bubble

            LiquidRef liquidLeft = LiquidCore.grid[self.x - 1, self.y];
            LiquidRef liquidRight = LiquidCore.grid[self.x + 1, self.y];
            LiquidRef liquidUp = LiquidCore.grid[self.x, self.y - 1];
            LiquidRef liquidDown = LiquidCore.grid[self.x, self.y + 1];
            LiquidRef liquidSelf = LiquidCore.grid[self.x, self.y];

            byte liquidTypeSelf = LiquidCore.liquidGrid[self.x, self.y].data;
            byte liquidTypeLeft = LiquidCore.liquidGrid[self.x - 1, self.y].data;
            byte liquidTypeRight = LiquidCore.liquidGrid[self.x + 1, self.y].data;
            byte b = LiquidCore.liquidGrid[self.x, self.y].data;
            if (b == 3)
            {
                Main.NewText("Waste found");
            }

            for (byte i = 0; i < 255; i++)
            {
                if (liquidSelf.Liquids(i))
                {
                    if (i >= 2)
                    {
                    }

                    liquidTypeSelf = i;
                    break;
                }
            }



            for (byte i = 0; i < 255; i++)
            {
                if (liquidLeft.Liquids(i))
                {
                    liquidTypeLeft = i;
                    break;
                }
            }

            for (byte i = 0; i < 255; i++)
            {
                if (liquidRight.Liquids(i))
                {
                    liquidTypeRight = i;
                    break;
                }
            }

            if (liquidSelf.tile.nactive() && Main.tileSolid[(int)liquidSelf.tile.type] &&
                !Main.tileSolidTop[(int)liquidSelf.tile.type])
            {

                self.kill = 9;
            }
            else
            {
                byte liquidAmount = liquidSelf.tile.liquid;
                //liquidSelf.liquidAmount = 0;

                if (liquidSelf.tile.liquid <= (byte)0)
                {
                    self.kill = 9;
                }
                else
                {
                    if (liquidTypeSelf == 0)
                    {
                        if (!Liquid.quickFall)
                        {
                            if (self.delay < 3)
                            {
                                ++self.delay;
                                return;
                            }

                            self.delay = 0;
                            if (liquidLeft.Liquids(0))
                            {
                                Liquid.AddWater(self.x - 1, self.y);
                            }

                            if (liquidRight.Liquids(0))
                            {
                                Liquid.AddWater(self.x + 1, self.y);
                            }

                            if (liquidUp.Liquids(0))
                            {
                                Liquid.AddWater(self.x, self.y - 1);
                            }

                            if (liquidDown.Liquids(0))
                            {
                                Liquid.AddWater(self.x, self.y + 1);
                            }
                        }
                    }
                    else if (liquidTypeSelf == 1)
                    {
                        LiquidCheck.LavaCheck(self.x, self.y);
                        if (!Liquid.quickFall)
                        {
                            if (self.delay < 5)
                            {
                                ++self.delay;
                                return;
                            }

                            self.delay = 0;
                            if (liquidSelf.Liquids(1))
                            {
                                Liquid.AddWater(self.x - 1, self.y);
                            }

                            if (liquidSelf.Liquids(1))
                            {
                                Liquid.AddWater(self.x + 1, self.y);
                            }

                            if (liquidSelf.Liquids(1))
                            {
                                Liquid.AddWater(self.x, self.y - 1);
                            }

                            if (liquidSelf.Liquids(1))
                            {
                                Liquid.AddWater(self.x, self.y + 1);
                            }
                        }
                    }
                    else if (liquidTypeSelf == 2)
                    {
                        LiquidCheck.HoneyCheck(self.x, self.y);
                        if (!Liquid.quickFall)
                        {
                            if (self.delay < 10)
                            {
                                ++self.delay;
                                return;
                            }

                            self.delay = 0;
                            if (liquidLeft.Liquids(2))
                            {
                                Liquid.AddWater(self.x - 1, self.y);
                            }

                            if (liquidRight.Liquids(2))
                            {
                                Liquid.AddWater(self.x + 1, self.y);
                            }

                            if (liquidUp.Liquids(2))
                            {
                                Liquid.AddWater(self.x, self.y - 1);
                            }

                            if (liquidDown.Liquids(2))
                            {
                                Liquid.AddWater(self.x, self.y + 1);
                            }
                        }
                    }
                    else if (!LiquidRegistry.RunUpdate(liquidTypeSelf, self.x, self.y))
                    {
                        return;
                    }


                    if ((!liquidDown.tile.nactive() || !Main.tileSolid[(int)liquidDown.tile.type] ||
                         Main.tileSolidTop[(int)liquidDown.tile.type]) &&
                        ((liquidDown.tile.liquid <= (byte)0 ||
                          liquidDown.Liquids(liquidTypeSelf)) &&
                         liquidDown.tile.liquid < byte.MaxValue))
                    {
                        float num = (float)((int)byte.MaxValue - (int)liquidDown.tile.liquid);
                        if ((double)num > (double)liquidSelf.tile.liquid)
                        {
                            num = (float)liquidSelf.tile.liquid;
                        }

                        liquidSelf.tile.liquid -= (byte)num;
                        liquidDown.tile.liquid += (byte)num;
                        liquidDown.tile.liquid = liquidSelf.tile.liquid;
                        Liquid.AddWater(self.x, self.y + 1);
                        liquidDown.tile.skipLiquid(true);
                        liquidSelf.tile.skipLiquid(true);
                        if (liquidSelf.tile.liquid > (byte)250)
                        {
                            liquidSelf.tile.liquid = byte.MaxValue;
                        }
                        else
                        {
                            Liquid.AddWater(self.x - 1, self.y);
                            Liquid.AddWater(self.x + 1, self.y);
                        }
                    }

                    if (liquidSelf.tile.liquid > (byte)0)
                    {
                        bool flag = true;
                        bool flag2 = true;
                        bool flag3 = true;
                        bool flag4 = true;
                        if (liquidLeft.tile.nactive() && Main.tileSolid[(int)liquidLeft.tile.type] &&
                            !Main.tileSolidTop[(int)liquidLeft.tile.type])
                        {
                            flag = false;
                        }
                        else if (liquidLeft.tile.liquid > 0 &&
                                 liquidLeft.Liquids(liquidTypeSelf))
                        {
                            flag = false;
                        }
                        else if (LiquidCore.grid[self.x - 2, self.y].tile.nactive() &&
                                 Main.tileSolid[(int)LiquidCore.grid[self.x - 2, self.y].tile.type] &&
                                 !Main.tileSolidTop[(int)LiquidCore.grid[self.x - 2, self.y].tile.type])
                        {
                            flag3 = false;
                        }
                        else if (LiquidCore.grid[self.x - 2, self.y].tile.liquid == 0)
                        {
                            flag3 = false;
                        }
                        else if (LiquidCore.grid[self.x - 2, self.y].tile.liquid > 0 &&
                                 LiquidCore.grid[self.x - 2, self.y].Liquids(liquidTypeLeft))
                        {
                            flag3 = false;
                        }

                        if (liquidRight.tile.nactive() && Main.tileSolid[(int)liquidRight.tile.type] &&
                            !Main.tileSolidTop[(int)liquidRight.tile.type])
                        {
                            flag2 = false;
                        }
                        else if (liquidRight.tile.liquid > 0 && !liquidRight.Liquids(liquidTypeSelf))
                        {
                            flag2 = false;
                        }
                        else if (LiquidCore.grid[self.x + 2, self.y].tile.nactive() &&
                                 Main.tileSolid[(int)LiquidCore.grid[self.x + 2, self.y].tile.type] &&
                                 !Main.tileSolidTop[(int)LiquidCore.grid[self.x + 2, self.y].tile.type])
                        {
                            flag4 = false;
                        }
                        else if (LiquidCore.grid[self.x + 2, self.y].tile.liquid == 0)
                        {
                            flag4 = false;
                        }
                        else if (LiquidCore.grid[self.x + 2, self.y].tile.liquid > 0 &&
                                 !LiquidCore.grid[self.x + 2, self.y].Liquids(liquidTypeSelf))
                        {
                            flag4 = false;
                        }

                        int num2 = 0;
                        if (liquidSelf.tile.liquid < 3)
                        {
                            num2 = -1;
                        }

                        if (flag && flag2)
                        {
                            if (flag3 && flag4)
                            {
                                bool flag5 = true;
                                bool flag6 = true;
                                if (LiquidCore.grid[self.x - 3, self.y].tile.nactive() &&
                                    Main.tileSolid[(int)LiquidCore.grid[self.x - 3, self.y].tile.type] &&
                                    !Main.tileSolidTop[(int)LiquidCore.grid[self.x - 3, self.y].tile.type])
                                {
                                    flag5 = false;
                                }
                                else if (LiquidCore.grid[self.x - 3, self.y].tile.liquid == 0)
                                {
                                    flag5 = false;
                                }
                                else if (LiquidCore.grid[self.x - 3, self.y].Liquids(liquidTypeSelf))
                                {
                                    flag5 = false;
                                }

                                if (LiquidCore.grid[self.x + 3, self.y].tile.nactive() &&
                                    Main.tileSolid[(int)LiquidCore.grid[self.x + 3, self.y].tile.type] &&
                                    !Main.tileSolidTop[(int)LiquidCore.grid[self.x + 3, self.y].tile.type])
                                {
                                    flag6 = false;
                                }
                                else if (LiquidCore.grid[self.x + 3, self.y].tile.liquid == 0)
                                {
                                    flag6 = false;
                                }
                                else if (LiquidCore.grid[self.x + 3, self.y].Liquids(liquidTypeSelf))
                                {
                                    flag6 = false;
                                }

                                if (flag5 && flag6)
                                {
                                    float num = (float)((int)(liquidLeft.tile.liquid + liquidRight.tile.liquid +
                                                                LiquidCore.grid[self.x - 2, self.y].tile.liquid +
                                                                LiquidCore.grid[self.x + 2, self.y].tile.liquid +
                                                                LiquidCore.grid[self.x - 3, self.y].tile.liquid +
                                                                LiquidCore.grid[self.x + 3, self.y].tile.liquid +
                                                                liquidSelf.tile.liquid) + num2);
                                    num = (float)Math.Round((double)(num / 7f));
                                    int num3 = 0;
                                    liquidLeft.SetLiquidsState(liquidTypeSelf, true);
                                    if (liquidLeft.tile.liquid != (byte)num)
                                    {
                                        liquidLeft.tile.liquid = (byte)num;
                                        Liquid.AddWater(self.x - 1, self.y);
                                    }
                                    else
                                    {
                                        num3++;
                                    }

                                    liquidRight.SetLiquidsState(liquidTypeSelf, true);
                                    if (liquidRight.tile.liquid != (byte)num)
                                    {
                                        liquidRight.tile.liquid = (byte)num;
                                        Liquid.AddWater(self.x + 1, self.y);
                                    }
                                    else
                                    {
                                        num3++;
                                    }

                                    LiquidCore.grid[self.x - 2, self.y].SetLiquidsState(liquidTypeSelf, true);
                                    if (LiquidCore.grid[self.x - 2, self.y].tile.liquid != (byte)num)
                                    {
                                        LiquidCore.grid[self.x - 2, self.y].tile.liquid = (byte)num;
                                        Liquid.AddWater(self.x - 2, self.y);
                                    }
                                    else
                                    {
                                        num3++;
                                    }

                                    LiquidCore.grid[self.x + 2, self.y].SetLiquidsState(liquidTypeSelf, true);
                                    if (LiquidCore.grid[self.x + 2, self.y].tile.liquid != (byte)num)
                                    {
                                        LiquidCore.grid[self.x + 2, self.y].tile.liquid = (byte)num;
                                        Liquid.AddWater(self.x + 2, self.y);
                                    }
                                    else
                                    {
                                        num3++;
                                    }

                                    LiquidCore.grid[self.x - 3, self.y].SetLiquidsState(liquidTypeSelf, true);
                                    if (LiquidCore.grid[self.x - 3, self.y].tile.liquid != (byte)num)
                                    {
                                        LiquidCore.grid[self.x - 3, self.y].tile.liquid = (byte)num;
                                        Liquid.AddWater(self.x - 3, self.y);
                                    }
                                    else
                                    {
                                        num3++;
                                    }

                                    LiquidCore.grid[self.x + 3, self.y].SetLiquidsState(liquidTypeSelf, true);
                                    if (LiquidCore.grid[self.x + 3, self.y].tile.liquid != (byte)num)
                                    {
                                        LiquidCore.grid[self.x + 3, self.y].tile.liquid = (byte)num;
                                        Liquid.AddWater(self.x + 3, self.y);
                                    }
                                    else
                                    {
                                        num3++;
                                    }

                                    if (liquidLeft.tile.liquid != (byte)num ||
                                        liquidSelf.tile.liquid != (byte)num)
                                    {
                                        Liquid.AddWater(self.x - 1, self.y);
                                    }

                                    if (liquidRight.tile.liquid != (byte)num ||
                                        liquidSelf.tile.liquid != (byte)num)
                                    {
                                        Liquid.AddWater(self.x + 1, self.y);
                                    }

                                    if (LiquidCore.grid[self.x - 2, self.y].tile.liquid != (byte)num ||
                                        liquidSelf.tile.liquid != (byte)num)
                                    {
                                        Liquid.AddWater(self.x - 2, self.y);
                                    }

                                    if (LiquidCore.grid[self.x + 2, self.y].tile.liquid != (byte)num ||
                                        liquidSelf.tile.liquid != (byte)num)
                                    {
                                        Liquid.AddWater(self.x + 2, self.y);
                                    }

                                    if (LiquidCore.grid[self.x - 3, self.y].tile.liquid != (byte)num ||
                                        liquidSelf.tile.liquid != (byte)num)
                                    {
                                        Liquid.AddWater(self.x - 3, self.y);
                                    }

                                    if (LiquidCore.grid[self.x + 3, self.y].tile.liquid != (byte)num ||
                                        liquidSelf.tile.liquid != (byte)num)
                                    {
                                        Liquid.AddWater(self.x + 3, self.y);
                                    }

                                    if (num3 != 6 || liquidUp.tile.liquid <= 0)
                                    {
                                        liquidSelf.tile.liquid = (byte)num;
                                    }
                                }
                                else
                                {
                                    int num4 = 0;
                                    float num = (float)((int)(liquidLeft.tile.liquid + liquidRight.tile.liquid +
                                                                LiquidCore.grid[self.x - 2, self.y].tile.liquid +
                                                                LiquidCore.grid[self.x + 2, self.y].tile.liquid +
                                                                liquidSelf.tile.liquid) + num2);
                                    num = (float)Math.Round((double)(num / 5f));
                                    liquidLeft.SetLiquidsState(liquidTypeSelf, true);
                                    if (liquidLeft.tile.liquid != (byte)num)
                                    {
                                        liquidLeft.tile.liquid = (byte)num;
                                        Liquid.AddWater(self.x - 1, self.y);
                                    }
                                    else
                                    {
                                        num4++;
                                    }

                                    liquidRight.SetLiquidsState(liquidTypeSelf, true);
                                    if (liquidRight.tile.liquid != (byte)num)
                                    {
                                        liquidRight.tile.liquid = (byte)num;
                                        Liquid.AddWater(self.x + 1, self.y);
                                    }
                                    else
                                    {
                                        num4++;
                                    }

                                    LiquidCore.grid[self.x - 2, self.y].SetLiquidsState(liquidTypeSelf, true);
                                    if (LiquidCore.grid[self.x - 2, self.y].tile.liquid != (byte)num)
                                    {
                                        LiquidCore.grid[self.x - 2, self.y].tile.liquid = (byte)num;
                                        Liquid.AddWater(self.x - 2, self.y);
                                    }
                                    else
                                    {
                                        num4++;
                                    }

                                    LiquidCore.grid[self.x + 2, self.y].SetLiquidsState(liquidTypeSelf, true);
                                    if (LiquidCore.grid[self.x + 2, self.y].tile.liquid != (byte)num)
                                    {
                                        LiquidCore.grid[self.x + 2, self.y].tile.liquid = (byte)num;
                                        Liquid.AddWater(self.x + 2, self.y);
                                    }
                                    else
                                    {
                                        num4++;
                                    }

                                    if (liquidLeft.tile.liquid != (byte)num ||
                                        liquidSelf.tile.liquid != (byte)num)
                                    {
                                        Liquid.AddWater(self.x - 1, self.y);
                                    }

                                    if (liquidRight.tile.liquid != (byte)num ||
                                        liquidSelf.tile.liquid != (byte)num)
                                    {
                                        Liquid.AddWater(self.x + 1, self.y);
                                    }

                                    if (LiquidCore.grid[self.x - 2, self.y].tile.liquid != (byte)num ||
                                        liquidSelf.tile.liquid != (byte)num)
                                    {
                                        Liquid.AddWater(self.x - 2, self.y);
                                    }

                                    if (LiquidCore.grid[self.x + 2, self.y].tile.liquid != (byte)num ||
                                        liquidSelf.tile.liquid != (byte)num)
                                    {
                                        Liquid.AddWater(self.x + 2, self.y);
                                    }

                                    if (num4 != 4 || liquidUp.tile.liquid <= 0)
                                    {
                                        liquidSelf.tile.liquid = (byte)num;
                                    }
                                }
                            }
                            else if (flag3)
                            {
                                float num = (float)((int)(liquidLeft.tile.liquid + liquidRight.tile.liquid +
                                                            LiquidCore.grid[self.x - 2, self.y].tile.liquid +
                                                            liquidSelf.tile.liquid) + num2);
                                num = (float)Math.Round((double)(num / 4f) + 0.001);
                                liquidLeft.SetLiquidsState(liquidTypeSelf, true);
                                if (liquidLeft.tile.liquid != (byte)num ||
                                    liquidSelf.tile.liquid != (byte)num)
                                {
                                    liquidLeft.tile.liquid = (byte)num;
                                    Liquid.AddWater(self.x - 1, self.y);
                                }

                                liquidRight.SetLiquidsState(liquidTypeSelf, true);
                                if (liquidRight.tile.liquid != (byte)num ||
                                    liquidSelf.tile.liquid != (byte)num)
                                {
                                    liquidRight.tile.liquid = (byte)num;
                                    Liquid.AddWater(self.x + 1, self.y);
                                }

                                LiquidCore.grid[self.x - 2, self.y].SetLiquidsState(liquidTypeSelf, true);
                                if (LiquidCore.grid[self.x - 2, self.y].tile.liquid != (byte)num ||
                                    liquidSelf.tile.liquid != (byte)num)
                                {
                                    LiquidCore.grid[self.x - 2, self.y].tile.liquid = (byte)num;
                                    Liquid.AddWater(self.x - 2, self.y);
                                }

                                liquidSelf.tile.liquid = (byte)num;
                            }
                            else if (flag4)
                            {
                                float num = (float)((int)(liquidLeft.tile.liquid + liquidRight.tile.liquid +
                                                            LiquidCore.grid[self.x + 2, self.y].tile.liquid +
                                                            liquidSelf.tile.liquid) + num2);
                                num = (float)Math.Round((double)(num / 4f) + 0.001);
                                liquidLeft.SetLiquidsState(liquidTypeSelf, true);
                                if (liquidLeft.tile.liquid != (byte)num ||
                                    liquidSelf.tile.liquid != (byte)num)
                                {
                                    liquidLeft.tile.liquid = (byte)num;
                                    Liquid.AddWater(self.x - 1, self.y);
                                }

                                liquidRight.SetLiquidsState(liquidTypeSelf, true);
                                if (liquidRight.tile.liquid != (byte)num ||
                                    liquidSelf.tile.liquid != (byte)num)
                                {
                                    liquidRight.tile.liquid = (byte)num;
                                    Liquid.AddWater(self.x + 1, self.y);
                                }

                                LiquidCore.grid[self.x + 2, self.y].SetLiquidsState(liquidTypeSelf, true);
                                if (LiquidCore.grid[self.x + 2, self.y].tile.liquid != (byte)num ||
                                    liquidSelf.tile.liquid != (byte)num)
                                {
                                    LiquidCore.grid[self.x + 2, self.y].tile.liquid = (byte)num;
                                    Liquid.AddWater(self.x + 2, self.y);
                                }

                                liquidSelf.tile.liquid = (byte)num;
                            }
                            else
                            {
                                float num = (float)((int)(liquidLeft.tile.liquid + liquidRight.tile.liquid +
                                                            liquidSelf.tile.liquid) + num2);
                                num = (float)Math.Round((double)(num / 3f) + 0.001);
                                liquidLeft.SetLiquidsState(liquidTypeSelf, true);
                                if (liquidLeft.tile.liquid != (byte)num)
                                {
                                    liquidLeft.tile.liquid = (byte)num;
                                }

                                if (liquidSelf.tile.liquid != (byte)num ||
                                    liquidLeft.tile.liquid != (byte)num)
                                {
                                    Liquid.AddWater(self.x - 1, self.y);
                                }

                                liquidRight.SetLiquidsState(liquidTypeSelf, true);
                                if (liquidRight.tile.liquid != (byte)num)
                                {
                                    liquidRight.tile.liquid = (byte)num;
                                }

                                if (liquidSelf.tile.liquid != (byte)num ||
                                    liquidRight.tile.liquid != (byte)num)
                                {
                                    Liquid.AddWater(self.x + 1, self.y);
                                }

                                liquidSelf.tile.liquid = (byte)num;
                            }
                        }
                        else if (flag)
                        {
                            float num =
                                (float)((int)(liquidLeft.tile.liquid + liquidSelf.tile.liquid) + num2);
                            num = (float)Math.Round((double)(num / 2f) + 0.001);
                            if (liquidLeft.tile.liquid != (byte)num)
                            {
                                liquidLeft.tile.liquid = (byte)num;
                            }

                            liquidLeft.SetLiquidsState(liquidTypeSelf, true);
                            if (liquidSelf.tile.liquid != (byte)num ||
                                liquidLeft.tile.liquid != (byte)num)
                            {
                                Liquid.AddWater(self.x - 1, self.y);
                            }

                            liquidSelf.tile.liquid = (byte)num;
                        }
                        else if (flag2)
                        {
                            float num =
                                (float)((int)(liquidRight.tile.liquid + liquidSelf.tile.liquid) +
                                         num2);
                            num = (float)Math.Round((double)(num / 2f) + 0.001);
                            if (liquidRight.tile.liquid != (byte)num)
                            {
                                liquidRight.tile.liquid = (byte)num;
                            }

                            liquidRight.SetLiquidsState(liquidTypeSelf, true);
                            if (liquidSelf.tile.liquid != (byte)num ||
                                liquidRight.tile.liquid != (byte)num)
                            {
                                Liquid.AddWater(self.x + 1, self.y);
                            }

                            liquidLeft.tile.liquid = (byte)num;
                        }
                    }

                }

            }
        }



        public static void AddModdedLiquidAround(int x, int y)
        {
            LiquidRef liquid = LiquidCore.grid[x, y];
            if (Main.tile[x, y] == null || liquid.CheckingLiquid() || (x >= Main.maxTilesX - 5 || y >= Main.maxTilesY - 5) || (x < 5 || y < 5 || liquid.NoLiquid()))
            {
                return;
            }

            if (Liquid.numLiquid >= Liquid.maxLiquid - 1)
            {
                LiquidBuffer.AddBuffer(x, y);
            }
            else
            {
                Main.liquid[Liquid.numLiquid].kill = 0;
                Main.liquid[Liquid.numLiquid].x = x;
                Main.liquid[Liquid.numLiquid].y = y;
                Main.liquid[Liquid.numLiquid].delay = 0;

                ++Liquid.numLiquid;
                if (Main.netMode == 2)
                {
                    Liquid.NetSendLiquid(x, y);
                }

                if (liquid.tile.active() || WorldGen.gen)
                {
                    return;
                }

                WorldGen.KillTile(x, y, false, false, false);
                if (Main.netMode != 2)
                {
                    return;
                }

                NetMessage.SendData(17, -1, -1, (NetworkText)null, 0, (float)x, (float)y, 0.0f, 0, 0, 0);
            }
        }

        public static void AddWater(On.Terraria.Liquid.orig_AddWater org, int x, int y)
        {
            if (x >= Main.maxTilesX - 5 || y >= Main.maxTilesY - 5 || x < 5 || y < 5)
            {
                return;
            }
            Tile checkTile;
            try
            {
                checkTile = Main.tile[x, y];
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine(e.StackTrace);
                return;
            }
            if (Main.tile[x, y] == null || checkTile.checkingLiquid() || (x >= Main.maxTilesX - 5 || y >= Main.maxTilesY - 5) || (x < 5 || y < 5 || checkTile.liquid == (byte)0))
            {
                return;
            }

            if (Liquid.numLiquid >= Liquid.maxLiquid - 1)
            {
                LiquidBuffer.AddBuffer(x, y);
                return;
            }
            else
            {
                Main.liquid[Liquid.numLiquid].kill = 0;
                Main.liquid[Liquid.numLiquid].x = x;
                Main.liquid[Liquid.numLiquid].y = y;
                Main.liquid[Liquid.numLiquid].delay = 0;

                ++Liquid.numLiquid;
                if (Main.netMode == 2)
                {
                    Liquid.NetSendLiquid(x, y);
                }

                if (!checkTile.active() || WorldGen.gen)
                {
                    return;
                }

                //WorldGen.KillTile(x, y, false, false, false);
                if (Main.netMode != 2)
                {
                    return;
                }

                NetMessage.SendData(17, -1, -1, (NetworkText)null, 0, (float)x, (float)y, 0.0f, 0, 0, 0);
            }
        }
    }
}
