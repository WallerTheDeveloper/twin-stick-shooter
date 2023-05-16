using UnityEngine;

namespace Code.Infrastructure.Services.GameFactory
{
    public interface IGameFactory : IService
    {
        GameObject CreatePlayer(GameObject at);
        GameObject CreateHud();
    }
}