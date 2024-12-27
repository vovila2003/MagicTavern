namespace Tavern.Gardening
{
    public static class ProductNameProvider
    {
        private const string ProductName = "Product";
        
        public static string GetName(string plantName)
        {
            return $"{plantName}{ProductName}";
        }
    }
}