using Code.Entities.EnemyEntity.Data;
using Code.Entities.EntitiesTransformation;
using Code.Extensions;
using UnityEngine;

namespace Code.Entities.EnemyEntity.Patrol
{
    public class PatrolBehaviour : IPatrolBehaviour
    {
        private readonly IEntity _entity;
        private readonly PatrolPath _assignedPatrolPath;
        private readonly AISettings _aiSettings;
        private readonly IMovement _movement;
        private readonly ITimerUpdater _timerUpdater;

        private int _currentWaypointIndex = 0;

        public PatrolBehaviour(
            IEntity entity,
            PatrolPath assignedPatrolPath,
            AISettings aiSettings,
            IMovement movement,
            ITimerUpdater timerUpdater)
        {
            _entity = entity;
            _assignedPatrolPath = assignedPatrolPath;
            _aiSettings = aiSettings;
            _movement = movement;
            _timerUpdater = timerUpdater;
        }


        public void PatrolArea()
        {
            Vector3 nextPosition = _entity.EntityTransform.position;

            if (_assignedPatrolPath != null)
            {
                if (AtWaypoint())
                {
                    _timerUpdater.TimeSinceArrivedAtWaypoint = 0;
                    CycleWaypoint();
                }
                nextPosition = GetCurrentWaypoint();
            }

            if(_timerUpdater.TimeSinceArrivedAtWaypoint > _aiSettings.WaypointDwellTime)
            {
                _movement.Move(nextPosition, _movement.MovementSpeed);
            }
        }
        private bool AtWaypoint()
        {
            float distanceToWaypoint = Vector3.Distance(_entity.EntityTransform.position, GetCurrentWaypoint());
            return distanceToWaypoint < _aiSettings.WaypointTolerance;
        }

        private void CycleWaypoint() => 
            _currentWaypointIndex = _assignedPatrolPath.GetNextIndex(_currentWaypointIndex);

        private Vector3 GetCurrentWaypoint() => 
            _assignedPatrolPath.GetWaypoint(_currentWaypointIndex);
    }
}