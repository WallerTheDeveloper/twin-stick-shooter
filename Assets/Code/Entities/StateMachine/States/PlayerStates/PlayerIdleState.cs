using Code.Animator;
using Code.Animator.Logic;

namespace Code.Entities.StateMachine.States.PlayerStates
{
    public class PlayerIdleState : IEntityState
    {
        private readonly IAnimationPlayer _animationPlayer;

        public PlayerIdleState(IAnimationPlayer animationPlayer)
        {
            _animationPlayer = animationPlayer;
        }
        public void OnEnter()
        {
           _animationPlayer.ResetMovement();
           _animationPlayer.ResetStandingTurn();
           _animationPlayer.PlayShooting(false);
        }

        public void Tick()
        {
        }

        public void FixedTick()
        {
        }

        public void OnExit()
        {
        }
    }
}