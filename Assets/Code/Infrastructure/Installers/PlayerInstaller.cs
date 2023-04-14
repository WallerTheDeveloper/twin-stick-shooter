using Code.Camera;
using Code.Entities.EntitiesTransformation.Calculations;
using Code.Entities.Player;
using Code.Infrastructure.Services.AssetsManagement;
using Code.Infrastructure.Services.Input;
using Code.UI;
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
            Container.Bind<IInputService>().To<InputService>().AsSingle();
            Container.Bind<ITransformationCalculator>().To<TransformationCalculator>().AsSingle();
        }

        public override void Start()
        {
            _assets = new AssetsProvider();
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