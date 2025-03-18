namespace Tavern.Infrastructure
{
    public interface INightBeginListener : ITimeListener
    {
        void OnNightBegin();
        void SetNight();
    }
}