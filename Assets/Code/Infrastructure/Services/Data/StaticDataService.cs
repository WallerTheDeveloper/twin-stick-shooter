using System.Collections.Generic;
using Code.StaticData;
using System.Linq;
using Code.StaticData.Enemies;
using UnityEngine;

namespace Code.Infrastructure.Services.Data
{
    public class StaticDataService : IStaticDataService
    {
        private Dictionary<EnemyTypeId, MonsterStaticData> _monstersData;
        private Dictionary<string, LevelStaticData> _levelsData;
        
        public void LoadStaticData()
        {
            _monstersData = Resources
                .LoadAll<MonsterStaticData>("StaticData/Monsters")
                .ToDictionary(x => x.EnemyTypeId, x => x);
            _levelsData = Resources
                .LoadAll<LevelStaticData>("StaticData/Levels")
                .ToDictionary(x => x.LevelKey, x => x);
        }

        public MonsterStaticData ForMonster(EnemyTypeId typeId) =>
            _monstersData.TryGetValue(typeId, out MonsterStaticData staticData)
                ? staticData
                : null;

        public LevelStaticData ForLevel(string sceneKey) =>
            _levelsData.TryGetValue(sceneKey, out LevelStaticData staticData)
                ? staticData
                : null;
    }
}