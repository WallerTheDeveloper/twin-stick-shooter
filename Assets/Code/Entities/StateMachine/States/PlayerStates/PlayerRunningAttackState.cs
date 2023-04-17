using Code.Animator;
using Code.Animator.Logic;
using Code.Entities.EntitiesTransformation;
using Code.Infrastructure.Services.Input;
using UnityEngine;

namespace Code.Entities.StateMachine.States.PlayerStates
{
    public class PlayerRunningAttackState : IEntityState
    {
        private readonly IAnimationStateReader _playerAnimator;
        private readonly IMovement _movement;
        private readonly IRotation _rotation;
        private readonly IInputService _inputService;
        private readonly Transform _characterTransform;

        public PlayerRunningAttackState(
            IAnimationStateReader playerAnimator,
            IMovement movement,
            IRotation rotation,
            IInputService inputService,
            Transform characterTransform)
        {
            _playerAnimator = playerAnimator;
            _movement = movement;
            _rotation = rotation;
            _inputService = inputService;
            _characterTransform = characterTransform;
        }
        public void OnEnter()
        {
        }

        public void Tick()
        {
            _playerAnimator.Tick();
        }

        public void FixedTick()
        {
            _movement.Move(_inputService.MovementAxis(), _movement.MovementSpeed);
            _rotation.RotateTowardsInputDirection(_characterTransform);
        }

        public void OnExit()
        {
        }
    }
}