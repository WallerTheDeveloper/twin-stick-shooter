using Code.Entities;
using Code.Entities.EnemyEntity;
using Zenject;

namespace Code.Infrastructure.Installers
{
    public class EnemyInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<IEntity>().To<Enemy>().AsSingle();
        }
    }
}