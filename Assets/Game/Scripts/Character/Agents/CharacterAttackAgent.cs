using Components;
using UnityEngine;

namespace Character
{
    public sealed class CharacterAttackAgent 
    {
        private WeaponComponent _weapon;

        public void Init(WeaponComponent weapon)
        {
            _weapon = weapon;
        }

        public void Fire()
        {
            Debug.Log("Fire");
        }
        
        public void AlternativeFire()
        {
            Debug.Log("Alternative Fire");
        }
    }
}