using Code.Entities.EntitiesTransformation.Calculations;
using Code.Infrastructure.Services.Input;
using UnityEngine;

namespace Code.Entities.EntitiesTransformation
{
    public class CharacterMovement : IMovement
    {
        private readonly CharacterController _characterController;
        private readonly ITransformationCalculator _transformationCalculator;

        public float MovementSpeed { get; set; }

        public CharacterMovement(
            CharacterController characterController,
            ITransformationCalculator transformationCalculator)
        {
            _characterController = characterController;
            _transformationCalculator = transformationCalculator;
        }

        public void Move(Vector3 destination, float speed)
        {
            Vector3 moveDirection = Vector3.zero;
            
            if (destination.sqrMagnitude > Constants.Epsilon)
            {
                moveDirection = _transformationCalculator.CalculateInputDirection(destination);
            }
            
            float joystickRelativeHeroSpeed = destination.magnitude * speed;
            _characterController.Move(joystickRelativeHeroSpeed * moveDirection * Time.deltaTime);
            
        }

    }
}