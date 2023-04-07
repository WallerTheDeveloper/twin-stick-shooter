using System;
using UnityEngine;
using UnityEngine.UI;

namespace Code.UI
{
    public class WeaponSwitcher : MonoBehaviour, IWeaponSwitcher
    {
        public event Action OnButtonClick;
        
        public Button _switchWeaponButton;

        private void Start()
        {
            _switchWeaponButton.onClick.AddListener(SwitchWeapon);
        }

        private void SwitchWeapon() => 
            OnButtonClick?.Invoke();
    }
}