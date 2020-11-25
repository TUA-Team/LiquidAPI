/*using System;
using System.Collections.Generic;
using LiquidAPI.LiquidMod;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Graphics;
using Terraria.Utilities;

namespace LiquidAPI
{
	public class LiquidRenderer
	{
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

		private const int ANIMATION_FRAME_COUNT = 16;
		private const int CACHE_PADDING = 2;
		private const int CACHE_PADDING_2 = 4;
		public const float MIN_LIQUID_SIZE = 0.25f;

		private static readonly int[] WATERFALL_LENGTH = new int[]
		{
			10,
			3,
			2
		};

		private static readonly float[] DEFAULT_OPACITY = new float[]
		{
			0.6f,
			0.95f,
			0.95f
		};

		private static readonly byte[] WAVE_MASK_STRENGTH = new byte[]
		{
			0,
			0,
			0
		};
		private static readonly byte[] VISCOSITY_MASK = new byte[]
		{
			0,
			200,
			240
		};

		public Dictionary<int, Texture2D> LiquidTextures = new Dictionary<int, Texture2D>();

		//private Tile[,] _tiles = Main.tile;
		private LiquidCache[] _cache = new LiquidCache[1];
		private LiquidDrawCache[] _drawCache = new LiquidDrawCache[1];
		private int _animationFrame;
		private Rectangle _drawArea = new Rectangle(0, 0, 1, 1);
		private UnifiedRandom _random = new UnifiedRandom();
		private Color[] _waveMask = new Color[1];
		private float _frameState;

		public event Action<Color[], Rectangle> WaveFilters;

		public LiquidRenderer()
		{
			for (int i = 0; i < 12; i++)
			{
				this.LiquidTextures[i] = TextureManager.Load("Images/Misc/water_" + i);
			}
		}

		private unsafe void InternalPrepareDraw(Rectangle drawArea)
		{
			Rectangle rectangle = new Rectangle(drawArea.X - CACHE_PADDING, drawArea.Y - CACHE_PADDING, drawArea.Width + CACHE_PADDING_2, drawArea.Height + CACHE_PADDING_2);
			this._drawArea = drawArea;
			if (this._cache.Length < rectangle.Width * rectangle.Height + 1)
			{
				this._cache = new LiquidCache[rectangle.Width * rectangle.Height + 1];
			}

			if (this._drawCache.Length < drawArea.Width * drawArea.Height + 1)
			{
				this._drawCache = new LiquidDrawCache[drawArea.Width * drawArea.Height + 1];
			}

			if (this._waveMask.Length < drawArea.Width * drawArea.Height)
			{
				this._waveMask = new Color[drawArea.Width * drawArea.Height];
			}

			fixed (LiquidCache* ptr = &this._cache[1])
			{
				LiquidCache* ptr2 = ptr;
				int num = rectangle.Height * 2 + CACHE_PADDING;
				for (int i = rectangle.X; i < rectangle.X + rectangle.Width; i++)
				{
					for (int j = rectangle.Y; j < rectangle.Y + rectangle.Height; j++)
					{
						LiquidRef liquid = LiquidWorld.grid[i, j];

						ptr2->LiquidLevel = liquid.Amount / 255f;
						ptr2->IsHalfBrick = liquid.Tile.halfBrick() && ptr2[-1].HasLiquid;
						ptr2->IsSolid = WorldGen.SolidOrSlopedTile(liquid.Tile) && !ptr2->IsHalfBrick;
						ptr2->HasLiquid = liquid.HasLiquid;
						ptr2->VisibleLiquidLevel = 0f;
						ptr2->HasWall = liquid.Tile.wall != 0;
						ptr2->Type = liquid.TypeID;
						if (ptr2->IsHalfBrick && !ptr2->HasLiquid)
						{
							ptr2->Type = ptr2[-1].Type;
						}

						ptr2++;
					}
				}

				ptr2 = ptr;
				ptr2 += num;
				for (int k = CACHE_PADDING; k < rectangle.Width - CACHE_PADDING; k++)
				{
					for (int l = CACHE_PADDING; l < rectangle.Height - CACHE_PADDING; l++)
					{
						float num2 = 0f;
						if (ptr2->IsHalfBrick && ptr2[-1].HasLiquid)
						{
							num2 = 1f;
						}
						else if (!ptr2->HasLiquid)
						{
							LiquidCache cacheL = ptr2[-rectangle.Height];
							LiquidCache cacheR = ptr2[rectangle.Height];
							LiquidCache cacheU = ptr2[-1];
							LiquidCache cacheD = ptr2[1];
							if (cacheL.HasLiquid && cacheR.HasLiquid && cacheL.Type == cacheR.Type)
							{
								num2 = cacheL.LiquidLevel + cacheR.LiquidLevel;
								ptr2->Type = cacheL.Type;
							}

							if (cacheU.HasLiquid && cacheD.HasLiquid && cacheU.Type == cacheD.Type)
							{
								num2 = Math.Max(num2, cacheU.LiquidLevel + cacheD.LiquidLevel);
								ptr2->Type = cacheU.Type;
							}
							num2 *= 0.5f;
						}
						else
						{
							num2 = ptr2->LiquidLevel;
						}

						ptr2->VisibleLiquidLevel = num2;
						ptr2->HasVisibleLiquid = num2 != 0f;
						ptr2++;
					}

					ptr2 += CACHE_PADDING_2;
				}

				ptr2 = ptr;
				for (int m = 0; m < rectangle.Width; m++)
				{
					for (int n = 0; n < rectangle.Height - 10; n++)
					{
						if (ptr2->HasVisibleLiquid && !ptr2->IsSolid)
						{
							ModLiquid liquid=LiquidRegistry.liquidList[ptr2->Type];
							ptr2->Opacity = 1f;
							ptr2->VisibleType = ptr2->Type;
							byte waterfallLength=liquid.waterfallLength;
							float num3 = 1f / waterfallLength + 1;
							float num4 = 1f;
							for (int num5 = 1; num5 <= waterfallLength; num5++)
							{
								num4 -= num3;
								if (ptr2[num5].IsSolid)
								{
									break;
								}

								ptr2[num5].VisibleLiquidLevel = Math.Max(ptr2[num5].VisibleLiquidLevel,ptr2->VisibleLiquidLevel * num4);
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
				for (int num6 = CACHE_PADDING; num6 < rectangle.Width - CACHE_PADDING; num6++)
				{
					for (int num7 = CACHE_PADDING; num7 < rectangle.Height - CACHE_PADDING; num7++)
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
							LiquidCache cacheU = ptr2[-1];
							LiquidCache cacheD = ptr2[1];
							LiquidCache cacheL = ptr2[-rectangle.Height];
							LiquidCache cacheR = ptr2[rectangle.Height];
							float leftWall = 0f;
							float rightWall = 1f;
							float topWall = 0f;
							float bottomWall = 1f;
							float visibleLiquidLevel = ptr2->VisibleLiquidLevel;
							if (!cacheU.HasVisibleLiquid)
							{
								topWall += cacheD.VisibleLiquidLevel * (1f - visibleLiquidLevel);
							}

							if (!cacheD.HasVisibleLiquid && !cacheD.IsSolid && !cacheD.IsHalfBrick)
							{
								bottomWall -= cacheU.VisibleLiquidLevel * (1f - visibleLiquidLevel);
							}

							if (!cacheL.HasVisibleLiquid && !cacheL.IsSolid && !cacheL.IsHalfBrick)
							{
								leftWall += cacheR.VisibleLiquidLevel * (1f - visibleLiquidLevel);
							}

							if (!cacheR.HasVisibleLiquid && !cacheR.IsSolid && !cacheR.IsHalfBrick)
							{
								rightWall -= cacheL.VisibleLiquidLevel * (1f - visibleLiquidLevel);
							}

							ptr2->LeftWall = leftWall;
							ptr2->RightWall = rightWall;
							ptr2->BottomWall = bottomWall;
							ptr2->TopWall = topWall;
							ptr2->HasTopEdge = (!cacheU.HasVisibleLiquid && !cacheU.IsSolid) || topWall != 0f;
							ptr2->HasBottomEdge = (!cacheD.HasVisibleLiquid && !cacheD.IsSolid) || bottomWall != 1f;
							ptr2->HasLeftEdge = (!cacheL.HasVisibleLiquid && !cacheL.IsSolid) || leftWall != 0f;
							ptr2->HasRightEdge = (!cacheR.HasVisibleLiquid && !cacheR.IsSolid) || rightWall != 1f;
							Point zero = Point.Zero;
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

							ptr2->FrameOffset = zero;
						}

						ptr2++;
					}

					ptr2 += CACHE_PADDING;
				}

				ptr2 = ptr;
				ptr2 += num;
				for (int num12 = CACHE_PADDING; num12 < rectangle.Width - CACHE_PADDING; num12++)
				{
					for (int num13 = CACHE_PADDING; num13 < rectangle.Height - CACHE_PADDING; num13++)
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

					ptr2 += CACHE_PADDING;
				}

				ptr2 = ptr;
				ptr2 += num;
				for (int num14 = CACHE_PADDING; num14 < rectangle.Width - CACHE_PADDING; num14++)
				{
					for (int num15 = CACHE_PADDING; num15 < rectangle.Height - CACHE_PADDING; num15++)
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

					ptr2 += CACHE_PADDING;
				}

				ptr2 = ptr;
				ptr2 += num;
				for (int num16 = CACHE_PADDING; num16 < rectangle.Width - CACHE_PADDING; num16++)
				{
					for (int num17 = CACHE_PADDING; num17 < rectangle.Height - CACHE_PADDING; num17++)
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
									ptr2->FrameOffset.X = Math.Max(4, (int) (16 - liquidCache.VisibleLeftWall * 16)) - 4;
									ptr2->FrameOffset.Y = 48 + Math.Max(4, (int) (16 - liquidCache3.VisibleTopWall * 16)) - 4;
									ptr2->VisibleLeftWall = 0f;
									ptr2->VisibleTopWall = 0f;
									ptr2->VisibleRightWall = 1f;
									ptr2->VisibleBottomWall = 1f;
								}
								else if (liquidCache4.HasTopEdge && liquidCache.HasRightEdge)
								{
									ptr2->FrameOffset.X = 32 - Math.Min(16, (int) (liquidCache.VisibleRightWall * 16) - 4);
									ptr2->FrameOffset.Y = 48 + Math.Max(4, (int) (16 - liquidCache4.VisibleTopWall * 16)) - 4;
									ptr2->VisibleLeftWall = 0f;
									ptr2->VisibleTopWall = 0f;
									ptr2->VisibleRightWall = 1f;
									ptr2->VisibleBottomWall = 1f;
								}
							}
						}

						ptr2++;
					}

					ptr2 += CACHE_PADDING;
				}

				ptr2 = ptr;
				ptr2 += num;
				fixed (LiquidDrawCache* ptr3 = &this._drawCache[0])
				{
					fixed (Color* ptr4 = &this._waveMask[0])
					{
						LiquidDrawCache* ptr5 = ptr3;
						Color* ptr6 = ptr4;
						for (int num18 = CACHE_PADDING; num18 < rectangle.Width - CACHE_PADDING; num18++)
						{
							for (int num19 = CACHE_PADDING; num19 < rectangle.Height - CACHE_PADDING; num19++)
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
									ptr5->SourceRectangle = new Rectangle
									(
										(int) (16f - num21 * 16f) + ptr2->FrameOffset.X,
										(int) (16f - num23 * 16f) + ptr2->FrameOffset.Y,
										(int) Math.Ceiling((num21 - num20) * 16f),
										(int) Math.Ceiling((num23 - num22) * 16f)
									);
									ptr5->IsSurfaceLiquid = (ptr2->FrameOffset.X == 16 && ptr2->FrameOffset.Y == 0 && (num19 + rectangle.Y) > Main.worldSurface - 40.0);
									ptr5->Opacity = ptr2->Opacity;
									ptr5->LiquidOffset = new Vector2((float) Math.Floor(num20 * 16),(float) Math.Floor(num22 * 16));
									ptr5->Type = ptr2->VisibleType;
									ptr5->HasWall = ptr2->HasWall;
									byte b = LiquidRegistry.liquidList[ptr2->VisibleType].waveMaskStrength;
									byte b2 = (byte) (b >> 1);
									ptr6->R = b2;
									ptr6->G = b2;
									ptr6->B = LiquidRegistry.liquidList[ptr2->VisibleType].viscosityMask;
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
									bool flag = !ptr2->IsSolid && !ptr2->IsHalfBrick;
									byte b3 = flag?(byte)0:(byte)255;
									byte b4 = (byte) (b3 >> 1);
									ptr6->R = b4;
									ptr6->G = b4;
									ptr6->B = 0;
									ptr6->A = b3;
								}

								ptr2++;
								ptr5++;
								ptr6++;
							}

							ptr2 += CACHE_PADDING;
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
							if (this._random.Next(700) == 0)
							{
								Dust.NewDust(new Vector2(num25 * 16,num26 * 16), 16, 16, 35, 0f,0f, 0, Color.White, 1f);
							}

							if (this._random.Next(350) == 0)
							{
								Dust dust=Main.dust[Dust.NewDust(new Vector2(num25 * 16,num26 * 16), 16, 8, 35, 0f, 0f, 50, Color.White, 1.5f)];
								dust.velocity *= 0.8f;
								dust.velocity.X *= 2f;
								dust.velocity.Y -= this._random.Next(1, 7) * 0.1f;
								if (this._random.Next(10) == 0)
								{
									dust.velocity.Y *= this._random.Next(2, 5);
								}

								dust.noGravity = true;
							}
						}

						ptr2++;
					}
				}
			}

			this.WaveFilters?.Invoke(this._waveMask,this.GetCachedDrawArea());
		}

		private unsafe void InternalDraw(SpriteBatch spriteBatch, Vector2 drawOffset, int waterStyle, float globalAlpha, bool isBackgroundDraw)
		{
			Rectangle drawArea = this._drawArea;
			Main.tileBatch.Begin();
			fixed (LiquidDrawCache* ptr = &this._drawCache[0])
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
								sourceRectangle.Y += this._animationFrame * 80;
							}

							Vector2 liquidOffset = ptr2->LiquidOffset;
							float opacity = ptr2->Opacity * (isBackgroundDraw ? 1f : LiquidRegistry.liquidList[ptr2->Type].defaultOpacity);
							int type = ptr2->Type;
							if (type == 0)
							{
								type = waterStyle;
								opacity *= (isBackgroundDraw ? 1f : globalAlpha);
							}
							else if (type >= 2)
							{
								type += 9;
							}

							opacity = Math.Min(1f, opacity);
							Lighting.GetColor4Slice_New(i, j, out VertexColors colors, 1f);
							colors.BottomLeftColor *= opacity;
							colors.BottomRightColor *= opacity;
							colors.TopLeftColor *= opacity;
							colors.TopRightColor *= opacity;
							Main.tileBatch.Draw(this.LiquidTextures[type],new Vector2(i << 4, j << 4) + drawOffset + liquidOffset,sourceRectangle, colors, Vector2.Zero, 1f, SpriteEffects.None);
						}

						ptr2++;
					}
				}
			}

			Main.tileBatch.End();
		}

		public bool HasFullWater(int x, int y)
		{
			x -= this._drawArea.X;
			y -= this._drawArea.Y;
			int num = x * this._drawArea.Height + y;
			return num < 0 || num >= this._drawCache.Length || (this._drawCache[num].IsVisible && !this._drawCache[num].IsSurfaceLiquid);
		}

		public float GetVisibleLiquid(int x, int y)
		{
			x -= this._drawArea.X;
			y -= this._drawArea.Y;
			if (x < 0 || x >= this._drawArea.Width || y < 0 || y >= this._drawArea.Height)
			{
				return 0f;
			}

			int num = (x + 2) * (this._drawArea.Height + 4) + y + 2;
			if (!this._cache[num].HasVisibleLiquid)
			{
				return 0f;
			}

			return this._cache[num].VisibleLiquidLevel;
		}

		public void Update(GameTime gameTime)
		{
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

			this._frameState += num * (float) gameTime.ElapsedGameTime.TotalSeconds;
			if (this._frameState < 0f)
			{
				this._frameState += 16f;
			}

			this._frameState %= 16f;
			this._animationFrame = (int) this._frameState;
		}

		public void PrepareDraw(Rectangle drawArea)
		{
			this.InternalPrepareDraw(drawArea);
		}

		public void SetWaveMaskData(ref Texture2D texture)
		{
			if (texture == null || texture.Width < this._drawArea.Height || texture.Height < this._drawArea.Width)
			{
				Console.WriteLine($"WaveMaskData texture recreated. {this._drawArea.Height}x{this._drawArea.Width}");
				try{texture?.Dispose();}catch{}
				texture = new Texture2D(Main.instance.GraphicsDevice, this._drawArea.Height, this._drawArea.Width, false, SurfaceFormat.Color);
			}

			texture.SetData(0, new Rectangle(0, 0, this._drawArea.Height, this._drawArea.Width), this._waveMask, 0, this._drawArea.Width * this._drawArea.Height);
		}

		public Rectangle GetCachedDrawArea()
		{
			return this._drawArea;
		}

		public void Draw(SpriteBatch spriteBatch, Vector2 drawOffset, int waterStyle, float alpha, bool isBackgroundDraw)
		{
			this.InternalDraw(spriteBatch, drawOffset, waterStyle, alpha, isBackgroundDraw);
		}

		static LiquidRenderer()
		{
			#if SERVER //client initialization has been moved to LoadContent to prevent FNA deadlocks
			Instance = new LiquidRenderer();
			#endif
		}
	}
}*/