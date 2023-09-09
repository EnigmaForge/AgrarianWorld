using System;
using System.Collections.Generic;

namespace EntryPoint.StateMachine
{
    public interface IStateMachine
    {
        public Dictionary<Type, IState> States { get; set; }
        public IState CurrentState { get; set; }

        public void Enter<TState>() where TState : IState;
    }
}