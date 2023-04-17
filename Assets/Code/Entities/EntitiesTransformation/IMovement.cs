using UnityEngine;

namespace Code.Entities.EntitiesTransformation
{
    public interface IMovement
    {
        float MovementSpeed { get; set; }
        void Move(Vector3 destination, float speed);
    }
}