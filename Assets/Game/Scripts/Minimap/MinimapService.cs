using System;
using JetBrains.Annotations;
using Modules.GameCycle.Interfaces;
using Tavern.Character;
using Tavern.Settings;
using UnityEngine;

namespace Tavern.Minimap
{
    [UsedImplicitly]
    public class MinimapService : IStartGameListener, IFinishGameListener
    {
        private readonly ICharacter _character;
        public event Action<Vector3> OnPositionChanged;
        
        public float Scale => 0.3f;
        public Sprite Minimap { get; private set; }

        public MinimapService(
            ICharacter character, 
            GameSettings gameSettings, 
            SceneSettings sceneSettings)
        {
            _character = character;
            Minimap = gameSettings.MinimapSettings.MiniMap;
            Rect ground = sceneSettings.Ground;
            // TODO
        }

        // for debug
        public void ChangePosition(Vector3 position)
        {
            OnCharacterPositionChanged(position);
        }

        void IStartGameListener.OnStart() => _character.OnPositionChanged += OnCharacterPositionChanged;

        void IFinishGameListener.OnFinish() => _character.OnPositionChanged -= OnCharacterPositionChanged;

        private void OnCharacterPositionChanged(Vector3 position)
        {
            //TODO factor
            float factor = -10f;
            OnPositionChanged?.Invoke(new Vector3(factor * position.x, factor * position.z, 0));
        }
    }
}