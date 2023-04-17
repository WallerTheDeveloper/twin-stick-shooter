using Code.Entities.EnemyEntity.Patrol.Data;
using Code.Entities.EntitiesTransformation;
using UnityEngine;

namespace Code.Entities.EnemyEntity.Patrol
{
    public class PatrolBehaviour : IPatrolBehaviour
    {
        private readonly PatrolPath _assignedPatrolPath;
        private readonly PatrolBehaviourSettings _patrolBehaviourSettings;
        private readonly IMovement _movement;
        private readonly IEntity _entity;

        private float _timeSinceArrivedAtWaypoint = Mathf.Infinity;

        private int _currentWaypointIndex = 0;

        public PatrolBehaviour(
            IEntity entity,
            PatrolPath assignedPatrolPath,
            PatrolBehaviourSettings patrolBehaviourSettings,
            IMovement movement)
        {
            _entity = entity;
            _assignedPatrolPath = assignedPatrolPath;
            _patrolBehaviourSettings = patrolBehaviourSettings;
            _movement = movement;
        }
        
        public void PatrolArea()
        {
            Vector3 nextPosition = _entity.EntityTransform.position;

            if (_assignedPatrolPath != null)
            {
                if (AtWaypoint())
                {
                    _timeSinceArrivedAtWaypoint = 0;
                    CycleWaypoint();
                }
                nextPosition = GetCurrentWaypoint();
            }

            if (_timeSinceArrivedAtWaypoint > _patrolBehaviourSettings.WaypointDwellTime)
            {
                _movement.Move(nextPosition, _movement.MovementSpeed);
            }
        }

        public void UpdateTimers() => 
            _timeSinceArrivedAtWaypoint += Time.deltaTime;

        private bool AtWaypoint()
        {
            float distanceToWaypoint = Vector3.Distance(_entity.EntityTransform.position, GetCurrentWaypoint());
            return distanceToWaypoint < _patrolBehaviourSettings.WaypointTolerance;
        }

        private void CycleWaypoint() => 
            _currentWaypointIndex = _assignedPatrolPath.GetNextIndex(_currentWaypointIndex);

        private Vector3 GetCurrentWaypoint() => 
            _assignedPatrolPath.GetWaypoint(_currentWaypointIndex);
    }
}