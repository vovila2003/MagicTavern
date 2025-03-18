using JetBrains.Annotations;

namespace Tavern.Infrastructure
{
    [UsedImplicitly]
    public class TimeGameCycleSerializer : GameSerializer<TimeGameCycleData>
    {
        private readonly TimeGameCycle _timeGameCycle;
        private readonly TimerSerializer _timerSerializer = new();

        public TimeGameCycleSerializer(TimeGameCycle timeGameCycle)
        {
            _timeGameCycle = timeGameCycle;
        }

        protected override TimeGameCycleData Serialize()
        {
            return new TimeGameCycleData
            {
                CurrentWeek = _timeGameCycle.CurrentWeek,
                CurrentDayOfWeek = _timeGameCycle.CurrentDayOfWeek,
                CurrentDayState = _timeGameCycle.CurrentDayState,
                TimerData = _timerSerializer.Serialize(_timeGameCycle.Timer)
            };
        }

        protected override void Deserialize(TimeGameCycleData data)
        {
            _timeGameCycle.SetCurrentWeek(data.CurrentWeek);
            _timeGameCycle.SetCurrentDayOfWeek(data.CurrentDayOfWeek);
            _timeGameCycle.SetCurrentDayState(data.CurrentDayState);

            _timerSerializer.Deserialize(_timeGameCycle.Timer, data.TimerData);
        }
    }
}