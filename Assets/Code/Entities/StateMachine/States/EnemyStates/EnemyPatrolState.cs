using Code.Entities.EnemyEntity.Patrol;

namespace Code.Entities.StateMachine.States.EnemyStates
{
    public class EnemyPatrolState : IEntityState
    {
        private readonly IPatrolBehaviour _patrolBehaviour;

        public EnemyPatrolState(IPatrolBehaviour patrolBehaviour)
        {
            _patrolBehaviour = patrolBehaviour;
        }
        
        public void OnEnter()
        {
        }

        public void Tick()
        {
            _patrolBehaviour.PatrolArea();
        }

        public void FixedTick()
        {
        }

        public void OnExit()
        {
        }
    }
}