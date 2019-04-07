using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace LiquidAPI.Test
{
    class PlutonicWaste : ModLiquid
    {
        public override string name => "Liquid waste";

        public override Color liquidColor => Color.GreenYellow;

        public override bool Autoload(ref string name)
        {
            
            return base.Autoload(ref name);
        }

        public override void PreDrawValueSet(ref bool bg, ref int style, ref float Alpha)
        {
            style = 12;
            Alpha = 0.2f;
        }

        public override float SetLiquidOpacity()
        {
            return 0.5f;
        }

        public override void PlayerInteraction(Player target)
        {
            Main.NewText("This is liquid waste");
        }

        public override void NpcInteraction(NPC target)
        {
            if (target.type == NPCID.GreenSlime || target.type == NPCID.BlueSlime || target.type == NPCID.PurpleSlime)
            {
                Vector2 position = target.Center;
                target.active = false;
                NPC.NewNPC((int) position.X, (int) position.Y,
                    ModLoader.GetMod("TUA").NPCType("MutatedSludge"));
            }
        }

        
    }
}
