using LiquidAPI.ID;
using LiquidAPI.LiquidMod;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;

namespace LiquidAPI.Hooks
{
	internal static partial class LiquidHooks
	{

		private static void LiquidOnLavaCheck(On.Terraria.Liquid.orig_LavaCheck orig, int x, int y)
		{
			LiquidRef liquidLeft = LiquidCore.grid[x - 1, y];
			LiquidRef liquidRight = LiquidCore.grid[x + 1, y];
			LiquidRef liquidDown = LiquidCore.grid[x, y - 1];
			LiquidRef liquidUp = LiquidCore.grid[x, y + 1];
			LiquidRef liquidSelf = LiquidCore.grid[x, y];

			if (liquidLeft.Amount > 0 && liquidLeft.Type != LiquidID.lava || liquidRight.Amount > 0 && liquidRight.Type != LiquidID.lava || liquidDown.Amount > 0 && liquidDown.Type != LiquidID.lava)
			{
				int num = 0;
				int type = TileID.Obsidian;

				if (liquidLeft.Type != LiquidID.lava)
				{
					num += liquidLeft.Amount;
					liquidLeft.Amount = 0;
				}

				if (liquidRight.Type != LiquidID.lava)
				{
					num += liquidRight.Amount;
					liquidRight.Amount = 0;
				}

				if (liquidDown.Type != LiquidID.lava)
				{
					num += liquidDown.Amount;
					liquidDown.Amount = 0;
				}

				if (liquidLeft.Type == LiquidID.honey || liquidRight.Type == LiquidID.honey || liquidDown.Type == LiquidID.honey)
					type = TileID.CrispyHoneyBlock;

				if (num < 24)
					return;
				if (liquidSelf.Tile.active() && Main.tileObsidianKill[liquidSelf.Tile.type])
				{
					WorldGen.KillTile(x, y);
					if (Main.netMode == NetmodeID.Server)
						NetMessage.SendData(MessageID.TileChange, -1, -1, null, 0, x, y);
				}

				if (liquidSelf.Tile.active())
					return;

				liquidSelf.Amount = 0;
				liquidSelf.Type = LiquidID.water;
				//liquidSelf.lava(false);

				Main.PlaySound( type == TileID.Obsidian ? SoundID.LiquidsWaterLava : SoundID.LiquidsHoneyLava, new Vector2(x * 16 + 8, y * 16 + 8));

				WorldGen.PlaceTile(x, y, type, true, true);
				WorldGen.SquareTileFrame(x, y);

				if (Main.netMode != NetmodeID.Server)
					return;

				NetMessage.SendTileSquare(-1, x - 1, y - 1, 3,
					type == TileID.Obsidian ? TileChangeType.LavaWater : TileChangeType.HoneyLava);
			}
			else
			{
				if (liquidUp.Amount <= 0 || liquidUp.Type == LiquidID.lava)
					return;

				bool flag = liquidSelf.Tile.active() && TileID.Sets.ForceObsidianKill[liquidSelf.Tile.type] &&
							!TileID.Sets.ForceObsidianKill[liquidUp.Tile.type];

				if (Main.tileCut[liquidUp.Tile.type])
				{
					WorldGen.KillTile(x, y + 1);

					if (Main.netMode == NetmodeID.Server)
						NetMessage.SendData(MessageID.TileChange, -1, -1, null, 0, x, y + 1);
				}
				else if (liquidUp.Tile.active() && Main.tileObsidianKill[liquidUp.Tile.type])
				{
					WorldGen.KillTile(x, y + 1);

					if (Main.netMode == NetmodeID.Server)
						NetMessage.SendData(MessageID.TileChange, -1, -1, null, 0, x, y + 1);
				}

				if (!(!liquidUp.Tile.active() | flag))
					return;

				if (liquidSelf.Amount < 24)
				{
					liquidSelf.Amount = 0;
					liquidSelf.Type = LiquidID.water;

					if (Main.netMode != NetmodeID.Server)
						return;

					NetMessage.SendTileSquare(-1, x - 1, y, 3, TileChangeType.None);
				}
				else
				{
					int type = TileID.Obsidian;

					if (liquidUp.Type == LiquidID.honey)
						type = TileID.CrispyHoneyBlock;

					liquidSelf.Amount = 0;
					liquidSelf.Type = LiquidID.water;

					//liquidSelf.lava(false);
					liquidUp.Amount = 0;

					if (type == TileID.Obsidian)
						Main.PlaySound(type == TileID.Obsidian ? SoundID.LiquidsWaterLava : SoundID.LiquidsHoneyLava, new Vector2(x * 16 + 8, y * 16 + 8));

					WorldGen.PlaceTile(x, y + 1, type, true, true);
					WorldGen.SquareTileFrame(x, y + 1);

					if (Main.netMode != NetmodeID.Server)
						return;

					NetMessage.SendTileSquare(-1, x - 1, y, 3, type == TileID.Obsidian ? TileChangeType.LavaWater : TileChangeType.HoneyLava);
				}
			}
		}

		private static void LiquidOnHoneyCheck(On.Terraria.Liquid.orig_HoneyCheck orig, int x, int y)
		{
			LiquidRef liquidLeft = LiquidCore.grid[x - 1, y];
			LiquidRef liquidRight = LiquidCore.grid[x + 1, y];
			LiquidRef liquidDown = LiquidCore.grid[x, y - 1];
			LiquidRef liquidUp = LiquidCore.grid[x, y + 1];
			LiquidRef liquidSelf = LiquidCore.grid[x, y];

			bool flag = false;

			if (liquidLeft.Amount > 0 && liquidLeft.Type == LiquidID.water || liquidRight.Amount > 0 && liquidRight.Type == LiquidID.water || liquidDown.Amount > 0 && liquidDown.Type == LiquidID.water)
			{
				int num = 0;

				if (liquidLeft.Type == LiquidID.water)
				{
					num += liquidLeft.Amount;
					liquidLeft.Amount = 0;
				}

				if (liquidRight.Type == LiquidID.water)
				{
					num += liquidRight.Amount;
					liquidRight.Amount = 0;
				}

				if (liquidDown.Type == LiquidID.water)
				{
					num += liquidDown.Amount;
					liquidDown.Amount = 0;
				}

				if (liquidLeft.Type == LiquidID.lava || liquidRight.Type == LiquidID.lava || liquidDown.Type == LiquidID.lava)
					flag = true;

				if (num < 32)
					return;

				if (liquidSelf.Tile.active() && Main.tileObsidianKill[liquidSelf.Tile.type])
				{
					WorldGen.KillTile(x, y);

					if (Main.netMode == NetmodeID.Server)
						NetMessage.SendData(MessageID.TileChange, -1, -1, null, 0, x, y);
				}

				if (liquidSelf.Tile.active())
					return;

				liquidSelf.Amount = 0;
				liquidSelf.Type = LiquidID.water;

				WorldGen.PlaceTile(x, y, TileID.HoneyBlock, true, true);

				Main.PlaySound(flag?SoundID.LiquidsHoneyLava:SoundID.LiquidsHoneyWater, new Vector2(x * 16 + 8, y * 16 + 8));
				
				WorldGen.SquareTileFrame(x, y);

				if (Main.netMode != NetmodeID.Server)
					return;

				NetMessage.SendTileSquare(-1, x - 1, y - 1, 3, flag ? TileChangeType.HoneyLava : TileChangeType.HoneyWater);
			}
			else
			{
				if (liquidUp.Amount <= 0 || liquidUp.Type != LiquidID.water)
					return;

				if (Main.tileCut[liquidUp.Tile.type])
				{
					WorldGen.KillTile(x, y + 1);

					if (Main.netMode == NetmodeID.Server)
						NetMessage.SendData(MessageID.TileChange, -1, -1, null, 0, x, y + 1);
				}
				else if (liquidUp.Tile.active() && Main.tileObsidianKill[liquidUp.Tile.type])
				{
					WorldGen.KillTile(x, y + 1);
					if (Main.netMode == NetmodeID.Server)
						NetMessage.SendData(MessageID.TileChange, -1, -1, null, 0, x, y + 1);
				}

				if (liquidUp.Tile.active())
					return;

				if (liquidSelf.Amount < 32)
				{
					liquidSelf.Amount = 0;
					liquidSelf.Type = LiquidID.water;

					if (Main.netMode != NetmodeID.Server)
						return;

					NetMessage.SendTileSquare(-1, x - 1, y, 3, TileChangeType.None);
				}
				else
				{
					if (liquidUp.Type == LiquidID.lava)
					{
						flag = true;
					}

					liquidSelf.Amount = 0;
					liquidSelf.Type = LiquidID.water;
					liquidUp.Amount = 0;
					liquidUp.Type = LiquidID.water;

					Main.PlaySound(flag?SoundID.LiquidsHoneyLava:SoundID.LiquidsHoneyWater, new Vector2(x * 16 + 8,y * 16 + 8));

					WorldGen.PlaceTile(x, y + 1, TileID.HoneyBlock, true, true);
					WorldGen.SquareTileFrame(x, y + 1);

					if (Main.netMode != NetmodeID.Server)
						return;

					NetMessage.SendTileSquare(-1, x - 1, y, 3, flag ? TileChangeType.HoneyLava : TileChangeType.HoneyWater);
				}
			}
		}
	}
}
