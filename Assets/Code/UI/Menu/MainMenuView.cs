using Code.Infrastructure.GameStates;
using Code.Infrastructure.SceneManagement;
using Code.Infrastructure.Services.Core;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Code.UI.Menu
{
    public class MainMenuView : View
    {
        [SerializeField] private Button _newGameButton;
        [SerializeField] private Button _loadGameButton;
        [SerializeField] private Button _creditsButton;
        [SerializeField] private Button _exitButton;

        private IButtonClickMediator _buttonClickMediator;
        // private IEventMediator _eventMediator;
        
        [Inject]
        public void Construct(IButtonClickMediator buttonClickMediator)
        {
            _buttonClickMediator = buttonClickMediator;
        }
        
        public override void Initialize()
        {
            _newGameButton.onClick.AddListener(NotifyNewGameButtonClick);
            _creditsButton.onClick.AddListener(() => ViewManager.Show<CreditsView>());
            _exitButton.onClick.AddListener(() => Debug.Log("cilicked")); // Quit app here
        }

        private void NotifyNewGameButtonClick()
        {
            _buttonClickMediator.NotifyButtonClick();
        }
    }
}