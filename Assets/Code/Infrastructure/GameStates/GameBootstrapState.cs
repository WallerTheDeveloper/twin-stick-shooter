using Code.Infrastructure.SceneManagement;
using Code.Infrastructure.Services.Data;
using Zenject;

namespace Code.Infrastructure.GameStates
{
    public class GameBootstrapState : IGameState
    {
        private readonly IGameStateMachine _gameStateMachine;
        private ISceneLoader _sceneLoader;
        public GameBootstrapState(IGameStateMachine gameStateMachine, ISceneLoader sceneLoader)
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
        
        public class Factory : PlaceholderFactory<IGameStateMachine, GameBootstrapState>
        {
        }
    }
}