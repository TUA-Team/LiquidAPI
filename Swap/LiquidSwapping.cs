using System;
using LiquidAPI.LiquidMod;
using Terraria;
using Terraria.ObjectData;

namespace LiquidAPI.Swap
{
    class LiquidSwapping
    {
        public static void MethodSwap()
        {
            Type liquid = typeof(Liquid);
            Type thisObject = typeof(LiquidSwapping);

            //ReflectionUtils.MethodSwap(liquid, "AddWater", thisObject, "AddWater");
        }

        /*public static void AddWater(int x, int y)
        {
            Tile Tile = Main.Tile[x, y];
            if (Tile == null)
            {
                return;
            }
            LiquidRef liquid = new LiquidRef(x, y);
            if (Main.Tile[x, y] == null)
            {
                return;
            }

            if (Tile.checkingLiquid())
            {
                return;
            }

            for (byte b = 0; b < LiquidRegistry.liquidList.Count + 1; b = (byte) (b + 1))
            {
                if (liquid.NoLiquid())
                {
                    return;
                }
            }

            if (x >= Main.maxTilesX - 5 || y >= Main.maxTilesY - 5)
            {
                return;
            }

            if (x < 5 || y < 5)
            {
                return;
            }

            if (Tile.liquid == 0)
            {
                return;
            }

            if (Liquid.numLiquid >= Liquid.maxLiquid - 1)
            {
                LiquidBuffer.AddBuffer(x, y);
                return;
            }

            Tile.checkingLiquid(true);
            Main.liquid[Liquid.numLiquid].kill = 0;
            Main.liquid[Liquid.numLiquid].x = x;
            Main.liquid[Liquid.numLiquid].y = y;
            Main.liquid[Liquid.numLiquid].delay = 0;
            Tile.skipLiquid(false);
            Liquid.numLiquid++;
            if (Main.netMode == 2)
            {
                Liquid.NetSendLiquid(x, y);
            }

            if (Tile.active() && !WorldGen.gen)
            {
                bool flag = false;
                if (liquid.Liquids(1))
                {
                    if (TileObjectData.CheckLavaDeath(Tile))
                    {
                        flag = true;
                    }
                }
                else if (TileObjectData.CheckWaterDeath(Tile))
                {
                    flag = true;
                }

                if (flag)
                {
                    WorldGen.KillTile(x, y, false, false, false);
                    if (Main.netMode == 2)
                    {
                        NetMessage.SendData(17, -1, -1, null, 0, (float) x, (float) y, 0f, 0, 0, 0);
                    }
                }
            }
        }*/
    }
}


