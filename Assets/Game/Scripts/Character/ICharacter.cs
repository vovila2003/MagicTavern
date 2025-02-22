using Tavern.Character.Agents;
using Tavern.Components;
using Tavern.Components.Interfaces;
using UnityEngine;

namespace Tavern.Character
{
    public interface ICharacter
    {
        CharacterAttackAgent GetAttackAgent();
        HitPointsComponent GetHpComponent();
        IMovable GetMoveComponent();
        Transform GetTransform();
        Animator GetAnimator();
        CharacterState GetState();
    }
}