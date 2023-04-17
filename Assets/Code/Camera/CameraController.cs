using System;
using Code.Entities.PlayerEntity;
using Code.Infrastructure.Services.Input;
using UnityEngine;
using Zenject;

namespace Code.Camera
{
    public class CameraController : MonoBehaviour, ICameraController
    {
        [SerializeField] private float _turnSpeed;
        
        private Player _followTarget;
        private IInputService _inputService;
        private Vector3 _movementInput;
        
        [Inject]
        public void Construct(IInputService inputService)
        {
            _inputService = inputService;
        }

        private void Update()
        {
            _movementInput = _inputService.MovementAxis();
        }

        private void FixedUpdate()
        {
            if (_followTarget == null)
            {
                _followTarget = FindObjectOfType<Player>();
                return;
            }
            
            AddYawInput(_movementInput.x);
            FollowTarget(_followTarget.transform.position);
        }

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