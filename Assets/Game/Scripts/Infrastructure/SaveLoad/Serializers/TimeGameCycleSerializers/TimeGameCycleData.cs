using System;

namespace Tavern.Infrastructure
{
    [Serializable]
    public class TimeGameCycleData
    {
        public int CurrentDayOfWeek;
        public DayState CurrentDayState;
        public int CurrentWeek;
        public TimerData TimerData;
    }
}