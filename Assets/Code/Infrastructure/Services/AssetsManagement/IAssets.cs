using UnityEngine;

namespace Code.Infrastructure.Services.AssetsManagement
{
    public interface IAssets : IService
    {
        GameObject Instantiate(string path);
        GameObject Instantiate(string path, Vector3 at);
        T GetAsset<T>(string path) where T : Object;
    }
}