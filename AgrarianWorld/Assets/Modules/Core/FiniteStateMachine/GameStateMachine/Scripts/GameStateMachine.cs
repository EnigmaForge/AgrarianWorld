using System;
using System.Collections.Generic;

namespace Modules.Core.FiniteStateMachine.GameStateMachine {
    public class GameStateMachine : IGameStateMachine {
        private readonly Dictionary<Type, IState> _states;
        private IState _currentState;
        public event Action<Type> OnChangeState;

        public GameStateMachine(IGameState[] states) {
            if (states is { Length: 0 })
                throw new ArgumentException("States can`t be empty", nameof(states));
            
            _states = new Dictionary<Type, IState>();
            foreach (IGameState state in states)
                _states.Add(state.GetType(), state);
        }

        public void ChangeState<TState>() where TState : IState {
            if (_states.ContainsKey(typeof(TState)) is false)
                throw new ArgumentException("State does not exist in possible states", nameof(TState));
            
            _currentState?.Exit();
            _currentState = _states[typeof(TState)];
            _currentState.Enter();
            
            OnChangeState?.Invoke(typeof(TState));
        }
    }
}