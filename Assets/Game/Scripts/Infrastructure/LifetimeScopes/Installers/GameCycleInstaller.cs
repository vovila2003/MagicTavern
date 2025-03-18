using VContainer;
using VContainer.Unity;

namespace Tavern.Infrastructure
{
    public class GameCycleInstaller : IInstaller
    {
        public void Install(IContainerBuilder builder)
        {
            builder.Register<Modules.GameCycle.GameCycle>(Lifetime.Singleton).AsSelf();
            builder.Register<TimeGameCycle>(Lifetime.Singleton).AsImplementedInterfaces().AsSelf();
            builder.RegisterEntryPoint<GameCycleInjector>();
            builder.Register<FinishGameController>(Lifetime.Singleton);
            builder.Register<PauseGameController>(Lifetime.Singleton).AsSelf().AsImplementedInterfaces();
        }
    }
}