using Code.Entities;
using Code.Entities.EnemyEntity;
using Code.Entities.Factories;
using Code.Infrastructure.Services.AssetsManagement;
using UnityEngine;
using Zenject;

namespace Code.Infrastructure.Installers
{
    public class EnemyInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            //should be moved to SpawnerInstaller?
            Container.BindFactory<EnemySpawner, EnemySpawner.Factory>()
                .FromComponentInNewPrefabResource(AssetPath.SPAWNER_PATH);

            Container.BindFactory<GameObject, GameObject, Enemy, Enemy.Factory>()
                .FromComponentInNewPrefabResource(AssetPath.ENEMYBASE_PATH);
        }
    }
}