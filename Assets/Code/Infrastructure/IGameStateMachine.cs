namespace Code.Infrastructure
{
    public interface IGameStateMachine
    {
        void Enter<TState>() where TState : class, IGameState;
        void Enter<TState, TPayload>(TPayload payload) where TState : class, IPayloadedState<TPayload>;
    }
}