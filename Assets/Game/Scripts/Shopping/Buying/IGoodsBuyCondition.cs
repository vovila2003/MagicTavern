namespace Tavern.Buying
{
    public interface IGoodsBuyCondition
    {
        bool CanBuy(Modules.Shopping.Goods product);
    }
}