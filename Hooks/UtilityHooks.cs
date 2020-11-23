using Terraria;
using Terraria.ObjectData;
using Terraria.ID;

namespace LiquidAPI.Hooks
{
    internal sealed class UtilityHooks : ILiquidHook
    {
        public void Init()
        {
            On.Terraria.Liquid.AddWater += AddWater;
        }

        private static void AddWater(On.Terraria.Liquid.orig_AddWater orig, int x, int y)
		{
			Tile tile = Main.tile[x, y];

			// Return if one of the following is true
			// If the tile doesn't exist at all
			if (Main.tile[x, y] == null
                || tile.checkingLiquid()
                || x >= Main.maxTilesX - 5
                || y >= Main.maxTilesY - 5
                || x < 5
                || y < 5
                || tile.liquid == 0
                || (tile.nactive() && Main.tileSolid[tile.type] && tile.type != 546 && !Main.tileSolidTop[tile.type]))
				return;

			if (Liquid.numLiquid >= Liquid.curMaxLiquid - 1)
			{
				LiquidBuffer.AddBuffer(x, y);
				return;
			}

			tile.checkingLiquid(checkingLiquid: true);
			tile.skipLiquid(skipLiquid: false);
			Main.liquid[Liquid.numLiquid].kill = 0;
			Main.liquid[Liquid.numLiquid].x = x;
			Main.liquid[Liquid.numLiquid].y = y;
			Main.liquid[Liquid.numLiquid].delay = 0;
			Liquid.numLiquid++;

			if (Main.netMode == NetmodeID.Server)
			{
				Liquid.NetSendLiquid(x, y);
			}
			if (!tile.active() || WorldGen.gen)
			{
				return;
			}
			bool flag = false;
			if (tile.lava())
			{
				if (TileObjectData.CheckLavaDeath(tile))
				{
					flag = true;
				}
			}
			else if (TileObjectData.CheckWaterDeath(tile))
			{
				flag = true;
			}
			if (flag)
			{
				WorldGen.KillTile(x, y);
				if (Main.netMode == NetmodeID.Server)
				{
					NetMessage.SendData(MessageID.TileManipulation, -1, -1, null, 0, x, y);
				}
			}
		}
	}
}
