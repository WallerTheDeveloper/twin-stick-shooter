using System.Collections.Generic;
using System.Linq;
using Code.UI;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace Code.Combat.InventorySystem
{
    public class PlayerBackpack : SerializedMonoBehaviour
    {
        public Dictionary<Weapon, Transform> _weapons;
        private IEnumerator<KeyValuePair<Weapon, Transform>> _weaponsEnumerator;

        private Animator _animator;
        private IWeaponSwitcher _weaponSwitcher;

        [Inject]
        public void Construct(IWeaponSwitcher weaponSwitcher)
        {   
            _weaponSwitcher = weaponSwitcher;
        }

        private void Awake()
        {
            _weaponSwitcher.OnButtonClick += SwitchWeapon;
            _animator = GetComponent<Animator>();
            
            _weaponsEnumerator = _weapons.GetEnumerator();
            
            SwitchWeapon();
        }

        private void OnDestroy()
        {
            _weaponSwitcher.OnButtonClick -= SwitchWeapon;
        }

        private void SwitchWeapon()
        {
            if (_weaponsEnumerator.MoveNext())
            {
                EquipCurrentWeapon();
            }
            else
            {
                _weaponsEnumerator.Reset();

                _weaponsEnumerator.MoveNext();

                EquipCurrentWeapon();
            }
        }

        private void EquipCurrentWeapon()
        {
            var currentWeapon = _weaponsEnumerator.Current.Key;
            var currentTransform = _weaponsEnumerator.Current.Value;
            
            currentWeapon.Equip(currentTransform, _animator);

        }
    }
}