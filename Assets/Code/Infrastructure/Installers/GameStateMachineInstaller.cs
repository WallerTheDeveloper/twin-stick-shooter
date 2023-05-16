using Code.Infrastructure.GameStates;
using Zenject;

namespace Code.Infrastructure.Installers
{
    public class GameStateMachineInstaller : Installer<GameStateMachineInstaller>
    {
        public override void InstallBindings()
        {
            Container.BindFactory<IGameStateMachine, GameBootstrapState, GameBootstrapState.Factory>();
            Container.BindFactory<IGameStateMachine, LoadLevelState, LoadLevelState.Factory>();
            Container.BindFactory<IGameStateMachine, GameLoopState, GameLoopState.Factory>();
            Container.BindFactory<IGameStateMachine, LoadProgressState, LoadProgressState.Factory>();
            // Container.BindFactory<IGameStateMachine, GamePauseState, GamePauseState.Factory>();
            
            Container.Bind<IGameStateMachine>().To<GameStateMachine>().AsSingle();
        }
    }
}