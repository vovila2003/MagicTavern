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

        void IDayBeginListener.OnDayBegin(int dayOfWeek)
        {
            _dayOfWeek = dayOfWeek;
            UpdateText();
        }

        void INewWeekListener.OnNewWeek(int weekNumber)
        {
            _week = weekNumber;
            UpdateText();
        }

        private void UpdateText()
        {
            Text.text = $"{_days[_dayOfWeek]}\n" +
                        $"неделя {_week + 1}";
        }
    }
}