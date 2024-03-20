namespace Modules.Core.FiniteStateMachine {
    public interface IFiniteStateMachine {
        public void ChangeState<TState>() where TState : IState;
    }
}
