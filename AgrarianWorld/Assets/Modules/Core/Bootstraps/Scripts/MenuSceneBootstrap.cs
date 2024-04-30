using Modules.GameMenu;
using Modules.ViewsModule;
using UnityEngine;
using Zenject;

namespace Modules.Core.Bootstraps {
    public class MenuSceneBootstrap : MonoBehaviour {
        [SerializeField] private SceneContext _sceneContext;
        [SerializeField] private GameMenuWindow _gameMenuWindow;
        private DiContainer _container;

        [Inject]
        private void InjectDependencies(DiContainer container) =>
            _container = container;

        private void Start() {
            InitializeContext();
            InitializeGameMenu();
        }

        private void InitializeContext() {
            SceneContext sceneContext = Instantiate(_sceneContext);
            sceneContext.Run();
        }

        private void InitializeGameMenu() {
            GameObject gameMenuWindowObject = _container.InstantiatePrefab(_gameMenuWindow);

            if (gameMenuWindowObject.TryGetComponent(out WindowBehaviour gameMenuWindow))
                gameMenuWindow.SetActive(true);
        }
    }
}