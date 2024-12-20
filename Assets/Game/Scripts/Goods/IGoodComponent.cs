namespace Tavern.Goods
{
    public interface IGoodComponent
    {
        string Name { get; }
        int Count { get; }
        GoodsMetadata GoodsMetadata { get; }
    }
}