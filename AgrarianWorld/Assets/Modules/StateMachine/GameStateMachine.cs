using System;
using System.Collections.Generic;
using Zenject;

namespace StateMachine {
    public class GameStateMachine : IStateMachine, IInitializable {
        private IState _currentState;
        private Dictionary<Type, IState> _states;

        public void Initialize() =>
            InitializeStates();

        private void InitializeStates() {
            _states = new Dictionary<Type, IState> {
                [typeof(InitializeState)] = new InitializeState(), 
                [typeof(GameMenuState)] = new GameMenuState(),
                [typeof(GameState)] = new GameState()
            };
        }
        
        public void ChangeState<TState>() where TState : IState {
            Type newStateType = typeof(TState);
            
            if (_states.ContainsKey(newStateType) is false)
                throw new Exception($"{newStateType} does not belong to {this}");
            
            _currentState?.Exit();
            _currentState = _states[newStateType];
            _currentState.Enter();
        }
    }
}