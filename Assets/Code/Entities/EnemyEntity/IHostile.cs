using Code.Entities.EnemyEntity.Patrol;

namespace Code.Entities.EnemyEntity
{
    public interface IHostile : IEntity
    {
        // string PrefabPath { get; }
        PatrolPath PatrolPath { get; set; }
    }
}