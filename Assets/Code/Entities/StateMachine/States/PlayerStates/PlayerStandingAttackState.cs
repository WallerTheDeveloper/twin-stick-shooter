using UnityEngine;
using Code.Animator;
using Code.Animator.Logic;
using Code.Entities.EntitiesTransformation;
using Code.Infrastructure.Services.Input;

namespace Code.Entities.StateMachine.States.PlayerStates
{
    public class PlayerStandingAttackState : IEntityState
    {
        private readonly IRotation _rotation;
        private readonly IAnimationStateReader _playerAnimator;
        private readonly IInputService _inputService;
        private readonly Transform _characterTransform;

        public PlayerStandingAttackState(
            IRotation rotation,
            IAnimationStateReader playerAnimator,
            IInputService inputService,
            Transform characterTransform)
        {
            _rotation = rotation;
            _playerAnimator = playerAnimator;
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
            _rotation.RotateTowardsInputDirection(_characterTransform);
        }

        public void OnExit()
        {
        }
    }
}