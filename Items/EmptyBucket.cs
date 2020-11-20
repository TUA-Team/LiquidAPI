using LiquidAPI.LiquidMod;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace LiquidAPI.Items
{
    public class EmptyBucket : ModItem
    {
        public override string Texture => "LiquidAPI/ModBucket";

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Empty bucket");
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

        public override bool UseItem(Player player)
        {
            player.blockRange = 4;
            if (!player.noBuilding && player.position.X / 16f - (float)Player.tileRangeX - (float)player.inventory[player.selectedItem].tileBoost - (float)player.blockRange <= (float)Player.tileTargetX && (player.position.X + (float)player.width) / 16f + (float)Player.tileRangeX + (float)player.inventory[player.selectedItem].tileBoost - 1f + (float)player.blockRange >= (float)Player.tileTargetX && player.position.Y / 16f - (float)Player.tileRangeY - (float)player.inventory[player.selectedItem].tileBoost - (float)player.blockRange <= (float)Player.tileTargetY && (player.position.Y + (float)player.height) / 16f + (float)Player.tileRangeY + (float)player.inventory[player.selectedItem].tileBoost - 2f + (float)player.blockRange >= (float)Player.tileTargetY)
            {
                LiquidRef liqRef = LiquidWorld.grid[Player.tileTargetX, Player.tileTargetY];
                if (liqRef.LiquidType == null || liqRef.Amount <= 200)
                {
                    return false;
                }

                ModLiquid liquid = liqRef.LiquidType;
                player.QuickSpawnItem(ModBucket.CreateBucketItem(liquid));
            }

            return base.UseItem(player);
        }
    }
}
