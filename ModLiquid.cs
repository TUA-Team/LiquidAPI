using System;
using LiquidAPI.LiquidMod;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Graphics;
using Terraria.ID;
using Terraria.ModLoader;
// ReSharper disable InconsistentNaming

namespace LiquidAPI
{
	public class ModLiquid
	{
		public bool gravity = true;
		public int customDelay = 1; //Default value, aka 

	    internal int liquidIndex;

		/// <summary>
		/// Take an array that contain legacy style texture and 1.3.4+ texture style
		/// </summary>
		/// <param name="name"></param>
		/// <returns></returns>
		public virtual bool Autoload(ref string name)
		{
			return true;
		}

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

		public virtual float SetLiquidOpacity()
		{
			return 1;
		}

		public virtual void PreDrawValueSet(ref bool bg, ref int style, ref float Alpha) { }

		public virtual void PreDraw(TileBatch batch) { }

		public virtual void Draw(TileBatch batch) { }

		public virtual void PostDraw(TileBatch batch) { }

		public virtual void PlayerInteraction(Player target) { }

		public virtual void NPCInteraction(NPC target) { }

		public virtual void ItemInteraction(Item target) { }

		public virtual void LiquidInteraction(int x, int y, ModLiquid target) { }

		public virtual void LavaInteraction(int x, int y) { }

		public virtual void HoneyInteraction(int x, int y) { }

		internal void AddModBucket()
		{
			ModBucket bucket = new ModBucket(liquidIndex, liquidColor, name);
			mod.AddItem(bucket.name, bucket.Clone());
		}

	    public virtual Color liquidColor
	    {
	        get { return Color.White; }
	    }

	    public Mod mod { get; internal set; }

	    public virtual string name { get; }

	    public Liquid liquid { get; } = new Liquid();

	    public virtual Texture2D texture
	    {
	        get { return ModContent.GetTexture(this.GetType().FullName.Replace(".", "/")); }
	    }

	    public virtual Texture2D oldTexture
	    {
	        get { return ModContent.GetTexture(this.GetType().FullName.Replace(".", "/")); }
	    }
    }

	public class ModBucket : ModItem
	{
		private readonly Color liquidColor;
		internal string name;

		public ModBucket() { }

        public override bool CloneNewInstances => true;

        public override bool Autoload(ref string name) => false;

		public override void SetDefaults()
		{
			item.width = 24;
			item.height = 22;
			item.maxStack = 99;
			item.useStyle = ItemUseStyleID.SwingThrow;
			item.useTime = 100;
			item.useAnimation = 1;
			item.consumable = true;
		}

        public ModBucket(ModLiquid liquid)
        {
            this.liquid = liquid;
        }



        public override void SetStaticDefaults()
        {
            if (liquid == null)
                return;
            DisplayName.SetDefault((liquid.DisplayName.GetDefault() ?? "Empty") + " Bucket");
        }

        public override void SetDefaults()
        {
            item.width = 24;
            item.height = 22;
            item.maxStack = 99;
            item.useStyle = ItemUseStyleID.SwingThrow;
            item.useTime = 100;
            item.useAnimation = 1;
            item.consumable = true;
        }

				WorldGen.SquareTileFrame(Player.tileTargetX, Player.tileTargetY, true);
				//player.PutModItemInInventory(newBucket);
				//item.stack--;
			}

			return true;
		}

		public override void PostDrawInInventory(SpriteBatch spriteBatch, Vector2 position, Rectangle frame, Color drawColor, Color itemColor, Vector2 origin, float scale)
		{
			item.useAnimation = 45;
			item.useTime = 45;
			item.useStyle = 4;

			if (liquidType == -1) return;

			Texture2D liquidTexture = LiquidAPI.instance.GetTexture("Texture/Bucket/liquid");
			spriteBatch.Draw(liquidTexture, position, null, liquidColor, 0.0f, origin, new Vector2(scale), SpriteEffects.None, 0);
		}

		public override void PostDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, float rotation, float scale, int whoAmI)
		{
			if (liquidType == -1) return;

			Texture2D liquidTexture = LiquidAPI.instance.GetTexture("Texture/Bucket/liquid");
			spriteBatch.Draw(liquidTexture, item.position, liquidColor);
		}

		public override bool OnPickup(Player player)
		{
			return base.OnPickup(player);
		}

	    private int liquidType { get; set; }

	    public override string Texture => "LiquidAPI/ModBucket";
	    public override bool CloneNewInstances => true;
    }
}