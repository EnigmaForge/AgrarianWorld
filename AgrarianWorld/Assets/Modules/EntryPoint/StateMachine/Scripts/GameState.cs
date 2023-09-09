using UnityEngine;

namespace EntryPoint.StateMachine
{
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