using UnityEngine;

namespace Code.Entities.EnemyEntity.Attack
{
    public interface IAttackBehaviour
    {
        void Attack();
        // bool InAttackRangeOfTarget();
        // bool HasReachedTarget();
        bool IsInRange(float threshold);
        bool CanAttack(GameObject target);
    }
}