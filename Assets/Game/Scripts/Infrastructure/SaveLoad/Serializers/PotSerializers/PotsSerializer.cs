using JetBrains.Annotations;
using Modules.GameCycle;
using Modules.GameCycle.Interfaces;
using Modules.Items;
using Tavern.Gardening;
using Tavern.Settings;
using Tavern.Utils;
using UnityEngine;

namespace Tavern.Infrastructure
{
    [UsedImplicitly]
    public class PotsSerializer : GameSerializer<PotsData>
    {
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

        protected override PotsData Serialize()
        {
            var data = new PotsData(_factory.Pots.Count);
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
                data.Pots.Add(potData);
            }

            return data;
        }

        protected override void Deserialize(PotsData data)
        {
            _factory.Clear();
            foreach (PotData potData in data.Pots)
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