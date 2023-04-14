using UnityEngine;

namespace Code.Entities.EntitiesTransformation.Calculations
{
    public interface ITransformationCalculator
    {
        Quaternion CharacterRotation { get; set; }
        Vector3 CalculateInputDirection(Vector3 inputValue);
        float CalculateCurrentTurnSpeed(Vector3 targetDirection, Quaternion prevRotation, Transform characterTransform);
    }
}