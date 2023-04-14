using System;
using UnityEngine;
using UnityEngine.UI;

namespace Code.UI
{
    public class WeaponSwitchHandler : MonoBehaviour, IWeaponSwitchHandler
    {
        public event Action OnSwitchButtonClick;
        
        public Button _switchWeaponButton;
    
        private void Start()
        {
            _switchWeaponButton.onClick.AddListener(SwitchWeapon);
        }

        private void SwitchWeapon() => 
            OnSwitchButtonClick?.Invoke();
    }
}