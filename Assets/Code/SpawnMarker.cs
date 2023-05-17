using Code.StaticData.Enemies;
using UnityEngine;
using UnityEngine.Serialization;

namespace Code
{
    public class SpawnMarker : MonoBehaviour
    {
        [FormerlySerializedAs("MonsterTypeId")] public EnemyTypeId enemyTypeId;
    }
}