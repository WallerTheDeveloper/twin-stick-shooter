using UnityEngine;

namespace Code.Entities.EntitiesTransformation
{
    public interface IRotation
    {
        float TurnSpeed { get; set; }
        void RotateTowardsInputDirection(Transform characterTransform);
    }
}