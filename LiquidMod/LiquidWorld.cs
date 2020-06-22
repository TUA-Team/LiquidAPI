using System.Collections.Generic;
using System.IO;
using Terraria;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using Terraria.Utilities;
using LiquidAPI.Data;

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
			LiquidAPI.OnUnload+=()=>
			{
				grid=null;
				liquidGrid=null;
			};
		}

		public override void Initialize()
		{
			grid=this;
			liquidGrid = new Bit[Main.maxTilesX, Main.maxTilesY];
		}

		public override TagCompound Save()
		{
			try
			{
				string path = Path.ChangeExtension(Main.ActiveWorldFileData.Path, EXTENSION); //Change current world path to the custom save one
				if (FileUtilities.Exists(path, false)) { FileUtilities.Copy(path, path + ".bak", false, true); } //also make a backup
				Queue<byte> data = new Queue<byte>();
				data.Enqueue(MODE);
				data.Enqueue(FORM);//Point Storage
				for (ushort y = 0; y < Main.maxTilesY; y++)
				{
					for (ushort x = 0; x < Main.maxTilesX; x++)
					{
						if (liquidGrid[x, y] != 0)
						{
							data.Enqueue((byte)(x >> 8)); data.Enqueue((byte)x);
							data.Enqueue((byte)(y >> 8)); data.Enqueue((byte)y);
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
				if (!FileUtilities.Exists(path, false)) { return; }
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
		}

		public LiquidRef this[int x, int y]=>new LiquidRef(x, y);
	}
}
