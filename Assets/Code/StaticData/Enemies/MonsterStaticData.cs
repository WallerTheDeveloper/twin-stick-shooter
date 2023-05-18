using Code.Entities.EnemyEntity.Patrol;
using UnityEngine;
using UnityEngine.Serialization;

namespace Code.StaticData.Enemies
{
    [CreateAssetMenu(fileName = "MonsterData", menuName = "StaticData/Monster")]
    public class MonsterStaticData : ScriptableObject
    {
        public EnemyTypeId EnemyTypeId;

        // [Range(1, 100)]
        // public int Hp;

        // [Range(1f, 30)]
        // public float Damage;

        // public int MinLoot;

        // public int MaxLoot;

        // [Range(1f, 10f)]
        // public float MoveSpeed;

        // [Range(0.5f, 1)]
        // public float AttackEffectiveDistance;
        
        public GameObject Prefab;
    }
}