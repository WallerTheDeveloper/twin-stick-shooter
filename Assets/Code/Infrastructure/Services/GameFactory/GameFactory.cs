// using Code.Infrastructure.Services.AssetsManagement;
// using UnityEngine;
//
// namespace Code.Infrastructure.Services.GameFactory
// {
//     public class GameFactory : IGameFactory
//     {
//         private readonly IAssets _assets;
//         private GameObject PlayerGameObject { get; set; }
//
//         public GameFactory(IAssets assets)
//         {
//             _assets = assets;
//         }
//         public GameObject CreatePlayer(GameObject at)
//         {
//             PlayerGameObject = InstantiateRegistered(AssetPath.PLAYER_PATH, at.transform.position);
//             return PlayerGameObject;
//         }
//
//         public GameObject CreateHud()
//         {
//             GameObject hud = InstantiateRegistered(AssetPath.HUD_PATH);
//             return hud;
//         }
//         
//         private GameObject InstantiateRegistered(string prefabPath, Vector3 at)
//         {
//             GameObject gameObject = _assets.Instantiate(prefabPath, at: at);
//             return gameObject;
//         }
//         
//         private GameObject InstantiateRegistered(string prefabPath)
//         {
//             GameObject gameObject = _assets.Instantiate(prefabPath);
//             return gameObject;
//         }
//     }
// }