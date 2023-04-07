using Code.Camera;
using Code.Infrastructure.Services.AssetsManagement;
using Code.Infrastructure.Services.Input;
using Code.Player;
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
            Container.Bind<ICameraController>().To<CameraController>().FromComponentInHierarchy().AsSingle();
        }

        public override void Start()
        {
            _assets = new AssetsProvider();
            InstallPlayer();
        }
        
        private void InstallPlayer()
        {

            var playerPrefab = _assets.GetAsset<GameObject>(AssetPath.PLAYER_PATH);
            
            var playerController =
                Container.InstantiatePrefabForComponent<PlayerController>(
                    playerPrefab, 
                    PlayerStartPoint.position,
                    Quaternion.identity,
                    null);
            
            Container
                .Bind<PlayerController>()
                .FromInstance(playerController)
                .AsSingle();
        }
    }
}