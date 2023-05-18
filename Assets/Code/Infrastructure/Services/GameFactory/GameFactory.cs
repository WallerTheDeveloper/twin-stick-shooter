using Code.Entities;
using Code.Entities.EnemyEntity.Patrol;
using Code.Entities.Factories;
using Code.Infrastructure.Services.AssetsManagement;
using Code.StaticData.Enemies;
using UnityEngine;

namespace Code.Infrastructure.Services.GameFactory
{
    public class GameFactory : IGameFactory
    {
        private readonly IAssets _assets;
        public GameFactory(IAssets assets)
        {
            _assets = assets;
        }
        public GameObject CreatePlayer(GameObject at)
        {
            GameObject player = _assets.Instantiate(AssetPath.PLAYER_PATH, at.transform.position);
            return player;
        }
        public GameObject CreateHud()
        {
            GameObject hud = _assets.Instantiate(AssetPath.HUD_PATH);
            return hud;
        }
        
        public void CreateSpawner(Vector3 at, EnemyTypeId enemyTypeId)
        {
            EnemySpawner enemySpawner = _assets.Instantiate(AssetPath.SPAWNER_PATH, at).GetComponent<EnemySpawner>();
            
            enemySpawner.enemyType = enemyTypeId;
        }

        public void CreatePatrolPath(Vector3 at)
        {
            PatrolPath patrolPath = _assets.Instantiate(AssetPath.PATROL_PATH, at).GetComponent<PatrolPath>();
        }
    }
}