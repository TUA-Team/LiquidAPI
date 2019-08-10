using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using LiquidAPI.Hooks;
using LiquidAPI.LiquidMod;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
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
				Array.Resize(ref Main.liquidTexture, vanillaMaxVanilla);
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
			Array.Resize(ref Main.liquidTexture, Main.liquidTexture.Length + 1);
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

		public static float setOpacity(LiquidRef liquid)
		{
			/*for (byte by = 0; by < LiquidRegistry.liquidList.Count; by = (byte) (by + 1))
			{
				if (liquid.Liquids((byte) (2 + by)))
				{
					return liquidList[by].SetLiquidOpacity();
				}
			}*/

			return 1f;
		}

		public static void PlayerInteraction(byte index, Player target)
		{
			liquidList[index].PlayerInteraction(target);
		}

		public static void NPCInteraction(byte index, NPC target)
		{
			liquidList[index].NPCInteraction(target);
		}

		public static void ItemInteraction(byte index, Item item)
		{
			liquidList[index].ItemInteraction(item);
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