using Code.Entities.EntitiesTransformation.Calculations;
using Code.Infrastructure.Services.Input;
using UnityEngine;

namespace Code.Entities.EntitiesTransformation
{
    public class CharacterMovement : IMovement
    {
        private readonly CharacterController _characterController;
        private readonly IInputService _inputService;
        private readonly ITransformationCalculator _transformationCalculator;

        public float MovementSpeed { get; set; }

        public CharacterMovement(
            CharacterController characterController,
            IInputService inputService,
            ITransformationCalculator transformationCalculator)
        {
            _characterController = characterController;
            _inputService = inputService;
            _transformationCalculator = transformationCalculator;
        }
        public void Move()
        {
            Vector3 movementInput = _inputService.MovementAxis();
            
            Vector3 moveDirection = Vector3.zero;
            
            if (movementInput.sqrMagnitude > Constants.Epsilon)
            {
                moveDirection = _transformationCalculator.CalculateInputDirection(movementInput);
            }

            float joystickRelativeHeroSpeed = movementInput.magnitude * MovementSpeed;
            _characterController.Move(joystickRelativeHeroSpeed * moveDirection * Time.deltaTime);
            
        }

    }
}