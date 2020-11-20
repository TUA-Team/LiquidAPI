using LiquidAPI.Caches;
using LiquidAPI.Vanilla;
using System;
using Terraria;
using Terraria.ModLoader;

namespace LiquidAPI
{
    public sealed partial class LiquidAPI : Mod
    {
        public static LiquidAPI Instance => ModContent.GetInstance<LiquidAPI>();

        private const int INITIAL_LIQUID_TEXTURE_INDEX = 12;

        public static event Action OnUnload;

        /// <summary>
        /// Simple block interaction? This would allow creating block when 2 liquid merge
        /// </summary>
        public static int[,] interactionResult = new int[256, 256];
        public static bool[,] killTile = new bool[TileLoader.TileCount, 256];

        public override void Load()
        {
            interactionResult = new int[256, 256];
            killTile = new bool[TileLoader.TileCount, 256];

            ReflectionCaches.Load();

            for (int i = 0; i < 256; i++)
            {
                for (int j = 0; j < 256; j++)
                {
                    interactionResult[i, j] = -1;
                }
            }

            this.AddLiquid<Water>("LiquidWater");
            this.AddLiquid<Lava>("LiquidLava");
            this.AddLiquid<Honey>("LiquidHoney");
            this.AddLiquid<Oil>("LiquidOil");

            renderer = new LiquidRenderer();

            LiquidHooks.LoadOldVanillaTextures();
        }

        public override void PostSetupContent()
        {
            // Do this after everything is loaded to ensure that ModLoader liquids get taken care of.
            // Otherwise we would need to override ModLoader functions to resize the arrays and load textures.


            //LiquidSwapping.MethodSwap();
            //WaterDrawInjection.MethodSwap();
            //InternalLiquidDrawInjection.SwapMethod();
            AddHooks();
        }

        public override void Unload()
        {
            OnUnload?.Invoke();
            OnUnload = null;

            Array.Resize(ref Main.liquidTexture, INITIAL_LIQUID_TEXTURE_INDEX);
            ReflectionCaches.Unload();
        }

        public static void Autoload(Mod mod)
        {
            if (mod.Code == null)
            { return; }

            foreach (Type type in mod.Code.DefinedTypes)
            {
                if (type.IsAbstract)
                    continue;

                if (type.IsSubclassOf(typeof(ModLiquid)))
                {
                    AutoloadLiquid(mod, type);
                }
            }
        }
        private static void AutoloadLiquid(Mod mod, Type type)
        {
            ModLiquid liquid = (ModLiquid)Activator.CreateInstance(type);
            string name = type.Name;
            string texturePath = type.FullName.Replace(".", "/").Substring(mod.Name.Length + 1);
            string fancyTexturePath = texturePath + "Fancy";
            if (liquid.Autoload(ref name, ref texturePath, ref fancyTexturePath))
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
                mod.AddLiquid(name, liquid, mod.GetTexture(texturePath),
                    mod.GetTexture(fancyTexturePath));
            }
        }
    }
}