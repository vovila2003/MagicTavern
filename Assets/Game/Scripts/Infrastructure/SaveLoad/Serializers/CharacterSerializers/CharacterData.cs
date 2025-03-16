using System;
using System.Collections.Generic;

namespace Tavern.Infrastructure
{
    [Serializable]
    public class CharacterData
    {
        public int Hp;
        public List<CharacterStateData> State;
        public float[] Position;
        public float[] Rotation;
        public float Speed;
    }
    
    [Serializable]
    public class CharacterStateData
    {
        public string EffectName;
    }
}