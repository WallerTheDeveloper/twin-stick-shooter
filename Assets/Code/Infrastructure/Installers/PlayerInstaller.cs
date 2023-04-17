using Code.Entities.EntitiesTransformation.Calculations;
using Code.Entities.PlayerEntity;
using Code.Infrastructure.Services.AssetsManagement;
using Code.Infrastructure.Services.Input;
using UnityEngine;
using Zenject;

namespace Code.Infrastructure.Installers
{
    public class PlayerInstaller : MonoInstaller
    {
        [SerializeField] private Transform PlayerStartPoint;
        
        private IAssets _assets; 
        
        public override void InstallBindings()
        {
            _assets = new AssetsProvider();
            
            Container.Bind<IInputService>().To<InputService>().AsSingle();
            Container.Bind<ITransformationCalculator>().To<TransformationCalculator>().AsSingle();
            
            InstallPlayer();
        }
        
        private void InstallPlayer()
        {
            var playerPrefab = _assets.GetAsset<GameObject>(AssetPath.PLAYER_PATH);
            
            var playerComponent =
                Container.InstantiatePrefabForComponent<Player>(
                    playerPrefab, 
                    PlayerStartPoint.position,
                    Quaternion.identity,
                    null);
            
            Container
                .Bind<Player>()
                .FromInstance(playerComponent)
                .AsSingle();
        }
    }
}