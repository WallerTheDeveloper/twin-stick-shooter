namespace Code.Entities.StateMachine
{
    public interface IEntityState
    {
        void OnEnter();
        void Tick();
        void FixedTick();
        void OnExit();
    }
}