using Code.Entities.EnemyEntity.Data;
using Code.Entities.EntitiesTransformation;
using Code.Extensions;
using UnityEngine;

namespace Code.Entities.StateMachine.States.EnemyStates
{
    public class EnemyPursueState : IEntityState
    {
        private readonly Transform _target;
        private readonly ITimerUpdater _timerUpdater;
        private readonly IMovement _movement;

        public EnemyPursueState(Transform target, ITimerUpdater timerUpdater, IMovement movement)
        {
            _target = target;
            _timerUpdater = timerUpdater;
            _movement = movement;
        }
        
        public void OnEnter()
        {
            _timerUpdater.TimeSinceLastSawTarget = 0f;
        }

        public void Tick()
        {
            _movement.Move(_target.position, _movement.MovementSpeed);
        }

        public void FixedTick()
        {
        }

        public void OnExit()
        {
        }
    }
}