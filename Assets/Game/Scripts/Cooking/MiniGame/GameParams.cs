namespace Tavern.Cooking.MiniGame
{
    public struct GameParams
    {
        public Regions Regions;
        public int TimeInSeconds;
    }
    
    public struct Regions
    {
        public float RedYellow;
        public float YellowGreen;
        public float GreenYellow;
        public float YellowRed;
    }
    
    public enum Result
    {
        Red,
        Yellow,
        Green
    }
}