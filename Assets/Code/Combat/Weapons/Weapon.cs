using UnityEngine;

namespace Code.Combat.Weapons
{
    [CreateAssetMenu(fileName = "Weapon", menuName = "Weapons/Make New Weapon", order = 0)]
    public class Weapon : ScriptableObject
    {
        public GameObject _weaponPrefab = null;
        [SerializeField] private AnimatorOverrideController animatorOverride = null;
        [SerializeField] private float weaponDamage = 5f;
        [SerializeField] private float shootingSpeed = 1f;
        // [SerializeField] Projectile projectile = null;

        private const string weaponName = "Weapon";

        public void Equip(Transform attachPlace, UnityEngine.Animator animator)
        {
            DestroyOldWeapon(at: attachPlace);

            if (_weaponPrefab != null)
            {
                var weapon = Instantiate(_weaponPrefab, attachPlace);
                weapon.name = weaponName;
            }

            if (animatorOverride != null)
            {
                animator.runtimeAnimatorController = animatorOverride;
                animator.SetFloat("shootingSpeed", shootingSpeed);
            }
        }

        private void DestroyOldWeapon(Transform at)
        {
            Transform oldWeapon = at.Find(weaponName);
            
            if (oldWeapon == null)
                return;
            
            oldWeapon.name = "DESTROYING";

            Destroy(oldWeapon.gameObject);
        }
    }
}