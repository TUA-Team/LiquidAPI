using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;
using LiquidAPI.Hooks;
using LiquidAPI.Vanilla;

namespace LiquidAPI
{
	public class LiquidAPI : Mod
	{
		internal static LiquidAPI instance;

		private const int INITIAL_LIQUID_TEXTURE_INDEX = 12;

		public override void Load()
		{
            LiquidRenderer.Instance = new LiquidRenderer();
            instance = this;

			ModBucket emptyBucket = new ModBucket(null, Color.Transparent, "Empty");
			AddItem(emptyBucket.name, emptyBucket.Clone());

			ModLiquid waterLiquid = new Water();
			ModLiquid lavaLiquid = new Lava();
			ModLiquid honeyLiquid = new Honey();
			waterLiquid.AddModBucket();
			lavaLiquid.AddModBucket();
			honeyLiquid.AddModBucket();

		    LiquidHooks.OldHoneyTexture = Main.liquidTexture[11];
		    LiquidHooks.OldLavaTexture = Main.liquidTexture[1];
            List<Texture2D> OldWaterTextureList = new List<Texture2D>();
		    for (int i = 0; i < 11; i++)
		    {
		        if (i == 1 || i == 11)
		        {
		            continue;
		        }
                OldWaterTextureList.Add(Main.liquidTexture[i]);
		    }

		    LiquidHooks.OldWaterTexture = OldWaterTextureList;
		    LoadModContent(Autoload);
        }

		public override void PostSetupContent()
		{
			// Do this after everything is loaded to ensure that ModLoader liquids get taken care of.
			// Otherwise we would need to override ModLoader functions to resize the arrays and load textures.
			
			
			LiquidRegistry.MassMethodSwap();

		    
        }

		public override void Unload()
		{

			LiquidRegistry.MassMethodSwap();

			Array.Resize(ref Main.liquidTexture, INITIAL_LIQUID_TEXTURE_INDEX);
		}

		private static void LoadModContent(Action<Mod> loadAction)
		{
			foreach(Mod mod in ModLoader.Mods)
			{
			    try
			    {
			        loadAction(mod);
			    }
			    catch (Exception e)
			    {
			        Main.statusText = e.Message;
					throw e;
			    }
			}
		}


		private void Autoload(Mod mod)
		{
			if (mod.Code == null)
			{
				return;
			}
			foreach(Type type in mod.Code.DefinedTypes.Where(type => type.IsSubclassOf(typeof(ModLiquid))).OrderBy(type => type.FullName))
			{
				AutoloadLiquid(mod, type);
			}
		}
		private void AutoloadLiquid(Mod mod, Type type)
		{
			ModLiquid liquid = (ModLiquid)Activator.CreateInstance(type);
			liquid.Mod = mod;
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