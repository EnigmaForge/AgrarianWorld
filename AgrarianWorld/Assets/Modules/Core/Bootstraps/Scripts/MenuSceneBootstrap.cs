using Modules.Core.SceneLoader;
using Modules.GameMenu;
using Modules.ViewsModule;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace Modules.Core.Bootstraps {
    public class MenuSceneBootstrap : BootstrapBehaviour {
        [SerializeField] private SceneContext _sceneContext;
        [SerializeField] private GameMenuWindow _gameMenuWindow;

        private void Start() {
            InitializeContext();
            InitializeGameMenu();
        }

        private void InitializeContext() {
            SceneContext sceneContext = Instantiate(_sceneContext);
            sceneContext.Run();
        }

        private void InitializeGameMenu() {
            GameObject gameMenuWindowObject = ProjectContext.Instance.Container.InstantiatePrefab(_gameMenuWindow);
            
            if (gameMenuWindowObject.TryGetComponent(out WindowBehaviour gameMenuWindow))
                gameMenuWindow.SetActive(true);
            
            SceneManager.MoveGameObjectToScene(gameMenuWindowObject, SceneManager.GetSceneByName(SceneNames.MenuScene.ToString()));
        }
    }
}