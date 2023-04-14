using Code.Infrastructure.Services.AssetsManagement;
using Code.UI;
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

            var switchButton = hud.gameObject.GetComponentInChildren<WeaponSwitchHandler>();

            Container
                .Bind<IWeaponSwitchHandler>()
                .To<WeaponSwitchHandler>()
                .FromInstance(switchButton)
                .AsSingle();
            Object.DontDestroyOnLoad(hud.gameObject);
        }
    }
}