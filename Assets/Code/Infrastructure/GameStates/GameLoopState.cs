using Zenject;

namespace Code.Infrastructure.GameStates
{
    public class GameLoopState : IState
    {
        public GameLoopState(GameStateMachine gameStateMachine)
        {
            
        }
        
        public void Enter()
        {
        }

        public void Exit()
        {
        }

        public class Factory : PlaceholderFactory<IGameStateMachine, GameLoopState>
        {
        }
    }
}