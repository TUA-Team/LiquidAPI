using Terraria;

namespace LiquidAPI.LiquidMod
{
    public class LiquidRef
    {

        public int x;
        public int y;

        public byte _liquidType;
        public byte _liquidAmount;

        public Tile tile;

        public byte liquidType
        {
            get
            {
                return _liquidType;
            }
            set
            {
                _liquidType = value;
            }
        }

        public byte liquidAmount
        {
            get { return _liquidAmount; }
            set
            {
                tile.liquid = value;
                _liquidAmount = value;
            }
            
        }

        public LiquidRef(int x, int y)
        {
            if (Main.tile[x, y] == null)
            {
                Main.tile[x, y] = new Tile();
            }
            tile = Main.tile[x, y];
            this.x = x;
            this.y = y;
            

            if (tile != null)
            {
                if (tile.bTileHeader == 159)
                {
                    LiquidCore.liquidGrid[x, y][0] = true;
                }
                else if (tile.lava())
                {
                    LiquidCore.liquidGrid[x, y][1] = true;
                }
                else if (tile.honey())
                {
                    LiquidCore.liquidGrid[x, y][2] = true;
                }
                this._liquidType = tile.liquidType();
                this._liquidAmount = tile.liquid;
            }
            else
            {
                _liquidType = 255;
            }
        }

        public bool CheckingLiquid()
        {
            return liquidType == 255;
        }

        public bool Liquids(byte index)
        {
            return LiquidCore.liquidGrid[x, y][index];
        }

        public byte liquidsType()
        {
            return _liquidType;
        }

        public void SetLiquidsState(byte index, bool value)
        {
            LiquidCore.liquidGrid[x, y].data = index;
            /*switch (index)
            {
                case 0:
                    _liquidType = 0;
                    LiquidCore.liquidGrid[x, y].data = index;
                    break;
                case 1:
                    _liquidType = 1;
                    LiquidCore.liquidGrid[x, y][index] = value;
                    break;
                case 2:
                    _liquidType = 2;
                    LiquidCore.liquidGrid[x, y][index] = value;
                    break;
                default:
                    _liquidType = index;
                    LiquidCore.liquidGrid[x, y][index] = value;
                    break;
            }*/
        }

        public byte GetLiquidAmount()
        {
            return tile.liquid;
        }

        public bool NoLiquid()
        {
            return tile.liquid == 0;
        }
    }
}
