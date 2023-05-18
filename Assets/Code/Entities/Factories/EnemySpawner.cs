using Code.Entities.EnemyEntity.Patrol;
using Code.Infrastructure.Services.Data;
using Code.StaticData.Enemies;
using UnityEngine;
using Zenject;

namespace Code.Entities.Factories
{
    public class EnemySpawner : MonoBehaviour
    {
        public EnemyTypeId enemyType;
        [SerializeField] private float _spawnInterval;
        
        private IEnemyFactory _enemyFactory;
        private IStaticDataService _staticDataService;

        private float _timer;

        // [Inject]
        // public void Construct(IStaticDataService staticDataService)
        // {
            // _staticDataService = staticDataService;
        // }
        
        private void Awake()
        {
            //should be refactored!
            _staticDataService = new StaticDataService();
            _staticDataService.LoadStaticData();
            
            _enemyFactory = new EnemyFactory(_staticDataService);
        }

        private void Update()
        {
            _timer += Time.deltaTime;
            if (_timer >= _spawnInterval)
            {
                _enemyFactory.Create(enemyType, transform.position);
                _timer = 0;
            }             
        }
    }
}