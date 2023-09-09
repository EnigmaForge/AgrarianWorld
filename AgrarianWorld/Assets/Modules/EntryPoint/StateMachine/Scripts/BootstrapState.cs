using EntryPoint.SceneLoader;
using UnityEngine;

namespace EntryPoint.StateMachine
{
    public class BootstrapState : IState
    {
        private IStateMachine _gameStateMachine;
        private ISceneLoader _sceneLoader;

        public BootstrapState(IStateMachine gameStateMachine, ISceneLoader sceneLoader)
        {
            _gameStateMachine = gameStateMachine;
            _sceneLoader = sceneLoader;
        }

        public void Enter()
        {
            Debug.Log("Enter Bootstrap State");
            _sceneLoader.LoadScene(SceneNames.CORE_SCENE);
        }

        public void Exit()
        {
            Debug.Log("Exit Bootstrap State");
        }
    }
}