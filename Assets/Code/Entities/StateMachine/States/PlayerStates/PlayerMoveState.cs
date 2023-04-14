using Code.Animator;
using Code.Animator.Logic;
using Code.Entities.EntitiesTransformation;

namespace Code.Entities.StateMachine.States.PlayerStates
{
    public class PlayerMoveState : IEntityState
    {

        private readonly PlayerAnimator _playerAnimator;
        private readonly IMovement _movement;

        public PlayerMoveState(PlayerAnimator playerAnimator, IMovement movement)
        {
            _playerAnimator = playerAnimator;
            _movement = movement;
        }
        public void OnEnter()
        {
            _playerAnimator.PlayShooting(false);
        }

        public void Tick()
        {
            _playerAnimator.Tick();
        }

        public void FixedTick()
        {
            _movement.Move();
        }

        public void OnExit()
        {
        }
    }
}