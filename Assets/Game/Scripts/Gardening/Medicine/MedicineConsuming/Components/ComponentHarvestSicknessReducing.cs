using System;
using UnityEngine;

namespace Tavern.Gardening.Medicine
{
    [Serializable]
    public class ComponentHarvestSicknessReducing : ICloneable
    {
        [SerializeField, Range(0, 100)] 
        private int SicknessReducingInPercents;

        public int Reducing
        {
            get => SicknessReducingInPercents;
            private set => SicknessReducingInPercents = value;
        }

        public ComponentHarvestSicknessReducing()
        {
        }

        private ComponentHarvestSicknessReducing(int reducing = 0)
        {
            SicknessReducingInPercents = reducing;
        }

        object ICloneable.Clone() => new ComponentHarvestSicknessReducing(SicknessReducingInPercents);
    }
}