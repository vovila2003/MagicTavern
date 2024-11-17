using Modules.GameCycle.Interfaces;
using Tavern.InputServices.Interfaces;
using UnityEngine;

namespace Tavern.Character.Controllers
{
    public sealed class CharacterBlockController : 
        IStartGameListener,
        IFinishGameListener,
        IUpdateListener
    {
        private readonly ICharacter _character;
        private readonly IBlockInput _blockInput;
        private bool _blockRequired;

        public CharacterBlockController(ICharacter character, IBlockInput blockInput)
        {
            _character = character;
            _blockInput = blockInput;
        }
        
        void IUpdateListener.OnUpdate(float _)
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