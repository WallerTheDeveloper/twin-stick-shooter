using Code.Infrastructure.Services.Data;
using Zenject;

namespace Code.Infrastructure.Installers
{
    public class DataInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<StaticDataService>().AsSingle();
        }
    }
}