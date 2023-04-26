using System;
using System.Collections.Generic;
using UnityEngine;

namespace Code.Entities.StateMachine
{
    public class EntityStateMachine : IStateMachine
    {
        private IEntityState _currentState;
        private Dictionary<Type, List<Transition>> _transitions = new Dictionary<Type, List<Transition>>();
        private List<Transition> _currentTransitions = new List<Transition>();
        private List<Transition> _anyTransitions = new List<Transition>();
        
        private static List<Transition> EmptyTransitions = new List<Transition>();
        
        public void Enter(IEntityState state)
        {
            if(_currentState == state)
                return;
            
            _currentState?.OnExit();
            _currentState = state;

            _transitions.TryGetValue(_currentState.GetType(), out _currentTransitions);
            _currentTransitions ??= EmptyTransitions;

            Debug.Log($"Enter to {_currentState}");
            
            _currentState.OnEnter();
        }

        public virtual void Tick()
        {
            var transition = GetTransition();
            if(transition != null)
                Enter(transition.To);
                
            _currentState?.Tick();
        }

        public void FixedTick()
        {
            var transition = GetTransition();
            if(transition != null)
                Enter(transition.To);
                
            _currentState?.FixedTick();
        }

        public void AddTransition(IEntityState from, IEntityState to, Func<bool> predicate)
        {
            if (_transitions.TryGetValue(from.GetType(), out var transitions) == false)
            {
                transitions = new List<Transition>();
                _transitions[from.GetType()] = transitions;
            }
      
            transitions.Add(new Transition(to, predicate));
        }

        public void AddAnyTransition(IEntityState state, Func<bool> predicate)
        {      
            Debug.Log("Entered");
            _anyTransitions.Add(new Transition(state, predicate));
        }
        
        private Transition GetTransition()
        {
            foreach(var transition in _anyTransitions)
                if (transition.Condition())
                    return transition;
      
            foreach (var transition in _currentTransitions)
                if (transition.Condition())
                    return transition;

            return null;
        }
    }
}