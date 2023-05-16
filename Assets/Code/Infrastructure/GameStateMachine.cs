using System;
using System.Collections.Generic;
using Code.Infrastructure.GameStates;

namespace Code.Infrastructure
{
    public class GameStateMachine : IGameStateMachine
    {
        private readonly Dictionary<Type,IExitableState> _states;

        public GameStateMachine(
            GameBootstrapState.Factory bootstrapStateFactory,
            LoadProgressState.Factory loadProgressStateFactory,
            LoadLevelState.Factory loadLevelStateFactory,
            GameLoopState.Factory gameLoopStateFactory
            // GamePauseState.Factory gamePauseStateFactory
        )
        {
            _states = new Dictionary<Type, IExitableState>();

            RegisterState(bootstrapStateFactory.Create(this));
            RegisterState(loadProgressStateFactory.Create(this));
            RegisterState(loadLevelStateFactory.Create(this));
            RegisterState(gameLoopStateFactory.Create(this));
            // RegisterState(gamePauseStateFactory.Create(this));
        }

        private IExitableState _activeState;

        public void Enter<TState>() where TState : class, IGameState
        {
            IGameState gameState = ChangeState<TState>();
            gameState.Enter();
        }

        public void Enter<TState, TPayload>(TPayload payload) where TState : class, IPayloadedState<TPayload>
        {
            TState state = ChangeState<TState>();
            state.Enter(payload);
        }
        
        private void RegisterState<TState>(TState state) where TState : class, IExitableState
            => _states.Add(typeof(TState), state);
        
        private void UnregisterState<TState>() where TState : class, IExitableState
            => _states.Remove(typeof(TState));

        private TState ChangeState<TState>() where TState : class, IExitableState
        {
            _activeState?.Exit();
            
            TState state = GetState<TState>();
            _activeState = state;
            
            return state;
        }

        private TState GetState<TState>() where TState : class, IExitableState => 
            _states[typeof(TState)] as TState;
    }
}