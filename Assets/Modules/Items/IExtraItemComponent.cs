namespace Modules.Items
{
    public interface IExtraItemComponent
    {
        IExtraItemComponent Clone();
        string ComponentName { get; }
    }
}