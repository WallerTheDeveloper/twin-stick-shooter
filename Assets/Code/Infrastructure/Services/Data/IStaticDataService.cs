using Code.StaticData;
using Code.StaticData.Enemies;

namespace Code.Infrastructure.Services.Data
{
    public interface IStaticDataService : IService
    {
        void LoadStaticData();
        MonsterStaticData ForMonster(EnemyTypeId typeId);
        LevelStaticData ForLevel(string sceneKey);
    }
}