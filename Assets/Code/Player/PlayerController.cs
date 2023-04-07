using System.Numerics;
using Code.Camera;
using Code.Infrastructure.Services.Input;
using UnityEngine;
using UnityEngine.AI;
using Zenject;
using Quaternion = UnityEngine.Quaternion;
using Vector3 = UnityEngine.Vector3;

namespace Code.Player
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField]
        private float _turnSpeed;

        [SerializeField]
        private float _turnAnimationSpeed;
        
        private IInputService _inputService;
        private ICameraController _cameraController;

        private Vector3 _aimInput;
        private Vector3 _movementInput;
        private NavMeshAgent _navMeshAgent;

        private Animator _animator;

        private UnityEngine.Camera _mainCamera;
        
        private float _animatorTurnSpeed;
        
        [Inject]
        public void Construct(IInputService inputService, ICameraController cameraController)
        {
            _inputService = inputService;
            _cameraController = cameraController;
        }

        private void Awake()
        {
            _animator = GetComponent<Animator>();
            _navMeshAgent = GetComponent<NavMeshAgent>();
            _mainCamera = UnityEngine.Camera.main;
        }

        private void FixedUpdate()
        {
            MovePlayer();

            RotatePlayer();
        
            CameraMovement();
        }

        private void Update()
        {
            _aimInput = _inputService.AimAxis();
            _movementInput = _inputService.MovementAxis();
        }

        private void CameraMovement()
        {
            _cameraController.AddYawInput(_movementInput.x);
            _cameraController.FollowTarget(transform.position);
        }

        private void MovePlayer()
        {
            Vector3 moveDirection = Vector3.zero;

            if (_movementInput.sqrMagnitude > Constants.Epsilon)
            {
                moveDirection = CalculateInputDirection(_movementInput);
            }
            
            _navMeshAgent.Move(_navMeshAgent.speed * moveDirection * Time.deltaTime);

            AnimateMovement(moveDirection);
        }

        private void RotatePlayer()
        {
            float currentTurnSpeed = 0;
            
            if (_aimInput.sqrMagnitude > Constants.Epsilon)
            {
                Quaternion prevRotation = transform.rotation;

                var turnAlpha = _turnSpeed * Time.deltaTime;
                var targetDirection = CalculateInputDirection(_aimInput);

                transform.rotation = Quaternion.Lerp(transform.rotation,
                    Quaternion.LookRotation(targetDirection, Vector3.up), turnAlpha);
                
                currentTurnSpeed = CalculateCurrentTurnSpeed(targetDirection, prevRotation);
            }
            AnimateStandingRotation(currentTurnSpeed);
        }

        private void AnimateStandingRotation(float currentTurnSpeed)
        {
            _animatorTurnSpeed = Mathf.Lerp(_animatorTurnSpeed, currentTurnSpeed, Time.deltaTime * _turnAnimationSpeed);
            _animator.SetFloat("turnSpeed", _animatorTurnSpeed);
        }

        private float CalculateCurrentTurnSpeed(Vector3 targetDirection, Quaternion prevRotation)
        {
            float currentTurnSpeed = 0;
            
            Quaternion currentRotation = transform.rotation;
            
            float turnDirection = Vector3.Dot(targetDirection, transform.right) > 0 ? 1 : -1;
            float rotationDelta = Quaternion.Angle(prevRotation, currentRotation) * turnDirection;
            
            currentTurnSpeed = rotationDelta / Time.deltaTime;
            
            return currentTurnSpeed;
        }

        private Vector3 CalculateInputDirection(Vector3 inputValue)
        {
            Vector3 targetDirection = _mainCamera.transform.TransformDirection(inputValue);
            targetDirection.y = 0;
            targetDirection.Normalize();
            
            return targetDirection;
        }

        private void AnimateMovement(Vector3 moveDirection) // refactor later
        {
            float forward = Vector3.Dot(moveDirection, transform.forward);
            float right = Vector3.Dot(moveDirection, transform.right);

            _animator.SetFloat("forwardSpeed", forward);
            _animator.SetFloat("sideSpeed", right);
        }
    }
}