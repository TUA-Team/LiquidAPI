using LiquidAPI.ID;
using LiquidAPI.LiquidMod;
using LiquidAPI.Vanilla;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;

namespace LiquidAPI.Hooks
{
	internal static partial class LiquidHooks
	{

		private static void LiquidOnLavaCheck(On.Terraria.Liquid.orig_LavaCheck orig, int x, int y)
		{
			LiquidRef liquidLeft = LiquidWorld.grid[x - 1, y];
			LiquidRef liquidRight = LiquidWorld.grid[x + 1, y];
			LiquidRef liquidDown = LiquidWorld.grid[x, y - 1];
			LiquidRef liquidUp = LiquidWorld.grid[x, y + 1];
			LiquidRef liquidSelf = LiquidWorld.grid[x, y];

			if (liquidLeft.Amount > 0 && liquidLeft.TypeID != LiquidID.Lava || liquidRight.Amount > 0 && liquidRight.TypeID != LiquidID.Lava || liquidDown.Amount > 0 && liquidDown.TypeID != LiquidID.Lava)
			{
				int num = 0;
				if (!(liquidLeft.Type is Lava))
				{
					num += liquidLeft.Amount;
					liquidLeft.Amount = 0;
				}

				if (!(liquidRight.Type is Lava))
				{
					num += liquidRight.Amount;
					liquidRight.Amount = 0;
				}

				if (!(liquidDown.Type is Lava))
				{
					num += liquidDown.Amount;
					liquidDown.Amount = 0;
				}

				int type = (liquidLeft.Type is Honey || liquidRight.Type is Honey || liquidDown.Type is Honey)?TileID.CrispyHoneyBlock:TileID.Obsidian;

				if (num >= 24)
				{
					if (liquidSelf.Tile.active() && Main.tileObsidianKill[liquidSelf.Tile.type])
					{
						WorldGen.KillTile(x, y);
						if (Main.netMode == NetmodeID.Server)
						{
							NetMessage.SendData(MessageID.TileChange, -1, -1, null, 0, x, y);
						}
					}

					if(!liquidSelf.Tile.active())
					{
						liquidSelf.Amount = 0;
						liquidSelf.Type = null;

						Main.PlaySound(type == TileID.Obsidian ? SoundID.LiquidsWaterLava : SoundID.LiquidsHoneyLava, new Vector2(x * 16 + 8, y * 16 + 8));

						WorldGen.PlaceTile(x, y, type, true, true);
						WorldGen.SquareTileFrame(x, y);

						if (Main.netMode == NetmodeID.Server)
						{
							NetMessage.SendTileSquare(-1, x - 1, y - 1, 3, type == TileID.Obsidian ? TileChangeType.LavaWater : TileChangeType.HoneyLava);
						}
					}
				}
				
			}
			else if (liquidUp.Amount > 0 && liquidUp.TypeID != LiquidID.Lava)
			{
				bool flag = liquidSelf.Tile.active() && TileID.Sets.ForceObsidianKill[liquidSelf.Tile.type] && !TileID.Sets.ForceObsidianKill[liquidUp.Tile.type];

				if (Main.tileCut[liquidUp.Tile.type])
				{
					WorldGen.KillTile(x, y + 1);

					if (Main.netMode == NetmodeID.Server)
					{
						NetMessage.SendData(MessageID.TileChange, -1, -1, null, 0, x, y + 1);
					}
				}
				else if (liquidUp.Tile.active() && Main.tileObsidianKill[liquidUp.Tile.type])
				{
					WorldGen.KillTile(x, y + 1);

					if (Main.netMode == NetmodeID.Server)
					{
						NetMessage.SendData(MessageID.TileChange, -1, -1, null, 0, x, y + 1);
					}
				}

				if (!liquidUp.Tile.active() | flag)
				{
					if (liquidSelf.Amount < 24)
					{
						liquidSelf.Amount = 0;
						liquidSelf.Type = null;

						if (Main.netMode == NetmodeID.Server)
						{
							NetMessage.SendTileSquare(-1, x - 1, y, 3, TileChangeType.None);
						}
					}
					else
					{
						int type = TileID.Obsidian;

						if (liquidUp.TypeID == LiquidID.Honey)
						{
							type = TileID.CrispyHoneyBlock;
						}
						liquidSelf.Amount = 0;
						liquidSelf.Type = null;

						//liquidSelf.lava(false);
						liquidUp.Amount = 0;

						if (type == TileID.Obsidian)
						{
							Main.PlaySound(type == TileID.Obsidian ? SoundID.LiquidsWaterLava : SoundID.LiquidsHoneyLava, new Vector2(x * 16 + 8, y * 16 + 8));
						}
						WorldGen.PlaceTile(x, y + 1, type, true, true);
						WorldGen.SquareTileFrame(x, y + 1);

						if (Main.netMode == NetmodeID.Server)
						{
							NetMessage.SendTileSquare(-1, x - 1, y, 3, type == TileID.Obsidian ? TileChangeType.LavaWater : TileChangeType.HoneyLava);
						}
					}
				}
			}
		}

		private static void LiquidOnHoneyCheck(On.Terraria.Liquid.orig_HoneyCheck orig, int x, int y)
		{
			LiquidRef liquidLeft = LiquidWorld.grid[x - 1, y];
			LiquidRef liquidRight = LiquidWorld.grid[x + 1, y];
			LiquidRef liquidDown = LiquidWorld.grid[x, y - 1];
			LiquidRef liquidUp = LiquidWorld.grid[x, y + 1];
			LiquidRef liquidSelf = LiquidWorld.grid[x, y];

			bool flag = false;

			if (liquidLeft.Amount > 0 && liquidLeft.Type is Water || liquidRight.Amount > 0 && liquidRight.Type is Water || liquidDown.Amount > 0 && liquidDown.Type is Water)
			{
				int num = 0;

				if (liquidLeft.Type is Water)
				{
					num += liquidLeft.Amount;
					liquidLeft.Amount = 0;
				}

				if (liquidRight.Type is Water)
				{
					num += liquidRight.Amount;
					liquidRight.Amount = 0;
				}

				if (liquidDown.Type is Water)
				{
					num += liquidDown.Amount;
					liquidDown.Amount = 0;
				}

				if (liquidLeft.Type is Lava || liquidRight.Type is Lava || liquidDown.Type is Lava)
				{
					flag = true;
				}
				if (num < 32)
				{
					return;
				}

				if (liquidSelf.Tile.active() && Main.tileObsidianKill[liquidSelf.Tile.type])
				{
					WorldGen.KillTile(x, y);

					if (Main.netMode == NetmodeID.Server)
					{
						NetMessage.SendData(MessageID.TileChange, -1, -1, null, 0, x, y);
					}
				}

				if (liquidSelf.Tile.active())
				{
					return;
				}

				liquidSelf.Amount = 0;
				liquidSelf.Type = null;

				WorldGen.PlaceTile(x, y, TileID.HoneyBlock, true, true);

				Main.PlaySound(flag?SoundID.LiquidsHoneyLava:SoundID.LiquidsHoneyWater, new Vector2(x * 16 + 8, y * 16 + 8));
				
				WorldGen.SquareTileFrame(x, y);

				if (Main.netMode != NetmodeID.Server)
				{
					return;
				}

				NetMessage.SendTileSquare(-1, x - 1, y - 1, 3, flag ? TileChangeType.HoneyLava : TileChangeType.HoneyWater);
			}
			else if(liquidUp.Amount > 0 && liquidUp.Type is Water)
			{
				if(Main.tileCut[liquidUp.Tile.type] || (liquidUp.Tile.active() && Main.tileObsidianKill[liquidUp.Tile.type]))
				{
					WorldGen.KillTile(x, y + 1);
					if (Main.netMode == NetmodeID.Server)
					{
						NetMessage.SendData(MessageID.TileChange, -1, -1, null, 0, x, y + 1);
					}
				}

				if (!liquidUp.Tile.active())
				{
					if (liquidSelf.Amount < 32)
					{
						liquidSelf.Amount = 0;
						liquidSelf.Type = null;

						if (Main.netMode != NetmodeID.Server)
						{
							NetMessage.SendTileSquare(-1, x - 1, y, 3);
						}
					}
					else
					{
						if (liquidUp.Type is Lava)
						{
							flag = true;
						}

						liquidSelf.Amount = 0;
						liquidSelf.Type = null;
						liquidUp.Amount = 0;
						liquidUp.Type = null;

						Main.PlaySound(flag?SoundID.LiquidsHoneyLava:SoundID.LiquidsHoneyWater, new Vector2(x * 16 + 8,y * 16 + 8));

						WorldGen.PlaceTile(x, y + 1, TileID.HoneyBlock, true, true);
						WorldGen.SquareTileFrame(x, y + 1);

						if (Main.netMode == NetmodeID.Server)
						{
							NetMessage.SendTileSquare(-1, x - 1, y, 3, flag ? TileChangeType.HoneyLava : TileChangeType.HoneyWater);
						}
					}
				}
			}
		}
	}
}
