using System;
using Tavern.Character.Agents;
using Tavern.Components;
using Tavern.Components.Interfaces;
using UnityEngine;

namespace Tavern.Character
{
    public interface ICharacter
    {
        event Action<Vector3> OnPositionChanged;
        
        CharacterAttackAgent GetAttackAgent();
        HitPointsComponent GetHpComponent();
        IMovable GetMoveComponent();
        Transform GetTransform();
        void SetPosition(Vector3 position);
        void SetRotation(Quaternion quaternion);
        Animator GetAnimator();
        CharacterState GetState();
    }
}