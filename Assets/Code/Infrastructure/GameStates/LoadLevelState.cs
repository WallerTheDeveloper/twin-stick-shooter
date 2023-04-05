using Code.Camera;
using Code.Infrastructure.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace Code.Infrastructure.GameStates
{
    public class LoadLevelState : IPayloadedState<string>
    {
        private readonly IGameStateMachine _gameStateMachine;
        private readonly ISceneLoader _sceneLoader;
        
        public LoadLevelState(IGameStateMachine gameStateMachine, ISceneLoader sceneLoader)
        {
            _gameStateMachine = gameStateMachine;
            _sceneLoader = sceneLoader;
        }

        public void Enter(string sceneName) => 
            _sceneLoader.Load(sceneName, OnLoaded);

        public void Exit()
        {
            
        }

        private void OnLoaded()
        {
            InitGameWorld();
            _gameStateMachine.Enter<GameLoopState>();
        }

        private void InitGameWorld()
        {
            InitSpawners();
        }

        private void InitSpawners()
        {
            string sceneKey = SceneManager.GetActiveScene().name;
            // LevelStaticData levelData = _staticData.ForLevel(sceneKey);

            // Initialize enemy spawners here
        }

        public class Factory: PlaceholderFactory<IGameStateMachine, LoadLevelState>
        {
        }
    }
}