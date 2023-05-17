using Code.StaticData.Enemies;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Code.Entities.Factories
{
    public interface IEnemyFactory
    {
        public IEntity Create(EnemyTypeId enemyType, Vector3 position);
    }
}