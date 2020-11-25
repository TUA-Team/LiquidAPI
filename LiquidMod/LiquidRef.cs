using Terraria;

namespace LiquidAPI.LiquidMod
{
	/// <summary>
	/// An object that represents a single tile point in the world and provides liquid-related information about said point.
	/// </summary>
	public readonly ref struct LiquidRef
	{
		public readonly ushort X;
		public readonly ushort Y;

		public LiquidRef(ushort x, ushort y)
		{
			X = x;
			Y = y;
			if (Main.tile[x, y] == null)
			{
				Main.tile[x, y] = new Tile();
			}
		}

		/// <summary>
		/// The ModLiquid responsible for handling this liquid and its like. Shorthand for LiquidLoader.GetLiquid(TypeID)
		/// </summary>
		public ModLiquid ModLiquid
		{
			get=>LiquidLoader.liquids[LiquidWorld.liquidGrid[X,Y].data];
			set
			{
				if(value==null)
				{
					LiquidWorld.liquidGrid[X, Y].data=0;
					Main.tile[X,Y].liquid=0;
				}
				else
				{
					LiquidWorld.liquidGrid[X, Y].data=(byte)value.Type;
				}
			}
		}

		// TODO: 
		public bool CheckingLiquid
		{
			get => Main.tile[X, Y].checkingLiquid();
			set => Main.tile[X, Y].checkingLiquid(value);
		}

		// TODO:
		public bool SkipLiquid
		{
			get => Main.tile[X, Y].skipLiquid();
			set => Main.tile[X, Y].skipLiquid(value);
		}

		/// <summary>
		/// The tile at this point.
		/// </summary>
		public Tile Tile => Main.tile[X, Y];

		// NOTE: Shouldn't this be a normal property? Modders shouldn't be able to just change liquids on a whim
		/// <summary>
		/// The numeric type ID of the liquid at this point.
		/// </summary>
		public ref byte TypeID => ref LiquidWorld.liquidGrid[X, Y].data;

		/// <summary>
		/// The amount of liquid at this point.
		/// </summary>
		public ref byte Amount => ref Main.tile[X,Y].liquid;

		/// <summary>
		/// A nice shorthand for Amount > 0
		/// </summary>
		public bool HasLiquid=>Amount>0;
	}
}
