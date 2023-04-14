using UnityEngine;

namespace Code.Animator.Hashers
{
    [CreateAssetMenu(menuName = "Animator/Animator State Hasher")]
    public class AnimatorStateHasher : ScriptableObject
    {
        [field: SerializeField] public string SwitchWeaponName { get; private set; } = "Switch Weapon";
        [field: SerializeField] public string ShootName { get; private set; } = "Shoot";
        [field: SerializeField] public string IdleName { get; private set; } = "Idle";
        [field: SerializeField] public string MoveName { get; private set; } = "Move";
        
        public int SwitchWeaponHash { get; private set; }
        public int IdleHash { get; private set; }
        public int MoveHash { get; private set; }
        public int ShootHash { get; private set; }

        public void Init()
        {
            SwitchWeaponHash = UnityEngine.Animator.StringToHash(SwitchWeaponName);
            IdleHash = UnityEngine.Animator.StringToHash(IdleName);
            MoveHash = UnityEngine.Animator.StringToHash(MoveName);
            ShootHash = UnityEngine.Animator.StringToHash(ShootName);
        }
    }
}