using SceneLoader;
using Zenject;

namespace StateMachine
{
    public class LoadGameSceneState : IState
    {
        private readonly IStateMachine _stateMachine;
        private readonly ISceneLoader _sceneLoader;

        [Inject]
        public LoadGameSceneState(IStateMachine stateMachine, ISceneLoader sceneLoader)
        {
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
        }

        public void Enter() => 
            _sceneLoader.LoadScene(SceneNames.GAME_SCENE, ChangeState);

        private void ChangeState() => 
            _stateMachine.ChangeState<ActiveGameSceneState>();

        public void Exit() { }
    }
}