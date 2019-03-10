using System;
using System.Diagnostics;
using System.Reflection;
using LiquidAPI.LiquidMod;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Mono.Cecil.Cil;
using MonoMod.RuntimeDetour.HookGen;
using Terraria;
using Terraria.GameContent.Liquid;
using Terraria.Graphics;
using Terraria.Utilities;

namespace LiquidAPI.Swap
{
    class InternalLiquidDrawInjection
    {
        public static void SwapMethod()
        {
	        On.Terraria.GameContent.Liquid.LiquidRenderer.HasFullWater += LiquidRendererExtension.HasFullWater;
	        On.Terraria.GameContent.Liquid.LiquidRenderer.SetWaveMaskData += LiquidRendererExtension.SetWaveMaskData;

			//Type liquidRenderer = typeof(LiquidRenderer);
            //Type customRenderer = typeof(LiquidRendererExtension);
            //ReflectionExtension.MethodSwap(typeof(Main), "DrawWater", customRenderer, "DrawWater");
            //ReflectionExtension.MethodSwap(liquidRenderer, "PrepareDraw", customRenderer, "PrepareDraw");
            //ReflectionUtils.MethodSwap(liquidRenderer, "Update", customRenderer, "Update");
            //ReflectionExtension.MethodSwap(liquidRenderer, "InternalDraw", customRenderer, "InternalDraw");

            LiquidRendererExtension.Instance.load();
        }

    }

    public class LiquidRendererExtension
    {

        public static readonly LiquidRendererExtension Instance = new LiquidRendererExtension();

        public void load()
        {
            //IL.Terraria.GameContent.Liquid.LiquidRenderer.InternalDraw += ILEdit;
            On.Terraria.GameContent.Liquid.LiquidRenderer.InternalPrepareDraw += LiquidRendererExtension.InternalPrepareDraw;
            On.Terraria.GameContent.Liquid.LiquidRenderer.InternalDraw += InternalDraw;
        }

        public void unload()
        {
            //On.Terraria.GameContent.Liquid.LiquidRenderer.InternalPrepareDraw -= LiquidRendererExtension.InternalPrepareDraw;
        }

        public void ILEdit(HookIL il)
        {
            int textureIndex = 0;
            il.At(0).GotoNext(i => i.MatchLdcI4(11), i => i.MatchStloc(out textureIndex));
            var c = il.At(0);
            c.GotoNext(i => i.MatchStobj(typeof(Color)),
                i => i.MatchLdsfld(typeof(Main).GetField("tileBatch", BindingFlags.Public | BindingFlags.Static)));
            //c.Index++;
            c.Emit(OpCodes.Ldc_I4, 15);
            c.Emit(OpCodes.Stloc, textureIndex);



            /*var processor = il.Method.Body.GetILProcessor();
            var insert1 = processor.Create(OpCodes.Ldc_I4_S, 13);
            var stack = processor.Create(OpCodes.Stloc_S, 9);
            processor.InsertAfter(initialIntruction, insert1);
            processor.InsertAfter(insert1, stack);*/
        }

        public void modifyLiquidTextureColor(int liquidIndex)
        {

        }

        public static Texture2D[] liquidTexture2D = (Texture2D[]) LiquidRenderer.Instance._liquidTextures.Clone(); 

        private struct LiquidDrawCache
        {
            public Rectangle SourceRectangle;
            public Vector2 LiquidOffset;
            public bool IsVisible;
            public float Opacity;
            public byte Type;
            public bool IsSurfaceLiquid;
            public bool HasWall;
        }

        private struct LiquidCache
        {
            public float LiquidLevel;
            public float VisibleLiquidLevel;
            public float Opacity;
            public bool IsSolid;
            public bool IsHalfBrick;
            public bool HasLiquid;
            public bool HasVisibleLiquid;
            public bool HasWall;
            public Point FrameOffset;
            public bool HasLeftEdge;
            public bool HasRightEdge;
            public bool HasTopEdge;
            public bool HasBottomEdge;
            public float LeftWall;
            public float RightWall;
            public float BottomWall;
            public float TopWall;
            public float VisibleLeftWall;
            public float VisibleRightWall;
            public float VisibleBottomWall;
            public float VisibleTopWall;
            public byte Type;
            public byte VisibleType;
        }

        private static readonly float[] DEFAULT_OPACITY = new float[]
        {
            0.6f,
            0.95f,
            0.95f,
            0.95f,
            0.95f,
            0.95f,
            0.95f,
            0.95f,
            0.95f,
            0.95f,
            0.95f,
            0.95f,
            0.95f,
            0.95f,
            0.95f,
            0.95f,
            0.95f,
            0.95f,
            0.95f,
            0.95f,
            0.95f
        };

        private static readonly int[] WATERFALL_LENGTH = new int[3]
        {
            10,
            3,
            2
        };

        private static readonly byte[] WAVE_MASK_STRENGTH = new byte[5]
        {
            (byte) 0,
            (byte) 0,
            (byte) 0,
            byte.MaxValue,
            (byte) 0
        };
        private static readonly byte[] VISCOSITY_MASK = new byte[5]
        {
            (byte) 0,
            (byte) 200,
            (byte) 240,
            (byte) 0,
            (byte) 0
        };

        private static LiquidDrawCache[] _drawCache = new LiquidDrawCache[1];
        private static LiquidCache[] _cache = new LiquidCache[1];


        protected void DrawWater(bool bg = false, int Style = 0, float Alpha = 1f)
        {
            LiquidRegistry.PreDrawValue(ref bg, ref Style, ref Alpha);

            if (!Lighting.NotRetro)
            {
                Main.instance.oldDrawWater(bg, Style, Alpha);
                return;
            }
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            Vector2 drawOffset = (Main.drawToScreen ? Vector2.Zero : new Vector2((float)Main.offScreenRange, (float)Main.offScreenRange)) - Main.screenPosition;
            Draw(Main.spriteBatch, drawOffset, Style, Alpha, false);

            if (!bg)
            {
                TimeLogger.DrawTime(4, stopwatch.Elapsed.TotalMilliseconds);
            }
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 drawOffset, int waterStyle, float alpha, bool isBackgroundDraw)
        {
            //InternalDraw(spriteBatch, drawOffset, waterStyle, alpha, isBackgroundDraw);
        }

        public void PrepareDraw(Rectangle drawArea)
        {
            //InternalPrepareDraw(drawArea);
        }

        public static bool HasFullWater(On.Terraria.GameContent.Liquid.LiquidRenderer.orig_HasFullWater orig, LiquidRenderer renderer, int x, int y)
        {
            FieldInfo _drawAreaInfo =
                typeof(LiquidRenderer).GetField("_drawArea", BindingFlags.Instance | BindingFlags.NonPublic);
            Rectangle _drawArea = (Rectangle)_drawAreaInfo.GetValue(LiquidRenderer.Instance);
            x -= _drawArea.X;
            y -= _drawArea.Y;
            int num = x * _drawArea.Height + y;
            return num < 0 || num >= _drawCache.Length || (_drawCache[num].IsVisible && !_drawCache[num].IsSurfaceLiquid);
        }

        protected static unsafe void InternalPrepareDraw(On.Terraria.GameContent.Liquid.LiquidRenderer.orig_InternalPrepareDraw orig, LiquidRenderer renderer, Rectangle drawArea)
        {

            FieldInfo _animationFrameInfo =
                typeof(LiquidRenderer).GetField("_animationFrame", BindingFlags.Instance | BindingFlags.NonPublic);

            Rectangle rectangle = new Rectangle(drawArea.X - 2, drawArea.Y - 2, drawArea.Width + 4, drawArea.Height + 4);
            

            FieldInfo _waveMaskInfo =
                typeof(LiquidRenderer).GetField("_waveMask", BindingFlags.Instance | BindingFlags.NonPublic);
            Color[] _waveMask = (Color[]) _waveMaskInfo.GetValue(LiquidRenderer.Instance);

            FieldInfo _randomInfo =
                typeof(LiquidRenderer).GetField("_random", BindingFlags.Instance | BindingFlags.NonPublic);
            UnifiedRandom _random = (UnifiedRandom) _randomInfo.GetValue(LiquidRenderer.Instance);

            FieldInfo _drawAreaInfo =
                typeof(LiquidRenderer).GetField("_drawArea", BindingFlags.Instance | BindingFlags.NonPublic);
            Rectangle _drawArea = (Rectangle)_drawAreaInfo.GetValue(LiquidRenderer.Instance);
            _drawArea = drawArea;

            if (_cache.Length < rectangle.Width * rectangle.Height + 1)
            {
                _cache = new LiquidCache[rectangle.Width * rectangle.Height + 1];
            }
            if (_drawCache.Length < drawArea.Width * drawArea.Height + 1)
            {
                _drawCache = new LiquidDrawCache[drawArea.Width * drawArea.Height + 1];
            }
            if (_waveMask.Length < drawArea.Width * drawArea.Height)
            {
                _waveMask = new Color[drawArea.Width * drawArea.Height];
            }
            fixed (LiquidCache* ptr = &_cache[1])
            {
                LiquidCache* ptr2 = ptr;
                int num = rectangle.Height * 2 + 2;
                ptr2 = ptr;
                for (int i = rectangle.X; i < rectangle.X + rectangle.Width; i++)
                {
                    for (int j = rectangle.Y; j < rectangle.Y + rectangle.Height; j++)
                    {
                        Tile tile = Main.tile[i, j];
                        if (tile == null)
                        {
                            tile = new Tile();
                        }
                        ptr2->LiquidLevel = (float)tile.liquid / 255f;
                        ptr2->IsHalfBrick = (tile.halfBrick() && ptr2[-1].HasLiquid);
                        ptr2->IsSolid = (WorldGen.SolidOrSlopedTile(tile) && !ptr2->IsHalfBrick);
                        ptr2->HasLiquid = (tile.liquid != 0);
                        ptr2->VisibleLiquidLevel = 0f;
                        ptr2->HasWall = (tile.wall != 0);
                        ptr2->Type = /*tile.liquidType();*/LiquidCore.grid[i, j].liquidsType();
                        if (ptr2->IsHalfBrick && !ptr2->HasLiquid)
                        {
                            ptr2->Type = ptr2[-1].Type;
                        }
                        ptr2++;
                    }
                }
                ptr2 = ptr;
                ptr2 += num;
                for (int k = 2; k < rectangle.Width - 2; k++)
                {
                    for (int l = 2; l < rectangle.Height - 2; l++)
                    {
                        float num2 = 0f;
                        if (ptr2->IsHalfBrick && ptr2[-1].HasLiquid)
                        {
                            num2 = 1f;
                        }
                        else if (!ptr2->HasLiquid)
                        {
                            LiquidCache liquidCache = ptr2[-rectangle.Height];
                            LiquidCache liquidCache2 = ptr2[rectangle.Height];
                            LiquidCache liquidCache3 = ptr2[-1];
                            LiquidCache liquidCache4 = ptr2[1];
                            if (liquidCache.HasLiquid && liquidCache2.HasLiquid && liquidCache.Type == liquidCache2.Type)
                            {
                                num2 = liquidCache.LiquidLevel + liquidCache2.LiquidLevel;
                                ptr2->Type = liquidCache.Type;
                            }
                            if (liquidCache3.HasLiquid && liquidCache4.HasLiquid && liquidCache3.Type == liquidCache4.Type)
                            {
                                num2 = Math.Max(num2, liquidCache3.LiquidLevel + liquidCache4.LiquidLevel);
                                ptr2->Type = liquidCache3.Type;
                            }
                            num2 *= 0.5f;
                        }
                        else
                        {
                            num2 = ptr2->LiquidLevel;
                        }
                        ptr2->VisibleLiquidLevel = num2;
                        ptr2->HasVisibleLiquid = (num2 != 0f);
                        ptr2++;
                    }
                    ptr2 += 4;
                }
                ptr2 = ptr;
                for (int m = 0; m < rectangle.Width; m++)
                {
                    for (int n = 0; n < rectangle.Height - 10; n++)
                    {
                        if (ptr2->HasVisibleLiquid && !ptr2->IsSolid)
                        {
                            ptr2->Opacity = 1f;
                            ptr2->VisibleType = ptr2->Type;
                            float num3 = 1f / (float)(WATERFALL_LENGTH[(int)ptr2->Type] + 1);
                            float num4 = 1f;
                            for (int num5 = 1; num5 <= WATERFALL_LENGTH[(int)ptr2->Type]; num5++)
                            {
                                num4 -= num3;
                                if (ptr2[num5].IsSolid)
                                {
                                    break;
                                }
                                ptr2[num5].VisibleLiquidLevel = Math.Max(ptr2[num5].VisibleLiquidLevel, ptr2->VisibleLiquidLevel * num4);
                                ptr2[num5].Opacity = num4;
                                ptr2[num5].VisibleType = ptr2->Type;
                            }
                        }
                        if (ptr2->IsSolid)
                        {
                            ptr2->VisibleLiquidLevel = 1f;
                            ptr2->HasVisibleLiquid = false;
                        }
                        else
                        {
                            ptr2->HasVisibleLiquid = (ptr2->VisibleLiquidLevel != 0f);
                        }
                        ptr2++;
                    }
                    ptr2 += 10;
                }
                ptr2 = ptr;
                ptr2 += num;
                for (int num6 = 2; num6 < rectangle.Width - 2; num6++)
                {
                    for (int num7 = 2; num7 < rectangle.Height - 2; num7++)
                    {
                        if (!ptr2->HasVisibleLiquid || ptr2->IsSolid)
                        {
                            ptr2->HasLeftEdge = false;
                            ptr2->HasTopEdge = false;
                            ptr2->HasRightEdge = false;
                            ptr2->HasBottomEdge = false;
                        }
                        else
                        {
                            LiquidCache liquidCache = ptr2[-1];
                            LiquidCache liquidCache2 = ptr2[1];
                            LiquidCache liquidCache3 = ptr2[-rectangle.Height];
                            LiquidCache liquidCache4 = ptr2[rectangle.Height];
                            float num8 = 0f;
                            float num9 = 1f;
                            float num10 = 0f;
                            float num11 = 1f;
                            float visibleLiquidLevel = ptr2->VisibleLiquidLevel;
                            if (!liquidCache.HasVisibleLiquid)
                            {
                                num10 += liquidCache2.VisibleLiquidLevel * (1f - visibleLiquidLevel);
                            }
                            if (!liquidCache2.HasVisibleLiquid && !liquidCache2.IsSolid && !liquidCache2.IsHalfBrick)
                            {
                                num11 -= liquidCache.VisibleLiquidLevel * (1f - visibleLiquidLevel);
                            }
                            if (!liquidCache3.HasVisibleLiquid && !liquidCache3.IsSolid && !liquidCache3.IsHalfBrick)
                            {
                                num8 += liquidCache4.VisibleLiquidLevel * (1f - visibleLiquidLevel);
                            }
                            if (!liquidCache4.HasVisibleLiquid && !liquidCache4.IsSolid && !liquidCache4.IsHalfBrick)
                            {
                                num9 -= liquidCache3.VisibleLiquidLevel * (1f - visibleLiquidLevel);
                            }
                            ptr2->LeftWall = num8;
                            ptr2->RightWall = num9;
                            ptr2->BottomWall = num11;
                            ptr2->TopWall = num10;
                            Point zero = Point.Zero;
                            ptr2->HasTopEdge = ((!liquidCache.HasVisibleLiquid && !liquidCache.IsSolid) || num10 != 0f);
                            ptr2->HasBottomEdge = ((!liquidCache2.HasVisibleLiquid && !liquidCache2.IsSolid) || num11 != 1f);
                            ptr2->HasLeftEdge = ((!liquidCache3.HasVisibleLiquid && !liquidCache3.IsSolid) || num8 != 0f);
                            ptr2->HasRightEdge = ((!liquidCache4.HasVisibleLiquid && !liquidCache4.IsSolid) || num9 != 1f);
                            if (!ptr2->HasLeftEdge)
                            {
                                if (ptr2->HasRightEdge)
                                {
                                    zero.X += 32;
                                }
                                else
                                {
                                    zero.X += 16;
                                }
                            }
                            if (ptr2->HasLeftEdge && ptr2->HasRightEdge)
                            {
                                zero.X = 16;
                                zero.Y += 32;
                                if (ptr2->HasTopEdge)
                                {
                                    zero.Y = 16;
                                }
                            }
                            else if (!ptr2->HasTopEdge)
                            {
                                if (!ptr2->HasLeftEdge && !ptr2->HasRightEdge)
                                {
                                    zero.Y += 48;
                                }
                                else
                                {
                                    zero.Y += 16;
                                }
                            }
                            if (zero.Y == 16 && (ptr2->HasLeftEdge ^ ptr2->HasRightEdge) && (num7 + rectangle.Y) % 2 == 0)
                            {
                                zero.Y += 16;
                            }

                            ptr2->FrameOffset =  zero;
                        }
                        ptr2++;
                    }
                    ptr2 += 4;
                }
                ptr2 = ptr;
                ptr2 += num;
                for (int num12 = 2; num12 < rectangle.Width - 2; num12++)
                {
                    for (int num13 = 2; num13 < rectangle.Height - 2; num13++)
                    {
                        if (ptr2->HasVisibleLiquid)
                        {
                            LiquidCache liquidCache = ptr2[-1];
                            LiquidCache liquidCache2 = ptr2[1];
                            LiquidCache liquidCache3 = ptr2[-rectangle.Height];
                            LiquidCache liquidCache4 = ptr2[rectangle.Height];
                            ptr2->VisibleLeftWall = ptr2->LeftWall;
                            ptr2->VisibleRightWall = ptr2->RightWall;
                            ptr2->VisibleTopWall = ptr2->TopWall;
                            ptr2->VisibleBottomWall = ptr2->BottomWall;
                            if (liquidCache.HasVisibleLiquid && liquidCache2.HasVisibleLiquid)
                            {
                                if (ptr2->HasLeftEdge)
                                {
                                    ptr2->VisibleLeftWall = (ptr2->LeftWall * 2f + liquidCache.LeftWall + liquidCache2.LeftWall) * 0.25f;
                                }
                                if (ptr2->HasRightEdge)
                                {
                                    ptr2->VisibleRightWall = (ptr2->RightWall * 2f + liquidCache.RightWall + liquidCache2.RightWall) * 0.25f;
                                }
                            }
                            if (liquidCache3.HasVisibleLiquid && liquidCache4.HasVisibleLiquid)
                            {
                                if (ptr2->HasTopEdge)
                                {
                                    ptr2->VisibleTopWall = (ptr2->TopWall * 2f + liquidCache3.TopWall + liquidCache4.TopWall) * 0.25f;
                                }
                                if (ptr2->HasBottomEdge)
                                {
                                    ptr2->VisibleBottomWall = (ptr2->BottomWall * 2f + liquidCache3.BottomWall + liquidCache4.BottomWall) * 0.25f;
                                }
                            }
                        }
                        ptr2++;
                    }
                    ptr2 += 4;
                }
                ptr2 = ptr;
                ptr2 += num;
                for (int num14 = 2; num14 < rectangle.Width - 2; num14++)
                {
                    for (int num15 = 2; num15 < rectangle.Height - 2; num15++)
                    {
                        if (ptr2->HasLiquid)
                        {
                            LiquidCache liquidCache = ptr2[-1];
                            LiquidCache liquidCache2 = ptr2[1];
                            LiquidCache liquidCache3 = ptr2[-rectangle.Height];
                            LiquidCache liquidCache4 = ptr2[rectangle.Height];
                            if (ptr2->HasTopEdge && !ptr2->HasBottomEdge && (ptr2->HasLeftEdge ^ ptr2->HasRightEdge))
                            {
                                if (ptr2->HasRightEdge)
                                {
                                    ptr2->VisibleRightWall = liquidCache2.VisibleRightWall;
                                    ptr2->VisibleTopWall = liquidCache3.VisibleTopWall;
                                }
                                else
                                {
                                    ptr2->VisibleLeftWall = liquidCache2.VisibleLeftWall;
                                    ptr2->VisibleTopWall = liquidCache4.VisibleTopWall;
                                }
                            }
                            else if (liquidCache2.FrameOffset.X == 16 && liquidCache2.FrameOffset.Y == 32)
                            {
                                if (ptr2->VisibleLeftWall > 0.5f)
                                {
                                    ptr2->VisibleLeftWall = 0f;
                                    ptr2->FrameOffset = new Point(0, 0);
                                }
                                else if (ptr2->VisibleRightWall < 0.5f)
                                {
                                    ptr2->VisibleRightWall = 1f;
                                    ptr2->FrameOffset = new Point(32, 0);
                                }
                            }
                        }
                        ptr2++;
                    }
                    ptr2 += 4;
                }
                ptr2 = ptr;
                ptr2 += num;
                for (int num16 = 2; num16 < rectangle.Width - 2; num16++)
                {
                    for (int num17 = 2; num17 < rectangle.Height - 2; num17++)
                    {
                        if (ptr2->HasLiquid)
                        {
                            LiquidCache liquidCache = ptr2[-1];
                            LiquidCache liquidCache2 = ptr2[1];
                            LiquidCache liquidCache3 = ptr2[-rectangle.Height];
                            LiquidCache liquidCache4 = ptr2[rectangle.Height];
                            if (!ptr2->HasBottomEdge && !ptr2->HasLeftEdge && !ptr2->HasTopEdge && !ptr2->HasRightEdge)
                            {
                                if (liquidCache3.HasTopEdge && liquidCache.HasLeftEdge)
                                {
                                    ptr2->FrameOffset.X = Math.Max(4, (int)(16f - liquidCache.VisibleLeftWall * 16f)) - 4;
                                    ptr2->FrameOffset.Y = 48 + Math.Max(4, (int)(16f - liquidCache3.VisibleTopWall * 16f)) - 4;
                                    ptr2->VisibleLeftWall = 0f;
                                    ptr2->VisibleTopWall = 0f;
                                    ptr2->VisibleRightWall = 1f;
                                    ptr2->VisibleBottomWall = 1f;
                                }
                                else if (liquidCache4.HasTopEdge && liquidCache.HasRightEdge)
                                {
                                    ptr2->FrameOffset.X = 32 - Math.Min(16, (int)(liquidCache.VisibleRightWall * 16f) - 4);
                                    ptr2->FrameOffset.Y = 48 + Math.Max(4, (int)(16f - liquidCache4.VisibleTopWall * 16f)) - 4;
                                    ptr2->VisibleLeftWall = 0f;
                                    ptr2->VisibleTopWall = 0f;
                                    ptr2->VisibleRightWall = 1f;
                                    ptr2->VisibleBottomWall = 1f;
                                }
                            }
                        }
                        ptr2++;
                    }
                    ptr2 += 4;
                }
                ptr2 = ptr;
                ptr2 += num;
                fixed (LiquidDrawCache* ptr3 = &_drawCache[0])
                {
                    fixed (Color* ptr4 = &_waveMask[0])
                    {
                        LiquidDrawCache* ptr5 = ptr3;
                        Color* ptr6 = ptr4;
                        for (int num18 = 2; num18 < rectangle.Width - 2; num18++)
                        {
                            for (int num19 = 2; num19 < rectangle.Height - 2; num19++)
                            {
                                if (ptr2->HasVisibleLiquid)
                                {
                                    float num20 = Math.Min(0.75f, ptr2->VisibleLeftWall);
                                    float num21 = Math.Max(0.25f, ptr2->VisibleRightWall);
                                    float num22 = Math.Min(0.75f, ptr2->VisibleTopWall);
                                    float num23 = Math.Max(0.25f, ptr2->VisibleBottomWall);
                                    if (ptr2->IsHalfBrick && num23 > 0.5f)
                                    {
                                        num23 = 0.5f;
                                    }
                                    ptr5->IsVisible = (ptr2->HasWall || !ptr2->IsHalfBrick || !ptr2->HasLiquid);
                                    ptr5->SourceRectangle = new Rectangle((int)(16f - num21 * 16f) + ptr2->FrameOffset.X, (int)(16f - num23 * 16f) + ptr2->FrameOffset.Y, (int)Math.Ceiling((double)((num21 - num20) * 16f)), (int)Math.Ceiling((double)((num23 - num22) * 16f)));
                                    ptr5->IsSurfaceLiquid = (ptr2->FrameOffset.X == 16 && ptr2->FrameOffset.Y == 0 && (double)(num19 + rectangle.Y) > Main.worldSurface - 40.0);
                                    ptr5->Opacity = ptr2->Opacity;
                                    ptr5->LiquidOffset = new Vector2((float)Math.Floor((double)(num20 * 16f)), (float)Math.Floor((double)(num22 * 16f)));
                                    ptr5->Type = ptr2->VisibleType;
                                    ptr5->HasWall = ptr2->HasWall;
                                    byte b = WAVE_MASK_STRENGTH[(int)ptr2->VisibleType];
                                    byte b2 = (byte)(b >> 1);
                                    ptr6->R = b2;
                                    ptr6->G = b2;
                                    ptr6->B = VISCOSITY_MASK[(int)ptr2->VisibleType];
                                    ptr6->A = b;
                                    LiquidCache* ptr7 = ptr2 - 1;
                                    if (num19 != 2 && !ptr7->HasVisibleLiquid && !ptr7->IsSolid && !ptr7->IsHalfBrick)
                                    {
                                        *(ptr6 - 1) = *ptr6;
                                    }
                                }
                                else
                                {
                                    ptr5->IsVisible = false;
                                    int num24 = (!ptr2->IsSolid && !ptr2->IsHalfBrick) ? 4 : 3;
                                    bool flag = (!ptr2->IsSolid && !ptr2->IsHalfBrick);
                                    byte b3 = (byte) ((flag)?0:255);
                                    byte b4 = (byte)(b3 >> 1);
                                    ptr6->R = b4;
                                    ptr6->G = b4;
                                    ptr6->B = 0;
                                    ptr6->A = b3;
                                }
                                ptr2++;
                                ptr5++;
                                ptr6++;
                            }
                            ptr2 += 4;
                        }
                    }
                }
                ptr2 = ptr;
                for (int num25 = rectangle.X; num25 < rectangle.X + rectangle.Width; num25++)
                {
                    for (int num26 = rectangle.Y; num26 < rectangle.Y + rectangle.Height; num26++)
                    {
                        if (ptr2->VisibleType == 1 && ptr2->HasVisibleLiquid && Dust.lavaBubbles < 200)
                        {
                            if (_random.Next(700) == 0)
                            {
                                Dust.NewDust(new Vector2((float)(num25 * 16), (float)(num26 * 16)), 16, 16, 35, 0f, 0f, 0, Color.White, 1f);
                            }
                            if (_random.Next(350) == 0)
                            {
                                int num27 = Dust.NewDust(new Vector2((float)(num25 * 16), (float)(num26 * 16)), 16, 8, 35, 0f, 0f, 50, Color.White, 1.5f);
                                Main.dust[num27].velocity *= 0.8f;
                                Dust expr_1205_cp_0 = Main.dust[num27];
                                expr_1205_cp_0.velocity.X = expr_1205_cp_0.velocity.X * 2f;
                                Dust expr_1223_cp_0 = Main.dust[num27];
                                expr_1223_cp_0.velocity.Y = expr_1223_cp_0.velocity.Y - (float)_random.Next(1, 7) * 0.1f;
                                if (_random.Next(10) == 0)
                                {
                                    Dust expr_125F_cp_0 = Main.dust[num27];
                                    expr_125F_cp_0.velocity.Y = expr_125F_cp_0.velocity.Y * (float)_random.Next(2, 5);
                                }
                                Main.dust[num27].noGravity = true;
                            }
                        }
                        ptr2++;
                    }
                }
            }

            EventInfo waveFilterInfo =
                typeof(LiquidRenderer).GetEvent("WaveFilters", BindingFlags.Public | BindingFlags.Instance);
            

            if (waveFilterInfo != null)
            {
                Object[] waveObject = {_waveMask, _drawArea};
                MethodInfo[] methodArray = waveFilterInfo.GetOtherMethods();
                for (int i = 0; i < methodArray.Length; i++)
                {
                    MethodInfo m = methodArray[i];
                    m.Invoke(LiquidRenderer.Instance, waveObject);
                }
            }

            _waveMaskInfo.SetValue(LiquidRenderer.Instance, _waveMask);
            _drawAreaInfo.SetValue(LiquidRenderer.Instance, _drawArea);
        }

        public static void SetWaveMaskData(On.Terraria.GameContent.Liquid.LiquidRenderer.orig_SetWaveMaskData orig, LiquidRenderer renderer, ref Texture2D texture)
        {
            FieldInfo _drawAreaInfo =
                typeof(LiquidRenderer).GetField("_drawArea", BindingFlags.Instance | BindingFlags.NonPublic);
            Rectangle _drawArea = (Rectangle)_drawAreaInfo.GetValue(LiquidRenderer.Instance);

            FieldInfo _waveMaskInfo =
                typeof(LiquidRenderer).GetField("_waveMask", BindingFlags.Instance | BindingFlags.NonPublic);
            Color[] _waveMask = (Color[])_waveMaskInfo.GetValue(LiquidRenderer.Instance);

            if (texture == null || texture.Width < _drawArea.Height || texture.Height < _drawArea.Width)
            {
                Console.WriteLine("WaveMaskData texture recreated. {0}x{1}", _drawArea.Height, _drawArea.Width);
                if (texture != null)
                {
                    try
                    {
                        texture.Dispose();
                    }
                    catch
                    {
                    }
                }
                texture = new Texture2D(Main.instance.GraphicsDevice, _drawArea.Height, _drawArea.Width, false, SurfaceFormat.Color);
            }
            texture.SetData<Color>(0, new Rectangle?(new Rectangle(0, 0, _drawArea.Height, _drawArea.Width)), _waveMask, 0, _drawArea.Width * _drawArea.Height);
            _drawAreaInfo.SetValue(LiquidRenderer.Instance, _drawArea);
            _waveMaskInfo.SetValue(LiquidRenderer.Instance, _waveMask);
        }

        public void Update(GameTime gameTime)
        {
            FieldInfo _frameStateInfo =
                typeof(LiquidRenderer).GetField("_frameState", BindingFlags.NonPublic | BindingFlags.Instance);
            float _frameState = (float) _frameStateInfo.GetValue(LiquidRenderer.Instance);

            FieldInfo _animationFrameInfo =
                typeof(LiquidRenderer).GetField("_animationFrame", BindingFlags.NonPublic | BindingFlags.Instance);
            int _animationFrame = (int)_animationFrameInfo.GetValue(LiquidRenderer.Instance);

            if (Main.gamePaused || !Main.hasFocus)
            {
                return;
            }
            float num = Main.windSpeed * 80f;
            num = MathHelper.Clamp(num, -20f, 20f);
            if (num < 0f)
            {
                num = Math.Min(-10f, num);
            }
            else
            {
                num = Math.Max(10f, num);
            }
            _frameState += num * (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (_frameState < 0f)
            {
                _frameState += 16f;
            }
            _frameState %= 16f;
            _animationFrame = (int)_frameState;
            _frameStateInfo.SetValue(LiquidRenderer.Instance, _frameState);
            _animationFrameInfo.SetValue(LiquidRenderer.Instance, _animationFrame);
        }

        protected static unsafe void InternalDraw(On.Terraria.GameContent.Liquid.LiquidRenderer.orig_InternalDraw orig, LiquidRenderer randerer,SpriteBatch spriteBatch, Vector2 drawOffset, int waterStyle, float globalAlpha, bool isBackgroundDraw)
        {
            FieldInfo _drawAreaInfo =
                typeof(LiquidRenderer).GetField("_drawArea", BindingFlags.Instance | BindingFlags.NonPublic);
            Rectangle _drawArea = (Rectangle)_drawAreaInfo.GetValue(LiquidRenderer.Instance);

            FieldInfo _animationFrameInfo =
                typeof(LiquidRenderer).GetField("_animationFrame", BindingFlags.Instance | BindingFlags.NonPublic);
            

            Rectangle drawArea = _drawArea;

            fixed (LiquidDrawCache* ptr = &_drawCache[0])
            {
                LiquidDrawCache* ptr2 = ptr;
                for (int i = drawArea.X; i < drawArea.X + drawArea.Width; i++)
                {
                    for (int j = drawArea.Y; j < drawArea.Y + drawArea.Height; j++)
                    {
                        if (ptr2->IsVisible)
                        {
                            Rectangle sourceRectangle = ptr2->SourceRectangle;
                            if (ptr2->IsSurfaceLiquid)
                            {
                                sourceRectangle.Y = 1280;
                            }
                            else
                            {
                                sourceRectangle.Y += (int)_animationFrameInfo.GetValue(LiquidRenderer.Instance) * 80;
                            }
                            Vector2 liquidOffset = ptr2->LiquidOffset;
                            float num = ptr2->Opacity * (isBackgroundDraw ? 1f : DEFAULT_OPACITY[0]);
                            int liquidTextureIndex = /*(int)ptr2->Type*/14;
                            if (liquidTextureIndex == 0)
                            {
                                liquidTextureIndex = waterStyle;
                                num *= (isBackgroundDraw ? 1f : globalAlpha);
                            }
                            else if (liquidTextureIndex == 2)
                            {
                                liquidTextureIndex = 11;
                            }

                            
                            VertexColors colors;
                            Lighting.GetColor4Slice_New(i, j, out colors, Math.Min(1f, isBackgroundDraw ? 1f : ptr2->Opacity * (globalAlpha * DEFAULT_OPACITY[0])));

                            /*Main.spriteBatch.Draw(liquidTexture2D[liquidTextureIndex],
                                new Vector2((float)(i << 4), (float)(j << 4)) + drawOffset + liquidOffset,
                                new Rectangle?(sourceRectangle), colors.BottomLeftColor, 0f, Vector2.Zero, new Vector2(1, 1), SpriteEffects.None, 1f);*/
                            //Main.tileBatch.Draw(liquidTexture2D[liquidTextureIndex], new Vector2((float)(i << 4), (float)(j << 4)) + drawOffset + liquidOffset, new Rectangle?(sourceRectangle), colors, Vector2.Zero, 1f, SpriteEffects.None);
                            Main.tileBatch.Draw(liquidTexture2D[liquidTextureIndex], new Vector2((float)(i << 4) + drawOffset.X + ptr2->LiquidOffset.X, (float)(j << 4) + drawOffset.Y + ptr2->LiquidOffset.Y), ptr2->SourceRectangle, colors, Vector2.Zero, 1f, SpriteEffects.None);
                        }
                        ptr2++;
                    }
                }
            }
            Main.tileBatch.End();
            _drawAreaInfo.SetValue(LiquidRenderer.Instance, _drawArea);
        }

        public float GetVisibleLiquid(int x, int y)
        {
            FieldInfo _drawAreaInfo =
                typeof(LiquidRenderer).GetField("_drawArea", BindingFlags.Instance | BindingFlags.NonPublic);
            Rectangle _drawArea = (Rectangle)_drawAreaInfo.GetValue(LiquidRenderer.Instance);
            x -= _drawArea.X;
            y -= _drawArea.Y;
            if (x < 0 || x >= _drawArea.Width || y < 0 || y >= _drawArea.Height)
            {
                return 0f;
            }
            int num = (x + 2) * (_drawArea.Height + 4) + y + 2;
            if (!_cache[num].HasVisibleLiquid)
            {
                return 0f;
            }
            _drawAreaInfo.SetValue(LiquidRenderer.Instance, _drawArea);
            return _cache[num].VisibleLiquidLevel;
        }

        static LiquidRendererExtension()
        {
            // Note: this type is marked as 'beforefieldinit'.
            byte[] array = new byte[5];
            array[3] = 255;
            WAVE_MASK_STRENGTH = array;
            byte[] array2 = new byte[5];
            array2[1] = 200;
            array2[2] = 240;
            VISCOSITY_MASK = array2;
        }
    }
}
