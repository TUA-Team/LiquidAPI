using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Map;
using Terraria.ModLoader;

namespace LiquidAPI
{
	public class LiquidAPI : Mod
	{
		internal static LiquidAPI instance;

		private const int initialLiquidTextureIndex = 12;

		public override void Load()
		{
			instance = this;

			ModBucket bucket = new ModBucket(-1, Color.Transparent, "Empty");
			AddItem(bucket.name, bucket.Clone());

			bucket = new ModBucket(0, new Color(51, 107, 249), "Water");
			AddItem(bucket.name, bucket.Clone());

			bucket = new ModBucket(1, new Color(253, 62, 3), "Lava");
			AddItem(bucket.name, bucket.Clone());

			bucket = new ModBucket(2, new Color(215, 131, 8), "Honey");
			AddItem(bucket.name, bucket.Clone());

			LoadModContent(mod => { Autoload(mod); });
		}

		public override void PostSetupContent()
		{
			// Do this after everything is loaded to ensure that ModLoader liquids get taken care of.
			// Otherwise we would need to override ModLoader functions to resize the arrays and load textures.
			LiquidRenderer.Instance = new LiquidRenderer();
			
			LiquidRegistry.MassMethodSwap();
		}

		public override void Unload()
		{

			LiquidRegistry.MassMethodSwap();

			Array.Resize(ref Main.liquidTexture, initialLiquidTextureIndex);
		}

		private static void LoadModContent(Action<Mod> loadAction)
		{
			for (int i = 0; i < ModLoader.Mods.Length; i++)
			{
				Mod mod = ModLoader.Mods[i];
				try
				{
					loadAction(mod);
				}
				catch { }
			}
		}


		private void Autoload(Mod mod)
		{
			if (mod.Code == null)
			{
				return;
			}

			var array = mod.Code.DefinedTypes.OrderBy(type => type.FullName, StringComparer.InvariantCulture);
			for (int i = 0; i < array.Count(); i++)
			{
				var type = array.ElementAt(i);
				if (type.IsSubclassOf(typeof(ModLiquid)))
				{
					AutoloadLiquid(mod, type);
				}
			}
		}
		private void AutoloadLiquid(Mod mod, Type type)
		{
			Color[] color = (Color[])typeof(MapHelper).GetField("colorLookup", BindingFlags.Static | BindingFlags.NonPublic).GetValue(null);
			ModLiquid liquid = (ModLiquid)Activator.CreateInstance(type);
			liquid.mod = mod;
			string texturePath = liquid.GetType().FullName.Replace(".", "/").Replace(this.Name + "/", "");
			if (liquid.Autoload(ref texturePath))
			{
				// @Dradon y u do dis twice lulz
				// it supposed to be in func call belowe butt dis no need
				// an break honey textuar
				//if (Main.netMode == 0)
				//{
					//Main.liquidTexture[Main.liquidTexture.Length - 1] = this.GetTexture(texturePath);
					//LiquidRenderer.Instance.LiquidTextures[LiquidRenderer.Instance.LiquidTextures.Count - 1] =
					//	this.GetTexture(texturePath);
				//}
				LiquidRegistry.getInstance().AddNewModLiquid(liquid, this.GetTexture(texturePath));
			}
		}
	}
}