using SceneLoaderModule;
using StateMachine;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace Core.EntryPoint {
    public class GameInitializer : MonoBehaviour {
        private GameStateMachine _gameStateMachine;
        private ISceneLoader _sceneLoader;

        [Inject]
        private void InjectDependencies(GameStateMachine gameStateMachine, ISceneLoader sceneLoader) {
            _gameStateMachine = gameStateMachine;
            _sceneLoader = sceneLoader;
        }

        private void Start() {
            _gameStateMachine.ChangeState<InitializeState>();
            _sceneLoader.Load(SceneNames.MenuScene, LoadSceneMode.Single, null, ChangeGameStateToMenuState);
        }

        private void ChangeGameStateToMenuState() =>
            _gameStateMachine.ChangeState<GameMenuState>();
    }
}