using Code.Entities.EnemyEntity.Attack;

namespace Code.Entities.StateMachine.States.EnemyStates
{
    public class EnemyAttackState : IEntityState
    {
        private readonly IAttackBehaviour _attackBehaviour;

        public EnemyAttackState(IAttackBehaviour attackBehaviour)
        {
            _attackBehaviour = attackBehaviour;
        }
        
        public void OnEnter()
        {
            _attackBehaviour.Attack();
        }

        public void Tick()
        {
        }

        public void FixedTick()
        {
        }

        public void OnExit()
        {
        }
    }
}