using LiquidAPI.Hooks;
using LiquidAPI.Vanilla;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.ModLoader;

namespace LiquidAPI
{
    public class LiquidAPI : Mod
    {
        internal static LiquidAPI instance;

        private const int INITIAL_LIQUID_TEXTURE_INDEX = 12;

        public static event Action OnUnload;

        /// <summary>
        /// Simple block interaction? This would allow creating block when 2 liquid merge
        /// </summary>
        public static int[,] interactionResult = new int[256, 256];
        public static bool[,] killTile = new bool[TileLoader.TileCount, 256];

        public override void Load()
        {
            LiquidRenderer.Instance = new LiquidRenderer();
            instance = this;

            interactionResult = new int[256, 256];
            killTile = new bool[TileLoader.TileCount, 256];

            ModBucket emptyBucket = new ModBucket();
            AddItem("BucketEmpty", emptyBucket);

            this.AddLiquid<Water>("LiquidWater");
            this.AddLiquid<Lava>("LiquidLava");
            this.AddLiquid<Honey>("LiquidHoney");
            this.AddLiquid<Oil>("LiquidOil");

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
            LoadModContent(Autoload);
        }

        public override void PostSetupContent()
        {
            // Do this after everything is loaded to ensure that ModLoader liquids get taken care of.
            // Otherwise we would need to override ModLoader functions to resize the arrays and load textures.


            LiquidRegistry.AddHooks();
        }

        public override void Unload()
        {
            OnUnload?.Invoke();
            OnUnload = null;
            Array.Resize(ref Main.liquidTexture, INITIAL_LIQUID_TEXTURE_INDEX);
        }

        private static void LoadModContent(Action<Mod> loadAction)
        {
            foreach (Mod mod in ModLoader.Mods)
            {
                try
                {
                    loadAction(mod);
                }
                catch (Exception e)
                {
                    Main.statusText = e.Message;
                    throw e;
                }
            }
        }


        private void Autoload(Mod mod)
        {
            if (mod.Code == null) { return; }

            foreach (Type type in mod.Code.DefinedTypes.Where(type => type.IsSubclassOf(typeof(ModLiquid))).OrderBy(type => type.FullName))
            {
                AutoloadLiquid(mod, type);
            }
        }
        private void AutoloadLiquid(Mod mod, Type type)
        {
            ModLiquid liquid = (ModLiquid)Activator.CreateInstance(type);
            liquid.Mod = mod;
            string name = type.Name;
            string texturePath = liquid.GetType().FullName.Replace(".", "/").Replace(this.Name + "/", "");
            string fancyTexturePath = liquid.GetType().FullName.Replace(".", "/").Replace(this.Name + "/", "") + "Fancy";
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
                mod.AddLiquid(name, liquid, this.GetTexture(texturePath), this.GetTexture(fancyTexturePath));
            }
        }
    }
}