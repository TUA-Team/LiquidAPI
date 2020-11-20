using LiquidAPI.Items;
using LiquidAPI.LiquidMod;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Graphics;
using Terraria.ModLoader;
using Terraria.ObjectData;
// ReSharper disable InconsistentNaming

namespace LiquidAPI
{
    public abstract class ModLiquid
    {
        public bool gravity = true;
        public int customDelay = 1; //Default value, aka 

        public int Type { get; internal set; }

        public Mod Mod { get; internal set; }

        public ModTranslation DisplayName { get; internal set; }

        public string Name { get; internal set; }

        public virtual Texture2D Texture => ModContent.GetTexture(this.GetType().FullName.Replace(".", "/") + "Fancy");
        public virtual Texture2D OldTexture => ModContent.GetTexture(this.GetType().FullName.Replace(".", "/"));

        public virtual Color LiquidColor => Color.White;
        public virtual int dustAmountOnEnter => 10;

        public byte WaterfallLength = 10;
        public float DefaultOpacity = 1f;
        public byte WaveMaskStrength = 0;
        public byte ViscosityMask = 0;
        internal LiquidDust _liquidDust;

        public LiquidDust LiquidDust
        {
            get { return _liquidDust; }
            set { this._liquidDust = value; }
        }

        protected ModLiquid()
        {
            Name = this.GetType().Name;
        }

        /// <summary>
        /// Takes an array that contain legacy style texture and 1.3.4+ texture style
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public virtual bool Autoload(ref string name, ref string texture, ref string fancyTexture) => true;

        public virtual void SetDefaults() { }

        public virtual void Update() { }

        //Normally trigger if gravity is at false
        /*public virtual bool CustomPhysic(int x, int y)
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

        public virtual void PreDrawValueSet(ref bool bg, ref int style, ref float Alpha) { }

        public virtual void PreDraw(TileBatch batch) { }

        public virtual void Draw(TileBatch batch) { }

        public virtual void PostDraw(TileBatch batch) { }

        public virtual void PlayerInteraction(Player target) { }

        public virtual void NPCInteraction(NPC target) { }

        public virtual void ItemInteraction(Item target) { }

        public virtual bool LiquidInteraction(int x, int y, ModLiquid target)
        {
            return false;
        }

        public virtual bool LiquidInteraction(LiquidRef liquidUp, LiquidRef liquidDown, LiquidRef liquidLeft, LiquidRef liquidRight, int x, int y)
        {
            return false;
        }

        public virtual bool CanKillTile(int x, int y)
        {
            return TileObjectData.CheckWaterDeath(Main.tile[x, y]);
        }

        internal void AddModBucket()
        {
            Mod.AddItem("Bucket" + Name, new ModBucket(this));
        }

        protected void RegisterMapColorCode(Color color)
        {
            LiquidRegistry.mapColorLookup[Type] = color;
        }
    }
}
