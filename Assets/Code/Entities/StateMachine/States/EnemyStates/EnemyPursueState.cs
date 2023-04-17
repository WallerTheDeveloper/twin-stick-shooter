using Code.Entities.EntitiesTransformation;
using UnityEngine;

namespace Code.Entities.StateMachine.States.EnemyStates
{
    public class EnemyPursueState : IEntityState
    {
        private readonly Transform _target;
        private readonly IMovement _movement;

        public EnemyPursueState(Transform target, IMovement movement)
        {
            _target = target;
            _movement = movement;
        }
        
        public void OnEnter()
        {
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