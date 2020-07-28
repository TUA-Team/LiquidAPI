using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.GameContent.UI.States;
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
        }
    }
}
