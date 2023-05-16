using Code.Entities.EnemyEntity.Data;
using Code.Entities.EntitiesTransformation;
using Code.Extensions;

namespace Code.Entities.StateMachine.States.EnemyStates
{
    public class EnemySuspiciousState : IEntityState
    {
        private readonly IMovement _movement;
        private readonly AISettings _data;
        private readonly ITimerUpdater _timerUpdater;

        public EnemySuspiciousState(IMovement movement, AISettings data, ITimerUpdater timerUpdater)
        {
            _movement = movement;
            _data = data;
            _timerUpdater = timerUpdater;
        }

        public void OnEnter()
        {
            _timerUpdater.IsSuspecting = true;
            _movement.MovementSpeed = 0;
        }

        public void Tick()
        {
            if (_timerUpdater.TimeSinceLastSawTarget < _data.SuspitionTime)
            {
                _movement.MovementSpeed = 0;
            }
            else
            {
                _timerUpdater.IsSuspecting = false;
            }
        }

        public void FixedTick()
        {
        }

        public void OnExit()
        {
            _movement.MovementSpeed = _data.MovementSpeed;
        }
    }
}