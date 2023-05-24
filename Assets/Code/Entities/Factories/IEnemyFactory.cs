using Code.Entities.EnemyEntity.Patrol;
using Code.StaticData.Enemies;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Code.Entities.Factories
{
    public interface IEnemyFactory
    {
        public IEntity Create(EnemyTypeId enemyType, Transform position, float positionCoefficient);
    }
}