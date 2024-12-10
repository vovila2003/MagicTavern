using System;
using JetBrains.Annotations;
using Tavern.InputServices.Interfaces;
using UnityEngine;
using VContainer.Unity;

namespace Tavern.InputServices
{
    [UsedImplicitly]
    public sealed class MiniGameInputService :
        ISpaceInput,
        ITickable
    {
        
        public event Action OnSpace;
        
        void ITickable.Tick()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                OnSpace?.Invoke();
            }
        }
    }
}