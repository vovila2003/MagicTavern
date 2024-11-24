using UnityEngine;

namespace Modules.Gardening
{
    public class HarvestSickness
    {
        private readonly int _penalty;

        public int Probability { get; private set; }

        public HarvestSickness(Plant plant, int reducing)
        {
            SetNewProbabilityValue(plant.SicknessProbability - reducing);
            _penalty = plant.SicknessPenalty;
        }

        public bool IsSick()
        {
            float random = Random.Range(0, 101);
            float start = Random.Range(0, 101 - Probability);

            return random > start && random < start + Probability;
        }

        public void Penalty() => SetNewProbabilityValue(Probability + _penalty);

        public void DecreaseSicknessProbability(int value) => SetNewProbabilityValue(Probability - value);

        private void SetNewProbabilityValue(int value) => Probability = Mathf.Clamp(value, 0, 100);
    }
}