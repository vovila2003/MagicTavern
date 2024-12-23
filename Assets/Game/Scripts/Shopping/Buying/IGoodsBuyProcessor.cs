namespace Tavern.Buying
{
    public interface IGoodsBuyProcessor
    {
        void ProcessBuy(Modules.Shopping.Goods goods);
    }
}