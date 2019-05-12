using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using LiquidAPI.Hooks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Map;
using Terraria.ModLoader;

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

			ModBucket bucket = new ModBucket(-1, Color.Transparent, "Empty");
			AddItem(bucket.name, bucket.Clone());

			bucket = new ModBucket(0, new Color(51, 107, 249), "Water");
			AddItem(bucket.name, bucket.Clone());

			bucket = new ModBucket(1, new Color(253, 62, 3), "Lava");
			AddItem(bucket.name, bucket.Clone());

			bucket = new ModBucket(2, new Color(215, 131, 8), "Honey");
			AddItem(bucket.name, bucket.Clone());

		    LiquidHooks.OldHoneyTexture = new List<Texture2D>()
		    {
		        Main.liquidTexture[11] //default honey

		    };
		    LiquidHooks.OldLavaTexture = new List<Texture2D>()
		    {
		        Main.liquidTexture[1], //default lava
                LiquidAPI.instance.GetTexture("Texture/Lava_Test/Cursed_Lava"),
		        LiquidAPI.instance.GetTexture("Texture/Lava_Test/Ichor_Lava")
            };
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
		    LoadModContent(mod => { Autoload(mod); });
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
			for (int i = 0; i < ModLoader.Mods.Length; i++)
			{
				Mod mod = ModLoader.Mods[i];

			    try
			    {
			        loadAction(mod);
			    }
			    catch (Exception e)
			    {
			        Main.statusText = e.Message;
			    }
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
				var type =  array.ElementAt(i);
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
				LiquidRegistry.getInstance().AddNewModLiquid(liquid, this.GetTexture(texturePath));
			}
		}
	}
}