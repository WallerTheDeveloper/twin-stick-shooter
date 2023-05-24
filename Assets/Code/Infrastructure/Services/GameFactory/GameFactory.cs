using Code.Entities.EnemyEntity.Patrol;
using Code.Entities.Factories;
using Code.Entities.PlayerEntity;
using Code.Infrastructure.Services.AssetsManagement;
using Code.StaticData.Enemies;
using UnityEngine;

namespace Code.Infrastructure.Services.GameFactory
{
    public class GameFactory : IGameFactory
    {
        private readonly IAssets _assets;
        private readonly EnemySpawner.Factory _enemySpawnerFactory;
        private readonly Player.Factory _playerFactory;

        public GameFactory(
            IAssets assets,
            EnemySpawner.Factory enemySpawnerFactory,
            Player.Factory playerFactory)
        {
            _assets = assets;
            _enemySpawnerFactory = enemySpawnerFactory;
            _playerFactory = playerFactory;
        }
        public GameObject CreatePlayer(GameObject at)
        {
            GameObject playerPrefab = _assets.GetAsset<GameObject>(AssetPath.PLAYER_PATH);
            Player player = _playerFactory.Create(playerPrefab, at.transform);
            return player.gameObject;
        }
        public GameObject CreateHud()
        {
            GameObject hud = _assets.Instantiate(AssetPath.HUD_PATH);
            return hud;
        }
        public void CreateSpawner(Vector3 at, EnemyTypeId enemyTypeId)
        {
            EnemySpawner enemySpawner = _enemySpawnerFactory.Create();
            enemySpawner.transform.position = at;
            enemySpawner.EnemyType = enemyTypeId;
        }

        public void CreatePatrolPath(Vector3 at)
        {
            PatrolPath patrolPath = _assets.Instantiate(AssetPath.PATROL_PATH, at).GetComponent<PatrolPath>();
        }
    }
}