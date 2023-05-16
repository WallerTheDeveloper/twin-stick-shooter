using Code.Combat.DamageCalculation;
using Code.Entities.EnemyEntity;
using UnityEngine;
using UnityEngine.Serialization;

namespace Code.Combat.Weapons
{
    [CreateAssetMenu(fileName = "Weapon", menuName = "Weapons/Make New Weapon", order = 0)]
    public class Weapon : ScriptableObject
    {
        public GameObject _weaponPrefab = null;
        [SerializeField] private AnimatorOverrideController animatorOverride = null;
        // [SerializeField] Projectile projectile = null;
        [SerializeField] private float _weaponDamage = 5f;
        [SerializeField] private float shootingSpeed = 1f;

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

        public void InflictDamage(IDamageCalculator damageCalculator)
        {
            AimComponent aimComponent = _weaponPrefab.GetComponent<AimComponent>();
            GameObject target = aimComponent.GetAimTarget();
            
            if (target != null)
            {
                Enemy enemyTarget = target.GetComponent<Enemy>();
                if (enemyTarget != null)
                {
                    Debug.Log("Aiming at: " + aimComponent.GetAimTarget());   
                    damageCalculator.CalculateDamage(enemyTarget.Health, _weaponDamage);
                }
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