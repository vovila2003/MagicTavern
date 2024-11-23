using System;
using UnityEngine;

namespace Tavern.Gardening.Medicine
{
    [Serializable]
    public class ComponentHarvestSickProbabilityReducing : ICloneable
    {
        [SerializeField] 
        private int SickProbabilityReducing;

        public int Reducing
        {
            get => SickProbabilityReducing;
            private set => SickProbabilityReducing = value;
        }

        public ComponentHarvestSickProbabilityReducing(int reducing = 0)
        {
            SickProbabilityReducing = reducing;
        }

        object ICloneable.Clone() => new ComponentHarvestSickProbabilityReducing(SickProbabilityReducing);
    }
}