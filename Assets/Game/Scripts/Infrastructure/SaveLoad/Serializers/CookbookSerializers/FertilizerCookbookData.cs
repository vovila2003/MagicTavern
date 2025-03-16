using System;

namespace Tavern.Infrastructure
{
    [Serializable]
    public class FertilizerCookbookData : DishAutoCookbookData
    {
        public FertilizerCookbookData(int count) : base(count)
        {
        }
    }
}