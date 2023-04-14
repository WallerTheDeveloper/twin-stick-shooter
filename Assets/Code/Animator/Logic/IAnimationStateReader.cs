using Code.Animator.Hashers;

namespace Code.Animator.Logic
{
    public interface IAnimationStateReader
    {
        void EnteredState(int stateHash);
        void ExitedState(int stateHash);
        void Tick();
        AnimatorState State { get; }
    }
}