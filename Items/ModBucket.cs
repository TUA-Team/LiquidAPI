using LiquidAPI.Caches;
using LiquidAPI.LiquidMod;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace LiquidAPI.Items
{
    public class ModBucket : ModItem
    {
        private readonly ModLiquid liquid;

        public override string Texture => "LiquidAPI/ModBucket";

        public override bool CloneNewInstances => true;

        public override bool Autoload(ref string name) => false;

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

        public ModBucket(ModLiquid liquid)
        {
            this.liquid = liquid;
        }

        internal ModBucket()
        {

        }
        public override bool UseItem(Player player)
        {
            //Implement liquid picking logic here
            if (this.liquid == null)
            {
                return false;
            }
            LiquidRef liquid = LiquidWorld.grid[Player.tileTargetX, Player.tileTargetY];

            if (!liquid.HasLiquid || liquid.LiquidType == this.liquid)
            {

                //Item newItem = Main.item[Item.NewItem(player.position, item.type)];
                //ModBucket newBucket = newItem.modItem as ModBucket;

                liquid.LiquidType = this.liquid;
                liquid.Amount = 255;

                WorldGen.SquareTileFrame(Player.tileTargetX, Player.tileTargetY, true);

            }

            return true;
        }

        public override void PostDrawInInventory(SpriteBatch spriteBatch, Vector2 position, Rectangle frame, Color drawColor, Color itemColor, Vector2 origin, float scale)
        {
            item.useAnimation = 45;
            item.useTime = 45;
            item.useStyle = 4;

            if (liquid != null)
            {
                Texture2D liquidTexture = LiquidAPI.Instance.GetTexture("Texture/Bucket/liquid");
                spriteBatch.Draw(liquidTexture, position, null, liquid.LiquidColor, 0f, origin, new Vector2(scale), SpriteEffects.None, 0);
            }
        }

        public override void PostDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, float rotation, float scale, int whoAmI)
        {
            if (liquid != null)
            {
                Texture2D liquidTexture = LiquidAPI.Instance.GetTexture("Texture/Bucket/liquid");
                spriteBatch.Draw(liquidTexture, item.position, liquid.LiquidColor);
            }
        }

        //This is a static method for helping at creating mod bucket in game
        public static Item CreateBucketItem(ModLiquid liquid)
        {
            List<ModItem> itemList = (List<ModItem>)ReflectionCaches.fieldCache[typeof(ItemLoader)]["items"].GetValue(null);
            ModBucket modBucket = (ModBucket)itemList.Single(i => i is ModBucket i2 && i2.liquid.Type == liquid.Type);

            Item bucket = new Item();
            bucket.SetDefaults(modBucket.item.type);
            return bucket;
        }
    }
}
