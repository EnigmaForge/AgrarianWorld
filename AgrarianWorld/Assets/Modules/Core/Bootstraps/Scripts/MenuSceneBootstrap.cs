using Modules.GameMenu;
using UnityEngine;
using Zenject;

namespace Modules.Core.Bootstraps {
    public class MenuSceneBootstrap : BootstrapBehaviour {
        [SerializeField] private SceneContext _sceneContext;
        
        [Header("Windows")] 
        [SerializeField] private OpenWorldWindow _openWorldWindow;
        [SerializeField] private GameMenuWindow _gameMenuWindow;
        
        private SceneContext _sceneContextInstance;

        private void Start() {
            InitializeContext();
            InitializeWindows();
        }

        private void InitializeContext() {
            _sceneContextInstance = Instantiate(_sceneContext);
            _sceneContextInstance.Run();
        }

        private void InitializeWindows() {
            GameObject openWorldWindowInstance = _sceneContextInstance.Container.InstantiatePrefab(_openWorldWindow);
            GameObject gameMenuWindowInstance = _sceneContextInstance.Container.InstantiatePrefab(_gameMenuWindow);
            
            if (openWorldWindowInstance.TryGetComponent(out OpenWorldWindow openWorldWindow))
                openWorldWindow.SetActive(false);
            if (gameMenuWindowInstance.TryGetComponent(out GameMenuWindow gameMenuWindow))
                gameMenuWindow.SetActive(true);
        }
    }
}