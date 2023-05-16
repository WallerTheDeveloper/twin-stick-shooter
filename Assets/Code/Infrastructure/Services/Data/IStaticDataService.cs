using Code.StaticData;
using Code.StaticData.Enemies;

namespace Code.Infrastructure.Services.Data
{
    public interface IStaticDataService : IService
    {
        void LoadMonsters();
        MonsterStaticData ForMonster(MonsterTypeId typeId);
        LevelStaticData ForLevel(string sceneKey);
    }
}