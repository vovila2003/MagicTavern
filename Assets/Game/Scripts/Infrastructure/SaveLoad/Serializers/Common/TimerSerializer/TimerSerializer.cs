using Modules.Timers;

namespace Tavern.Infrastructure
{
    public class TimerSerializer
    {
        public TimerData Serialize(Timer timer) =>
            new()
            {
                Duration = timer.Duration,
                Progress = timer.Progress,
                CurrentState = timer.CurrentState
            };

        public void Deserialize(Timer timer, TimerData data)
        {
            timer.Duration = data.Duration;
            timer.SetProgress(data.Progress);
            timer.SetState(data.CurrentState);
        }
    }
}