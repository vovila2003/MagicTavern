using Modules.GameCycle.Interfaces;
using Tavern.InputServices.Interfaces;
using UnityEngine;
using VContainer.Unity;

namespace Tavern.Character.Controllers
{
    public sealed class CharacterBlockController : 
        IStartGameListener,
        IFinishGameListener,
        ITickable
    {
        private readonly ICharacter _character;
        private readonly IBlockInput _blockInput;
        private bool _blockRequired;

        public CharacterBlockController(ICharacter character, IBlockInput blockInput)
        {
            _character = character;
            _blockInput = blockInput;
        }
        
        void ITickable.Tick()
        {
            if (!_blockRequired) return;

            Debug.Log("Block");
            _blockRequired = false;
        }

        private void OnBlock()
        {
            _blockRequired = true;
        }

        void IStartGameListener.OnStart()
        {
            _blockInput.OnBlock += OnBlock;
        }

        void IFinishGameListener.OnFinish()
        {
            _blockInput.OnBlock -= OnBlock;
        }
    }
}