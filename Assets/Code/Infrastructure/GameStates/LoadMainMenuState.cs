using System.Collections;
using Code.Infrastructure.SceneManagement;
using Code.Infrastructure.Services.Core;
using UniRx;
using Unity.VisualScripting;
using UnityEngine;
using Zenject;

namespace Code.Infrastructure.GameStates
{
    public class LoadMainMenuState : IGameState
    {
        private readonly ISceneLoader _sceneLoader;
        private readonly IGameStateMachine _gameStateMachine;
        private readonly IButtonClickMediator _buttonClickMediator;

        public LoadMainMenuState(ISceneLoader sceneLoader, IGameStateMachine gameStateMachine, IButtonClickMediator buttonClickMediator)
        {
            _sceneLoader = sceneLoader;
            _gameStateMachine = gameStateMachine;
            _buttonClickMediator = buttonClickMediator;

            _buttonClickMediator.OnButtonClick.Subscribe(_ => EnterLoadProgressState());
        }
        
        public void Enter() => 
            _sceneLoader.Load(SceneNames.MainMenuKey);
        
        public void Exit()
        {
        }

        private void EnterLoadProgressState()
        {
            _gameStateMachine.Enter<LoadProgressState>();
        }
        
        public class Factory : PlaceholderFactory<IGameStateMachine, LoadMainMenuState>
        {
        }
    }
}