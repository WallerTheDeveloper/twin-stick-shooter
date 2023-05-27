using Code.UI;
using Code.UI.Menu;
using Zenject;

namespace Code.Infrastructure.Installers
{
    public class UIInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<MainMenuView>().AsSingle();
            // Container.Bind<IWeaponSwitchHandler>().To<WeaponSwitchHandler>().AsSingle().NonLazy();
        }
    }
}