using Code.Entities;
using Code.Infrastructure.Services.AssetsManagement;
using UnityEngine;

namespace Code.Infrastructure.Services.GameFactory
{
    public class GameFactory : IGameFactory
    {
        private readonly IAssets _assets;
        // private GameObject PlayerGameObject { get; set; }

        public GameFactory(IAssets assets)
        {
            _assets = assets;
        }
        public GameObject CreatePlayer(GameObject at)
        {
            GameObject player = _assets.Instantiate(AssetPath.PLAYER_PATH, at.transform.position);
            return player;
        }
        public GameObject CreateHud()
        {
            GameObject hud = _assets.Instantiate(AssetPath.HUD_PATH);
            return hud;
            // GameObject HUD_PATH = InstantiateRegistered(AssetPath.HUD_PATH);
            // return hud;
        }
        //
        // private GameObject InstantiateRegistered(string prefabPath, Vector3 at)
        // {
        //     GameObject gameObject = _assets.Instantiate(prefabPath, at: at);
        //     return gameObject;
        // }
        //
        // private GameObject InstantiateRegistered(string prefabPath)
        // {
        //     GameObject gameObject = _assets.Instantiate(prefabPath);
        //     return gameObject;
        // }
    }
}