namespace EntryPoint.StateMachine
{
    public interface IState
    {
        void Enter();
        void Exit();
    }
}