using EntryPoint.SceneLoader;
using System;
using System.Collections.Generic;

namespace EntryPoint.StateMachine
{
    public class GameStateMachine : IStateMachine
    {
        public Dictionary<Type, IState> States { get; set; }
        public IState CurrentState { get; set; }

        public GameStateMachine() 
        {
            States = new Dictionary<Type, IState>()
            {
                [typeof(BootstrapState)] = new BootstrapState(this, new AsyncSceneLoader())
            };
        }

        public void Enter<TState>() where TState : IState
        {
            CurrentState?.Exit();
            IState state = States[typeof(TState)];
            CurrentState = state;
            state.Enter();
        }
    }
}