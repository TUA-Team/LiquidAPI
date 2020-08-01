using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LiquidAPI.Items;
using LiquidAPI.LiquidMod;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace LiquidAPI.Globals
{
    class GlobalLiquidItem : GlobalItem
    {
        private static int[] LiquidBucketID = {ItemID.WaterBucket, ItemID.LavaBucket, ItemID.HoneyBucket};

        public override bool CanUseItem(Item item, Player player)
        {
            if (LiquidBucketID.Contains(item.type))
            {
                return false;
            }
            if (item.type == ItemID.EmptyBucket)
            {
                
                if (!player.noBuilding && player.position.X / 16f - (float)Player.tileRangeX - (float)player.inventory[player.selectedItem].tileBoost - (float)player.blockRange <= (float)Player.tileTargetX && (player.position.X + (float)player.width) / 16f + (float)Player.tileRangeX + (float)player.inventory[player.selectedItem].tileBoost - 1f + (float)player.blockRange >= (float)Player.tileTargetX && player.position.Y / 16f - (float)Player.tileRangeY - (float)player.inventory[player.selectedItem].tileBoost - (float)player.blockRange <= (float)Player.tileTargetY && (player.position.Y + (float)player.height) / 16f + (float)Player.tileRangeY + (float)player.inventory[player.selectedItem].tileBoost - 2f + (float)player.blockRange >= (float)Player.tileTargetY)
                {
                    LiquidRef liqRef = LiquidWorld.grid[Player.tileTargetX, Player.tileTargetY];
                    if (liqRef.Type == null || liqRef.Amount < 255)
                    {
                        return false;
                    }

                    ModLiquid liquid = liqRef.Type;
                    liqRef.Amount = 0;
                    player.QuickSpawnItem(ModBucket.CreateBucketItem(liquid));
                    item.stack--;
                }
                return false;
            }
            return base.CanUseItem(item, player);
        }

        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {
            base.ModifyTooltips(item, tooltips);
        }
    }
}
