namespace Tavern.Infrastructure
{
    public interface INewWeekListener : ITimeListener
    {
        void OnNewWeek(int weekNumber);
    }
}