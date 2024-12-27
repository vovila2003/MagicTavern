namespace Modules.Shopping
{
    public interface IGoodComponent
    {
        string Name { get; }
        GoodsMetadata GoodsMetadata { get; }
    }
}