using System;
using Code.Entities.EnemyEntity.Attack;
using Code.Entities.EnemyEntity.Data;
using Code.Entities.EnemyEntity.Patrol;
using Code.Entities.EntitiesTransformation;
using Code.Entities.PlayerEntity;
using Code.Entities.StateMachine;
using Code.Entities.StateMachine.States.EnemyStates;
using Code.Extensions;
using UnityEngine;
using UnityEngine.AI;
using Zenject;

namespace Code.Entities.EnemyEntity
{
    public class Enemy : MonoBehaviour, IEntity
    {
        [SerializeField] private AISettings _settings;
        [SerializeField] private PatrolPath _patrolPath;

        private IStateMachine _stateMachine;
        private NavMeshAgent _navMeshAgent;
        private IMovement _movement;
        private Transform _targetTransform;
        private IPatrolBehaviour _patrolBehaviour;
        private IAttackBehaviour _attackBehaviour;
        private ITimerUpdater _timerUpdater;
        
        public Transform EntityTransform => transform;
        public Transform TargetTransform => _targetTransform;

        [Inject]
        public void Construct(Player player)
        {
            _targetTransform = player.EntityTransform;
        }
        private void Awake()
        {
            GetComponents();

            _timerUpdater = new TimerUpdater();
            
            _movement = new NavMeshMovement(_navMeshAgent);
            _patrolBehaviour = new PatrolBehaviour(this, _patrolPath, _settings, _movement, _timerUpdater);
            _attackBehaviour = new AttackBehaviour(this, _settings);
            
            _movement.MovementSpeed = _settings.MovementSpeed;
            
            _stateMachine = new EntityStateMachine();
            
            CreateStates();
        }
        private void Update()
        {
            _stateMachine.Tick();
            _timerUpdater.UpdateTimers();
        }
        private void GetComponents()
        {
            _navMeshAgent = GetComponent<NavMeshAgent>();
        }
        private void CreateStates()
        {
            var enemyPursueState = new EnemyPursueState(_targetTransform, _timerUpdater, _movement);
            var enemyPatrolState = new EnemyPatrolState(_patrolBehaviour);
            var enemySuspiciousState = new EnemySuspiciousState(_movement, _settings, _timerUpdater);
            var enemyAttackState = new EnemyAttackState(_attackBehaviour);
            
            RegisterTransition(enemyPatrolState, enemyPursueState, () => InAttackRange()());
            
            RegisterTransition(enemyPursueState, enemySuspiciousState, () => !InAttackRange()());
            RegisterTransition(enemyPursueState, enemyAttackState, () => CanAttackTarget()() && ReachedTarget()());
             
            RegisterTransition(enemyAttackState, enemyPursueState, () => InAttackRange()() && !ReachedTarget()());
            
            RegisterTransition(enemySuspiciousState, enemyPatrolState, () => !InAttackRange()() && !CurrentlySuspecting()());
            RegisterTransition(enemySuspiciousState, enemyPursueState, () => InAttackRange()() && !CurrentlySuspecting()());

            
            _stateMachine.Enter(enemyPatrolState);

            Func<bool> InAttackRange() => () => _attackBehaviour.IsInRange(_settings.ChaseDistance);
            Func<bool> CurrentlySuspecting() => () => _timerUpdater.IsSuspecting;
            Func<bool> ReachedTarget() => () => _attackBehaviour.IsInRange(_settings.AttackDistance);
            Func<bool> CanAttackTarget() => () => _attackBehaviour.CanAttack(_targetTransform.gameObject);
            
        }
        private void RegisterTransition(IEntityState from, IEntityState to, Func<bool> condition) => 
            _stateMachine.AddTransition(from, to, condition);
        private void OnDrawGizmosSelected() {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(transform.position, _settings.ChaseDistance);
        }
    }
}