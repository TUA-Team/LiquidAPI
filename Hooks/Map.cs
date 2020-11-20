using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LiquidAPI.LiquidMod;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Map;
using static Terraria.Map.MapHelper;

namespace LiquidAPI.Hooks
{
    internal static partial class LiquidHooks
    {
        public static MapTile CreateMapTile(On.Terraria.Map.MapHelper.orig_CreateMapTile orig, int i, int j, byte Light) {
			Tile tile = Main.tile[i, j];
			if (tile == null)
				tile = (Main.tile[i, j] = new Tile());

            LiquidRef liquid = LiquidWorld.grid[i, j];

            int num2 = Light;
            int num7 = 0;

            if (tile.liquid > 32) {
                num7 = liquid.LiquidType.Type + 10000;
            }
            else
            {
                return orig(i, j, Light);
            }

            return MapTile.Create((ushort) num7, (byte)num2, (byte)0);
        }

        public static Color GetMapTileXnaColor(On.Terraria.Map.MapHelper.orig_GetMapTileXnaColor orig, ref MapTile tile)
        {
            int actualID = 0;
            if (tile.Type < 10000)
            {
                return orig(ref tile);
            }
			
            actualID = tile.Type - 10000;

            if (!LiquidRegistry.mapColorLookup.ContainsKey(actualID))
            {
                return orig(ref tile);
            }

            Color oldColor = LiquidRegistry.mapColorLookup[actualID];
            byte color = tile.Color;
            if (color > 0)
                //MapColor(tile.Type, ref oldColor, color);

            if (tile.Light == byte.MaxValue)
                return oldColor;

            float num = (float)(int)tile.Light / 255f;
            oldColor.R = (byte)((float)(int)oldColor.R * num);
            oldColor.G = (byte)((float)(int)oldColor.G * num);
            oldColor.B = (byte)((float)(int)oldColor.B * num);
            return oldColor;
        }

        public static string GetMapObjectName(On.Terraria.Lang.orig_GetMapObjectName orig, int id)
        {
            if (id >= 10000)
                return "Programmer are working, do not disturb them unless you are ready to face the wrath of hell";
            return orig(id);
        }
    }
}
