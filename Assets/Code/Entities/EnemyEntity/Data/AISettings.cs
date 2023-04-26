using UnityEngine;

namespace Code.Entities.EnemyEntity.Data
{
    [CreateAssetMenu(menuName = "AI/Create AI settings")]
    public class AISettings : ScriptableObject
    {
        public float MovementSpeed;
        public float ChaseDistance;
        public float SuspitionTime; 
        public float WaypointDwellTime;
        public float WaypointTolerance;
        public float AttackDistance;
    }
}