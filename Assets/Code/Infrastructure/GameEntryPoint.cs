using UnityEngine;
using Zenject;

namespace Code.Infrastructure
{
    public class GameEntryPoint : MonoBehaviour
    {
        GameBootstrapper.Factory _gameBootstrapperFactory;
        
        [Inject]
        public void Construct(GameBootstrapper.Factory gameBootstrapperFactory)
        {
            _gameBootstrapperFactory = gameBootstrapperFactory;
        }

        private void Awake()
        {
            var bootstrap = FindObjectOfType<GameBootstrapper>();
            
            if(bootstrap != null)
                return;

            _gameBootstrapperFactory.Create();
        }
    }
}