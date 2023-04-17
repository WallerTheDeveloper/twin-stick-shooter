using UnityEngine;

namespace Code.Entities.EnemyEntity.Patrol.Data
{
    [CreateAssetMenu(menuName = "Patrol/Patrol Behaviour Settings")]
    public class PatrolBehaviourSettings : ScriptableObject
    {
        public float WaypointDwellTime;
        public double WaypointTolerance;
    }
}