using Components;
using UnityEngine;

namespace Character
{
    public interface ICharacter
    {
        CharacterAttackAgent GetAttackAgent();
        HitPointsComponent GetHpComponent();
        IMovable GetMoveComponent();
        Transform GetTransform();
    }
}