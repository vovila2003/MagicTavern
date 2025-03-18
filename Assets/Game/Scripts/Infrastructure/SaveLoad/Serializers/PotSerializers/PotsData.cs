using System;
using System.Collections.Generic;
using Modules.Gardening;

namespace Tavern.Infrastructure
{
    [Serializable]
    public class PotsData
    {
        public List<PotData> Pots;

        public PotsData(int count)
        {
            Pots = new List<PotData>(count);
        }
    }
    
    [Serializable]
    public class PotData
    {
        public bool IsSeeded;
        public float[] Position;
        public float[] Rotation;
        public string SeedConfigName;
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
        public int Value;
        public HarvestState HarvestState;
        public HarvestAge HarvestAge;
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