using Code.Entities;
using Code.Entities.PlayerEntity;
using Zenject;

namespace Code.Infrastructure.Installers
{
    public class PlayerInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            // Container.Bind<IEntity>().To<Player>().AsSingle();
        }
    }
}