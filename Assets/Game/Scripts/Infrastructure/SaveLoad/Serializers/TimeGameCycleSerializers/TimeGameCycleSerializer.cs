using System.Collections.Generic;
using JetBrains.Annotations;
using Modules.SaveLoad;
using Tavern.Utils;

namespace Tavern.Infrastructure
{
    [UsedImplicitly]
    public class TimeGameCycleSerializer : IGameSerializer
    {
        private const string TimeGameCycle = "TimeGameCycle";
        
        private readonly TimeGameCycle _timeGameCycle;
        private readonly TimerSerializer _timerSerializer = new();

        public TimeGameCycleSerializer(TimeGameCycle timeGameCycle)
        {
            _timeGameCycle = timeGameCycle;
        }

        public void Serialize(IDictionary<string, string> saveState)
        {
            var data = new TimeGameCycleData
            {
                CurrentWeek = _timeGameCycle.CurrentWeek,
                CurrentDayOfWeek = _timeGameCycle.CurrentDayOfWeek,
                CurrentDayState = _timeGameCycle.CurrentDayState,
                TimerData = _timerSerializer.Serialize(_timeGameCycle.Timer)
            };

            saveState[TimeGameCycle] = Serializer.SerializeObject(data);
        }

        public void Deserialize(IDictionary<string, string> loadState)
        {
            if (!loadState.TryGetValue(TimeGameCycle, out string json)) return;
    
            (TimeGameCycleData info, bool ok) = Serializer.DeserializeObject<TimeGameCycleData>(json);
            if (!ok) return;
            
            //order is important
            _timeGameCycle.SetCurrentWeek(info.CurrentWeek);
            _timeGameCycle.SetCurrentDayOfWeek(info.CurrentDayOfWeek);
            _timeGameCycle.SetCurrentDayState(info.CurrentDayState);

            _timerSerializer.Deserialize(_timeGameCycle.Timer, info.TimerData);
        }
    }
}