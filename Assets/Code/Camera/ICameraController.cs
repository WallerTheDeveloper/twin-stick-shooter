using UnityEngine;

namespace Code.Camera
{
    public interface ICameraController
    {
        void AddYawInput(float amount);
        void FollowTarget(Vector3 transformPosition);
    }
}