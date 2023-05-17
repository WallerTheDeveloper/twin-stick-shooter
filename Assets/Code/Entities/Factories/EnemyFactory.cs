using Code.Entities.EnemyEntity;
using Code.Infrastructure.Services.Data;
using Code.StaticData.Enemies;
using UnityEngine;

namespace Code.Entities.Factories
{
    public class EnemyFactory : IEnemyFactory
    {
        private readonly IStaticDataService _staticDataService;
        public EnemyFactory(IStaticDataService staticDataService)
        {
            _staticDataService = staticDataService;
        }        
        public IEntity Create(EnemyTypeId enemyTypeId, Vector3 position)
        {
            MonsterStaticData monsterStaticData = _staticDataService.ForMonster(enemyTypeId);

            GameObject monsterObject = Object.Instantiate(monsterStaticData.Prefab, position, Quaternion.identity);

            IHostile monster = monsterObject.GetComponent<Enemy>();
            
            monster.Initialize();

            return monster;
        }
    }
}