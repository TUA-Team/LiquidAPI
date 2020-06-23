using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LiquidAPI.Globals;
using LiquidAPI.ID;
using LiquidAPI.LiquidMod;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;

namespace LiquidAPI.Hooks
{
    internal static partial class LiquidHooks
    {
        public static Dictionary<int, bool> staticNPCWet = new Dictionary<int, bool>();

        private const int HoneyDustID = 152;
        private const int LavaDustID = 35;
        private static int WaterDustID => Dust.dustWater();

        public static bool WetCollision(On.Terraria.Collision.orig_WetCollision orig, Vector2 Position, int Width, int Height)
        {
            foreach (int key in new Dictionary<int, bool>(staticNPCWet).Keys)
            {
                staticNPCWet[key] = false;
            }
            Collision.honey = false;
            Vector2 vector = new Vector2(Position.X + (float)(Width / 2), Position.Y + (float)(Height / 2));
            int width = 10;
            int height = Height / 2;
            if (width > Width)
                width = Width;

            if (height > Height)
                height = Height;

            vector = new Vector2(vector.X - (float)(width / 2), vector.Y - (float)(height / 2));
            int leftPosition = (int)(Position.X / 16f) - 1;
            int rightPosition = (int)((Position.X + (float)Width) / 16f) + 2;
            int downPosition = (int)(Position.Y / 16f) - 1;
            int upPosition = (int)((Position.Y + (float)Height) / 16f) + 2;
            int num3 = Utils.Clamp(leftPosition, 0, Main.maxTilesX - 1);
            rightPosition = Utils.Clamp(rightPosition, 0, Main.maxTilesX - 1);
            downPosition = Utils.Clamp(downPosition, 0, Main.maxTilesY - 1);
            upPosition = Utils.Clamp(upPosition, 0, Main.maxTilesY - 1);
            Vector2 vector2 = default(Vector2);
            for (int posX = num3; posX < rightPosition; posX++)
            {
                for (int posY = downPosition; posY < upPosition; posY++)
                {

                    if (Main.tile[posX, posY] == null)
                        continue;
                    LiquidRef tile = LiquidWorld.grid[posX, posY];
                    LiquidRef tile2 = LiquidWorld.grid[posX, posY - 1];
                    if (tile.Amount > 0)
                    {
                        vector2.X = posX * 16;
                        vector2.Y = posY * 16;
                        int num4 = 16;
                        float num5 = 256 - tile.Amount;
                        num5 /= 32f;
                        vector2.Y += num5 * 2f;
                        num4 -= (int)(num5 * 2f);
                        if (vector.X + (float)width > vector2.X && vector.X < vector2.X + 16f && vector.Y + (float)height > vector2.Y && vector.Y < vector2.Y + (float)num4)
                        {
                            /*if (tile.TypeID == LiquidID.Honey)
                            {
                                Collision.honey = true;
                            }*/
                            staticNPCWet[tile.TypeID] = true;
                            return true;
                        }
                    }
                    else
                    {
                        if (!Main.tile[posX, posY].active() || Main.tile[posX, posY].slope() == 0 || posY <= 0 || Main.tile[posX, posY - 1] == null || tile2.Amount <= 0)
                            continue;

                        vector2.X = posX * 16;
                        vector2.Y = posY * 16;
                        int num6 = 16;
                        if (vector.X + (float)width > vector2.X && vector.X < vector2.X + 16f && vector.Y + (float)height > vector2.Y && vector.Y < vector2.Y + (float)num6)
                        {
                            /*if (tile2.TypeID == LiquidID.Honey)
                            {
                                Collision.honey = true;
                            }*/

                            staticNPCWet[tile2.TypeID] = true;

                            return true;
                        }
                    }
                }
            }

            return false;
        }

        public static bool LavaCollision(On.Terraria.Collision.orig_LavaCollision orig, Vector2 Position, int Width, int Height)
        {
            int value = (int)(Position.X / 16f) - 1;
            int value2 = (int)((Position.X + (float)Width) / 16f) + 2;
            int value3 = (int)(Position.Y / 16f) - 1;
            int value4 = (int)((Position.Y + (float)Height) / 16f) + 2;
            int num = Utils.Clamp(value, 0, Main.maxTilesX - 1);
            value2 = Utils.Clamp(value2, 0, Main.maxTilesX - 1);
            value3 = Utils.Clamp(value3, 0, Main.maxTilesY - 1);
            value4 = Utils.Clamp(value4, 0, Main.maxTilesY - 1);
            Vector2 vector = default(Vector2);
            for (int posX = num; posX < value2; posX++)
            {
                for (int posY = value3; posY < value4; posY++)
                {
                    LiquidRef liquid = LiquidWorld.grid[posX, posY];
                    if (Main.tile[posX, posY] != null && liquid.Amount > 0 && liquid.TypeID == LiquidID.Lava)
                    {
                        vector.X = posX * 16;
                        vector.Y = posY * 16;
                        int num2 = 16;
                        float num3 = 256 - liquid.Amount;
                        num3 /= 32f;
                        vector.Y += num3 * 2f;
                        num2 -= (int)(num3 * 2f);
                        if (Position.X + (float)Width > vector.X && Position.X < vector.X + 16f && Position.Y + (float)Height > vector.Y && Position.Y < vector.Y + (float)num2)
                            return true;
                    }
                }
            }

            return false;
        }

        private static int[] wetImmunityByNPCAI = new int[] { 21, 67 };
        private static int[] wetImmunityByNPCType = new int[] { NPCID.BlazingWheel, NPCID.SleepingAngler, NPCID.SandElemental, NPCID.BartenderUnconscious };

        private static bool Collision_WaterCollision(On.Terraria.NPC.orig_Collision_WaterCollision origWaterCollision, NPC self, bool lava)
        {
            bool currentlyWet;
            GlobalLiquidNPC liquidGlobalNPC = self.GetGlobalNPC<GlobalLiquidNPC>();
            if(wetImmunityByNPCType.Contains(self.type) || wetImmunityByNPCAI.Contains(self.aiStyle))
            {
                currentlyWet = false;
                self.wetCount = 0;
                lava = false;
            }
            else
            {
                currentlyWet = Collision.WetCollision(self.position, self.width, self.height);
                if (Collision.honey)
                {
                    self.honeyWet = true;
                    liquidGlobalNPC.SetLiquidWetState(LiquidID.Honey, true);
                }

                if (self.lavaWet)
                {
                    liquidGlobalNPC.SetLiquidWetState(LiquidID.Lava, true);
                }

                for(int i = 3; i < staticNPCWet.Count; i++) 
                    liquidGlobalNPC.SetLiquidWetState(i, staticNPCWet[i]);
            }

            if (currentlyWet)
            {
                RemoveOnFireDebuff(self, liquidGlobalNPC);
                if (!self.wet && self.wetCount == 0)
                {
                    self.wetCount = 10;
                    SpawnLiquidDust(self, liquidGlobalNPC);
                }

                self.wet = true;
            }
            else if (self.wet)
            {
                self.velocity.X *= 0.5f; //halve the velocity of the entity
                self.wet = false;
                if (self.wetCount == 0)
                {
                    self.wetCount = 10;
                    SpawnLiquidDust(self, liquidGlobalNPC);
                }
            }
        }

        

        private static void SpawnLiquidDust(NPC self, GlobalLiquidNPC liquidGlobalNPC)
        {
            if (!liquidGlobalNPC.LavaWet())
            {
                if (liquidGlobalNPC.HoneyWet())
                    SpawnHoneyDust(self);
                else
                {
                    int liquidID = liquidGlobalNPC.GetFirstLiquidWet();
                    if (liquidGlobalNPC.GetFirstLiquidWet() != 0)
                        SpawnLiquidDust(self, LiquidRegistry.GetLiquid(liquidID).LiquidDust);
                    else
                        SpawnWaterDust(self);
                }
                    
            }
            else
                SpawnLavaDust(self);
            
        }

        private static void SpawnLiquidDust(NPC self, LiquidDust liquid)
        {
            SpawnLiquidDust(self, liquid.dustID, liquid.amountOfDust, liquid.dustVelocityX, liquid.dustVelocityY, liquid.dustScale, liquid.dustAlpha, liquid.noGravity);
        }

        private static void SpawnLiquidDust(NPC self, int dustID, int amountOfDust, float dustVelocityX, float dustVelocityY, float dustScale, int dustAlpha = 100, bool noGravity = true)
        {
            for (int m = 0; m < amountOfDust; m++)
            {
                int dustInstanceID = Dust.NewDust(new Vector2(self.position.X - 6f, self.position.Y + (float) (self.height / 2) - 8f), self.width + 12, 24, dustID);
                Main.dust[dustInstanceID].velocity.Y -= dustVelocityX;
                Main.dust[dustInstanceID].velocity.X *= dustVelocityY;
                Main.dust[dustInstanceID].scale = dustScale;
                Main.dust[dustInstanceID].alpha = dustAlpha;
                Main.dust[dustInstanceID].noGravity = noGravity;
            }
        }

        private static readonly int[] blacklistedHoneyNPCAIForSound = new int[] {1, 39};
        private static readonly int[] blacklistedHoneyNPCTypeForSound = new int[] { NPCID.BlueSlime, NPCID.MotherSlime, NPCID.LavaSlime, NPCID.IceSlime, NPCID.Mouse };

        private static void SpawnHoneyDust(NPC self)
        {
            SpawnLiquidDust(self, HoneyDustID, 10, 1f, 2.5f, 1.3f, 100, true);
            
            if(!blacklistedHoneyNPCTypeForSound.Contains(self.type) && !blacklistedHoneyNPCAIForSound.Contains(self.aiStyle)  && !self.noGravity)
                Main.PlaySound(SoundID.Splash, (int) self.position.X, (int) self.position.Y);
        }

        private static readonly int[] blacklistedLavaNPCAIForSound = new int[] {1, 39};
        private static readonly int[] blacklistedLavaNPCTypeForSound = new int[] { NPCID.BlueSlime, NPCID.MotherSlime, NPCID.LavaSlime, NPCID.IceSlime, NPCID.Mouse };

        private static void SpawnLavaDust(NPC self)
        {
            SpawnLiquidDust(self, LavaDustID, 10, 1.5f, 2.5f, 1.3f, 100, true);
            
            if(!blacklistedLavaNPCTypeForSound.Contains(self.type) && !blacklistedLavaNPCAIForSound.Contains(self.aiStyle)  && !self.noGravity)
                Main.PlaySound(SoundID.Splash, (int) self.position.X, (int) self.position.Y);
        }

        private static readonly int[] blacklistedWaterNPCAIForSound = new int[] {1, 39, 68};
        private static readonly int[] blacklistedWaterNPCTypeForSound = new int[] { NPCID.BlueSlime, NPCID.MotherSlime, NPCID.LavaSlime, NPCID.IceSlime, NPCID.Mouse, NPCID.Frog, NPCID.Duck, NPCID.DuckWhite, NPCID.SleepingAngler, NPCID.GoldFrog, NPCID.BartenderUnconscious};

        private static void SpawnWaterDust(NPC self)
        {
            
            for (int n = 0; n < 30; n++)
            {
                int dustInstanceID = Dust.NewDust(new Vector2(self.position.X - 6f, self.position.Y + (float) (self.height / 2) - 8f), self.width + 12, 24, WaterDustID);
                Main.dust[dustInstanceID].velocity.Y -= 4f;
                Main.dust[dustInstanceID].velocity.X *= 2.5f;
                Main.dust[dustInstanceID].scale *= 0.8f;
                Main.dust[dustInstanceID].alpha = 100;
                Main.dust[dustInstanceID].noGravity = true;
            }

            if(!blacklistedWaterNPCTypeForSound.Contains(self.type) && !blacklistedWaterNPCAIForSound.Contains(self.aiStyle) && !self.noGravity)
                Main.PlaySound(SoundID.Splash, (int) self.position.X, (int) self.position.Y, 0);
        }

        private static void RemoveOnFireDebuff(NPC self, GlobalLiquidNPC liquidGlobalNPC)
        {
            if (self.onFire && !liquidGlobalNPC.LavaWet() && Main.netMode != 1)
            {
                for (int buffIndex = 0; buffIndex < 5; buffIndex++)
                {
                    if (self.buffType[buffIndex] == 24)
                        self.DelBuff(buffIndex);
                }
            }
        }
    }
}
