using Modules.GameCycle.Interfaces;
using Tavern.InputServices.Interfaces;
using UnityEngine;
using VContainer.Unity;

namespace Tavern.Character.Controllers
{
    public sealed class CharacterActionController : 
        IStartGameListener,
        IFinishGameListener,
        ITickable
    {
        private readonly ICharacter _character;
        private readonly IActionInput _actionInput;
        private bool _actionRequired;

        public CharacterActionController(ICharacter character, IActionInput actionInput)
        {
            _character = character;
            _actionInput = actionInput;
        }
        
        void ITickable.Tick()
        {
            if (!_actionRequired) return;

            Debug.Log("Action");
            _actionRequired = false;
        }

        private void OnAction()
        {
            _actionRequired = true;
        }

        void IStartGameListener.OnStart()
        {
            _actionInput.OnAction += OnAction;
        }

        void IFinishGameListener.OnFinish()
        {
            _actionInput.OnAction -= OnAction;
        }
    }
}