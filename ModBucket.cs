using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.ID;
using Terraria.ModLoader;

namespace LiquidAPI
{
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

		/*public override bool UseItem(Player player)
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
		}*/

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

		public sealed override string Texture => "LiquidAPI/Assets/ModBucket";


		private static Texture2D BaseLiquidTexture { get; } = (Texture2D)LiquidAPI.Instance.GetTexture("Assets/Liquid");

		public static ModBucket Empty { get; } = new ModBucket("EmptyBucket");


		public override void Unload()
		{
			BaseLiquidTexture?.Dispose();
		}
	}
}
