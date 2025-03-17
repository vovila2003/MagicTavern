using Tavern.Infrastructure;
using TMPro;
using UnityEngine;

namespace Tavern.TestAndDebug
{
    public class WeekView : 
        MonoBehaviour,
        IDayBeginListener,
        INewWeekListener
    {
        [SerializeField]
        private TMP_Text Text;

        private readonly string[] _days = { "пн", "вт", "ср", "чт", "пт", "сб", "вс" };
        
        private int _dayOfWeek;
        private int _week;

        void IDayBeginListener.OnDayBegin(int dayOfWeek) => SetDay(dayOfWeek);

        void IDayBeginListener.SetDay(int dayOfWeek) => SetDay(dayOfWeek);

        void INewWeekListener.OnNewWeek(int weekNumber) => SetWeek(weekNumber);

        void INewWeekListener.SetWeek(int weekNumber) => SetWeek(weekNumber);

        private void SetWeek(int weekNumber)
        {
            _week = weekNumber;
            UpdateText();
        }

        private void SetDay(int dayOfWeek)
        {
            _dayOfWeek = dayOfWeek;
            UpdateText();
        }

        private void UpdateText() =>
            Text.text = $"{_days[_dayOfWeek]}\n" +
                $"неделя {_week + 1}";
    }
}