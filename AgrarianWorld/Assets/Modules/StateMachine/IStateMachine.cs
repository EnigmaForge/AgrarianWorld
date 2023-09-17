using System;
using System.Collections.Generic;

namespace StateMachine
{
    public interface IStateMachine
    {
        public Dictionary<Type, IState> States { get; set; }
        public IState CurrentState { get; set; }

        public void ChangeState<TState>() where TState : IState;
    }
}