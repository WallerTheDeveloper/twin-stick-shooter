using Code.Infrastructure.SceneManagement;
using Code.Infrastructure.Services.Data;
using Zenject;

namespace Code.Infrastructure.GameStates
{
    public class GameBootstrapState : IGameState
    {
        private readonly IGameStateMachine _gameStateMachine;
        private readonly ISceneLoader _sceneLoader;
        public GameBootstrapState(IGameStateMachine gameStateMachine, ISceneLoader sceneLoader)
        {
            _gameStateMachine = gameStateMachine;
            _sceneLoader = sceneLoader;
        }
        public void Enter() => 
            _sceneLoader.Load(SceneNames.BootSceneKey, onLoaded: EnterLoadMainMenu);


        public void Exit()
        {
        }

        private void EnterLoadMainMenu()
        {
            // _gameStateMachine.Enter<LoadProgressState>();
            _gameStateMachine.Enter<LoadMainMenuState>();
        }
        
        public class Factory : PlaceholderFactory<IGameStateMachine, GameBootstrapState>
        {
        }
    }
}