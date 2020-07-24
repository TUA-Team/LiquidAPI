using LiquidAPI.LiquidMod;
using Terraria;
using Terraria.ID;

namespace LiquidAPI.Hooks
{
    internal static partial class LiquidHooks
    {
        public static void AddWater(On.Terraria.Liquid.orig_AddWater org, int x, int y)
        {
            Tile tile = Main.tile[x, y];
            if (!WorldGen.InWorld(x, y, 5) || tile == null || tile.checkingLiquid() || tile.liquid == 0)
            {
                return;
            }

            if (Liquid.numLiquid >= Liquid.maxLiquid - 1)
            {
                LiquidBuffer.AddBuffer(x, y);
                return;
            }

            tile.checkingLiquid(true);
            tile.skipLiquid(false);
            Liquid liquid = Main.liquid[Liquid.numLiquid++];
            liquid.kill = 0;
            liquid.x = x;
            liquid.y = y;
            liquid.delay = 0;
            if (Main.netMode == NetmodeID.Server)
            {
                Liquid.NetSendLiquid(x, y);
            }

            if (!tile.active() || WorldGen.gen)
            {
                return;
            }

            if (LiquidRegistry.liquidList[LiquidWorld.liquidGrid[x, y].data].CanKillTile(x, y))
            {
                WorldGen.KillTile(x, y);
                if (Main.netMode == NetmodeID.Server)
                {
                    NetMessage.SendData(MessageID.TileChange, -1, -1, null, 0, x, y);
                }
            }
        }
    }
}