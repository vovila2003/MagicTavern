using Tavern.Architecture.GameManager.Interfaces;
using Tavern.Character.Agents;
using Tavern.InputServices.Interfaces;

namespace Tavern.Character.Controllers
{
    public sealed class CharacterFireController : 
        IStartGameListener,
        IFinishGameListener,
        IUpdateListener
    {
        private readonly CharacterAttackAgent _attackAgent;
        private readonly IShootInput _shootInput;
        private bool _fireRequired;
        private bool _alternativeFireRequired;

        public CharacterFireController(ICharacter character, IShootInput shootInput)
        {
            _attackAgent = character.GetAttackAgent();
            _shootInput = shootInput;
        }
        
        void IUpdateListener.OnUpdate(float _)
        {
            CheckAttack();
            CheckAlternativeAttack();
        }
        
        void IStartGameListener.OnStart()
        {
            _shootInput.OnFire += OnFire;
            _shootInput.OnAlternativeFire += OnAlternativeFire;
        }

        void IFinishGameListener.OnFinish()
        {
            _shootInput.OnFire -= OnFire;
            _shootInput.OnAlternativeFire -= OnAlternativeFire;
        }

        private void CheckAttack()
        {
            if (!_fireRequired) return;

            _attackAgent.Fire();
            _fireRequired = false;
        }

        private void CheckAlternativeAttack()
        {
            if (!_alternativeFireRequired) return;

            _attackAgent.AlternativeFire();
            _alternativeFireRequired = false;
        }

        private void OnFire()
        {
            _fireRequired = true;
        }
        
        private void OnAlternativeFire()
        {
            _alternativeFireRequired = true;
        }
    }
}
