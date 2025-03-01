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

        void INightBeginListener.OnNightBegin()
        {
            Light.intensity = 0.4f;
        }

        void IDayBeginListener.OnDayBegin(int dayOfWeek)
        {
            Light.intensity = 1;
        }
    }
}