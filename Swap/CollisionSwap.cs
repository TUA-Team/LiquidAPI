using LiquidAPI.LiquidMod;
using Microsoft.Xna.Framework;
using Terraria;

namespace LiquidAPI.Swap
{
    class CollisionSwap
    {


        public static bool[] ModdedWetCollision(Vector2 Position, int Width, int Height)
        {
            bool[] moddedLiquid = new bool[256];
            for (int i = 0; i < moddedLiquid.Length; i++)
            {
                moddedLiquid[i] = false;
            }

            Vector2 vector = new Vector2(Position.X + (float)(Width / 2), Position.Y + (float)(Height / 2));
            int num = 10;
            int num2 = Height / 2;
            if (num > Width)
            {
                num = Width;
            }
            if (num2 > Height)
            {
                num2 = Height;
            }
            vector = new Vector2(vector.X - (float)(num / 2), vector.Y - (float)(num2 / 2));
            int num3 = (int)(Position.X / 16f) - 1;
            int num4 = (int)((Position.X + (float)Width) / 16f) + 2;
            int num5 = (int)(Position.Y / 16f) - 1;
            int num6 = (int)((Position.Y + (float)Height) / 16f) + 2;
            num3 = Utils.Clamp<int>(num3, 0, Main.maxTilesX - 1);
            num4 = Utils.Clamp<int>(num4, 0, Main.maxTilesX - 1);
            num5 = Utils.Clamp<int>(num5, 0, Main.maxTilesY - 1);
            num6 = Utils.Clamp<int>(num6, 0, Main.maxTilesY - 1);
            for (int x = num3; x < num4; x++)
            {
                for (int y = num5; y < num6; y++)
                {

                    if (Main.tile[x, y] != null)
                    {
                        LiquidRef liquid = LiquidCore.grid[x, y];
                        if (!liquid.NoLiquid())
                        {
                            Vector2 vector2;
                            vector2.X = (float)(x * 16);
                            vector2.Y = (float)(y * 16);
                            int num7 = 16;
                            float num8 = (float)(256 - (int)Main.tile[x, y].liquid);
                            num8 /= 32f;
                            vector2.Y += num8 * 2f;
                            num7 -= (int)(num8 * 2f);
                            if (vector.X + (float)num > vector2.X && vector.X < vector2.X + 16f && vector.Y + (float)num2 > vector2.Y && vector.Y < vector2.Y + (float)num7)
                            {

                                for (byte b = 3; b < 2 + LiquidRegistry.liquidList.Count; b++)
                                {
                                    if (LiquidCore.liquidGrid[x, y].data == b)
                                    {
                                        moddedLiquid[b] = true;
                                    }
                                }
                            }


                        }
                        else if (Main.tile[x, y].active() && Main.tile[x, y].slope() != 0 && y > 0 && Main.tile[x, y - 1] != null && Main.tile[x, y - 1].liquid > 0)
                        {
                            liquid = LiquidCore.grid[x, y - 1];
                            Vector2 vector2;
                            vector2.X = (float)(x * 16);
                            vector2.Y = (float)(y * 16);
                            int num9 = 16;
                            if (vector.X + (float)num > vector2.X && vector.X < vector2.X + 16f && vector.Y + (float)num2 > vector2.Y && vector.Y < vector2.Y + (float)num9)
                            {
                                {
                                    for (byte b = 3; b < 2 + LiquidRegistry.liquidList.Count; b++)
                                    {
                                        if (LiquidCore.liquidGrid[x, y].data == b)
                                        {
                                            moddedLiquid[b] = true;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            return moddedLiquid;
        }
    }
}
