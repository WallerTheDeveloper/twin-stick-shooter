using System;

namespace Code.Entities.StateMachine
{
    public class Transition
    {
        public Func<bool> Condition { get; }
        public IEntityState To { get; }

        public Transition(IEntityState to, Func<bool> condition)
        {
            To = to;
            Condition = condition;
        }
    }
}