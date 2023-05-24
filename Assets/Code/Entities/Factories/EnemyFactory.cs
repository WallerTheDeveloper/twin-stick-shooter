using Code.Entities.EnemyEntity;
using Code.Entities.EnemyEntity.Patrol;
using Code.Infrastructure.Services.Data;
using Code.StaticData.Enemies;
using UnityEngine;
using UnityEngine.AI;

namespace Code.Entities.Factories
{
    public class EnemyFactory : IEnemyFactory
    {
        private readonly Enemy.Factory _enemyFactory;
        private readonly IStaticDataService _staticDataService;

        public EnemyFactory(Enemy.Factory enemyFactory, IStaticDataService staticDataService)
        {
            _enemyFactory = enemyFactory;
            _staticDataService = staticDataService;
        }

        public IEntity Create(EnemyTypeId enemyTypeId, Transform transform, float positionCoefficient)
        {
            MonsterStaticData monsterStaticData = _staticDataService.ForMonster(enemyTypeId);
            
            IHostile enemyObject =
                _enemyFactory.Create(monsterStaticData.ParentPrefab, monsterStaticData.MonsterPrefab);

            Vector3 targetPosition = transform.position;
            
            targetPosition.z += positionCoefficient;
            
            NavMeshAgent enemyNavMesh = enemyObject.EntityTransform.GetComponent<NavMeshAgent>();

            enemyNavMesh.Warp(targetPosition);
            
            enemyObject.Initialize();

            return enemyObject;
        }
    }
}