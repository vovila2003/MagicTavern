using System;

namespace Modules.Timers.Interfaces
{
    public interface IProgressable
    {
        event Action<float> OnProgressChanged; 

        float GetProgress();
        void SetProgress(float progress);
    }
}