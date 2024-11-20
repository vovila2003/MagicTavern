using System;
using Modules.Gardening;
using UnityEngine;

namespace Tavern.Gardening
{
    public class SeedbedViewController
    {
        private readonly ISeedbed _seedbed;
        private readonly MeshRenderer _renderer;
        private readonly Material _notReadyMaterial;
        private readonly Material _readyMaterial;

        public SeedbedViewController(
            ISeedbed seedbed, 
            MeshRenderer renderer, 
            Material notReadyMaterial, 
            Material readyMaterial)
        {
            _seedbed = seedbed;
            _renderer = renderer;
            _notReadyMaterial = notReadyMaterial;
            _readyMaterial = readyMaterial;

            _seedbed.OnStateChanged += OnStateChanged;
        }

        public void Dispose()
        {
            _seedbed.OnStateChanged -= OnStateChanged;
        }

        private void OnStateChanged(SeedbedState state)
        {
            switch (state)
            {
                case SeedbedState.NotReady:
                    _renderer.material = _notReadyMaterial;
                    break;
                case SeedbedState.Ready:
                    _renderer.material = _readyMaterial;
                    break;
                case SeedbedState.Seeded:
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(state), state, null);
            }
        }
    }
}