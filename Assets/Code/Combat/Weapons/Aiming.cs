using UnityEngine;

namespace Code.Combat.Weapons
{
    public class Aiming : MonoBehaviour
    {
        [SerializeField] private Transform _weaponMuzzle;
        [SerializeField] private float _aimRange = 1000;
        [SerializeField] private LayerMask _aimMask;

        public GameObject GetAimTarget()
        {
            Vector3 aimStart = _weaponMuzzle.position;

            if (Physics.Raycast(aimStart, GetAimDirection(), out RaycastHit hit, _aimRange, _aimMask))
            {
                return hit.collider.gameObject;
            }
            return null;
        }

        private Vector3 GetAimDirection()
        {
            Vector3 muzzleDirection = _weaponMuzzle.forward;
            return new Vector3(muzzleDirection.x, 0f, muzzleDirection.z).normalized;
        }

        private void OnDrawGizmos()
        {
            Debug.DrawRay(_weaponMuzzle.position,  _weaponMuzzle.position + GetAimDirection() * _aimRange, Color.red);
        }
    }
}