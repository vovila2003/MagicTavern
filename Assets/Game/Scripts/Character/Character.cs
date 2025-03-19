using System;
using Modules.GameCycle.Interfaces;
using Sirenix.OdinInspector;
using Tavern.Character.Agents;
using Tavern.Components;
using Tavern.Components.Interfaces;
using Tavern.Settings;
using UnityEngine;
using VContainer;

namespace Tavern.Character
{
    [RequireComponent(typeof(WeaponComponent), typeof(HitPointsComponent), typeof(WeaponComponent))]
    public sealed class Character : 
        MonoBehaviour, 
        ICharacter,
        ISpeedable,
        IInitGameListener,
        IPrepareGameListener,
        IExitGameListener
    {
        public event Action<Vector3> OnPositionChanged;
        
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
        
        [ShowInInspector]
        private CharacterState _state;

        [ShowInInspector, ReadOnly]
        private float _speed;

        [Inject]
        private void Construct(CharacterAttackAgent attackAgent, IMovable movable, GameSettings settings)
        {
            _settings = settings.CharacterSettings;
            _attackAgent = attackAgent;
            _movable = movable;
            _state = new CharacterState(_settings);
        }
        
        public CharacterAttackAgent GetAttackAgent() => _attackAgent;
        public HitPointsComponent GetHpComponent() => _hpComponent;
        public IMovable GetMoveComponent() => _movable;
        public Transform GetTransform() => transform;
        public float GetSpeed() => _speed;

        public void SetSpeed(float value)
        {
            _speed = value;
        }

        public void SetPosition(Vector3 position)
        {
            transform.position = position;
            OnPositionChanged?.Invoke(position);
        }

        public void SetRotation(Quaternion quaternion)
        {
            transform.rotation = quaternion;
        }

        public Animator GetAnimator() => _animator;
        public CharacterState GetState() => _state;

        private void Awake()
        {
            _hpComponent = GetComponent<HitPointsComponent>();
            _weapon = GetComponent<WeaponComponent>();
            _animator = View.GetComponent<Animator>();
            _state = new CharacterState(_settings);
        }

        void IInitGameListener.OnInit()
        {
            _movable.Init(Rigidbody, this);
            _movable.OnPositionChanged += OnCharacterPositionChanged;
            _attackAgent.Init(_weapon);
            _speed = _settings.Speed;
            _hpComponent.Init(_settings.MaxHealth);
        }

        void IPrepareGameListener.OnPrepare()
        {
            _hpComponent.Reset();
        }

        void IExitGameListener.OnExit()
        {
            _movable.OnPositionChanged -= OnCharacterPositionChanged;
        }

        private void OnCharacterPositionChanged(Vector3 position) => OnPositionChanged?.Invoke(position);
    }
}