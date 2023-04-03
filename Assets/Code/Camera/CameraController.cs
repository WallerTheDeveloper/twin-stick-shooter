using UnityEngine;
using Zenject;

namespace Code.Camera
{
    public class CameraController : MonoBehaviour, ICameraController
    {
        [SerializeField]
        private float _turnSpeed;

        public void AddYawInput(float amount)
        {
            transform.Rotate(Vector3.up, amount * Time.deltaTime * _turnSpeed);
        }

        public void FollowTarget(Vector3 transformPosition)
        {
            transform.position = transformPosition;
        }
    }
}