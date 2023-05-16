using System;
using UnityEngine;

namespace Code.StaticData.Enemies
{
    [Serializable]
    public class EnemySpawnerData
    {
        public string Id;
        public MonsterTypeId MonsterTypeId;
        public Vector3 Position;
        
        public EnemySpawnerData(string id, MonsterTypeId monsterTypeId, Vector3 position)
        {
            Id = id;
            MonsterTypeId = monsterTypeId;
            Position = position;
        }
    }
}