using Code.Infrastructure.Services.Pause;
using Zenject;

namespace Code.Infrastructure.GameStates
{
    public class GamePauseState : IGameState
    {
        private readonly IGameStateMachine _gameStateMachine;
        private readonly PauseService _pauseService;

        public GamePauseState(IGameStateMachine gameStateMachine, PauseService pauseService)
        {
            _gameStateMachine = gameStateMachine;
            _pauseService = pauseService;
        }
        public void Enter()
        {
            _pauseService.SetPaused(true);
        }

        public void Exit()
        {
            _pauseService.SetPaused(false);
        }

        public class Factory : PlaceholderFactory<IGameStateMachine, GamePauseState>
        {
            
        }
    }
}