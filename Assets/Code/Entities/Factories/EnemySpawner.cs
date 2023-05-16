using System;
using Code.Entities.EnemyEntity;
using Code.Infrastructure.Services.AssetsManagement;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Code.Entities.Factories
{
    public class EnemySpawner : SerializedMonoBehaviour
    {
        [field: SerializeField] private IHostile _hostileEntity { get; set; }
        [SerializeField] private float _spawnInterval;
        
        private IEntityFactory _enemyFactory;
        private IAssets _assets;

        private float timer;

        private void Awake()
        {
            _assets = new AssetsProvider();
            _enemyFactory = new EnemyFactory(_assets);
        }

        private void Update()
        {
            timer += Time.deltaTime;
            if (timer >= _spawnInterval)
            {
                _enemyFactory.GetEntity(_hostileEntity.PrefabPath, transform.position);
                timer -= _spawnInterval;
            }
        }
    }
}