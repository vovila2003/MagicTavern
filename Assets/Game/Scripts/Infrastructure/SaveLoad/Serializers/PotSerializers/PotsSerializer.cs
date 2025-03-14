using System.Collections.Generic;
using JetBrains.Annotations;
using Modules.SaveLoad;
using Tavern.Gardening;
using Tavern.Utils;
using UnityEngine;

namespace Tavern.Infrastructure
{
    [UsedImplicitly]
    public class PotsSerializer : IGameSerializer
    {
        private const string Pots = "Pots";
        
        private readonly PotFactory _factory;
        private readonly SeedbedSerializer _seedbedSerializer;
        
        public PotsSerializer(PotFactory factory)
        {
            _factory = factory;
            _seedbedSerializer = new SeedbedSerializer();
        }

        public void Serialize(IDictionary<string, string> saveState)
        {
            var pots = new List<PotData>(_factory.Pots.Count);
            foreach (Pot pot in _factory.Pots)
            {
                Transform transform = pot.transform;
                var potData = new PotData
                {
                    Position = transform.position.ToFloat3(),
                    Rotation = transform.rotation.ToFloat4(),
                    SeedbedData = _seedbedSerializer.Serialize(pot.Seedbed) 
                };
                pots.Add(potData);
            }
    
            saveState[Pots] = Serializer.SerializeObject(pots);
        }
        
        public void Deserialize(IDictionary<string, string> loadState)
        {
            //TODO
        }
    }
}