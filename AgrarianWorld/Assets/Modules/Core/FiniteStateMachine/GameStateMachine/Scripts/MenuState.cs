using Modules.Core.SceneLoader;
using UnityEngine.SceneManagement;

namespace Modules.Core.FiniteStateMachine.GameStateMachine {
    public class MenuState : IGameState {
        private readonly ISceneLoader _sceneLoader;

        private MenuState(ISceneLoader sceneLoader) =>
            _sceneLoader = sceneLoader;

        public void Enter() =>
            _sceneLoader.Load(SceneNames.MenuScene.ToString(), LoadSceneMode.Single);

        public void Exit() { }
    }
}