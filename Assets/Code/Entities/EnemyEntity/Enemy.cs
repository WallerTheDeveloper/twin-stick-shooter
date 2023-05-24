using System;
using Code.Entities.EnemyEntity.Attack;
using Code.Entities.EnemyEntity.Data;
using Code.Entities.EnemyEntity.Death;
using Code.Entities.EnemyEntity.Patrol;
using Code.Entities.EntitiesTransformation;
using Code.Entities.PlayerEntity;
using Code.Entities.StateMachine;
using Code.Entities.StateMachine.States.EnemyStates;
using Code.Misc;
using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;
using Zenject;

namespace Code.Entities.EnemyEntity
{
    public class Enemy : SerializedMonoBehaviour, IHostile
    {
        // [field: SerializeField] private PatrolPath PatrolPath { get; set; }
        [SerializeField] private AISettings _settings;
            
        private IStateMachine _stateMachine;
        private NavMeshAgent _navMeshAgent;
        private IMovement _movement;
        private Transform _targetTransform;
        private IPatrolBehaviour _patrolBehaviour;
        private IAttackBehaviour _attackBehaviour;
        private ITimerUpdater _timerUpdater;
        private IDeathComponent _deathComponent;
        private ObjectFinder _objectFinder;
        private PatrolPath _assignedPatrolPath;
        
        public IHealth Health;
        public Transform EntityTransform => transform;

        public Transform TargetTransform => _targetTransform;

        [Inject]
        public void Construct(Player player)
        {
            _targetTransform = player.EntityTransform;
        }
        
        public void Initialize()
        {
            GetComponents();

            _assignedPatrolPath = _objectFinder.FindClosestObjectOfType<PatrolPath>();
            
            _timerUpdater = new TimerUpdater();
            
            _movement = new NavMeshMovement(_navMeshAgent);
            _patrolBehaviour = new PatrolBehaviour(this, _assignedPatrolPath, _settings, _movement, _timerUpdater);
            _attackBehaviour = new AttackBehaviour(this, _settings);
            
            Health = new Health(15f);
            
            //move to MonsterStaticData? (on second thought - to achieve what?)
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
            _deathComponent = GetComponent<DeathComponent>();
            _objectFinder = GetComponent<ObjectFinder>();
        }
        
        private void CreateStates()
        {
            var enemyPursueState = new EnemyPursueState(_targetTransform, _timerUpdater, _movement);
            var enemyPatrolState = new EnemyPatrolState(_patrolBehaviour);
            var enemySuspiciousState = new EnemySuspiciousState(_movement, _settings, _timerUpdater);
            var enemyAttackState = new EnemyAttackState(_attackBehaviour);
            var enemyDeathState = new EnemyDeathState(_deathComponent);
            
            RegisterTransition(enemyPatrolState, enemyPursueState, () => InAttackRange()());
            
            RegisterTransition(enemyPursueState, enemySuspiciousState, () => !InAttackRange()());
            RegisterTransition(enemyPursueState, enemyAttackState, () => CanAttackTarget()() && ReachedTarget()());
             
            RegisterTransition(enemyAttackState, enemyPursueState, () => InAttackRange()() && !ReachedTarget()());
            
            RegisterTransition(enemySuspiciousState, enemyPatrolState, () => !InAttackRange()() && !CurrentlySuspecting()());
            RegisterTransition(enemySuspiciousState, enemyPursueState, () => InAttackRange()() && !CurrentlySuspecting()());

            RegisterFromAnyTransition(enemyDeathState, IsDead());
            
            _stateMachine.Enter(enemyPatrolState);

            Func<bool> InAttackRange() => () => _attackBehaviour.IsInRange(_settings.ChaseDistance);
            Func<bool> CurrentlySuspecting() => () => _timerUpdater.IsSuspecting;
            Func<bool> ReachedTarget() => () => _attackBehaviour.IsInRange(_settings.AttackDistance);
            Func<bool> CanAttackTarget() => () => _attackBehaviour.CanAttack(_targetTransform.gameObject);
            Func<bool> IsDead() => () => Health.IsDead();
        }
        
        private void RegisterTransition(IEntityState from, IEntityState to, Func<bool> condition) => 
            _stateMachine.AddTransition(from, to, condition);

        private void RegisterFromAnyTransition(IEntityState entityState, Func<bool> predicate) =>
            _stateMachine.AddAnyTransition(entityState, predicate);
        
        private void OnDrawGizmosSelected() {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(transform.position, _settings.ChaseDistance);
        }
        public class Factory : PlaceholderFactory<GameObject, GameObject, Enemy>
        {
            private readonly DiContainer _container;
            public Factory(DiContainer container)
            {
                _container = container;
            }

            public override Enemy Create(GameObject parent, GameObject child)
            {
                GameObject mainPrefab = _container.InstantiatePrefab(parent);
                GameObject childPrefab = _container.InstantiatePrefab(child, mainPrefab.transform);

                return mainPrefab.GetComponent<Enemy>();
            }
        }
    }
}