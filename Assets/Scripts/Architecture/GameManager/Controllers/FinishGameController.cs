using Architecture.Interfaces;

namespace Architecture.Controllers
{
    public sealed class FinishGameController : 
        IStartGameListener,
        IFinishGameListener
    {
        public void OnStart()
        {
            
        }

        public void OnFinish()
        {
            
        }
    }
}