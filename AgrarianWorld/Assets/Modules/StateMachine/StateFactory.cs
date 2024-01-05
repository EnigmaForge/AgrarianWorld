﻿using Zenject;

namespace StateMachine
{
    public class StateFactory : IStateFactory
    {
        private readonly DiContainer _container;

        public StateFactory(DiContainer container)
        {
            _container = container;
        }

        public IState Create<TState>() where TState : IState => 
            _container.Resolve<TState>();
    }
}