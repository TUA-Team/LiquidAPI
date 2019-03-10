using LiquidAPI.Swap;
using Terraria.ModLoader;

namespace LiquidAPI.LiquidMod
{
    class LiquidPlayer : ModPlayer
    {
        public override void PostUpdate()
        {
            bool[] liquidCollision = CollisionSwap.ModdedWetCollision(player.Center, player.width, player.height);
            for (byte i = 0; i < LiquidRegistry.liquidList.Capacity; i++)
            {
                if (liquidCollision[i])
                {
                    LiquidRegistry.PlayerInteraction(i, player);
                }
            }
        }
    }
}
