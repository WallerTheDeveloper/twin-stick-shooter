using System;
using Code.Entities.EnemyEntity.Patrol;
using Code.Entities.EnemyEntity.Patrol.Data;
using Code.Entities.EntitiesTransformation;
using Code.Entities.PlayerEntity;
using Code.Entities.StateMachine;
using Code.Entities.StateMachine.States.EnemyStates;
using UnityEngine;
using UnityEngine.AI;
using Zenject;

namespace Code.Entities.EnemyEntity
{
    public class Enemy : MonoBehaviour, IEntity
    {
        [SerializeField] private float _movementSpeed;
        [SerializeField] private PatrolPath _patrolPath;
        [SerializeField] private PatrolBehaviourSettings _patrolBehaviourSettings;
        
        private IStateMachine _stateMachine;
        private NavMeshAgent _navMeshAgent;
        private IMovement _movement;
        private Transform _targetTransform;
        private IPatrolBehaviour _patrolBehaviour;

        public Transform EntityTransform => transform;

        [Inject]
        public void Construct(Player player)
        {
            _targetTransform = player.EntityTransform;
        }
        
        private void Awake()
        {
            GetComponents();
            
            _movement = new NavMeshMovement(_navMeshAgent);
            _patrolBehaviour = new PatrolBehaviour(this, _patrolPath, _patrolBehaviourSettings, _movement);

            _movement.MovementSpeed = _movementSpeed;
            
            _stateMachine = new EntityStateMachine();
            
            CreateStates();
        }

        private void Update()
        {
            _stateMachine.Tick();
        }

        private void GetComponents()
        {
            _navMeshAgent = GetComponent<NavMeshAgent>();
        }
        private void CreateStates()
        {
            var enemyIdleState = new EnemyIdleState();
            var enemyPursueState = new EnemyPursueState(_targetTransform, _movement);
            var enemyPatrolState = new EnemyPatrolState(_patrolBehaviour);
            
            StateTransit(enemyIdleState, enemyPatrolState, SpottedTarget());

            _stateMachine.Enter(enemyIdleState);
            
            Func<bool> SpottedTarget() => () => true;
        }

        private void StateTransit(IEntityState from, IEntityState to, Func<bool> condition) => 
            _stateMachine.AddTransition(from, to, condition);
    }
}