using System;
using Modules.Gardening;
using Modules.Timers;

namespace Tavern.Infrastructure
{
    [Serializable]
    public class PotData
    {
        public bool IsSeeded;
        public float[] Position;
        public float[] Rotation;
        public SeedbedData SeedbedData;
    }
    
    [Serializable]
    public class SeedbedData
    {
        public bool IsBoosted;
        public bool IsSickReduced;
        public bool IsAccelerated;
        public int SeedInHarvestProbability;
        public HarvestData HarvestData;
    }

    [Serializable]
    public class HarvestData
    {
        public bool IsPaused;
        public bool IsReadyAfterWatering;
        public bool IsPenalized;
        public bool IsSick;
        public bool IsWaterRequired;
        public int ResultHarvestAmount;
        public int SickProbability;
        public int Value;
        public HarvestState HarvestState;
        public HarvestAge HarvestAge;
        public string PlantConfigName;
        public TimerData GrowthTimerData;
        public HarvestWateringData HarvestWateringData;
        public HarvestSicknessData HarvestSicknessData;
    }

    [Serializable]
    public class HarvestWateringData
    {
        public float BaseDryingTimerDuration;
        public TimerData WateringTimerData;
        public TimerData DryingTimerData;
    }

    [Serializable]
    public class HarvestSicknessData
    {
        public int Probability;
    }
}