namespace StateMachine {
    public interface IStateMachine {
        public void ChangeState<TState>() where TState : IState;
    }
}