using UnityEngine;

namespace Code.Camera
{
    [ExecuteAlways]
    public class CameraArm : MonoBehaviour
    {
        [SerializeField] private float _armLength;
        [SerializeField] private Transform _cameraTransform;

        private void Update()
        {
            _cameraTransform.position = transform.position - _cameraTransform.forward * _armLength;
        }
    }
}