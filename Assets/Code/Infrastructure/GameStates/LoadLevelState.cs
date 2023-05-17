using Code.Camera;
using Code.Entities;
using Code.Entities.PlayerEntity;
using Code.Infrastructure.SceneManagement;
using Code.Infrastructure.Services.Data;
using Code.Infrastructure.Services.GameFactory;
using Code.StaticData;
using Code.StaticData.Enemies;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace Code.Infrastructure.GameStates
{
    public class LoadLevelState : IPayloadedState<string>
    {
        private const string InitialPointTag = "InitialPoint";
        
        private readonly IGameStateMachine _gameStateMachine;
        private readonly ISceneLoader _sceneLoader;
        private readonly IStaticDataService _staticData;
        private readonly IGameFactory _gameFactory;
        
        public LoadLevelState(
            IGameStateMachine gameStateMachine, 
            ISceneLoader sceneLoader,
            IStaticDataService staticData,
            IGameFactory gameFactory)
        {
            _gameStateMachine = gameStateMachine;
            _sceneLoader = sceneLoader;
            _staticData = staticData;
            _gameFactory = gameFactory;
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
            _staticData.LoadStaticData();

            InitSpawners();

            InitHud();

            InitPlayer();

        }

        private void InitSpawners()
        {
            string sceneKey = SceneManager.GetActiveScene().name;
            LevelStaticData levelData = _staticData.ForLevel(sceneKey);

            foreach (EnemySpawnerData spawnerData in levelData.EnemySpawners)
            {
                _gameFactory.CreateSpawner(spawnerData.Position, spawnerData.enemyTypeId);
            }
        }

        private void InitPlayer()
        {
            GameObject playerObject = _gameFactory.CreatePlayer(GameObject.FindWithTag(InitialPointTag));

            playerObject.GetComponent<Player>().Initialize();
        }

        private void InitHud()
        {
            GameObject hud = _gameFactory.CreateHud();
        }

        public class Factory: PlaceholderFactory<IGameStateMachine, LoadLevelState>
        {
        }
    }
}