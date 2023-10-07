using Zenject;

namespace StateMachine
{
    public class ActiveGameSceneState : IState
    {
        private IStateMachine _stateMachine;

        [Inject]
        public ActiveGameSceneState(IStateMachine stateMachine)
        {
            _stateMachine = stateMachine;
        }

        public void Enter() { }

        public void Exit() { }
    }
}