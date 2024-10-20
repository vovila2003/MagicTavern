using System;
using System.Threading;
using Cysharp.Threading.Tasks;

namespace Modules.Timers
{
    public sealed class CountdownTimer
    {
        private const int DelayInMs = 1000;
        
        public event Action OnFinished;
        public event Action<int> OnTicked;

        private CancellationTokenSource _source;
        
        public async UniTaskVoid Start(int durationInSeconds)
        {
            _source = new CancellationTokenSource();
            CancellationToken token = _source.Token;
            while (durationInSeconds >= 0)
            {
                await UniTask.Delay(DelayInMs, cancellationToken: token);
                durationInSeconds--;
                OnTicked?.Invoke(durationInSeconds);
            }
            OnFinished?.Invoke();
        }

        public void Reset() => _source?.Cancel();
    }
}