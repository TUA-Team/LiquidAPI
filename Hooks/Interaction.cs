using System;
using LiquidAPI.ID;
using LiquidAPI.LiquidMod;
using LiquidAPI.Vanilla;
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
            NewModLiquidCheck(x, y, LiquidRegistry.GetLiquid(LiquidAPI.instance, "Lava"));
        }

        internal static void NewModLiquidCheck(int x, int y, ModLiquid targetType)
        {
            LiquidRef liquidLeft = LiquidWorld.grid[x - 1, y];
            LiquidRef liquidRight = LiquidWorld.grid[x + 1, y];
            LiquidRef liquidDown = LiquidWorld.grid[x, y - 1];
            LiquidRef liquidUp = LiquidWorld.grid[x, y + 1];
            LiquidRef liquidSelf = LiquidWorld.grid[x, y];


            LiquidRef self = LiquidWorld.grid[x, y];
            ModLiquid selfType = self.Type;

            int validLiquidAmount = 0;

            

            if (liquidLeft.Amount > 0 && liquidLeft.Type.Type != targetType.Type || liquidRight.Amount > 0 && liquidRight.Type.Type != targetType.Type || liquidDown.Amount > 0 && liquidDown.Type.Type != targetType.Type)
            {
                int liquidAmount = 0;

                if (LiquidWorld.grid[x, y - 1].Amount != 0 && LiquidWorld.grid[x, y - 1].Type.GetType() != selfType.GetType())
                {
                    liquidAmount += LiquidWorld.grid[x, y - 1].Amount;
                    LiquidWorld.grid[x, y - 1].Amount = 0;
                }

                if (LiquidWorld.grid[x, y + 1].Amount != 0 && LiquidWorld.grid[x, y + 1].Type.GetType() != selfType.GetType())
                {
                    liquidAmount += LiquidWorld.grid[x, y + 1].Amount;
                    LiquidWorld.grid[x, y + 1].Amount = 0;
                }

                if (LiquidWorld.grid[x - 1, y].Amount != 0 && LiquidWorld.grid[x - 1, y].Type.GetType() != selfType.GetType())
                {
                    liquidAmount += LiquidWorld.grid[x - 1, y].Amount;
                    LiquidWorld.grid[x - 1, y].Amount = 0;
                }

                if (LiquidWorld.grid[x + 1, y].Amount != 0 && LiquidWorld.grid[x + 1, y].Type.GetType() != selfType.GetType()){
                    liquidAmount += LiquidWorld.grid[x + 1, y].Amount;
                    LiquidWorld.grid[x + 1, y].Amount = 0;
                }

                if (liquidLeft.Type.GetType() == liquidSelf.Type.GetType() && liquidRight.Type.GetType() == liquidSelf.Type.GetType() && liquidDown.Type.GetType() == liquidSelf.Type.GetType())
                    return;
                int type = -1;

                if (self.Type.LiquidInteraction(x, y, targetType))
                {
                    return;
                }

                type = LiquidAPI.interactionResult[LiquidWorld.grid[x - 1, y].Type.Type, targetType.Type];
                if (type == -1)
                {
                    type = LiquidAPI.interactionResult[LiquidWorld.grid[x + 1, y].Type.Type, targetType.Type];
                    if (type == -1)
                    {
                        type = LiquidAPI.interactionResult[LiquidWorld.grid[x, y + 1].Type.Type, targetType.Type];
                    }
                }
                if (liquidAmount >= 24)
                {
                    if (liquidSelf.Tile.active() && Main.tileObsidianKill[liquidSelf.Tile.type])
                    {
                        WorldGen.KillTile(x, y);
                        if (Main.netMode == NetmodeID.Server)
                        {
                            NetMessage.SendData(MessageID.TileChange, -1, -1, null, 0, x, y);
                        }
                    }

                    if (!liquidSelf.Tile.active())
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
            else if (liquidUp.Amount > 0 && liquidUp.Type.Type != targetType.Type)
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
                        

                        if (self.Type.LiquidInteraction(x, y, targetType))
                        {
                            return;
                        }
                        int type = -1;
                        type = LiquidAPI.interactionResult[LiquidWorld.grid[x - 1, y].Type.Type, targetType.Type];
                        if (type == -1)
                        {
                            type = LiquidAPI.interactionResult[LiquidWorld.grid[x + 1, y].Type.Type, targetType.Type];
                            if (type == -1)
                            {
                                type = LiquidAPI.interactionResult[LiquidWorld.grid[x, y + 1].Type.Type, targetType.Type];
                            }
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
            NewModLiquidCheck(x, y, LiquidRegistry.GetLiquid(LiquidAPI.instance, "Honey"));
        }

        /*
        private static void LiquidOnHoneyCheck(On.Terraria.Liquid.orig_HoneyCheck orig, int x, int y)
        {
            LiquidRef liquidLeft = LiquidWorld.grid[x - 1, y];
            LiquidRef liquidRight = LiquidWorld.grid[x + 1, y];
            LiquidRef liquidDown = LiquidWorld.grid[x, y - 1];
            LiquidRef liquidUp = LiquidWorld.grid[x, y + 1];
            LiquidRef liquidSelf = LiquidWorld.grid[x, y];

            bool lava = false;

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
                    lava = true;
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

                int type = -1;
                try
                {
                    if (liquidUp.Type == null || liquidDown.Type == null || liquidLeft.Type == null || liquidRight.Type == null)
                    {
                        type = TileID.HoneyBlock;
                    }
                    else
                    {
                        type = liquidSelf.Type.LiquidInteraction(liquidUp, liquidDown, liquidLeft, liquidRight, liquidSelf.X, liquidSelf.Y);
                    }
                }
                catch
                {
                    type = TileID.HoneyBlock;
                }
                WorldGen.PlaceTile(x, y, type, true, true);

                Main.PlaySound(lava ? SoundID.LiquidsHoneyLava : SoundID.LiquidsHoneyWater, new Vector2(x * 16 + 8, y * 16 + 8));

                WorldGen.SquareTileFrame(x, y);

                if (Main.netMode != NetmodeID.Server)
                {
                    return;
                }

                NetMessage.SendTileSquare(-1, x - 1, y - 1, 3, lava ? TileChangeType.HoneyLava : TileChangeType.HoneyWater);
            }
            else if (liquidUp.Amount > 0 && liquidUp.Type is Water)
            {
                if (Main.tileCut[liquidUp.Tile.type] || (liquidUp.Tile.active() && Main.tileObsidianKill[liquidUp.Tile.type]))
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
                            lava = true;
                        }

                        liquidSelf.Amount = 0;
                        liquidSelf.Type = null;
                        liquidUp.Amount = 0;
                        liquidUp.Type = null;

                        Main.PlaySound(lava ? SoundID.LiquidsHoneyLava : SoundID.LiquidsHoneyWater, new Vector2(x * 16 + 8, y * 16 + 8));

                        WorldGen.PlaceTile(x, y + 1, TileID.HoneyBlock, true, true);
                        WorldGen.SquareTileFrame(x, y + 1);

                        if (Main.netMode == NetmodeID.Server)
                        {
                            NetMessage.SendTileSquare(-1, x - 1, y, 3, lava ? TileChangeType.HoneyLava : TileChangeType.HoneyWater);
                        }
                    }
                }
            }
        }
        */
    }
}
