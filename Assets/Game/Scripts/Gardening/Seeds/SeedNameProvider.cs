namespace Tavern.Gardening
{
    public static class SeedNameProvider
    {
        private const string SeedName = "Seed";
        
        public static string GetName(string plantName)
        {
            return $"{plantName}{SeedName}";
        }
    }
}