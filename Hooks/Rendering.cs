using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.Remoting.Messaging;
using LiquidAPI.ID;
using LiquidAPI.LiquidMod;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Mono.Cecil.Cil;
using MonoMod.Cil;
using Terraria;
using Terraria.ModLoader;

namespace LiquidAPI.Hooks
{
    internal static partial class LiquidHooks
    {
        public static List<Texture2D> OldHoneyTexture;
        public static List<Texture2D> OldLavaTexture;
        public static List<Texture2D> OldWaterTexture;

        public static int lavaStyle = 0; //0 being default lava texture
        public static int honeyStyle = 0; //0 being default honey texture


        public static void ILDrawWaterSlope(ILContext il)
        {
            ILCursor c = new ILCursor(il);
            if (c.TryGotoNext(
                i => i.MatchLdcI4(0),
                i => i.MatchStloc(out _),
                i => i.MatchLdcI4(0),
                i => i.MatchStloc(out _),
                i => i.MatchLdcI4(0),
                i => i.MatchStloc(out _),
                i => i.MatchLdcI4(0),
                i => i.MatchStloc(out _),
                i => i.MatchLdcI4(0),
                i => i.MatchStloc(out _),
                i => i.MatchLdcI4(0),
                i => i.MatchStloc(out _)))
            {
                var c2 = c.Clone();
                if (c2.TryGotoNext(i => i.MatchLdloc(out _),
                    i => i.MatchLdcI4(0x13A),
                    i => i.MatchBneUn(out _)))
                { 
                    c.Emit(OpCodes.Br, c2.MarkLabel());

                    c2.Emit(OpCodes.Ldloc_3);
                    c2.Emit(OpCodes.Ldarg_2);
                    c2.Emit(OpCodes.Ldloc_S, (byte)15);
                    c2.Emit(OpCodes.Ldloc_S, (byte)16);
                    c2.EmitDelegate<Action<Vector2, int, int, int>>((vector, water, i, j)
                        =>
                    {
                        DrawWaterSlope(vector, i, j, water);
                    });
                }
            }
        }

        public static void Hooked_DrawWater(On.Terraria.Main.orig_DrawWater orig, Main self, bool bg = false, int Style = 0,
            float Alpha = 1f)
        {
            DrawWater(bg, Style, Alpha);
        }

        public static void DrawWater(bool bg = false, int Style = 0, float Alpha = 1f)
        {
            if (!Lighting.NotRetro)
            {
                MainOnOldDrawWater(Main.instance, bg, Style, Alpha);
                return;
            }

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            Vector2 drawOffset = (Main.drawToScreen ? Vector2.Zero : new Vector2((float)Main.offScreenRange, (float)Main.offScreenRange)) - Main.screenPosition;
            LiquidRenderer.Instance.Draw(Main.spriteBatch, drawOffset, Style, Alpha, bg);
            if (!bg)
            {
                TimeLogger.DrawTime(4, stopwatch.Elapsed.TotalMilliseconds);
            }
        }

        public static void Hooked_drawWaters(On.Terraria.Main.orig_drawWaters orig, Main self, bool bg = false,
            int styleOverride = -1, bool allowUpdate = true)
        {
            drawWaters(bg, styleOverride, allowUpdate);
        }

        public static void drawWaters(bool bg = false, int styleOverride = -1, bool allowUpdate = true)
        {
            if (!bg)
            {
                lavaStyle = 0;
                switch (Main.bgStyle)
                {
                    case 1:
                        Main.waterStyle = 1;
                        lavaStyle = 1;
                        break;
                    case 5 when Main.evilTiles > Main.holyTiles:
                        {
                            if (Main.bloodTiles > Main.evilTiles)
                            {
                                Main.waterStyle = 9;
                                lavaStyle = 2;
                            }
                            else
                            {
                                Main.waterStyle = 1;
                                lavaStyle = 1;
                            }

                            break;
                        }
                    case 5 when Main.bloodTiles > Main.holyTiles:
                        Main.waterStyle = 10;
                        lavaStyle = 2;
                        break;
                    case 5:
                        Main.waterStyle = 3;
                        break;
                    default:
                        {
                            switch (Main.bgStyle)
                            {
                                case 5 when Main.bloodTiles > Main.holyTiles:
                                    Main.waterStyle = 9;
                                    lavaStyle = 2;
                                    break;
                                case 3:
                                    Main.waterStyle = 2;
                                    break;
                                case 8:
                                    Main.waterStyle = 9;
                                    lavaStyle = 2;
                                    break;
                                case 6:
                                    Main.waterStyle = 3;
                                    break;
                                case 7:
                                    Main.waterStyle = 4;
                                    break;
                                case 2:
                                    Main.waterStyle = 5;
                                    break;
                                default:
                                    {
                                        if ((double)(Main.screenPosition.Y / 16f) > Main.rockLayer + 40.0)
                                        {
                                            if (Main.shroomTiles > 300)
                                            {
                                                Main.waterStyle = 6;
                                            }
                                            else
                                            {
                                                Main.waterStyle = 7;
                                            }
                                        }
                                        else if ((double)(Main.screenPosition.Y / 16f) > Main.worldSurface)
                                        {
                                            Main.waterStyle = 6;
                                        }
                                        else
                                        {
                                            Main.waterStyle = 0;
                                        }

                                        break;
                                    }
                            }

                            break;
                        }
                }

                WaterStyleLoader.ChooseWaterStyle(ref Main.waterStyle);
                if (Main.bgStyle != 4 && Main.bloodMoon && !Main.dayTime)
                {
                    Main.waterStyle = 8;
                }

                if (Main.fountainColor >= 0)
                {
                    Main.waterStyle = Main.fountainColor;
                }

                switch (Main.waterStyle)
                {
                    case 0:
                        {
                            Main.liquidAlpha[2] -= 0.2f;
                            if (Main.liquidAlpha[2] < 0f)
                            {
                                Main.liquidAlpha[2] = 0f;
                            }

                            Main.liquidAlpha[3] -= 0.2f;
                            if (Main.liquidAlpha[3] < 0f)
                            {
                                Main.liquidAlpha[3] = 0f;
                            }

                            Main.liquidAlpha[4] -= 0.2f;
                            if (Main.liquidAlpha[4] < 0f)
                            {
                                Main.liquidAlpha[4] = 0f;
                            }

                            Main.liquidAlpha[5] -= 0.2f;
                            if (Main.liquidAlpha[5] < 0f)
                            {
                                Main.liquidAlpha[5] = 0f;
                            }

                            Main.liquidAlpha[6] -= 0.2f;
                            if (Main.liquidAlpha[6] < 0f)
                            {
                                Main.liquidAlpha[6] = 0f;
                            }

                            Main.liquidAlpha[7] -= 0.2f;
                            if (Main.liquidAlpha[7] < 0f)
                            {
                                Main.liquidAlpha[7] = 0f;
                            }

                            Main.liquidAlpha[8] -= 0.2f;
                            if (Main.liquidAlpha[8] < 0f)
                            {
                                Main.liquidAlpha[8] = 0f;
                            }

                            Main.liquidAlpha[9] -= 0.2f;
                            if (Main.liquidAlpha[9] < 0f)
                            {
                                Main.liquidAlpha[9] = 0f;
                            }

                            Main.liquidAlpha[10] -= 0.2f;
                            if (Main.liquidAlpha[10] < 0f)
                            {
                                Main.liquidAlpha[10] = 0f;
                            }

                            Main.liquidAlpha[0] += 0.2f;
                            if (Main.liquidAlpha[0] > 1f)
                            {
                                Main.liquidAlpha[0] = 1f;
                            }

                            break;
                        }
                    case 1:
                        {
                            Main.liquidAlpha[0] -= 0.2f;
                            if (Main.liquidAlpha[0] < 0f)
                            {
                                Main.liquidAlpha[0] = 0f;
                            }

                            Main.liquidAlpha[3] -= 0.2f;
                            if (Main.liquidAlpha[3] < 0f)
                            {
                                Main.liquidAlpha[3] = 0f;
                            }

                            Main.liquidAlpha[4] -= 0.2f;
                            if (Main.liquidAlpha[4] < 0f)
                            {
                                Main.liquidAlpha[4] = 0f;
                            }

                            Main.liquidAlpha[5] -= 0.2f;
                            if (Main.liquidAlpha[5] < 0f)
                            {
                                Main.liquidAlpha[5] = 0f;
                            }

                            Main.liquidAlpha[6] -= 0.2f;
                            if (Main.liquidAlpha[6] < 0f)
                            {
                                Main.liquidAlpha[6] = 0f;
                            }

                            Main.liquidAlpha[7] -= 0.2f;
                            if (Main.liquidAlpha[7] < 0f)
                            {
                                Main.liquidAlpha[7] = 0f;
                            }

                            Main.liquidAlpha[8] -= 0.2f;
                            if (Main.liquidAlpha[8] < 0f)
                            {
                                Main.liquidAlpha[8] = 0f;
                            }

                            Main.liquidAlpha[9] -= 0.2f;
                            if (Main.liquidAlpha[9] < 0f)
                            {
                                Main.liquidAlpha[9] = 0f;
                            }

                            Main.liquidAlpha[10] -= 0.2f;
                            if (Main.liquidAlpha[10] < 0f)
                            {
                                Main.liquidAlpha[10] = 0f;
                            }

                            Main.liquidAlpha[2] += 0.2f;
                            if (Main.liquidAlpha[2] > 1f)
                            {
                                Main.liquidAlpha[2] = 1f;
                            }

                            break;
                        }
                    case 2:
                        {
                            Main.liquidAlpha[0] -= 0.2f;
                            if (Main.liquidAlpha[0] < 0f)
                            {
                                Main.liquidAlpha[0] = 0f;
                            }

                            Main.liquidAlpha[2] -= 0.2f;
                            if (Main.liquidAlpha[2] < 0f)
                            {
                                Main.liquidAlpha[2] = 0f;
                            }

                            Main.liquidAlpha[4] -= 0.2f;
                            if (Main.liquidAlpha[4] < 0f)
                            {
                                Main.liquidAlpha[4] = 0f;
                            }

                            Main.liquidAlpha[5] -= 0.2f;
                            if (Main.liquidAlpha[5] < 0f)
                            {
                                Main.liquidAlpha[5] = 0f;
                            }

                            Main.liquidAlpha[6] -= 0.2f;
                            if (Main.liquidAlpha[6] < 0f)
                            {
                                Main.liquidAlpha[6] = 0f;
                            }

                            Main.liquidAlpha[7] -= 0.2f;
                            if (Main.liquidAlpha[7] < 0f)
                            {
                                Main.liquidAlpha[7] = 0f;
                            }

                            Main.liquidAlpha[8] -= 0.2f;
                            if (Main.liquidAlpha[8] < 0f)
                            {
                                Main.liquidAlpha[8] = 0f;
                            }

                            Main.liquidAlpha[9] -= 0.2f;
                            if (Main.liquidAlpha[9] < 0f)
                            {
                                Main.liquidAlpha[9] = 0f;
                            }

                            Main.liquidAlpha[10] -= 0.2f;
                            if (Main.liquidAlpha[10] < 0f)
                            {
                                Main.liquidAlpha[10] = 0f;
                            }

                            Main.liquidAlpha[3] += 0.2f;
                            if (Main.liquidAlpha[3] > 1f)
                            {
                                Main.liquidAlpha[3] = 1f;
                            }

                            break;
                        }
                    case 3:
                        {
                            Main.liquidAlpha[0] -= 0.2f;
                            if (Main.liquidAlpha[0] < 0f)
                            {
                                Main.liquidAlpha[0] = 0f;
                            }

                            Main.liquidAlpha[2] -= 0.2f;
                            if (Main.liquidAlpha[2] < 0f)
                            {
                                Main.liquidAlpha[2] = 0f;
                            }

                            Main.liquidAlpha[3] -= 0.2f;
                            if (Main.liquidAlpha[3] < 0f)
                            {
                                Main.liquidAlpha[3] = 0f;
                            }

                            Main.liquidAlpha[5] -= 0.2f;
                            if (Main.liquidAlpha[5] < 0f)
                            {
                                Main.liquidAlpha[5] = 0f;
                            }

                            Main.liquidAlpha[6] -= 0.2f;
                            if (Main.liquidAlpha[6] < 0f)
                            {
                                Main.liquidAlpha[6] = 0f;
                            }

                            Main.liquidAlpha[7] -= 0.2f;
                            if (Main.liquidAlpha[7] < 0f)
                            {
                                Main.liquidAlpha[7] = 0f;
                            }

                            Main.liquidAlpha[8] -= 0.2f;
                            if (Main.liquidAlpha[8] < 0f)
                            {
                                Main.liquidAlpha[8] = 0f;
                            }

                            Main.liquidAlpha[9] -= 0.2f;
                            if (Main.liquidAlpha[9] < 0f)
                            {
                                Main.liquidAlpha[9] = 0f;
                            }

                            Main.liquidAlpha[10] -= 0.2f;
                            if (Main.liquidAlpha[10] < 0f)
                            {
                                Main.liquidAlpha[10] = 0f;
                            }

                            Main.liquidAlpha[4] += 0.2f;
                            if (Main.liquidAlpha[4] > 1f)
                            {
                                Main.liquidAlpha[4] = 1f;
                            }

                            break;
                        }
                    case 4:
                        {
                            Main.liquidAlpha[0] -= 0.2f;
                            if (Main.liquidAlpha[0] < 0f)
                            {
                                Main.liquidAlpha[0] = 0f;
                            }

                            Main.liquidAlpha[2] -= 0.2f;
                            if (Main.liquidAlpha[2] < 0f)
                            {
                                Main.liquidAlpha[2] = 0f;
                            }

                            Main.liquidAlpha[3] -= 0.2f;
                            if (Main.liquidAlpha[3] < 0f)
                            {
                                Main.liquidAlpha[3] = 0f;
                            }

                            Main.liquidAlpha[4] -= 0.2f;
                            if (Main.liquidAlpha[4] < 0f)
                            {
                                Main.liquidAlpha[4] = 0f;
                            }

                            Main.liquidAlpha[6] -= 0.2f;
                            if (Main.liquidAlpha[6] < 0f)
                            {
                                Main.liquidAlpha[6] = 0f;
                            }

                            Main.liquidAlpha[7] -= 0.2f;
                            if (Main.liquidAlpha[7] < 0f)
                            {
                                Main.liquidAlpha[7] = 0f;
                            }

                            Main.liquidAlpha[8] -= 0.2f;
                            if (Main.liquidAlpha[8] < 0f)
                            {
                                Main.liquidAlpha[8] = 0f;
                            }

                            Main.liquidAlpha[9] -= 0.2f;
                            if (Main.liquidAlpha[9] < 0f)
                            {
                                Main.liquidAlpha[9] = 0f;
                            }

                            Main.liquidAlpha[10] -= 0.2f;
                            if (Main.liquidAlpha[10] < 0f)
                            {
                                Main.liquidAlpha[10] = 0f;
                            }

                            Main.liquidAlpha[5] += 0.2f;
                            if (Main.liquidAlpha[5] > 1f)
                            {
                                Main.liquidAlpha[5] = 1f;
                            }

                            break;
                        }
                    case 5:
                        {
                            Main.liquidAlpha[0] -= 0.2f;
                            if (Main.liquidAlpha[0] < 0f)
                            {
                                Main.liquidAlpha[0] = 0f;
                            }

                            Main.liquidAlpha[2] -= 0.2f;
                            if (Main.liquidAlpha[2] < 0f)
                            {
                                Main.liquidAlpha[2] = 0f;
                            }

                            Main.liquidAlpha[3] -= 0.2f;
                            if (Main.liquidAlpha[3] < 0f)
                            {
                                Main.liquidAlpha[3] = 0f;
                            }

                            Main.liquidAlpha[4] -= 0.2f;
                            if (Main.liquidAlpha[4] < 0f)
                            {
                                Main.liquidAlpha[4] = 0f;
                            }

                            Main.liquidAlpha[5] -= 0.2f;
                            if (Main.liquidAlpha[5] < 0f)
                            {
                                Main.liquidAlpha[5] = 0f;
                            }

                            Main.liquidAlpha[7] -= 0.2f;
                            if (Main.liquidAlpha[7] < 0f)
                            {
                                Main.liquidAlpha[7] = 0f;
                            }

                            Main.liquidAlpha[8] -= 0.2f;
                            if (Main.liquidAlpha[8] < 0f)
                            {
                                Main.liquidAlpha[8] = 0f;
                            }

                            Main.liquidAlpha[9] -= 0.2f;
                            if (Main.liquidAlpha[9] < 0f)
                            {
                                Main.liquidAlpha[9] = 0f;
                            }

                            Main.liquidAlpha[10] -= 0.2f;
                            if (Main.liquidAlpha[10] < 0f)
                            {
                                Main.liquidAlpha[10] = 0f;
                            }

                            Main.liquidAlpha[6] += 0.2f;
                            if (Main.liquidAlpha[6] > 1f)
                            {
                                Main.liquidAlpha[6] = 1f;
                            }

                            break;
                        }
                    case 6:
                        {
                            Main.liquidAlpha[0] -= 0.2f;
                            if (Main.liquidAlpha[0] < 0f)
                            {
                                Main.liquidAlpha[0] = 0f;
                            }

                            Main.liquidAlpha[2] -= 0.2f;
                            if (Main.liquidAlpha[2] < 0f)
                            {
                                Main.liquidAlpha[2] = 0f;
                            }

                            Main.liquidAlpha[3] -= 0.2f;
                            if (Main.liquidAlpha[3] < 0f)
                            {
                                Main.liquidAlpha[3] = 0f;
                            }

                            Main.liquidAlpha[4] -= 0.2f;
                            if (Main.liquidAlpha[4] < 0f)
                            {
                                Main.liquidAlpha[4] = 0f;
                            }

                            Main.liquidAlpha[5] -= 0.2f;
                            if (Main.liquidAlpha[5] < 0f)
                            {
                                Main.liquidAlpha[5] = 0f;
                            }

                            Main.liquidAlpha[6] -= 0.2f;
                            if (Main.liquidAlpha[6] < 0f)
                            {
                                Main.liquidAlpha[6] = 0f;
                            }

                            Main.liquidAlpha[8] -= 0.2f;
                            if (Main.liquidAlpha[8] < 0f)
                            {
                                Main.liquidAlpha[8] = 0f;
                            }

                            Main.liquidAlpha[9] -= 0.2f;
                            if (Main.liquidAlpha[9] < 0f)
                            {
                                Main.liquidAlpha[9] = 0f;
                            }

                            Main.liquidAlpha[10] -= 0.2f;
                            if (Main.liquidAlpha[10] < 0f)
                            {
                                Main.liquidAlpha[10] = 0f;
                            }

                            Main.liquidAlpha[7] += 0.2f;
                            if (Main.liquidAlpha[7] > 1f)
                            {
                                Main.liquidAlpha[7] = 1f;
                            }

                            break;
                        }
                    case 7:
                        {
                            Main.liquidAlpha[0] -= 0.2f;
                            if (Main.liquidAlpha[0] < 0f)
                            {
                                Main.liquidAlpha[0] = 0f;
                            }

                            Main.liquidAlpha[2] -= 0.2f;
                            if (Main.liquidAlpha[2] < 0f)
                            {
                                Main.liquidAlpha[2] = 0f;
                            }

                            Main.liquidAlpha[3] -= 0.2f;
                            if (Main.liquidAlpha[3] < 0f)
                            {
                                Main.liquidAlpha[3] = 0f;
                            }

                            Main.liquidAlpha[4] -= 0.2f;
                            if (Main.liquidAlpha[4] < 0f)
                            {
                                Main.liquidAlpha[4] = 0f;
                            }

                            Main.liquidAlpha[5] -= 0.2f;
                            if (Main.liquidAlpha[5] < 0f)
                            {
                                Main.liquidAlpha[5] = 0f;
                            }

                            Main.liquidAlpha[6] -= 0.2f;
                            if (Main.liquidAlpha[6] < 0f)
                            {
                                Main.liquidAlpha[6] = 0f;
                            }

                            Main.liquidAlpha[7] -= 0.2f;
                            if (Main.liquidAlpha[7] < 0f)
                            {
                                Main.liquidAlpha[7] = 0f;
                            }

                            Main.liquidAlpha[9] -= 0.2f;
                            if (Main.liquidAlpha[9] < 0f)
                            {
                                Main.liquidAlpha[9] = 0f;
                            }

                            Main.liquidAlpha[10] -= 0.2f;
                            if (Main.liquidAlpha[10] < 0f)
                            {
                                Main.liquidAlpha[10] = 0f;
                            }

                            Main.liquidAlpha[8] += 0.2f;
                            if (Main.liquidAlpha[8] > 1f)
                            {
                                Main.liquidAlpha[8] = 1f;
                            }

                            break;
                        }
                    case 8:
                        {
                            Main.liquidAlpha[0] -= 0.2f;
                            if (Main.liquidAlpha[0] < 0f)
                            {
                                Main.liquidAlpha[0] = 0f;
                            }

                            Main.liquidAlpha[2] -= 0.2f;
                            if (Main.liquidAlpha[2] < 0f)
                            {
                                Main.liquidAlpha[2] = 0f;
                            }

                            Main.liquidAlpha[3] -= 0.2f;
                            if (Main.liquidAlpha[3] < 0f)
                            {
                                Main.liquidAlpha[3] = 0f;
                            }

                            Main.liquidAlpha[4] -= 0.2f;
                            if (Main.liquidAlpha[4] < 0f)
                            {
                                Main.liquidAlpha[4] = 0f;
                            }

                            Main.liquidAlpha[5] -= 0.2f;
                            if (Main.liquidAlpha[5] < 0f)
                            {
                                Main.liquidAlpha[5] = 0f;
                            }

                            Main.liquidAlpha[6] -= 0.2f;
                            if (Main.liquidAlpha[6] < 0f)
                            {
                                Main.liquidAlpha[6] = 0f;
                            }

                            Main.liquidAlpha[7] -= 0.2f;
                            if (Main.liquidAlpha[7] < 0f)
                            {
                                Main.liquidAlpha[7] = 0f;
                            }

                            Main.liquidAlpha[8] -= 0.2f;
                            if (Main.liquidAlpha[8] < 0f)
                            {
                                Main.liquidAlpha[8] = 0f;
                            }

                            Main.liquidAlpha[10] -= 0.2f;
                            if (Main.liquidAlpha[10] < 0f)
                            {
                                Main.liquidAlpha[10] = 0f;
                            }

                            Main.liquidAlpha[9] += 0.2f;
                            if (Main.liquidAlpha[9] > 1f)
                            {
                                Main.liquidAlpha[9] = 1f;
                            }

                            break;
                        }
                    case 9:
                        {
                            Main.liquidAlpha[0] -= 0.2f;
                            if (Main.liquidAlpha[0] < 0f)
                            {
                                Main.liquidAlpha[0] = 0f;
                            }

                            Main.liquidAlpha[2] -= 0.2f;
                            if (Main.liquidAlpha[2] < 0f)
                            {
                                Main.liquidAlpha[2] = 0f;
                            }

                            Main.liquidAlpha[3] -= 0.2f;
                            if (Main.liquidAlpha[3] < 0f)
                            {
                                Main.liquidAlpha[3] = 0f;
                            }

                            Main.liquidAlpha[4] -= 0.2f;
                            if (Main.liquidAlpha[4] < 0f)
                            {
                                Main.liquidAlpha[4] = 0f;
                            }

                            Main.liquidAlpha[5] -= 0.2f;
                            if (Main.liquidAlpha[5] < 0f)
                            {
                                Main.liquidAlpha[5] = 0f;
                            }

                            Main.liquidAlpha[6] -= 0.2f;
                            if (Main.liquidAlpha[6] < 0f)
                            {
                                Main.liquidAlpha[6] = 0f;
                            }

                            Main.liquidAlpha[7] -= 0.2f;
                            if (Main.liquidAlpha[7] < 0f)
                            {
                                Main.liquidAlpha[7] = 0f;
                            }

                            Main.liquidAlpha[8] -= 0.2f;
                            if (Main.liquidAlpha[8] < 0f)
                            {
                                Main.liquidAlpha[8] = 0f;
                            }

                            Main.liquidAlpha[9] -= 0.2f;
                            if (Main.liquidAlpha[9] < 0f)
                            {
                                Main.liquidAlpha[9] = 0f;
                            }

                            Main.liquidAlpha[10] += 0.2f;
                            if (Main.liquidAlpha[10] > 1f)
                            {
                                Main.liquidAlpha[10] = 1f;
                            }

                            break;
                        }
                }

                WaterStyleLoader.UpdateLiquidAlphas();
            }

            Main.drewLava = false;
            if (!Main.drawToScreen)
            {
                if ((!bg ^ styleOverride != -1) && allowUpdate)
                {
                    Vector2 value = Main.drawToScreen ? Vector2.Zero : new Vector2((float)Main.offScreenRange, (float)Main.offScreenRange);
                    int num = (int)((Main.screenPosition.X - value.X) / 16f - 1f);
                    int num2 = (int)((Main.screenPosition.X + (float)Main.screenWidth + value.X) / 16f) + 2;
                    int num3 = (int)((Main.screenPosition.Y - value.Y) / 16f - 1f);
                    int num4 = (int)((Main.screenPosition.Y + (float)Main.screenHeight + value.Y) / 16f) + 5;
                    value -= Main.screenPosition;
                    num = Math.Max(num, 5) - 2;
                    num3 = Math.Max(num3, 5);
                    num2 = Math.Min(num2, Main.maxTilesX - 5) + 2;
                    num4 = Math.Min(num4, Main.maxTilesY - 5) + 4;
                    Microsoft.Xna.Framework.Rectangle drawArea = new Microsoft.Xna.Framework.Rectangle(num, num3, num2 - num, num4 - num3);
                    LiquidRenderer.Instance.PrepareDraw(drawArea);
                }

                if (styleOverride != -1)
                {
                    DrawWater(bg, styleOverride, 1f);
                    return;
                }

                for (int i = 0; i < WaterStyleLoader.WaterStyleCount; i++)
                {
                    if (i != 1 && i != 11 && Main.liquidAlpha[i] > 0f)
                    {
                        DrawWater(bg, i, Main.liquidAlpha[i]);
                    }
                }

                return;
            }
            else
            {
                if (styleOverride != -1)
                {
                    DrawWater(bg, styleOverride, 1f);
                    return;
                }

                if (Main.liquidAlpha[0] > 0f)
                {
                    if (bg)
                    {
                        DrawWater(bg, 0, 1f);
                    }
                    else
                    {
                        DrawWater(bg, 0, Main.liquidAlpha[0]);
                    }
                }

                if (Main.liquidAlpha[2] > 0f)
                {
                    if (bg)
                    {
                        if (Main.waterStyle < 1)
                        {
                            DrawWater(bg, 1, Main.liquidAlpha[2]);
                        }
                        else
                        {
                            DrawWater(bg, 1, 1f);
                        }
                    }
                    else
                    {
                        DrawWater(bg, 1, Main.liquidAlpha[2]);
                    }
                }

                if (Main.liquidAlpha[3] > 0f)
                {
                    if (bg)
                    {
                        if (Main.waterStyle < 2)
                        {
                            DrawWater(bg, 2, Main.liquidAlpha[3]);
                        }
                        else
                        {
                            DrawWater(bg, 2, 1f);
                        }
                    }
                    else
                    {
                        DrawWater(bg, 2, Main.liquidAlpha[3]);
                    }
                }

                if (Main.liquidAlpha[4] > 0f)
                {
                    if (bg)
                    {
                        if (Main.waterStyle < 3)
                        {
                            DrawWater(bg, 3, Main.liquidAlpha[4]);
                        }
                        else
                        {
                            DrawWater(bg, 3, 1f);
                        }
                    }
                    else
                    {
                        DrawWater(bg, 3, Main.liquidAlpha[4]);
                    }
                }

                if (Main.liquidAlpha[5] > 0f)
                {
                    if (bg)
                    {
                        if (Main.waterStyle < 4)
                        {
                            DrawWater(bg, 4, Main.liquidAlpha[5]);
                        }
                        else
                        {
                            DrawWater(bg, 4, 1f);
                        }
                    }
                    else
                    {
                        DrawWater(bg, 4, Main.liquidAlpha[5]);
                    }
                }

                if (Main.liquidAlpha[6] > 0f)
                {
                    if (bg)
                    {
                        if (Main.waterStyle < 5)
                        {
                            DrawWater(bg, 5, Main.liquidAlpha[6]);
                        }
                        else
                        {
                            DrawWater(bg, 5, 1f);
                        }
                    }
                    else
                    {
                        DrawWater(bg, 5, Main.liquidAlpha[6]);
                    }
                }

                if (Main.liquidAlpha[7] > 0f)
                {
                    if (bg)
                    {
                        if (Main.waterStyle < 6)
                        {
                            DrawWater(bg, 6, Main.liquidAlpha[7]);
                        }
                        else
                        {
                            DrawWater(bg, 6, 1f);
                        }
                    }
                    else
                    {
                        DrawWater(bg, 6, Main.liquidAlpha[7]);
                    }
                }

                if (Main.liquidAlpha[8] > 0f)
                {
                    if (bg)
                    {
                        if (Main.waterStyle < 7)
                        {
                            DrawWater(bg, 7, Main.liquidAlpha[8]);
                        }
                        else
                        {
                            DrawWater(bg, 7, 1f);
                        }
                    }
                    else
                    {
                        DrawWater(bg, 7, Main.liquidAlpha[8]);
                    }
                }

                if (Main.liquidAlpha[9] > 0f)
                {
                    if (bg)
                    {
                        if (Main.waterStyle < 8)
                        {
                            DrawWater(bg, 8, Main.liquidAlpha[9]);
                        }
                        else
                        {
                            DrawWater(bg, 8, 1f);
                        }
                    }
                    else
                    {
                        DrawWater(bg, 8, Main.liquidAlpha[9]);
                    }
                }

                if (Main.liquidAlpha[10] > 0f)
                {
                    if (bg)
                    {
                        if (Main.waterStyle < 9)
                        {
                            DrawWater(bg, 9, Main.liquidAlpha[10]);
                        }
                        else
                        {
                            DrawWater(bg, 9, 1f);
                        }
                    }
                    else
                    {
                        DrawWater(bg, 9, Main.liquidAlpha[10]);
                    }
                }

                WaterStyleLoader.DrawWatersToScreen(bg);
                return;
            }
        }

        private static void OldWaterDraw(On.Terraria.Main.orig_oldDrawWater orig, Terraria.Main self,
            bool bg = false, int Style = 0, float Alpha = 1f)
        {
            MainOnOldDrawWater(self, bg, Style, Alpha);
        }

        private static void MainOnOldDrawWater(Terraria.Main self,
            bool bg = false, int Style = 0, float Alpha = 1f)
        {
            Texture2D liquidTexture = OldWaterTexture[Style];
            if (Style > 1)
                Style--;

            float num = 0f;
            float num2 = 99999f;
            float num3 = 99999f;
            int num4 = -1;
            int num5 = -1;
            Vector2 zero = new Vector2((float)Main.offScreenRange, (float)Main.offScreenRange);
            if (Main.drawToScreen)
            {
                zero = Vector2.Zero;
            }

            int num6 = (int)(255f * (1f - Main.gfxQuality) + 40f * Main.gfxQuality);
            float arg_5D_0 = Main.gfxQuality;
            float arg_63_0 = Main.gfxQuality;
            int num7 = (int)((Main.screenPosition.X - zero.X) / 16f - 1f);
            int num8 = (int)((Main.screenPosition.X + (float)Main.screenWidth + zero.X) / 16f) + 2;
            int num9 = (int)((Main.screenPosition.Y - zero.Y) / 16f - 1f);
            int num10 = (int)((Main.screenPosition.Y + (float)Main.screenHeight + zero.Y) / 16f) + 5;
            if (num7 < 5)
            {
                num7 = 5;
            }

            if (num8 > Main.maxTilesX - 5)
            {
                num8 = Main.maxTilesX - 5;
            }

            if (num9 < 5)
            {
                num9 = 5;
            }

            if (num10 > Main.maxTilesY - 5)
            {
                num10 = Main.maxTilesY - 5;
            }

            for (int i = num9; i < num10 + 4; i++)
            {
                for (int j = num7 - 2; j < num8 + 2; j++)
                {
                    LiquidRef liquid = LiquidWorld.grid[j, i];
                    LiquidRef liquidUp = LiquidWorld.grid[j, i + 1];
                    LiquidRef liquidDown = LiquidWorld.grid[j, i - 1];

                    if (liquid.Amount > 0 &&
                        (!liquid.Tile.nactive() || !Main.tileSolid[liquid.Tile.type] ||
                         Main.tileSolidTop[liquid.Tile.type]) && (Lighting.Brightness(j, i) > 0f || bg))
                    {
                        Microsoft.Xna.Framework.Color color = Lighting.GetColor(j, i);
                        float num11 = (float)(256 - (int)liquid.Amount);
                        num11 /= 32f;
                        int num12 = 0;
                        if (liquid.TypeID == LiquidID.Lava)
                        {
                            if (Main.drewLava)
                            {
                                goto IL_E7F;
                            }

                            float num13 = Math.Abs(j * 16 + 8 - (Main.screenPosition.X + (Main.screenWidth / 2)));
                            float num14 = Math.Abs(i * 16 + 8 - (Main.screenPosition.Y + (Main.screenHeight / 2)));
                            if (num13 < (Main.screenWidth * 2) && num14 < (Main.screenHeight * 2))
                            {
                                float num15 = (float)Math.Sqrt(num13 * num13 + num14 * num14);
                                float num16 = 1f - num15 / (Main.screenWidth * 0.75f);
                                if (num16 > 0f)
                                {
                                    num += num16;
                                }
                            }

                            if (num13 < num2)
                            {
                                num2 = num13;
                                num4 = j * 16 + 8;
                            }

                            if (num14 < num3)
                            {
                                num3 = num13;
                                num5 = i * 16 + 8;
                            }

                            liquidTexture = OldLavaTexture[lavaStyle];
                        }
                        else if (liquid.TypeID == LiquidID.Honey)
                        {
                            liquidTexture = OldHoneyTexture[honeyStyle];
                        }
                        else if (liquid.TypeID != LiquidID.Water)
                        {
                            liquidTexture = LiquidRegistry.liquidList[liquid.TypeID].OldTexture;
                        }


                        if (liquid.TypeID == LiquidID.Water)
                        {
                            num12 = Style;
                            liquidTexture = OldWaterTexture[num12];

                        }

                        if (num12 <= 10 || !Main.drewLava)
                        {
                            float num17 = 0.5f;
                            if (bg)
                            {
                                num17 = 1f;
                            }

                            if (num12 <= 10)
                            {
                                num17 *= Alpha;
                            }

                            Vector2 value = new Vector2((float)(j * 16), (float)(i * 16 + (int)num11 * 2));
                            Microsoft.Xna.Framework.Rectangle value2 =
                                new Microsoft.Xna.Framework.Rectangle(0, 0, 16, 16 - (int)num11 * 2);

                            if (liquidUp.Amount < 245 &&
                                (!liquidUp.Tile.nactive() || !Main.tileSolid[liquidUp.Tile.type] ||
                                 Main.tileSolidTop[liquidUp.Tile.type]))
                            {
                                float num18 = (float)(256 - (int)liquidUp.Amount);
                                num18 /= 32f;
                                num17 = 0.5f * (8f - num11) / 4f;
                                if ((double)num17 > 0.55)
                                {
                                    num17 = 0.55f;
                                }

                                if ((double)num17 < 0.35)
                                {
                                    num17 = 0.35f;
                                }

                                float num19 = num11 / 2f;
                                if (liquidUp.Amount < 200)
                                {
                                    if (bg)
                                    {
                                        goto IL_E7F;
                                    }

                                    if (liquidDown.Amount > 0 && liquidDown.Amount > 0)
                                    {
                                        value2 = new Microsoft.Xna.Framework.Rectangle(0, 4, 16, 16);
                                        num17 = 0.5f;
                                    }
                                    else if (liquidDown.Amount > 0)
                                    {
                                        value = new Vector2((float)(j * 16), (float)(i * 16 + 4));
                                        value2 = new Microsoft.Xna.Framework.Rectangle(0, 4, 16, 12);
                                        num17 = 0.5f;
                                    }
                                    else if (liquidUp.Amount > 0)
                                    {
                                        value = new Vector2((float)(j * 16),
                                            (float)(i * 16 + (int)num11 * 2 + (int)num18 * 2));
                                        value2 = new Microsoft.Xna.Framework.Rectangle(0, 4, 16, 16 - (int)num11 * 2);
                                    }
                                    else
                                    {
                                        value = new Vector2((float)(j * 16 + (int)num19),
                                            (float)(i * 16 + (int)num19 * 2 + (int)num18 * 2));
                                        value2 = new Microsoft.Xna.Framework.Rectangle(0, 4, 16 - (int)num19 * 2,
                                            16 - (int)num19 * 2);
                                    }
                                }
                                else
                                {
                                    num17 = 0.5f;
                                    value2 = new Microsoft.Xna.Framework.Rectangle(0, 4, 16,
                                        16 - (int)num11 * 2 + (int)num18 * 2);
                                }
                            }
                            else if (liquidDown.Amount > 32)
                            {
                                value2 = new Microsoft.Xna.Framework.Rectangle(0, 4, value2.Width, value2.Height);
                            }
                            else if (num11 < 1f && liquidDown.Tile.nactive() &&
                                     Main.tileSolid[liquidDown.Tile.type] &&
                                     !Main.tileSolidTop[liquidDown.Tile.type])
                            {
                                value = new Vector2((float)(j * 16), (float)(i * 16));
                                value2 = new Microsoft.Xna.Framework.Rectangle(0, 4, 16, 16);
                            }
                            else
                            {
                                bool flag = true;
                                int num20 = i + 1;
                                while (num20 < i + 6 && (!LiquidWorld.grid[j, num20].Tile.nactive() || !Main.tileSolid[LiquidWorld.grid[j, num20].Tile.type] || Main.tileSolidTop[LiquidWorld.grid[j, num20].Tile.type]))
                                {
                                    if (LiquidWorld.grid[j, num20].Amount < 200)
                                    {
                                        flag = false;
                                        break;
                                    }

                                    num20++;
                                }

                                if (!flag)
                                {
                                    num17 = 0.5f;
                                    value2 = new Microsoft.Xna.Framework.Rectangle(0, 4, 16, 16);
                                }
                                else if (liquidDown.HasLiquid)
                                {
                                    value2 = new Microsoft.Xna.Framework.Rectangle(0, 2, value2.Width, value2.Height);
                                }
                            }

                            if ((color.R > 20 || color.B > 20 || color.G > 20) && value2.Y < 4)
                            {
                                int num21 = (int)color.R;
                                if ((int)color.G > num21)
                                {
                                    num21 = (int)color.G;
                                }

                                if ((int)color.B > num21)
                                {
                                    num21 = (int)color.B;
                                }

                                num21 /= 30;
                                if (Main.rand.Next(20000) < num21)
                                {
                                    Microsoft.Xna.Framework.Color newColor =
                                        new Microsoft.Xna.Framework.Color(255, 255, 255);
                                    if (liquid.TypeID == LiquidID.Honey)
                                    {
                                        newColor = new Microsoft.Xna.Framework.Color(255, 255, 50);
                                    }

                                    int num22 = Dust.NewDust(new Vector2((float)(j * 16), value.Y - 2f), 16, 8, 43, 0f,
                                        0f, 254, newColor, 0.75f);
                                    Main.dust[num22].velocity *= 0f;
                                }
                            }

                            if (liquid.TypeID == LiquidID.Honey)
                            {
                                num17 *= 1.6f;
                                if (num17 > 1f)
                                {
                                    num17 = 1f;
                                }
                            }

                            if (liquid.TypeID == LiquidID.Lava)
                            {
                                num17 *= 1.8f;
                                if (num17 > 1f)
                                {
                                    num17 = 1f;
                                }

                                if (self.IsActive && !Main.gamePaused && Dust.lavaBubbles < 200)
                                {
                                    if (liquid.Amount > 200 && Main.rand.Next(700) == 0)
                                    {
                                        Dust.NewDust(new Vector2((float)(j * 16), (float)(i * 16)), 16, 16, 35, 0f,
                                            0f, 0, default(Microsoft.Xna.Framework.Color), 1f);
                                    }

                                    if (value2.Y == 0 && Main.rand.Next(350) == 0)
                                    {
                                        int num23 = Dust.NewDust(
                                            new Vector2((float)(j * 16), (float)(i * 16) + num11 * 2f - 8f), 16, 8,
                                            35, 0f, 0f, 50, default(Microsoft.Xna.Framework.Color), 1.5f);
                                        Main.dust[num23].velocity *= 0.8f;
                                        Dust expr_9CA_cp_0 = Main.dust[num23];
                                        expr_9CA_cp_0.velocity.X = expr_9CA_cp_0.velocity.X * 2f;
                                        Dust expr_9E8_cp_0 = Main.dust[num23];
                                        expr_9E8_cp_0.velocity.Y =
                                            expr_9E8_cp_0.velocity.Y - (float)Main.rand.Next(1, 7) * 0.1f;
                                        if (Main.rand.Next(10) == 0)
                                        {
                                            Dust expr_A22_cp_0 = Main.dust[num23];
                                            expr_A22_cp_0.velocity.Y =
                                                expr_A22_cp_0.velocity.Y * (float)Main.rand.Next(2, 5);
                                        }

                                        Main.dust[num23].noGravity = true;
                                    }
                                }
                            }

                            float num24 = (float)color.R * num17;
                            float num25 = (float)color.G * num17;
                            float num26 = (float)color.B * num17;
                            float num27 = (float)color.A * num17;
                            color = new Microsoft.Xna.Framework.Color((int)((byte)num24), (int)((byte)num25),
                                (int)((byte)num26), (int)((byte)num27));
                            if (Lighting.NotRetro && !bg)
                            {
                                Microsoft.Xna.Framework.Color color2 = color;
                                if (((double)color2.R > (double)num6 * 0.6 ||
                                     (double)color2.G > (double)num6 * 0.65 ||
                                     (double)color2.B > (double)num6 * 0.7))
                                {
                                    for (int k = 0; k < 4; k++)
                                    {
                                        int num28 = 0;
                                        int num29 = 0;
                                        int width = 8;
                                        int height = 8;
                                        Microsoft.Xna.Framework.Color color3 = color2;
                                        Microsoft.Xna.Framework.Color color4 = Lighting.GetColor(j, i);
                                        if (k == 0)
                                        {
                                            color4 = Lighting.GetColor(j - 1, i - 1);
                                            if (value2.Height < 8)
                                            {
                                                height = value2.Height;
                                            }
                                        }

                                        if (k == 1)
                                        {
                                            color4 = Lighting.GetColor(j + 1, i - 1);
                                            num28 = 8;
                                            if (value2.Height < 8)
                                            {
                                                height = value2.Height;
                                            }
                                        }

                                        if (k == 2)
                                        {
                                            color4 = Lighting.GetColor(j - 1, i + 1);
                                            num29 = 8;
                                            height = 8 - (16 - value2.Height);
                                        }

                                        if (k == 3)
                                        {
                                            color4 = Lighting.GetColor(j + 1, i + 1);
                                            num28 = 8;
                                            num29 = 8;
                                            height = 8 - (16 - value2.Height);
                                        }

                                        num24 = (float)color4.R * num17;
                                        num25 = (float)color4.G * num17;
                                        num26 = (float)color4.B * num17;
                                        num27 = (float)color4.A * num17;
                                        color4 = new Microsoft.Xna.Framework.Color((int)((byte)num24),
                                            (int)((byte)num25), (int)((byte)num26), (int)((byte)num27));
                                        color3.R = (byte)((color2.R * 3 + color4.R * 2) / 5);
                                        color3.G = (byte)((color2.G * 3 + color4.G * 2) / 5);
                                        color3.B = (byte)((color2.B * 3 + color4.B * 2) / 5);
                                        color3.A = (byte)((color2.A * 3 + color4.A * 2) / 5);
                                        Main.spriteBatch.Draw(liquidTexture,
                                            value - Main.screenPosition + new Vector2((float)num28, (float)num29) +
                                            zero,
                                            new Microsoft.Xna.Framework.Rectangle?(
                                                new Microsoft.Xna.Framework.Rectangle(value2.X + num28,
                                                    value2.Y + num29, width, height)), color3, 0f, default(Vector2), 1f,
                                            SpriteEffects.None, 0f);
                                    }
                                }
                                else
                                {
                                    Main.spriteBatch.Draw(liquidTexture, value - Main.screenPosition + zero,
                                        new Microsoft.Xna.Framework.Rectangle?(value2), color, 0f, default(Vector2), 1f,
                                        SpriteEffects.None, 0f);
                                }
                            }
                            else
                            {
                                if (value2.Y < 4)
                                {
                                    value2.X += (int)(Main.wFrame * 18f);
                                }

                                Main.spriteBatch.Draw(liquidTexture, value - Main.screenPosition + zero,
                                    new Microsoft.Xna.Framework.Rectangle?(value2), color, 0f, default(Vector2), 1f,
                                    SpriteEffects.None, 0f);
                            }

                            if (liquidUp.Tile.halfBrick())
                            {
                                color = Lighting.GetColor(j, i + 1);
                                num24 = (float)color.R * num17;
                                num25 = (float)color.G * num17;
                                num26 = (float)color.B * num17;
                                num27 = (float)color.A * num17;
                                color = new Microsoft.Xna.Framework.Color((int)((byte)num24), (int)((byte)num25),
                                    (int)((byte)num26), (int)((byte)num27));
                                value = new Vector2((float)(j * 16), (float)(i * 16 + 16));
                                Main.spriteBatch.Draw(liquidTexture, value - Main.screenPosition + zero,
                                    new Microsoft.Xna.Framework.Rectangle?(
                                        new Microsoft.Xna.Framework.Rectangle(0, 4, 16, 8)), color, 0f,
                                    default(Vector2), 1f, SpriteEffects.None, 0f);
                            }
                        }
                    }

                IL_E7F:;
                }
            }

            if (!Main.drewLava)
            {
                Main.ambientLavaX = (float)num4;
                Main.ambientLavaY = (float)num5;
                Main.ambientLavaStrength = num;
            }

            Main.drewLava = true;
        }


        public static void DrawWaterSlope(Vector2 value, int i, int j, int waterStyleOverride = -1)
        {
            LiquidRef tile = LiquidWorld.grid[j, i];

            if (tile.TypeID == 255)
            {
                return;
            }
            LiquidRef tile2 = LiquidWorld.grid[j + 1, i];
            LiquidRef tile3 = LiquidWorld.grid[j - 1, i];
            LiquidRef tile4 = LiquidWorld.grid[j, i - 1];
            LiquidRef tile5 = LiquidWorld.grid[j, i + 1];
            int num110 = 0;
            bool flag8 = false;
            bool flag9 = false;
            bool flag10 = false;
            bool flag11 = false;
            int num111 = 0;
            bool flag12 = false;
            int num112 = tile.Tile.slope();
            if (tile3.Amount > 0 && num112 != 1 && num112 != 3) {
                flag8 = true;
                switch (tile3.TypeID) {
                    case 0:
                        flag12 = true;
                        break;
                    case 1:
                        num111 = 1;
                        break;
                    case 2:
                        num111 = 11;
                        break;
                    default:
                        num111 = 9 + tile3.TypeID;
                        break;
                }

                if (tile3.Amount > num110)
                    num110 = tile3.Amount;
            }

            if (tile2.Amount > 0 && num112 != 2 && num112 != 4) {
                flag9 = true;
                switch (tile2.TypeID) {
                    case 0:
                        flag12 = true;
                        break;
                    case 1:
                        num111 = 1;
                        break;
                    case 2:
                        num111 = 11;
                        break;
                    default:
                        num111 = 9 + tile2.TypeID;
                        break;
                }

                if (tile2.Amount > num110)
                    num110 = tile2.Amount;
            }

            if (tile4.Amount > 0 && num112 != 3 && num112 != 4) {
                flag10 = true;
                switch (tile4.TypeID) {
                    case 0:
                        flag12 = true;
                        break;
                    case 1:
                        num111 = 1;
                        break;
                    case 2:
                        num111 = 11;
                        break;
                    default:
                        num111 = 9 + tile4.TypeID;
                        break;
                }
            }

            if (tile5.Amount > 0 && num112 != 1 && num112 != 2)
            {
                if (tile5.Amount > 240)
                    flag11 = true;

                switch (tile5.TypeID)
                {
                    case 0:
                        flag12 = true;
                        break;
                    case 1:
                        num111 = 1;
                        break;
                    case 2:
                        num111 = 11;
                        break;
                    default:
                        num111 = 9 + tile5.TypeID;
                        break;
                }
            }

            if (waterStyleOverride != -1)
                Main.waterStyle = waterStyleOverride;

            if (num111 == 0)
                num111 = Main.waterStyle;

            if ((flag10 || flag11 || flag8 || flag9) && (!flag12 || num111 != 1))
            {
                Microsoft.Xna.Framework.Color color7 = Lighting.GetColor(j, i);
                float num113 = 0f;
                Vector2 value4 = new Vector2(j * 16, i * 16);
                Microsoft.Xna.Framework.Rectangle value5 = new Microsoft.Xna.Framework.Rectangle(0, 4, 16, 16);
                if (flag11 && (flag8 || flag9))
                {
                    flag8 = true;
                    flag9 = true;
                }

                if ((!flag10 || !(flag8 || flag9)) && !(flag11 && flag10))
                {
                    if (flag10)
                    {
                        value5 = new Microsoft.Xna.Framework.Rectangle(0, 4, 16, 4);
                        if (tile.Tile.halfBrick() || tile.Tile.slope() != 0)
                            value5 = new Microsoft.Xna.Framework.Rectangle(0, 4, 16, 12);
                    }
                    else if (flag11 && !flag8 && !flag9)
                    {
                        value4 = new Vector2(j * 16, i * 16 + 12);
                        value5 = new Microsoft.Xna.Framework.Rectangle(0, 4, 16, 4);
                    }
                    else
                    {
                        num113 = 256 - num110;
                        num113 /= 32f;
                        int y = 4;
                        if (tile4.Amount == 0 && !WorldGen.SolidTile(j, i - 1))
                            y = 0;

                        if ((flag8 && flag9) || tile.Tile.halfBrick() || tile.Tile.slope() != 0)
                        {
                            value4 = new Vector2(j * 16, i * 16 + (int)num113 * 2);
                            value5 = new Microsoft.Xna.Framework.Rectangle(0, y, 16, 16 - (int)num113 * 2);
                        }
                        else if (!flag8)
                        {
                            value4 = new Vector2(j * 16 + 12, i * 16 + (int)num113 * 2);
                            value5 = new Microsoft.Xna.Framework.Rectangle(0, y, 4, 16 - (int)num113 * 2);
                        }
                        else
                        {
                            value4 = new Vector2(j * 16, i * 16 + (int)num113 * 2);
                            value5 = new Microsoft.Xna.Framework.Rectangle(0, y, 4, 16 - (int)num113 * 2);
                        }
                    }
                }

                float num114 = 0.5f;
                switch (num111)
                {
                    case 1:
                        num114 = 1f;
                        break;
                    case 11:
                        num114 *= 1.7f;
                        if (num114 > 1f)
                            num114 = 1f;
                        break;
                }

                if ((double)i < Main.worldSurface || num114 > 1f)
                {
                    num114 = 1f;
                    if (tile4.Tile.wall > 0 || tile3.Tile.wall > 0 || tile2.Tile.wall > 0 || tile5.Tile.wall > 0)
                        num114 = 0.65f;

                    if (tile.Tile.wall > 0)
                        num114 = 0.5f;
                }

                if (tile.Tile.halfBrick() && tile4.Tile.liquid > 0 && tile.Tile.wall > 0)
                    num114 = 0f;

                float num115 = (float)(int)color7.R * num114;
                float num116 = (float)(int)color7.G * num114;
                float num117 = (float)(int)color7.B * num114;
                float num118 = (float)(int)color7.A * num114;
                color7 = new Microsoft.Xna.Framework.Color((byte)num115, (byte)num116, (byte)num117, (byte)num118);
                if (num111 > 11)
                {
                    Main.spriteBatch.Draw(LiquidRegistry.liquidList[num111 - 9].OldTexture, value4 - Main.screenPosition + value, value5, color7, 0f, default(Vector2), 1f, SpriteEffects.None, 0f);
                }
                else
                {
                    Main.spriteBatch.Draw(Main.liquidTexture[num111], value4 - Main.screenPosition + value, value5, color7, 0f, default(Vector2), 1f, SpriteEffects.None, 0f);
                }
                
            }
        }

        private static bool SetTextureIndex(LiquidRef liquidRef, int num112, bool flag8, ref bool water, ref int textureIndex, ref int amount)
        {
            if (liquidRef.Amount > 0 && num112 != 1 && num112 != 3)
            {
                flag8 = true;
                switch (liquidRef.TypeID)
                {
                    case 0:
                        water = true;
                        break;
                    case 1:
                        textureIndex = 1;
                        break;
                    case 2:
                        textureIndex = 11;
                        break;
                    default:
                        textureIndex = 9 + liquidRef.TypeID;
                        break;
                }

                if (liquidRef.Amount > amount)
                    amount = liquidRef.Amount;
            }

            return flag8;
        }
    }
}