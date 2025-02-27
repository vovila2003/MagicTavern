using JetBrains.Annotations;
using Modules.GameCycle.Interfaces;
using Tavern.Character.Agents;
using Tavern.InputServices.Interfaces;
using VContainer.Unity;

namespace Tavern.Character.Controllers
{
    [UsedImplicitly]
    public sealed class CharacterFireController : 
        IStartGameListener,
        IFinishGameListener,
        IPauseGameListener,
        IResumeGameListener,
        ITickable
    {
        private readonly CharacterAttackAgent _attackAgent;
        private readonly IShootInput _shootInput;
        private bool _fireRequired;
        private bool _alternativeFireRequired;
        private bool _enable;

        public CharacterFireController(ICharacter character, IShootInput shootInput)
        {
            _attackAgent = character.GetAttackAgent();
            _shootInput = shootInput;
        }
        
        void ITickable.Tick()
        {
            if (!_enable) return;
            
            CheckAttack();
            CheckAlternativeAttack();
        }
        
        void IStartGameListener.OnStart() => Activate();

        void IFinishGameListener.OnFinish() => Deactivate();

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

        void IPauseGameListener.OnPause() => Deactivate();

        void IResumeGameListener.OnResume() => Activate();

        private void Activate()
        {
            _shootInput.OnFire += OnFire;
            _shootInput.OnAlternativeFire += OnAlternativeFire;
            _enable = true;
        }

        private void Deactivate()
        {
            _shootInput.OnFire -= OnFire;
            _shootInput.OnAlternativeFire -= OnAlternativeFire;
            _enable = false;
        }
    }
}
