using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using LiquidAPI.Hooks;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace LiquidAPI
{
	public static class LiquidRegistry
	{
		internal static Dictionary<int, ModLiquid> liquidList = new Dictionary<int, ModLiquid>();
		private static int initialLiquidIndex = 0;//3;
		private static int liquidTextureIndex = 12;

		private const int vanillaMaxVanilla = 13;

		static LiquidRegistry()
		{
			LiquidAPI.OnUnload+=()=>
			{
				Array.Resize(ref TextureAssets.Liquid, vanillaMaxVanilla);
				liquidList.Clear();
				liquidList = null;
			};
		}

		public static void AddLiquid<TLiquid>(this Mod mod,string name,Texture2D texture = null) where TLiquid:ModLiquid,new()
		{
			mod.AddLiquid(name,new TLiquid(),texture);
		}
		public static void AddLiquid(this Mod mod,string name,ModLiquid liquid, Texture2D texture = null)
		{
			liquid.Mod=mod;
			liquid.Name=name;
			liquid.DisplayName = mod.CreateTranslation($"Mods.{mod.Name}.ItemName.{name}".Replace(" ","_"));
			liquid.DisplayName.SetDefault(Regex.Replace(name, "([A-Z])", " $1").Trim());

			Texture2D usedTexture = texture ?? liquid.Texture;
			Array.Resize(ref TextureAssets.Liquid, TextureAssets.Liquid.Length + 1);
			liquid.Type = initialLiquidIndex++;
			liquidList.Add(liquid.Type, liquid);
			if (Main.netMode == NetmodeID.SinglePlayer)
			{
				LiquidRenderer.Instance.LiquidTextures[LiquidRenderer.Instance.LiquidTextures.Count - 1] = usedTexture;
			}

			liquid.AddModBucket();
		}

		public static ModLiquid GetLiquid(int i)=>liquidList[i];

		public static void AddHooks()
		{
			//LiquidSwapping.MethodSwap();
			//WaterDrawInjection.MethodSwap();
			//InternalLiquidDrawInjection.SwapMethod();
			LiquidHooks.AddHooks();
		}

		public static void PreDrawValue(ref bool bg, ref int style, ref float Alpha)
		{
			foreach(ModLiquid liquid in liquidList.Values)
			{
				liquid.PreDraw(Main.tileBatch);
			}
		}

		public static void Update()
		{
			foreach(ModLiquid liquid in liquidList.Values)
			{
				liquid.Update();
			}
		}

		/*public static bool RunUpdate(byte index, int x, int y)
		{
			int newIndex = index - 3;
			if (newIndex > liquidList.Count || newIndex < 0)
			{
				return false;
			}
			else
			{
				return liquidList[newIndex].CustomPhysic(x, y);
			}
		}*/
	}
}