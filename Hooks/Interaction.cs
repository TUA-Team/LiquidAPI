using LiquidAPI.ID;
using LiquidAPI.LiquidMod;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.Localization;

namespace LiquidAPI.Hooks
{
    internal static partial class LiquidHooks
    {
        private const int DEF_TYPE = 56;

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
                int type = DEF_TYPE;

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
                    type = 230;

                if (num < 24)
                    return;
                if (liquidSelf.Tile.active() && Main.tileObsidianKill[liquidSelf.Tile.type])
                {
                    WorldGen.KillTile(x, y, false, false, false);
                    if (Main.netMode == NetmodeID.Server)
                        NetMessage.SendData(17, -1, -1, (NetworkText)null, 0, (float)x, (float)y, 0.0f, 0, 0, 0);
                }

                if (liquidSelf.Tile.active())
                    return;

                liquidSelf.Amount = 0;
                liquidSelf.Type = LiquidID.water;
                //liquidSelf.lava(false);

                if (type == DEF_TYPE)
                    Main.PlaySound(SoundID.LiquidsWaterLava, new Vector2((float)(x * 16 + 8), (float)(y * 16 + 8)));
                else
                    Main.PlaySound(SoundID.LiquidsHoneyLava, new Vector2((float)(x * 16 + 8), (float)(y * 16 + 8)));

                WorldGen.PlaceTile(x, y, type, true, true, -1, 0);
                WorldGen.SquareTileFrame(x, y, true);

                if (Main.netMode != NetmodeID.Server)
                    return;

                NetMessage.SendTileSquare(-1, x - 1, y - 1, 3,
                    type == DEF_TYPE ? TileChangeType.LavaWater : TileChangeType.HoneyLava);
            }
            else
            {
                if (liquidUp.Amount <= 0 || liquidUp.Type == LiquidID.lava)
                    return;

                bool flag = liquidSelf.Tile.active() && TileID.Sets.ForceObsidianKill[liquidSelf.Tile.type] &&
                            !TileID.Sets.ForceObsidianKill[liquidUp.Tile.type];

                if (Main.tileCut[liquidUp.Tile.type])
                {
                    WorldGen.KillTile(x, y + 1, false, false, false);

                    if (Main.netMode == NetmodeID.Server)
                        NetMessage.SendData(17, -1, -1, (NetworkText)null, 0, (float)x, (float)(y + 1), 0.0f, 0, 0, 0);
                }
                else if (liquidUp.Tile.active() && Main.tileObsidianKill[liquidUp.Tile.type])
                {
                    WorldGen.KillTile(x, y + 1, false, false, false);

                    if (Main.netMode == NetmodeID.Server)
                        NetMessage.SendData(17, -1, -1, (NetworkText)null, 0, (float)x, (float)(y + 1), 0.0f, 0, 0, 0);
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
                    int type = DEF_TYPE;

                    if (liquidUp.Type == LiquidID.honey)
                        type = 230;

                    liquidSelf.Amount = 0;
                    liquidSelf.Type = LiquidID.water;

                    //liquidSelf.lava(false);
                    liquidUp.Amount = 0;

                    if (type == DEF_TYPE)
                        Main.PlaySound(SoundID.LiquidsWaterLava, new Vector2((float)(x * 16 + 8), (float)(y * 16 + 8)));
                    else
                        Main.PlaySound(SoundID.LiquidsHoneyLava, new Vector2((float)(x * 16 + 8), (float)(y * 16 + 8)));

                    WorldGen.PlaceTile(x, y + 1, type, true, true, -1, 0);
                    WorldGen.SquareTileFrame(x, y + 1, true);

                    if (Main.netMode != NetmodeID.Server)
                        return;

                    NetMessage.SendTileSquare(-1, x - 1, y, 3, type == DEF_TYPE ? TileChangeType.LavaWater : TileChangeType.HoneyLava);
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
                    num += (int)liquidLeft.Amount;
                    liquidLeft.Amount = (byte)0;
                }

                if (liquidRight.Type == LiquidID.water)
                {
                    num += (int)liquidRight.Amount;
                    liquidRight.Amount = (byte)0;
                }

                if (liquidDown.Type == LiquidID.water)
                {
                    num += (int)liquidDown.Amount;
                    liquidDown.Amount = (byte)0;
                }

                if (liquidLeft.Type == LiquidID.lava || liquidRight.Type == LiquidID.lava || liquidDown.Type == LiquidID.lava)
                    flag = true;

                if (num < 32)
                    return;

                if (liquidSelf.Tile.active() && Main.tileObsidianKill[liquidSelf.Tile.type])
                {
                    WorldGen.KillTile(x, y, false, false, false);

                    if (Main.netMode == NetmodeID.Server)
                        NetMessage.SendData(17, -1, -1, (NetworkText)null, 0, (float)x, (float)y, 0.0f, 0, 0, 0);
                }

                if (liquidSelf.Tile.active())
                    return;

                liquidSelf.Amount = 0;
                liquidSelf.Type = LiquidID.water;

                WorldGen.PlaceTile(x, y, 229, true, true, -1, 0);

                if (flag)
                    Main.PlaySound(SoundID.LiquidsHoneyLava, new Vector2((float)(x * 16 + 8), (float)(y * 16 + 8)));
                else
                    Main.PlaySound(SoundID.LiquidsHoneyWater, new Vector2((float)(x * 16 + 8), (float)(y * 16 + 8)));

                WorldGen.SquareTileFrame(x, y, true);

                if (Main.netMode != NetmodeID.Server)
                    return;

                NetMessage.SendTileSquare(-1, x - 1, y - 1, 3,
                    flag ? TileChangeType.HoneyLava : TileChangeType.HoneyWater);
            }
            else
            {
                if (liquidUp.Amount <= 0 || liquidUp.Type != LiquidID.water)
                    return;

                if (Main.tileCut[liquidUp.Tile.type])
                {
                    WorldGen.KillTile(x, y + 1, false, false, false);

                    if (Main.netMode == NetmodeID.Server)
                        NetMessage.SendData(17, -1, -1, (NetworkText)null, 0, (float)x, (float)(y + 1), 0.0f, 0, 0, 0);
                }
                else if (liquidUp.Tile.active() && Main.tileObsidianKill[liquidUp.Tile.type])
                {
                    WorldGen.KillTile(x, y + 1, false, false, false);
                    if (Main.netMode == NetmodeID.Server)
                        NetMessage.SendData(17, -1, -1, (NetworkText)null, 0, (float)x, (float)(y + 1), 0.0f, 0, 0,
                            0);
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
                        flag = true;

                    liquidSelf.Amount = 0;
                    liquidSelf.Type = LiquidID.water;
                    liquidUp.Amount = 0;
                    liquidUp.Type = LiquidID.water;

                    if (flag)
                        Main.PlaySound(SoundID.LiquidsHoneyLava, new Vector2((float)(x * 16 + 8), (float)(y * 16 + 8)));
                    else
                        Main.PlaySound(SoundID.LiquidsHoneyWater, new Vector2((float)(x * 16 + 8), (float)(y * 16 + 8)));

                    WorldGen.PlaceTile(x, y + 1, 229, true, true, -1, 0);
                    WorldGen.SquareTileFrame(x, y + 1, true);

                    if (Main.netMode != NetmodeID.Server)
                        return;

                    NetMessage.SendTileSquare(-1, x - 1, y, 3,
                        flag ? TileChangeType.HoneyLava : TileChangeType.HoneyWater);
                }
            }
        }
    }
}
