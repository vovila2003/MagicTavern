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
        [SerializeField] 
        private Transform View;
        
        [SerializeField]
        private Rigidbody Rigidbody;
        
        private WeaponComponent _weapon;
        private CharacterAttackAgent _attackAgent;
        private HitPointsComponent _hpComponent;
        private IMovable _movable;
        private CharacterSettings _settings;
        private Animator _animator;

        [ShowInInspector, ReadOnly]
        private float _speed;

        [Inject]
        private void Construct(CharacterAttackAgent attackAgent, IMovable movable, CharacterSettings settings)
        {
            _settings = settings;
            _attackAgent = attackAgent;
            _movable = movable;
        }

        public CharacterAttackAgent GetAttackAgent() => _attackAgent;
        public HitPointsComponent GetHpComponent() => _hpComponent;
        public IMovable GetMoveComponent() => _movable;
        public Transform GetTransform() => transform;
        public float GetSpeed() => _speed;
        public Animator GetAnimator() => _animator;

        void IInitGameListener.OnInit()
        {
            _movable.Init(Rigidbody, this);
            _attackAgent.Init(_weapon);
            _speed = _settings.InitSpeed;
            _hpComponent.Init(_settings.Health);
        }

        private void Awake()
        {
            _hpComponent = GetComponent<HitPointsComponent>();
            _weapon = GetComponent<WeaponComponent>();
            _animator = View.GetComponent<Animator>();
        }

        void IPrepareGameListener.OnPrepare()
        {
            _hpComponent.Reset();
        }
    }
}