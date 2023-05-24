using Code.Entities;
using Code.Entities.PlayerEntity;
using Code.Infrastructure.Services.AssetsManagement;
using UnityEngine;
using Zenject;

namespace Code.Infrastructure.Installers
{
    public class PlayerInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.BindFactory<GameObject, Transform, Player, Player.Factory>()
                .FromComponentInNewPrefabResource(AssetPath.PLAYER_PATH);
            
            // Container.Bind<Player>().FromNewComponentOnNewGameObject().AsSingle();
        }
    }
}