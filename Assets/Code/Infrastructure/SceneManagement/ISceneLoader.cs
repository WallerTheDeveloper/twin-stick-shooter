using System;

namespace Code.Infrastructure.SceneManagement
{
    public interface ISceneLoader
    {
        void Load(string sceneKey, Action onLoaded = null);
    }
}