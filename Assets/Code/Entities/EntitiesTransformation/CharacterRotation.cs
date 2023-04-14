using Code.Animator;
using Code.Animator.Logic;
using Code.Entities.EntitiesTransformation.Calculations;
using Code.Infrastructure.Services.Input;
using UnityEngine;

namespace Code.Entities.EntitiesTransformation
{
    public class CharacterRotation : IRotation
    {
        private readonly IInputService _inputService;
        private readonly ITransformationCalculator _transformationCalculator;
        private readonly IAnimationPlayer _animationPlayer;
        
        private float _animatorTurnSpeed;
        public float TurnSpeed { get; set; }

        public CharacterRotation(
            IInputService inputService, 
            ITransformationCalculator transformationCalculator,
            IAnimationPlayer animationPlayer)
        {
            _inputService = inputService;
            _transformationCalculator = transformationCalculator;
            _animationPlayer = animationPlayer;
        }

        public void RotateTowardsInputDirection(Transform characterTransform)
        {
            Vector2 aimJoystickInput = _inputService.AimAxis();

            float currentTurnSpeed = 0;
            
            if (aimJoystickInput.sqrMagnitude > Constants.Epsilon)
            {
                Quaternion prevRotation = characterTransform.rotation;
                
                var turnAlpha = TurnSpeed * Time.deltaTime;
                var targetDirection = _transformationCalculator.CalculateInputDirection(aimJoystickInput);

                characterTransform.rotation = Quaternion.Lerp(characterTransform.rotation,
                    Quaternion.LookRotation(targetDirection, Vector3.up), turnAlpha);
                
                currentTurnSpeed = _transformationCalculator.CalculateCurrentTurnSpeed(targetDirection, prevRotation, characterTransform);

                _animationPlayer.PlayShooting(true);
            }
            
            _animatorTurnSpeed = Mathf.Lerp(_animatorTurnSpeed, 
                currentTurnSpeed,
                Time.deltaTime * 1.0f);
            _animationPlayer.PlayRotation(_animatorTurnSpeed);
        }
    }
}