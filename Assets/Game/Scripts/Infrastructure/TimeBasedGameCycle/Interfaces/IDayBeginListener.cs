namespace Tavern.Infrastructure
{
    public interface IDayBeginListener : ITimeListener
    {
        void OnDayBegin(int dayOfWeek);
    }
}