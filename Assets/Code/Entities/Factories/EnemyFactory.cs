using Code.Entities.EnemyEntity;
using Code.Infrastructure.Services.AssetsManagement;
using UnityEngine;

namespace Code.Entities.Factories
{
    public class EnemyFactory : IEntityFactory
    {
        private readonly IAssets _assets;
        public EnemyFactory(IAssets assets)
        {
            _assets = assets;
        }        
        public IEntity GetEntity(string entityPath, Vector3 position)
        {
            GameObject enemyInstance = _assets.Instantiate(entityPath, position);

            IHostile enemy = enemyInstance.GetComponent<Enemy>();
            
            enemy.Initialize();

            return enemy;
        }
    }
}