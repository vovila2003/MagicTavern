namespace Tavern.ProductsAndIngredients
{
    public static class PlantProductNameProvider
    {
        private const string ProductName = "Product";
        
        public static string GetName(string plantName)
        {
            return $"{plantName}{ProductName}";
        }
    }
}