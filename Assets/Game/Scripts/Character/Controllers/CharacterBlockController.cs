using JetBrains.Annotations;
using Modules.GameCycle.Interfaces;
using Tavern.InputServices.Interfaces;
using UnityEngine;
using VContainer.Unity;

namespace Tavern.Character.Controllers
{
    [UsedImplicitly]
    public sealed class CharacterBlockController : 
        IStartGameListener,
        IFinishGameListener,
        IPauseGameListener,
        IResumeGameListener,
        ITickable
    {
        private readonly ICharacter _character;
        private readonly IBlockInput _blockInput;
        private bool _blockRequired;
        private bool _enable;

        public CharacterBlockController(ICharacter character, IBlockInput blockInput)
        {
            _character = character;
            _blockInput = blockInput;
        }
        
        void ITickable.Tick()
        {
            if (!_enable) return;
            
            if (!_blockRequired) return;

            Debug.Log("Block");
            _blockRequired = false;
        }

        private void OnBlock()
        {
            _blockRequired = true;
        }

        void IStartGameListener.OnStart() => Activate();

        void IFinishGameListener.OnFinish() => Deactivate();

        void IPauseGameListener.OnPause() => Deactivate();

        void IResumeGameListener.OnResume() => Activate();

        private void Activate()
        {
            _blockInput.OnBlock += OnBlock;
            _enable = true;
        }

        private void Deactivate()
        {
            _blockInput.OnBlock -= OnBlock;
            _enable = false;
        }
    }
}