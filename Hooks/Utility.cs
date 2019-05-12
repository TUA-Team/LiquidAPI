using LiquidAPI.ID;
using LiquidAPI.LiquidMod;
using Terraria;
using Terraria.ID;
using Terraria.ObjectData;

namespace LiquidAPI.Hooks
{
	internal static partial class LiquidHooks
	{
		public static void AddWater(On.Terraria.Liquid.orig_AddWater org, int x, int y)
		{
			LiquidRef liquid = LiquidCore.grid[x, y];
			if (liquid?.Tile == null ||
			    liquid.CheckingLiquid() ||
			    x >= Main.maxTilesX - 5 ||
			    y >= Main.maxTilesY - 5 ||
			    x < 5 || y < 5 ||
			    liquid.Amount == 0)
			{
				return;
			}

			if (Liquid.numLiquid >= Liquid.maxLiquid - 1)
			{
				LiquidBuffer.AddBuffer(x, y);
				return;
			}

			liquid.SetCheckingLiquid(true);
			Main.liquid[Liquid.numLiquid].kill = 0;
			Main.liquid[Liquid.numLiquid].x = x;
			Main.liquid[Liquid.numLiquid].y = y;
			Main.liquid[Liquid.numLiquid].delay = 0;
			liquid.SetSkipLiquid(false);
			Liquid.numLiquid++;
			if (Main.netMode == NetmodeID.Server)
			{
				Liquid.NetSendLiquid(x, y);
			}

			if (!liquid.Tile.active() || WorldGen.gen)
			{
				return;
			}

			bool flag = false;
			if (LiquidCore.liquidGrid[x, y].data == LiquidID.lava)
			{
				if (TileObjectData.CheckLavaDeath(liquid.Tile))
				{
					flag = true;
				}
			}
			else if (TileObjectData.CheckWaterDeath(liquid.Tile))
			{
				flag = true;
			}

			if (flag)
			{
				WorldGen.KillTile(x, y, false, false, false);
				if (Main.netMode == NetmodeID.Server)
				{
					NetMessage.SendData(17, -1, -1, null, 0, (float)x, (float)y, 0f, 0, 0, 0);
				}
			}
		}

		private static void LiquidBufferOnAddBuffer(On.Terraria.LiquidBuffer.orig_AddBuffer orig, int x, int y)
		{
			if (LiquidBuffer.numLiquidBuffer == 9999 || LiquidCore.grid[x, y].CheckingLiquid())
				return;
			LiquidCore.grid[x, y].SetCheckingLiquid(true);
			Main.liquidBuffer[LiquidBuffer.numLiquidBuffer].x = x;
			Main.liquidBuffer[LiquidBuffer.numLiquidBuffer].y = y;
			++LiquidBuffer.numLiquidBuffer;
		}
	}
}
