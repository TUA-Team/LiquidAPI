using System;
using System.Collections.Generic;
using System.Reflection;
using Terraria;
using Terraria.ModLoader;

namespace LiquidAPI.Caches
{
    class ReflectionCaches
    {
        internal static Dictionary<Type, Dictionary<string, Delegate>> methodCache = new Dictionary<Type, Dictionary<string, Delegate>>();
        internal static Dictionary<Type, Dictionary<string, PropertyInfo>> propertyCache = new Dictionary<Type, Dictionary<string, PropertyInfo>>();
        internal static Dictionary<Type, Dictionary<string, FieldInfo>> fieldCache = new Dictionary<Type, Dictionary<string, FieldInfo>>();
        /// <summary>
        /// For mod injection later on
        /// </summary>
        internal static Dictionary<Type, Dictionary<string, Assembly>> assemblyCache = new Dictionary<Type, Dictionary<string, Assembly>>();


        internal static void Unload()
        {
            fieldCache.Clear();
            propertyCache.Clear();
            methodCache.Clear();
        }

        internal static void Load()
        {
            AddFieldInfoCaches();
        }

        private static void AddFieldInfoCaches()
        {
            fieldCache.Add(typeof(ItemLoader), new Dictionary<string, FieldInfo>()
            {
                ["items"] = typeof(ItemLoader).GetField("items", BindingFlags.Static | BindingFlags.NonPublic)
            });
            fieldCache.Add(typeof(Liquid), new Dictionary<string, FieldInfo>()
            {
                ["wetCounter"] = typeof(Liquid).GetField("wetCounter", BindingFlags.Static | BindingFlags.NonPublic),

            });
        }
    }
}
