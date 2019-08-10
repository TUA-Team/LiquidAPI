using Terraria;

namespace LiquidAPI.LiquidMod
{
    public readonly ref struct LiquidRef
    {
        public readonly int X;
        public readonly int Y;

        public Tile Tile => Main.tile[X, Y];

        public ModLiquid Type
        {
            get => LiquidRegistry.liquidList[LiquidWorld.liquidGrid[X, Y].data];
            set
            {
                if (value == null)
                {
                    LiquidWorld.liquidGrid[X, Y].data = 0;
                    Main.tile[X, Y].liquid = 0;
                }
                else
                {
                    LiquidWorld.liquidGrid[X, Y].data = (byte)value.Type;
                }
            }
        }
        public ref byte TypeID => ref LiquidWorld.liquidGrid[X, Y].data;

        public ref byte Amount => ref Main.tile[X, Y].liquid;

        public bool CheckingLiquid
        {
            get => Main.tile[X, Y].checkingLiquid();
            set => Main.tile[X, Y].checkingLiquid(value);
        }

        public bool SkipLiquid
        {
            get => Main.tile[X, Y].skipLiquid();
            set => Main.tile[X, Y].skipLiquid(value);
        }

        public bool HasLiquid => Amount > 0;

        public LiquidRef(int x, int y)
        {
            X = x;
            Y = y;
            if (Main.tile[x, y] == null)
            {
                Main.tile[x, y] = new Tile();
            }
        }
    }
}