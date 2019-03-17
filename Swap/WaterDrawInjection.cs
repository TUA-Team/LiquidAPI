using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using LiquidAPI;
using LiquidAPI.LiquidMod;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.GameContent.Events;
using Terraria.GameContent.Tile_Entities;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Graphics.Capture;
using Terraria.ObjectData;
using Terraria.UI;

namespace LiquidAPI.Swap
{
    class WaterDrawInjection
    {

        public static void MethodSwap()
        {
	        //On.Terraria.Main.oldDrawWater += oldDrawWater;
	        //On.Terraria.Main.DrawTiles += DrawTiles;
        }

        //public static void oldDrawWater(On.Terraria.Main.orig_oldDrawWater orig, Main main, bool bg = false, int Style = 0, float Alpha = 1f)
        //{
        //    Vector2 zero = new Vector2((float)Main.offScreenRange, (float)Main.offScreenRange);
        //    if (Main.drawToScreen)
        //    {
        //        zero = Vector2.Zero;
        //    }

        //    float ambientLavaStrength = 0f;
        //    float num2 = 99999f;
        //    float num3 = 99999f;
        //    int num4 = -1;
        //    int num5 = -1;
        //    int num6 = (int)(255f * (1f - Main.gfxQuality) + 40f * Main.gfxQuality);
        //    int startingXPosition = (int)((Main.screenPosition.X - zero.X) / 16f - 1f);
        //    int maxXPosition = (int)((Main.screenPosition.X + (float)Main.screenWidth + zero.X) / 16f) + 2;
        //    int startingYPosition = (int)((Main.screenPosition.y - zero.y) / 16f - 1f);
        //    int maxYPosition = (int)((Main.screenPosition.y + (float)Main.screenHeight + zero.y) / 16f) + 5;
            
        //    if (startingXPosition < 5)
        //    {
        //        startingXPosition = 5;
        //    }
        //    if (maxXPosition > Main.maxTilesX - 5)
        //    {
        //        maxXPosition = Main.maxTilesX - 5;
        //    }
        //    if (startingYPosition < 5)
        //    {
        //        startingYPosition = 5;
        //    }
        //    if (maxYPosition > Main.maxTilesY - 5)
        //    {
        //        maxYPosition = Main.maxTilesY - 5;
        //    }
        //    for (int y = startingYPosition; y < maxYPosition + 4; y++)
        //    {
        //        for (int x = startingXPosition - 2; x < maxXPosition + 2; x++)
        //        {
        //            LiquidRef liquid = LiquidCore.grid[x, y];
        //            if (Main.Tile[x, y] == null)
        //            {
        //                Main.Tile[x, y] = new Tile();
        //            }
        //            if (!liquid.NoLiquid() && (!Main.Tile[x, y].nactive() || !Main.tileSolid[(int)Main.Tile[x, y].type] || Main.tileSolidTop[(int)Main.Tile[x, y].type]) && (Lighting.Brightness(x, y) > 0f || bg))
        //            {
        //                Microsoft.Xna.Framework.Color color = Lighting.GetColor(x, y);
                        
        //                float liquidLayer = (float)(256 - (int)Main.Tile[x, y].liquid);
        //                liquidLayer /= 32f;
        //                int textureIndex = 0;
        //                if (liquid.liquidType == 1)
        //                {
        //                    if (Main.drewLava)
        //                    {
        //                        goto IL_E7F;
        //                    }
        //                    float num13 = Math.Abs((float)(x * 16 + 8) - (Main.screenPosition.X + (float)(Main.screenWidth / 2)));
        //                    float num14 = Math.Abs((float)(y * 16 + 8) - (Main.screenPosition.y + (float)(Main.screenHeight / 2)));
        //                    if (num13 < (float)(Main.screenWidth * 2) && num14 < (float)(Main.screenHeight * 2))
        //                    {
        //                        float num15 = (float)Math.Sqrt((double)(num13 * num13 + num14 * num14));
        //                        float num16 = 1f - num15 / ((float)Main.screenWidth * 0.75f);
        //                        if (num16 > 0f)
        //                        {
        //                            ambientLavaStrength += num16;
        //                        }
        //                    }
        //                    if (num13 < num2)
        //                    {
        //                        num2 = num13;
        //                        num4 = x * 16 + 8;
        //                    }
        //                    if (num14 < num3)
        //                    {
        //                        num3 = num13;
        //                        num5 = y * 16 + 8;
        //                    }
        //                    textureIndex = 1;
        //                }
        //                //if liquid is honey, it give it's proper texture index, aka 12
        //                else if (liquid.liquidType == 2)
        //                {
        //                    textureIndex = 11;
        //                }
        //                if (textureIndex == 0)
        //                {
        //                    textureIndex = Style;
        //                }
                        
        //                for (byte b = 2; b < Main.liquidTexture.Length + 3; b = (byte)(b + 1))
        //                {
        //                    if (liquid.Liquids(b))
        //                    {
        //                        textureIndex = 9 + b;
        //                    }
        //                }
                        
        //                if ((textureIndex != 1 && textureIndex < 11) || !Main.drewLava)
        //                {
        //                    float Opacity = 0.5f;
        //                    if (bg)
        //                    {
        //                        Opacity = 2f;
        //                    }
        //                    if (textureIndex != 1 && textureIndex < 11)
        //                    {
        //                        Opacity *= Alpha;
        //                    }
        //                    Vector2 drawingPosition = new Vector2((float)(x * 16), (float)(y * 16 + (int)liquidLayer * 2));
        //                    Microsoft.Xna.Framework.Rectangle sourceRectangle = new Microsoft.Xna.Framework.Rectangle(0, 0, 16, 16 - (int)liquidLayer * 2);
        //                    if (LiquidCore.grid[x, y + 1].GetLiquidAmount() < 245 && (!Main.Tile[x, y + 1].nactive() || !Main.tileSolid[(int)Main.Tile[x, y + 1].type] || Main.tileSolidTop[(int)Main.Tile[x, y + 1].type]))
        //                    {
        //                        float num18 = (float)(256 - (int)Main.Tile[x, y + 1].liquid);
        //                        num18 /= 32f;
        //                        Opacity = 0.5f * (8f - liquidLayer) / 4f;
        //                        if ((double)Opacity > 0.55)
        //                        {
        //                            Opacity = 0.55f;
        //                        }
        //                        if ((double)Opacity < 0.35)
        //                        {
        //                            Opacity = 0.35f;
        //                        }
        //                        float num19 = liquidLayer / 2f;
        //                        if (LiquidCore.grid[x, y + 1].GetLiquidAmount() < 200)
        //                        {
        //                            if (bg)
        //                            {
        //                                goto IL_E7F;
        //                            }
        //                            /*
        //                            if (Main.Tile[x, y - 1].liquid > 0 && Main.Tile[x, y - 1].liquid > 0)
        //                            {
        //                                value2 = new Microsoft.Xna.Framework.Rectangle(16, 4, 16, 16);
        //                                Opacity = 0.5f;
        //                            }
        //                            else*/
        //                            if (LiquidCore.grid[x, y - 1].GetLiquidAmount() > 0)
        //                            {
        //                                drawingPosition = new Vector2((float)(x * 16), (float)(y * 16 + 4));
        //                                sourceRectangle = new Microsoft.Xna.Framework.Rectangle(0, 4, 16, 12);
        //                                Opacity = 0.5f;
        //                            }
        //                            else if (LiquidCore.grid[x, y + 1].GetLiquidAmount() > 0)
        //                            {
        //                                drawingPosition = new Vector2((float)(x * 16), (float)(y * 16 + (int)liquidLayer * 2 + (int)num18 * 2));
        //                                sourceRectangle = new Microsoft.Xna.Framework.Rectangle(0, 4, 16, 16 - (int)liquidLayer * 2);
        //                            }
        //                            else
        //                            {
        //                                drawingPosition = new Vector2((float)(x * 16 + (int)num19), (float)(y * 16 + (int)num19 * 2 + (int)num18 * 2));
        //                                sourceRectangle = new Microsoft.Xna.Framework.Rectangle(0, 4, 16 - (int)num19 * 2, 16 - (int)num19 * 2);
        //                            }
        //                        }
        //                        else
        //                        {
        //                            Opacity = 0.5f;
        //                            sourceRectangle = new Microsoft.Xna.Framework.Rectangle(0, 4, 16, 16 - (int)liquidLayer * 2 + (int)num18 * 2);
        //                        }
        //                    }
        //                    else if (Main.Tile[x, y - 1].liquid > 32)
        //                    {
        //                        sourceRectangle = new Microsoft.Xna.Framework.Rectangle(0, 4, sourceRectangle.Width, sourceRectangle.Height);
        //                    }
        //                    else if (liquidLayer < 1f && Main.Tile[x, y - 1].nactive() && Main.tileSolid[(int)Main.Tile[x, y - 1].type] && !Main.tileSolidTop[(int)Main.Tile[x, y - 1].type])
        //                    {
        //                        drawingPosition = new Vector2((float)(x * 16), (float)(y * 16));
        //                        sourceRectangle = new Microsoft.Xna.Framework.Rectangle(0, 4, 16, 16);
        //                    }
        //                    else
        //                    {
        //                        bool flag = true;
        //                        int num20 = y + 1;
        //                        while (num20 < y + 6 && (!Main.Tile[x, num20].nactive() || !Main.tileSolid[(int)Main.Tile[x, num20].type] || Main.tileSolidTop[(int)Main.Tile[x, num20].type]))
        //                        {
        //                            if (Main.Tile[x, num20].liquid < 200)
        //                            {
        //                                flag = false;
        //                                break;
        //                            }
        //                            num20++;
        //                        }
        //                        if (!flag)
        //                        {
        //                            Opacity = 0.5f;
        //                            sourceRectangle = new Microsoft.Xna.Framework.Rectangle(0, 4, 16, 16);
        //                        }
        //                        else if (Main.Tile[x, y - 1].liquid > 0)
        //                        {
        //                            sourceRectangle = new Microsoft.Xna.Framework.Rectangle(0, 2, sourceRectangle.Width, sourceRectangle.Height);
        //                        }
        //                    }
        //                    if ((color.R > 20 || color.B > 20 || color.G > 20) && sourceRectangle.y < 4)
        //                    {
        //                        int num21 = (int)color.R;
        //                        if ((int)color.G > num21)
        //                        {
        //                            num21 = (int)color.G;
        //                        }
        //                        if ((int)color.B > num21)
        //                        {
        //                            num21 = (int)color.B;
        //                        }
        //                        num21 /= 30;
        //                        if (Main.rand.Next(20000) < num21)
        //                        {
        //                            Microsoft.Xna.Framework.Color newColor = new Microsoft.Xna.Framework.Color(255, 255, 255);
        //                            if (LiquidCore.grid[x, y].liquidType == 2)
        //                            {
        //                                newColor = new Microsoft.Xna.Framework.Color(255, 255, 50);
        //                            }
        //                            int num22 = Dust.NewDust(new Vector2((float)(x * 16), drawingPosition.y - 2f), 16, 8, 43, 0f, 0f, 254, newColor, 0.75f);
        //                            Main.dust[num22].velocity *= 0f;
        //                        }
        //                    }
        //                    if (liquid.liquidType == 2)
        //                    {
        //                        Opacity *= 1.6f;
        //                        if (Opacity > 1f)
        //                        {
        //                            Opacity = 0.2f;
        //                        }
        //                    }
                            
        //                    if (liquid.liquidType == 1)
        //                    {
        //                        Opacity *= 1.8f;
        //                        if (Opacity > 1f)
        //                        {
        //                            Opacity = 5f;
        //                        }
        //                        if (Main.instance.IsActive && !Main.gamePaused && Dust.lavaBubbles < 200)
        //                        {
        //                            if (Main.Tile[x, y].liquid > 200 && Main.rand.Next(700) == 0)
        //                            {
        //                                Dust.NewDust(new Vector2((float)(x * 16), (float)(y * 16)), 16, 16, 35, 0f, 0f, 0, default(Microsoft.Xna.Framework.Color), 1f);
        //                            }
        //                            if (sourceRectangle.y == 0 && Main.rand.Next(350) == 0)
        //                            {
        //                                int num23 = Dust.NewDust(new Vector2((float)(x * 16), (float)(y * 16) + liquidLayer * 2f - 8f), 16, 8, 35, 0f, 0f, 50, default(Microsoft.Xna.Framework.Color), 1.5f);
        //                                Main.dust[num23].velocity *= 0.8f;
        //                                Dust expr_9CA_cp_0 = Main.dust[num23];
        //                                expr_9CA_cp_0.velocity.X = expr_9CA_cp_0.velocity.X * 2f;
        //                                Dust expr_9E8_cp_0 = Main.dust[num23];
        //                                expr_9E8_cp_0.velocity.y = expr_9E8_cp_0.velocity.y - (float)Main.rand.Next(1, 7) * 0.1f;
        //                                if (Main.rand.Next(10) == 0)
        //                                {
        //                                    Dust expr_A22_cp_0 = Main.dust[num23];
        //                                    expr_A22_cp_0.velocity.y = expr_A22_cp_0.velocity.y * (float)Main.rand.Next(2, 5);
        //                                }
        //                                Main.dust[num23].noGravity = true;
        //                            }
        //                        }
        //                    }

                            

        //                    //liquid lib: opacity hook
        //                    Opacity = LiquidRegistry.setOpacity(liquid);
                            

        //                    float finalRedColorValue = (float)color.R * Opacity;
        //                    float finalGreenColorValue = (float)color.G * Opacity;
        //                    float finalBlueColorValue = (float)color.B * Opacity;
        //                    float finalAlphaColorValue = (float)color.A * Opacity;
        //                    color = new Microsoft.Xna.Framework.Color((int)((byte)finalRedColorValue), (int)((byte)finalGreenColorValue), (int)((byte)finalBlueColorValue), (int)((byte)finalAlphaColorValue));
        //                    if (Lighting.NotRetro && !bg)
        //                    {
        //                        Microsoft.Xna.Framework.Color color2 = color;
        //                        if (textureIndex != 1 && ((double)color2.R > (double)num6 * 0.6 || (double)color2.G > (double)num6 * 0.65 || (double)color2.B > (double)num6 * 0.7))
        //                        {
        //                            for (int k = 0; k < 4; k++)
        //                            {
        //                                int num28 = 0;
        //                                int num29 = 0;
        //                                int width = 8;
        //                                int height = 8;
        //                                Microsoft.Xna.Framework.Color color3 = color2;
        //                                Microsoft.Xna.Framework.Color color4 = Lighting.GetColor(x, y);
        //                                if (k == 0)
        //                                {
        //                                    color4 = Lighting.GetColor(x - 1, y - 1);
        //                                    if (sourceRectangle.Height < 8)
        //                                    {
        //                                        height = sourceRectangle.Height;
        //                                    }
        //                                }
        //                                if (k == 1)
        //                                {
        //                                    color4 = Lighting.GetColor(x + 1, y - 1);
        //                                    num28 = 8;
        //                                    if (sourceRectangle.Height < 8)
        //                                    {
        //                                        height = sourceRectangle.Height;
        //                                    }
        //                                }
        //                                if (k == 2)
        //                                {
        //                                    color4 = Lighting.GetColor(x - 1, y + 1);
        //                                    num29 = 8;
        //                                    height = 8 - (16 - sourceRectangle.Height);
        //                                }
        //                                if (k == 3)
        //                                {
        //                                    color4 = Lighting.GetColor(x + 1, y + 1);
        //                                    num28 = 8;
        //                                    num29 = 8;
        //                                    height = 8 - (16 - sourceRectangle.Height);
        //                                }
        //                                finalRedColorValue = (float)color4.R * Opacity;
        //                                finalGreenColorValue = (float)color4.G * Opacity;
        //                                finalBlueColorValue = (float)color4.B * Opacity;
        //                                finalAlphaColorValue = (float)color4.A * Opacity;
        //                                color4 = new Microsoft.Xna.Framework.Color((int)((byte)finalRedColorValue), (int)((byte)finalGreenColorValue), (int)((byte)finalBlueColorValue), (int)((byte)finalAlphaColorValue));
        //                                color3.R = (byte)((color2.R * 3 + color4.R * 2) / 5);
        //                                color3.G = (byte)((color2.G * 3 + color4.G * 2) / 5);
        //                                color3.B = (byte)((color2.B * 3 + color4.B * 2) / 5);
        //                                color3.A = (byte)((color2.A * 3 + color4.A * 2) / 5);
        //                                Main.spriteBatch.Draw(Main.liquidTexture[textureIndex], drawingPosition - Main.screenPosition + new Vector2((float)num28, (float)num29) + zero, new Microsoft.Xna.Framework.Rectangle?(new Microsoft.Xna.Framework.Rectangle(sourceRectangle.X + num28, sourceRectangle.y + num29, width, height)), color3, 0f, default(Vector2), 1f, SpriteEffects.None, 0f);
        //                            }
        //                        }
        //                        else
        //                        {
        //                            Main.spriteBatch.Draw(Main.liquidTexture[textureIndex], drawingPosition - Main.screenPosition + zero, new Microsoft.Xna.Framework.Rectangle?(sourceRectangle), color, 0f, default(Vector2), 1f, SpriteEffects.None, 0f);
        //                        }
        //                    }
        //                    else
        //                    {
        //                        if (sourceRectangle.y < 4)
        //                        {
        //                            sourceRectangle.X += (int)(Main.wFrame * 18f);
        //                        }

        //                            Vector2 megaVector = new Vector2(drawingPosition.X - Main.screenPosition.X + zero.X,
        //                                drawingPosition.y - Main.screenPosition.y + zero.y);
        //                            Main.spriteBatch.Draw(Main.liquidTexture[11], megaVector, sourceRectangle, color, 0f, default(Vector2), new Vector2(1f, 1.30f), SpriteEffects.None, 0f);
                                
                                   
        //                    }

        //                    //If liquid is in a half block
        //                    if (Main.Tile[x, y + 1].halfBrick())
        //                    {
        //                        sourceRectangle = new Microsoft.Xna.Framework.Rectangle(0, 4, 16, 8);
        //                        if (textureIndex > 11)
        //                        {
        //                            sourceRectangle = new Microsoft.Xna.Framework.Rectangle(16, 1285, 16, 8);
        //                        }
        //                        color = Lighting.GetColor(x, y + 1);
        //                        finalRedColorValue = (float) color.R * Opacity; //* num17;
        //                        finalGreenColorValue = (float) color.G * Opacity; //* num17;
        //                        finalBlueColorValue = (float) color.B * Opacity; //* num17;
        //                        finalAlphaColorValue = (float) color.A * Opacity; //* num17;
        //                        color = new Microsoft.Xna.Framework.Color((int)((byte)finalRedColorValue), (int)((byte)finalGreenColorValue), (int)((byte)finalBlueColorValue), (int)((byte)finalAlphaColorValue));
        //                        drawingPosition = new Vector2((float)(x * 16), (float)(y * 16 + 16));
        //                        Main.spriteBatch.Draw(Main.liquidTexture[textureIndex], drawingPosition - Main.screenPosition + zero, new Microsoft.Xna.Framework.Rectangle?(new Microsoft.Xna.Framework.Rectangle(0, 4, 16, 8)), color, 0f, default(Vector2), 1f, SpriteEffects.None, 0f);
        //                    }
        //                }
        //            }
        //            IL_E7F:
        //            ;
        //        }
        //    }
        //    if (!Main.drewLava)
        //    {
        //        Main.ambientLavaX = (float)num4;
        //        Main.ambientLavaY = (float)num5;
        //        Main.ambientLavaStrength = ambientLavaStrength;
        //    }
        //    Main.drewLava = true;
        //}

        //protected static void DrawTiles(On.Terraria.Main.orig_DrawTiles orig, Main main, bool solidOnly = true, int waterStyleOverride = -1)
        //{
        //    if (!solidOnly)
        //    {
        //        Main.critterCage = false;
        //    }
        //    Stopwatch stopwatch = new Stopwatch();
        //    stopwatch.Start();
        //    int num = (int)(255f * (1f - Main.gfxQuality) + 30f * Main.gfxQuality);
        //    int num2 = (int)(50f * (1f - Main.gfxQuality) + 2f * Main.gfxQuality);
        //    Vector2 zero = new Vector2((float)Main.offScreenRange, (float)Main.offScreenRange);
        //    if (Main.drawToScreen)
        //    {
        //        zero = Vector2.Zero;
        //    }
        //    int num3 = 0;
        //    int[] arg_7B_0 = Main.specX;
        //    int num4 = (int)((Main.screenPosition.X - zero.X) / 16f - 1f);
        //    int num5 = (int)((Main.screenPosition.X + (float)Main.screenWidth + zero.X) / 16f) + 2;
        //    int num6 = (int)((Main.screenPosition.y - zero.y) / 16f - 1f);
        //    int num7 = (int)((Main.screenPosition.y + (float)Main.screenHeight + zero.y) / 16f) + 5;
        //    if (num4 < 4)
        //    {
        //        num4 = 4;
        //    }
        //    if (num5 > Main.maxTilesX - 4)
        //    {
        //        num5 = Main.maxTilesX - 4;
        //    }
        //    if (num6 < 4)
        //    {
        //        num6 = 4;
        //    }
        //    if (num7 > Main.maxTilesY - 4)
        //    {
        //        num7 = Main.maxTilesY - 4;
        //    }
        //    if (Main.sectionManager.FrameSectionsLeft > 0)
        //    {
        //        TimeLogger.DetailedDrawReset();
        //        WorldGen.SectionTileFrameWithCheck(num4, num6, num5, num7);
        //        TimeLogger.DetailedDrawTime(5);
        //    }
        //    Dictionary<Microsoft.Xna.Framework.Point, int> dictionary = new Dictionary<Microsoft.Xna.Framework.Point, int>();
        //    Dictionary<Microsoft.Xna.Framework.Point, int> dictionary2 = new Dictionary<Microsoft.Xna.Framework.Point, int>();
        //    Dictionary<Microsoft.Xna.Framework.Point, int> dictionary3 = new Dictionary<Microsoft.Xna.Framework.Point, int>();
        //    int arg_188_0 = Main.player[Main.myPlayer].team;
        //    if (Main.player[Main.myPlayer].active)
        //    {
        //        int arg_1A2_0 = Main.netMode;
        //    }
        //    int num8 = 16;
        //    Microsoft.Xna.Framework.Color[] array = new Microsoft.Xna.Framework.Color[9];
        //    for (int i = num6; i < num7 + 4; i++)
        //    {
        //        for (int j = num4 - 2; j < num5 + 2; j++)
        //        {
        //            Tile Tile = Main.Tile[j, i];
        //            //patch file: j, i
        //            if (Tile == null)
        //            {
        //                Tile = new Tile();
        //                Main.Tile[j, i] = Tile;
        //                Main.mapTime += 60;
        //            }
        //            ushort type = Tile.type;
        //            short num9 = Tile.frameX;
        //            short num10 = Tile.frameY;
        //            bool flag = Main.tileSolid[(int)type];
        //            if (type == 11)
        //            {
        //                flag = true;
        //            }
        //            if (Tile.active() && flag == solidOnly)
        //            {
        //                if (!Main.tileSetsLoaded[(int)type])
        //                {
        //                    Main.instance.LoadTiles((int)type);
        //                }
        //                SpriteEffects effects = SpriteEffects.None;
        //                if (type == 3 || type == 13 || type == 20 || type == 24 || type == 49 || type == 372 || type == 50 || type == 52 || type == 61 || type == 62 || type == 71 || type == 73 || type == 74 || type == 81 || type == 82 || type == 83 || type == 84 || type == 91 || type == 92 || type == 93 || type == 110 || type == 113 || type == 115 || type == 135 || type == 141 || type == 165 || type == 174 || type == 201 || type == 205 || type == 227 || type == 270 || type == 271 || type == 382)
        //                {
        //                    if (j % 2 == 1)
        //                    {
        //                        effects = SpriteEffects.FlipHorizontally;
        //                    }
        //                }
        //                else if (type == 184)
        //                {
        //                    if (num10 < 108)
        //                    {
        //                        if (j % 2 == 1)
        //                        {
        //                            effects = SpriteEffects.FlipHorizontally;
        //                        }
        //                    }
        //                    else if (i % 2 == 1)
        //                    {
        //                        effects = SpriteEffects.FlipVertically;
        //                    }
        //                }
        //                else if (type == 185 && num10 == 0 && j % 2 == 1)
        //                {
        //                    effects = SpriteEffects.FlipHorizontally;
        //                }
        //                TileLoader.SetSpriteEffects(j, i, type, ref effects);
        //                Microsoft.Xna.Framework.Color color = Lighting.GetColor(j, i);
        //                int num11 = 0;
        //                int num12 = 16;
        //                if (type >= 330 && type <= 333)
        //                {
        //                    num11 += 2;
        //                }
        //                if (type == 4 && WorldGen.SolidTile(j, i - 1))
        //                {
        //                    num11 = 2;
        //                    if (WorldGen.SolidTile(j - 1, i + 1) || WorldGen.SolidTile(j + 1, i + 1))
        //                    {
        //                        num11 = 4;
        //                    }
        //                }
        //                if (type == 336)
        //                {
        //                    num11 = 2;
        //                }
        //                if (type == 457)
        //                {
        //                    num11 = 2;
        //                }
        //                if (type == 466)
        //                {
        //                    num11 = 2;
        //                }
        //                if ((type >= 275 && type <= 282) || type == 414 || type == 413)
        //                {
        //                    num11 = 2;
        //                }
        //                if (type == 285 || type == 286 || type == 298 || type == 299 || type == 309 || type == 358 || type == 359 || type == 360 || type == 361 || type == 362 || type == 363 || type == 364 || type == 391 || type == 392 || type == 393 || type == 394 || type == 310)
        //                {
        //                    num11 = 2;
        //                }
        //                if (type == 100 || type == 173 || type == 283)
        //                {
        //                    num11 = 2;
        //                }
        //                if (type == 78 || type == 85 || type == 210 || type == 133 || type == 134 || type == 233)
        //                {
        //                    num11 = 2;
        //                }
        //                if (type == 33 || type == 49 || type == 174 || type == 372)
        //                {
        //                    num11 = -4;
        //                }
        //                if (type == 3 || type == 4 || type == 5 || type == 24 || type == 33 || type == 49 || type == 372 || type == 61 || type == 71 || type == 110 || type == 174 || type == 201 || type == 323 || type == 324)
        //                {
        //                    num12 = 20;
        //                }
        //                else if (type == 16 || type == 17 || type == 18 || type == 20 || type == 26 || type == 32 || type == 352 || type == 69 || type == 72 || type == 77 || type == 79 || type == 80)
        //                {
        //                    num12 = 18;
        //                }
        //                else if (type == 14 || type == 469 || type == 15 || type == 21 || type == 467 || type == 411 || type == 441 || type == 468)
        //                {
        //                    if (num10 == 18)
        //                    {
        //                        num12 = 18;
        //                    }
        //                }
        //                else if (type == 172 || type == 376)
        //                {
        //                    if (num10 % 38 == 18)
        //                    {
        //                        num12 = 18;
        //                    }
        //                }
        //                else if (type == 27)
        //                {
        //                    if (num10 % 74 == 54)
        //                    {
        //                        num12 = 18;
        //                    }
        //                }
        //                else if (type == 137)
        //                {
        //                    num12 = 18;
        //                }
        //                else if (type == 462)
        //                {
        //                    num12 = 18;
        //                }
        //                else if (type == 135)
        //                {
        //                    num11 = 2;
        //                    num12 = 18;
        //                }
        //                else if (type == 378)
        //                {
        //                    num11 = 2;
        //                }
        //                else if (type == 254)
        //                {
        //                    num11 = 2;
        //                }
        //                else if (type == 132)
        //                {
        //                    num11 = 2;
        //                    num12 = 18;
        //                }
        //                else if (type == 405)
        //                {
        //                    num12 = 16;
        //                    if (num10 > 0)
        //                    {
        //                        num12 = 18;
        //                    }
        //                }
        //                else if (type == 406)
        //                {
        //                    num12 = 16;
        //                    if (num10 % 54 >= 36)
        //                    {
        //                        num12 = 18;
        //                    }
        //                }
        //                else
        //                {
        //                    num12 = 16;
        //                }
        //                if (type == 52)
        //                {
        //                    num11 -= 2;
        //                }
        //                if (type == 324)
        //                {
        //                    num11 = -2;
        //                }
        //                if (type == 231 || type == 238)
        //                {
        //                    num11 += 2;
        //                }
        //                if (type == 207)
        //                {
        //                    num11 = 2;
        //                }
        //                if (type == 4 || type == 5 || type == 323 || type == 324)
        //                {
        //                    num8 = 20;
        //                }
        //                else
        //                {
        //                    num8 = 16;
        //                }
        //                if (type == 73 || type == 74 || type == 113)
        //                {
        //                    num11 -= 12;
        //                    num12 = 32;
        //                }
        //                if (type == 388 || type == 389)
        //                {
        //                    TileObjectData tileData = TileObjectData.GetTileData((int)type, (int)(num9 / 18), 0);
        //                    int height = tileData.Height;
        //                    int num13 = height * 18 + 4;
        //                    num11 = -2;
        //                    if ((int)num10 == num13 - 20 || (int)num10 == num13 * 2 - 20 || num10 == 0 || (int)num10 == num13)
        //                    {
        //                        num12 = 18;
        //                    }
        //                }
        //                if (type == 410 && num10 == 36)
        //                {
        //                    num12 = 18;
        //                }
        //                if (type == 227)
        //                {
        //                    //patch file: num8, num12
        //                    num8 = 32;
        //                    num12 = 38;
        //                    if (num9 == 238)
        //                    {
        //                        num11 -= 6;
        //                    }
        //                    else
        //                    {
        //                        num11 -= 20;
        //                    }
        //                }
        //                if (type == 185 || type == 186 || type == 187)
        //                {
        //                    num11 = 2;
        //                    if (type == 185)
        //                    {
        //                        if (num10 == 18 && num9 >= 576 && num9 <= 882)
        //                        {
        //                            Main.tileShine2[185] = true;
        //                        }
        //                        else
        //                        {
        //                            Main.tileShine2[185] = false;
        //                        }
        //                    }
        //                    else if (type == 186)
        //                    {
        //                        if (num9 >= 864 && num9 <= 1170)
        //                        {
        //                            Main.tileShine2[186] = true;
        //                        }
        //                        else
        //                        {
        //                            Main.tileShine2[186] = false;
        //                        }
        //                    }
        //                }
        //                if (type == 178 && num10 <= 36)
        //                {
        //                    num11 = 2;
        //                }
        //                if (type == 184)
        //                {
        //                    num8 = 20;
        //                    if (num10 <= 36)
        //                    {
        //                        num11 = 2;
        //                    }
        //                    else if (num10 <= 108)
        //                    {
        //                        num11 = -2;
        //                    }
        //                }
        //                if (type == 28)
        //                {
        //                    num11 += 2;
        //                }
        //                if (type == 81)
        //                {
        //                    num11 -= 8;
        //                    num12 = 26;
        //                    num8 = 24;
        //                }
        //                if (type == 105)
        //                {
        //                    num11 = 2;
        //                }
        //                if (type == 124)
        //                {
        //                    num12 = 18;
        //                }
        //                if (type == 137)
        //                {
        //                    num12 = 18;
        //                }
        //                if (type == 138)
        //                {
        //                    num12 = 18;
        //                }
        //                if (type == 139 || type == 142 || type == 143)
        //                {
        //                    num11 = 2;
        //                }
        //                TileLoader.SetDrawPositions(j, i, ref num8, ref num11, ref num12);
        //                int num14 = 0;
        //                if (Tile.halfBrick())
        //                {
        //                    num14 = 8;
        //                }
        //                int num15 = Main.tileFrame[(int)type] * 38;
        //                int num16 = 0;
        //                if (type == 272)
        //                {
        //                    num15 = 0;
        //                }
        //                if (type == 106)
        //                {
        //                    num15 = Main.tileFrame[(int)type] * 54;
        //                }
        //                if (type >= 300 && type <= 308)
        //                {
        //                    num15 = Main.tileFrame[(int)type] * 54;
        //                    num11 = 2;
        //                }
        //                if (type == 354)
        //                {
        //                    num15 = Main.tileFrame[(int)type] * 54;
        //                    num11 = 2;
        //                }
        //                if (type == 355)
        //                {
        //                    num15 = Main.tileFrame[(int)type] * 54;
        //                    num11 = 2;
        //                }
        //                if (type == 377)
        //                {
        //                    num15 = Main.tileFrame[(int)type] * 38;
        //                    num11 = 2;
        //                }
        //                if (type == 463)
        //                {
        //                    num15 = Main.tileFrame[(int)type] * 72;
        //                    num11 = 2;
        //                }
        //                if (type == 464)
        //                {
        //                    num15 = Main.tileFrame[(int)type] * 72;
        //                    num11 = 2;
        //                }
        //                if (type == 379)
        //                {
        //                    num15 = Main.tileFrame[(int)type] * 90;
        //                }
        //                if (type == 349)
        //                {
        //                    int num17 = (int)(num9 % 36);
        //                    int num18 = (int)(num10 % 54);
        //                    int num19;
        //                    if (Animation.GetTemporaryFrame(j - num17 / 18, i - num18 / 18, out num19))
        //                    {
        //                        num9 = (short)(36 * num19 + num17);
        //                    }
        //                }
        //                if (type == 441 || type == 468)
        //                {
        //                    int num20 = (int)(num9 % 36);
        //                    int num21 = (int)(num10 % 38);
        //                    int num22;
        //                    if (Animation.GetTemporaryFrame(j - num20 / 18, i - num21 / 18, out num22))
        //                    {
        //                        num10 = (short)(38 * num22 + num21);
        //                    }
        //                }
        //                if (type == 390)
        //                {
        //                    num15 = Main.tileFrame[(int)type] * 36;
        //                }
        //                if (type == 412)
        //                {
        //                    num15 = 0;
        //                    num11 = 2;
        //                }
        //                if (type == 455)
        //                {
        //                    num15 = 0;
        //                    num11 = 2;
        //                }
        //                if (type == 406)
        //                {
        //                    int num23 = Main.tileFrame[(int)type];
        //                    if (num10 >= 108)
        //                    {
        //                        num23 = (int)(6 - num10 / 54);
        //                    }
        //                    else if (num10 >= 54)
        //                    {
        //                        num23 = Main.tileFrame[(int)type] - 1;
        //                    }
        //                    num15 = num23 * 56;
        //                    num15 += (int)(num10 / 54 * 2);
        //                }
        //                if (type == 452)
        //                {
        //                    int num24 = Main.tileFrame[(int)type];
        //                    if (num9 >= 54)
        //                    {
        //                        num24 = 0;
        //                    }
        //                    num15 = num24 * 54;
        //                }
        //                if (type == 455)
        //                {
        //                    int num25 = 1 + Main.tileFrame[(int)type];
        //                    if (!BirthdayParty.PartyIsUp)
        //                    {
        //                        num25 = 0;
        //                    }
        //                    num15 = num25 * 54;
        //                }
        //                if (type == 454)
        //                {
        //                    int num26 = Main.tileFrame[(int)type];
        //                    num15 = num26 * 54;
        //                }
        //                if (type == 453)
        //                {
        //                    int num27 = Main.tileFrameCounter[(int)type];
        //                    num27 /= 20;
        //                    int num28 = i - (int)(Tile.frameY / 18);
        //                    num27 += num28 + j;
        //                    num27 %= 3;
        //                    num15 = num27 * 54;
        //                }
        //                if (type == 456)
        //                {
        //                    int num29 = Main.tileFrameCounter[(int)type];
        //                    num29 /= 20;
        //                    int num30 = i - (int)(Tile.frameY / 18);
        //                    int num31 = j - (int)(Tile.frameX / 18);
        //                    num29 += num30 + num31;
        //                    num29 %= 4;
        //                    num15 = num29 * 54;
        //                }
        //                if (type == 405)
        //                {
        //                    int num32 = Main.tileFrame[(int)type];
        //                    if (num9 >= 54)
        //                    {
        //                        num32 = 0;
        //                    }
        //                    num15 = num32 * 38;
        //                }
        //                if (type == 12)
        //                {
        //                    num15 = Main.tileFrame[(int)type] * 36;
        //                }
        //                if (type == 96)
        //                {
        //                    num15 = Main.tileFrame[(int)type] * 36;
        //                }
        //                if (type == 238)
        //                {
        //                    num15 = Main.tileFrame[(int)type] * 36;
        //                }
        //                if (type == 31)
        //                {
        //                    num15 = Main.tileFrame[(int)type] * 36;
        //                }
        //                if (type == 215)
        //                {
        //                    if (num10 < 36)
        //                    {
        //                        num15 = Main.tileFrame[(int)type] * 36;
        //                    }
        //                    else
        //                    {
        //                        num15 = 252;
        //                    }
        //                    num11 = 2;
        //                }
        //                if (type == 231)
        //                {
        //                    num15 = Main.tileFrame[(int)type] * 54;
        //                    num11 = 2;
        //                }
        //                if (type == 243)
        //                {
        //                    num15 = Main.tileFrame[(int)type] * 54;
        //                    num11 = 2;
        //                }
        //                if (type == 247)
        //                {
        //                    num15 = Main.tileFrame[(int)type] * 54;
        //                    num11 = 2;
        //                }
        //                if (type == 228)
        //                {
        //                    num15 = Main.tileFrame[(int)type] * 54;
        //                    num11 = 2;
        //                }
        //                if (type == 244)
        //                {
        //                    num11 = 2;
        //                    if (num9 < 54)
        //                    {
        //                        num15 = Main.tileFrame[(int)type] * 36;
        //                    }
        //                    else
        //                    {
        //                        num15 = 0;
        //                    }
        //                }
        //                if (type == 235)
        //                {
        //                    num15 = Main.tileFrame[(int)type] * 18;
        //                }
        //                if (type == 217 || type == 218)
        //                {
        //                    num15 = Main.tileFrame[(int)type] * 36;
        //                    num11 = 2;
        //                }
        //                if (type == 219 || type == 220)
        //                {
        //                    num15 = Main.tileFrame[(int)type] * 54;
        //                    num11 = 2;
        //                }
        //                if (type == 270 || type == 271)
        //                {
        //                    int k = Main.tileFrame[(int)type] + j % 6;
        //                    if (j % 2 == 0)
        //                    {
        //                        k += 3;
        //                    }
        //                    if (j % 3 == 0)
        //                    {
        //                        k += 3;
        //                    }
        //                    if (j % 4 == 0)
        //                    {
        //                        k += 3;
        //                    }
        //                    while (k > 5)
        //                    {
        //                        k -= 6;
        //                    }
        //                    num16 = k * 18;
        //                    num15 = 0;
        //                }
        //                if (type == 428)
        //                {
        //                    num11 += 4;
        //                    if (PressurePlateHelper.PressurePlatesPressed.ContainsKey(new Microsoft.Xna.Framework.Point(j, i)))
        //                    {
        //                        num16 += 18;
        //                    }
        //                }
        //                else if (type == 442)
        //                {
        //                    num8 = 20;
        //                    num12 = 20;
        //                    switch (num9 / 22)
        //                    {
        //                        case 1:
        //                            num11 = -4;
        //                            break;
        //                        case 2:
        //                            num11 = -2;
        //                            num8 = 24;
        //                            break;
        //                        case 3:
        //                            num11 = -2;
        //                            num8 = 16;
        //                            break;
        //                    }
        //                }
        //                if (TileID.Sets.TeamTiles[(int)type])
        //                {
        //                    if (TileID.Sets.Platforms[(int)type])
        //                    {
        //                        num15 = num15;
        //                    }
        //                    else
        //                    {
        //                        num15 += 90;
        //                    }
        //                }
        //                TileLoader.SetAnimationFrame(type, j, i, ref num16, ref num15);
        //                if (!TileLoader.PreDraw(j, i, type, Main.spriteBatch))
        //                {
        //                    TileLoader.PostDraw(j, i, type, Main.spriteBatch);
        //                    continue;
        //                }
        //                if (type == 373 || type == 374 || type == 375 || type == 461)
        //                {
        //                    int num33 = 60;
        //                    if (type == 374)
        //                    {
        //                        num33 = 120;
        //                    }
        //                    else if (type == 375)
        //                    {
        //                        num33 = 180;
        //                    }
        //                    if (Main.rand.Next(num33 * 2) == 0 && Tile.liquid == 0)
        //                    {
        //                        Microsoft.Xna.Framework.Rectangle rectangle = new Microsoft.Xna.Framework.Rectangle(j * 16, i * 16, 16, 16);
        //                        rectangle.X -= 34;
        //                        rectangle.Width += 68;
        //                        rectangle.y -= 100;
        //                        rectangle.Height = 400;
        //                        bool flag2 = true;
        //                        for (int l = 0; l < 500; l++)
        //                        {
        //                            if (Main.gore[l].active && ((Main.gore[l].type >= 706 && Main.gore[l].type <= 717) || Main.gore[l].type == 943))
        //                            {
        //                                Microsoft.Xna.Framework.Rectangle value = new Microsoft.Xna.Framework.Rectangle((int)Main.gore[l].position.X, (int)Main.gore[l].position.y, 16, 16);
        //                                if (rectangle.Intersects(value))
        //                                {
        //                                    flag2 = false;
        //                                }
        //                            }
        //                        }
        //                        if (flag2)
        //                        {
        //                            Vector2 position = new Vector2((float)(j * 16), (float)(i * 16));
        //                            int num34 = 706;
        //                            if (Main.waterStyle > 1)
        //                            {
        //                                num34 = 706 + Main.waterStyle - 1;
        //                                if (Main.waterStyle >= WaterStyleLoader.vanillaWaterCount)
        //                                {
        //                                    num34 = WaterStyleLoader.GetWaterStyle(Main.waterStyle).GetDropletGore();
        //                                }
        //                            }
        //                            if (type == 374)
        //                            {
        //                                num34 = 716;
        //                            }
        //                            if (type == 375)
        //                            {
        //                                num34 = 717;
        //                            }
        //                            if (type == 461)
        //                            {
        //                                num34 = 943;
        //                            }
        //                            if (num34 != 943 || Main.rand.Next(3) == 0)
        //                            {
        //                                int num35 = Gore.NewGore(position, default(Vector2), num34, 1f);
        //                                Main.gore[num35].velocity *= 0f;
        //                            }
        //                        }
        //                    }
        //                }
        //                else
        //                {
        //                    if ((type >= 275 && type <= 281) || type == 296 || type == 297 || type == 309 || type == 358 || type == 359 || type == 414 || type == 413)
        //                    {
        //                        Main.critterCage = true;
        //                        int num36 = j - (int)(num9 / 18);
        //                        int num37 = i - (int)(num10 / 18);
        //                        int num38 = num36 / 6 * (num37 / 4);
        //                        num38 %= Main.cageFrames;
        //                        if (type == 275 || type == 359)
        //                        {
        //                            num15 = Main.bunnyCageFrame[num38] * 54;
        //                        }
        //                        if (type == 276 || type == 414)
        //                        {
        //                            num15 = Main.squirrelCageFrame[num38] * 54;
        //                        }
        //                        if (type == 413)
        //                        {
        //                            num15 = Main.squirrelCageFrameOrange[num38] * 54;
        //                        }
        //                        if (type == 277)
        //                        {
        //                            num15 = Main.mallardCageFrame[num38] * 54;
        //                        }
        //                        if (type == 278)
        //                        {
        //                            num15 = Main.duckCageFrame[num38] * 54;
        //                        }
        //                        if (type == 279 || type == 358)
        //                        {
        //                            num15 = Main.birdCageFrame[num38] * 54;
        //                        }
        //                        if (type == 280)
        //                        {
        //                            num15 = Main.blueBirdCageFrame[num38] * 54;
        //                        }
        //                        if (type == 281)
        //                        {
        //                            num15 = Main.redBirdCageFrame[num38] * 54;
        //                        }
        //                        if (type == 296)
        //                        {
        //                            num15 = Main.scorpionCageFrame[0, num38] * 54;
        //                        }
        //                        if (type == 297)
        //                        {
        //                            num15 = Main.scorpionCageFrame[0, num38] * 54;
        //                        }
        //                        if (type == 309)
        //                        {
        //                            num15 = Main.penguinCageFrame[num38] * 54;
        //                        }
        //                    }
        //                    else if (type == 285 || type == 286 || type == 298 || type == 299 || type == 310 || type == 339 || (type >= 361 && type <= 364) || (type >= 391 && type <= 394))
        //                    {
        //                        Main.critterCage = true;
        //                        int num39 = j - (int)(num9 / 18);
        //                        int num40 = i - (int)(num10 / 18);
        //                        int num41 = num39 / 3 * (num40 / 3);
        //                        num41 %= Main.cageFrames;
        //                        if (type == 285)
        //                        {
        //                            num15 = Main.snailCageFrame[num41] * 36;
        //                        }
        //                        if (type == 286)
        //                        {
        //                            num15 = Main.snail2CageFrame[num41] * 36;
        //                        }
        //                        if (type == 298 || type == 361)
        //                        {
        //                            num15 = Main.frogCageFrame[num41] * 36;
        //                        }
        //                        if (type == 299 || type == 363)
        //                        {
        //                            num15 = Main.mouseCageFrame[num41] * 36;
        //                        }
        //                        if (type == 310 || type == 364 || type == 391)
        //                        {
        //                            num15 = Main.wormCageFrame[num41] * 36;
        //                        }
        //                        if (type == 339 || type == 362)
        //                        {
        //                            num15 = Main.grasshopperCageFrame[num41] * 36;
        //                        }
        //                        if (type == 392 || type == 393 || type == 394)
        //                        {
        //                            num15 = Main.slugCageFrame[(int)(type - 392), num41] * 36;
        //                        }
        //                    }
        //                    else if (type == 282 || (type >= 288 && type <= 295) || (type >= 316 && type <= 318) || type == 360)
        //                    {
        //                        Main.critterCage = true;
        //                        int num42 = j - (int)(num9 / 18);
        //                        int num43 = i - (int)(num10 / 18);
        //                        int num44 = num42 / 2 * (num43 / 3);
        //                        num44 %= Main.cageFrames;
        //                        if (type == 282)
        //                        {
        //                            num15 = Main.fishBowlFrame[num44] * 36;
        //                        }
        //                        else if ((type >= 288 && type <= 295) || type == 360)
        //                        {
        //                            int num45 = (int)(type - 288);
        //                            if (type == 360)
        //                            {
        //                                num45 = 8;
        //                            }
        //                            num15 = Main.butterflyCageFrame[num45, num44] * 36;
        //                        }
        //                        else if (type >= 316 && type <= 318)
        //                        {
        //                            int num46 = (int)(type - 316);
        //                            num15 = Main.jellyfishCageFrame[num46, num44] * 36;
        //                        }
        //                    }
        //                    else if (type == 207)
        //                    {
        //                        if (num10 >= 72)
        //                        {
        //                            num15 = Main.tileFrame[(int)type];
        //                            int num47 = j;
        //                            if (num9 % 36 != 0)
        //                            {
        //                                num47--;
        //                            }
        //                            num15 += num47 % 6;
        //                            if (num15 >= 6)
        //                            {
        //                                num15 -= 6;
        //                            }
        //                            num15 *= 72;
        //                        }
        //                        else
        //                        {
        //                            num15 = 0;
        //                        }
        //                    }
        //                    else if (type == 410)
        //                    {
        //                        if (num10 >= 56)
        //                        {
        //                            num15 = Main.tileFrame[(int)type];
        //                            num15 *= 56;
        //                        }
        //                        else
        //                        {
        //                            num15 = 0;
        //                        }
        //                    }
        //                    else if (type == 326 || type == 327 || type == 328 || type == 329 || type == 336 || type == 340 || type == 341 || type == 342 || type == 343 || type == 344 || type == 345 || type == 351 || type == 421 || type == 422 || type == 458 || type == 459)
        //                    {
        //                        num15 = Main.tileFrame[(int)type] * 90;
        //                    }
        //                    Texture2D texture2D = null;
        //                    Microsoft.Xna.Framework.Rectangle empty = Microsoft.Xna.Framework.Rectangle.Empty;
        //                    Microsoft.Xna.Framework.Color color2 = Microsoft.Xna.Framework.Color.Transparent;
        //                    byte b = (byte)(100f + 150f * Main.martianLight);
        //                    Microsoft.Xna.Framework.Color color3 = new Microsoft.Xna.Framework.Color((int)b, (int)b, (int)b, 0);
        //                    Microsoft.Xna.Framework.Color color4 = new Microsoft.Xna.Framework.Color(100, 100, 100, 0);
        //                    Microsoft.Xna.Framework.Color color5 = new Microsoft.Xna.Framework.Color(150, 100, 50, 0);
        //                    ushort num48 = type;
        //                    int num49;
        //                    if (num48 <= 93)
        //                    {
        //                        if (num48 <= 34)
        //                        {
        //                            switch (num48)
        //                            {
        //                                case 10:
        //                                    num49 = (int)(num10 / 54);
        //                                    if (num49 == 32)
        //                                    {
        //                                        texture2D = Main.glowMaskTexture[57];
        //                                        empty = new Microsoft.Xna.Framework.Rectangle((int)num9, (int)(num10 % 54), num8, num12);
        //                                        color2 = color3;
        //                                    }
        //                                    break;
        //                                case 11:
        //                                    num49 = (int)(num10 / 54);
        //                                    if (num49 == 32)
        //                                    {
        //                                        texture2D = Main.glowMaskTexture[58];
        //                                        empty = new Microsoft.Xna.Framework.Rectangle((int)num9, (int)(num10 % 54), num8, num12);
        //                                        color2 = color3;
        //                                    }
        //                                    if (num49 == 33)
        //                                    {
        //                                        texture2D = Main.glowMaskTexture[119];
        //                                        empty = new Microsoft.Xna.Framework.Rectangle((int)num9, (int)(num10 % 54), num8, num12);
        //                                        color2 = color4;
        //                                    }
        //                                    break;
        //                                case 12:
        //                                case 13:
        //                                case 16:
        //                                case 17:
        //                                case 20:
        //                                    break;
        //                                case 14:
        //                                    num49 = (int)(num9 / 54);
        //                                    if (num49 == 31)
        //                                    {
        //                                        texture2D = Main.glowMaskTexture[67];
        //                                        empty = new Microsoft.Xna.Framework.Rectangle((int)(num9 % 54), (int)num10, num8, num12);
        //                                        color2 = color3;
        //                                    }
        //                                    if (num49 == 32)
        //                                    {
        //                                        texture2D = Main.glowMaskTexture[124];
        //                                        empty = new Microsoft.Xna.Framework.Rectangle((int)(num9 % 54), (int)num10, num8, num12);
        //                                        color2 = color4;
        //                                    }
        //                                    break;
        //                                case 15:
        //                                    num49 = (int)(num10 / 40);
        //                                    if (num49 == 32)
        //                                    {
        //                                        texture2D = Main.glowMaskTexture[54];
        //                                        empty = new Microsoft.Xna.Framework.Rectangle((int)num9, (int)(num10 % 40), num8, num12);
        //                                        color2 = color3;
        //                                    }
        //                                    if (num49 == 33)
        //                                    {
        //                                        texture2D = Main.glowMaskTexture[116];
        //                                        empty = new Microsoft.Xna.Framework.Rectangle((int)num9, (int)(num10 % 40), num8, num12);
        //                                        color2 = color4;
        //                                    }
        //                                    break;
        //                                case 18:
        //                                    num49 = (int)(num9 / 36);
        //                                    if (num49 == 27)
        //                                    {
        //                                        texture2D = Main.glowMaskTexture[69];
        //                                        empty = new Microsoft.Xna.Framework.Rectangle((int)(num9 % 36), (int)num10, num8, num12);
        //                                        color2 = color3;
        //                                    }
        //                                    if (num49 == 28)
        //                                    {
        //                                        texture2D = Main.glowMaskTexture[125];
        //                                        empty = new Microsoft.Xna.Framework.Rectangle((int)(num9 % 36), (int)num10, num8, num12);
        //                                        color2 = color4;
        //                                    }
        //                                    break;
        //                                case 19:
        //                                    num49 = (int)(num10 / 18);
        //                                    if (num49 == 26)
        //                                    {
        //                                        texture2D = Main.glowMaskTexture[65];
        //                                        empty = new Microsoft.Xna.Framework.Rectangle((int)num9, (int)(num10 % 18), num8, num12);
        //                                        color2 = color3;
        //                                    }
        //                                    if (num49 == 27)
        //                                    {
        //                                        texture2D = Main.glowMaskTexture[112];
        //                                        empty = new Microsoft.Xna.Framework.Rectangle((int)num9, (int)(num10 % 18), num8, num12);
        //                                        color2 = color4;
        //                                    }
        //                                    break;
        //                                case 21:
        //                                    goto IL_1ABF;
        //                                default:
        //                                    switch (num48)
        //                                    {
        //                                        case 33:
        //                                            if (num9 / 18 == 0)
        //                                            {
        //                                                num49 = (int)(num10 / 22);
        //                                                if (num49 == 26)
        //                                                {
        //                                                    texture2D = Main.glowMaskTexture[61];
        //                                                    empty = new Microsoft.Xna.Framework.Rectangle((int)num9, (int)(num10 % 22), num8, num12);
        //                                                    color2 = color3;
        //                                                }
        //                                            }
        //                                            break;
        //                                        case 34:
        //                                            if (num9 / 54 == 0)
        //                                            {
        //                                                num49 = (int)(num10 / 54);
        //                                                if (num49 == 33)
        //                                                {
        //                                                    texture2D = Main.glowMaskTexture[55];
        //                                                    empty = new Microsoft.Xna.Framework.Rectangle((int)num9, (int)(num10 % 54), num8, num12);
        //                                                    color2 = color3;
        //                                                }
        //                                            }
        //                                            break;
        //                                    }
        //                                    break;
        //                            }
        //                        }
        //                        else if (num48 != 42)
        //                        {
        //                            if (num48 != 79)
        //                            {
        //                                switch (num48)
        //                                {
        //                                    case 87:
        //                                        num49 = (int)(num9 / 54);
        //                                        if (num49 == 26)
        //                                        {
        //                                            texture2D = Main.glowMaskTexture[64];
        //                                            empty = new Microsoft.Xna.Framework.Rectangle((int)(num9 % 54), (int)num10, num8, num12);
        //                                            color2 = color3;
        //                                        }
        //                                        if (num49 == 27)
        //                                        {
        //                                            texture2D = Main.glowMaskTexture[121];
        //                                            empty = new Microsoft.Xna.Framework.Rectangle((int)(num9 % 54), (int)num10, num8, num12);
        //                                            color2 = color4;
        //                                        }
        //                                        break;
        //                                    case 88:
        //                                        num49 = (int)(num9 / 54);
        //                                        if (num49 == 24)
        //                                        {
        //                                            texture2D = Main.glowMaskTexture[59];
        //                                            empty = new Microsoft.Xna.Framework.Rectangle((int)(num9 % 54), (int)num10, num8, num12);
        //                                            color2 = color3;
        //                                        }
        //                                        if (num49 == 25)
        //                                        {
        //                                            texture2D = Main.glowMaskTexture[120];
        //                                            empty = new Microsoft.Xna.Framework.Rectangle((int)(num9 % 54), (int)num10, num8, num12);
        //                                            color2 = color4;
        //                                        }
        //                                        break;
        //                                    case 89:
        //                                        num49 = (int)(num9 / 54);
        //                                        if (num49 == 29)
        //                                        {
        //                                            texture2D = Main.glowMaskTexture[66];
        //                                            empty = new Microsoft.Xna.Framework.Rectangle((int)(num9 % 54), (int)num10, num8, num12);
        //                                            color2 = color3;
        //                                        }
        //                                        if (num49 == 30)
        //                                        {
        //                                            texture2D = Main.glowMaskTexture[123];
        //                                            empty = new Microsoft.Xna.Framework.Rectangle((int)(num9 % 54), (int)num10, num8, num12);
        //                                            color2 = color4;
        //                                        }
        //                                        break;
        //                                    case 90:
        //                                        num49 = (int)(num10 / 36);
        //                                        if (num49 == 27)
        //                                        {
        //                                            texture2D = Main.glowMaskTexture[52];
        //                                            empty = new Microsoft.Xna.Framework.Rectangle((int)num9, (int)(num10 % 36), num8, num12);
        //                                            color2 = color3;
        //                                        }
        //                                        if (num49 == 28)
        //                                        {
        //                                            texture2D = Main.glowMaskTexture[113];
        //                                            empty = new Microsoft.Xna.Framework.Rectangle((int)num9, (int)(num10 % 36), num8, num12);
        //                                            color2 = color4;
        //                                        }
        //                                        break;
        //                                    case 93:
        //                                        num49 = (int)(num9 / 54);
        //                                        if (num49 == 27)
        //                                        {
        //                                            texture2D = Main.glowMaskTexture[62];
        //                                            empty = new Microsoft.Xna.Framework.Rectangle((int)num9, (int)(num10 % 54), num8, num12);
        //                                            color2 = color3;
        //                                        }
        //                                        break;
        //                                }
        //                            }
        //                            else
        //                            {
        //                                num49 = (int)(num10 / 36);
        //                                if (num49 == 27)
        //                                {
        //                                    texture2D = Main.glowMaskTexture[53];
        //                                    empty = new Microsoft.Xna.Framework.Rectangle((int)num9, (int)(num10 % 36), num8, num12);
        //                                    color2 = color3;
        //                                }
        //                                if (num49 == 28)
        //                                {
        //                                    texture2D = Main.glowMaskTexture[114];
        //                                    empty = new Microsoft.Xna.Framework.Rectangle((int)num9, (int)(num10 % 36), num8, num12);
        //                                    color2 = color4;
        //                                }
        //                            }
        //                        }
        //                        else
        //                        {
        //                            num49 = (int)(num10 / 36);
        //                            if (num49 == 33)
        //                            {
        //                                texture2D = Main.glowMaskTexture[63];
        //                                empty = new Microsoft.Xna.Framework.Rectangle((int)num9, (int)(num10 % 36), num8, num12);
        //                                color2 = color3;
        //                            }
        //                        }
        //                    }
        //                    else if (num48 <= 184)
        //                    {
        //                        switch (num48)
        //                        {
        //                            case 100:
        //                                if (num9 / 36 == 0)
        //                                {
        //                                    num49 = (int)(num10 / 36);
        //                                    if (num49 == 27)
        //                                    {
        //                                        texture2D = Main.glowMaskTexture[68];
        //                                        empty = new Microsoft.Xna.Framework.Rectangle((int)num9, (int)(num10 % 36), num8, num12);
        //                                        color2 = color3;
        //                                    }
        //                                }
        //                                break;
        //                            case 101:
        //                                num49 = (int)(num9 / 54);
        //                                if (num49 == 28)
        //                                {
        //                                    texture2D = Main.glowMaskTexture[60];
        //                                    empty = new Microsoft.Xna.Framework.Rectangle((int)(num9 % 54), (int)num10, num8, num12);
        //                                    color2 = color3;
        //                                }
        //                                if (num49 == 29)
        //                                {
        //                                    texture2D = Main.glowMaskTexture[115];
        //                                    empty = new Microsoft.Xna.Framework.Rectangle((int)(num9 % 54), (int)num10, num8, num12);
        //                                    color2 = color4;
        //                                }
        //                                break;
        //                            case 102:
        //                            case 103:
        //                                break;
        //                            case 104:
        //                                num49 = (int)(num9 / 36);
        //                                if (num49 == 24)
        //                                {
        //                                    texture2D = Main.glowMaskTexture[51];
        //                                    empty = new Microsoft.Xna.Framework.Rectangle((int)(num9 % 36), (int)num10, num8, num12);
        //                                    color2 = color3;
        //                                }
        //                                if (num49 == 25)
        //                                {
        //                                    texture2D = Main.glowMaskTexture[118];
        //                                    empty = new Microsoft.Xna.Framework.Rectangle((int)(num9 % 36), (int)num10, num8, num12);
        //                                    color2 = color4;
        //                                }
        //                                break;
        //                            default:
        //                                if (num48 != 172)
        //                                {
        //                                    if (num48 == 184)
        //                                    {
        //                                        if (Tile.frameX == 110)
        //                                        {
        //                                            texture2D = Main.glowMaskTexture[127];
        //                                            empty = new Microsoft.Xna.Framework.Rectangle((int)num9, (int)num10, num8, num12);
        //                                            color2 = color5;
        //                                        }
        //                                    }
        //                                }
        //                                else
        //                                {
        //                                    num49 = (int)(num10 / 38);
        //                                    if (num49 == 28)
        //                                    {
        //                                        texture2D = Main.glowMaskTexture[88];
        //                                        empty = new Microsoft.Xna.Framework.Rectangle((int)num9, (int)(num10 % 38), num8, num12);
        //                                        color2 = color3;
        //                                    }
        //                                    if (num49 == 29)
        //                                    {
        //                                        texture2D = Main.glowMaskTexture[122];
        //                                        empty = new Microsoft.Xna.Framework.Rectangle((int)num9, (int)(num10 % 38), num8, num12);
        //                                        color2 = color4;
        //                                    }
        //                                }
        //                                break;
        //                        }
        //                    }
        //                    else
        //                    {
        //                        if (num48 != 441)
        //                        {
        //                            if (num48 == 463)
        //                            {
        //                                texture2D = Main.glowMaskTexture[243];
        //                                empty = new Microsoft.Xna.Framework.Rectangle((int)num9, (int)num10 + num15, num8, num12);
        //                                color2 = new Microsoft.Xna.Framework.Color(127, 127, 127, 0);
        //                                goto IL_1EDE;
        //                            }
        //                            switch (num48)
        //                            {
        //                                case 467:
        //                                    goto IL_1ABF;
        //                                case 468:
        //                                    break;
        //                                default:
        //                                    goto IL_1EDE;
        //                            }
        //                        }
        //                        num49 = (int)(num9 / 36);
        //                        if (num49 == 48)
        //                        {
        //                            texture2D = Main.glowMaskTexture[56];
        //                            empty = new Microsoft.Xna.Framework.Rectangle((int)(num9 % 36), (int)num10, num8, num12);
        //                            color2 = color3;
        //                        }
        //                        if (num49 == 49)
        //                        {
        //                            texture2D = Main.glowMaskTexture[117];
        //                            empty = new Microsoft.Xna.Framework.Rectangle((int)(num9 % 36), (int)num10, num8, num12);
        //                            color2 = color4;
        //                        }
        //                    }
        //                    IL_1EDE:
        //                    Texture2D texture2D2 = null;
        //                    Microsoft.Xna.Framework.Rectangle empty2 = Microsoft.Xna.Framework.Rectangle.Empty;
        //                    Microsoft.Xna.Framework.Color transparent = Microsoft.Xna.Framework.Color.Transparent;
        //                    if (TileID.Sets.HasOutlines[(int)type] && Collision.InTileBounds(j, i, Main.TileInteractionLX, Main.TileInteractionLY, Main.TileInteractionHX, Main.TileInteractionHY) && Main.SmartInteractTileCoords.Contains(new Microsoft.Xna.Framework.Point(j, i)))
        //                    {
        //                        int num50 = (int)((color.R + color.G + color.B) / 3);
        //                        bool flag3 = false;
        //                        if (Main.SmartInteractTileCoordsSelected.Contains(new Microsoft.Xna.Framework.Point(j, i)))
        //                        {
        //                            flag3 = true;
        //                        }
        //                        if (num50 > 10)
        //                        {
        //                            texture2D2 = Main.highlightMaskTexture[(int)type];
        //                            if (flag3)
        //                            {
        //                                transparent = new Microsoft.Xna.Framework.Color(num50, num50, num50 / 3, num50);
        //                            }
        //                            else
        //                            {
        //                                transparent = new Microsoft.Xna.Framework.Color(num50 / 2, num50 / 2, num50 / 2, num50);
        //                            }
        //                        }
        //                    }
        //                    if (Main.player[Main.myPlayer].dangerSense)
        //                    {
        //                        bool flag4 = false || type == 135 || type == 137 || type == 138 || type == 141 || type == 210 || type == 442 || type == 443 || type == 444;
        //                        if (Tile.slope() == 0 && !Tile.inActive())
        //                        {
        //                            flag4 = (flag4 || type == 32 || type == 69 || type == 48 || type == 232 || type == 352 || type == 51 || type == 229);
        //                            if (!Main.player[Main.myPlayer].fireWalk)
        //                            {
        //                                flag4 = (flag4 || type == 37 || type == 58 || type == 76);
        //                            }
        //                            if (!Main.player[Main.myPlayer].iceSkate)
        //                            {
        //                                flag4 = (flag4 || type == 162);
        //                            }
        //                        }
        //                        flag4 = flag4 || TileLoader.Dangersense(j, i, type, Main.player[Main.myPlayer]);
        //                        if (flag4)
        //                        {
        //                            if (color.R < 255)
        //                            {
        //                                color.R = 255;
        //                            }
        //                            if (color.G < 50)
        //                            {
        //                                color.G = 50;
        //                            }
        //                            if (color.B < 50)
        //                            {
        //                                color.B = 50;
        //                            }
        //                            color.A = Main.mouseTextColor;
        //                            if (!Main.gamePaused && Main.instance.IsActive && Main.rand.Next(30) == 0)
        //                            {
        //                                int num51 = Dust.NewDust(new Vector2((float)(j * 16), (float)(i * 16)), 16, 16, 60, 0f, 0f, 100, default(Microsoft.Xna.Framework.Color), 0.3f);
        //                                Main.dust[num51].fadeIn = 1f;
        //                                Main.dust[num51].velocity *= 0.1f;
        //                                Main.dust[num51].noLight = true;
        //                                Main.dust[num51].noGravity = true;
        //                            }
        //                        }
        //                    }
        //                    if (Main.player[Main.myPlayer].findTreasure)
        //                    {
        //                        bool flag5 = false;
        //                        if (type == 185 && num10 == 18 && num9 >= 576 && num9 <= 882)
        //                        {
        //                            flag5 = true;
        //                        }
        //                        if (type == 186 && num9 >= 864 && num9 <= 1170)
        //                        {
        //                            flag5 = true;
        //                        }
        //                        if (flag5 || Main.tileSpelunker[(int)type] || (Main.tileAlch[(int)type] && type != 82))
        //                        {
        //                            byte b2 = 200;
        //                            byte b3 = 170;
        //                            if (color.R < b2)
        //                            {
        //                                color.R = b2;
        //                            }
        //                            if (color.G < b3)
        //                            {
        //                                color.G = b3;
        //                            }
        //                            color.A = Main.mouseTextColor;
        //                            if (!Main.gamePaused && Main.instance.IsActive && Main.rand.Next(60) == 0)
        //                            {
        //                                int num52 = Dust.NewDust(new Vector2((float)(j * 16), (float)(i * 16)), 16, 16, 204, 0f, 0f, 150, default(Microsoft.Xna.Framework.Color), 0.3f);
        //                                Main.dust[num52].fadeIn = 1f;
        //                                Main.dust[num52].velocity *= 0.1f;
        //                                Main.dust[num52].noLight = true;
        //                            }
        //                        }
        //                    }
        //                    if (!Main.gamePaused && Main.instance.IsActive && (!Lighting.UpdateEveryFrame || Main.rand.Next(4) == 0))
        //                    {
        //                        if (type == 238)
        //                        {
        //                            if (Main.rand.Next(10) == 0)
        //                            {
        //                                int num53 = Dust.NewDust(new Vector2((float)(j * 16), (float)(i * 16)), 16, 16, 168, 0f, 0f, 0, default(Microsoft.Xna.Framework.Color), 1f);
        //                                Main.dust[num53].noGravity = true;
        //                                Main.dust[num53].alpha = 200;
        //                            }
        //                        }
        //                        else if (type == 463)
        //                        {
        //                            if (num10 == 54 && num9 == 0)
        //                            {
        //                                for (int m = 0; m < 4; m++)
        //                                {
        //                                    if (Main.rand.Next(2) != 0)
        //                                    {
        //                                        Dust dust = Dust.NewDustDirect(new Vector2((float)(j * 16 + 4), (float)(i * 16)), 36, 8, 16, 0f, 0f, 0, default(Microsoft.Xna.Framework.Color), 1f);
        //                                        dust.noGravity = true;
        //                                        dust.alpha = 140;
        //                                        dust.fadeIn = 1.2f;
        //                                        dust.velocity = Vector2.Zero;
        //                                    }
        //                                }
        //                            }
        //                            if (num10 == 18 && (num9 == 0 || num9 == 36))
        //                            {
        //                                for (int n = 0; n < 1; n++)
        //                                {
        //                                    if (Main.rand.Next(13) == 0)
        //                                    {
        //                                        Dust dust = Dust.NewDustDirect(new Vector2((float)(j * 16), (float)(i * 16)), 8, 8, 274, 0f, 0f, 0, default(Microsoft.Xna.Framework.Color), 1f);
        //                                        dust.position = new Vector2((float)(j * 16 + 8), (float)(i * 16 + 8));
        //                                        Dust expr_2503_cp_0 = dust;
        //                                        expr_2503_cp_0.position.X = expr_2503_cp_0.position.X + (float)((num9 == 36) ? 4 : -4);
        //                                        dust.noGravity = true;
        //                                        dust.alpha = 128;
        //                                        dust.fadeIn = 1.2f;
        //                                        dust.noLight = true;
        //                                        dust.velocity = new Vector2(0f, Main.rand.NextFloatDirection() * 1.2f);
        //                                    }
        //                                }
        //                            }
        //                        }
        //                        if (type == 139 && Tile.frameX == 36 && Tile.frameY % 36 == 0 && (int)Main.time % 7 == 0 && Main.rand.Next(3) == 0)
        //                        {
        //                            int num54 = Main.rand.Next(570, 573);
        //                            Vector2 position2 = new Vector2((float)(j * 16 + 8), (float)(i * 16 - 8));
        //                            Vector2 velocity = new Vector2(Main.windSpeed * 2f, -0.5f);
        //                            velocity.X *= 1f + (float)Main.rand.Next(-50, 51) * 0.01f;
        //                            velocity.y *= 1f + (float)Main.rand.Next(-50, 51) * 0.01f;
        //                            if (num54 == 572)
        //                            {
        //                                position2.X -= 8f;
        //                            }
        //                            if (num54 == 571)
        //                            {
        //                                position2.X -= 4f;
        //                            }
        //                            Gore.NewGore(position2, velocity, num54, 0.8f);
        //                        }
        //                        if (type == 244 && num9 == 18 && num10 == 18 && Main.rand.Next(2) == 0)
        //                        {
        //                            if (Main.rand.Next(500) == 0)
        //                            {
        //                                Gore.NewGore(new Vector2((float)(j * 16 + 8), (float)(i * 16 + 8)), default(Vector2), 415, (float)Main.rand.Next(51, 101) * 0.01f);
        //                            }
        //                            else if (Main.rand.Next(250) == 0)
        //                            {
        //                                Gore.NewGore(new Vector2((float)(j * 16 + 8), (float)(i * 16 + 8)), default(Vector2), 414, (float)Main.rand.Next(51, 101) * 0.01f);
        //                            }
        //                            else if (Main.rand.Next(80) == 0)
        //                            {
        //                                Gore.NewGore(new Vector2((float)(j * 16 + 8), (float)(i * 16 + 8)), default(Vector2), 413, (float)Main.rand.Next(51, 101) * 0.01f);
        //                            }
        //                            else if (Main.rand.Next(10) == 0)
        //                            {
        //                                Gore.NewGore(new Vector2((float)(j * 16 + 8), (float)(i * 16 + 8)), default(Vector2), 412, (float)Main.rand.Next(51, 101) * 0.01f);
        //                            }
        //                            else if (Main.rand.Next(3) == 0)
        //                            {
        //                                Gore.NewGore(new Vector2((float)(j * 16 + 8), (float)(i * 16 + 8)), default(Vector2), 411, (float)Main.rand.Next(51, 101) * 0.01f);
        //                            }
        //                        }
        //                        if (type == 165 && num9 >= 162 && num9 <= 214 && num10 == 72 && Main.rand.Next(60) == 0)
        //                        {
        //                            int num55 = Dust.NewDust(new Vector2((float)(j * 16 + 2), (float)(i * 16 + 6)), 8, 4, 153, 0f, 0f, 0, default(Microsoft.Xna.Framework.Color), 1f);
        //                            Main.dust[num55].scale -= (float)Main.rand.Next(3) * 0.1f;
        //                            Main.dust[num55].velocity.y = 0f;
        //                            Dust expr_2939_cp_0 = Main.dust[num55];
        //                            expr_2939_cp_0.velocity.X = expr_2939_cp_0.velocity.X * 0.05f;
        //                            Main.dust[num55].alpha = 100;
        //                        }
        //                        if (type == 42 && num9 == 0)
        //                        {
        //                            int num56 = (int)(num10 / 36);
        //                            int num57 = (int)(num10 / 18 % 2);
        //                            if (num56 == 7 && num57 == 1)
        //                            {
        //                                if (Main.rand.Next(50) == 0)
        //                                {
        //                                    int num58 = Dust.NewDust(new Vector2((float)(j * 16 + 4), (float)(i * 16 + 4)), 8, 8, 58, 0f, 0f, 150, default(Microsoft.Xna.Framework.Color), 1f);
        //                                    Main.dust[num58].velocity *= 0.5f;
        //                                }
        //                                if (Main.rand.Next(100) == 0)
        //                                {
        //                                    int num59 = Gore.NewGore(new Vector2((float)(j * 16 - 2), (float)(i * 16 - 4)), default(Vector2), Main.rand.Next(16, 18), 1f);
        //                                    Main.gore[num59].scale *= 0.7f;
        //                                    Main.gore[num59].velocity *= 0.25f;
        //                                }
        //                            }
        //                            else if (num56 == 29 && num57 == 1 && Main.rand.Next(40) == 0)
        //                            {
        //                                int num60 = Dust.NewDust(new Vector2((float)(j * 16 + 4), (float)(i * 16)), 8, 8, 59, 0f, 0f, 100, default(Microsoft.Xna.Framework.Color), 1f);
        //                                if (Main.rand.Next(3) != 0)
        //                                {
        //                                    Main.dust[num60].noGravity = true;
        //                                }
        //                                Main.dust[num60].velocity *= 0.3f;
        //                                Dust expr_2B23_cp_0 = Main.dust[num60];
        //                                expr_2B23_cp_0.velocity.y = expr_2B23_cp_0.velocity.y - 1.5f;
        //                            }
        //                        }
        //                        if (type == 215 && num10 < 36 && Main.rand.Next(3) == 0 && ((Main.drawToScreen && Main.rand.Next(4) == 0) || !Main.drawToScreen) && num10 == 0)
        //                        {
        //                            int num61 = Dust.NewDust(new Vector2((float)(j * 16 + 2), (float)(i * 16 - 4)), 4, 8, 31, 0f, 0f, 100, default(Microsoft.Xna.Framework.Color), 1f);
        //                            if (num9 == 0)
        //                            {
        //                                Dust expr_2BCE_cp_0 = Main.dust[num61];
        //                                expr_2BCE_cp_0.position.X = expr_2BCE_cp_0.position.X + (float)Main.rand.Next(8);
        //                            }
        //                            if (num9 == 36)
        //                            {
        //                                Dust expr_2BF9_cp_0 = Main.dust[num61];
        //                                expr_2BF9_cp_0.position.X = expr_2BF9_cp_0.position.X - (float)Main.rand.Next(8);
        //                            }
        //                            Main.dust[num61].alpha += Main.rand.Next(100);
        //                            Main.dust[num61].velocity *= 0.2f;
        //                            Dust expr_2C5B_cp_0 = Main.dust[num61];
        //                            expr_2C5B_cp_0.velocity.y = expr_2C5B_cp_0.velocity.y - (0.5f + (float)Main.rand.Next(10) * 0.1f);
        //                            Main.dust[num61].fadeIn = 0.5f + (float)Main.rand.Next(10) * 0.1f;
        //                        }
        //                        if (type == 4 && Main.rand.Next(40) == 0 && num9 < 66)
        //                        {
        //                            int num62 = (int)(num10 / 22);
        //                            if (num62 == 0)
        //                            {
        //                                num62 = 6;
        //                            }
        //                            else if (num62 == 8)
        //                            {
        //                                num62 = 75;
        //                            }
        //                            else if (num62 == 9)
        //                            {
        //                                num62 = 135;
        //                            }
        //                            else if (num62 == 10)
        //                            {
        //                                num62 = 158;
        //                            }
        //                            else if (num62 == 11)
        //                            {
        //                                num62 = 169;
        //                            }
        //                            else if (num62 == 12)
        //                            {
        //                                num62 = 156;
        //                            }
        //                            else if (num62 == 13)
        //                            {
        //                                num62 = 234;
        //                            }
        //                            else if (num62 == 14)
        //                            {
        //                                num62 = 66;
        //                            }
        //                            else
        //                            {
        //                                num62 = 58 + num62;
        //                            }
        //                            int num63;
        //                            if (num9 == 22)
        //                            {
        //                                num63 = Dust.NewDust(new Vector2((float)(j * 16 + 6), (float)(i * 16)), 4, 4, num62, 0f, 0f, 100, default(Microsoft.Xna.Framework.Color), 1f);
        //                            }
        //                            else if (num9 == 44)
        //                            {
        //                                num63 = Dust.NewDust(new Vector2((float)(j * 16 + 2), (float)(i * 16)), 4, 4, num62, 0f, 0f, 100, default(Microsoft.Xna.Framework.Color), 1f);
        //                            }
        //                            else
        //                            {
        //                                num63 = Dust.NewDust(new Vector2((float)(j * 16 + 4), (float)(i * 16)), 4, 4, num62, 0f, 0f, 100, default(Microsoft.Xna.Framework.Color), 1f);
        //                            }
        //                            if (Main.rand.Next(3) != 0)
        //                            {
        //                                Main.dust[num63].noGravity = true;
        //                            }
        //                            Main.dust[num63].velocity *= 0.3f;
        //                            Dust expr_2E53_cp_0 = Main.dust[num63];
        //                            expr_2E53_cp_0.velocity.y = expr_2E53_cp_0.velocity.y - 1.5f;
        //                            if (num62 == 66)
        //                            {
        //                                Main.dust[num63].color = new Microsoft.Xna.Framework.Color(Main.DiscoR, Main.DiscoG, Main.DiscoB);
        //                                Main.dust[num63].noGravity = true;
        //                            }
        //                        }
        //                        if (type == 93 && Main.rand.Next(40) == 0 && num9 == 0)
        //                        {
        //                            int num64 = (int)(num10 / 54);
        //                            if (num10 / 18 % 3 == 0)
        //                            {
        //                                int num65 = num64;
        //                                if (num65 == 0)
        //                                {
        //                                    goto IL_2F27;
        //                                }
        //                                int num66;
        //                                switch (num65)
        //                                {
        //                                    case 6:
        //                                    case 7:
        //                                    case 8:
        //                                    case 10:
        //                                    case 14:
        //                                    case 15:
        //                                    case 16:
        //                                        goto IL_2F27;
        //                                    case 20:
        //                                        num66 = 59;
        //                                        goto IL_2F35;
        //                                }
        //                                num66 = -1;
        //                                IL_2F35:
        //                                if (num66 != -1)
        //                                {
        //                                    int num67 = Dust.NewDust(new Vector2((float)(j * 16 + 4), (float)(i * 16 + 2)), 4, 4, num66, 0f, 0f, 100, default(Microsoft.Xna.Framework.Color), 1f);
        //                                    if (Main.rand.Next(3) != 0)
        //                                    {
        //                                        Main.dust[num67].noGravity = true;
        //                                    }
        //                                    Main.dust[num67].velocity *= 0.3f;
        //                                    Dust expr_2FC1_cp_0 = Main.dust[num67];
        //                                    expr_2FC1_cp_0.velocity.y = expr_2FC1_cp_0.velocity.y - 1.5f;
        //                                    goto IL_2FD2;
        //                                }
        //                                goto IL_2FD2;
        //                                IL_2F27:
        //                                num66 = 6;
        //                                goto IL_2F35;
        //                            }
        //                        }
        //                        IL_2FD2:
        //                        if (type == 100 && Main.rand.Next(40) == 0 && num9 < 36)
        //                        {
        //                            int num68 = (int)(num10 / 36);
        //                            if (num10 / 18 % 2 == 0)
        //                            {
        //                                int num69;
        //                                switch (num68)
        //                                {
        //                                    case 0:
        //                                    case 2:
        //                                    case 5:
        //                                    case 7:
        //                                    case 8:
        //                                    case 10:
        //                                    case 12:
        //                                    case 14:
        //                                    case 15:
        //                                    case 16:
        //                                        num69 = 6;
        //                                        break;
        //                                    case 1:
        //                                    case 3:
        //                                    case 4:
        //                                    case 6:
        //                                    case 9:
        //                                    case 11:
        //                                    case 13:
        //                                    case 17:
        //                                    case 18:
        //                                    case 19:
        //                                        goto IL_307C;
        //                                    case 20:
        //                                        num69 = 59;
        //                                        break;
        //                                    default:
        //                                        goto IL_307C;
        //                                }
        //                                IL_307F:
        //                                if (num69 != -1)
        //                                {
        //                                    Vector2 position3;
        //                                    if (num9 == 0)
        //                                    {
        //                                        if (Main.rand.Next(3) == 0)
        //                                        {
        //                                            position3 = new Vector2((float)(j * 16 + 4), (float)(i * 16 + 2));
        //                                        }
        //                                        else
        //                                        {
        //                                            position3 = new Vector2((float)(j * 16 + 14), (float)(i * 16 + 2));
        //                                        }
        //                                    }
        //                                    else if (Main.rand.Next(3) == 0)
        //                                    {
        //                                        position3 = new Vector2((float)(j * 16 + 6), (float)(i * 16 + 2));
        //                                    }
        //                                    else
        //                                    {
        //                                        position3 = new Vector2((float)(j * 16), (float)(i * 16 + 2));
        //                                    }
        //                                    int num70 = Dust.NewDust(position3, 4, 4, num69, 0f, 0f, 100, default(Microsoft.Xna.Framework.Color), 1f);
        //                                    if (Main.rand.Next(3) != 0)
        //                                    {
        //                                        Main.dust[num70].noGravity = true;
        //                                    }
        //                                    Main.dust[num70].velocity *= 0.3f;
        //                                    Dust expr_317A_cp_0 = Main.dust[num70];
        //                                    expr_317A_cp_0.velocity.y = expr_317A_cp_0.velocity.y - 1.5f;
        //                                    goto IL_318B;
        //                                }
        //                                goto IL_318B;
        //                                IL_307C:
        //                                num69 = -1;
        //                                goto IL_307F;
        //                            }
        //                        }
        //                        IL_318B:
        //                        if (type == 98 && Main.rand.Next(40) == 0 && num10 == 0 && num9 == 0)
        //                        {
        //                            int num71 = Dust.NewDust(new Vector2((float)(j * 16 + 12), (float)(i * 16 + 2)), 4, 4, 6, 0f, 0f, 100, default(Microsoft.Xna.Framework.Color), 1f);
        //                            if (Main.rand.Next(3) != 0)
        //                            {
        //                                Main.dust[num71].noGravity = true;
        //                            }
        //                            Main.dust[num71].velocity *= 0.3f;
        //                            Dust expr_3237_cp_0 = Main.dust[num71];
        //                            expr_3237_cp_0.velocity.y = expr_3237_cp_0.velocity.y - 1.5f;
        //                        }
        //                        if (type == 49 && Main.rand.Next(2) == 0)
        //                        {
        //                            int num72 = Dust.NewDust(new Vector2((float)(j * 16 + 4), (float)(i * 16 - 4)), 4, 4, 172, 0f, 0f, 100, default(Microsoft.Xna.Framework.Color), 1f);
        //                            if (Main.rand.Next(3) == 0)
        //                            {
        //                                Main.dust[num72].scale = 0.5f;
        //                            }
        //                            else
        //                            {
        //                                Main.dust[num72].scale = 0.9f;
        //                                Main.dust[num72].noGravity = true;
        //                            }
        //                            Main.dust[num72].velocity *= 0.3f;
        //                            Dust expr_330E_cp_0 = Main.dust[num72];
        //                            expr_330E_cp_0.velocity.y = expr_330E_cp_0.velocity.y - 1.5f;
        //                        }
        //                        if (type == 372 && Main.rand.Next(2) == 0)
        //                        {
        //                            int num73 = Dust.NewDust(new Vector2((float)(j * 16 + 4), (float)(i * 16 - 4)), 4, 4, 242, 0f, 0f, 100, default(Microsoft.Xna.Framework.Color), 1f);
        //                            if (Main.rand.Next(3) == 0)
        //                            {
        //                                Main.dust[num73].scale = 0.5f;
        //                            }
        //                            else
        //                            {
        //                                Main.dust[num73].scale = 0.9f;
        //                                Main.dust[num73].noGravity = true;
        //                            }
        //                            Main.dust[num73].velocity *= 0.3f;
        //                            Dust expr_33E8_cp_0 = Main.dust[num73];
        //                            expr_33E8_cp_0.velocity.y = expr_33E8_cp_0.velocity.y - 1.5f;
        //                        }
        //                        if (type == 34 && Main.rand.Next(40) == 0 && num9 < 54)
        //                        {
        //                            int num74 = (int)(num10 / 54);
        //                            int num75 = (int)(num9 / 18 % 3);
        //                            int num76 = (int)(num10 / 18 % 3);
        //                            if (num76 == 1 && num75 != 1)
        //                            {
        //                                int num65 = num74;
        //                                int num77;
        //                                switch (num65)
        //                                {
        //                                    case 0:
        //                                    case 1:
        //                                    case 2:
        //                                    case 3:
        //                                    case 4:
        //                                    case 5:
        //                                    case 12:
        //                                    case 13:
        //                                    case 16:
        //                                        goto IL_34BA;
        //                                    case 6:
        //                                    case 7:
        //                                    case 8:
        //                                    case 9:
        //                                    case 10:
        //                                    case 11:
        //                                    case 14:
        //                                    case 15:
        //                                        goto IL_34C5;
        //                                    default:
        //                                        switch (num65)
        //                                        {
        //                                            case 19:
        //                                            case 21:
        //                                                goto IL_34BA;
        //                                            case 20:
        //                                                goto IL_34C5;
        //                                            default:
        //                                                if (num65 != 25)
        //                                                {
        //                                                    goto IL_34C5;
        //                                                }
        //                                                num77 = 59;
        //                                                break;
        //                                        }
        //                                        break;
        //                                }
        //                                IL_34C8:
        //                                if (num77 != -1)
        //                                {
        //                                    int num78 = Dust.NewDust(new Vector2((float)(j * 16), (float)(i * 16 + 2)), 14, 6, num77, 0f, 0f, 100, default(Microsoft.Xna.Framework.Color), 1f);
        //                                    if (Main.rand.Next(3) != 0)
        //                                    {
        //                                        Main.dust[num78].noGravity = true;
        //                                    }
        //                                    Main.dust[num78].velocity *= 0.3f;
        //                                    Dust expr_3553_cp_0 = Main.dust[num78];
        //                                    expr_3553_cp_0.velocity.y = expr_3553_cp_0.velocity.y - 1.5f;
        //                                    goto IL_3564;
        //                                }
        //                                goto IL_3564;
        //                                IL_34C5:
        //                                num77 = -1;
        //                                goto IL_34C8;
        //                                IL_34BA:
        //                                num77 = 6;
        //                                goto IL_34C8;
        //                            }
        //                        }
        //                        IL_3564:
        //                        if (type == 22 && Main.rand.Next(400) == 0)
        //                        {
        //                            Dust.NewDust(new Vector2((float)(j * 16), (float)(i * 16)), 16, 16, 14, 0f, 0f, 0, default(Microsoft.Xna.Framework.Color), 1f);
        //                        }
        //                        else if ((type == 23 || type == 24 || type == 32) && Main.rand.Next(500) == 0)
        //                        {
        //                            Dust.NewDust(new Vector2((float)(j * 16), (float)(i * 16)), 16, 16, 14, 0f, 0f, 0, default(Microsoft.Xna.Framework.Color), 1f);
        //                        }
        //                        else if (type == 25 && Main.rand.Next(700) == 0)
        //                        {
        //                            Dust.NewDust(new Vector2((float)(j * 16), (float)(i * 16)), 16, 16, 14, 0f, 0f, 0, default(Microsoft.Xna.Framework.Color), 1f);
        //                        }
        //                        else if (type == 112 && Main.rand.Next(700) == 0)
        //                        {
        //                            Dust.NewDust(new Vector2((float)(j * 16), (float)(i * 16)), 16, 16, 14, 0f, 0f, 0, default(Microsoft.Xna.Framework.Color), 1f);
        //                        }
        //                        else if (type == 31 && Main.rand.Next(20) == 0)
        //                        {
        //                            if (num9 >= 36)
        //                            {
        //                                int num79 = Dust.NewDust(new Vector2((float)(j * 16), (float)(i * 16)), 16, 16, 5, 0f, 0f, 100, default(Microsoft.Xna.Framework.Color), 1f);
        //                                Main.dust[num79].velocity.y = 0f;
        //                                Dust expr_374C_cp_0 = Main.dust[num79];
        //                                expr_374C_cp_0.velocity.X = expr_374C_cp_0.velocity.X * 0.3f;
        //                            }
        //                            else
        //                            {
        //                                Dust.NewDust(new Vector2((float)(j * 16), (float)(i * 16)), 16, 16, 14, 0f, 0f, 100, default(Microsoft.Xna.Framework.Color), 1f);
        //                            }
        //                        }
        //                        else if (type == 26 && Main.rand.Next(20) == 0)
        //                        {
        //                            if (num9 >= 54)
        //                            {
        //                                int num80 = Dust.NewDust(new Vector2((float)(j * 16), (float)(i * 16)), 16, 16, 5, 0f, 0f, 100, default(Microsoft.Xna.Framework.Color), 1f);
        //                                Main.dust[num80].scale = 1.5f;
        //                                Main.dust[num80].noGravity = true;
        //                                Main.dust[num80].velocity *= 0.75f;
        //                            }
        //                            else
        //                            {
        //                                Dust.NewDust(new Vector2((float)(j * 16), (float)(i * 16)), 16, 16, 14, 0f, 0f, 100, default(Microsoft.Xna.Framework.Color), 1f);
        //                            }
        //                        }
        //                        else if ((type == 71 || type == 72) && Main.rand.Next(500) == 0)
        //                        {
        //                            Dust.NewDust(new Vector2((float)(j * 16), (float)(i * 16)), 16, 16, 41, 0f, 0f, 250, default(Microsoft.Xna.Framework.Color), 0.8f);
        //                        }
        //                        else if ((type == 17 || type == 77 || type == 133) && Main.rand.Next(40) == 0)
        //                        {
        //                            if (num9 == 18 & num10 == 18)
        //                            {
        //                                int num81 = Dust.NewDust(new Vector2((float)(j * 16 - 4), (float)(i * 16 - 6)), 8, 6, 6, 0f, 0f, 100, default(Microsoft.Xna.Framework.Color), 1f);
        //                                if (Main.rand.Next(3) != 0)
        //                                {
        //                                    Main.dust[num81].noGravity = true;
        //                                }
        //                            }
        //                        }
        //                        else if (type == 405 && Main.rand.Next(20) == 0)
        //                        {
        //                            if (num9 == 18 & num10 == 18)
        //                            {
        //                                int num82 = Dust.NewDust(new Vector2((float)(j * 16 - 4), (float)(i * 16 - 6)), 24, 10, 6, 0f, 0f, 100, default(Microsoft.Xna.Framework.Color), 1f);
        //                                if (Main.rand.Next(5) != 0)
        //                                {
        //                                    Main.dust[num82].noGravity = true;
        //                                }
        //                            }
        //                        }
        //                        else if (type == 452 && num10 == 0 && num9 == 0 && Main.rand.Next(3) == 0)
        //                        {
        //                            Vector2 position4 = new Vector2((float)(j * 16 + 16), (float)(i * 16 + 8));
        //                            Vector2 velocity2 = new Vector2(0f, 0f);
        //                            if (Main.windSpeed < 0f)
        //                            {
        //                                velocity2.X = -Main.windSpeed;
        //                            }
        //                            int num83 = Main.tileFrame[(int)type];
        //                            int type2 = 907 + num83 / 5;
        //                            if (Main.rand.Next(2) == 0)
        //                            {
        //                                Gore.NewGore(position4, velocity2, type2, Main.rand.NextFloat() * 0.4f + 0.4f);
        //                            }
        //                        }
        //                        else if (type == 406 && num10 == 54 && num9 == 0 && Main.rand.Next(3) == 0)
        //                        {
        //                            Vector2 position5 = new Vector2((float)(j * 16 + 16), (float)(i * 16 + 8));
        //                            Vector2 velocity3 = new Vector2(0f, 0f);
        //                            if (Main.windSpeed < 0f)
        //                            {
        //                                velocity3.X = -Main.windSpeed;
        //                            }
        //                            int type3 = Main.rand.Next(825, 828);
        //                            if (Main.rand.Next(4) == 0)
        //                            {
        //                                Gore.NewGore(position5, velocity3, type3, Main.rand.NextFloat() * 0.2f + 0.2f);
        //                            }
        //                            else if (Main.rand.Next(2) == 0)
        //                            {
        //                                Gore.NewGore(position5, velocity3, type3, Main.rand.NextFloat() * 0.3f + 0.3f);
        //                            }
        //                            else
        //                            {
        //                                Gore.NewGore(position5, velocity3, type3, Main.rand.NextFloat() * 0.4f + 0.4f);
        //                            }
        //                        }
        //                        else if (type == 37 && Main.rand.Next(250) == 0)
        //                        {
        //                            int num84 = Dust.NewDust(new Vector2((float)(j * 16), (float)(i * 16)), 16, 16, 6, 0f, 0f, 0, default(Microsoft.Xna.Framework.Color), (float)Main.rand.Next(3));
        //                            if (Main.dust[num84].scale > 1f)
        //                            {
        //                                Main.dust[num84].noGravity = true;
        //                            }
        //                        }
        //                        else if ((type == 58 || type == 76) && Main.rand.Next(250) == 0)
        //                        {
        //                            int num85 = Dust.NewDust(new Vector2((float)(j * 16), (float)(i * 16)), 16, 16, 6, 0f, 0f, 0, default(Microsoft.Xna.Framework.Color), (float)Main.rand.Next(3));
        //                            if (Main.dust[num85].scale > 1f)
        //                            {
        //                                Main.dust[num85].noGravity = true;
        //                            }
        //                            Main.dust[num85].noLight = true;
        //                        }
        //                        else if (type == 61)
        //                        {
        //                            if (num9 == 144)
        //                            {
        //                                if (Main.rand.Next(60) == 0)
        //                                {
        //                                    int num86 = Dust.NewDust(new Vector2((float)(j * 16), (float)(i * 16)), 16, 16, 44, 0f, 0f, 250, default(Microsoft.Xna.Framework.Color), 0.4f);
        //                                    Main.dust[num86].fadeIn = 0.7f;
        //                                }
        //                                color.A = (byte)(245f - (float)Main.mouseTextColor * 1.5f);
        //                                color.R = (byte)(245f - (float)Main.mouseTextColor * 1.5f);
        //                                color.B = (byte)(245f - (float)Main.mouseTextColor * 1.5f);
        //                                color.G = (byte)(245f - (float)Main.mouseTextColor * 1.5f);
        //                            }
        //                        }
        //                        else if (Main.tileShine[(int)type] > 0)
        //                        {
        //                            Main.tileShine[211] = 500;
        //                            if (color.R > 20 || color.B > 20 || color.G > 20)
        //                            {
        //                                int num87 = (int)color.R;
        //                                if ((int)color.G > num87)
        //                                {
        //                                    num87 = (int)color.G;
        //                                }
        //                                if ((int)color.B > num87)
        //                                {
        //                                    num87 = (int)color.B;
        //                                }
        //                                num87 /= 30;
        //                                if (Main.rand.Next(Main.tileShine[(int)type]) < num87 && (type != 21 || (num9 >= 36 && num9 < 180) || (num9 >= 396 && num9 <= 409)) && type != 467)
        //                                {
        //                                    Microsoft.Xna.Framework.Color white = Microsoft.Xna.Framework.Color.White;
        //                                    if (type == 178)
        //                                    {
        //                                        int num88 = (int)(num9 / 18);
        //                                        if (num88 == 0)
        //                                        {
        //                                            white = new Microsoft.Xna.Framework.Color(255, 0, 255, 255);
        //                                        }
        //                                        else if (num88 == 1)
        //                                        {
        //                                            white = new Microsoft.Xna.Framework.Color(255, 255, 0, 255);
        //                                        }
        //                                        else if (num88 == 2)
        //                                        {
        //                                            white = new Microsoft.Xna.Framework.Color(0, 0, 255, 255);
        //                                        }
        //                                        else if (num88 == 3)
        //                                        {
        //                                            white = new Microsoft.Xna.Framework.Color(0, 255, 0, 255);
        //                                        }
        //                                        else if (num88 == 4)
        //                                        {
        //                                            white = new Microsoft.Xna.Framework.Color(255, 0, 0, 255);
        //                                        }
        //                                        else if (num88 == 5)
        //                                        {
        //                                            white = new Microsoft.Xna.Framework.Color(255, 255, 255, 255);
        //                                        }
        //                                        else if (num88 == 6)
        //                                        {
        //                                            white = new Microsoft.Xna.Framework.Color(255, 255, 0, 255);
        //                                        }
        //                                        int num89 = Dust.NewDust(new Vector2((float)(j * 16), (float)(i * 16)), 16, 16, 43, 0f, 0f, 254, white, 0.5f);
        //                                        Main.dust[num89].velocity *= 0f;
        //                                    }
        //                                    else
        //                                    {
        //                                        if (type == 63)
        //                                        {
        //                                            white = new Microsoft.Xna.Framework.Color(0, 0, 255, 255);
        //                                        }
        //                                        if (type == 64)
        //                                        {
        //                                            white = new Microsoft.Xna.Framework.Color(255, 0, 0, 255);
        //                                        }
        //                                        if (type == 65)
        //                                        {
        //                                            white = new Microsoft.Xna.Framework.Color(0, 255, 0, 255);
        //                                        }
        //                                        if (type == 66)
        //                                        {
        //                                            white = new Microsoft.Xna.Framework.Color(255, 255, 0, 255);
        //                                        }
        //                                        if (type == 67)
        //                                        {
        //                                            white = new Microsoft.Xna.Framework.Color(255, 0, 255, 255);
        //                                        }
        //                                        if (type == 68)
        //                                        {
        //                                            white = new Microsoft.Xna.Framework.Color(255, 255, 255, 255);
        //                                        }
        //                                        if (type == 12)
        //                                        {
        //                                            white = new Microsoft.Xna.Framework.Color(255, 0, 0, 255);
        //                                        }
        //                                        if (type == 204)
        //                                        {
        //                                            white = new Microsoft.Xna.Framework.Color(255, 0, 0, 255);
        //                                        }
        //                                        if (type == 211)
        //                                        {
        //                                            white = new Microsoft.Xna.Framework.Color(50, 255, 100, 255);
        //                                        }
        //                                        // TODO, ModTile hook for Shine color.
        //                                        int num90 = Dust.NewDust(new Vector2((float)(j * 16), (float)(i * 16)), 16, 16, 43, 0f, 0f, 254, white, 0.5f);
        //                                        Main.dust[num90].velocity *= 0f;
        //                                    }
        //                                }
        //                            }
        //                        }
        //                    }
        //                    if (TileID.Sets.BasicChest[(int)type])
        //                    {
        //                        Microsoft.Xna.Framework.Point key = new Microsoft.Xna.Framework.Point(j, i);
        //                        if (num9 % 36 != 0)
        //                        {
        //                            key.X--;
        //                        }
        //                        if (num10 % 36 != 0)
        //                        {
        //                            key.y--;
        //                        }
        //                        if (!dictionary.ContainsKey(key))
        //                        {
        //                            dictionary[key] = Chest.FindChest(key.X, key.y);
        //                        }
        //                        int num91 = (int)(num9 / 18);
        //                        int num92 = (int)(num10 / 18);
        //                        int num93 = (int)(num9 / 36);
        //                        int num94 = num91 * 18;
        //                        num16 = num94 - (int)num9;
        //                        int num95 = num92 * 18;
        //                        if (dictionary[key] != -1)
        //                        {
        //                            int frame = Main.chest[dictionary[key]].frame;
        //                            if (frame == 1)
        //                            {
        //                                num95 += 38;
        //                            }
        //                            if (frame == 2)
        //                            {
        //                                num95 += 76;
        //                            }
        //                        }
        //                        num15 = num95 - (int)num10;
        //                        if (num92 != 0)
        //                        {
        //                            num12 = 18;
        //                        }
        //                        if (type == 21 && (num93 == 48 || num93 == 49))
        //                        {
        //                            empty = new Microsoft.Xna.Framework.Rectangle(16 * (num91 % 2), (int)num10 + num15, num8, num12);
        //                        }
        //                    }
        //                    if (type == 378)
        //                    {
        //                        Microsoft.Xna.Framework.Point key2 = new Microsoft.Xna.Framework.Point(j, i);
        //                        if (num9 % 36 != 0)
        //                        {
        //                            key2.X--;
        //                        }
        //                        if (num10 % 54 != 0)
        //                        {
        //                            key2.y -= (int)(num10 / 18);
        //                        }
        //                        if (!dictionary2.ContainsKey(key2))
        //                        {
        //                            dictionary2[key2] = TETrainingDummy.Find(key2.X, key2.y);
        //                        }
        //                        if (dictionary2[key2] != -1)
        //                        {
        //                            int num96 = ((TETrainingDummy)TileEntity.ByID[dictionary2[key2]]).npc;
        //                            if (num96 != -1)
        //                            {
        //                                int num97 = Main.npc[num96].frame.y / 55;
        //                                num97 *= 54;
        //                                num97 += (int)num10;
        //                                num15 = num97 - (int)num10;
        //                            }
        //                        }
        //                    }
        //                    if (type == 395)
        //                    {
        //                        Microsoft.Xna.Framework.Point key3 = new Microsoft.Xna.Framework.Point(j, i);
        //                        if (num9 % 36 != 0)
        //                        {
        //                            key3.X--;
        //                        }
        //                        if (num10 % 36 != 0)
        //                        {
        //                            key3.y--;
        //                        }
        //                        if (!dictionary3.ContainsKey(key3))
        //                        {
        //                            dictionary3[key3] = TEItemFrame.Find(key3.X, key3.y);
        //                            if (dictionary3[key3] != -1)
        //                            {
        //                                Main.specX[num3] = key3.X;
        //                                Main.specY[num3] = key3.y;
        //                                num3++;
        //                            }
        //                        }
        //                    }
        //                    if (type == 269 || type == 128)
        //                    {
        //                        int num98 = (int)(num10 / 18);
        //                        if (num98 == 2)
        //                        {
        //                            if (num9 >= 100)
        //                            {
        //                                bool flag6 = false;
        //                                int num99 = (int)Main.Tile[j, i - 1].frameX;
        //                                if (num99 >= 100)
        //                                {
        //                                    int num100 = 0;
        //                                    while (num99 >= 100)
        //                                    {
        //                                        num100++;
        //                                        num99 -= 100;
        //                                    }
        //                                    int num65 = num100;
        //                                    if (num65 <= 36)
        //                                    {
        //                                        if (num65 != 15 && num65 != 36)
        //                                        {
        //                                            goto IL_4453;
        //                                        }
        //                                    }
        //                                    else
        //                                    {
        //                                        switch (num65)
        //                                        {
        //                                            case 41:
        //                                            case 42:
        //                                                break;
        //                                            default:
        //                                                switch (num65)
        //                                                {
        //                                                    case 58:
        //                                                    case 59:
        //                                                    case 60:
        //                                                    case 61:
        //                                                    case 62:
        //                                                    case 63:
        //                                                        break;
        //                                                    default:
        //                                                        goto IL_4453;
        //                                                }
        //                                                break;
        //                                        }
        //                                    }
        //                                    flag6 = true;
        //                                }
        //                                IL_4453:
        //                                if (!flag6)
        //                                {
        //                                    Main.specX[num3] = j;
        //                                    Main.specY[num3] = i;
        //                                    num3++;
        //                                }
        //                            }
        //                            if (Main.Tile[j, i - 1].frameX >= 100)
        //                            {
        //                                Main.specX[num3] = j;
        //                                Main.specY[num3] = i - 1;
        //                                num3++;
        //                            }
        //                            if (Main.Tile[j, i - 2].frameX >= 100)
        //                            {
        //                                Main.specX[num3] = j;
        //                                Main.specY[num3] = i - 2;
        //                                num3++;
        //                            }
        //                        }
        //                    }
        //                    if (type == 5 && num10 >= 198 && num9 >= 22)
        //                    {
        //                        Main.specX[num3] = j;
        //                        Main.specY[num3] = i;
        //                        num3++;
        //                    }
        //                    if (type == 323 && num9 <= 132 && num9 >= 88)
        //                    {
        //                        Main.specX[num3] = j;
        //                        Main.specY[num3] = i;
        //                        num3++;
        //                    }
        //                    if (type == 412 && num9 == 0 && num10 == 0)
        //                    {
        //                        Main.specX[num3] = j;
        //                        Main.specY[num3] = i;
        //                        num3++;
        //                    }
        //                    if (type == 237 && num9 == 18 && num10 == 0)
        //                    {
        //                        Main.specX[num3] = j;
        //                        Main.specY[num3] = i;
        //                        num3++;
        //                    }
        //                    if (type == 334)
        //                    {
        //                        int num101 = (int)(num10 / 18);
        //                        if (num101 == 1 && num9 >= 5000)
        //                        {
        //                            int num102 = (int)Main.Tile[j, i].frameX;
        //                            int num103 = 0;
        //                            while (num102 >= 5000)
        //                            {
        //                                num103++;
        //                                num102 -= 5000;
        //                            }
        //                            if (num103 == 1 || num103 == 4)
        //                            {
        //                                Main.specX[num3] = j;
        //                                Main.specY[num3] = i;
        //                                num3++;
        //                            }
        //                        }
        //                    }
        //                    if (type == 5 && num10 >= 198 && num9 >= 22)
        //                    {
        //                        Main.specX[num3] = j;
        //                        Main.specY[num3] = i;
        //                        num3++;
        //                    }
        //                    if (type == 323 && num9 <= 132 && num9 >= 88)
        //                    {
        //                        Main.specX[num3] = j;
        //                        Main.specY[num3] = i;
        //                        num3++;
        //                    }
        //                    if (type == 412 && num9 == 0 && num10 == 0)
        //                    {
        //                        Main.specX[num3] = j;
        //                        Main.specY[num3] = i;
        //                        num3++;
        //                    }
        //                    if (type == 237 && num9 == 18 && num10 == 0)
        //                    {
        //                        Main.specX[num3] = j;
        //                        Main.specY[num3] = i;
        //                        num3++;
        //                    }
        //                    if (type == 72 && num9 >= 36)
        //                    {
        //                        int num104 = 0;
        //                        if (num10 == 18)
        //                        {
        //                            num104 = 1;
        //                        }
        //                        else if (num10 == 36)
        //                        {
        //                            num104 = 2;
        //                        }
        //                        Main.spriteBatch.Draw(Main.shroomCapTexture, new Vector2((float)(j * 16 - (int)Main.screenPosition.X - 22), (float)(i * 16 - (int)Main.screenPosition.y - 26)) + zero, new Microsoft.Xna.Framework.Rectangle?(new Microsoft.Xna.Framework.Rectangle(num104 * 62, 0, 60, 42)), Lighting.GetColor(j, i), 0f, default(Vector2), 1f, effects, 0f);
        //                    }
        //                    TileLoader.DrawEffects(j, i, type, Main.spriteBatch, ref color, ref num3);
        //                    if (color.R >= 1 || color.G >= 1 || color.B >= 1)
        //                    {
        //                        Tile tile2 = Main.Tile[j + 1, i];
        //                        Tile tile3 = Main.Tile[j - 1, i];
        //                        Tile tile4 = Main.Tile[j, i - 1];
        //                        Tile tile5 = Main.Tile[j, i + 1];
        //                        LiquidRef tile2r = LiquidCore.grid[j + 1, i];
        //                        LiquidRef tile3r = LiquidCore.grid[j - 1, i];
        //                        LiquidRef tile4r = LiquidCore.grid[j, i - 1];
        //                        LiquidRef tile5r = LiquidCore.grid[j, i + 1];
        //                        LiquidRef tiler = LiquidCore.grid[j, i];
        //                        if (tile2 == null)
        //                        {
        //                            tile2 = new Tile();
        //                            Main.Tile[j + 1, i] = tile2;
        //                        }
        //                        if (tile3 == null)
        //                        {
        //                            tile3 = new Tile();
        //                            Main.Tile[j - 1, i] = tile3;
        //                        }
        //                        if (tile4 == null)
        //                        {
        //                            tile4 = new Tile();
        //                            Main.Tile[j, i - 1] = tile4;
        //                        }
        //                        if (tile5 == null)
        //                        {
        //                            tile5 = new Tile();
        //                            Main.Tile[j, i + 1] = tile5;
        //                        }
        //                        if (solidOnly && flag && !Tile.inActive() && !Main.tileSolidTop[(int)type])
        //                        {
        //                            bool flag7 = false;
        //                            if (Tile.halfBrick())
        //                            {
        //                                int num105 = 160;
        //                                if (((int)tile3.liquid > num105 || (int)tile2.liquid > num105) && Main.instance.waterfallManager.CheckForWaterfall(j, i))
        //                                {
        //                                    flag7 = true;
        //                                }
        //                            }
        //                            if (!flag7)
        //                            {
        //                                int num106 = 0;
        //                                bool flag8 = false;
        //                                bool flag9 = false;
        //                                bool flag10 = false;
        //                                bool flag11 = false;
        //                                int liquidTextureIndex = 0;
        //                                bool flag12 = false;
        //                                int num108 = (int)Tile.slope();

        //                                // Search note: Slope liquid drawing

        //                                if (!tile3r.NoLiquid() && num108 != 1 && num108 != 3)
        //                                {
        //                                    flag8 = true;
        //                                    switch (tile3r.liquidType)
        //                                    {
        //                                        case 0:
        //                                            flag12 = true;
        //                                            break;
        //                                        case 1:
        //                                            liquidTextureIndex = 1;
        //                                            break;
        //                                        case 2:
        //                                            liquidTextureIndex = 11;
        //                                            break;
        //                                        default:
                                                    
        //                                            liquidTextureIndex = LiquidCore.liquidGrid[j - 1, i].data + 8;
        //                                            break;
        //                                    }
        //                                    if ((int)tile3.liquid > num106)
        //                                    {
        //                                        num106 = (int)tile3.liquid;
        //                                    }
        //                                }
        //                                if (!tile2r.NoLiquid() && num108 != 2 && num108 != 4)
        //                                {
        //                                    flag9 = true;
        //                                    switch (tile2r.liquidType)
        //                                    {
        //                                        case 0:
        //                                            flag12 = true;
        //                                            break;
        //                                        case 1:
        //                                            liquidTextureIndex = 1;
        //                                            break;
        //                                        case 2:
        //                                            liquidTextureIndex = 11;
        //                                            break;
        //                                        default:
        //                                            liquidTextureIndex = LiquidCore.liquidGrid[j + 1, i].data + 8;
        //                                            break;
        //                                    }
        //                                    if ((int)tile2.liquid > num106)
        //                                    {
        //                                        num106 = (int)tile2.liquid;
        //                                    }
        //                                }
        //                                if (!tile4r.NoLiquid()&& num108 != 3 && num108 != 4)
        //                                {
        //                                    flag10 = true;
        //                                    switch (tile4r.liquidType)
        //                                    {
        //                                        case 0:
        //                                            flag12 = true;
        //                                            break;
        //                                        case 1:
        //                                            liquidTextureIndex = 1;
        //                                            break;
        //                                        case 2:
        //                                            liquidTextureIndex = 11;
        //                                            break;
        //                                        default:
        //                                            liquidTextureIndex = LiquidCore.liquidGrid[j, i - 1].data + 8;
        //                                            break;
        //                                    }
        //                                }

                                        
        //                                if (!tile5r.NoLiquid() && num108 != 1 && num108 != 2)
        //                                {
        //                                    if (tile5.liquid > 240)
        //                                    {
        //                                        flag11 = true;
        //                                    }
        //                                    switch (tile5r.liquidType)
        //                                    {
        //                                        case 0:
        //                                            flag12 = true;
        //                                            break;
        //                                        case 1:
        //                                            liquidTextureIndex = 1;
        //                                            break;
        //                                        case 2:
        //                                            liquidTextureIndex = 11;
        //                                            break;
        //                                        default:
        //                                            liquidTextureIndex = LiquidCore.liquidGrid[j, i + 1].data + 8;
        //                                            break;
        //                                    }
        //                                }
        //                                if (waterStyleOverride != -1)
        //                                {
        //                                    Main.waterStyle = waterStyleOverride;
        //                                }
        //                                if (liquidTextureIndex == 0)
        //                                {
        //                                    liquidTextureIndex = Main.waterStyle;
        //                                }
        //                                if ((flag10 || flag11 || flag8 || flag9) && (!flag12 || liquidTextureIndex != 1))
        //                                {
        //                                    Microsoft.Xna.Framework.Color color6 = Lighting.GetColor(j, i);
        //                                    Vector2 value2 = new Vector2((float)(j * 16), (float)(i * 16));
        //                                    Microsoft.Xna.Framework.Rectangle value3 = new Microsoft.Xna.Framework.Rectangle(0, 4, 16, 16);
        //                                    if (flag11 && (flag8 || flag9))
        //                                    {
        //                                        flag8 = true;
        //                                        flag9 = true;
        //                                    }
        //                                    if ((!flag10 || (!flag8 && !flag9)) && (!flag11 || !flag10))
        //                                    {
        //                                        if (flag10)
        //                                        {
        //                                            value3 = new Microsoft.Xna.Framework.Rectangle(0, 4, 16, 4);
        //                                            if (Tile.halfBrick() || Tile.slope() != 0)
        //                                            {
        //                                                value3 = new Microsoft.Xna.Framework.Rectangle(0, 4, 16, 12);
        //                                            }
        //                                        }
        //                                        else if (flag11 && !flag8 && !flag9)
        //                                        {
        //                                            value2 = new Vector2((float)(j * 16), (float)(i * 16 + 12));
        //                                            value3 = new Microsoft.Xna.Framework.Rectangle(0, 4, 16, 4);
        //                                        }
        //                                        else
        //                                        {
        //                                            float num109 = (float)(256 - num106);
        //                                            num109 /= 32f;
        //                                            int y = 4;
        //                                            if (tile4.liquid == 0 && !WorldGen.SolidTile(j, i - 1))
        //                                            {
        //                                                y = 0;
        //                                            }
        //                                            if ((flag8 && flag9) || Tile.halfBrick() || Tile.slope() != 0)
        //                                            {
        //                                                value2 = new Vector2((float)(j * 16), (float)(i * 16 + (int)num109 * 2));
        //                                                value3 = new Microsoft.Xna.Framework.Rectangle(0, y, 16, 16 - (int)num109 * 2);
        //                                            }
        //                                            else if (flag8)
        //                                            {
        //                                                value2 = new Vector2((float)(j * 16), (float)(i * 16 + (int)num109 * 2));
        //                                                value3 = new Microsoft.Xna.Framework.Rectangle(0, y, 4, 16 - (int)num109 * 2);
        //                                            }
        //                                            else
        //                                            {
        //                                                value2 = new Vector2((float)(j * 16 + 12), (float)(i * 16 + (int)num109 * 2));
        //                                                value3 = new Microsoft.Xna.Framework.Rectangle(0, y, 4, 16 - (int)num109 * 2);
        //                                            }
        //                                        }
        //                                    }
        //                                    float Opacity = 0.5f;
        //                                    if (liquidTextureIndex == 1)
        //                                    {
        //                                        Opacity = 1f;
        //                                    }
        //                                    else if (liquidTextureIndex == 11)
        //                                    {
        //                                        Opacity *= 1.7f;
        //                                        if (Opacity > 1f)
        //                                        {
        //                                            Opacity = 1f;
        //                                        }
        //                                    }
        //                                    if ((double)i < Main.worldSurface || Opacity > 1f)
        //                                    {
        //                                        Opacity = 1f;
        //                                        if (tile4.wall > 0 || tile3.wall > 0 || tile2.wall > 0 || tile5.wall > 0)
        //                                        {
        //                                            Opacity = 0.65f;
        //                                        }
        //                                        if (Tile.wall > 0)
        //                                        {
        //                                            Opacity = 0.5f;
        //                                        }
        //                                    }
        //                                    if (Tile.halfBrick() && tile4.liquid > 0 && Tile.wall > 0)
        //                                    {
        //                                        Opacity = 0f;
        //                                    }

        //                                    if (liquidTextureIndex > 11)
        //                                    {
        //                                        Opacity = LiquidRegistry.setOpacity(tiler);
        //                                    }

        //                                    float num111 = (float)color6.R * Opacity;
        //                                    float num112 = (float)color6.G * Opacity;
        //                                    float num113 = (float)color6.B * Opacity;
        //                                    float num114 = (float)color6.A * Opacity;
        //                                    color6 = new Microsoft.Xna.Framework.Color((int)((byte)num111), (int)((byte)num112), (int)((byte)num113), (int)((byte)num114));
        //                                    Main.spriteBatch.Draw(Main.liquidTexture[liquidTextureIndex], value2 - Main.screenPosition + zero, new Microsoft.Xna.Framework.Rectangle?(value3), color6, 0f, default(Vector2), 1f, SpriteEffects.None, 0f);
        //                                }
        //                            }
        //                        }
        //                        if (type == 314)
        //                        {
        //                            if (Tile.inActive())
        //                            {
        //                                color = Tile.actColor(color);
        //                            }
        //                            else if (Main.tileShine2[(int)type])
        //                            {
        //                                color = Main.shine(color, (int)type);
        //                            }
        //                            int num115;
        //                            int num116;
        //                            Minecart.TrackColors(j, i, Tile, out num115, out num116);
        //                            Texture2D texture;
        //                            if (Main.canDrawColorTile(type, num115))
        //                            {
        //                                texture = Main.tileAltTexture[(int)type, num115];
        //                            }
        //                            else
        //                            {
        //                                texture = Main.tileTexture[(int)type];
        //                            }
        //                            Texture2D texture2;
        //                            if (Main.canDrawColorTile(type, num116))
        //                            {
        //                                texture2 = Main.tileAltTexture[(int)type, num116];
        //                            }
        //                            else
        //                            {
        //                                texture2 = Main.tileTexture[(int)type];
        //                            }
        //                            Tile.frameNumber();
        //                            if (num10 != -1)
        //                            {
        //                                Main.spriteBatch.Draw(texture2, new Vector2((float)(j * 16 - (int)Main.screenPosition.X), (float)(i * 16 - (int)Main.screenPosition.y)) + zero, new Microsoft.Xna.Framework.Rectangle?(Minecart.GetSourceRect((int)num10, Main.tileFrame[314])), color, 0f, default(Vector2), 1f, effects, 0f);
        //                            }
        //                            Main.spriteBatch.Draw(texture, new Vector2((float)(j * 16 - (int)Main.screenPosition.X), (float)(i * 16 - (int)Main.screenPosition.y)) + zero, new Microsoft.Xna.Framework.Rectangle?(Minecart.GetSourceRect((int)num9, Main.tileFrame[314])), color, 0f, default(Vector2), 1f, effects, 0f);
        //                            if (Minecart.DrawLeftDecoration((int)num10))
        //                            {
        //                                Main.spriteBatch.Draw(texture2, new Vector2((float)(j * 16 - (int)Main.screenPosition.X), (float)((i + 1) * 16 - (int)Main.screenPosition.y)) + zero, new Microsoft.Xna.Framework.Rectangle?(Minecart.GetSourceRect(36, 0)), color, 0f, default(Vector2), 1f, effects, 0f);
        //                            }
        //                            if (Minecart.DrawLeftDecoration((int)num9))
        //                            {
        //                                Main.spriteBatch.Draw(texture, new Vector2((float)(j * 16 - (int)Main.screenPosition.X), (float)((i + 1) * 16 - (int)Main.screenPosition.y)) + zero, new Microsoft.Xna.Framework.Rectangle?(Minecart.GetSourceRect(36, 0)), color, 0f, default(Vector2), 1f, effects, 0f);
        //                            }
        //                            if (Minecart.DrawRightDecoration((int)num10))
        //                            {
        //                                Main.spriteBatch.Draw(texture2, new Vector2((float)(j * 16 - (int)Main.screenPosition.X), (float)((i + 1) * 16 - (int)Main.screenPosition.y)) + zero, new Microsoft.Xna.Framework.Rectangle?(Minecart.GetSourceRect(37, Main.tileFrame[314])), color, 0f, default(Vector2), 1f, effects, 0f);
        //                            }
        //                            if (Minecart.DrawRightDecoration((int)num9))
        //                            {
        //                                Main.spriteBatch.Draw(texture, new Vector2((float)(j * 16 - (int)Main.screenPosition.X), (float)((i + 1) * 16 - (int)Main.screenPosition.y)) + zero, new Microsoft.Xna.Framework.Rectangle?(Minecart.GetSourceRect(37, 0)), color, 0f, default(Vector2), 1f, effects, 0f);
        //                            }
        //                            if (Minecart.DrawBumper((int)num9))
        //                            {
        //                                Main.spriteBatch.Draw(texture, new Vector2((float)(j * 16 - (int)Main.screenPosition.X), (float)((i - 1) * 16 - (int)Main.screenPosition.y)) + zero, new Microsoft.Xna.Framework.Rectangle?(Minecart.GetSourceRect(39, 0)), color, 0f, default(Vector2), 1f, effects, 0f);
        //                            }
        //                            else if (Minecart.DrawBouncyBumper((int)num9))
        //                            {
        //                                Main.spriteBatch.Draw(texture, new Vector2((float)(j * 16 - (int)Main.screenPosition.X), (float)((i - 1) * 16 - (int)Main.screenPosition.y)) + zero, new Microsoft.Xna.Framework.Rectangle?(Minecart.GetSourceRect(38, 0)), color, 0f, default(Vector2), 1f, effects, 0f);
        //                            }
        //                        }
        //                        else if (type == 51)
        //                        {
        //                            Microsoft.Xna.Framework.Color color7 = Lighting.GetColor(j, i);
        //                            float num117 = 0.5f;
        //                            float num118 = (float)color7.R * num117;
        //                            float num119 = (float)color7.G * num117;
        //                            float num120 = (float)color7.B * num117;
        //                            float num121 = (float)color7.A * num117;
        //                            color7 = new Microsoft.Xna.Framework.Color((int)((byte)num118), (int)((byte)num119), (int)((byte)num120), (int)((byte)num121));
        //                            if (Main.canDrawColorTile(j, i))
        //                            {
        //                                Main.spriteBatch.Draw(Main.tileAltTexture[(int)type, (int)Tile.color()], new Vector2((float)(j * 16 - (int)Main.screenPosition.X) - ((float)num8 - 16f) / 2f, (float)(i * 16 - (int)Main.screenPosition.y + num11)) + zero, new Microsoft.Xna.Framework.Rectangle?(new Microsoft.Xna.Framework.Rectangle((int)num9, (int)num10, num8, num12)), color7, 0f, default(Vector2), 1f, effects, 0f);
        //                            }
        //                            else
        //                            {
        //                                Main.spriteBatch.Draw(Main.tileTexture[(int)type], new Vector2((float)(j * 16 - (int)Main.screenPosition.X) - ((float)num8 - 16f) / 2f, (float)(i * 16 - (int)Main.screenPosition.y + num11)) + zero, new Microsoft.Xna.Framework.Rectangle?(new Microsoft.Xna.Framework.Rectangle((int)num9, (int)num10, num8, num12)), color7, 0f, default(Vector2), 1f, effects, 0f);
        //                            }
        //                        }
        //                        else if (type == 171)
        //                        {
        //                            if (num6 > i - (int)num10 && num10 == 7)
        //                            {
        //                                num11 -= (int)(16 * num10);
        //                                num9 = Main.Tile[j, i - (int)num10].frameX;
        //                                num10 = Main.Tile[j, i - (int)num10].frameY;
        //                            }
        //                            if (num9 >= 10)
        //                            {
        //                                int num122 = 0;
        //                                if ((num10 & 1) == 1)
        //                                {
        //                                    num122++;
        //                                }
        //                                if ((num10 & 2) == 2)
        //                                {
        //                                    num122 += 2;
        //                                }
        //                                if ((num10 & 4) == 4)
        //                                {
        //                                    num122 += 4;
        //                                }
        //                                int num123 = 0;
        //                                if ((num10 & 8) == 8)
        //                                {
        //                                    num123++;
        //                                }
        //                                if ((num10 & 16) == 16)
        //                                {
        //                                    num123 += 2;
        //                                }
        //                                if ((num10 & 32) == 32)
        //                                {
        //                                    num123 += 4;
        //                                }
        //                                int num124 = 0;
        //                                if ((num10 & 64) == 64)
        //                                {
        //                                    num124++;
        //                                }
        //                                if ((num10 & 128) == 128)
        //                                {
        //                                    num124 += 2;
        //                                }
        //                                if ((num10 & 256) == 256)
        //                                {
        //                                    num124 += 4;
        //                                }
        //                                if ((num10 & 512) == 512)
        //                                {
        //                                    num124 += 8;
        //                                }
        //                                int num125 = 0;
        //                                if ((num10 & 1024) == 1024)
        //                                {
        //                                    num125++;
        //                                }
        //                                if ((num10 & 2048) == 2048)
        //                                {
        //                                    num125 += 2;
        //                                }
        //                                if ((num10 & 4096) == 4096)
        //                                {
        //                                    num125 += 4;
        //                                }
        //                                if ((num10 & 8192) == 8192)
        //                                {
        //                                    num125 += 8;
        //                                }
        //                                Microsoft.Xna.Framework.Color color8 = Lighting.GetColor(j + 1, i + 4);
        //                                Main.spriteBatch.Draw(Main.xmasTree[0], new Vector2((float)(j * 16 - (int)Main.screenPosition.X) - ((float)num8 - 16f) / 2f, (float)(i * 16 - (int)Main.screenPosition.y + num11)) + zero, new Microsoft.Xna.Framework.Rectangle?(new Microsoft.Xna.Framework.Rectangle(0, 0, 64, 128)), color8, 0f, default(Vector2), 1f, SpriteEffects.None, 0f);
        //                                if (num122 > 0)
        //                                {
        //                                    num122--;
        //                                    Microsoft.Xna.Framework.Color color9 = color8;
        //                                    if (num122 != 3)
        //                                    {
        //                                        color9 = new Microsoft.Xna.Framework.Color(255, 255, 255, 255);
        //                                    }
        //                                    Main.spriteBatch.Draw(Main.xmasTree[3], new Vector2((float)(j * 16 - (int)Main.screenPosition.X) - ((float)num8 - 16f) / 2f, (float)(i * 16 - (int)Main.screenPosition.y + num11)) + zero, new Microsoft.Xna.Framework.Rectangle?(new Microsoft.Xna.Framework.Rectangle(66 * num122, 0, 64, 128)), color9, 0f, default(Vector2), 1f, SpriteEffects.None, 0f);
        //                                }
        //                                if (num123 > 0)
        //                                {
        //                                    num123--;
        //                                    Main.spriteBatch.Draw(Main.xmasTree[1], new Vector2((float)(j * 16 - (int)Main.screenPosition.X) - ((float)num8 - 16f) / 2f, (float)(i * 16 - (int)Main.screenPosition.y + num11)) + zero, new Microsoft.Xna.Framework.Rectangle?(new Microsoft.Xna.Framework.Rectangle(66 * num123, 0, 64, 128)), color8, 0f, default(Vector2), 1f, SpriteEffects.None, 0f);
        //                                }
        //                                if (num124 > 0)
        //                                {
        //                                    num124--;
        //                                    Main.spriteBatch.Draw(Main.xmasTree[2], new Vector2((float)(j * 16 - (int)Main.screenPosition.X) - ((float)num8 - 16f) / 2f, (float)(i * 16 - (int)Main.screenPosition.y + num11)) + zero, new Microsoft.Xna.Framework.Rectangle?(new Microsoft.Xna.Framework.Rectangle(66 * num124, 0, 64, 128)), color8, 0f, default(Vector2), 1f, SpriteEffects.None, 0f);
        //                                }
        //                                if (num125 > 0)
        //                                {
        //                                    num125--;
        //                                    Main.spriteBatch.Draw(Main.xmasTree[4], new Vector2((float)(j * 16 - (int)Main.screenPosition.X) - ((float)num8 - 16f) / 2f, (float)(i * 16 - (int)Main.screenPosition.y + num11)) + zero, new Microsoft.Xna.Framework.Rectangle?(new Microsoft.Xna.Framework.Rectangle(66 * num125, 130 * Main.tileFrame[171], 64, 128)), new Microsoft.Xna.Framework.Color(255, 255, 255, 255), 0f, default(Vector2), 1f, SpriteEffects.None, 0f);
        //                                }
        //                            }
        //                        }
        //                        else if (type == 160 && !Tile.halfBrick())
        //                        {
        //                            Microsoft.Xna.Framework.Color color10 = default(Microsoft.Xna.Framework.Color);
        //                            color10 = new Microsoft.Xna.Framework.Color(Main.DiscoR, Main.DiscoG, Main.DiscoB, 255);
        //                            if (Tile.inActive())
        //                            {
        //                                color10 = Tile.actColor(color10);
        //                            }
        //                            if (Tile.slope() == 0)
        //                            {
        //                                Main.spriteBatch.Draw(Main.tileTexture[(int)type], new Vector2((float)(j * 16 - (int)Main.screenPosition.X) - ((float)num8 - 16f) / 2f, (float)(i * 16 - (int)Main.screenPosition.y + num11)) + zero, new Microsoft.Xna.Framework.Rectangle?(new Microsoft.Xna.Framework.Rectangle((int)num9, (int)num10, num8, num12)), color10, 0f, default(Vector2), 1f, effects, 0f);
        //                            }
        //                            else if (Tile.slope() > 2)
        //                            {
        //                                if (Tile.slope() == 3)
        //                                {
        //                                    for (int num126 = 0; num126 < 8; num126++)
        //                                    {
        //                                        int num127 = 2;
        //                                        int num128 = num126 * 2;
        //                                        int num129 = num126 * -2;
        //                                        int num130 = 16 - num126 * 2;
        //                                        if (Main.canDrawColorTile(j, i))
        //                                        {
        //                                            Main.spriteBatch.Draw(Main.tileAltTexture[(int)type, (int)Tile.color()], new Vector2((float)(j * 16 - (int)Main.screenPosition.X) - ((float)num8 - 16f) / 2f + (float)num128, (float)(i * 16 - (int)Main.screenPosition.y + num11 + num126 * num127 + num129)) + zero, new Microsoft.Xna.Framework.Rectangle?(new Microsoft.Xna.Framework.Rectangle((int)num9 + num128, (int)(num10 + 16) - num130, num127, num130)), color10, 0f, default(Vector2), 1f, effects, 0f);
        //                                        }
        //                                        else
        //                                        {
        //                                            Main.spriteBatch.Draw(Main.tileTexture[(int)type], new Vector2((float)(j * 16 - (int)Main.screenPosition.X) - ((float)num8 - 16f) / 2f + (float)num128, (float)(i * 16 - (int)Main.screenPosition.y + num11 + num126 * num127 + num129)) + zero, new Microsoft.Xna.Framework.Rectangle?(new Microsoft.Xna.Framework.Rectangle((int)num9 + num128, (int)(num10 + 16) - num130, num127, num130)), color10, 0f, default(Vector2), 1f, effects, 0f);
        //                                        }
        //                                    }
        //                                }
        //                                else
        //                                {
        //                                    for (int num131 = 0; num131 < 8; num131++)
        //                                    {
        //                                        int num132 = 2;
        //                                        int num133 = 16 - num131 * num132 - num132;
        //                                        int num134 = 16 - num131 * num132;
        //                                        int num135 = num131 * -2;
        //                                        if (Main.canDrawColorTile(j, i))
        //                                        {
        //                                            Main.spriteBatch.Draw(Main.tileAltTexture[(int)type, (int)Tile.color()], new Vector2((float)(j * 16 - (int)Main.screenPosition.X) - ((float)num8 - 16f) / 2f + (float)num133, (float)(i * 16 - (int)Main.screenPosition.y + num11 + num131 * num132 + num135)) + zero, new Microsoft.Xna.Framework.Rectangle?(new Microsoft.Xna.Framework.Rectangle((int)num9 + num133, (int)(num10 + 16) - num134, num132, num134)), color10, 0f, default(Vector2), 1f, effects, 0f);
        //                                        }
        //                                        else
        //                                        {
        //                                            Main.spriteBatch.Draw(Main.tileTexture[(int)type], new Vector2((float)(j * 16 - (int)Main.screenPosition.X) - ((float)num8 - 16f) / 2f + (float)num133, (float)(i * 16 - (int)Main.screenPosition.y + num11 + num131 * num132 + num135)) + zero, new Microsoft.Xna.Framework.Rectangle?(new Microsoft.Xna.Framework.Rectangle((int)num9 + num133, (int)(num10 + 16) - num134, num132, num134)), color10, 0f, default(Vector2), 1f, effects, 0f);
        //                                        }
        //                                    }
        //                                }
        //                                if (Main.canDrawColorTile(j, i))
        //                                {
        //                                    Main.spriteBatch.Draw(Main.tileAltTexture[(int)type, (int)Tile.color()], new Vector2((float)(j * 16 - (int)Main.screenPosition.X) - ((float)num8 - 16f) / 2f, (float)(i * 16 - (int)Main.screenPosition.y + num11)) + zero, new Microsoft.Xna.Framework.Rectangle?(new Microsoft.Xna.Framework.Rectangle((int)num9, (int)num10, 16, 2)), color10, 0f, default(Vector2), 1f, effects, 0f);
        //                                }
        //                                else
        //                                {
        //                                    Main.spriteBatch.Draw(Main.tileTexture[(int)type], new Vector2((float)(j * 16 - (int)Main.screenPosition.X) - ((float)num8 - 16f) / 2f, (float)(i * 16 - (int)Main.screenPosition.y + num11)) + zero, new Microsoft.Xna.Framework.Rectangle?(new Microsoft.Xna.Framework.Rectangle((int)num9, (int)num10, 16, 2)), color10, 0f, default(Vector2), 1f, effects, 0f);
        //                                }
        //                            }
        //                            else
        //                            {
        //                                if (Tile.slope() == 1)
        //                                {
        //                                    for (int num136 = 0; num136 < 8; num136++)
        //                                    {
        //                                        int num137 = 2;
        //                                        int num138 = num136 * 2;
        //                                        int height2 = 14 - num136 * num137;
        //                                        Main.spriteBatch.Draw(Main.tileTexture[(int)type], new Vector2((float)(j * 16 - (int)Main.screenPosition.X) - ((float)num8 - 16f) / 2f + (float)num138, (float)(i * 16 - (int)Main.screenPosition.y + num11 + num136 * num137)) + zero, new Microsoft.Xna.Framework.Rectangle?(new Microsoft.Xna.Framework.Rectangle((int)num9 + num138, (int)num10, num137, height2)), color10, 0f, default(Vector2), 1f, effects, 0f);
        //                                    }
        //                                }
        //                                if (Tile.slope() == 2)
        //                                {
        //                                    for (int num139 = 0; num139 < 8; num139++)
        //                                    {
        //                                        int num140 = 2;
        //                                        int num141 = 16 - num139 * num140 - num140;
        //                                        int height3 = 14 - num139 * num140;
        //                                        Main.spriteBatch.Draw(Main.tileTexture[(int)type], new Vector2((float)(j * 16 - (int)Main.screenPosition.X) - ((float)num8 - 16f) / 2f + (float)num141, (float)(i * 16 - (int)Main.screenPosition.y + num11 + num139 * num140)) + zero, new Microsoft.Xna.Framework.Rectangle?(new Microsoft.Xna.Framework.Rectangle((int)num9 + num141, (int)num10, num140, height3)), color10, 0f, default(Vector2), 1f, effects, 0f);
        //                                    }
        //                                }
        //                                if (Main.canDrawColorTile(j, i))
        //                                {
        //                                    Main.spriteBatch.Draw(Main.tileAltTexture[(int)type, (int)Tile.color()], new Vector2((float)(j * 16 - (int)Main.screenPosition.X) - ((float)num8 - 16f) / 2f, (float)(i * 16 - (int)Main.screenPosition.y + num11 + 14)) + zero, new Microsoft.Xna.Framework.Rectangle?(new Microsoft.Xna.Framework.Rectangle((int)num9, (int)(num10 + 14), 16, 2)), color10, 0f, default(Vector2), 1f, effects, 0f);
        //                                }
        //                                else
        //                                {
        //                                    Main.spriteBatch.Draw(Main.tileTexture[(int)type], new Vector2((float)(j * 16 - (int)Main.screenPosition.X) - ((float)num8 - 16f) / 2f, (float)(i * 16 - (int)Main.screenPosition.y + num11 + 14)) + zero, new Microsoft.Xna.Framework.Rectangle?(new Microsoft.Xna.Framework.Rectangle((int)num9, (int)(num10 + 14), 16, 2)), color10, 0f, default(Vector2), 1f, effects, 0f);
        //                                }
        //                            }
        //                        }
        //                        else if (Tile.slope() > 0)
        //                        {
        //                            if (Tile.inActive())
        //                            {
        //                                color = Tile.actColor(color);
        //                            }
        //                            else if (Main.tileShine2[(int)type])
        //                            {
        //                                color = Main.shine(color, (int)type);
        //                            }
        //                            if (TileID.Sets.Platforms[(int)Tile.type])
        //                            {
        //                                if (Main.canDrawColorTile(j, i))
        //                                {
        //                                    Main.spriteBatch.Draw(Main.tileAltTexture[(int)type, (int)Tile.color()], new Vector2((float)(j * 16 - (int)Main.screenPosition.X) - ((float)num8 - 16f) / 2f, (float)(i * 16 - (int)Main.screenPosition.y + num11)) + zero, new Microsoft.Xna.Framework.Rectangle?(new Microsoft.Xna.Framework.Rectangle((int)num9, (int)num10, 16, 16)), color, 0f, default(Vector2), 1f, effects, 0f);
        //                                }
        //                                else
        //                                {
        //                                    Main.spriteBatch.Draw(Main.tileTexture[(int)type], new Vector2((float)(j * 16 - (int)Main.screenPosition.X) - ((float)num8 - 16f) / 2f, (float)(i * 16 - (int)Main.screenPosition.y + num11)) + zero, new Microsoft.Xna.Framework.Rectangle?(new Microsoft.Xna.Framework.Rectangle((int)num9, (int)num10, 16, 16)), color, 0f, default(Vector2), 1f, effects, 0f);
        //                                }
        //                                if (Tile.slope() == 1 && Main.Tile[j + 1, i + 1].active() && Main.Tile[j + 1, i + 1].slope() != 2 && !Main.Tile[j + 1, i + 1].halfBrick() && !TileID.Sets.BlocksStairs[(int)Main.Tile[j + 1, i + 1].type] && !TileID.Sets.BlocksStairsAbove[(int)Main.Tile[j, i + 1].type])
        //                                {
        //                                    if (TileID.Sets.Platforms[(int)Main.Tile[j + 1, i + 1].type] && Main.Tile[j + 1, i + 1].slope() == 0)
        //                                    {
        //                                        if (Main.canDrawColorTile(j, i))
        //                                        {
        //                                            Main.spriteBatch.Draw(Main.tileAltTexture[(int)type, (int)Tile.color()], new Vector2((float)(j * 16 - (int)Main.screenPosition.X) - ((float)num8 - 16f) / 2f, (float)(i * 16 - (int)Main.screenPosition.y + num11 + 16)) + zero, new Microsoft.Xna.Framework.Rectangle?(new Microsoft.Xna.Framework.Rectangle(324, (int)num10, 16, 16)), color, 0f, default(Vector2), 1f, effects, 0f);
        //                                        }
        //                                        else
        //                                        {
        //                                            Main.spriteBatch.Draw(Main.tileTexture[(int)type], new Vector2((float)(j * 16 - (int)Main.screenPosition.X) - ((float)num8 - 16f) / 2f, (float)(i * 16 - (int)Main.screenPosition.y + num11 + 16)) + zero, new Microsoft.Xna.Framework.Rectangle?(new Microsoft.Xna.Framework.Rectangle(324, (int)num10, 16, 16)), color, 0f, default(Vector2), 1f, effects, 0f);
        //                                        }
        //                                    }
        //                                    else if (Main.canDrawColorTile(j, i))
        //                                    {
        //                                        Main.spriteBatch.Draw(Main.tileAltTexture[(int)type, (int)Tile.color()], new Vector2((float)(j * 16 - (int)Main.screenPosition.X) - ((float)num8 - 16f) / 2f, (float)(i * 16 - (int)Main.screenPosition.y + num11 + 16)) + zero, new Microsoft.Xna.Framework.Rectangle?(new Microsoft.Xna.Framework.Rectangle(198, (int)num10, 16, 16)), color, 0f, default(Vector2), 1f, effects, 0f);
        //                                    }
        //                                    else
        //                                    {
        //                                        Main.spriteBatch.Draw(Main.tileTexture[(int)type], new Vector2((float)(j * 16 - (int)Main.screenPosition.X) - ((float)num8 - 16f) / 2f, (float)(i * 16 - (int)Main.screenPosition.y + num11 + 16)) + zero, new Microsoft.Xna.Framework.Rectangle?(new Microsoft.Xna.Framework.Rectangle(198, (int)num10, 16, 16)), color, 0f, default(Vector2), 1f, effects, 0f);
        //                                    }
        //                                }
        //                                else if (Tile.slope() == 2 && Main.Tile[j - 1, i + 1].active() && Main.Tile[j - 1, i + 1].slope() != 1 && !Main.Tile[j - 1, i + 1].halfBrick() && !TileID.Sets.BlocksStairs[(int)Main.Tile[j - 1, i + 1].type] && !TileID.Sets.BlocksStairsAbove[(int)Main.Tile[j, i + 1].type])
        //                                {
        //                                    if (TileID.Sets.Platforms[(int)Main.Tile[j - 1, i + 1].type] && Main.Tile[j - 1, i + 1].slope() == 0)
        //                                    {
        //                                        if (Main.canDrawColorTile(j, i))
        //                                        {
        //                                            Main.spriteBatch.Draw(Main.tileAltTexture[(int)type, (int)Tile.color()], new Vector2((float)(j * 16 - (int)Main.screenPosition.X) - ((float)num8 - 16f) / 2f, (float)(i * 16 - (int)Main.screenPosition.y + num11 + 16)) + zero, new Microsoft.Xna.Framework.Rectangle?(new Microsoft.Xna.Framework.Rectangle(306, (int)num10, 16, 16)), color, 0f, default(Vector2), 1f, effects, 0f);
        //                                        }
        //                                        else
        //                                        {
        //                                            Main.spriteBatch.Draw(Main.tileTexture[(int)type], new Vector2((float)(j * 16 - (int)Main.screenPosition.X) - ((float)num8 - 16f) / 2f, (float)(i * 16 - (int)Main.screenPosition.y + num11 + 16)) + zero, new Microsoft.Xna.Framework.Rectangle?(new Microsoft.Xna.Framework.Rectangle(306, (int)num10, 16, 16)), color, 0f, default(Vector2), 1f, effects, 0f);
        //                                        }
        //                                    }
        //                                    else if (Main.canDrawColorTile(j, i))
        //                                    {
        //                                        Main.spriteBatch.Draw(Main.tileAltTexture[(int)type, (int)Tile.color()], new Vector2((float)(j * 16 - (int)Main.screenPosition.X) - ((float)num8 - 16f) / 2f, (float)(i * 16 - (int)Main.screenPosition.y + num11 + 16)) + zero, new Microsoft.Xna.Framework.Rectangle?(new Microsoft.Xna.Framework.Rectangle(162, (int)num10, 16, 16)), color, 0f, default(Vector2), 1f, effects, 0f);
        //                                    }
        //                                    else
        //                                    {
        //                                        Main.spriteBatch.Draw(Main.tileTexture[(int)type], new Vector2((float)(j * 16 - (int)Main.screenPosition.X) - ((float)num8 - 16f) / 2f, (float)(i * 16 - (int)Main.screenPosition.y + num11 + 16)) + zero, new Microsoft.Xna.Framework.Rectangle?(new Microsoft.Xna.Framework.Rectangle(162, (int)num10, 16, 16)), color, 0f, default(Vector2), 1f, effects, 0f);
        //                                    }
        //                                }
        //                            }
        //                            else if (TileID.Sets.HasSlopeFrames[(int)Tile.type])
        //                            {
        //                                if (Main.canDrawColorTile(j, i))
        //                                {
        //                                    Main.spriteBatch.Draw(Main.tileAltTexture[(int)type, (int)Tile.color()], new Vector2((float)(j * 16 - (int)Main.screenPosition.X) - ((float)num8 - 16f) / 2f, (float)(i * 16 - (int)Main.screenPosition.y + num11)) + zero, new Microsoft.Xna.Framework.Rectangle?(new Microsoft.Xna.Framework.Rectangle((int)num9 + num16, (int)num10 + num15, 16, 16)), color, 0f, default(Vector2), 1f, effects, 0f);
        //                                }
        //                                else
        //                                {
        //                                    Main.spriteBatch.Draw(Main.tileTexture[(int)type], new Vector2((float)(j * 16 - (int)Main.screenPosition.X) - ((float)num8 - 16f) / 2f, (float)(i * 16 - (int)Main.screenPosition.y + num11)) + zero, new Microsoft.Xna.Framework.Rectangle?(new Microsoft.Xna.Framework.Rectangle((int)num9 + num16, (int)num10 + num15, 16, 16)), color, 0f, default(Vector2), 1f, effects, 0f);
        //                                }
        //                            }
        //                            else if (Tile.slope() > 2)
        //                            {
        //                                if (Tile.slope() == 3)
        //                                {
        //                                    for (int num142 = 0; num142 < 8; num142++)
        //                                    {
        //                                        int num143 = 2;
        //                                        int num144 = num142 * 2;
        //                                        int num145 = num142 * -2;
        //                                        int num146 = 16 - num142 * 2;
        //                                        if (Main.canDrawColorTile(j, i))
        //                                        {
        //                                            Main.spriteBatch.Draw(Main.tileAltTexture[(int)type, (int)Tile.color()], new Vector2((float)(j * 16 - (int)Main.screenPosition.X) - ((float)num8 - 16f) / 2f + (float)num144, (float)(i * 16 - (int)Main.screenPosition.y + num11 + num142 * num143 + num145)) + zero, new Microsoft.Xna.Framework.Rectangle?(new Microsoft.Xna.Framework.Rectangle((int)num9 + num144 + num16, (int)(num10 + 16) - num146 + num15, num143, num146)), color, 0f, default(Vector2), 1f, effects, 0f);
        //                                        }
        //                                        else
        //                                        {
        //                                            Main.spriteBatch.Draw(Main.tileTexture[(int)type], new Vector2((float)(j * 16 - (int)Main.screenPosition.X) - ((float)num8 - 16f) / 2f + (float)num144, (float)(i * 16 - (int)Main.screenPosition.y + num11 + num142 * num143 + num145)) + zero, new Microsoft.Xna.Framework.Rectangle?(new Microsoft.Xna.Framework.Rectangle((int)num9 + num144 + num16, (int)(num10 + 16) - num146 + num15, num143, num146)), color, 0f, default(Vector2), 1f, effects, 0f);
        //                                        }
        //                                    }
        //                                }
        //                                else
        //                                {
        //                                    for (int num147 = 0; num147 < 8; num147++)
        //                                    {
        //                                        int num148 = 2;
        //                                        int num149 = 16 - num147 * num148 - num148;
        //                                        int num150 = 16 - num147 * num148;
        //                                        int num151 = num147 * -2;
        //                                        if (Main.canDrawColorTile(j, i))
        //                                        {
        //                                            Main.spriteBatch.Draw(Main.tileAltTexture[(int)type, (int)Tile.color()], new Vector2((float)(j * 16 - (int)Main.screenPosition.X) - ((float)num8 - 16f) / 2f + (float)num149, (float)(i * 16 - (int)Main.screenPosition.y + num11 + num147 * num148 + num151)) + zero, new Microsoft.Xna.Framework.Rectangle?(new Microsoft.Xna.Framework.Rectangle((int)num9 + num149 + num16, (int)(num10 + 16) - num150 + num15, num148, num150)), color, 0f, default(Vector2), 1f, effects, 0f);
        //                                        }
        //                                        else
        //                                        {
        //                                            Main.spriteBatch.Draw(Main.tileTexture[(int)type], new Vector2((float)(j * 16 - (int)Main.screenPosition.X) - ((float)num8 - 16f) / 2f + (float)num149, (float)(i * 16 - (int)Main.screenPosition.y + num11 + num147 * num148 + num151)) + zero, new Microsoft.Xna.Framework.Rectangle?(new Microsoft.Xna.Framework.Rectangle((int)num9 + num149 + num16, (int)(num10 + 16) - num150 + num15, num148, num150)), color, 0f, default(Vector2), 1f, effects, 0f);
        //                                        }
        //                                    }
        //                                }
        //                                if (Main.canDrawColorTile(j, i))
        //                                {
        //                                    Main.spriteBatch.Draw(Main.tileAltTexture[(int)type, (int)Tile.color()], new Vector2((float)(j * 16 - (int)Main.screenPosition.X) - ((float)num8 - 16f) / 2f, (float)(i * 16 - (int)Main.screenPosition.y + num11)) + zero, new Microsoft.Xna.Framework.Rectangle?(new Microsoft.Xna.Framework.Rectangle((int)num9 + num16, (int)num10 + num15, 16, 2)), color, 0f, default(Vector2), 1f, effects, 0f);
        //                                }
        //                                else
        //                                {
        //                                    Main.spriteBatch.Draw(Main.tileTexture[(int)type], new Vector2((float)(j * 16 - (int)Main.screenPosition.X) - ((float)num8 - 16f) / 2f, (float)(i * 16 - (int)Main.screenPosition.y + num11)) + zero, new Microsoft.Xna.Framework.Rectangle?(new Microsoft.Xna.Framework.Rectangle((int)num9 + num16, (int)num10 + num15, 16, 2)), color, 0f, default(Vector2), 1f, effects, 0f);
        //                                }
        //                            }
        //                            else
        //                            {
        //                                if (Tile.slope() == 1)
        //                                {
        //                                    for (int num152 = 0; num152 < 8; num152++)
        //                                    {
        //                                        int num153 = 2;
        //                                        int num154 = num152 * 2;
        //                                        int height4 = 14 - num152 * num153;
        //                                        if (Main.canDrawColorTile(j, i))
        //                                        {
        //                                            Main.spriteBatch.Draw(Main.tileAltTexture[(int)type, (int)Tile.color()], new Vector2((float)(j * 16 - (int)Main.screenPosition.X) - ((float)num8 - 16f) / 2f + (float)num154, (float)(i * 16 - (int)Main.screenPosition.y + num11 + num152 * num153)) + zero, new Microsoft.Xna.Framework.Rectangle?(new Microsoft.Xna.Framework.Rectangle((int)num9 + num154 + num16, (int)num10 + num15, num153, height4)), color, 0f, default(Vector2), 1f, effects, 0f);
        //                                        }
        //                                        else
        //                                        {
        //                                            Main.spriteBatch.Draw(Main.tileTexture[(int)type], new Vector2((float)(j * 16 - (int)Main.screenPosition.X) - ((float)num8 - 16f) / 2f + (float)num154, (float)(i * 16 - (int)Main.screenPosition.y + num11 + num152 * num153)) + zero, new Microsoft.Xna.Framework.Rectangle?(new Microsoft.Xna.Framework.Rectangle((int)num9 + num154 + num16, (int)num10 + num15, num153, height4)), color, 0f, default(Vector2), 1f, effects, 0f);
        //                                        }
        //                                    }
        //                                }
        //                                if (Tile.slope() == 2)
        //                                {
        //                                    for (int num155 = 0; num155 < 8; num155++)
        //                                    {
        //                                        int num156 = 2;
        //                                        int num157 = 16 - num155 * num156 - num156;
        //                                        int height5 = 14 - num155 * num156;
        //                                        if (Main.canDrawColorTile(j, i))
        //                                        {
        //                                            Main.spriteBatch.Draw(Main.tileAltTexture[(int)type, (int)Tile.color()], new Vector2((float)(j * 16 - (int)Main.screenPosition.X) - ((float)num8 - 16f) / 2f + (float)num157, (float)(i * 16 - (int)Main.screenPosition.y + num11 + num155 * num156)) + zero, new Microsoft.Xna.Framework.Rectangle?(new Microsoft.Xna.Framework.Rectangle((int)num9 + num157 + num16, (int)num10 + num15, num156, height5)), color, 0f, default(Vector2), 1f, effects, 0f);
        //                                        }
        //                                        else
        //                                        {
        //                                            Main.spriteBatch.Draw(Main.tileTexture[(int)type], new Vector2((float)(j * 16 - (int)Main.screenPosition.X) - ((float)num8 - 16f) / 2f + (float)num157, (float)(i * 16 - (int)Main.screenPosition.y + num11 + num155 * num156)) + zero, new Microsoft.Xna.Framework.Rectangle?(new Microsoft.Xna.Framework.Rectangle((int)num9 + num157 + num16, (int)num10 + num15, num156, height5)), color, 0f, default(Vector2), 1f, effects, 0f);
        //                                        }
        //                                    }
        //                                }
        //                                if (Main.canDrawColorTile(j, i))
        //                                {
        //                                    Main.spriteBatch.Draw(Main.tileAltTexture[(int)type, (int)Tile.color()], new Vector2((float)(j * 16 - (int)Main.screenPosition.X) - ((float)num8 - 16f) / 2f, (float)(i * 16 - (int)Main.screenPosition.y + num11 + 14)) + zero, new Microsoft.Xna.Framework.Rectangle?(new Microsoft.Xna.Framework.Rectangle((int)num9 + num16, (int)(num10 + 14) + num15, 16, 2)), color, 0f, default(Vector2), 1f, effects, 0f);
        //                                }
        //                                else
        //                                {
        //                                    Main.spriteBatch.Draw(Main.tileTexture[(int)type], new Vector2((float)(j * 16 - (int)Main.screenPosition.X) - ((float)num8 - 16f) / 2f, (float)(i * 16 - (int)Main.screenPosition.y + num11 + 14)) + zero, new Microsoft.Xna.Framework.Rectangle?(new Microsoft.Xna.Framework.Rectangle((int)num9 + num16, (int)(num10 + 14) + num15, 16, 2)), color, 0f, default(Vector2), 1f, effects, 0f);
        //                                }
        //                            }
        //                        }
        //                        else if (type == 129)
        //                        {
        //                            Vector2 value4 = new Vector2(0f, 0f);
        //                            if (num10 < 36)
        //                            {
        //                                value4.y += (float)(2 * (num10 == 0).ToDirectionInt());
        //                            }
        //                            else
        //                            {
        //                                value4.X += (float)(2 * (num10 == 36).ToDirectionInt());
        //                            }
        //                            Main.spriteBatch.Draw(Main.tileTexture[(int)type], new Vector2((float)(j * 16 - (int)Main.screenPosition.X) - ((float)num8 - 16f) / 2f, (float)(i * 16 - (int)Main.screenPosition.y + num11)) + zero + value4, new Microsoft.Xna.Framework.Rectangle?(new Microsoft.Xna.Framework.Rectangle((int)num9, (int)num10, num8, num12)), new Microsoft.Xna.Framework.Color(255, 255, 255, 100), 0f, default(Vector2), 1f, effects, 0f);
        //                        }
        //                        else if (Main.tileAlch[(int)type])
        //                        {
        //                            num12 = 20;
        //                            num11 = -2;
        //                            int num158 = (int)type;
        //                            int num159 = (int)(num9 / 18);
        //                            if (num158 > 82)
        //                            {
        //                                if (num159 == 0 && Main.dayTime)
        //                                {
        //                                    num158 = 84;
        //                                }
        //                                if (num159 == 1 && !Main.dayTime)
        //                                {
        //                                    num158 = 84;
        //                                }
        //                                if (num159 == 3 && !Main.dayTime && (Main.bloodMoon || Main.moonPhase == 0))
        //                                {
        //                                    num158 = 84;
        //                                }
        //                                if (num159 == 4 && (Main.raining || Main.cloudAlpha > 0f))
        //                                {
        //                                    num158 = 84;
        //                                }
        //                                if (num159 == 5 && !Main.raining && Main.time > 40500.0)
        //                                {
        //                                    num158 = 84;
        //                                }
        //                            }
        //                            if (num158 == 84)
        //                            {
        //                                if (num159 == 0 && Main.rand.Next(100) == 0)
        //                                {
        //                                    int num160 = Dust.NewDust(new Vector2((float)(j * 16), (float)(i * 16 - 4)), 16, 16, 19, 0f, 0f, 160, default(Microsoft.Xna.Framework.Color), 0.1f);
        //                                    Dust expr_7460_cp_0 = Main.dust[num160];
        //                                    expr_7460_cp_0.velocity.X = expr_7460_cp_0.velocity.X / 2f;
        //                                    Dust expr_747E_cp_0 = Main.dust[num160];
        //                                    expr_747E_cp_0.velocity.y = expr_747E_cp_0.velocity.y / 2f;
        //                                    Main.dust[num160].noGravity = true;
        //                                    Main.dust[num160].fadeIn = 1f;
        //                                }
        //                                if (num159 == 1 && Main.rand.Next(100) == 0)
        //                                {
        //                                    Dust.NewDust(new Vector2((float)(j * 16), (float)(i * 16)), 16, 16, 41, 0f, 0f, 250, default(Microsoft.Xna.Framework.Color), 0.8f);
        //                                }
        //                                if (num159 == 3)
        //                                {
        //                                    if (Main.rand.Next(200) == 0)
        //                                    {
        //                                        int num161 = Dust.NewDust(new Vector2((float)(j * 16), (float)(i * 16)), 16, 16, 14, 0f, 0f, 100, default(Microsoft.Xna.Framework.Color), 0.2f);
        //                                        Main.dust[num161].fadeIn = 1.2f;
        //                                    }
        //                                    if (Main.rand.Next(75) == 0)
        //                                    {
        //                                        int num162 = Dust.NewDust(new Vector2((float)(j * 16), (float)(i * 16)), 16, 16, 27, 0f, 0f, 100, default(Microsoft.Xna.Framework.Color), 1f);
        //                                        Dust expr_75C1_cp_0 = Main.dust[num162];
        //                                        expr_75C1_cp_0.velocity.X = expr_75C1_cp_0.velocity.X / 2f;
        //                                        Dust expr_75DF_cp_0 = Main.dust[num162];
        //                                        expr_75DF_cp_0.velocity.y = expr_75DF_cp_0.velocity.y / 2f;
        //                                    }
        //                                }
        //                                if (num159 == 4 && Main.rand.Next(150) == 0)
        //                                {
        //                                    int num163 = Dust.NewDust(new Vector2((float)(j * 16), (float)(i * 16)), 16, 8, 16, 0f, 0f, 0, default(Microsoft.Xna.Framework.Color), 1f);
        //                                    Dust expr_7654_cp_0 = Main.dust[num163];
        //                                    expr_7654_cp_0.velocity.X = expr_7654_cp_0.velocity.X / 3f;
        //                                    Dust expr_7672_cp_0 = Main.dust[num163];
        //                                    expr_7672_cp_0.velocity.y = expr_7672_cp_0.velocity.y / 3f;
        //                                    Dust expr_7690_cp_0 = Main.dust[num163];
        //                                    expr_7690_cp_0.velocity.y = expr_7690_cp_0.velocity.y - 0.7f;
        //                                    Main.dust[num163].alpha = 50;
        //                                    Main.dust[num163].scale *= 0.1f;
        //                                    Main.dust[num163].fadeIn = 0.9f;
        //                                    Main.dust[num163].noGravity = true;
        //                                }
        //                                if (num159 == 5)
        //                                {
        //                                    if (Main.rand.Next(40) == 0)
        //                                    {
        //                                        int num164 = Dust.NewDust(new Vector2((float)(j * 16), (float)(i * 16 - 6)), 16, 16, 6, 0f, 0f, 0, default(Microsoft.Xna.Framework.Color), 1.5f);
        //                                        Dust expr_7749_cp_0 = Main.dust[num164];
        //                                        expr_7749_cp_0.velocity.y = expr_7749_cp_0.velocity.y - 2f;
        //                                        Main.dust[num164].noGravity = true;
        //                                    }
        //                                    color.A = (byte)(Main.mouseTextColor / 2);
        //                                    color.G = Main.mouseTextColor;
        //                                    color.B = Main.mouseTextColor;
        //                                }
        //                                if (num159 == 6)
        //                                {
        //                                    if (Main.rand.Next(30) == 0)
        //                                    {
        //                                        Microsoft.Xna.Framework.Color newColor = new Microsoft.Xna.Framework.Color(50, 255, 255, 255);
        //                                        int num165 = Dust.NewDust(new Vector2((float)(j * 16), (float)(i * 16)), 16, 16, 43, 0f, 0f, 254, newColor, 0.5f);
        //                                        Main.dust[num165].velocity *= 0f;
        //                                    }
        //                                    byte b4 = (byte)((Main.mouseTextColor + color.G * 2) / 3);
        //                                    byte b5 = (byte)((Main.mouseTextColor + color.B * 2) / 3);
        //                                    if (b4 > color.G)
        //                                    {
        //                                        color.G = b4;
        //                                    }
        //                                    if (b5 > color.B)
        //                                    {
        //                                        color.B = b5;
        //                                    }
        //                                }
        //                            }
        //                            if (Main.canDrawColorTile(j, i))
        //                            {
        //                                Main.spriteBatch.Draw(Main.tileAltTexture[(int)type, (int)Tile.color()], new Vector2((float)(j * 16 - (int)Main.screenPosition.X) - ((float)num8 - 16f) / 2f, (float)(i * 16 - (int)Main.screenPosition.y + num11)) + zero, new Microsoft.Xna.Framework.Rectangle?(new Microsoft.Xna.Framework.Rectangle((int)num9, (int)num10, num8, num12)), color, 0f, default(Vector2), 1f, effects, 0f);
        //                            }
        //                            else
        //                            {
        //                                Main.instance.LoadTiles(num158);
        //                                Main.spriteBatch.Draw(Main.tileTexture[num158], new Vector2((float)(j * 16 - (int)Main.screenPosition.X) - ((float)num8 - 16f) / 2f, (float)(i * 16 - (int)Main.screenPosition.y + num11)) + zero, new Microsoft.Xna.Framework.Rectangle?(new Microsoft.Xna.Framework.Rectangle((int)num9, (int)num10, num8, num12)), color, 0f, default(Vector2), 1f, effects, 0f);
        //                            }
        //                        }
        //                        else if (type == 80)
        //                        {
        //                            bool flag13 = false;
        //                            bool flag14 = false;
        //                            bool flag15 = false;
        //                            Texture2D modCactusTexture = null;
        //                            if (!Main.canDrawColorTile(j, i))
        //                            {
        //                                int num166 = j;
        //                                if (num9 == 36)
        //                                {
        //                                    num166--;
        //                                }
        //                                if (num9 == 54)
        //                                {
        //                                    num166++;
        //                                }
        //                                if (num9 == 108)
        //                                {
        //                                    if (num10 == 18)
        //                                    {
        //                                        num166--;
        //                                    }
        //                                    else
        //                                    {
        //                                        num166++;
        //                                    }
        //                                }
        //                                int num167 = i;
        //                                bool flag16 = false;
        //                                if (Main.Tile[num166, num167].type == 80 && Main.Tile[num166, num167].active())
        //                                {
        //                                    flag16 = true;
        //                                }
        //                                while (!Main.Tile[num166, num167].active() || !Main.tileSolid[(int)Main.Tile[num166, num167].type] || !flag16)
        //                                {
        //                                    if (Main.Tile[num166, num167].type == 80 && Main.Tile[num166, num167].active())
        //                                    {
        //                                        flag16 = true;
        //                                    }
        //                                    num167++;
        //                                    if (num167 > i + 20)
        //                                    {
        //                                        break;
        //                                    }
        //                                }
        //                                if (Main.Tile[num166, num167].type == 112)
        //                                {
        //                                    flag13 = true;
        //                                }
        //                                if (Main.Tile[num166, num167].type == 116)
        //                                {
        //                                    flag14 = true;
        //                                }
        //                                //patch file: num166, num167
        //                                if (Main.Tile[num166, num167].type == 234)
        //                                {
        //                                    flag15 = true;
        //                                }
        //                                modCactusTexture = TileLoader.GetCactusTexture(Main.Tile[num166, num167].type);
        //                            }
        //                            if (modCactusTexture != null)
        //                            {
        //                                Main.spriteBatch.Draw(modCactusTexture, new Vector2(j * 16 - (int)Main.screenPosition.X - ((float)num8 - 16f) / 2f, i * 16 - (int)Main.screenPosition.y + num11) + zero, new Microsoft.Xna.Framework.Rectangle(num9, num10, num8, num12), color, 0f, default(Vector2), 1f, effects, 0f);
        //                            }
        //                            else if (flag13)
        //                            {
        //                                Main.spriteBatch.Draw(Main.evilCactusTexture, new Vector2((float)(j * 16 - (int)Main.screenPosition.X) - ((float)num8 - 16f) / 2f, (float)(i * 16 - (int)Main.screenPosition.y + num11)) + zero, new Microsoft.Xna.Framework.Rectangle?(new Microsoft.Xna.Framework.Rectangle((int)num9, (int)num10, num8, num12)), color, 0f, default(Vector2), 1f, effects, 0f);
        //                            }
        //                            else if (flag15)
        //                            {
        //                                Main.spriteBatch.Draw(Main.crimsonCactusTexture, new Vector2((float)(j * 16 - (int)Main.screenPosition.X) - ((float)num8 - 16f) / 2f, (float)(i * 16 - (int)Main.screenPosition.y + num11)) + zero, new Microsoft.Xna.Framework.Rectangle?(new Microsoft.Xna.Framework.Rectangle((int)num9, (int)num10, num8, num12)), color, 0f, default(Vector2), 1f, effects, 0f);
        //                            }
        //                            else if (flag14)
        //                            {
        //                                Main.spriteBatch.Draw(Main.goodCactusTexture, new Vector2((float)(j * 16 - (int)Main.screenPosition.X) - ((float)num8 - 16f) / 2f, (float)(i * 16 - (int)Main.screenPosition.y + num11)) + zero, new Microsoft.Xna.Framework.Rectangle?(new Microsoft.Xna.Framework.Rectangle((int)num9, (int)num10, num8, num12)), color, 0f, default(Vector2), 1f, effects, 0f);
        //                            }
        //                            else if (Main.canDrawColorTile(j, i))
        //                            {
        //                                Main.spriteBatch.Draw(Main.tileAltTexture[(int)type, (int)Tile.color()], new Vector2((float)(j * 16 - (int)Main.screenPosition.X) - ((float)num8 - 16f) / 2f, (float)(i * 16 - (int)Main.screenPosition.y + num11)) + zero, new Microsoft.Xna.Framework.Rectangle?(new Microsoft.Xna.Framework.Rectangle((int)num9, (int)num10, num8, num12)), color, 0f, default(Vector2), 1f, effects, 0f);
        //                            }
        //                            else
        //                            {
        //                                Main.spriteBatch.Draw(Main.tileTexture[(int)type], new Vector2((float)(j * 16 - (int)Main.screenPosition.X) - ((float)num8 - 16f) / 2f, (float)(i * 16 - (int)Main.screenPosition.y + num11)) + zero, new Microsoft.Xna.Framework.Rectangle?(new Microsoft.Xna.Framework.Rectangle((int)num9, (int)num10, num8, num12)), color, 0f, default(Vector2), 1f, effects, 0f);
        //                            }
        //                        }
        //                        else if (type == 272 && !Tile.halfBrick() && !Main.Tile[j - 1, i].halfBrick() && !Main.Tile[j + 1, i].halfBrick())
        //                        {
        //                            int num168 = Main.tileFrame[(int)type];
        //                            num168 += j % 2;
        //                            num168 += i % 2;
        //                            num168 += j % 3;
        //                            for (num168 += i % 3; num168 > 1; num168 -= 2)
        //                            {
        //                            }
        //                            num168 *= 90;
        //                            if (Tile.inActive())
        //                            {
        //                                color = Tile.actColor(color);
        //                            }
        //                            else if (Main.tileShine2[(int)type])
        //                            {
        //                                color = Main.shine(color, (int)type);
        //                            }
        //                            if (Main.canDrawColorTile(j, i))
        //                            {
        //                                Main.spriteBatch.Draw(Main.tileAltTexture[(int)type, (int)Tile.color()], new Vector2((float)(j * 16 - (int)Main.screenPosition.X) - ((float)num8 - 16f) / 2f, (float)(i * 16 - (int)Main.screenPosition.y + num11)) + zero, new Microsoft.Xna.Framework.Rectangle?(new Microsoft.Xna.Framework.Rectangle((int)num9, (int)num10 + num168, num8, num12)), color, 0f, default(Vector2), 1f, effects, 0f);
        //                            }
        //                            else
        //                            {
        //                                Main.spriteBatch.Draw(Main.tileTexture[(int)type], new Vector2((float)(j * 16 - (int)Main.screenPosition.X) - ((float)num8 - 16f) / 2f, (float)(i * 16 - (int)Main.screenPosition.y + num11)) + zero, new Microsoft.Xna.Framework.Rectangle?(new Microsoft.Xna.Framework.Rectangle((int)num9, (int)num10 + num168, num8, num12)), color, 0f, default(Vector2), 1f, effects, 0f);
        //                            }
        //                        }
        //                        else
        //                        {
        //                            if (type == 160)
        //                            {
        //                                color = new Microsoft.Xna.Framework.Color(Main.DiscoR, Main.DiscoG, Main.DiscoB, 255);
        //                            }
        //                            if (type != 19 && type != 380 && Main.tileSolid[(int)type] && !TileID.Sets.NotReallySolid[(int)type] && !Tile.halfBrick() && (Main.Tile[j - 1, i].halfBrick() || Main.Tile[j + 1, i].halfBrick()))
        //                            {
        //                                if (Tile.inActive())
        //                                {
        //                                    color = Tile.actColor(color);
        //                                }
        //                                else if (Main.tileShine2[(int)type])
        //                                {
        //                                    color = Main.shine(color, (int)type);
        //                                }
        //                                if (Main.Tile[j - 1, i].halfBrick() && Main.Tile[j + 1, i].halfBrick())
        //                                {
        //                                    if (Main.canDrawColorTile(j, i))
        //                                    {
        //                                        Main.spriteBatch.Draw(Main.tileAltTexture[(int)type, (int)Tile.color()], new Vector2((float)(j * 16 - (int)Main.screenPosition.X) - ((float)num8 - 16f) / 2f, (float)(i * 16 - (int)Main.screenPosition.y + num11 + 8)) + zero, new Microsoft.Xna.Framework.Rectangle?(new Microsoft.Xna.Framework.Rectangle((int)num9 + num16, num15 + (int)num10 + 8, num8, 8)), color, 0f, default(Vector2), 1f, effects, 0f);
        //                                        Main.spriteBatch.Draw(Main.tileAltTexture[(int)type, (int)Tile.color()], new Vector2((float)(j * 16 - (int)Main.screenPosition.X) - ((float)num8 - 16f) / 2f, (float)(i * 16 - (int)Main.screenPosition.y + num11)) + zero, new Microsoft.Xna.Framework.Rectangle?(new Microsoft.Xna.Framework.Rectangle(126 + num16, num15, 16, 8)), color, 0f, default(Vector2), 1f, effects, 0f);
        //                                    }
        //                                    else
        //                                    {
        //                                        Main.spriteBatch.Draw(Main.tileTexture[(int)type], new Vector2((float)(j * 16 - (int)Main.screenPosition.X) - ((float)num8 - 16f) / 2f, (float)(i * 16 - (int)Main.screenPosition.y + num11 + 8)) + zero, new Microsoft.Xna.Framework.Rectangle?(new Microsoft.Xna.Framework.Rectangle((int)num9 + num16, num15 + (int)num10 + 8, num8, 8)), color, 0f, default(Vector2), 1f, effects, 0f);
        //                                        if (!Main.Tile[j, i - 1].bottomSlope() && Main.Tile[j, i - 1].type == type)
        //                                        {
        //                                            Main.spriteBatch.Draw(Main.tileTexture[(int)type], new Vector2((float)(j * 16 - (int)Main.screenPosition.X) - ((float)num8 - 16f) / 2f, (float)(i * 16 - (int)Main.screenPosition.y + num11)) + zero, new Microsoft.Xna.Framework.Rectangle?(new Microsoft.Xna.Framework.Rectangle(90 + num16, num15, 16, 8)), color, 0f, default(Vector2), 1f, effects, 0f);
        //                                        }
        //                                        else
        //                                        {
        //                                            Main.spriteBatch.Draw(Main.tileTexture[(int)type], new Vector2((float)(j * 16 - (int)Main.screenPosition.X) - ((float)num8 - 16f) / 2f, (float)(i * 16 - (int)Main.screenPosition.y + num11)) + zero, new Microsoft.Xna.Framework.Rectangle?(new Microsoft.Xna.Framework.Rectangle(126 + num16, num15, 16, 8)), color, 0f, default(Vector2), 1f, effects, 0f);
        //                                        }
        //                                    }
        //                                }
        //                                else if (Main.Tile[j - 1, i].halfBrick())
        //                                {
        //                                    if (Main.canDrawColorTile(j, i))
        //                                    {
        //                                        Main.spriteBatch.Draw(Main.tileAltTexture[(int)type, (int)Tile.color()], new Vector2((float)(j * 16 - (int)Main.screenPosition.X) - ((float)num8 - 16f) / 2f, (float)(i * 16 - (int)Main.screenPosition.y + num11 + 8)) + zero, new Microsoft.Xna.Framework.Rectangle?(new Microsoft.Xna.Framework.Rectangle((int)num9 + num16, num15 + (int)num10 + 8, num8, 8)), color, 0f, default(Vector2), 1f, effects, 0f);
        //                                        Main.spriteBatch.Draw(Main.tileAltTexture[(int)type, (int)Tile.color()], new Vector2((float)(j * 16 - (int)Main.screenPosition.X) - ((float)num8 - 16f) / 2f + 4f, (float)(i * 16 - (int)Main.screenPosition.y + num11)) + zero, new Microsoft.Xna.Framework.Rectangle?(new Microsoft.Xna.Framework.Rectangle((int)(num9 + 4) + num16, num15 + (int)num10, num8 - 4, num12)), color, 0f, default(Vector2), 1f, effects, 0f);
        //                                        Main.spriteBatch.Draw(Main.tileAltTexture[(int)type, (int)Tile.color()], new Vector2((float)(j * 16 - (int)Main.screenPosition.X) - ((float)num8 - 16f) / 2f, (float)(i * 16 - (int)Main.screenPosition.y + num11)) + zero, new Microsoft.Xna.Framework.Rectangle?(new Microsoft.Xna.Framework.Rectangle(126 + num16, num15, 4, 8)), color, 0f, default(Vector2), 1f, effects, 0f);
        //                                    }
        //                                    else
        //                                    {
        //                                        Main.spriteBatch.Draw(Main.tileTexture[(int)type], new Vector2((float)(j * 16 - (int)Main.screenPosition.X) - ((float)num8 - 16f) / 2f, (float)(i * 16 - (int)Main.screenPosition.y + num11 + 8)) + zero, new Microsoft.Xna.Framework.Rectangle?(new Microsoft.Xna.Framework.Rectangle((int)num9 + num16, num15 + (int)num10 + 8, num8, 8)), color, 0f, default(Vector2), 1f, effects, 0f);
        //                                        Main.spriteBatch.Draw(Main.tileTexture[(int)type], new Vector2((float)(j * 16 - (int)Main.screenPosition.X) - ((float)num8 - 16f) / 2f + 4f, (float)(i * 16 - (int)Main.screenPosition.y + num11)) + zero, new Microsoft.Xna.Framework.Rectangle?(new Microsoft.Xna.Framework.Rectangle((int)(num9 + 4) + num16, num15 + (int)num10, num8 - 4, num12)), color, 0f, default(Vector2), 1f, effects, 0f);
        //                                        Main.spriteBatch.Draw(Main.tileTexture[(int)type], new Vector2((float)(j * 16 - (int)Main.screenPosition.X) - ((float)num8 - 16f) / 2f, (float)(i * 16 - (int)Main.screenPosition.y + num11)) + zero, new Microsoft.Xna.Framework.Rectangle?(new Microsoft.Xna.Framework.Rectangle(126 + num16, num15, 4, 8)), color, 0f, default(Vector2), 1f, effects, 0f);
        //                                    }
        //                                }
        //                                else if (Main.Tile[j + 1, i].halfBrick())
        //                                {
        //                                    if (Main.canDrawColorTile(j, i))
        //                                    {
        //                                        Main.spriteBatch.Draw(Main.tileAltTexture[(int)type, (int)Tile.color()], new Vector2((float)(j * 16 - (int)Main.screenPosition.X) - ((float)num8 - 16f) / 2f, (float)(i * 16 - (int)Main.screenPosition.y + num11 + 8)) + zero, new Microsoft.Xna.Framework.Rectangle?(new Microsoft.Xna.Framework.Rectangle((int)num9 + num16, num15 + (int)num10 + 8, num8, 8)), color, 0f, default(Vector2), 1f, effects, 0f);
        //                                        Main.spriteBatch.Draw(Main.tileAltTexture[(int)type, (int)Tile.color()], new Vector2((float)(j * 16 - (int)Main.screenPosition.X) - ((float)num8 - 16f) / 2f, (float)(i * 16 - (int)Main.screenPosition.y + num11)) + zero, new Microsoft.Xna.Framework.Rectangle?(new Microsoft.Xna.Framework.Rectangle((int)num9 + num16, num15 + (int)num10, num8 - 4, num12)), color, 0f, default(Vector2), 1f, effects, 0f);
        //                                        Main.spriteBatch.Draw(Main.tileAltTexture[(int)type, (int)Tile.color()], new Vector2((float)(j * 16 - (int)Main.screenPosition.X) - ((float)num8 - 16f) / 2f + 12f, (float)(i * 16 - (int)Main.screenPosition.y + num11)) + zero, new Microsoft.Xna.Framework.Rectangle?(new Microsoft.Xna.Framework.Rectangle(138 + num16, num15, 4, 8)), color, 0f, default(Vector2), 1f, effects, 0f);
        //                                    }
        //                                    else
        //                                    {
        //                                        Main.spriteBatch.Draw(Main.tileTexture[(int)type], new Vector2((float)(j * 16 - (int)Main.screenPosition.X) - ((float)num8 - 16f) / 2f, (float)(i * 16 - (int)Main.screenPosition.y + num11 + 8)) + zero, new Microsoft.Xna.Framework.Rectangle?(new Microsoft.Xna.Framework.Rectangle((int)num9 + num16, num15 + (int)num10 + 8, num8, 8)), color, 0f, default(Vector2), 1f, effects, 0f);
        //                                        Main.spriteBatch.Draw(Main.tileTexture[(int)type], new Vector2((float)(j * 16 - (int)Main.screenPosition.X) - ((float)num8 - 16f) / 2f, (float)(i * 16 - (int)Main.screenPosition.y + num11)) + zero, new Microsoft.Xna.Framework.Rectangle?(new Microsoft.Xna.Framework.Rectangle((int)num9 + num16, num15 + (int)num10, num8 - 4, num12)), color, 0f, default(Vector2), 1f, effects, 0f);
        //                                        Main.spriteBatch.Draw(Main.tileTexture[(int)type], new Vector2((float)(j * 16 - (int)Main.screenPosition.X) - ((float)num8 - 16f) / 2f + 12f, (float)(i * 16 - (int)Main.screenPosition.y + num11)) + zero, new Microsoft.Xna.Framework.Rectangle?(new Microsoft.Xna.Framework.Rectangle(138, 0, 4, 8)), color, 0f, default(Vector2), 1f, effects, 0f);
        //                                    }
        //                                }
        //                                else if (Main.canDrawColorTile(j, i))
        //                                {
        //                                    Main.spriteBatch.Draw(Main.tileAltTexture[(int)type, (int)Tile.color()], new Vector2((float)(j * 16 - (int)Main.screenPosition.X) - ((float)num8 - 16f) / 2f, (float)(i * 16 - (int)Main.screenPosition.y + num11)) + zero, new Microsoft.Xna.Framework.Rectangle?(new Microsoft.Xna.Framework.Rectangle((int)num9 + num16, num15 + (int)num10, num8, num12)), color, 0f, default(Vector2), 1f, effects, 0f);
        //                                }
        //                                else
        //                                {
        //                                    Main.spriteBatch.Draw(Main.tileTexture[(int)type], new Vector2((float)(j * 16 - (int)Main.screenPosition.X) - ((float)num8 - 16f) / 2f, (float)(i * 16 - (int)Main.screenPosition.y + num11)) + zero, new Microsoft.Xna.Framework.Rectangle?(new Microsoft.Xna.Framework.Rectangle((int)num9 + num16, num15 + (int)num10, num8, num12)), color, 0f, default(Vector2), 1f, effects, 0f);
        //                                }
        //                            }
        //                            else if (Lighting.NotRetro && Main.tileSolid[(int)type] && type != 137 && type != 235 && type != 388 && !Tile.halfBrick() && !Tile.inActive())
        //                            {
        //                                if ((int)color.R > num || (double)color.G > (double)num * 1.1 || (double)color.B > (double)num * 1.2)
        //                                {
        //                                    Lighting.GetColor9Slice(j, i, ref array);
        //                                    bool flag17 = Tile.inActive();
        //                                    bool flag18 = Main.tileShine2[(int)type];
        //                                    Texture2D texture;
        //                                    if (Main.canDrawColorTile(j, i))
        //                                    {
        //                                        texture = Main.tileAltTexture[(int)type, (int)Tile.color()];
        //                                    }
        //                                    else
        //                                    {
        //                                        texture = Main.tileTexture[(int)type];
        //                                    }
        //                                    for (int num169 = 0; num169 < 9; num169++)
        //                                    {
        //                                        int num170 = 0;
        //                                        int num171 = 0;
        //                                        int width = 4;
        //                                        int height6 = 4;
        //                                        switch (num169)
        //                                        {
        //                                            case 1:
        //                                                width = 8;
        //                                                num170 = 4;
        //                                                break;
        //                                            case 2:
        //                                                num170 = 12;
        //                                                break;
        //                                            case 3:
        //                                                height6 = 8;
        //                                                num171 = 4;
        //                                                break;
        //                                            case 4:
        //                                                width = 8;
        //                                                height6 = 8;
        //                                                num170 = 4;
        //                                                num171 = 4;
        //                                                break;
        //                                            case 5:
        //                                                num170 = 12;
        //                                                num171 = 4;
        //                                                height6 = 8;
        //                                                break;
        //                                            case 6:
        //                                                num171 = 12;
        //                                                break;
        //                                            case 7:
        //                                                width = 8;
        //                                                height6 = 4;
        //                                                num170 = 4;
        //                                                num171 = 12;
        //                                                break;
        //                                            case 8:
        //                                                num170 = 12;
        //                                                num171 = 12;
        //                                                break;
        //                                        }
        //                                        Microsoft.Xna.Framework.Color color11 = color;
        //                                        Microsoft.Xna.Framework.Color color12 = array[num169];
        //                                        color11.R = (byte)((color.R + color12.R) / 2);
        //                                        color11.G = (byte)((color.G + color12.G) / 2);
        //                                        color11.B = (byte)((color.B + color12.B) / 2);
        //                                        if (flag17)
        //                                        {
        //                                            color11 = Tile.actColor(color11);
        //                                        }
        //                                        else if (flag18)
        //                                        {
        //                                            color11 = Main.shine(color11, (int)type);
        //                                        }
        //                                        Main.spriteBatch.Draw(texture, new Vector2((float)(j * 16 - (int)Main.screenPosition.X) - ((float)num8 - 16f) / 2f + (float)num170, (float)(i * 16 - (int)Main.screenPosition.y + num11 + num171)) + zero, new Microsoft.Xna.Framework.Rectangle?(new Microsoft.Xna.Framework.Rectangle((int)num9 + num170 + num16, (int)num10 + num171 + num15, width, height6)), color11, 0f, default(Vector2), 1f, effects, 0f);
        //                                    }
        //                                }
        //                                else if ((int)color.R > num2 || (double)color.G > (double)num2 * 1.1 || (double)color.B > (double)num2 * 1.2)
        //                                {
        //                                    Lighting.GetColor4Slice(j, i, ref array);
        //                                    bool flag19 = Tile.inActive();
        //                                    bool flag20 = Main.tileShine2[(int)type];
        //                                    Texture2D texture;
        //                                    if (Main.canDrawColorTile(j, i))
        //                                    {
        //                                        texture = Main.tileAltTexture[(int)type, (int)Tile.color()];
        //                                    }
        //                                    else
        //                                    {
        //                                        texture = Main.tileTexture[(int)type];
        //                                    }
        //                                    for (int num172 = 0; num172 < 4; num172++)
        //                                    {
        //                                        int num173 = 0;
        //                                        int num174 = 0;
        //                                        switch (num172)
        //                                        {
        //                                            case 1:
        //                                                num173 = 8;
        //                                                break;
        //                                            case 2:
        //                                                num174 = 8;
        //                                                break;
        //                                            case 3:
        //                                                num173 = 8;
        //                                                num174 = 8;
        //                                                break;
        //                                        }
        //                                        Microsoft.Xna.Framework.Color color13 = color;
        //                                        Microsoft.Xna.Framework.Color color14 = array[num172];
        //                                        color13.R = (byte)((color.R + color14.R) / 2);
        //                                        color13.G = (byte)((color.G + color14.G) / 2);
        //                                        color13.B = (byte)((color.B + color14.B) / 2);
        //                                        if (flag19)
        //                                        {
        //                                            color13 = Tile.actColor(color13);
        //                                        }
        //                                        else if (flag20)
        //                                        {
        //                                            color13 = Main.shine(color13, (int)type);
        //                                        }
        //                                        Main.spriteBatch.Draw(texture, new Vector2((float)(j * 16 - (int)Main.screenPosition.X) - ((float)num8 - 16f) / 2f + (float)num173, (float)(i * 16 - (int)Main.screenPosition.y + num11 + num174)) + zero, new Microsoft.Xna.Framework.Rectangle?(new Microsoft.Xna.Framework.Rectangle((int)num9 + num173 + num16, (int)num10 + num174 + num15, 8, 8)), color13, 0f, default(Vector2), 1f, effects, 0f);
        //                                    }
        //                                }
        //                                else
        //                                {
        //                                    if (Tile.inActive())
        //                                    {
        //                                        color = Tile.actColor(color);
        //                                    }
        //                                    else if (Main.tileShine2[(int)type])
        //                                    {
        //                                        color = Main.shine(color, (int)type);
        //                                    }
        //                                    Texture2D texture;
        //                                    if (Main.canDrawColorTile(j, i))
        //                                    {
        //                                        texture = Main.tileAltTexture[(int)type, (int)Tile.color()];
        //                                    }
        //                                    else
        //                                    {
        //                                        texture = Main.tileTexture[(int)type];
        //                                    }
        //                                    Main.spriteBatch.Draw(texture, new Vector2((float)(j * 16 - (int)Main.screenPosition.X) - ((float)num8 - 16f) / 2f, (float)(i * 16 - (int)Main.screenPosition.y + num11)) + zero, new Microsoft.Xna.Framework.Rectangle?(new Microsoft.Xna.Framework.Rectangle((int)num9 + num16, (int)num10 + num15, num8, num12)), color, 0f, default(Vector2), 1f, effects, 0f);
        //                                }
        //                            }
        //                            else
        //                            {
        //                                if (Lighting.NotRetro && Main.tileShine2[(int)type])
        //                                {
        //                                    if (type == 21)
        //                                    {
        //                                        if (num9 >= 36 && num9 < 178)
        //                                        {
        //                                            color = Main.shine(color, (int)type);
        //                                        }
        //                                    }
        //                                    else if (!Tile.inActive())
        //                                    {
        //                                        color = Main.shine(color, (int)type);
        //                                    }
        //                                }
        //                                if (Tile.inActive())
        //                                {
        //                                    color = Tile.actColor(color);
        //                                }
        //                                if (type == 128 || type == 269)
        //                                {
        //                                    int num175;
        //                                    for (num175 = (int)num9; num175 >= 100; num175 -= 100)
        //                                    {
        //                                    }
        //                                    Main.spriteBatch.Draw(Main.tileTexture[(int)type], new Vector2((float)(j * 16 - (int)Main.screenPosition.X) - ((float)num8 - 16f) / 2f, (float)(i * 16 - (int)Main.screenPosition.y + num11)) + zero, new Microsoft.Xna.Framework.Rectangle?(new Microsoft.Xna.Framework.Rectangle(num175, (int)num10, num8, num12)), color, 0f, default(Vector2), 1f, effects, 0f);
        //                                }
        //                                else if (type == 334)
        //                                {
        //                                    int num176 = (int)num9;
        //                                    int num177 = 0;
        //                                    while (num176 >= 5000)
        //                                    {
        //                                        num176 -= 5000;
        //                                        num177++;
        //                                    }
        //                                    if (num177 != 0)
        //                                    {
        //                                        num176 = (num177 - 1) * 18;
        //                                    }
        //                                    Main.spriteBatch.Draw(Main.tileTexture[(int)type], new Vector2((float)(j * 16 - (int)Main.screenPosition.X) - ((float)num8 - 16f) / 2f, (float)(i * 16 - (int)Main.screenPosition.y + num11)) + zero, new Microsoft.Xna.Framework.Rectangle?(new Microsoft.Xna.Framework.Rectangle(num176, (int)num10, num8, num12)), color, 0f, default(Vector2), 1f, effects, 0f);
        //                                }
        //                                else if (type == 5)
        //                                {
        //                                    int num178 = j;
        //                                    int num179 = i;
        //                                    if (num9 == 66 && num10 <= 45)
        //                                    {
        //                                        num178++;
        //                                    }
        //                                    if (num9 == 88 && num10 >= 66 && num10 <= 110)
        //                                    {
        //                                        num178--;
        //                                    }
        //                                    if (num9 == 22 && num10 >= 132)
        //                                    {
        //                                        num178--;
        //                                    }
        //                                    if (num9 == 44 && num10 >= 132)
        //                                    {
        //                                        num178++;
        //                                    }
        //                                    while (Main.Tile[num178, num179].active() && Main.Tile[num178, num179].type == 5)
        //                                    {
        //                                        num179++;
        //                                    }

        //                                    MethodInfo GetTreeVariant = typeof(Main).GetMethod("GetTreeVariant",
        //                                        BindingFlags.NonPublic | BindingFlags.Static);
        //                                    Object[] arg = {num178, num179};

        //                                    int treeVariant = (int)GetTreeVariant.Invoke(null, arg);
        //                                    Texture2D modTreeTexture = TileLoader.GetTreeTexture(Main.Tile[num178, num179]);
        //                                    if (modTreeTexture != null)
        //                                    {
        //                                        Main.spriteBatch.Draw(modTreeTexture, new Vector2(j * 16 - (int)Main.screenPosition.X - (num8 - 16f) / 2f, i * 16 - (int)Main.screenPosition.y + num11) + zero, new Microsoft.Xna.Framework.Rectangle(num9, num10, num8, num12), color, 0f, default(Vector2), 1f, effects, 0f);
        //                                    }
        //                                    else if (treeVariant == -1)
        //                                    {
        //                                        if (Main.canDrawColorTile(j, i))
        //                                        {
        //                                            //patch file
        //                                            Main.spriteBatch.Draw(Main.tileAltTexture[(int)type, (int)Tile.color()], new Vector2((float)(j * 16 - (int)Main.screenPosition.X) - ((float)num8 - 16f) / 2f, (float)(i * 16 - (int)Main.screenPosition.y + num11)) + zero, new Microsoft.Xna.Framework.Rectangle?(new Microsoft.Xna.Framework.Rectangle((int)num9, (int)num10, num8, num12)), color, 0f, default(Vector2), 1f, effects, 0f);
        //                                        }
        //                                        else
        //                                        {
        //                                            Main.spriteBatch.Draw(Main.tileTexture[(int)type], new Vector2((float)(j * 16 - (int)Main.screenPosition.X) - ((float)num8 - 16f) / 2f, (float)(i * 16 - (int)Main.screenPosition.y + num11)) + zero, new Microsoft.Xna.Framework.Rectangle?(new Microsoft.Xna.Framework.Rectangle((int)num9, (int)num10, num8, num12)), color, 0f, default(Vector2), 1f, effects, 0f);
        //                                        }
        //                                    }
        //                                    else if (Main.canDrawColorTree(j, i, treeVariant))
        //                                    {
        //                                        Main.spriteBatch.Draw(Main.woodAltTexture[treeVariant, (int)Tile.color()], new Vector2((float)(j * 16 - (int)Main.screenPosition.X) - ((float)num8 - 16f) / 2f, (float)(i * 16 - (int)Main.screenPosition.y + num11)) + zero, new Microsoft.Xna.Framework.Rectangle?(new Microsoft.Xna.Framework.Rectangle((int)num9, (int)num10, num8, num12)), color, 0f, default(Vector2), 1f, effects, 0f);
        //                                    }
        //                                    else
        //                                    {
        //                                        Main.spriteBatch.Draw(Main.woodTexture[treeVariant], new Vector2((float)(j * 16 - (int)Main.screenPosition.X) - ((float)num8 - 16f) / 2f, (float)(i * 16 - (int)Main.screenPosition.y + num11)) + zero, new Microsoft.Xna.Framework.Rectangle?(new Microsoft.Xna.Framework.Rectangle((int)num9, (int)num10, num8, num12)), color, 0f, default(Vector2), 1f, effects, 0f);
        //                                    }
        //                                }
        //                                else if (type == 323)
        //                                {
        //                                    int num180 = -1;
        //                                    int num181 = j;
        //                                    int num182 = i;
        //                                    while (Main.Tile[num181, num182].active() && Main.Tile[num181, num182].type == 323)
        //                                    {
        //                                        num182++;
        //                                    }
        //                                    if (Main.Tile[num181, num182].active() && Main.Tile[num181, num182].type == 53)
        //                                    {
        //                                        num180 = 0;
        //                                    }
        //                                    if (Main.Tile[num181, num182].active() && Main.Tile[num181, num182].type == 234)
        //                                    {
        //                                        num180 = 1;
        //                                    }
        //                                    if (Main.Tile[num181, num182].active() && Main.Tile[num181, num182].type == 116)
        //                                    {
        //                                        num180 = 2;
        //                                    }
        //                                    if (Main.Tile[num181, num182].active() && Main.Tile[num181, num182].type == 112)
        //                                    {
        //                                        //patch file: num181, num182
        //                                        num180 = 3;
        //                                    }
        //                                    int y2 = 22 * num180;
        //                                    int num183 = (int)num10;
        //                                    Texture2D modTreeTexture = TileLoader.GetPalmTreeTexture(Main.Tile[num181, num182]);
        //                                    if (modTreeTexture != null)
        //                                    {
        //                                        Main.spriteBatch.Draw(modTreeTexture, new Vector2(j * 16 - (int)Main.screenPosition.X - (num8 - 16f) / 2f + num183, i * 16 - (int)Main.screenPosition.y + num11) + zero, new Microsoft.Xna.Framework.Rectangle(num9, 0, num8, num12), color, 0f, default(Vector2), 1f, effects, 0f);
        //                                    }
        //                                    else if (Main.canDrawColorTile(j, i))
        //                                    {
        //                                        Main.spriteBatch.Draw(Main.tileAltTexture[(int)type, (int)Tile.color()], new Vector2((float)(j * 16 - (int)Main.screenPosition.X) - ((float)num8 - 16f) / 2f + (float)num183, (float)(i * 16 - (int)Main.screenPosition.y + num11)) + zero, new Microsoft.Xna.Framework.Rectangle?(new Microsoft.Xna.Framework.Rectangle((int)num9, y2, num8, num12)), color, 0f, default(Vector2), 1f, effects, 0f);
        //                                    }
        //                                    else
        //                                    {
        //                                        Main.spriteBatch.Draw(Main.tileTexture[(int)type], new Vector2((float)(j * 16 - (int)Main.screenPosition.X) - ((float)num8 - 16f) / 2f + (float)num183, (float)(i * 16 - (int)Main.screenPosition.y + num11)) + zero, new Microsoft.Xna.Framework.Rectangle?(new Microsoft.Xna.Framework.Rectangle((int)num9, y2, num8, num12)), color, 0f, default(Vector2), 1f, effects, 0f);
        //                                    }
        //                                }
        //                                else
        //                                {
        //                                    if (num14 == 8 && (!Main.Tile[j, i + 1].active() || !Main.tileSolid[(int)Main.Tile[j, i + 1].type] || Main.Tile[j, i + 1].halfBrick()))
        //                                    {
        //                                        Texture2D texture;
        //                                        if (Main.canDrawColorTile(j, i))
        //                                        {
        //                                            texture = Main.tileAltTexture[(int)type, (int)Tile.color()];
        //                                        }
        //                                        else
        //                                        {
        //                                            texture = Main.tileTexture[(int)type];
        //                                        }
        //                                        if (TileID.Sets.Platforms[(int)type])
        //                                        {
        //                                            Main.spriteBatch.Draw(texture, new Vector2((float)(j * 16 - (int)Main.screenPosition.X) - ((float)num8 - 16f) / 2f, (float)(i * 16 - (int)Main.screenPosition.y + num11 + num14)) + zero, new Microsoft.Xna.Framework.Rectangle?(new Microsoft.Xna.Framework.Rectangle((int)num9 + num16, (int)num10 + num15, num8, num12)), color, 0f, default(Vector2), 1f, effects, 0f);
        //                                        }
        //                                        else
        //                                        {
        //                                            Main.spriteBatch.Draw(texture, new Vector2((float)(j * 16 - (int)Main.screenPosition.X) - ((float)num8 - 16f) / 2f, (float)(i * 16 - (int)Main.screenPosition.y + num11 + num14)) + zero, new Microsoft.Xna.Framework.Rectangle?(new Microsoft.Xna.Framework.Rectangle((int)num9 + num16, (int)num10 + num15, num8, num12 - num14 - 4)), color, 0f, default(Vector2), 1f, effects, 0f);
        //                                            Main.spriteBatch.Draw(texture, new Vector2((float)(j * 16 - (int)Main.screenPosition.X) - ((float)num8 - 16f) / 2f, (float)(i * 16 - (int)Main.screenPosition.y + num11 + 12)) + zero, new Microsoft.Xna.Framework.Rectangle?(new Microsoft.Xna.Framework.Rectangle(144 + num16, 66 + num15, num8, 4)), color, 0f, default(Vector2), 1f, effects, 0f);
        //                                        }
        //                                    }
        //                                    else
        //                                    {
        //                                        Texture2D texture;
        //                                        if (Main.canDrawColorTile(j, i))
        //                                        {
        //                                            texture = Main.tileAltTexture[(int)type, (int)Tile.color()];
        //                                        }
        //                                        else
        //                                        {
        //                                            texture = Main.tileTexture[(int)type];
        //                                        }
        //                                        Main.spriteBatch.Draw(texture, new Vector2((float)(j * 16 - (int)Main.screenPosition.X) - ((float)num8 - 16f) / 2f, (float)(i * 16 - (int)Main.screenPosition.y + num11 + num14)) + zero, new Microsoft.Xna.Framework.Rectangle?(new Microsoft.Xna.Framework.Rectangle((int)num9 + num16, (int)num10 + num15, num8, num12 - num14)), color, 0f, default(Vector2), 1f, effects, 0f);
        //                                    }
        //                                    if (type == 27)
        //                                    {
        //                                        int num184 = 14;
        //                                        Main.spriteBatch.Draw(Main.FlameTexture[num184], new Vector2((float)(j * 16 - (int)Main.screenPosition.X) - ((float)num8 - 16f) / 2f, (float)(i * 16 - (int)Main.screenPosition.y + num11)) + zero, new Microsoft.Xna.Framework.Rectangle?(new Microsoft.Xna.Framework.Rectangle((int)num9, (int)num10 + num15, num8, num12)), new Microsoft.Xna.Framework.Color(255, 255, 255, 255), 0f, default(Vector2), 1f, effects, 0f);
        //                                    }
        //                                    if (type == 215 && num10 < 36)
        //                                    {
        //                                        int num185 = 15;
        //                                        Microsoft.Xna.Framework.Color color15 = new Microsoft.Xna.Framework.Color(255, 255, 255, 0);
        //                                        if (num9 / 54 == 5)
        //                                        {
        //                                            color15 = new Microsoft.Xna.Framework.Color((float)Main.DiscoR / 255f, (float)Main.DiscoG / 255f, (float)Main.DiscoB / 255f, 0f);
        //                                        }
        //                                        Main.spriteBatch.Draw(Main.FlameTexture[num185], new Vector2((float)(j * 16 - (int)Main.screenPosition.X) - ((float)num8 - 16f) / 2f, (float)(i * 16 - (int)Main.screenPosition.y + num11)) + zero, new Microsoft.Xna.Framework.Rectangle?(new Microsoft.Xna.Framework.Rectangle((int)num9, (int)num10 + num15, num8, num12)), color15, 0f, default(Vector2), 1f, effects, 0f);
        //                                    }
        //                                    if (type == 286)
        //                                    {
        //                                        Main.spriteBatch.Draw(Main.glowSnailTexture, new Vector2((float)(j * 16 - (int)Main.screenPosition.X) - ((float)num8 - 16f) / 2f, (float)(i * 16 - (int)Main.screenPosition.y + num11)) + zero, new Microsoft.Xna.Framework.Rectangle?(new Microsoft.Xna.Framework.Rectangle((int)num9 + num16, (int)num10 + num15, num8, num12)), new Microsoft.Xna.Framework.Color(75, 100, 255, 0), 0f, default(Vector2), 1f, effects, 0f);
        //                                    }
        //                                    if (type == 270)
        //                                    {
        //                                        Main.spriteBatch.Draw(Main.fireflyJarTexture, new Vector2((float)(j * 16 - (int)Main.screenPosition.X) - ((float)num8 - 16f) / 2f, (float)(i * 16 - (int)Main.screenPosition.y + num11)) + zero, new Microsoft.Xna.Framework.Rectangle?(new Microsoft.Xna.Framework.Rectangle((int)num9 + num16, (int)num10, num8, num12)), new Microsoft.Xna.Framework.Color(200, 200, 200, 0), 0f, default(Vector2), 1f, effects, 0f);
        //                                    }
        //                                    if (type == 271)
        //                                    {
        //                                        Main.spriteBatch.Draw(Main.lightningbugJarTexture, new Vector2((float)(j * 16 - (int)Main.screenPosition.X) - ((float)num8 - 16f) / 2f, (float)(i * 16 - (int)Main.screenPosition.y + num11)) + zero, new Microsoft.Xna.Framework.Rectangle?(new Microsoft.Xna.Framework.Rectangle((int)num9 + num16, (int)num10, num8, num12)), new Microsoft.Xna.Framework.Color(200, 200, 200, 0), 0f, default(Vector2), 1f, effects, 0f);
        //                                    }
        //                                    if (type == 316 || type == 317 || type == 318)
        //                                    {
        //                                        int num186 = j - (int)(num9 / 18);
        //                                        int num187 = i - (int)(num10 / 18);
        //                                        int num188 = num186 / 2 * (num187 / 3);
        //                                        num188 %= Main.cageFrames;
        //                                        Main.spriteBatch.Draw(Main.jellyfishBowlTexture[(int)(type - 316)], new Vector2((float)(j * 16 - (int)Main.screenPosition.X) - ((float)num8 - 16f) / 2f, (float)(i * 16 - (int)Main.screenPosition.y + num11)) + zero, new Microsoft.Xna.Framework.Rectangle?(new Microsoft.Xna.Framework.Rectangle((int)num9 + num16, (int)num10 + Main.jellyfishCageFrame[(int)(type - 316), num188] * 36, num8, num12)), new Microsoft.Xna.Framework.Color(200, 200, 200, 0), 0f, default(Vector2), 1f, effects, 0f);
        //                                    }
        //                                    if (type == 149 && num9 < 54)
        //                                    {
        //                                        Main.spriteBatch.Draw(Main.xmasLightTexture, new Vector2((float)(j * 16 - (int)Main.screenPosition.X) - ((float)num8 - 16f) / 2f, (float)(i * 16 - (int)Main.screenPosition.y + num11)) + zero, new Microsoft.Xna.Framework.Rectangle?(new Microsoft.Xna.Framework.Rectangle((int)num9, (int)num10, num8, num12)), new Microsoft.Xna.Framework.Color(200, 200, 200, 0), 0f, default(Vector2), 1f, effects, 0f);
        //                                    }
        //                                    if (type == 300 || type == 302 || type == 303 || type == 306)
        //                                    {
        //                                        int num189 = 9;
        //                                        if (type == 302)
        //                                        {
        //                                            num189 = 10;
        //                                        }
        //                                        if (type == 303)
        //                                        {
        //                                            num189 = 11;
        //                                        }
        //                                        if (type == 306)
        //                                        {
        //                                            num189 = 12;
        //                                        }
        //                                        Main.spriteBatch.Draw(Main.FlameTexture[num189], new Vector2((float)(j * 16 - (int)Main.screenPosition.X) - ((float)num8 - 16f) / 2f, (float)(i * 16 - (int)Main.screenPosition.y + num11)) + zero, new Microsoft.Xna.Framework.Rectangle?(new Microsoft.Xna.Framework.Rectangle((int)num9, (int)num10 + num15, num8, num12)), new Microsoft.Xna.Framework.Color(200, 200, 200, 0), 0f, default(Vector2), 1f, effects, 0f);
        //                                    }
        //                                    else if (Main.tileFlame[(int)type])
        //                                    {
        //                                        FieldInfo _tileFrameSeedInfo = typeof(Main).GetField("_tileFrameSeed",
        //                                            BindingFlags.NonPublic | BindingFlags.Static);

        //                                        ulong num190 = (ulong) _tileFrameSeedInfo.GetValue(null) ^ (ulong)(((long)j << 32) | (long)(ulong)i);
        //                                        int num191 = (int)type;
        //                                        int num192 = 0;
        //                                        if (num191 == 4)
        //                                        {
        //                                            num192 = 0;
        //                                        }
        //                                        else if (num191 == 33 || num191 == 174)
        //                                        {
        //                                            num192 = 1;
        //                                        }
        //                                        else if (num191 == 100 || num191 == 173)
        //                                        {
        //                                            num192 = 2;
        //                                        }
        //                                        else if (num191 == 34)
        //                                        {
        //                                            num192 = 3;
        //                                        }
        //                                        else if (num191 == 93)
        //                                        {
        //                                            num192 = 4;
        //                                        }
        //                                        else if (num191 == 49)
        //                                        {
        //                                            num192 = 5;
        //                                        }
        //                                        else if (num191 == 372)
        //                                        {
        //                                            num192 = 16;
        //                                        }
        //                                        else if (num191 == 98)
        //                                        {
        //                                            num192 = 6;
        //                                        }
        //                                        else if (num191 == 35)
        //                                        {
        //                                            num192 = 7;
        //                                        }
        //                                        else if (num191 == 42)
        //                                        {
        //                                            num192 = 13;
        //                                        }
        //                                        if (num192 == 7)
        //                                        {
        //                                            for (int num193 = 0; num193 < 4; num193++)
        //                                            {
        //                                                float num194 = (float)Utils.RandomInt(ref num190, -10, 11) * 0.15f;
        //                                                float num195 = (float)Utils.RandomInt(ref num190, -10, 10) * 0.15f;
        //                                                num194 = 0f;
        //                                                num195 = 0f;
        //                                                Main.spriteBatch.Draw(Main.FlameTexture[num192], new Vector2((float)(j * 16 - (int)Main.screenPosition.X) - ((float)num8 - 16f) / 2f + num194, (float)(i * 16 - (int)Main.screenPosition.y + num11) + num195) + zero, new Microsoft.Xna.Framework.Rectangle?(new Microsoft.Xna.Framework.Rectangle((int)num9, (int)num10, num8, num12)), new Microsoft.Xna.Framework.Color(50, 50, 50, 0), 0f, default(Vector2), 1f, effects, 0f);
        //                                            }
        //                                        }
        //                                        else if (num192 == 1)
        //                                        {
        //                                            int num196 = (int)(Main.Tile[j, i].frameY / 22);
        //                                            if (num196 == 5 || num196 == 6 || num196 == 7 || num196 == 10)
        //                                            {
        //                                                for (int num197 = 0; num197 < 7; num197++)
        //                                                {
        //                                                    float num198 = (float)Utils.RandomInt(ref num190, -10, 11) * 0.075f;
        //                                                    float num199 = (float)Utils.RandomInt(ref num190, -10, 11) * 0.075f;
        //                                                    Main.spriteBatch.Draw(Main.FlameTexture[num192], new Vector2((float)(j * 16 - (int)Main.screenPosition.X) - ((float)num8 - 16f) / 2f + num198, (float)(i * 16 - (int)Main.screenPosition.y + num11) + num199) + zero, new Microsoft.Xna.Framework.Rectangle?(new Microsoft.Xna.Framework.Rectangle((int)num9, (int)num10, num8, num12)), new Microsoft.Xna.Framework.Color(50, 50, 50, 0), 0f, default(Vector2), 1f, effects, 0f);
        //                                                }
        //                                            }
        //                                            else if (num196 == 8)
        //                                            {
        //                                                for (int num200 = 0; num200 < 7; num200++)
        //                                                {
        //                                                    float num201 = (float)Utils.RandomInt(ref num190, -10, 11) * 0.3f;
        //                                                    float num202 = (float)Utils.RandomInt(ref num190, -10, 11) * 0.3f;
        //                                                    Main.spriteBatch.Draw(Main.FlameTexture[num192], new Vector2((float)(j * 16 - (int)Main.screenPosition.X) - ((float)num8 - 16f) / 2f + num201, (float)(i * 16 - (int)Main.screenPosition.y + num11) + num202) + zero, new Microsoft.Xna.Framework.Rectangle?(new Microsoft.Xna.Framework.Rectangle((int)num9, (int)num10, num8, num12)), new Microsoft.Xna.Framework.Color(50, 50, 50, 0), 0f, default(Vector2), 1f, effects, 0f);
        //                                                }
        //                                            }
        //                                            else if (num196 == 12)
        //                                            {
        //                                                for (int num203 = 0; num203 < 7; num203++)
        //                                                {
        //                                                    float num204 = (float)Utils.RandomInt(ref num190, -10, 11) * 0.1f;
        //                                                    float num205 = (float)Utils.RandomInt(ref num190, -10, 1) * 0.15f;
        //                                                    Main.spriteBatch.Draw(Main.FlameTexture[num192], new Vector2((float)(j * 16 - (int)Main.screenPosition.X) - ((float)num8 - 16f) / 2f + num204, (float)(i * 16 - (int)Main.screenPosition.y + num11) + num205) + zero, new Microsoft.Xna.Framework.Rectangle?(new Microsoft.Xna.Framework.Rectangle((int)num9, (int)num10, num8, num12)), new Microsoft.Xna.Framework.Color(50, 50, 50, 0), 0f, default(Vector2), 1f, effects, 0f);
        //                                                }
        //                                            }
        //                                            else if (num196 == 14)
        //                                            {
        //                                                for (int num206 = 0; num206 < 8; num206++)
        //                                                {
        //                                                    float num207 = (float)Utils.RandomInt(ref num190, -10, 11) * 0.1f;
        //                                                    float num208 = (float)Utils.RandomInt(ref num190, -10, 11) * 0.1f;
        //                                                    Main.spriteBatch.Draw(Main.FlameTexture[num192], new Vector2((float)(j * 16 - (int)Main.screenPosition.X) - ((float)num8 - 16f) / 2f + num207, (float)(i * 16 - (int)Main.screenPosition.y + num11) + num208) + zero, new Microsoft.Xna.Framework.Rectangle?(new Microsoft.Xna.Framework.Rectangle((int)num9, (int)num10, num8, num12)), new Microsoft.Xna.Framework.Color(75, 75, 75, 0), 0f, default(Vector2), 1f, effects, 0f);
        //                                                }
        //                                            }
        //                                            else if (num196 == 16)
        //                                            {
        //                                                for (int num209 = 0; num209 < 4; num209++)
        //                                                {
        //                                                    float num210 = (float)Utils.RandomInt(ref num190, -10, 11) * 0.15f;
        //                                                    float num211 = (float)Utils.RandomInt(ref num190, -10, 11) * 0.15f;
        //                                                    Main.spriteBatch.Draw(Main.FlameTexture[num192], new Vector2((float)(j * 16 - (int)Main.screenPosition.X) - ((float)num8 - 16f) / 2f + num210, (float)(i * 16 - (int)Main.screenPosition.y + num11) + num211) + zero, new Microsoft.Xna.Framework.Rectangle?(new Microsoft.Xna.Framework.Rectangle((int)num9, (int)num10, num8, num12)), new Microsoft.Xna.Framework.Color(75, 75, 75, 0), 0f, default(Vector2), 1f, effects, 0f);
        //                                                }
        //                                            }
        //                                            else if (num196 == 27 || num196 == 28)
        //                                            {
        //                                                Main.spriteBatch.Draw(Main.FlameTexture[num192], new Vector2((float)(j * 16 - (int)Main.screenPosition.X) - ((float)num8 - 16f) / 2f, (float)(i * 16 - (int)Main.screenPosition.y + num11)) + zero, new Microsoft.Xna.Framework.Rectangle?(new Microsoft.Xna.Framework.Rectangle((int)num9, (int)num10, num8, num12)), new Microsoft.Xna.Framework.Color(75, 75, 75, 0), 0f, default(Vector2), 1f, effects, 0f);
        //                                            }
        //                                            else
        //                                            {
        //                                                for (int num212 = 0; num212 < 7; num212++)
        //                                                {
        //                                                    float num213 = (float)Utils.RandomInt(ref num190, -10, 11) * 0.15f;
        //                                                    float num214 = (float)Utils.RandomInt(ref num190, -10, 1) * 0.35f;
        //                                                    Main.spriteBatch.Draw(Main.FlameTexture[num192], new Vector2((float)(j * 16 - (int)Main.screenPosition.X) - ((float)num8 - 16f) / 2f + num213, (float)(i * 16 - (int)Main.screenPosition.y + num11) + num214) + zero, new Microsoft.Xna.Framework.Rectangle?(new Microsoft.Xna.Framework.Rectangle((int)num9, (int)num10, num8, num12)), new Microsoft.Xna.Framework.Color(100, 100, 100, 0), 0f, default(Vector2), 1f, effects, 0f);
        //                                                }
        //                                            }
        //                                        }
        //                                        else if (num192 == 2)
        //                                        {
        //                                            int num215 = (int)(Main.Tile[j, i].frameY / 36);
        //                                            if (num215 == 3)
        //                                            {
        //                                                for (int num216 = 0; num216 < 3; num216++)
        //                                                {
        //                                                    float num217 = (float)Utils.RandomInt(ref num190, -10, 11) * 0.05f;
        //                                                    float num218 = (float)Utils.RandomInt(ref num190, -10, 11) * 0.15f;
        //                                                    Main.spriteBatch.Draw(Main.FlameTexture[num192], new Vector2((float)(j * 16 - (int)Main.screenPosition.X) - ((float)num8 - 16f) / 2f + num217, (float)(i * 16 - (int)Main.screenPosition.y + num11) + num218) + zero, new Microsoft.Xna.Framework.Rectangle?(new Microsoft.Xna.Framework.Rectangle((int)num9, (int)num10, num8, num12)), new Microsoft.Xna.Framework.Color(50, 50, 50, 0), 0f, default(Vector2), 1f, effects, 0f);
        //                                                }
        //                                            }
        //                                            else if (num215 == 6)
        //                                            {
        //                                                for (int num219 = 0; num219 < 5; num219++)
        //                                                {
        //                                                    float num220 = (float)Utils.RandomInt(ref num190, -10, 11) * 0.15f;
        //                                                    float num221 = (float)Utils.RandomInt(ref num190, -10, 11) * 0.15f;
        //                                                    Main.spriteBatch.Draw(Main.FlameTexture[num192], new Vector2((float)(j * 16 - (int)Main.screenPosition.X) - ((float)num8 - 16f) / 2f + num220, (float)(i * 16 - (int)Main.screenPosition.y + num11) + num221) + zero, new Microsoft.Xna.Framework.Rectangle?(new Microsoft.Xna.Framework.Rectangle((int)num9, (int)num10, num8, num12)), new Microsoft.Xna.Framework.Color(75, 75, 75, 0), 0f, default(Vector2), 1f, effects, 0f);
        //                                                }
        //                                            }
        //                                            else if (num215 == 9)
        //                                            {
        //                                                for (int num222 = 0; num222 < 7; num222++)
        //                                                {
        //                                                    float num223 = (float)Utils.RandomInt(ref num190, -10, 11) * 0.3f;
        //                                                    float num224 = (float)Utils.RandomInt(ref num190, -10, 11) * 0.3f;
        //                                                    Main.spriteBatch.Draw(Main.FlameTexture[num192], new Vector2((float)(j * 16 - (int)Main.screenPosition.X) - ((float)num8 - 16f) / 2f + num223, (float)(i * 16 - (int)Main.screenPosition.y + num11) + num224) + zero, new Microsoft.Xna.Framework.Rectangle?(new Microsoft.Xna.Framework.Rectangle((int)num9, (int)num10, num8, num12)), new Microsoft.Xna.Framework.Color(100, 100, 100, 0), 0f, default(Vector2), 1f, effects, 0f);
        //                                                }
        //                                            }
        //                                            else if (num215 == 11)
        //                                            {
        //                                                for (int num225 = 0; num225 < 7; num225++)
        //                                                {
        //                                                    float num226 = (float)Utils.RandomInt(ref num190, -10, 11) * 0.1f;
        //                                                    float num227 = (float)Utils.RandomInt(ref num190, -10, 1) * 0.15f;
        //                                                    Main.spriteBatch.Draw(Main.FlameTexture[num192], new Vector2((float)(j * 16 - (int)Main.screenPosition.X) - ((float)num8 - 16f) / 2f + num226, (float)(i * 16 - (int)Main.screenPosition.y + num11) + num227) + zero, new Microsoft.Xna.Framework.Rectangle?(new Microsoft.Xna.Framework.Rectangle((int)num9, (int)num10, num8, num12)), new Microsoft.Xna.Framework.Color(50, 50, 50, 0), 0f, default(Vector2), 1f, effects, 0f);
        //                                                }
        //                                            }
        //                                            else if (num215 == 13)
        //                                            {
        //                                                for (int num228 = 0; num228 < 8; num228++)
        //                                                {
        //                                                    float num229 = (float)Utils.RandomInt(ref num190, -10, 11) * 0.1f;
        //                                                    float num230 = (float)Utils.RandomInt(ref num190, -10, 11) * 0.1f;
        //                                                    Main.spriteBatch.Draw(Main.FlameTexture[num192], new Vector2((float)(j * 16 - (int)Main.screenPosition.X) - ((float)num8 - 16f) / 2f + num229, (float)(i * 16 - (int)Main.screenPosition.y + num11) + num230) + zero, new Microsoft.Xna.Framework.Rectangle?(new Microsoft.Xna.Framework.Rectangle((int)num9, (int)num10, num8, num12)), new Microsoft.Xna.Framework.Color(75, 75, 75, 0), 0f, default(Vector2), 1f, effects, 0f);
        //                                                }
        //                                            }
        //                                            else if (num215 == 28 || num215 == 29)
        //                                            {
        //                                                Main.spriteBatch.Draw(Main.FlameTexture[num192], new Vector2((float)(j * 16 - (int)Main.screenPosition.X) - ((float)num8 - 16f) / 2f, (float)(i * 16 - (int)Main.screenPosition.y + num11)) + zero, new Microsoft.Xna.Framework.Rectangle?(new Microsoft.Xna.Framework.Rectangle((int)num9, (int)num10, num8, num12)), new Microsoft.Xna.Framework.Color(75, 75, 75, 0), 0f, default(Vector2), 1f, effects, 0f);
        //                                            }
        //                                            else
        //                                            {
        //                                                for (int num231 = 0; num231 < 7; num231++)
        //                                                {
        //                                                    float num232 = (float)Utils.RandomInt(ref num190, -10, 11) * 0.15f;
        //                                                    float num233 = (float)Utils.RandomInt(ref num190, -10, 1) * 0.35f;
        //                                                    Main.spriteBatch.Draw(Main.FlameTexture[num192], new Vector2((float)(j * 16 - (int)Main.screenPosition.X) - ((float)num8 - 16f) / 2f + num232, (float)(i * 16 - (int)Main.screenPosition.y + num11) + num233) + zero, new Microsoft.Xna.Framework.Rectangle?(new Microsoft.Xna.Framework.Rectangle((int)num9, (int)num10, num8, num12)), new Microsoft.Xna.Framework.Color(100, 100, 100, 0), 0f, default(Vector2), 1f, effects, 0f);
        //                                                }
        //                                            }
        //                                        }
        //                                        else if (num192 == 3)
        //                                        {
        //                                            int num234 = (int)(Main.Tile[j, i].frameY / 54);
        //                                            if (num234 == 8)
        //                                            {
        //                                                for (int num235 = 0; num235 < 7; num235++)
        //                                                {
        //                                                    float num236 = (float)Utils.RandomInt(ref num190, -10, 11) * 0.075f;
        //                                                    float num237 = (float)Utils.RandomInt(ref num190, -10, 11) * 0.075f;
        //                                                    Main.spriteBatch.Draw(Main.FlameTexture[num192], new Vector2((float)(j * 16 - (int)Main.screenPosition.X) - ((float)num8 - 16f) / 2f + num236, (float)(i * 16 - (int)Main.screenPosition.y + num11) + num237) + zero, new Microsoft.Xna.Framework.Rectangle?(new Microsoft.Xna.Framework.Rectangle((int)num9, (int)num10, num8, num12)), new Microsoft.Xna.Framework.Color(50, 50, 50, 0), 0f, default(Vector2), 1f, effects, 0f);
        //                                                }
        //                                            }
        //                                            else if (num234 == 9)
        //                                            {
        //                                                for (int num238 = 0; num238 < 3; num238++)
        //                                                {
        //                                                    float num239 = (float)Utils.RandomInt(ref num190, -10, 11) * 0.05f;
        //                                                    float num240 = (float)Utils.RandomInt(ref num190, -10, 11) * 0.15f;
        //                                                    Main.spriteBatch.Draw(Main.FlameTexture[num192], new Vector2((float)(j * 16 - (int)Main.screenPosition.X) - ((float)num8 - 16f) / 2f + num239, (float)(i * 16 - (int)Main.screenPosition.y + num11) + num240) + zero, new Microsoft.Xna.Framework.Rectangle?(new Microsoft.Xna.Framework.Rectangle((int)num9, (int)num10, num8, num12)), new Microsoft.Xna.Framework.Color(50, 50, 50, 0), 0f, default(Vector2), 1f, effects, 0f);
        //                                                }
        //                                            }
        //                                            else if (num234 == 11)
        //                                            {
        //                                                for (int num241 = 0; num241 < 7; num241++)
        //                                                {
        //                                                    float num242 = (float)Utils.RandomInt(ref num190, -10, 11) * 0.3f;
        //                                                    float num243 = (float)Utils.RandomInt(ref num190, -10, 11) * 0.3f;
        //                                                    Main.spriteBatch.Draw(Main.FlameTexture[num192], new Vector2((float)(j * 16 - (int)Main.screenPosition.X) - ((float)num8 - 16f) / 2f + num242, (float)(i * 16 - (int)Main.screenPosition.y + num11) + num243) + zero, new Microsoft.Xna.Framework.Rectangle?(new Microsoft.Xna.Framework.Rectangle((int)num9, (int)num10, num8, num12)), new Microsoft.Xna.Framework.Color(50, 50, 50, 0), 0f, default(Vector2), 1f, effects, 0f);
        //                                                }
        //                                            }
        //                                            else if (num234 == 15)
        //                                            {
        //                                                for (int num244 = 0; num244 < 7; num244++)
        //                                                {
        //                                                    float num245 = (float)Utils.RandomInt(ref num190, -10, 11) * 0.1f;
        //                                                    float num246 = (float)Utils.RandomInt(ref num190, -10, 1) * 0.15f;
        //                                                    Main.spriteBatch.Draw(Main.FlameTexture[num192], new Vector2((float)(j * 16 - (int)Main.screenPosition.X) - ((float)num8 - 16f) / 2f + num245, (float)(i * 16 - (int)Main.screenPosition.y + num11) + num246) + zero, new Microsoft.Xna.Framework.Rectangle?(new Microsoft.Xna.Framework.Rectangle((int)num9, (int)num10, num8, num12)), new Microsoft.Xna.Framework.Color(50, 50, 50, 0), 0f, default(Vector2), 1f, effects, 0f);
        //                                                }
        //                                            }
        //                                            else if (num234 == 17 || num234 == 20)
        //                                            {
        //                                                for (int num247 = 0; num247 < 7; num247++)
        //                                                {
        //                                                    float num248 = (float)Utils.RandomInt(ref num190, -10, 11) * 0.075f;
        //                                                    float num249 = (float)Utils.RandomInt(ref num190, -10, 11) * 0.075f;
        //                                                    Main.spriteBatch.Draw(Main.FlameTexture[num192], new Vector2((float)(j * 16 - (int)Main.screenPosition.X) - ((float)num8 - 16f) / 2f + num248, (float)(i * 16 - (int)Main.screenPosition.y + num11) + num249) + zero, new Microsoft.Xna.Framework.Rectangle?(new Microsoft.Xna.Framework.Rectangle((int)num9, (int)num10, num8, num12)), new Microsoft.Xna.Framework.Color(50, 50, 50, 0), 0f, default(Vector2), 1f, effects, 0f);
        //                                                }
        //                                            }
        //                                            else if (num234 == 18)
        //                                            {
        //                                                for (int num250 = 0; num250 < 8; num250++)
        //                                                {
        //                                                    float num251 = (float)Utils.RandomInt(ref num190, -10, 11) * 0.1f;
        //                                                    float num252 = (float)Utils.RandomInt(ref num190, -10, 11) * 0.1f;
        //                                                    Main.spriteBatch.Draw(Main.FlameTexture[num192], new Vector2((float)(j * 16 - (int)Main.screenPosition.X) - ((float)num8 - 16f) / 2f + num251, (float)(i * 16 - (int)Main.screenPosition.y + num11) + num252) + zero, new Microsoft.Xna.Framework.Rectangle?(new Microsoft.Xna.Framework.Rectangle((int)num9, (int)num10, num8, num12)), new Microsoft.Xna.Framework.Color(75, 75, 75, 0), 0f, default(Vector2), 1f, effects, 0f);
        //                                                }
        //                                            }
        //                                            else if (num234 == 34 || num234 == 35)
        //                                            {
        //                                                Main.spriteBatch.Draw(Main.FlameTexture[num192], new Vector2((float)(j * 16 - (int)Main.screenPosition.X) - ((float)num8 - 16f) / 2f, (float)(i * 16 - (int)Main.screenPosition.y + num11)) + zero, new Microsoft.Xna.Framework.Rectangle?(new Microsoft.Xna.Framework.Rectangle((int)num9, (int)num10, num8, num12)), new Microsoft.Xna.Framework.Color(75, 75, 75, 0), 0f, default(Vector2), 1f, effects, 0f);
        //                                            }
        //                                            else
        //                                            {
        //                                                for (int num253 = 0; num253 < 7; num253++)
        //                                                {
        //                                                    float num254 = (float)Utils.RandomInt(ref num190, -10, 11) * 0.15f;
        //                                                    float num255 = (float)Utils.RandomInt(ref num190, -10, 1) * 0.35f;
        //                                                    Main.spriteBatch.Draw(Main.FlameTexture[num192], new Vector2((float)(j * 16 - (int)Main.screenPosition.X) - ((float)num8 - 16f) / 2f + num254, (float)(i * 16 - (int)Main.screenPosition.y + num11) + num255) + zero, new Microsoft.Xna.Framework.Rectangle?(new Microsoft.Xna.Framework.Rectangle((int)num9, (int)num10, num8, num12)), new Microsoft.Xna.Framework.Color(100, 100, 100, 0), 0f, default(Vector2), 1f, effects, 0f);
        //                                                }
        //                                            }
        //                                        }
        //                                        else if (num192 == 4)
        //                                        {
        //                                            int num256 = (int)(Main.Tile[j, i].frameY / 54);
        //                                            if (num256 == 1)
        //                                            {
        //                                                for (int num257 = 0; num257 < 3; num257++)
        //                                                {
        //                                                    float num258 = (float)Utils.RandomInt(ref num190, -10, 11) * 0.15f;
        //                                                    float num259 = (float)Utils.RandomInt(ref num190, -10, 11) * 0.15f;
        //                                                    Main.spriteBatch.Draw(Main.FlameTexture[num192], new Vector2((float)(j * 16 - (int)Main.screenPosition.X) - ((float)num8 - 16f) / 2f + num258, (float)(i * 16 - (int)Main.screenPosition.y + num11) + num259) + zero, new Microsoft.Xna.Framework.Rectangle?(new Microsoft.Xna.Framework.Rectangle((int)num9, (int)num10, num8, num12)), new Microsoft.Xna.Framework.Color(50, 50, 50, 0), 0f, default(Vector2), 1f, effects, 0f);
        //                                                }
        //                                            }
        //                                            else if (num256 == 2 || num256 == 4)
        //                                            {
        //                                                for (int num260 = 0; num260 < 7; num260++)
        //                                                {
        //                                                    float num261 = (float)Utils.RandomInt(ref num190, -10, 11) * 0.075f;
        //                                                    float num262 = (float)Utils.RandomInt(ref num190, -10, 11) * 0.075f;
        //                                                    Main.spriteBatch.Draw(Main.FlameTexture[num192], new Vector2((float)(j * 16 - (int)Main.screenPosition.X) - ((float)num8 - 16f) / 2f + num261, (float)(i * 16 - (int)Main.screenPosition.y + num11) + num262) + zero, new Microsoft.Xna.Framework.Rectangle?(new Microsoft.Xna.Framework.Rectangle((int)num9, (int)num10, num8, num12)), new Microsoft.Xna.Framework.Color(50, 50, 50, 0), 0f, default(Vector2), 1f, effects, 0f);
        //                                                }
        //                                            }
        //                                            else if (num256 == 3)
        //                                            {
        //                                                for (int num263 = 0; num263 < 7; num263++)
        //                                                {
        //                                                    float num264 = (float)Utils.RandomInt(ref num190, -10, 11) * 0.2f;
        //                                                    float num265 = (float)Utils.RandomInt(ref num190, -20, 1) * 0.35f;
        //                                                    Main.spriteBatch.Draw(Main.FlameTexture[num192], new Vector2((float)(j * 16 - (int)Main.screenPosition.X) - ((float)num8 - 16f) / 2f + num264, (float)(i * 16 - (int)Main.screenPosition.y + num11) + num265) + zero, new Microsoft.Xna.Framework.Rectangle?(new Microsoft.Xna.Framework.Rectangle((int)num9, (int)num10, num8, num12)), new Microsoft.Xna.Framework.Color(100, 100, 100, 0), 0f, default(Vector2), 1f, effects, 0f);
        //                                                }
        //                                            }
        //                                            else if (num256 == 5)
        //                                            {
        //                                                for (int num266 = 0; num266 < 7; num266++)
        //                                                {
        //                                                    float num267 = (float)Utils.RandomInt(ref num190, -10, 11) * 0.3f;
        //                                                    float num268 = (float)Utils.RandomInt(ref num190, -10, 11) * 0.3f;
        //                                                    Main.spriteBatch.Draw(Main.FlameTexture[num192], new Vector2((float)(j * 16 - (int)Main.screenPosition.X) - ((float)num8 - 16f) / 2f + num267, (float)(i * 16 - (int)Main.screenPosition.y + num11) + num268) + zero, new Microsoft.Xna.Framework.Rectangle?(new Microsoft.Xna.Framework.Rectangle((int)num9, (int)num10, num8, num12)), new Microsoft.Xna.Framework.Color(50, 50, 50, 0), 0f, default(Vector2), 1f, effects, 0f);
        //                                                }
        //                                            }
        //                                            else if (num256 == 9)
        //                                            {
        //                                                for (int num269 = 0; num269 < 7; num269++)
        //                                                {
        //                                                    float num270 = (float)Utils.RandomInt(ref num190, -10, 11) * 0.1f;
        //                                                    float num271 = (float)Utils.RandomInt(ref num190, -10, 1) * 0.15f;
        //                                                    Main.spriteBatch.Draw(Main.FlameTexture[num192], new Vector2((float)(j * 16 - (int)Main.screenPosition.X) - ((float)num8 - 16f) / 2f + num270, (float)(i * 16 - (int)Main.screenPosition.y + num11) + num271) + zero, new Microsoft.Xna.Framework.Rectangle?(new Microsoft.Xna.Framework.Rectangle((int)num9, (int)num10, num8, num12)), new Microsoft.Xna.Framework.Color(50, 50, 50, 0), 0f, default(Vector2), 1f, effects, 0f);
        //                                                }
        //                                            }
        //                                            else if (num256 == 13)
        //                                            {
        //                                                for (int num272 = 0; num272 < 8; num272++)
        //                                                {
        //                                                    float num273 = (float)Utils.RandomInt(ref num190, -10, 11) * 0.1f;
        //                                                    float num274 = (float)Utils.RandomInt(ref num190, -10, 11) * 0.1f;
        //                                                    Main.spriteBatch.Draw(Main.FlameTexture[num192], new Vector2((float)(j * 16 - (int)Main.screenPosition.X) - ((float)num8 - 16f) / 2f + num273, (float)(i * 16 - (int)Main.screenPosition.y + num11) + num274) + zero, new Microsoft.Xna.Framework.Rectangle?(new Microsoft.Xna.Framework.Rectangle((int)num9, (int)num10, num8, num12)), new Microsoft.Xna.Framework.Color(75, 75, 75, 0), 0f, default(Vector2), 1f, effects, 0f);
        //                                                }
        //                                            }
        //                                            else if (num256 == 12)
        //                                            {
        //                                                float num275 = (float)Utils.RandomInt(ref num190, -10, 11) * 0.01f;
        //                                                float num276 = (float)Utils.RandomInt(ref num190, -10, 11) * 0.01f;
        //                                                Main.spriteBatch.Draw(Main.FlameTexture[num192], new Vector2((float)(j * 16 - (int)Main.screenPosition.X) - ((float)num8 - 16f) / 2f + num275, (float)(i * 16 - (int)Main.screenPosition.y + num11) + num276) + zero, new Microsoft.Xna.Framework.Rectangle?(new Microsoft.Xna.Framework.Rectangle((int)num9, (int)num10, num8, num12)), new Microsoft.Xna.Framework.Color(Utils.RandomInt(ref num190, 90, 111), Utils.RandomInt(ref num190, 90, 111), Utils.RandomInt(ref num190, 90, 111), 0), 0f, default(Vector2), 1f, effects, 0f);
        //                                            }
        //                                            else if (num256 == 28 || num256 == 29)
        //                                            {
        //                                                Main.spriteBatch.Draw(Main.FlameTexture[num192], new Vector2((float)(j * 16 - (int)Main.screenPosition.X) - ((float)num8 - 16f) / 2f, (float)(i * 16 - (int)Main.screenPosition.y + num11)) + zero, new Microsoft.Xna.Framework.Rectangle?(new Microsoft.Xna.Framework.Rectangle((int)num9, (int)num10, num8, num12)), new Microsoft.Xna.Framework.Color(75, 75, 75, 0), 0f, default(Vector2), 1f, effects, 0f);
        //                                            }
        //                                            else
        //                                            {
        //                                                for (int num277 = 0; num277 < 7; num277++)
        //                                                {
        //                                                    float num278 = (float)Utils.RandomInt(ref num190, -10, 11) * 0.15f;
        //                                                    float num279 = (float)Utils.RandomInt(ref num190, -10, 1) * 0.35f;
        //                                                    Main.spriteBatch.Draw(Main.FlameTexture[num192], new Vector2((float)(j * 16 - (int)Main.screenPosition.X) - ((float)num8 - 16f) / 2f + num278, (float)(i * 16 - (int)Main.screenPosition.y + num11) + num279) + zero, new Microsoft.Xna.Framework.Rectangle?(new Microsoft.Xna.Framework.Rectangle((int)num9, (int)num10, num8, num12)), new Microsoft.Xna.Framework.Color(100, 100, 100, 0), 0f, default(Vector2), 1f, effects, 0f);
        //                                                }
        //                                            }
        //                                        }
        //                                        else if (num192 == 13)
        //                                        {
        //                                            int num280 = (int)(num10 / 36);
        //                                            if (num280 == 1 || num280 == 3 || num280 == 6 || num280 == 8 || num280 == 19 || num280 == 27 || num280 == 29 || num280 == 30 || num280 == 31 || num280 == 32 || num280 == 36)
        //                                            {
        //                                                for (int num281 = 0; num281 < 7; num281++)
        //                                                {
        //                                                    float num282 = (float)Utils.RandomInt(ref num190, -10, 11) * 0.15f;
        //                                                    float num283 = (float)Utils.RandomInt(ref num190, -10, 1) * 0.35f;
        //                                                    Main.spriteBatch.Draw(Main.FlameTexture[num192], new Vector2((float)(j * 16 - (int)Main.screenPosition.X) - ((float)num8 - 16f) / 2f + num282, (float)(i * 16 - (int)Main.screenPosition.y + num11) + num283) + zero, new Microsoft.Xna.Framework.Rectangle?(new Microsoft.Xna.Framework.Rectangle((int)num9, (int)num10, num8, num12)), new Microsoft.Xna.Framework.Color(100, 100, 100, 0), 0f, default(Vector2), 1f, effects, 0f);
        //                                                }
        //                                            }
        //                                            else if (num280 == 25 || num280 == 16 || num280 == 2)
        //                                            {
        //                                                for (int num284 = 0; num284 < 7; num284++)
        //                                                {
        //                                                    float num285 = (float)Utils.RandomInt(ref num190, -10, 11) * 0.15f;
        //                                                    float num286 = (float)Utils.RandomInt(ref num190, -10, 1) * 0.1f;
        //                                                    Main.spriteBatch.Draw(Main.FlameTexture[num192], new Vector2((float)(j * 16 - (int)Main.screenPosition.X) - ((float)num8 - 16f) / 2f + num285, (float)(i * 16 - (int)Main.screenPosition.y + num11) + num286) + zero, new Microsoft.Xna.Framework.Rectangle?(new Microsoft.Xna.Framework.Rectangle((int)num9, (int)num10, num8, num12)), new Microsoft.Xna.Framework.Color(50, 50, 50, 0), 0f, default(Vector2), 1f, effects, 0f);
        //                                                }
        //                                            }
        //                                            else if (num280 == 29)
        //                                            {
        //                                                for (int num287 = 0; num287 < 7; num287++)
        //                                                {
        //                                                    float num288 = (float)Utils.RandomInt(ref num190, -10, 11) * 0.15f;
        //                                                    float num289 = (float)Utils.RandomInt(ref num190, -10, 1) * 0.15f;
        //                                                    Main.spriteBatch.Draw(Main.FlameTexture[num192], new Vector2((float)(j * 16 - (int)Main.screenPosition.X) - ((float)num8 - 16f) / 2f + num288, (float)(i * 16 - (int)Main.screenPosition.y + num11) + num289) + zero, new Microsoft.Xna.Framework.Rectangle?(new Microsoft.Xna.Framework.Rectangle((int)num9, (int)num10, num8, num12)), new Microsoft.Xna.Framework.Color(25, 25, 25, 0), 0f, default(Vector2), 1f, effects, 0f);
        //                                                }
        //                                            }
        //                                            else if (num280 == 34 || num280 == 35)
        //                                            {
        //                                                Main.spriteBatch.Draw(Main.FlameTexture[num192], new Vector2((float)(j * 16 - (int)Main.screenPosition.X) - ((float)num8 - 16f) / 2f, (float)(i * 16 - (int)Main.screenPosition.y + num11)) + zero, new Microsoft.Xna.Framework.Rectangle?(new Microsoft.Xna.Framework.Rectangle((int)num9, (int)num10, num8, num12)), new Microsoft.Xna.Framework.Color(75, 75, 75, 0), 0f, default(Vector2), 1f, effects, 0f);
        //                                            }
        //                                        }
        //                                        else
        //                                        {
        //                                            for (int num290 = 0; num290 < 7; num290++)
        //                                            {
        //                                                Microsoft.Xna.Framework.Color color16 = new Microsoft.Xna.Framework.Color(100, 100, 100, 0);
        //                                                if (num10 / 22 == 14)
        //                                                {
        //                                                    color16 = new Microsoft.Xna.Framework.Color((float)Main.DiscoR / 255f, (float)Main.DiscoG / 255f, (float)Main.DiscoB / 255f, 0f);
        //                                                }
        //                                                float num291 = (float)Utils.RandomInt(ref num190, -10, 11) * 0.15f;
        //                                                float num292 = (float)Utils.RandomInt(ref num190, -10, 1) * 0.35f;
        //                                                Main.spriteBatch.Draw(Main.FlameTexture[num192], new Vector2((float)(j * 16 - (int)Main.screenPosition.X) - ((float)num8 - 16f) / 2f + num291, (float)(i * 16 - (int)Main.screenPosition.y + num11) + num292) + zero, new Microsoft.Xna.Framework.Rectangle?(new Microsoft.Xna.Framework.Rectangle((int)num9, (int)num10, num8, num12)), color16, 0f, default(Vector2), 1f, effects, 0f);
        //                                            }
        //                                        }
        //                                    }
        //                                    if (type == 144)
        //                                    {
        //                                        Main.spriteBatch.Draw(Main.timerTexture, new Vector2((float)(j * 16 - (int)Main.screenPosition.X) - ((float)num8 - 16f) / 2f, (float)(i * 16 - (int)Main.screenPosition.y + num11)) + zero, new Microsoft.Xna.Framework.Rectangle?(new Microsoft.Xna.Framework.Rectangle((int)num9, (int)num10, num8, num12)), new Microsoft.Xna.Framework.Color(200, 200, 200, 0), 0f, default(Vector2), 1f, effects, 0f);
        //                                    }
        //                                    if (type == 237)
        //                                    {
        //                                        Main.spriteBatch.Draw(Main.sunAltarTexture, new Vector2((float)(j * 16 - (int)Main.screenPosition.X) - ((float)num8 - 16f) / 2f, (float)(i * 16 - (int)Main.screenPosition.y + num11)) + zero, new Microsoft.Xna.Framework.Rectangle?(new Microsoft.Xna.Framework.Rectangle((int)num9, (int)num10, num8, num12)), new Microsoft.Xna.Framework.Color((int)(Main.mouseTextColor / 2), (int)(Main.mouseTextColor / 2), (int)(Main.mouseTextColor / 2), 0), 0f, default(Vector2), 1f, effects, 0f);
        //                                    }
        //                                }
        //                            }
        //                        }
        //                    }
        //                    if (Main.tileGlowMask[(int)Tile.type] != 0)
        //                    {
        //                        Texture2D texture = Main.glowMaskTexture[(int)Main.tileGlowMask[(int)Tile.type]];
        //                        double num293 = Main.time * 0.08;
        //                        Microsoft.Xna.Framework.Color color17 = Microsoft.Xna.Framework.Color.White;
        //                        if (Tile.type == 350)
        //                        {
        //                            color17 = new Microsoft.Xna.Framework.Color(new Vector4((float)(-(float)Math.Cos(((int)(num293 / 6.283) % 3 == 1) ? num293 : 0.0) * 0.2 + 0.2)));
        //                        }
        //                        if (Tile.type == 381)
        //                        {
        //                            color17 = color5;
        //                        }
        //                        if (Tile.type == 370)
        //                        {
        //                            color17 = color4;
        //                        }
        //                        if (Tile.type == 390)
        //                        {
        //                            color17 = color4;
        //                        }
        //                        if (Tile.type == 391)
        //                        {
        //                            color17 = new Microsoft.Xna.Framework.Color(250, 250, 250, 200);
        //                        }
        //                        if (Tile.type == 209)
        //                        {
        //                            color17 = PortalHelper.GetPortalColor(Main.myPlayer, (Tile.frameX >= 288) ? 1 : 0);
        //                        }
        //                        if (Tile.type == 429 || Tile.type == 445)
        //                        {
        //                            if (Main.canDrawColorTile(j, i))
        //                            {
        //                                texture = Main.tileAltTexture[(int)type, (int)Tile.color()];
        //                            }
        //                            else
        //                            {
        //                                texture = Main.tileTexture[(int)type];
        //                            }
        //                            num15 = 18;
        //                        }
        //                        if (Tile.slope() == 0 && !Tile.halfBrick())
        //                        {
        //                            Main.spriteBatch.Draw(texture, new Vector2((float)(j * 16 - (int)Main.screenPosition.X) - ((float)num8 - 16f) / 2f, (float)(i * 16 - (int)Main.screenPosition.y)) + zero, new Microsoft.Xna.Framework.Rectangle?(new Microsoft.Xna.Framework.Rectangle((int)num9 + num16, (int)num10 + num15, num8, num12)), color17, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
        //                        }
        //                        else if (Tile.halfBrick())
        //                        {
        //                            Main.spriteBatch.Draw(texture, new Vector2((float)(j * 16 - (int)Main.screenPosition.X) - ((float)num8 - 16f) / 2f, (float)(i * 16 - (int)Main.screenPosition.y + 10)) + zero, new Microsoft.Xna.Framework.Rectangle?(new Microsoft.Xna.Framework.Rectangle((int)num9 + num16, (int)num10 + num15 + 10, num8, 6)), color17, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
        //                        }
        //                        else
        //                        {
        //                            byte b6 = Tile.slope();
        //                            for (int num294 = 0; num294 < 8; num294++)
        //                            {
        //                                int num295 = num294 << 1;
        //                                Microsoft.Xna.Framework.Rectangle value5 = new Microsoft.Xna.Framework.Rectangle((int)num9 + num16, (int)num10 + num15 + num294 * 2, num295, 2);
        //                                int num296 = 0;
        //                                switch (b6)
        //                                {
        //                                    case 2:
        //                                        value5.X = 16 - num295;
        //                                        num296 = 16 - num295;
        //                                        break;
        //                                    case 3:
        //                                        value5.Width = 16 - num295;
        //                                        break;
        //                                    case 4:
        //                                        value5.Width = 14 - num295;
        //                                        value5.X = num295 + 2;
        //                                        num296 = num295 + 2;
        //                                        break;
        //                                }
        //                                Main.spriteBatch.Draw(texture, new Vector2((float)(j * 16 - (int)Main.screenPosition.X) - ((float)num8 - 16f) / 2f + (float)num296, (float)(i * 16 - (int)Main.screenPosition.y + num294 * 2)) + zero, new Microsoft.Xna.Framework.Rectangle?(value5), color17, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
        //                            }
        //                        }
        //                    }
        //                    if (texture2D != null)
        //                    {
        //                        int num297 = 0;
        //                        int num298 = 0;
        //                        Main.spriteBatch.Draw(texture2D, new Vector2((float)(j * 16 - (int)Main.screenPosition.X) - ((float)num8 - 16f) / 2f + (float)num297, (float)(i * 16 - (int)Main.screenPosition.y + num11 + num298)) + zero, new Microsoft.Xna.Framework.Rectangle?(empty), color2, 0f, default(Vector2), 1f, effects, 0f);
        //                    }
        //                    if (texture2D2 != null)
        //                    {
        //                        empty2 = new Microsoft.Xna.Framework.Rectangle((int)num9 + num16, (int)num10 + num15, num8, num12);
        //                        int num299 = 0;
        //                        int num300 = 0;
        //                        Main.spriteBatch.Draw(texture2D2, new Vector2((float)(j * 16 - (int)Main.screenPosition.X) - ((float)num8 - 16f) / 2f + (float)num299, (float)(i * 16 - (int)Main.screenPosition.y + num11 + num300)) + zero, new Microsoft.Xna.Framework.Rectangle?(empty2), transparent, 0f, default(Vector2), 1f, effects, 0f);
        //                        goto IL_CEBF;
        //                    }
        //                    goto IL_CEBF;
        //                    IL_1ABF:
        //                    num49 = (int)(num9 / 36);
        //                    if (num49 == 48)
        //                    {
        //                        texture2D = Main.glowMaskTexture[56];
        //                        empty = new Microsoft.Xna.Framework.Rectangle((int)(num9 % 36), (int)num10, num8, num12);
        //                        color2 = color3;
        //                    }
        //                    if (num49 == 49)
        //                    {
        //                        texture2D = Main.glowMaskTexture[117];
        //                        empty = new Microsoft.Xna.Framework.Rectangle((int)(num9 % 36), (int)num10, num8, num12);
        //                        color2 = color4;
        //                        goto IL_1EDE;
        //                    }
        //                    goto IL_1EDE;
        //                }
        //            }
        //            IL_CEBF:
        //            TileLoader.PostDraw(j, i, type, Main.spriteBatch); // TODO, is Main.instance the right spot?
        //            ;
        //        }
        //    }
        //    if (solidOnly)
        //    {
        //        MethodInfo DrawTileCracks = typeof(Main).GetMethod("DrawTileCracks", BindingFlags.Instance | BindingFlags.NonPublic);
        //        Object[] arg = {1};
        //        DrawTileCracks.Invoke(Main.instance, arg);
        //    }
        //    for (int num301 = 0; num301 < num3; num301++)
        //    {
        //        int num302 = Main.specX[num301];
        //        int num303 = Main.specY[num301];
        //        Tile tile6 = Main.Tile[num302, num303];
        //        ushort type4 = tile6.type;
        //        short frameX = tile6.frameX;
        //        short frameY = tile6.frameY;
        //        if (type4 == 237)
        //        {
        //            Main.spriteBatch.Draw(Main.sunOrbTexture, new Vector2((float)(num302 * 16 - (int)Main.screenPosition.X) + (float)num8 / 2f, (float)(num303 * 16 - (int)Main.screenPosition.y - 36)) + zero, new Microsoft.Xna.Framework.Rectangle?(new Microsoft.Xna.Framework.Rectangle(0, 0, Main.sunOrbTexture.Width, Main.sunOrbTexture.Height)), new Microsoft.Xna.Framework.Color((int)Main.mouseTextColor, (int)Main.mouseTextColor, (int)Main.mouseTextColor, 0), Main.sunCircle, new Vector2((float)(Main.sunOrbTexture.Width / 2), (float)(Main.sunOrbTexture.Height / 2)), 1f, SpriteEffects.None, 0f);
        //        }
        //        if ((type4 == 128 || type4 == 269) && frameX >= 100)
        //        {
        //            MethodInfo LoadArmorLegs = typeof(Main).GetMethod("LoadArmorLegs", BindingFlags.Instance | BindingFlags.NonPublic);
        //            MethodInfo LoadArmorHead = typeof(Main).GetMethod("LoadArmorHead", BindingFlags.Instance | BindingFlags.NonPublic);
        //            MethodInfo LoadArmorBody = typeof(Main).GetMethod("LoadArmorBody", BindingFlags.Instance | BindingFlags.NonPublic);

        //            int num304 = (int)(frameY / 18);
        //            int num305 = (int)frameX;
        //            int num306 = 0;
        //            while (num305 >= 100)
        //            {
        //                num306++;
        //                num305 -= 100;
        //            }
        //            int num307 = -4;
        //            SpriteEffects effects2 = SpriteEffects.FlipHorizontally;
        //            if (num305 >= 36)
        //            {
        //                effects2 = SpriteEffects.None;
        //                num307 = -4;
        //            }
        //            if (num304 == 0)
        //            {
        //                bool flag21 = false;
        //                int num308 = Player.SetMatch(0, num306, type4 == 128, ref flag21);
        //                if (num308 == -1)
        //                {
        //                    num308 = num306;
        //                }

        //                Object[] arg = {num308};
        //                LoadArmorHead.Invoke(Main.instance, arg);
        //                Main.spriteBatch.Draw(Main.armorHeadTexture[num308], new Vector2((float)(num302 * 16 - (int)Main.screenPosition.X + num307), (float)(num303 * 16 - (int)Main.screenPosition.y - 12)) + zero, new Microsoft.Xna.Framework.Rectangle?(new Microsoft.Xna.Framework.Rectangle(0, 0, 40, 54)), Lighting.GetColor(num302, num303), 0f, default(Vector2), 1f, effects2, 0f);
        //            }
        //            else if (num304 == 1)
        //            {
        //                bool flag22 = false;
        //                int num309 = Player.SetMatch(1, num306, type4 == 128, ref flag22);
        //                if (num309 != -1)
        //                {
        //                    Object[] arg = {num309};
        //                    LoadArmorLegs.Invoke(Main.instance ,arg);
        //                    Main.spriteBatch.Draw(Main.armorLegTexture[num309], new Vector2((float)(num302 * 16 - (int)Main.screenPosition.X + num307), (float)(num303 * 16 - (int)Main.screenPosition.y - 28)) + zero, new Microsoft.Xna.Framework.Rectangle?(new Microsoft.Xna.Framework.Rectangle(0, 0, 40, 54)), Lighting.GetColor(num302, num303), 0f, default(Vector2), 1f, effects2, 0f);
        //                }
        //                Object[] arg2 = { num306 };
        //                LoadArmorBody.Invoke(Main.instance, arg2);
        //                if (type4 == 269)
        //                {
        //                    Main.spriteBatch.Draw(Main.femaleBodyTexture[num306], new Vector2((float)(num302 * 16 - (int)Main.screenPosition.X + num307), (float)(num303 * 16 - (int)Main.screenPosition.y - 28)) + zero, new Microsoft.Xna.Framework.Rectangle?(new Microsoft.Xna.Framework.Rectangle(0, 0, 40, 54)), Lighting.GetColor(num302, num303), 0f, default(Vector2), 1f, effects2, 0f);
        //                }
        //                else
        //                {
        //                    Main.spriteBatch.Draw(Main.armorBodyTexture[num306], new Vector2((float)(num302 * 16 - (int)Main.screenPosition.X + num307), (float)(num303 * 16 - (int)Main.screenPosition.y - 28)) + zero, new Microsoft.Xna.Framework.Rectangle?(new Microsoft.Xna.Framework.Rectangle(0, 0, 40, 54)), Lighting.GetColor(num302, num303), 0f, default(Vector2), 1f, effects2, 0f);
        //                }
        //                if (num306 >= 0 && num306 < 210 && ArmorIDs.Body.Sets.NeedsToDrawArm[num306])
        //                {
        //                    Main.spriteBatch.Draw(Main.armorArmTexture[num306], new Vector2((float)(num302 * 16 - (int)Main.screenPosition.X + num307), (float)(num303 * 16 - (int)Main.screenPosition.y - 28)) + zero, new Microsoft.Xna.Framework.Rectangle?(new Microsoft.Xna.Framework.Rectangle(0, 0, 40, 54)), Lighting.GetColor(num302, num303), 0f, default(Vector2), 1f, effects2, 0f);
        //                }
        //            }
        //            else if (num304 == 2)
        //            {
        //                bool flag23 = false;
        //                int num310 = Player.SetMatch(2, num306, type4 == 128, ref flag23);
        //                if (num310 == -1)
        //                {
        //                    num310 = num306;
        //                }
        //                Object[] arg = { num310 };
        //                LoadArmorLegs.Invoke(Main.instance, arg);
        //                Main.spriteBatch.Draw(Main.armorLegTexture[num310], new Vector2((float)(num302 * 16 - (int)Main.screenPosition.X + num307), (float)(num303 * 16 - (int)Main.screenPosition.y - 44)) + zero, new Microsoft.Xna.Framework.Rectangle?(new Microsoft.Xna.Framework.Rectangle(0, 0, 40, 54)), Lighting.GetColor(num302, num303), 0f, default(Vector2), 1f, effects2, 0f);
        //            }
        //        }
        //        if (type4 == 334 && frameX >= 5000)
        //        {
        //            short arg_D510_0 = (short)(frameY / 18);
        //            int num311 = (int)frameX;
        //            int num312 = 0;
        //            int num313 = num311 % 5000;
        //            num313 -= 100;
        //            while (num311 >= 5000)
        //            {
        //                num312++;
        //                num311 -= 5000;
        //            }
        //            int num314 = (int)Main.Tile[num302 + 1, num303].frameX;
        //            if (num314 >= 25000)
        //            {
        //                num314 -= 25000;
        //            }
        //            else
        //            {
        //                num314 -= 10000;
        //            }
        //            Item item = new Item();
        //            item.netDefaults(num313);
        //            item.Prefix(num314);
        //            Texture2D texture2D3 = Main.itemTexture[item.type];
        //            Microsoft.Xna.Framework.Rectangle value6;
        //            if (Main.itemAnimations[item.type] != null)
        //            {
        //                value6 = Main.itemAnimations[item.type].GetFrame(texture2D3);
        //            }
        //            else
        //            {
        //                value6 = texture2D3.Frame(1, 1, 0, 0);
        //            }
        //            int width2 = value6.Width;
        //            int height7 = value6.Height;
        //            float num315 = 1f;
        //            if (width2 > 40 || height7 > 40)
        //            {
        //                if (width2 > height7)
        //                {
        //                    num315 = 40f / (float)width2;
        //                }
        //                else
        //                {
        //                    num315 = 40f / (float)height7;
        //                }
        //            }
        //            num315 *= item.scale;
        //            SpriteEffects effects3 = SpriteEffects.None;
        //            if (num312 >= 3)
        //            {
        //                effects3 = SpriteEffects.FlipHorizontally;
        //            }
        //            Microsoft.Xna.Framework.Color color18 = Lighting.GetColor(num302, num303);
        //            Main.spriteBatch.Draw(texture2D3, new Vector2((float)(num302 * 16 - (int)Main.screenPosition.X + 24), (float)(num303 * 16 - (int)Main.screenPosition.y + 8)) + zero, new Microsoft.Xna.Framework.Rectangle?(value6), Lighting.GetColor(num302, num303), 0f, new Vector2((float)(width2 / 2), (float)(height7 / 2)), num315, effects3, 0f);
        //            if (item.color != default(Microsoft.Xna.Framework.Color))
        //            {
        //                Main.spriteBatch.Draw(texture2D3, new Vector2((float)(num302 * 16 - (int)Main.screenPosition.X + 24), (float)(num303 * 16 - (int)Main.screenPosition.y + 8)) + zero, new Microsoft.Xna.Framework.Rectangle?(value6), item.GetColor(color18), 0f, new Vector2((float)(width2 / 2), (float)(height7 / 2)), num315, effects3, 0f);
        //            }
        //        }
        //        if (type4 == 395)
        //        {
        //            TEItemFrame tEItemFrame = (TEItemFrame)TileEntity.ByPosition[new Point16(num302, num303)];
        //            Item item2 = tEItemFrame.item;
        //            Texture2D texture2D4 = Main.itemTexture[item2.type];
        //            Microsoft.Xna.Framework.Rectangle value7;
        //            if (Main.itemAnimations[item2.type] != null)
        //            {
        //                value7 = Main.itemAnimations[item2.type].GetFrame(texture2D4);
        //            }
        //            else
        //            {
        //                value7 = texture2D4.Frame(1, 1, 0, 0);
        //            }
        //            int width3 = value7.Width;
        //            int height8 = value7.Height;
        //            float num316 = 1f;
        //            if (width3 > 20 || height8 > 20)
        //            {
        //                if (width3 > height8)
        //                {
        //                    num316 = 20f / (float)width3;
        //                }
        //                else
        //                {
        //                    num316 = 20f / (float)height8;
        //                }
        //            }
        //            num316 *= item2.scale;
        //            SpriteEffects effects4 = SpriteEffects.None;
        //            Microsoft.Xna.Framework.Color color19 = Lighting.GetColor(num302, num303);
        //            Microsoft.Xna.Framework.Color color20 = color19;
        //            float num317 = 1f;
        //            ItemSlot.GetItemLight(ref color20, ref num317, item2, false);
        //            num316 *= num317;
        //            Main.spriteBatch.Draw(texture2D4, new Vector2((float)(num302 * 16 - (int)Main.screenPosition.X + 16), (float)(num303 * 16 - (int)Main.screenPosition.y + 16)) + zero, new Microsoft.Xna.Framework.Rectangle?(value7), color20, 0f, new Vector2((float)(width3 / 2), (float)(height8 / 2)), num316, effects4, 0f);
        //            if (item2.color != default(Microsoft.Xna.Framework.Color))
        //            {
        //                Main.spriteBatch.Draw(texture2D4, new Vector2((float)(num302 * 16 - (int)Main.screenPosition.X + 16), (float)(num303 * 16 - (int)Main.screenPosition.y + 16)) + zero, new Microsoft.Xna.Framework.Rectangle?(value7), item2.GetColor(color19), 0f, new Vector2((float)(width3 / 2), (float)(height8 / 2)), num316, effects4, 0f);
        //            }
        //        }
        //        if (type4 == 412)
        //        {
        //            Texture2D texture2D5 = Main.glowMaskTexture[202];
        //            int num318 = Main.tileFrame[(int)type4] / 60;
        //            int frameY2 = (num318 + 1) % 4;
        //            float num319 = (float)(Main.tileFrame[(int)type4] % 60) / 60f;
        //            Microsoft.Xna.Framework.Color value8 = new Microsoft.Xna.Framework.Color(255, 255, 255, 255);
        //            Main.spriteBatch.Draw(texture2D5, new Vector2((float)(num302 * 16 - (int)Main.screenPosition.X), (float)(num303 * 16 - (int)Main.screenPosition.y + 2)) + zero, new Microsoft.Xna.Framework.Rectangle?(texture2D5.Frame(1, 4, 0, num318)), value8 * (1f - num319), 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
        //            Main.spriteBatch.Draw(texture2D5, new Vector2((float)(num302 * 16 - (int)Main.screenPosition.X), (float)(num303 * 16 - (int)Main.screenPosition.y + 2)) + zero, new Microsoft.Xna.Framework.Rectangle?(texture2D5.Frame(1, 4, 0, frameY2)), value8 * num319, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
        //        }
        //        try
        //        {
        //            if (type4 == 5 && frameY >= 198 && frameX >= 22)
        //            {
        //                //frame
        //                int num320 = 0;
        //                if (frameX == 22)
        //                {
        //                    if (frameY == 220)
        //                    {
        //                        num320 = 1;
        //                    }
        //                    else if (frameY == 242)
        //                    {
        //                        num320 = 2;
        //                    }
        //                    int num321 = 0;
        //                    //frame width
        //                    int num322 = 80;
        //                    //frame height
        //                    int num323 = 80;
        //                    //x offset left
        //                    int num324 = 32;
        //                    //y offset
        //                    int num325 = 0;
        //                    int num326 = num303;
        //                    Texture2D modTopTextures = null;
        //                    while (num326 < num303 + 100)
        //                    {
        //                        modTopTextures = TileLoader.GetTreeTopTextures(Main.Tile[num302, num326].type,
        //                            num302, num326, ref num320, ref num322, ref num323, ref num324, ref num325);
        //                        if (modTopTextures != null)
        //                        {
        //                            break;
        //                        }
        //                        if (Main.Tile[num302, num326].type == 2)
        //                        {
        //                            num321 = Main.GetTreeStyle(num302);
        //                            break;
        //                        }
        //                        if (Main.Tile[num302, num326].type == 23)
        //                        {
        //                            num321 = 1;
        //                            break;
        //                        }
        //                        if (Main.Tile[num302, num326].type == 70)
        //                        {
        //                            num321 = 14;
        //                            break;
        //                        }
        //                        if (Main.Tile[num302, num326].type == 60)
        //                        {
        //                            num321 = 2;
        //                            if (WorldGen.jungleBG == 1)
        //                            {
        //                                num321 = 11;
        //                            }
        //                            if ((double)num326 > Main.worldSurface)
        //                            {
        //                                num321 = 13;
        //                            }
        //                            num322 = 114;
        //                            num323 = 96;
        //                            num324 = 48;
        //                            break;
        //                        }
        //                        if (Main.Tile[num302, num326].type == 147)
        //                        {
        //                            num321 = 4;
        //                            if (WorldGen.snowBG == 0)
        //                            {
        //                                num321 = 12;
        //                                if (num302 % 10 == 0)
        //                                {
        //                                    num321 = 18;
        //                                }
        //                            }
        //                            if (WorldGen.snowBG != 2 && WorldGen.snowBG != 3 && WorldGen.snowBG != 32 && WorldGen.snowBG != 4 && WorldGen.snowBG != 42)
        //                            {
        //                                break;
        //                            }
        //                            if (WorldGen.snowBG % 2 == 0)
        //                            {
        //                                if (num302 < Main.maxTilesX / 2)
        //                                {
        //                                    num321 = 16;
        //                                    break;
        //                                }
        //                                num321 = 17;
        //                                break;
        //                            }
        //                            else
        //                            {
        //                                if (num302 > Main.maxTilesX / 2)
        //                                {
        //                                    num321 = 16;
        //                                    break;
        //                                }
        //                                num321 = 17;
        //                                break;
        //                            }
        //                        }
        //                        else
        //                        {
        //                            if (Main.Tile[num302, num326].type == 199)
        //                            {
        //                                num321 = 5;
        //                                break;
        //                            }
        //                            if (Main.Tile[num302, num326].type == 109)
        //                            {
        //                                num321 = 3;
        //                                num323 = 140;
        //                                if (num302 % 3 == 1)
        //                                {
        //                                    num320 += 3;
        //                                    break;
        //                                }
        //                                if (num302 % 3 == 2)
        //                                {
        //                                    num320 += 6;
        //                                    break;
        //                                }
        //                                break;
        //                            }
        //                            else
        //                            {
        //                                num326++;
        //                            }
        //                        }
        //                    }
        //                    if (num321 == 14)
        //                    {
        //                        float num327 = (float)Main.rand.Next(28, 42) * 0.005f;
        //                        num327 += (float)(270 - (int)Main.mouseTextColor) / 1000f;
        //                        Lighting.AddLight(num302, num303, 0.1f, 0.2f + num327 / 2f, 0.7f + num327);
        //                    }
        //                    if (modTopTextures == null && tile6.color() > 0)
        //                    {
        //                        Main.checkTreeAlt[num321, (int)tile6.color()] = true;
        //                    }
        //                    if (modTopTextures != null)
        //                    {
        //                        Main.spriteBatch.Draw(modTopTextures, new Vector2(num302 * 16 - (int)Main.screenPosition.X - num324, num303 * 16 - (int)Main.screenPosition.y - num323 + 16 + num325) + zero, new Microsoft.Xna.Framework.Rectangle(num320 * (num322 + 2), 0, num322, num323), Lighting.GetColor(num302, num303), 0f, default(Vector2), 1f, SpriteEffects.None, 0f);
        //                    }
        //                    else if (tile6.color() > 0 && Main.treeAltTextureDrawn[num321, (int)tile6.color()])
        //                    {
        //                        Main.spriteBatch.Draw(Main.treeTopAltTexture[num321, (int)tile6.color()], new Vector2((float)(num302 * 16 - (int)Main.screenPosition.X - num324), (float)(num303 * 16 - (int)Main.screenPosition.y - num323 + 16 + num325)) + zero, new Microsoft.Xna.Framework.Rectangle?(new Microsoft.Xna.Framework.Rectangle(num320 * (num322 + 2), 0, num322, num323)), Lighting.GetColor(num302, num303), 0f, default(Vector2), 1f, SpriteEffects.None, 0f);
        //                    }
        //                    else
        //                    {
        //                        Main.spriteBatch.Draw(Main.treeTopTexture[num321], new Vector2((float)(num302 * 16 - (int)Main.screenPosition.X - num324), (float)(num303 * 16 - (int)Main.screenPosition.y - num323 + 16 + num325)) + zero, new Microsoft.Xna.Framework.Rectangle?(new Microsoft.Xna.Framework.Rectangle(num320 * (num322 + 2), 0, num322, num323)), Lighting.GetColor(num302, num303), 0f, default(Vector2), 1f, SpriteEffects.None, 0f);
        //                    }
        //                }
        //                else if (frameX == 44)
        //                {
        //                    if (frameY == 220)
        //                    {
        //                        num320 = 1;
        //                    }
        //                    else if (frameY == 242)
        //                    {
        //                        num320 = 2;
        //                    }
        //                    int num328 = 0;
        //                    int num329 = num303;
        //                    Texture2D modBranchTextures = null;
        //                    while (num329 < num303 + 100)
        //                    {
        //                        modBranchTextures = TileLoader.GetTreeBranchTextures(Main.Tile[num302 + 1, num329].type,
        //                            num302, num329, 1, ref num320);
        //                        if (modBranchTextures != null)
        //                        {
        //                            break;
        //                        }
        //                        if (Main.Tile[num302 + 1, num329].type == 2)
        //                        {
        //                            num328 = Main.GetTreeStyle(num302 + 1);
        //                            break;
        //                        }
        //                        if (Main.Tile[num302 + 1, num329].type == 23)
        //                        {
        //                            num328 = 1;
        //                            break;
        //                        }
        //                        if (Main.Tile[num302 + 1, num329].type == 70)
        //                        {
        //                            num328 = 14;
        //                            break;
        //                        }
        //                        if (Main.Tile[num302 + 1, num329].type == 60)
        //                        {
        //                            num328 = 2;
        //                            if ((double)num329 > Main.worldSurface)
        //                            {
        //                                num328 = 13;
        //                                break;
        //                            }
        //                            break;
        //                        }
        //                        else if (Main.Tile[num302 + 1, num329].type == 147)
        //                        {
        //                            num328 = 4;
        //                            if (WorldGen.snowBG == 0)
        //                            {
        //                                num328 = 12;
        //                                break;
        //                            }
        //                            break;
        //                        }
        //                        else
        //                        {
        //                            if (Main.Tile[num302 + 1, num329].type == 199)
        //                            {
        //                                num328 = 5;
        //                                break;
        //                            }
        //                            if (Main.Tile[num302 + 1, num329].type == 109)
        //                            {
        //                                num328 = 3;
        //                                if (num302 % 3 == 1)
        //                                {
        //                                    num320 += 3;
        //                                    break;
        //                                }
        //                                if (num302 % 3 == 2)
        //                                {
        //                                    num320 += 6;
        //                                    break;
        //                                }
        //                                break;
        //                            }
        //                            else
        //                            {
        //                                num329++;
        //                            }
        //                        }
        //                    }
        //                    if (num328 == 14)
        //                    {
        //                        float num330 = (float)Main.rand.Next(28, 42) * 0.005f;
        //                        num330 += (float)(270 - (int)Main.mouseTextColor) / 1000f;
        //                        Lighting.AddLight(num302, num303, 0.1f, 0.2f + num330 / 2f, 0.7f + num330);
        //                    }
        //                    if (modBranchTextures == null && tile6.color() > 0)
        //                    {
        //                        Main.checkTreeAlt[num328, (int)tile6.color()] = true;
        //                    }
        //                    if (modBranchTextures != null)
        //                    {
        //                        Main.spriteBatch.Draw(modBranchTextures, new Vector2(num302 * 16 - (int)Main.screenPosition.X - 24, num303 * 16 - (int)Main.screenPosition.y - 12) + zero, new Microsoft.Xna.Framework.Rectangle(0, num320 * 42, 40, 40), Lighting.GetColor(num302, num303), 0f, default(Vector2), 1f, SpriteEffects.None, 0f);
        //                    }
        //                    else if (tile6.color() > 0 && Main.treeAltTextureDrawn[num328, (int)tile6.color()])
        //                    {
        //                        Main.spriteBatch.Draw(Main.treeBranchAltTexture[num328, (int)tile6.color()], new Vector2((float)(num302 * 16 - (int)Main.screenPosition.X - 24), (float)(num303 * 16 - (int)Main.screenPosition.y - 12)) + zero, new Microsoft.Xna.Framework.Rectangle?(new Microsoft.Xna.Framework.Rectangle(0, num320 * 42, 40, 40)), Lighting.GetColor(num302, num303), 0f, default(Vector2), 1f, SpriteEffects.None, 0f);
        //                    }
        //                    else
        //                    {
        //                        Main.spriteBatch.Draw(Main.treeBranchTexture[num328], new Vector2((float)(num302 * 16 - (int)Main.screenPosition.X - 24), (float)(num303 * 16 - (int)Main.screenPosition.y - 12)) + zero, new Microsoft.Xna.Framework.Rectangle?(new Microsoft.Xna.Framework.Rectangle(0, num320 * 42, 40, 40)), Lighting.GetColor(num302, num303), 0f, default(Vector2), 1f, SpriteEffects.None, 0f);
        //                    }
        //                }
        //                else if (frameX == 66)
        //                {
        //                    if (frameY == 220)
        //                    {
        //                        num320 = 1;
        //                    }
        //                    else if (frameY == 242)
        //                    {
        //                        num320 = 2;
        //                    }
        //                    int num331 = 0;
        //                    int num332 = num303;
        //                    Texture2D modBranchTextures = null;
        //                    while (num332 < num303 + 100)
        //                    {
        //                        modBranchTextures = TileLoader.GetTreeBranchTextures(Main.Tile[num302 - 1, num332].type,
        //                            num302, num332, -1, ref num320);
        //                        if (modBranchTextures != null)
        //                        {
        //                            break;
        //                        }
        //                        if (Main.Tile[num302 - 1, num332].type == 2)
        //                        {
        //                            num331 = Main.GetTreeStyle(num302 - 1);
        //                            break;
        //                        }
        //                        if (Main.Tile[num302 - 1, num332].type == 23)
        //                        {
        //                            num331 = 1;
        //                            break;
        //                        }
        //                        if (Main.Tile[num302 - 1, num332].type == 70)
        //                        {
        //                            num331 = 14;
        //                            break;
        //                        }
        //                        if (Main.Tile[num302 - 1, num332].type == 60)
        //                        {
        //                            num331 = 2;
        //                            if ((double)num332 > Main.worldSurface)
        //                            {
        //                                num331 = 13;
        //                                break;
        //                            }
        //                            break;
        //                        }
        //                        else if (Main.Tile[num302 - 1, num332].type == 147)
        //                        {
        //                            num331 = 4;
        //                            if (WorldGen.snowBG == 0)
        //                            {
        //                                num331 = 12;
        //                                break;
        //                            }
        //                            break;
        //                        }
        //                        else
        //                        {
        //                            if (Main.Tile[num302 - 1, num332].type == 199)
        //                            {
        //                                num331 = 5;
        //                                break;
        //                            }
        //                            if (Main.Tile[num302 - 1, num332].type == 109)
        //                            {
        //                                num331 = 3;
        //                                if (num302 % 3 == 1)
        //                                {
        //                                    num320 += 3;
        //                                    break;
        //                                }
        //                                if (num302 % 3 == 2)
        //                                {
        //                                    num320 += 6;
        //                                    break;
        //                                }
        //                                break;
        //                            }
        //                            else
        //                            {
        //                                num332++;
        //                            }
        //                        }
        //                    }
        //                    if (num331 == 14)
        //                    {
        //                        float num333 = (float)Main.rand.Next(28, 42) * 0.005f;
        //                        num333 += (float)(270 - (int)Main.mouseTextColor) / 1000f;
        //                        Lighting.AddLight(num302, num303, 0.1f, 0.2f + num333 / 2f, 0.7f + num333);
        //                    }
        //                    if (modBranchTextures == null && tile6.color() > 0)
        //                    {
        //                        Main.checkTreeAlt[num331, (int)tile6.color()] = true;
        //                    }
        //                    if (modBranchTextures != null)
        //                    {
        //                        Main.spriteBatch.Draw(modBranchTextures, new Vector2(num302 * 16 - (int)Main.screenPosition.X, num303 * 16 - (int)Main.screenPosition.y - 12) + zero, new Microsoft.Xna.Framework.Rectangle(42, num320 * 42, 40, 40), Lighting.GetColor(num302, num303), 0f, default(Vector2), 1f, SpriteEffects.None, 0f);
        //                    }
        //                    else if (tile6.color() > 0 && Main.treeAltTextureDrawn[num331, (int)tile6.color()])
        //                    {
        //                        Main.spriteBatch.Draw(Main.treeBranchAltTexture[num331, (int)tile6.color()], new Vector2((float)(num302 * 16 - (int)Main.screenPosition.X), (float)(num303 * 16 - (int)Main.screenPosition.y - 12)) + zero, new Microsoft.Xna.Framework.Rectangle?(new Microsoft.Xna.Framework.Rectangle(42, num320 * 42, 40, 40)), Lighting.GetColor(num302, num303), 0f, default(Vector2), 1f, SpriteEffects.None, 0f);
        //                    }
        //                    else
        //                    {
        //                        Main.spriteBatch.Draw(Main.treeBranchTexture[num331], new Vector2((float)(num302 * 16 - (int)Main.screenPosition.X), (float)(num303 * 16 - (int)Main.screenPosition.y - 12)) + zero, new Microsoft.Xna.Framework.Rectangle?(new Microsoft.Xna.Framework.Rectangle(42, num320 * 42, 40, 40)), Lighting.GetColor(num302, num303), 0f, default(Vector2), 1f, SpriteEffects.None, 0f);
        //                    }
        //                }
        //            }
        //            if (type4 == 323 && frameX >= 88 && frameX <= 132)
        //            {
        //                int num334 = 0;
        //                if (frameX == 110)
        //                {
        //                    num334 = 1;
        //                }
        //                else if (frameX == 132)
        //                {
        //                    num334 = 2;
        //                }
        //                int num335 = 15;
        //                int num336 = 0;
        //                int num337 = 80;
        //                int num338 = 80;
        //                int num339 = 32;
        //                int num340 = 0;
        //                Texture2D modTopTextures = null;
        //                for (int num341 = num303; num341 < num303 + 100; num341++)
        //                {
        //                    modTopTextures = TileLoader.GetPalmTreeTopTextures(Main.Tile[num302, num341].type);
        //                    if (modTopTextures != null)
        //                    {
        //                        break;
        //                    }
        //                    if (Main.Tile[num302, num341].type == 53)
        //                    {
        //                        num336 = 0;
        //                        break;
        //                    }
        //                    if (Main.Tile[num302, num341].type == 234)
        //                    {
        //                        num336 = 1;
        //                        break;
        //                    }
        //                    if (Main.Tile[num302, num341].type == 116)
        //                    {
        //                        num336 = 2;
        //                        break;
        //                    }
        //                    if (Main.Tile[num302, num341].type == 112)
        //                    {
        //                        num336 = 3;
        //                        break;
        //                    }
        //                }
        //                int frameY3 = (int)Main.Tile[num302, num303].frameY;
        //                int y3 = num336 * 82;
        //                if (modTopTextures == null && tile6.color() > 0)
        //                {
        //                    Main.checkTreeAlt[num335, (int)tile6.color()] = true;
        //                }
        //                if (modTopTextures != null)
        //                {
        //                    Main.spriteBatch.Draw(modTopTextures, new Vector2(num302 * 16 - (int)Main.screenPosition.X - num339 + frameY3, num303 * 16 - (int)Main.screenPosition.y - num338 + 16 + num340) + zero, new Microsoft.Xna.Framework.Rectangle(num334 * (num337 + 2), y3, num337, num338), Lighting.GetColor(num302, num303), 0f, default(Vector2), 1f, SpriteEffects.None, 0f);
        //                }
        //                else if (tile6.color() > 0 && Main.treeAltTextureDrawn[num335, (int)tile6.color()])
        //                {
        //                    Main.spriteBatch.Draw(Main.treeTopAltTexture[num335, (int)tile6.color()], new Vector2((float)(num302 * 16 - (int)Main.screenPosition.X - num339 + frameY3), (float)(num303 * 16 - (int)Main.screenPosition.y - num338 + 16 + num340)) + zero, new Microsoft.Xna.Framework.Rectangle?(new Microsoft.Xna.Framework.Rectangle(num334 * (num337 + 2), y3, num337, num338)), Lighting.GetColor(num302, num303), 0f, default(Vector2), 1f, SpriteEffects.None, 0f);
        //                }
        //                else
        //                {
        //                    Main.spriteBatch.Draw(Main.treeTopTexture[num335], new Vector2((float)(num302 * 16 - (int)Main.screenPosition.X - num339 + frameY3), (float)(num303 * 16 - (int)Main.screenPosition.y - num338 + 16 + num340)) + zero, new Microsoft.Xna.Framework.Rectangle?(new Microsoft.Xna.Framework.Rectangle(num334 * (num337 + 2), y3, num337, num338)), Lighting.GetColor(num302, num303), 0f, default(Vector2), 1f, SpriteEffects.None, 0f);
        //                }
        //            }
        //        }
        //        catch
        //        {
        //        }
        //        TileLoader.SpecialDraw(type4, num302, num303, Main.spriteBatch);
        //    }
        //    if (TileObject.objectPreview.Active && Main.player[Main.myPlayer].showItemIcon && Main.placementPreview && !CaptureManager.Instance.Active)
        //    {
        //        Main.instance.LoadTiles((int)TileObject.objectPreview.Type);
        //        TileObject.DrawPreview(Main.spriteBatch, TileObject.objectPreview, Main.screenPosition - zero);
        //    }
        //    if (solidOnly)
        //    {
        //        TimeLogger.DrawTime(0, stopwatch.Elapsed.TotalMilliseconds);
        //        return;
        //    }
        //    TimeLogger.DrawTime(1, stopwatch.Elapsed.TotalMilliseconds);
        //}

    }
}
