using Code.Infrastructure.SceneManagement;
using Zenject;

namespace Code.Infrastructure.GameStates
{
    public class BootstrapState : IState
    {
        private readonly IGameStateMachine _gameStateMachine;
        private ISceneLoader _sceneLoader;

        public BootstrapState(IGameStateMachine gameStateMachine, ISceneLoader sceneLoader)
        {
            _gameStateMachine = gameStateMachine;
            _sceneLoader = sceneLoader;
        }
        public void Enter() => 
            _sceneLoader.Load(SceneNames.BootSceneKey, onLoaded: EnterLoadLevel);


        public void Exit()
        {
        }

        private void EnterLoadLevel()
        {
            _gameStateMachine.Enter<LoadProgressState>();
        }

        public class Factory : PlaceholderFactory<IGameStateMachine, BootstrapState>
        {
        }
    }
}