using UnityEngine;
using UnityEngine.UI;

namespace Code.UI.Menu
{
    public class CreditsView : View
    {
        [SerializeField] private Button _backButton;
        public override void Initialize()
        {
            _backButton.onClick.AddListener(() => ViewManager.Show<MainMenuView>());
        }
    }
}