using UnityEngine;
using UnityEngine.UI;

namespace Code.UI.Menu
{
    public class MainMenuView : View
    {
        [SerializeField] private Button _newGameButton;
        [SerializeField] private Button _loadGameButton;
        [SerializeField] private Button _creditsButton;
        [SerializeField] private Button _exitButton;
        public override void Initialize()
        {
            _exitButton.onClick.AddListener(() => Debug.Log("cilicked"));
        }
    }
}