using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Graphics;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;
using LiquidAPI.LiquidMod;
// ReSharper disable InconsistentNaming

namespace LiquidAPI
{
	public abstract class ModLiquid
	{
		public bool gravity = true;
		public int customDelay = 1; //Default value, aka 

		internal int Type;

		public Mod Mod{get;internal set;}

		public ModTranslation DisplayName{get;internal set;}

		public string Name{get;internal set;}

		public virtual Texture2D Texture=>(Texture2D)ModContent.GetTexture(GetType().FullName.Replace(".", "/"));
		public virtual Texture2D OldTexture=>(Texture2D)ModContent.GetTexture(GetType().FullName.Replace(".", "/"));

		public Color LiquidColor=Color.White;

		public byte WaterfallLength=10;
		public float DefaultOpacity=1f;
		public byte WaveMaskStrength=0;
		public byte ViscosityMask=0;

		/// <summary>
		/// Takes an array that contain legacy style texture and 1.3.4+ texture style
		/// </summary>
		/// <param name="name"></param>
		/// <returns></returns>
		public virtual bool Autoload(ref string name,ref string texture)=>true;

		public virtual void SetDefaults(){}

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

		public virtual void LiquidInteraction(int x, int y, ModLiquid target) { }

		public virtual void LavaInteraction(int x, int y) { }

		public virtual void HoneyInteraction(int x, int y) { }

		public virtual bool CanKillTile(int x,int y)
		{
			return TileObjectData.CheckWaterDeath(Main.tile[x,y]);
		}

		internal void AddModBucket()
		{
			Mod.AddContent(new ModBucket(this,"Bucket"+Name));
		}
	}

	[Autoload(false)]
	public class ModBucket : ModItem
	{
		private readonly ModLiquid _liquid;

		private readonly string _name;
        public sealed override string Name => _name;

        public sealed override string Texture => "LiquidAPI/ModBucket";
		
		public ModBucket(string name)
        {
			_name = name;
        }

		public ModBucket(ModLiquid liquid,string name) : this(name)
		{
			_liquid = liquid;
			DisplayName.SetDefault((liquid?.DisplayName.GetDefault()??"Empty")+" Bucket");
		}

		public override void SetDefaults()
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
				Texture2D liquidTexture = (Texture2D)Mod.GetTexture("Texture/Bucket/liquid");
				spriteBatch.Draw(liquidTexture, position, null, _liquid.LiquidColor, 0f, origin, new Vector2(scale), SpriteEffects.None, 0);
			}
		}

		public override void PostDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, float rotation, float scale, int whoAmI)
		{
			if (_liquid != null)
			{
				Texture2D liquidTexture = (Texture2D)Mod.GetTexture("Texture/Bucket/liquid");
				spriteBatch.Draw(liquidTexture, item.position, _liquid.LiquidColor);
			}
		}
	}
}