using System;
using Code.Animator;
using Code.Camera;
using Code.Entities.EntitiesTransformation;
using Code.Entities.EntitiesTransformation.Calculations;
using Code.Entities.StateMachine;
using Code.Entities.StateMachine.States.PlayerStates;
using Code.Infrastructure.Services.Input;
using UnityEngine;
using Zenject;

namespace Code.Entities.Player
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private float _movementSpeed;
        [SerializeField] private float _turnSpeed;

        private IStateMachine _stateMachine;
        private IInputService _inputService;
        private IMovement _movement;
        private IRotation _rotation;
        private PlayerAnimator _playerAnimator;
        private CharacterController _characterController;
        private ITransformationCalculator _transformationCalculator;
        
        [Inject]
        public void Construct(IInputService inputService, ITransformationCalculator transformationCalculator)
        {
            _inputService = inputService;
            _transformationCalculator = transformationCalculator;
        }

        private void Awake()
        {
            GetComponents();

            _movement = new CharacterMovement(_characterController, _inputService, _transformationCalculator);
            _rotation = new CharacterRotation(_inputService, _transformationCalculator, _playerAnimator);

            _movement.MovementSpeed = _movementSpeed;
            _rotation.TurnSpeed = _turnSpeed;

            _stateMachine = new EntityStateMachine();

            CreateStates();
        }

        private void Update()
        {
            _stateMachine.Tick();
        }

        private void FixedUpdate()
        {
            _stateMachine.FixedTick(); 
        }

        private void GetComponents()
        {
            _playerAnimator = GetComponent<PlayerAnimator>();
            _characterController = GetComponent<CharacterController>();
        }

        private void CreateStates()
        {
            var playerIdleState = new PlayerIdleState(_playerAnimator);
            var playerMoveState = new PlayerMoveState(_playerAnimator, _movement);
            var playerRunningAttackState = new PlayerRunningAttackState(_playerAnimator, _movement, _rotation, transform);
            var playerAttackState = new PlayerStandingAttackState(_rotation, _playerAnimator, _inputService, transform);
            
            StateTransit(playerIdleState, playerMoveState, IsMoving());
            StateTransit(playerIdleState, playerAttackState, IsAttacking());
            
            StateTransit(playerMoveState, playerIdleState, () => !IsMoving()() && !IsAttacking()());
            StateTransit(playerMoveState, playerRunningAttackState, () => IsMoving()() && IsAttacking()());
            
            StateTransit(playerRunningAttackState, playerMoveState, () => IsMoving()() && !IsAttacking()());
            StateTransit(playerRunningAttackState, playerAttackState, () => !IsMoving()() && IsAttacking()());
            
            StateTransit(playerAttackState, playerRunningAttackState, () => IsMoving()() && IsAttacking()());            
            StateTransit(playerAttackState, playerIdleState, () => !IsMoving()() && !IsAttacking()());            
            
            _stateMachine.Enter(playerIdleState);

            Func<bool> IsMoving() => () => _inputService.MovementAxis().sqrMagnitude > Constants.Epsilon;
            Func<bool> IsAttacking() => () => _inputService.AimAxis().sqrMagnitude > Constants.Epsilon;
        }

        private void StateTransit(IEntityState from, IEntityState to, Func<bool> condition)
        {
            _stateMachine.AddTransition(from, to, condition);
        }
    }
}