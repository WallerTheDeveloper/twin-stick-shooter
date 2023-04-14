namespace Code.Animator.Logic
{
    public interface IAnimationPlayer
    {
        void PlayRotation(float animatorTurnSpeed);
        void PlayShooting(bool isEnabled);
        void ResetMovement();
        void ResetStandingTurn();
    }
}