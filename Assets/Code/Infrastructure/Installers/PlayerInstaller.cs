using Code.Camera;
using Code.Player;
using Code.Services.Input;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

namespace Code.Infrastructure.Installers
{
    public class PlayerInstaller : MonoInstaller
    {
        public GameObject PlayerPrefab;
        public Transform PlayerStartPoint;
        
        public override void InstallBindings()
        {
            Container.Bind<IInputService>().To<InputService>().AsSingle();
            Container.Bind<ICameraController>().To<CameraController>().FromComponentInHierarchy().AsSingle();


        }

        public override void Start()
        {
            InstallPlayer();
        }
        
        private void InstallPlayer()
        {
            PlayerController playerController =
                Container.InstantiatePrefabForComponent<PlayerController>(PlayerPrefab, PlayerStartPoint.position, Quaternion.identity,
                    null);
            Container
                .Bind<PlayerController>()
                .FromInstance(playerController)
                .AsSingle();
        }
    }
}