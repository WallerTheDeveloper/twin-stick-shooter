using UnityEngine;

namespace Code.Infrastructure.Services.AssetsManagement
{
    public class AssetsProvider : IAssets
    {
        public GameObject Instantiate(string path)
        {
            var prefab = Resources.Load<GameObject>(path);
            return Object.Instantiate(prefab);
        }

        public GameObject Instantiate(string path, Vector3 at)
        {
            var prefab = Resources.Load<GameObject>(path);
            return Object.Instantiate(prefab, at, Quaternion.identity);
        }

        public T GetAsset<T>(string path) where T : Object
        {
            var asset = Resources.Load<T>(path: path);
            return asset;
        }
    }
}