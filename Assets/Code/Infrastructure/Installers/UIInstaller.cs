using Code.UI;
using Zenject;

namespace Code.Infrastructure.Installers
{
    public class UIInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            // Container.Bind<IWeaponSwitchHandler>().To<WeaponSwitchHandler>().AsSingle().NonLazy();
        }
    }
}