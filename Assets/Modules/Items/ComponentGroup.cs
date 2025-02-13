namespace Modules.Items
{
    public abstract class ComponentGroup : IItemComponent
    {
        public abstract IItemComponent Clone();
    }
}