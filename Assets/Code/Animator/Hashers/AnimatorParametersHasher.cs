using UnityEngine;

namespace Code.Animator.Hashers
{
    [CreateAssetMenu(menuName = "Animator/Animator Parameters Hasher")]
    public class AnimatorParametersHasher : ScriptableObject
    {
        [field: SerializeField] public string TurnSpeedName { get; private set; } = "turnSpeed";
        [field: SerializeField] public string ForwardSpeedName { get; private set; } = "forwardSpeed";
        [field: SerializeField] public string SideSpeedName { get; private set; } = "sideSpeed";
        [field: SerializeField] public string IsShootingName { get; private set; } = "isShooting";
        [field: SerializeField] public string SwitchWeaponName { get; private set; } = "switchWeapon";

        public int TurnSpeedHash { get; private set; }
        public int ForwardSpeedHash { get; private set; }
        public int SideSpeedHash { get; private set; }
        public int IsShootingHash { get; private set; }
        public int SwitchWeaponHash { get; set; }

        public void Init()
        {
            TurnSpeedHash = UnityEngine.Animator.StringToHash(TurnSpeedName);
            ForwardSpeedHash = UnityEngine.Animator.StringToHash(ForwardSpeedName);
            SideSpeedHash = UnityEngine.Animator.StringToHash(SideSpeedName);
            IsShootingHash = UnityEngine.Animator.StringToHash(IsShootingName);
            SwitchWeaponHash = UnityEngine.Animator.StringToHash(SwitchWeaponName);
        }
    }
}