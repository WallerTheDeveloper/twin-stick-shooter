using System;
using Code.Combat.InventorySystem;
using Code.Combat.Weapons;
using UnityEngine;

namespace Code.Combat
{
    public class Fighter : MonoBehaviour
    {
        private PlayerBackpack _playerBackpack;

        private void Awake()
        {
            _playerBackpack = GetComponent<PlayerBackpack>();
        }

        public void ShootInDirection() // Pistol_Firing/Rifle_Firing animation event
        {
            Aiming aiming = _playerBackpack.CurrentWeapon._weaponPrefab.GetComponent<Aiming>();
            var get = aiming.GetAimTarget();
            Debug.Log($"aiming at {get}");
            
        }
    }
}