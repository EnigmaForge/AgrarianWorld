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

            RegisterServices();
            _gameStateMachine.Enter<GameState>();
            _sceneLoader.LoadScene(SceneNames.CORE_SCENE);
        }

        private void RegisterServices() 
        {
            Debug.Log("Services Initialized!");
        }

        public void Exit()
        {
            Debug.Log("Exit Bootstrap State");
        }
    }

    public class GameState : IState
    {
        private IStateMachine _gameStateMachine;

        public GameState(IStateMachine gameStateMachine)
        {
            _gameStateMachine = gameStateMachine;
        }

        public void Enter()
        {
            Debug.Log("Enter Game State");
        }

        public void Exit()
        {
            
        }
    }
}