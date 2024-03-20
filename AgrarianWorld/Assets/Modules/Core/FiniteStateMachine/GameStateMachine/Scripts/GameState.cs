using Modules.Core.SceneLoader;
using UnityEngine.SceneManagement;

namespace Modules.Core.FiniteStateMachine.GameStateMachine {
    public class GameState : IGameState {
        private readonly ISceneLoader _sceneLoader;

        private GameState(ISceneLoader sceneLoader) =>
            _sceneLoader = sceneLoader;
        
        public void Enter() =>
            _sceneLoader.Load(SceneNames.GameScene.ToString(), LoadSceneMode.Single);

        public void Exit() { }
    }
}