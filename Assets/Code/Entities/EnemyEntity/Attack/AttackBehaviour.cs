using Code.Entities.EnemyEntity.Data;
using UnityEngine;

namespace Code.Entities.EnemyEntity.Attack
{
    public class AttackBehaviour : IAttackBehaviour
    {
        private readonly Enemy _enemy;
        private readonly AISettings _aiSettings;

        public AttackBehaviour(Enemy enemy, AISettings aiSettings)
        {
            _enemy = enemy;
            _aiSettings = aiSettings;
        }
        
        public void Attack()
        {
            Debug.Log("attacking");
        }
        
        // public bool InAttackRangeOfTarget()
        // {
        //     float distanceToTarget = Vector3.Distance(_enemy.TargetTransform.position, _enemy.EntityTransform.position);
        //     return distanceToTarget < _aiSettings.ChaseDistance;
        // }

        public bool IsInRange(float threshold)
        {
            float distanceToTarget = Vector3.Distance(_enemy.TargetTransform.position, _enemy.EntityTransform.position);
            return distanceToTarget < threshold;
        }
        // public bool HasReachedTarget()
        // {
        //     float distanceToTarget = Vector3.Distance(_enemy.TargetTransform.position, _enemy.EntityTransform.position);
        //     return distanceToTarget <= _aiSettings.AttackDistance;
        // }
        //
        public bool CanAttack(GameObject target) => 
            target != null;
    }
}