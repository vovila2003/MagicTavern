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

        private readonly Vector2 _factor;

        public Vector3 Scale { get; }
        public Sprite Minimap { get; }

        public MinimapService(
            ICharacter character, 
            SceneSettings sceneSettings)
        {
            _character = character;
            Minimap = sceneSettings.MinimapSettings.MinimapImage;
            GroundSettings groundSettings = sceneSettings.GroundSettings;
            
            float scale = sceneSettings.MinimapSettings.MinimapView.rect.width * groundSettings.Width / 
                (sceneSettings.MinimapSettings.MinimapWidthInMeters * Minimap.rect.width);
            Scale = new Vector3(scale, scale, 0);
            
            _factor = new Vector2
            {
                x = - Minimap.rect.width * Scale.x / groundSettings.Width,
                y = - Minimap.rect.height * Scale.y / groundSettings.Heigth,
            };
        }
        
        void IStartGameListener.OnStart() => _character.OnPositionChanged += OnCharacterPositionChanged;

        void IFinishGameListener.OnFinish() => _character.OnPositionChanged -= OnCharacterPositionChanged;

        private void OnCharacterPositionChanged(Vector3 position) => 
            OnPositionChanged?.Invoke(new Vector3(_factor.x * position.x, _factor.y * position.z, 0));
    }
}