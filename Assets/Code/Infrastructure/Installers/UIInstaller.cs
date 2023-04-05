using Code.Infrastructure.Services.AssetsManagement;
using UnityEngine;
using Zenject;

namespace Code.Infrastructure.Installers
{
    public class UIInstaller : MonoInstaller
    {
        private IAssets _assets;

        public override void InstallBindings()
        {
            
        }

        public void Awake()
        {
            _assets = new AssetsProvider();
            InstallHUD();
        }

        private void InstallHUD()
        {
            var hudPrefab = _assets.GetAsset<GameObject>(AssetPath.HUD_PATH);
            var hud = Container.InstantiatePrefab(hudPrefab);
            Object.DontDestroyOnLoad(hud.gameObject);
        }
    }
}