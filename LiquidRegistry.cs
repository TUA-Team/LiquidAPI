using System;
using System.Collections.Generic;
using LiquidAPI.Hooks;
using LiquidAPI.LiquidMod;
using LiquidAPI.Swap;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent.Liquid;

namespace LiquidAPI
{
	class LiquidRegistry
	{
		private static LiquidRegistry instance;
		internal static List<ModLiquid> liquidList = new List<ModLiquid>();
		private static int initialLiquidIndex = 3;
		private static int liquidTextureIndex = 12;

		private const int vanillaMaxVanilla = 13;

		public static LiquidRegistry getInstance()
		{
			if (instance == null)
			{
				instance = new LiquidRegistry();
			}

			return instance;
		}

		public void AddNewModLiquid(ModLiquid liquid, Texture2D texture = null)
		{
			Texture2D usedTexture = texture ?? liquid.texture;
			Array.Resize(ref Main.liquidTexture, Main.liquidTexture.Length + 1);
			liquid.liquidIndex = initialLiquidIndex;
			initialLiquidIndex++;
			liquidList.Add(liquid);
			if (Main.netMode == 0)
			{
				Main.liquidTexture[Main.liquidTexture.Length - 1] = usedTexture;
				LiquidRenderer.Instance.LiquidTextures[LiquidRenderer.Instance.LiquidTextures.Count - 1] = usedTexture;
			}

			liquid.AddModBucket();
		}

		private LiquidRegistry()
		{
			
		}

		public static void MassMethodSwap()
		{
			//LiquidSwapping.MethodSwap();
			//WaterDrawInjection.MethodSwap();
			//InternalLiquidDrawInjection.SwapMethod();
			LiquidHooks.MethodSwap();
		}

		public void Unload()
		{
			Array.Resize(ref Main.liquidTexture, vanillaMaxVanilla);
			liquidList.Clear();
			liquidList = null;
		}

		public static void PreDrawValue(ref bool bg, ref int style, ref float Alpha)
		{
			for (int i = 0; i < liquidList.Count; i++)
			{
				ModLiquid liquid = liquidList[i];
				liquid.PreDraw(Main.tileBatch);
			}
		}

		public static void Update()
		{
			for (int i = 0; i < liquidList.Count; i++)
			{
				ModLiquid liquid = liquidList[i];
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
			liquidList[index].NpcInteraction(target);
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