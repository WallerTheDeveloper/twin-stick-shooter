using UnityEngine;

namespace Code.Entities.EntitiesTransformation.Calculations
{
    public class TransformationCalculator : ITransformationCalculator
    {
        public Quaternion CharacterRotation { get; set; }

        public Vector3 CalculateInputDirection(Vector3 inputValue)
        {
            Vector3 targetDirection = UnityEngine.Camera.main.transform.TransformDirection(inputValue);
            targetDirection.y = 0;
            targetDirection.Normalize();
            
            return targetDirection;
        }
        
        public float CalculateCurrentTurnSpeed(Vector3 targetDirection, Quaternion prevRotation, Transform characterTransform)
        {
            float currentTurnSpeed = 0;
            
            Quaternion currentRotation = characterTransform.rotation;
            
            float turnDirection = Vector3.Dot(targetDirection, characterTransform.right) > 0 ? 1 : -1;
            float rotationDelta = Quaternion.Angle(prevRotation, currentRotation) * turnDirection;
            
            currentTurnSpeed = rotationDelta / Time.deltaTime;
            
            return currentTurnSpeed;
        }
    }
}