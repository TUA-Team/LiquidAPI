using LiquidAPI.Hooks;
using System;
using Terraria.ModLoader;

namespace LiquidAPI
{
    public sealed partial class LiquidAPI : Mod
	{
		public static event Action OnUnload;

		public override void Load()
		{
			AddContent(ModBucket.Empty);
		}

		public override void PostSetupContent()
		{
			// Do this after everything is loaded to ensure that ModLoader liquids get taken care of.
			// Otherwise we would need to override ModLoader functions to resize the arrays and load textures.
			LiquidHooks.AddHooks();
		}

		public override void Unload()
		{
			if (OnUnload != null)
			{
				OnUnload.Invoke();
				OnUnload = null;
			}
		}
	}
}