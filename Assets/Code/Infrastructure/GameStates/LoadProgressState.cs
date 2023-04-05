using Code.Infrastructure.SceneManagement;
using UnityEngine.SceneManagement;
using Zenject;

namespace Code.Infrastructure.GameStates
{
    public class LoadProgressState : IState
    {
        private readonly IGameStateMachine _gameStateMachine;

        public LoadProgressState(IGameStateMachine gameStateMachine)
        {
            _gameStateMachine = gameStateMachine;
        }
        public void Enter()
        {
            //load progress here
            
            _gameStateMachine.Enter<LoadLevelState, string>(SceneNames.Level1Key);
        }

        public void Exit()
        {
        }

        public class Factory : PlaceholderFactory<IGameStateMachine, LoadProgressState>
        {
        }
    }
}