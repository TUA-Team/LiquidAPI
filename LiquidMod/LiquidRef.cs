using Terraria;

namespace LiquidAPI.LiquidMod
{
	public class LiquidRef
	{
		public int x;
		public int y;

		public Tile Tile => Main.tile[x, y];

		public byte Type
		{
			get => LiquidCore.liquidGrid[x, y].data;
			set => LiquidCore.liquidGrid[x, y].data = value;
		}

		public byte Amount
		{
			get => Tile.liquid;
			set => Tile.liquid = value;
		}

		private bool _checkingLiquid;
		private bool _skipLiquid;

		public LiquidRef(int x, int y)
		{
			if (Main.tile[x, y] == null)
			{
				Main.tile[x, y] = new Tile();
			}

			this.x = x;
			this.y = y;

			if (Tile.bTileHeader == 159)
			{
				LiquidCore.liquidGrid[x, y].data = 0;
			}
			else if (Tile.lava())
			{
				LiquidCore.liquidGrid[x, y].data = 1;
			}
			else if (Tile.honey())
			{
				LiquidCore.liquidGrid[x, y].data = 2;
			}
		}

		public bool CheckingLiquid()
		{
			//return _checkingLiquid;
			return Tile.checkingLiquid();
		}

		public void SetCheckingLiquid(bool flag)
		{
			//_checkingLiquid = flag;
			Tile.checkingLiquid(flag);
		}

		public bool SkipLiquid()
		{
			//return _skipLiquid;
			return Tile.skipLiquid();
		}

		public void SetSkipLiquid(bool flag)
		{
			//_skipLiquid = flag;
			Tile.skipLiquid(flag);
		}

		public bool HasLiquid()
		{
			return Amount > 0;
		}
	}
}
