using Code.Infrastructure.GameStates;
using Code.Infrastructure.SceneManagement;
using UnityEngine;
using Zenject;

namespace Code.Infrastructure
{
    public class GameBootstrapper : MonoBehaviour
    {
        private IGameStateMachine _gameStateMachine;

        [Inject]
        public void Construct(IGameStateMachine gameStateMachine)
        {
            _gameStateMachine = gameStateMachine;
        }
        private void Awake()
        {
            _gameStateMachine.Enter<GameBootstrapState>();
            
            // DontDestroyOnLoad(this);
        }
        
        public class Factory : PlaceholderFactory<GameBootstrapper>
        {
        }
    }
}