using Tavern.Infrastructure;
using UnityEngine;

namespace Tavern.TestAndDebug
{
    public class LightController :
        MonoBehaviour,
        IDayBeginListener,
        INightBeginListener
    {
        [SerializeField]
        private Light Light;

        void INightBeginListener.OnNightBegin() => SetDark();

        void INightBeginListener.SetNight() => SetDark();

        void IDayBeginListener.OnDayBegin(int _) => SetBright();

        void IDayBeginListener.SetDay(int dayOfWeek) => SetBright();

        private void SetDark()
        {
            Light.intensity = 0.4f;
        }

        private void SetBright()
        {
            Light.intensity = 1;
        }
    }
}