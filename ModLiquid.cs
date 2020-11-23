using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using LiquidAPI.LiquidMod;
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
			LiquidLoader.Add(this);
        }


        public virtual void SetStaticDefaults()
        {
        }

		public virtual void SetDefaults()
		{
		}

		protected virtual ModBucket CreateBucket() => new ModBucket(this, $"{Name}Bucket");


		/// <summary>
		/// The ID of this liquid
		/// </summary>
		public int Type { get; internal set; }

		public ModTranslation DisplayName { get; internal set; }


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

	[Autoload(false)]
	public class ModBucket : ModItem
	{
		private readonly ModLiquid _liquid;
		private readonly string _name;

		private ModBucket(string name)
        {
			_name = name;
        }

		public ModBucket(ModLiquid liquid, string name) : this(name)
		{
			// TODO: Does this work with json localization?
			_liquid = liquid;
			DisplayName.SetDefault($"{liquid.DisplayName.GetDefault() ?? "Empty"} Bucket");
		}


		public sealed override void SetDefaults()
		{
			item.width = 24;
			item.height = 22;
			item.maxStack = 99;
			item.useStyle = ItemUseStyleID.Swing;
			item.useTime = 100;
			item.useAnimation = 1;
			item.consumable = true;
		}

		public override bool UseItem(Player player)
		{
			if(_liquid==null){return false;}
			LiquidRef tile = LiquidWorld.grid[Player.tileTargetX, Player.tileTargetY];

			if (!tile.HasLiquid || tile.Type == _liquid)
			{
				//Item newItem = Main.item[Item.NewItem(player.position, item.type)];
				//ModBucket newBucket = newItem.modItem as ModBucket;
				
				tile.Type = _liquid;
				tile.Amount = 255;

				WorldGen.SquareTileFrame(Player.tileTargetX,Player.tileTargetY, true);
				//player.PutModItemInInventory(newBucket);
				//item.stack--;
			}

			return true;
		}

		public override void PostDrawInInventory(SpriteBatch spriteBatch, Vector2 position, Rectangle frame, Color drawColor, Color itemColor, Vector2 origin, float scale)
		{
			item.useAnimation = 45;
			item.useTime = 45;
			item.useStyle = ItemUseStyleID.HoldUp;

			if (_liquid != null)
			{
				spriteBatch.Draw(BaseLiquidTexture, position, null, _liquid.liquidColor, 0f, origin, scale, SpriteEffects.None, 0);
			}
		}

		public override void PostDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, float rotation, float scale, int whoAmI)
		{
			if (_liquid != null)
			{
				spriteBatch.Draw(BaseLiquidTexture, item.position, _liquid.liquidColor);
			}
		}


		public sealed override string Name => _name;

		public sealed override string Texture => "LiquidAPI/ModBucket";


		private static Texture2D BaseLiquidTexture { get; } = (Texture2D)LiquidAPI.instance.GetTexture("Assets/Liquid");

		public static ModBucket Empty { get; } = new ModBucket("EmptyBucket");


        public override void Unload()
        {
			BaseLiquidTexture?.Dispose();
        }
    }
}