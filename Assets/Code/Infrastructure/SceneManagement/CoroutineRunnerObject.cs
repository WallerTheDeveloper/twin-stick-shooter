using UnityEngine;

namespace Code.Infrastructure.SceneManagement
{
    public class CoroutineRunnerObject : MonoBehaviour, ICoroutineRunner
    {
        private void Awake()
        {
            DontDestroyOnLoad(this);
        }
    }
}