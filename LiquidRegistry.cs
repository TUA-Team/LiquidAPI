using LiquidAPI.Hooks;
using LiquidAPI.LiquidMod;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace LiquidAPI
{
    public static class LiquidRegistry
    {
        internal static Dictionary<int, ModLiquid> liquidList;
        internal static Dictionary<int, Color> mapColorLookup;
        private static int initialLiquidIndex = 0;//3;
        private static int liquidTextureIndex = 12;

        private const int vanillaMax = 13;

        static LiquidRegistry()
        {
            liquidList = new Dictionary<int, ModLiquid>();
            mapColorLookup = new Dictionary<int, Color>();
            LiquidAPI.OnUnload += () =>
            {
                Array.Resize(ref Main.liquidTexture, vanillaMax);
                Array.Resize(ref LiquidRenderer.DEFAULT_OPACITY, 3);
                Array.Resize(ref LiquidRenderer.WATERFALL_LENGTH, 3);
                liquidList.Clear();
                liquidList = null;
            };
        }

        public static void AddLiquid<TLiquid>(this Mod mod, string name, Texture2D texture = null, Texture2D fancyTexture2D = null) where TLiquid : ModLiquid, new()
        {
            mod.AddLiquid(name, new TLiquid(), texture);
        }
        public static void AddLiquid(this Mod mod, string name, ModLiquid liquid, Texture2D texture = null, Texture2D fancyTexture2D = null)
        {
            liquid.Mod = mod;
            liquid.Name = name;
            liquid.DisplayName = mod.CreateTranslation($"Mods.{mod.Name}.ItemName.{name}".Replace(" ", "_"));
            liquid.DisplayName.SetDefault(Regex.Replace(name, "([A-Z])", " $1").Trim());

            // TODO: texture is not being used?? Also, I think all of this can be moved into the netMode != Server
            //Texture2D usedTexture = texture ?? liquid.OldTexture;
            Texture2D fancyTexture = fancyTexture2D ?? liquid.Texture;
            Array.Resize(ref Main.liquidTexture, Main.liquidTexture.Length + 1);
            Array.Resize(ref LiquidRenderer.DEFAULT_OPACITY, LiquidRenderer.DEFAULT_OPACITY.Length + 1);
            Array.Resize(ref LiquidRenderer.WATERFALL_LENGTH, LiquidRenderer.WATERFALL_LENGTH.Length + 1);
            LiquidRenderer.WATERFALL_LENGTH[LiquidRenderer.WATERFALL_LENGTH.Length - 1] = liquid.WaterfallLength;
            LiquidRenderer.DEFAULT_OPACITY[LiquidRenderer.DEFAULT_OPACITY.Length - 1] = liquid.DefaultOpacity;
            LiquidHooks.staticNPCWet.Add(initialLiquidIndex, false);
            liquid.Type = initialLiquidIndex++;
            liquid.SetDefaults();
            liquidList.Add(liquid.Type, liquid);

            if (Main.netMode != NetmodeID.Server && liquid.Type > 2)
            {
                LiquidRenderer.Instance.LiquidTextures.Add(liquid.Type + 9, fancyTexture);
            }

            liquid.AddModBucket();
        }

        public static void Unload()
        {
            liquidList.Clear();
        }

        public static ModLiquid GetLiquid(int i)
        {
            return liquidList.TryGetValue(i, out var liq) ? liq : null;
        }

        public static ModLiquid GetLiquid(Mod mod, string name)
        {
            return liquidList.Values.Single(i => i.Mod.Name == mod.Name && i.Name == name);
        }

        public static void PreDrawValue(ref bool bg, ref int style, ref float Alpha)
        {
            foreach (ModLiquid liquid in liquidList.Values)
            {
                liquid.PreDraw(Main.tileBatch);
            }
        }

        public static void Update()
        {
            foreach (ModLiquid liquid in liquidList.Values)
            {
                liquid.Update();
            }
        }

        public static float SetOpacity(LiquidRef liquid)
        {
            /*for (byte by = 0; by < LiquidRegistry.liquidList.Count; by = (byte) (by + 1))
			{
				if (liquid.Liquids((byte) (2 + by)))
				{
					return liquidList[by].SetLiquidOpacity();
				}
			}*/

            return 1f;
        }

        public static void PlayerInteraction(byte index, Player target)
        {
            liquidList[index].PlayerInteraction(target);
        }

        public static void NPCInteraction(byte index, NPC target)
        {
            liquidList[index].NPCInteraction(target);
        }

        public static void ItemInteraction(byte index, Item item)
        {
            liquidList[index].ItemInteraction(item);
        }

        /*public static bool RunUpdate(byte index, int x, int y)
		{
			int newIndex = index - 3;
			if (newIndex > liquidList.Count || newIndex < 0)
			{
				return false;
			}
			else
			{
				return liquidList[newIndex].CustomPhysic(x, y);
			}
		}*/

        public static void ModLiquidCheck(ModLiquid liquidType, int x, int y)
        {
            LiquidHooks.NewModLiquidCheck(x, y, liquidType);
        }
        /*
        public static void ModLiquidCheck(ModLiquid liquidType, int x, int y)
        {
            LiquidRef liquidLeft = LiquidWorld.grid[x - 1, y];
            LiquidRef liquidRight = LiquidWorld.grid[x + 1, y];
            LiquidRef liquidDown = LiquidWorld.grid[x, y - 1];
            LiquidRef liquidUp = LiquidWorld.grid[x, y + 1];
            LiquidRef liquidSelf = LiquidWorld.grid[x, y];

            if (liquidLeft.Amount > 0 && liquidLeft.TypeID != liquidType.Type || liquidRight.Amount > 0 && liquidRight.TypeID != liquidType.Type || liquidDown.Amount > 0 && liquidDown.TypeID != liquidType.Type)
            {
                int liquidAmount = 0;
                if (liquidLeft.Type != null && !(liquidLeft.Type.GetType() == liquidType.Type.GetType()))
                {
                    liquidAmount += liquidLeft.Amount;
                    liquidLeft.Amount = 0;
                }

                if (liquidRight.Type != null && !(liquidRight.Type.GetType() == liquidType.Type.GetType()))
                {
                    liquidAmount += liquidRight.Amount;
                    liquidRight.Amount = 0;
                }

                if (liquidDown.Type != null && !(liquidDown.Type.GetType() == liquidType.Type.GetType()))
                {
                    liquidAmount += liquidDown.Amount;
                    liquidDown.Amount = 0;

                }

                int type = liquidSelf.Type.LiquidInteraction(liquidUp, liquidDown, liquidLeft, liquidRight, liquidSelf.X, liquidSelf.Y);

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
            else if (liquidUp.Amount > 0 && liquidUp.TypeID != liquidType.Type)
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
                else if (liquidUp.Tile.active())
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
                        int type = liquidSelf.Type.LiquidInteraction(liquidUp, liquidDown, liquidLeft, liquidRight, liquidSelf.X, liquidSelf.Y);

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
        }*/
    }
}