using System;
using System.Collections.Generic;
using Terraria.GameContent;

namespace LiquidAPI
{
    public static class LiquidLoader
    {
        private const int INITIAL_LIQUID_TEXTURE_LENGTH = 13;

        private static readonly IList<ModLiquid> liquids = new List<ModLiquid>();

        static LiquidLoader()
        {
            LiquidAPI.OnUnload += () =>
            {
                liquids?.Clear();

                if (TextureAssets.Liquid.Length != INITIAL_LIQUID_TEXTURE_LENGTH)
                {
                    Array.Resize(ref TextureAssets.Liquid, INITIAL_LIQUID_TEXTURE_LENGTH);
                }
            };
        }


        internal static void Add(ModLiquid liquid)
        {
            var (name, mod) = (liquid.Name, liquid.Mod);
            // TODO: automatic json localization loading
            liquid.DisplayName = mod.CreateTranslation($"Mods.{mod.Name}.ItemName.{name}".Replace(" ", "_"));
            liquid.SetStaticDefaults();

            liquid.Type = liquids.Count;
            liquids.Add(liquid);

            liquid.AddModBucket();
        }
    }
}
