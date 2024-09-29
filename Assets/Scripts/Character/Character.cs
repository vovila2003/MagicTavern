using Architecture.Interfaces;
using Components;
using Sirenix.OdinInspector;
using UnityEngine;
using VContainer;

namespace Character
{
    [RequireComponent(typeof(WeaponComponent), typeof(HitPointsComponent), typeof(WeaponComponent))]
    public sealed class Character : 
        MonoBehaviour, 
        ICharacter,
        ISpeedable,
        IInitGameListener,
        IPrepareGameListener
    {
        public CharacterAttackAgent GetAttackAgent() => _attackAgent;
        public HitPointsComponent GetHpComponent() => _hpComponent;
        public IMovable GetMoveComponent() => _movable;
        public Transform GetTransform() => transform;
        public float GetSpeed() => _speed;

        private WeaponComponent _weapon;
        private CharacterAttackAgent _attackAgent;
        private HitPointsComponent _hpComponent;
        private IMovable _movable;
        private CharacterSettings _settings;
        
        [ShowInInspector, ReadOnly]
        private float _speed;

        [Inject]
        private void Construct(CharacterAttackAgent attackAgent, IMovable movable, CharacterSettings settings)
        {
            _settings = settings;
            _attackAgent = attackAgent;
            _movable = movable;
        }

        void IInitGameListener.OnInit()
        {
            _movable.Init(transform, this);
            _attackAgent.Init(_weapon);
            _speed = _settings.InitSpeed;
            _hpComponent.Init(_settings.Health);
        }

        private void Awake()
        {
            _hpComponent = GetComponent<HitPointsComponent>();
            _weapon = GetComponent<WeaponComponent>();
        }

        void IPrepareGameListener.OnPrepare()
        {
            _hpComponent.Reset();
        }
    }
}