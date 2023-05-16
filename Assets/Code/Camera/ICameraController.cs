using Code.Infrastructure.Services.Input;
using UnityEngine;

namespace Code.Camera
{
    public interface ICameraController
    {
        void Initialize(IInputService inputService);
        // void AddYawInput(float amount);
        // void FollowTarget(Vector3 transformPosition);
    }
}