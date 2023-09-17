using System;
using System.Collections.Generic;
using Zenject;

namespace StateMachine
{
    public class GameStateMachine : IStateMachine
    {
        public Dictionary<Type, IState> States { get; set; } = new Dictionary<Type, IState>();
        public IState CurrentState { get; set; }

        private IStateFactory _stateFactory;

        [Inject]
        public GameStateMachine(IStateFactory stateFactory)
        {
            _stateFactory = stateFactory;
        }

        public void ChangeState<TState>() where TState : IState
        {
            CurrentState?.Exit();
            CurrentState = GetNextState<TState>();
            CurrentState.Enter();
        }

        private IState GetNextState<TState>() where TState : IState
        {
            if (States.ContainsKey(typeof(TState)) is false)
                States.Add(typeof(TState), _stateFactory.Create<TState>());

            return States[typeof(TState)];
        }
    }
}