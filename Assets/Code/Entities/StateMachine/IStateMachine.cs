using System;

namespace Code.Entities.StateMachine
{
    public interface IStateMachine
    {
        void Enter(IEntityState entityState);
        void Tick();
        void FixedTick();
        void AddTransition(IEntityState from, IEntityState to, Func<bool> predicate);
        void AddAnyTransition(IEntityState entityState, Func<bool> predicate);
    }
}