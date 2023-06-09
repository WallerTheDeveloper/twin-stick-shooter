﻿using System.Collections.Generic;
using Code.Combat.Weapons;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Code.Combat.InventorySystem
{
    public class PlayerBackpack : SerializedMonoBehaviour, IPlayerBackpack
    {
        public Dictionary<Weapon, Transform> _weapons;
        public Weapon EquippedWeapon { get; private set; }

        
        private IEnumerator<KeyValuePair<Weapon, Transform>> _weaponsEnumerator;
        private UnityEngine.Animator _animator;

        private void Awake()
        {
            _animator = GetComponent<UnityEngine.Animator>();
            
            _weaponsEnumerator = _weapons.GetEnumerator();
            
            SwitchWeapon();
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

        private void SwitchWeaponEvent() // animation event
            => SwitchWeapon();

        private void EquipCurrentWeapon()
        {
            var currentWeapon = _weaponsEnumerator.Current.Key;
            var currentTransform = _weaponsEnumerator.Current.Value;

            EquippedWeapon = currentWeapon;

            currentWeapon.Equip(currentTransform, _animator);
        }

    }
}