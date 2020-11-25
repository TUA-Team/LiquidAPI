using LiquidAPI.LiquidMod;
using LiquidAPI.Vanilla;
using System;
using System.Collections.Generic;
using Terraria.GameContent;

namespace LiquidAPI
{
    public static class LiquidLoader
    {
        private const int INITIAL_LIQUID_TEXTURE_LENGTH = 13;

        internal static readonly IList<ModLiquid> liquids = new List<ModLiquid>();

        public static int LiquidCount => liquids.Count;

        static LiquidLoader()
        {
            LiquidAPI.OnUnload += () =>
            {
                liquids.Clear();

                if (TextureAssets.Liquid.Length != INITIAL_LIQUID_TEXTURE_LENGTH)
                {
                    Array.Resize(ref TextureAssets.Liquid, INITIAL_LIQUID_TEXTURE_LENGTH);
                }
            };

            Add(new Water());
        }


        internal static void Add(ModLiquid liquid)
        {
            liquid.AddModBucket();

            liquids.Add(liquid);
        }


        public static void BeginUpdate(LiquidRef liquid)
        {
            liquid.ModLiquid.BeginUpdate(liquid);
        }
    }
}
