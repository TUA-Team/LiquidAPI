namespace LiquidAPI
{
    public struct LiquidDust
    {
        //SpawnLiquidDust(NPC self, int dustID, int amountOfDust, float dustVelocityX, float dustVelocityY, float dustScale, int dustAlpha = 100, bool noGravity = true)
        public int dustID;
        public int amountOfDust;
        public float dustVelocityX;
        public float dustVelocityY;
        public float dustScale;
        public int dustAlpha;
        public bool noGravity;

        public LiquidDust(int dustID, int amountOfDust, float dustVelocityX, float dustVelocityY, float dustScale, int dustAlpha = 100, bool noGravity = false)
        {
            this.dustID = dustID;
            this.amountOfDust = amountOfDust;
            this.dustVelocityX = dustVelocityX;
            this.dustVelocityY = dustVelocityY;
            this.dustScale = dustScale;
            this.dustAlpha = dustAlpha;
            this.noGravity = noGravity;
        }
    }
}
