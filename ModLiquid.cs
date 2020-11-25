using LiquidAPI.LiquidMod;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
// ReSharper disable InconsistentNaming

namespace LiquidAPI
{
    public abstract class ModLiquid : ModTexturedType
	{
		public Color liquidColor = Color.White;
		public byte waterfallLength = 10;
		public float defaultOpacity = 1f;
		public byte waveMaskStrength = 0;
		public byte viscosityMask = 0;


		internal void AddModBucket() => Mod.AddContent(CreateBucket());


		protected sealed override void Register()
        {
			Type = LiquidLoader.liquids.Count;

			DisplayName = Mod.CreateTranslation($"Mods.{Mod.Name}.ItemName.{Name}");

			LiquidLoader.Add(this);
        }

        public sealed override void SetupContent()
        {
			SetStaticDefaults();
        }


        /// <summary>
        /// Register all static defaults for liquids here like <see cref="DisplayName"/>.
        /// </summary>
        public virtual void SetStaticDefaults()
        {
        }

		/// <summary>
		/// Make something happen at the beginning of <see cref="Liquid.Update"/>. Useful for things like water evaporating in the underworld.
		/// </summary>
		/// <param name="liquid"></param>
		public virtual void BeginUpdate(LiquidRef liquid)
        {
        }

		protected virtual ModBucket CreateBucket() => new ModBucket(this, $"{Name}Bucket");


		/// <summary>
		/// The ID of this liquid
		/// </summary>
		public int Type { get; private set; }

		public ModTranslation DisplayName { get; private set; }


		// Legacy (to be ported)		
		/*
		 * 		public bool gravity = true;
		public int customDelay = 1; //Default value, aka 

		 * //Normally trigger if gravity is at false
		public virtual bool CustomPhysic(int x, int y)
		{
			LiquidRef liquidLeft = LiquidCore.grid[x - 1, y];
			LiquidRef liquidRight = LiquidCore.grid[x + 1, y];
			LiquidRef liquidUp = LiquidCore.grid[x, y - 1];
			LiquidRef liquidDown = LiquidCore.grid[x, y + 1];
			LiquidRef liquidSelf = LiquidCore.grid[x, y];

			if (!Liquid.quickFall)
			{
				if (liquid.delay < 2)
				{
					++liquid.delay;
					return false;
				}

				liquid.delay = 0;
				if (liquidLeft.liquidsType() == liquidSelf.liquidsType())
				{
					Liquid.AddWater(liquid.x, liquid.y);
					//LiquidExtension.AddModdedLiquidAround(liquid.x, liquid.y);
				}

				if (liquidRight.liquidsType() == liquidSelf.liquidsType())
				{
					Liquid.AddWater(liquid.x, liquid.y);
					//LiquidExtension.AddModdedLiquidAround(liquid.x, liquid.y);
				}

				if (liquidUp.liquidsType() == liquidSelf.liquidsType())
				{
					Liquid.AddWater(liquid.x, liquid.y);
					//LiquidExtension.AddModdedLiquidAround(liquid.x, liquid.y);
				}

				if (liquidDown.liquidsType() == liquidSelf.liquidsType())
				{
					Liquid.AddWater(liquid.x, liquid.y);
					//LiquidExtension.AddModdedLiquidAround(liquid.x, liquid.y);
				}
			}

			return true;
		}*/

		/*public virtual void PreDrawValueSet(ref bool bg, ref int style, ref float Alpha) { }

		public virtual void PreDraw(TileBatch batch) { }

		public virtual void Draw(TileBatch batch) { }

		public virtual void PostDraw(TileBatch batch) { }

		public virtual void PlayerInteraction(Player target) { }

		public virtual void NPCInteraction(NPC target) { }

		public virtual void ItemInteraction(Item target) { }

		public virtual void LiquidInteraction(int x, int y, ModLiquid target) { }

		public virtual void LavaInteraction(int x, int y) { }

		public virtual void HoneyInteraction(int x, int y) { }
		
		 		public virtual void Update() { }
		
		 		public virtual bool CanKillWater(int x,int y)
		{
			return TileObjectData.CheckWaterDeath(Main.tile[x,y]);
		}*/
	}
}