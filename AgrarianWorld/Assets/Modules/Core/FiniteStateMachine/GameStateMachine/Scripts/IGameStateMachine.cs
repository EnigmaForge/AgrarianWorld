using System;

namespace Modules.Core.FiniteStateMachine.GameStateMachine {
    public interface IGameStateMachine : IFiniteStateMachine {
        public event Action<Type> OnChangeState;
    }
}