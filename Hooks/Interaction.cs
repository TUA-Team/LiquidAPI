using LiquidAPI.LiquidMod;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;

namespace LiquidAPI.Hooks
{
    internal static partial class LiquidHooks
    {
        //public const int DEF_TYPE = 56;

        public static void LiquidOnLavaCheck(On.Terraria.Liquid.orig_LavaCheck orig, int x, int y)
        {
            NewModLiquidCheck(x, y, LiquidRegistry.GetLiquid(LiquidAPI.Instance, "Lava"));
        }

        public static void NewModLiquidCheck(int x, int y, ModLiquid targetType)
        {
            LiquidRef left = default;
            LiquidRef right = default;
            LiquidRef up = default;
            LiquidRef down = default;
            LiquidRef self = LiquidWorld.grid[x, y];

            bool flag = false;
            int liquidAmount = 0;
            if (x != 0)
            {
                left = LiquidWorld.grid[x - 1, y];
                if (left.LiquidType != null)
                {
                    if (left.Amount != 0 && left.LiquidType.Type != targetType.Type)
                    {
                        if (left.LiquidType.Type != self.LiquidType.Type)
                        {
                            liquidAmount += left.Amount;
                            LiquidWorld.grid[x - 1, y].Amount = 0;
                        }
                        flag = true;
                    }
                }
            }
            else if (x != Main.maxTilesX)
            {
                right = LiquidWorld.grid[x + 1, y];
                if (right.Amount != 0 && right.LiquidType.Type != targetType.Type)
                {
                    if (right.LiquidType.Type != self.LiquidType.Type)
                    {
                        liquidAmount += right.Amount;
                        LiquidWorld.grid[x + 1, y].Amount = 0;
                    }
                    flag = true;
                }
            }
            else if (y != 0)
            {
                up = LiquidWorld.grid[x, y - 1];
                if (up.Amount != 0 && up.LiquidType.Type != targetType.Type)
                {
                    if (up.LiquidType.Type != self.LiquidType.Type)
                    {
                        liquidAmount += up.Amount;
                        LiquidWorld.grid[x, y - 1].Amount = 0;
                    }
                    flag = true;
                }
            }
            else if (x != Main.maxTilesY)
            {
                down = LiquidWorld.grid[x, y + 1];
                if (down.Amount != 0 && down.LiquidType.Type != targetType.Type)
                {
                    if (down.LiquidType.Type != self.LiquidType.Type)
                    {
                        liquidAmount += down.Amount;
                        LiquidWorld.grid[x, y + 1].Amount = 0;
                    }
                    flag = true;
                }
            }
            if (flag)
            {
                if (x != 0 && left.LiquidType.GetType() == self.LiquidType.GetType())
                    return;
                if (x != Main.maxTilesX && right.LiquidType.GetType() == self.LiquidType.GetType())
                    return;
                if (y != Main.maxTilesY && down.LiquidType.GetType() == self.LiquidType.GetType())
                    return;

                if (self.LiquidType.LiquidInteraction(x, y, targetType))
                    return;

                // what does this code even do? - Agrair
#pragma warning disable IDE0059 // Unnecessary assignment of a value
                int type = -1;
#pragma warning restore IDE0059 // Unnecessary assignment of a value
                type = LiquidAPI.interactionResult[LiquidWorld.grid[x - 1, y].LiquidType.Type, targetType.Type];
                if (type == -1)
                {
                    type = LiquidAPI.interactionResult[LiquidWorld.grid[x + 1, y].LiquidType.Type, targetType.Type];
                    if (type == -1)
                    {
                        type = LiquidAPI.interactionResult[LiquidWorld.grid[x, y + 1].LiquidType.Type, targetType.Type];
                    }
                }

                if (type == -1)
                    return;

                if (liquidAmount >= 24)
                {
                    if (self.Tile.active() && Main.tileObsidianKill[self.Tile.type])
                    {
                        WorldGen.KillTile(x, y);
                        if (Main.netMode == NetmodeID.Server)
                        {
                            NetMessage.SendData(MessageID.TileChange, -1, -1, null, 0, x, y);
                        }
                    }

                    if (!self.Tile.active())
                    {
                        self.Amount = 0;
                        self.LiquidType = null;

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
            else if (y != 0 && up.Amount > 0 && up.LiquidType.Type != targetType.Type)
            {
                flag = self.Tile.active() && TileID.Sets.ForceObsidianKill[self.Tile.type] && !TileID.Sets.ForceObsidianKill[up.Tile.type];

                if (Main.tileCut[up.Tile.type])
                {
                    WorldGen.KillTile(x, y + 1);

                    if (Main.netMode == NetmodeID.Server)
                    {
                        NetMessage.SendData(MessageID.TileChange, -1, -1, null, 0, x, y + 1);
                    }
                }
                else if (up.Tile.active() && Main.tileObsidianKill[up.Tile.type])
                {
                    WorldGen.KillTile(x, y + 1);

                    if (Main.netMode == NetmodeID.Server)
                    {
                        NetMessage.SendData(MessageID.TileChange, -1, -1, null, 0, x, y + 1);
                    }
                }

                if (!up.Tile.active() || flag)
                {
                    if (self.Amount < 24)
                    {
                        self.Amount = 0;
                        self.LiquidType = null;

                        if (Main.netMode == NetmodeID.Server)
                        {
                            NetMessage.SendTileSquare(-1, x - 1, y, 3, TileChangeType.None);
                        }
                    }
                    else
                    {


                        if (self.LiquidType.LiquidInteraction(x, y, targetType))
                            return;
                        int type = LiquidAPI.interactionResult[LiquidWorld.grid[x - 1, y].LiquidType.Type, targetType.Type];

                        if (type == -1)
                        {
                            return;
                        }
                        self.Amount = 0;
                        self.LiquidType = null;

                        //self.lava(false);
                        up.Amount = 0;

                        // TODO: add functionality for interaction sounds
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


        public static void LiquidOnHoneyCheck(On.Terraria.Liquid.orig_HoneyCheck orig, int x, int y)
        {
            NewModLiquidCheck(x, y, LiquidRegistry.GetLiquid(LiquidAPI.Instance, "Honey"));
        }

        /*
        public static void LiquidOnHoneyCheck(On.Terraria.Liquid.orig_HoneyCheck orig, int x, int y)
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
