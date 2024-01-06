using StateMachine;
using UnityEngine;
using Zenject;

namespace Core.EntryPoint {
    public class GameInitializer : MonoBehaviour {
        private GameStateMachine _gameStateMachine;

        [Inject]
        private void InjectDependencies(GameStateMachine gameStateMachine) =>
            _gameStateMachine = gameStateMachine;

        private void Start() {
            _gameStateMachine.ChangeState<InitializeState>();
            
            _gameStateMachine.ChangeState<GameMenuState>();
        }
    }
}