using LiquidAPI.LiquidMod;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.Localization;

namespace LiquidAPI.Swap
{
    class LiquidCheck
    {
        public static void LavaCheck(int x, int y)
        {
            Tile liquidLeft = Main.tile[x - 1, y];
            Tile liquidRight = Main.tile[x + 1, y];
            Tile liquidUp = Main.tile[x, y - 1];
            Tile liquidDown = Main.tile[x, y + 1];
            Tile liquidSelf = Main.tile[x, y];

            byte liquidLeftType = LiquidCore.liquidGrid[x - 1, y].data;
            byte liquidRightType = LiquidCore.liquidGrid[x + 1, y].data;
            byte liquidUpType = LiquidCore.liquidGrid[x, y - 1].data;
            byte liquidDownType = LiquidCore.liquidGrid[x, y + 1].data;

            if (liquidLeft.liquid > (byte)0 && liquidLeftType != 1 || liquidRight.liquid > (byte)0 && liquidRightType != 1 || liquidUp.liquid > (byte)0 && liquidUpType != 1)
            {
                int num = 0;
                int type = 56;
                if (liquidLeftType == 0)
                {
                    num += (int)liquidLeft.liquid;
                    liquidLeft.liquid = (byte)0;
                }
                if (liquidRightType == 0)
                {
                    num += (int)liquidRight.liquid;
                    liquidRight.liquid = (byte)0;
                }
                if (liquidUpType == 0)
                {
                    num += (int)liquidUp.liquid;
                    liquidUp.liquid = (byte)0;
                }
                if (liquidLeftType == 2 || liquidRightType == 2 || liquidUpType == 2) // check if it's honey
                    type = 230;
                if (num < 24)
                    return;
                if (liquidSelf.active() && Main.tileObsidianKill[(int)liquidSelf.type])
                {
                    WorldGen.KillTile(x, y, false, false, false);
                    if (Main.netMode == 2)
                        NetMessage.SendData(17, -1, -1, (NetworkText)null, 0, (float)x, (float)y, 0.0f, 0, 0, 0);
                }
                if (liquidSelf.active())
                    return;
                liquidSelf.liquid = (byte)0;
                LiquidCore.liquidGrid[x, y].data = 0;
                if (type == 56)
                    Main.PlaySound(SoundID.LiquidsWaterLava, new Vector2((float)(x * 16 + 8), (float)(y * 16 + 8)));
                else
                    Main.PlaySound(SoundID.LiquidsHoneyLava, new Vector2((float)(x * 16 + 8), (float)(y * 16 + 8)));
                WorldGen.PlaceTile(x, y, type, true, true, -1, 0);
                WorldGen.SquareTileFrame(x, y, true);
                if (Main.netMode != 2)
                    return;
                NetMessage.SendTileSquare(-1, x - 1, y - 1, 3, type == 56 ? TileChangeType.LavaWater : TileChangeType.HoneyLava);
            }
            else
            {
                if (liquidDown.liquid <= (byte)0 || liquidDownType == 1)
                    return;
                bool flag = false;
                if (liquidSelf.active() && TileID.Sets.ForceObsidianKill[(int)liquidSelf.type] && !TileID.Sets.ForceObsidianKill[(int)liquidDown.type])
                    flag = true;
                if (Main.tileCut[(int)liquidDown.type])
                {
                    WorldGen.KillTile(x, y + 1, false, false, false);
                    if (Main.netMode == 2)
                        NetMessage.SendData(17, -1, -1, (NetworkText)null, 0, (float)x, (float)(y + 1), 0.0f, 0, 0, 0);
                }
                else if (liquidDown.active() && Main.tileObsidianKill[(int)liquidDown.type])
                {
                    WorldGen.KillTile(x, y + 1, false, false, false);
                    if (Main.netMode == 2)
                        NetMessage.SendData(17, -1, -1, (NetworkText)null, 0, (float)x, (float)(y + 1), 0.0f, 0, 0, 0);
                }
                if (!(!liquidDown.active() | flag))
                    return;
                if (liquidSelf.liquid < (byte)24)
                {
                    liquidSelf.liquid = (byte)0;
                    liquidSelf.liquidType(0);
                    if (Main.netMode != 2)
                        return;
                    NetMessage.SendTileSquare(-1, x - 1, y, 3, TileChangeType.None);
                }
                else
                {
                    int type = 56;
                    if (liquidDown.honey())
                        type = 230;
                    liquidSelf.liquid = (byte)0;
                    LiquidCore.liquidGrid[x, y].data = 0;
                    liquidDown.liquid = (byte)0;
                    if (type == 56)
                        Main.PlaySound(SoundID.LiquidsWaterLava, new Vector2((float)(x * 16 + 8), (float)(y * 16 + 8)));
                    else
                        Main.PlaySound(SoundID.LiquidsHoneyLava, new Vector2((float)(x * 16 + 8), (float)(y * 16 + 8)));
                    WorldGen.PlaceTile(x, y + 1, type, true, true, -1, 0);
                    WorldGen.SquareTileFrame(x, y + 1, true);
                    if (Main.netMode != 2)
                        return;
                    NetMessage.SendTileSquare(-1, x - 1, y, 3, type == 56 ? TileChangeType.LavaWater : TileChangeType.HoneyLava);
                }
            }
        }

        public static void HoneyCheck(int x, int y)
        {
            Tile liquidLeft = Main.tile[x - 1, y];
            Tile liquidRight = Main.tile[x + 1, y];
            Tile liquidUp = Main.tile[x, y - 1];
            Tile liquidDown = Main.tile[x, y + 1];
            Tile liquidSelf = Main.tile[x, y];

            byte liquidLeftType = LiquidCore.liquidGrid[x - 1, y].data;
            byte liquidRightType = LiquidCore.liquidGrid[x + 1, y].data;
            byte liquidUpType = LiquidCore.liquidGrid[x, y - 1].data;
            byte liquidDownType = LiquidCore.liquidGrid[x, y + 1].data;

            bool flag = false;
            if (liquidLeft.liquid > (byte)0 && liquidLeftType == 0 || liquidRight.liquid > (byte)0 && liquidRightType == 0 || liquidUp.liquid > (byte)0 && liquidUpType == 0)
            {
                int num = 0;
                if (liquidLeftType == (byte)0)
                {
                    num += (int)liquidLeft.liquid;
                    liquidLeft.liquid = (byte)0;
                }
                if (liquidRightType == (byte)0)
                {
                    num += (int)liquidRight.liquid;
                    liquidRight.liquid = (byte)0;
                }
                if (liquidUpType == (byte)0)
                {
                    num += (int)liquidUp.liquid;
                    liquidUp.liquid = (byte)0;
                }
                if (liquidLeftType == 1 || liquidRightType == 1 || liquidUpType == 1)
                    flag = true;
                if (num < 32)
                    return;
                if (liquidSelf.active() && Main.tileObsidianKill[(int)liquidSelf.type])
                {
                    WorldGen.KillTile(x, y, false, false, false);
                    if (Main.netMode == 2)
                        NetMessage.SendData(17, -1, -1, (NetworkText)null, 0, (float)x, (float)y, 0.0f, 0, 0, 0);
                }
                if (liquidSelf.active())
                    return;
                liquidSelf.liquid = (byte)0;
                LiquidCore.liquidGrid[x, y].data = 0;
                WorldGen.PlaceTile(x, y, 229, true, true, -1, 0);
                if (flag)
                    Main.PlaySound(SoundID.LiquidsHoneyLava, new Vector2((float)(x * 16 + 8), (float)(y * 16 + 8)));
                else
                    Main.PlaySound(SoundID.LiquidsHoneyWater, new Vector2((float)(x * 16 + 8), (float)(y * 16 + 8)));
                WorldGen.SquareTileFrame(x, y, true);
                if (Main.netMode != 2)
                    return;
                NetMessage.SendTileSquare(-1, x - 1, y - 1, 3, flag ? TileChangeType.HoneyLava : TileChangeType.HoneyWater);
            }
            else
            {
                if (liquidDown.liquid <= (byte)0 || liquidDownType != 0)
                    return;
                if (Main.tileCut[(int)liquidDown.type])
                {
                    WorldGen.KillTile(x, y + 1, false, false, false);
                    if (Main.netMode == 2)
                        NetMessage.SendData(17, -1, -1, (NetworkText)null, 0, (float)x, (float)(y + 1), 0.0f, 0, 0, 0);
                }
                else if (liquidDown.active() && Main.tileObsidianKill[(int)liquidDown.type])
                {
                    WorldGen.KillTile(x, y + 1, false, false, false);
                    if (Main.netMode == 2)
                        NetMessage.SendData(17, -1, -1, (NetworkText)null, 0, (float)x, (float)(y + 1), 0.0f, 0, 0, 0);
                }
                if (liquidDown.active())
                    return;
                if (liquidSelf.liquid < (byte)32)
                {
                    liquidSelf.liquid = (byte)0;
                    LiquidCore.liquidGrid[x, y].data = 0;
                    if (Main.netMode != 2)
                        return;
                    NetMessage.SendTileSquare(-1, x - 1, y, 3, TileChangeType.None);
                }
                else
                {
                    if (liquidDownType == 1)
                        flag = true;
                    liquidSelf.liquid = (byte)0;
                    LiquidCore.liquidGrid[x, y].data = 0;
                    liquidDown.liquid = (byte)0;
                    LiquidCore.liquidGrid[x + 1, y].data = 0;
                    if (flag)
                        Main.PlaySound(SoundID.LiquidsHoneyLava, new Vector2((float)(x * 16 + 8), (float)(y * 16 + 8)));
                    else
                        Main.PlaySound(SoundID.LiquidsHoneyWater, new Vector2((float)(x * 16 + 8), (float)(y * 16 + 8)));
                    WorldGen.PlaceTile(x, y + 1, 229, true, true, -1, 0);
                    WorldGen.SquareTileFrame(x, y + 1, true);
                    if (Main.netMode != 2)
                        return;
                    NetMessage.SendTileSquare(-1, x - 1, y, 3, flag ? TileChangeType.HoneyLava : TileChangeType.HoneyWater);
                }
            }
        }
    }
}
