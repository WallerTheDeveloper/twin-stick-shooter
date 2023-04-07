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
        }

        private void FixedUpdate()
        {
            MovePlayer();

            RotatePlayer();
        
            _cameraController.AddYawInput(_movementInput.x);
            _cameraController.FollowTarget(transform.position);
        }

        private void Update()
        {
            _aimInput = _inputService.AimAxis();
            _movementInput = _inputService.MovementAxis();
        }

        // private void RotatePlayer(out float currentTurnSpeed)
        // {
        //     // Calculate the target direction based on the input and the camera's orientation
        //     // var linearVectorCombination = _aimInput.x * Camera.main.transform.right + _aimInput.y * Camera.main.transform.forward;
        //     
        //     var targetDirection = CalculateInputDirection(_aimInput);
        //
        //     Quaternion prevRotation = transform.rotation;
        //
        //     var turnAlpha = _turnSpeed * Time.deltaTime;
        //     
        //     float angle = Vector3.SignedAngle(transform.forward, targetDirection, Vector3.up);
        //     var lookRotation = transform.rotation * Quaternion.Euler(0, angle, 0);
        //
        //     transform.rotation = Quaternion.Lerp(transform.rotation, lookRotation, turnAlpha);
        //
        //     Quaternion currentRotation = transform.rotation;
        //     float direction = Vector3.Dot(targetDirection, transform.right) > 0 ? 1 : -1;
        //     float rotationDelta = Quaternion.Angle(prevRotation, currentRotation) * direction;
        //     currentTurnSpeed = rotationDelta / Time.deltaTime;
        // }

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
                
                Quaternion currentRotation = transform.rotation;
                float Dir = Vector3.Dot(targetDirection, transform.right) > 0 ? 1 : -1;
                float rotationDelta = Quaternion.Angle(prevRotation, currentRotation) * Dir;
                currentTurnSpeed = rotationDelta / Time.deltaTime;
            }
            _animatorTurnSpeed = Mathf.Lerp(_animatorTurnSpeed, currentTurnSpeed, Time.deltaTime * _turnAnimationSpeed);
            _animator.SetFloat("turnSpeed", _animatorTurnSpeed);
        }

        private Vector3 CalculateInputDirection(Vector3 inputValue)
        {
            Vector3 targetDirection = UnityEngine.Camera.main.transform.TransformDirection(inputValue);
            targetDirection.y = 0;
            targetDirection.Normalize();
            
            return targetDirection;
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

        private void AnimateMovement(Vector3 moveDirection) // refactor later
        {
            float forward = Vector3.Dot(moveDirection, transform.forward);
            float right = Vector3.Dot(moveDirection, transform.right);

            _animator.SetFloat("forwardSpeed", forward);
            _animator.SetFloat("sideSpeed", right);
        }
    }
}