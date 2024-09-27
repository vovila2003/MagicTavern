using Architecture.Interfaces;
using Components;
using UnityEngine;
using VContainer;

namespace Character
{
    [RequireComponent(typeof(WeaponComponent), typeof(HitPointsComponent))]
    public sealed class Character : 
        MonoBehaviour, 
        ICharacter,
        ISpeedable,
        IPrepareGameListener
    {
        public CharacterAttackAgent GetAttackAgent() => _attackAgent;
        public HitPointsComponent GetHpComponent() => _hpComponent;
        public IMovable GetMoveComponent() => _movable;
        public Transform GetTransform() => transform;
        public float GetSpeed() => Speed;

        [SerializeField]
        private float Speed = 5.0f;
        
        [SerializeField]
        private WeaponComponent Weapon;
        
        private CharacterAttackAgent _attackAgent;
        private HitPointsComponent _hpComponent;
        private IMovable _movable;

        [Inject]
        private void Construct(CharacterAttackAgent attackAgent, IMovable movable)
        {
            _attackAgent = attackAgent;
            _movable = movable;
            _movable.Init(transform, this);
            _attackAgent.Init(Weapon);
        }
        
        private void Awake()
        {
            _hpComponent = GetComponent<HitPointsComponent>();
        }
        
        void IPrepareGameListener.OnPrepare()
        {
            _hpComponent.Reset();
        }
    }
}