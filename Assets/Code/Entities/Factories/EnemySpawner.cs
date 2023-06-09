﻿using Code.Entities.EnemyEntity;
using Code.Entities.EnemyEntity.Patrol;
using Code.Infrastructure.Services.Data;
using Code.StaticData.Enemies;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

namespace Code.Entities.Factories
{
    public class EnemySpawner : MonoBehaviour
    {
        // public GameObject Model;
        public EnemyTypeId EnemyType;
        [SerializeField] private float _spawnInterval;
        [SerializeField] private float _spawnPositionCoefficient; // change name later
        [SerializeField] private int _maxMonsterAmount; // change carefully - every enemy must have assigned unique patrol path
        
        private IEnemyFactory _enemyFactory;
        private IStaticDataService _staticDataService;
        private Enemy.Factory _factory;

        private float _timer;
        private int _spawnedAmount = 0;
        
        [Inject]
        public void Construct(Enemy.Factory enemyFactory, IStaticDataService staticDataService)
        {
            _factory = enemyFactory;
            _staticDataService = staticDataService;
        }
        private void Awake()
        {
            _enemyFactory = new EnemyFactory(_factory, _staticDataService);
        }

        private void Update()
        {
            _timer += Time.deltaTime;
            if (_timer >= _spawnInterval)
            {

                if (_spawnedAmount >= _maxMonsterAmount) return;

                _spawnedAmount++;
                
                _enemyFactory.Create(EnemyType, transform, _spawnPositionCoefficient);
                
                _timer = 0;
            }             
        }
        public class Factory : PlaceholderFactory<EnemySpawner>
        {
        }
    }
}