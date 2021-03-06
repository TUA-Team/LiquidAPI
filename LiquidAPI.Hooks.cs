﻿using MonoMod.RuntimeDetour.HookGen;
using System.Reflection;
using Terraria.Map;
using Terraria.ModLoader;
using static LiquidAPI.Hooks.LiquidHooks;

namespace LiquidAPI
{
    partial class LiquidAPI
    {
        internal LiquidRenderer renderer;

        private void AddHooks()
        {
            // Update
            On.Terraria.Liquid.Update += ModdedLiquidUpdate;
            //On.Terraria.Liquid.QuickWater += QuickWater;

            // Utilities
            On.Terraria.Liquid.AddWater += AddWater;

            // Interaction
            On.Terraria.Liquid.LavaCheck += LiquidOnLavaCheck;
            On.Terraria.Liquid.HoneyCheck += LiquidOnHoneyCheck;

            // Rendering
            On.Terraria.Main.oldDrawWater += OldWaterDraw;
            On.Terraria.Main.DrawWater += Hooked_DrawWater;
            On.Terraria.Main.drawWaters += Hooked_drawWaters;
            IL.Terraria.Main.DrawTiles += ILDrawWaterSlope;

            // Liquid Renderer
            renderer.AddHooks();

            // Collision
            On.Terraria.NPC.Collision_WaterCollision += Collision_WaterCollision;
            On.Terraria.Collision.WetCollision += WetCollision;
            On.Terraria.Collision.LavaCollision += LavaCollision;

            // TODO: WaterStyleLoader -> Resize Arrays hook needed for LiquidRenderer texture array (Might not be required if done on PostLoad)
            // TODO: ModInternals -> SetupContent hook needed for LiquidRenderer texture array (Might not be required if done on PostLoad)	

            //Map
            On.Terraria.Map.MapHelper.CreateMapTile += CreateMapTile;
            On.Terraria.Map.MapHelper.GetMapTileXnaColor += GetMapTileXnaColor;
            On.Terraria.Lang.GetMapObjectName += GetMapObjectName;
            MapLegend_FromTile += FromTile;
        }

        private delegate string orig_FromTile(MapLegend instance, MapTile mapTile, int x, int y);
        private delegate string hook_FromTile(orig_FromTile orig, MapLegend instance, MapTile mapTile, int x, int y);
        private static readonly MethodBase method_MapLegend_FromTile = MethodBase.GetMethodFromHandle(typeof(MapLegend).GetMethod("FromTile", BindingFlags.Public | BindingFlags.Instance).MethodHandle);

        private static event hook_FromTile MapLegend_FromTile
        {
            add
            {
                HookEndpointManager.Add<hook_FromTile>(method_MapLegend_FromTile, value);
            }
            remove
            {
                HookEndpointManager.Remove<hook_FromTile>(method_MapLegend_FromTile, value);
            }
        }

        private static string FromTile(orig_FromTile orig, MapLegend instance, MapTile mapTile, int x, int y)
        {
            if (mapTile.Type < 10000)
            {
                return orig(instance, mapTile, x, y);
            }
            return "Programmer are working, do not disturb them unless you are ready to face the wrath of hell";
        }
    }
}
