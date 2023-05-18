using System;
using Code.Entities.EnemyEntity.Patrol;
using UnityEngine;

namespace Code.StaticData
{
    [Serializable]
    public class PatrolPathData
    {
        public string PathId;
        public Vector3 Position;

        public PatrolPathData(string pathId, Vector3 position)
        {
            PathId = pathId;
            Position = position;
        }
    }
}