namespace StateMachine
{
    public interface IStateFactory
    {
        public IState Create<TState>() where TState : IState;
    }
}
