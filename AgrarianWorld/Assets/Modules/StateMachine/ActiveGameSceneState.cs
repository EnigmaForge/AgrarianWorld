using Zenject;

namespace StateMachine
{
    public class ActiveGameSceneState : IState
    {
        private readonly IStateMachine _stateMachine;
        private readonly DayNightCycleFactoryBase _dayNightCycleFactory;

        [Inject]
        public ActiveGameSceneState(IStateMachine stateMachine, DayNightCycleFactoryBase dayNightCycleFactory)
        {
            _stateMachine = stateMachine;
            _dayNightCycleFactory = dayNightCycleFactory;
        }

        public void Enter() => 
            _dayNightCycleFactory.Create();

        public void Exit() { }
    }
}