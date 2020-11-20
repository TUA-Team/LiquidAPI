using LiquidAPI.Data;
using LiquidAPI.Vanilla;
using System.Collections.Generic;
using System.IO;
using Terraria;
using Terraria.GameContent.Generation;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using Terraria.Utilities;
using Terraria.World.Generation;

namespace LiquidAPI.LiquidMod
{
    //The following code is a modified version of DataCore from Project_Logic 0.5.0.1 provided by Rartrin
    public class LiquidWorld : ModWorld
    {
        private const string EXTENSION = "twliquid";//Should work without the leading period
        private const byte MODE = 0; //Extra data
        private const byte FORM = 3; //Saving format

        public static LiquidWorld grid;

        public static Bit[,] liquidGrid; // MAKE SURE YOU DEREFERENCE THIS ON UNLOAD

        static LiquidWorld()
        {
            LiquidAPI.OnUnload += () =>
              {
                  grid = null;
                  liquidGrid = null;
              };
        }

        public override void Initialize()
        {
            grid = this;
            liquidGrid = new Bit[Main.maxTilesX, Main.maxTilesY];
        }

        public override TagCompound Save()
        {
            try
            {
                string path = Path.ChangeExtension(Main.ActiveWorldFileData.Path, EXTENSION); //Change current world path to the custom save one
                if (FileUtilities.Exists(path, false))
                { FileUtilities.Copy(path, path + ".bak", false, true); } //also make a backup
                Queue<byte> data = new Queue<byte>();
                data.Enqueue(MODE);
                data.Enqueue(FORM);//Point Storage
                for (ushort y = 0; y < Main.maxTilesY; y++)
                {
                    for (ushort x = 0; x < Main.maxTilesX; x++)
                    {
                        if (liquidGrid[x, y] != 0)
                        {
                            data.Enqueue((byte)(x >> 8));
                            data.Enqueue((byte)x);
                            data.Enqueue((byte)(y >> 8));
                            data.Enqueue((byte)y);
                            data.Enqueue(liquidGrid[x, y]);
                        }
                    }
                }
                FileUtilities.WriteAllBytes(path, data.ToArray(), false);
                return new TagCompound();
            }
            catch { return null; }
        }

        public override void Load(TagCompound tag)
        {
            try
            {
                string path = Path.ChangeExtension(Main.ActiveWorldFileData.Path, EXTENSION);
                if (!FileUtilities.Exists(path, false))
                { return; }
                Queue<byte> data = new Queue<byte>(FileUtilities.ReadAllBytes(path, false));
                byte mode = data.Dequeue();
                byte form = data.Dequeue();
                if (form == 3)//Point Storage
                {
                    while (data.Count > 0)
                    {
                        liquidGrid[(data.Dequeue() << 8) + data.Dequeue(), (data.Dequeue() << 8) + data.Dequeue()] = data.Dequeue();
                    }
                }
            }
            catch { }


            for (int i = 0; i < Main.maxTilesX; i++)
            {
                for (int j = 0; j < Main.maxTilesY; j++)
                {
                    LiquidRef liquidRef = grid[i, j];
                    if (liquidRef.TypeID == 255)
                    {
                        liquidRef.Amount = 0;
                        liquidRef.TypeID = 0;
                        liquidRef.LiquidType = null;
                    }
                }
            }
        }

        public LiquidRef this[int x, int y] => new LiquidRef(x, y);

        public override void ModifyWorldGenTasks(List<GenPass> tasks, ref float totalWeight)
        {
            int liquidSettleIndex = tasks.FindIndex(i => i.Name == "Settle Liquids");
            int settleLiquidAgainIndex = tasks.FindIndex(i => i.Name == "Settle Liquids Again");
            int mossIntensifyIndex = tasks.FindIndex(i => i.Name == "Moss");
            if (liquidSettleIndex != -1)
            {
                tasks[liquidSettleIndex] = new PassLegacy("Settle Liquids", (progress) =>
                {
                    WorldGen.waterLine = (int)(Main.rockLayer + (double)Main.maxTilesY) / 2;
                    WorldGen.waterLine += WorldGen.genRand.Next(-100, 20);
                    WorldGen.lavaLine = WorldGen.waterLine + WorldGen.genRand.Next(50, 80);

                    for (int i = 0; i < Main.maxTilesX; i++)
                    {
                        for (int j = 0; j < Main.maxTilesY; j++)
                        {
                            Tile tile = Main.tile[i, j];

                            if (tile.liquid > 0)
                            {
                                LiquidRef liquidRef = grid[i, j];
                                switch (tile.liquidType())
                                {
                                    case 0:
                                        liquidRef.LiquidType = new Water();
                                        break;
                                    case 1:
                                        liquidRef.LiquidType = new Lava();
                                        break;
                                    case 2:
                                        liquidRef.LiquidType = new Honey();
                                        break;
                                    case 3:
                                        liquidRef.LiquidType = new Oil();
                                        break;
                                }
                            }
                        }
                    }

                    Liquid.QuickWater(2);
                    WorldGen.WaterCheck();
                    int num4 = 0;
                    Liquid.quickSettle = true;
                    int num5 = Liquid.numLiquid + LiquidBuffer.numLiquidBuffer;
                    float num6 = 0f;
                    while (Liquid.numLiquid > 0 && num4 < 100000)
                    {
                        num4++;
                        float num7 = (float)(num5 - (Liquid.numLiquid + LiquidBuffer.numLiquidBuffer)) / (float)num5;
                        if (Liquid.numLiquid + LiquidBuffer.numLiquidBuffer > num5)
                            num5 = Liquid.numLiquid + LiquidBuffer.numLiquidBuffer;

                        if (num7 > num6)
                            num6 = num7;
                        else
                            num7 = num6;

                        progress.Value = (num7 * 100f / 2f + 50f);
                        Liquid.UpdateLiquid();
                    }

                    Liquid.quickSettle = false;
                    WorldGen.WaterCheck();
                });
            }

            if (settleLiquidAgainIndex != -1)
            {
                tasks[settleLiquidAgainIndex] = new PassLegacy("Settle Liquids Again", progress =>
                {
                    return;
                    progress.Message = Lang.gen[27].Value;
                    Liquid.QuickWater(3);
                    //WorldGen.WaterCheck();
                    int num78 = 0;
                    Liquid.quickSettle = true;
                    while (num78 < 10)
                    {
                        int num79 = Liquid.numLiquid + LiquidBuffer.numLiquidBuffer;
                        num78++;
                        float num80 = 0f;
                        while (Liquid.numLiquid > 0)
                        {
                            float num81 = (float)(num79 - (Liquid.numLiquid + LiquidBuffer.numLiquidBuffer)) / (float)num79;
                            if (Liquid.numLiquid + LiquidBuffer.numLiquidBuffer > num79)
                                num79 = Liquid.numLiquid + LiquidBuffer.numLiquidBuffer;

                            if (num81 > num80)
                                num80 = num81;
                            else
                                num81 = num80;

                            if (num78 == 1)
                                progress.Set(num81 / 3f + 0.33f);

                            int num82 = 10;
                            if (num78 > num82)
                                num82 = num78;

                            Liquid.UpdateLiquid();
                        }

                        //WorldGen.WaterCheck();
                        progress.Set((float)num78 * 0.1f / 3f + 0.66f);
                    }

                    Liquid.quickSettle = false;
                });
            }

            base.ModifyWorldGenTasks(tasks, ref totalWeight);
        }
    }
}
