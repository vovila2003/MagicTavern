using System.Collections.Generic;
using JetBrains.Annotations;
using Modules.GameCycle;
using Modules.GameCycle.Interfaces;
using Modules.Items;
using Modules.SaveLoad;
using Tavern.Gardening;
using Tavern.Settings;
using Tavern.Utils;
using UnityEngine;

namespace Tavern.Infrastructure
{
    [UsedImplicitly]
    public class PotsSerializer : IGameSerializer
    {
        private const string Pots = "Pots";
        
        private readonly PotFactory _factory;
        private readonly GameCycle _gameCycle;
        private readonly PotsController _controller;
        private readonly SeedbedSerializer _seedbedSerializer = new ();
        private readonly SeedCatalog _seedCatalog;

        public PotsSerializer(
            PotFactory factory, 
            GameSettings settings, 
            GameCycle gameCycle, 
            PotsController controller)
        {
            _factory = factory;
            _gameCycle = gameCycle;
            _controller = controller;
            _seedCatalog = settings.GardeningSettings.SeedCatalog;
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
                    IsSeeded = pot.IsSeeded,
                    SeedConfigName = pot.CurrentSeedConfig is null ? string.Empty : pot.CurrentSeedConfig.Name,
                    SeedbedData = _seedbedSerializer.Serialize(pot.Seedbed)
                };
                pots.Add(potData);
            }
    
            saveState[Pots] = Serializer.SerializeObject(pots);
        }
        
        public void Deserialize(IDictionary<string, string> loadState)
        {
            if (!loadState.TryGetValue(Pots, out string json)) return;
    
            (List<PotData> info, bool ok) = Serializer.DeserializeObject<List<PotData>>(json);
            if (!ok) return;

            _factory.Clear();
            foreach (PotData potData in info)
            {
                var position = potData.Position.ToVector3();
                var rotation = potData.Rotation.ToQuaternion();
                
                Pot pot = _factory.Create(position, rotation);
                if (!potData.IsSeeded) continue;
                
                if (!_seedCatalog.TryGetItem(potData.SeedConfigName, out ItemConfig config)) continue;
                    
                if (config is not SeedItemConfig seedConfig) continue;

                if (pot.Seed(seedConfig))
                {
                    _seedbedSerializer.Deserialize(pot.Seedbed, potData.SeedbedData);
                }
            }

            SetupState();
        }

        private void SetupState()
        {
            GameState gameCycleState = _gameCycle.State;
            switch (gameCycleState)
            {
                case GameState.Pause:
                    (_controller as IPauseGameListener)?.OnPause();
                    break;
                case GameState.IsRunning:
                    (_controller as IResumeGameListener)?.OnResume();
                    break;
            }
        }
    }
}