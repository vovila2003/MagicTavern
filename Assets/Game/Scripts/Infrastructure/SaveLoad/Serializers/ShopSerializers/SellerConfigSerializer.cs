using Tavern.Shopping;

namespace Tavern.Infrastructure
{
    public class SellerConfigSerializer
    {
        private readonly SellerCatalog _sellerCatalog;

        public SellerConfigSerializer(SellerCatalog sellerCatalog)
        {
            _sellerCatalog = sellerCatalog;
        }

        public string Serialize(SellerConfig sellerConfig)
        {
            return sellerConfig.Name;
        }
    }
}