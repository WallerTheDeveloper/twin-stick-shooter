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

        public void ShootInDirection() // animation event
        {
            Debug.Log($"aiming at {_playerBackpack.CurrentWeapon._weaponPrefab.GetComponent<Aiming>().GetAimTarget()}");
        }
    }
}