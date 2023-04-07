using UnityEngine;
using UnityEngine.Serialization;

namespace Code.Combat
{
    [CreateAssetMenu(fileName = "Weapon", menuName = "Weapons/Make New Weapon", order = 0)]
    public class Weapon : ScriptableObject
    {
        [SerializeField] private AnimatorOverrideController animatorOverride = null;
        [SerializeField] private GameObject equippedWeapon = null;
        [SerializeField] private float weaponDamage = 5f;
        // [SerializeField] Projectile projectile = null;

        private const string weaponName = "Weapon";

        public void Equip(Transform attachPlace, Animator animator)
        {
            DestroyOldWeapon(attachPlace);

            if (equippedWeapon != null)
            {
                var weapon = Instantiate(equippedWeapon, attachPlace);
                weapon.name = weaponName;
            }

            if (animatorOverride != null)
            {
                animator.runtimeAnimatorController = animatorOverride;
            }
        }

        private void DestroyOldWeapon(Transform attachPlace)
        {
            Transform oldWeapon = attachPlace.Find(weaponName);
            
            if (oldWeapon == null)
                return;
            
            oldWeapon.name = "DESTROYING";

            Destroy(oldWeapon.gameObject);
        }
    }
}