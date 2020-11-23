﻿using System;
using System.Reflection;

namespace LiquidAPI.Hooks
{
    internal static partial class LiquidHooks
	{
		public static void AddHooks()
        {
			foreach (var type in Assembly.GetExecutingAssembly().DefinedTypes)
            {
				if (typeof(ILiquidHook).IsAssignableFrom(type))
                {
					var hook = (ILiquidHook)Activator.CreateInstance(type);
					hook.Init();
                }
            }
        }

		/*public static void AddHooks()
		{
			// Update
			On.Terraria.Liquid.Update += ModdedLiquidUpdate;

			// Utilities
			On.Terraria.Liquid.AddWater += AddWater;

			// Interaction
			On.Terraria.Liquid.LavaCheck += LiquidOnLavaCheck;
			On.Terraria.Liquid.HoneyCheck += LiquidOnHoneyCheck;

			// Rendering
			On.Terraria.Main.oldDrawWater += MainOnOldDrawWater;

			// Liquid Renderer
			On.Terraria.GameContent.Liquid.LiquidRenderer.Update +=
				(orig, self, time) => LiquidRenderer.Instance.Update(time);

			On.Terraria.GameContent.Liquid.LiquidRenderer.PrepareDraw +=
				(orig, self, area) => LiquidRenderer.Instance.PrepareDraw(area);

			On.Terraria.GameContent.Liquid.LiquidRenderer.Draw += (orig, self, batch, offset, style, alpha, draw) =>
				LiquidRenderer.Instance.Draw(batch, offset, style, alpha, draw);

			On.Terraria.GameContent.Liquid.LiquidRenderer.HasFullWater +=
				(orig, self, x, y) => LiquidRenderer.Instance.HasFullWater(x, y);

			On.Terraria.GameContent.Liquid.LiquidRenderer.SetWaveMaskData +=
				(On.Terraria.GameContent.Liquid.LiquidRenderer.orig_SetWaveMaskData orig, Terraria.GameContent.Liquid.LiquidRenderer self, ref Texture2D texture) =>
					LiquidRenderer.Instance.SetWaveMaskData(ref texture);

			On.Terraria.GameContent.Liquid.LiquidRenderer.GetCachedDrawArea +=
				(orig, self) => LiquidRenderer.Instance.GetCachedDrawArea();

			// TODO: WaterStyleLoader -> Resize Arrays hook needed for LiquidRenderer texture array (Might not be required if done on PostLoad)
			// TODO: ModInternals -> SetupContent hook needed for LiquidRenderer texture array (Might not be required if done on PostLoad)	
		}*/
	}
}