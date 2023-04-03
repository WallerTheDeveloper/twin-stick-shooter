using Code.Camera;
using Code.Services.Input;
using UnityEngine;
using UnityEngine.AI;
using Zenject;

namespace Code.Player
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField]
        private float _turnSpeed;

        private IInputService _inputService;
        private ICameraController _cameraController;

        private Vector3 _aimInput;
        private Vector3 _movementInput;
        private NavMeshAgent _navMeshAgent;

        private Animator _animator;

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
            if (_aimInput.sqrMagnitude > Constants.Epsilon)
            {
                RotatePlayer();
            }
            
            _cameraController.AddYawInput(_movementInput.x);
            _cameraController.FollowTarget(transform.position);
        }

        private void Update()
        {
            _aimInput = _inputService.AimAxis();
            _movementInput = _inputService.MovementAxis();
        }

        private void RotatePlayer()
        {
            // Calculate the target direction based on the input and the camera's orientation
            // var linearVectorCombination = _aimInput.x * Camera.main.transform.right + _aimInput.y * Camera.main.transform.forward;
            
            var targetDirection = CalculateInputDirection(_aimInput);

            // Calculate the rotation angle between the current forward direction and the target direction
            float angle = Vector3.SignedAngle(transform.forward, targetDirection, Vector3.up);

            transform.rotation = Quaternion.Lerp(transform.rotation, transform.rotation * Quaternion.Euler(0, angle, 0), _turnSpeed * Time.deltaTime);
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