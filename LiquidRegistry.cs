using System;
using System.Collections.Generic;
using LiquidAPI.LiquidMod;
using LiquidAPI.Swap;
using Terraria;
using Terraria.GameContent.Liquid;

namespace LiquidAPI
{
	class LiquidRegistry
	{
		private static LiquidRegistry instance;
		internal static List<ModLiquid> liquidList;
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

		public void addNewModLiquid(ModLiquid liquid)
		{
			Array.Resize(ref Main.liquidTexture, Main.liquidTexture.Length + 1);
			Array.Resize(ref LiquidRendererExtension.liquidTexture2D, LiquidRendererExtension.liquidTexture2D.Length + 1);
			liquid.liquidIndex = initialLiquidIndex;
			initialLiquidIndex++;
			liquidList.Add(liquid);
			if(Main.netMode == 0) {
				Main.liquidTexture[Main.liquidTexture.Length-1] = liquid.texture;
			    LiquidRendererExtension.liquidTexture2D[LiquidRendererExtension.liquidTexture2D.Length - 1] =
			        liquid.texture;
			}
			liquid.AddModBucket();
		}

		private LiquidRegistry()
		{
			liquidList = new List<ModLiquid>();		
		}

		public static void MassMethodSwap()
		{
			LiquidSwapping.MethodSwap();
			WaterDrawInjection.MethodSwap();
			InternalLiquidDrawInjection.SwapMethod();
            LiquidExtension.MethodSwap();
		}

		public void Unload()
		{
			Array.Resize(ref Main.liquidTexture, vanillaMaxVanilla);
			Array.Resize(ref LiquidRenderer.Instance._liquidTextures, vanillaMaxVanilla);
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
			for (byte by = 0; by < LiquidRegistry.liquidList.Count; by = (byte)(by + 1))
			{
				if (liquid.Liquids((byte) (2 + by)))
				{
					return liquidList[by].SetLiquidOpacity();
				}
			}
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

	    public static bool RunUpdate(byte index, int x, int y)
	    {
            return liquidList[index].CustomPhysic(x, y);
	    }
	}
}
