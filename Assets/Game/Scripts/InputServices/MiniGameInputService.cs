using System;
using JetBrains.Annotations;
using Modules.GameCycle.Interfaces;
using Tavern.InputServices.Interfaces;
using UnityEngine;

namespace Tavern.InputServices
{
    [UsedImplicitly]
    public sealed class MiniGameInputService :
        ISpaceInput,
        IUpdateListener
    {
        
        public event Action OnSpace;
        
        void IUpdateListener.OnUpdate(float _)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                OnSpace?.Invoke();
            }
        }
    }
}