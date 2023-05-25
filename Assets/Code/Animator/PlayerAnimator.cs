using System;
using Code.Animator.Hashers;
using Code.Animator.Logic;
using Code.Entities.EntitiesTransformation.Calculations;
using Code.Infrastructure.Services.Input;
using Code.UI;
using UnityEngine;  
using Zenject;

namespace Code.Animator
{
    public class PlayerAnimator : MonoBehaviour, IAnimationStateReader, IAnimationPlayer
    {
        [SerializeField] private UnityEngine.Animator _animator;
        [SerializeField] private AnimatorStateHasher _animatorStateHasher;
        [SerializeField] private AnimatorParametersHasher _parametersHasher;
        public AnimatorState State { get; private set; }
        
        public event Action<AnimatorState> StateEntered;
        public event Action<AnimatorState> StateExited;

        private ITransformationCalculator _transformationCalculator;
        private IInputService _inputService;
        private IWeaponSwitchHandler _weaponSwitchHandler;

        private Vector2 _movementJoystickAxis;
        private Vector2 _aimJoystickInput;

        private const string HUD = "HUD";
        
        public void Initialize(
            ITransformationCalculator transformationCalculator,
            IInputService inputService)
        {
            _transformationCalculator = transformationCalculator;
            _inputService = inputService;
        }   
        
        private void Awake()
        {
            // should be refactored - try using Signals from Zenject / or better approach would be inject service
            _weaponSwitchHandler = GameObject.FindWithTag(HUD).GetComponentInChildren<WeaponSwitchHandler>(); 
            _animatorStateHasher.Init();
            _parametersHasher.Init();
        }

        private void OnEnable()
        {
            _weaponSwitchHandler.OnSwitchButtonClick += SwitchWeaponTrigger;
        }

        private void OnDestroy()
        {
            _weaponSwitchHandler.OnSwitchButtonClick -= SwitchWeaponTrigger;
        }

        public void EnteredState(int stateHash)
        {
            State = StateFor(stateHash);
            StateEntered?.Invoke(State);
        }

        public void ExitedState(int stateHash) => 
            StateExited?.Invoke(StateFor(stateHash));

        public void Tick()
        {
            _movementJoystickAxis = _inputService.MovementAxis();
            
            AnimateMovement(_transformationCalculator.CalculateInputDirection(_movementJoystickAxis));
        }
        
        public void PlayRotation(float animatorTurnSpeed) => 
            _animator.SetFloat(_parametersHasher.TurnSpeedHash, animatorTurnSpeed);

        public void PlayShooting(bool isEnabled) => 
            _animator.SetBool(_parametersHasher.IsShootingHash, isEnabled);

        public void ResetMovement()
        {
            _animator.SetFloat(_parametersHasher.ForwardSpeedHash, 0);
            _animator.SetFloat(_parametersHasher.SideSpeedHash, 0);
        }

        public void ResetStandingTurn() => 
            _animator.SetFloat(_parametersHasher.TurnSpeedHash, 0);

        private void SwitchWeaponTrigger() => 
            _animator.SetTrigger(_parametersHasher.SwitchWeaponHash);
        
        private void AnimateMovement(Vector3 moveDirection)
        {
            float forward = Vector3.Dot(moveDirection, transform.forward);
            float right = Vector3.Dot(moveDirection, transform.right);
            
            _animator.SetFloat(_parametersHasher.ForwardSpeedHash, forward);
            _animator.SetFloat(_parametersHasher.SideSpeedHash, right);
        }

        private AnimatorState StateFor(int stateHash)
        {
            AnimatorState state;
            if (stateHash == _animatorStateHasher.IdleHash)
                state = AnimatorState.Idle;
            else if (stateHash == _animatorStateHasher.SwitchWeaponHash)
                state = AnimatorState.SwitchWeapon;
            else
                state = AnimatorState.Unknown;

            return state;
        }

    }
}