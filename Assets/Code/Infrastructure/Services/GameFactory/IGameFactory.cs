using Code.StaticData.Enemies;
using UnityEngine;

namespace Code.Infrastructure.Services.GameFactory
{
    public interface IGameFactory : IService
    {
        GameObject CreatePlayer(GameObject at);
        GameObject CreateHud();
        void CreateSpawner(Vector3 at, EnemyTypeId enemyTypeId);
    }
}