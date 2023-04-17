using Code.Animator;
using Code.Animator.Logic;
using Code.Entities.EntitiesTransformation;
using Code.Infrastructure.Services.Input;

namespace Code.Entities.StateMachine.States.PlayerStates
{
    public class PlayerMoveState : IEntityState
    {
        private readonly PlayerAnimator _playerAnimator;
        private readonly IMovement _movement;
        private readonly IInputService _inputService;

        public PlayerMoveState(PlayerAnimator playerAnimator, IMovement movement, IInputService inputService)
        {
            _playerAnimator = playerAnimator;
            _movement = movement;
            _inputService = inputService;
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
            _movement.Move(_inputService.MovementAxis(), _movement.MovementSpeed);
        }

        public void OnExit()
        {
        }
    }
}