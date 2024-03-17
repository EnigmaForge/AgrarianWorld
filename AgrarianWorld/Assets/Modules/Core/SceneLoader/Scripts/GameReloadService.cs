using Modules.DebugMenu;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace Modules.Core.SceneLoader {
    public class GameReloadService : IInitializable {
        private DiContainer _container;

        [Inject]
        private void InjectDependencies(DiContainer container) =>
            _container = container;

        public void Initialize() {
            if (SceneManager.GetActiveScene().name != SceneNames.InitialScene.ToString())
                Reload();
        }

        public void Reload() {
            UnloadProjectContext();
            UnloadDebugMenu();
            LoadInitialScene();
        }

        private void UnloadProjectContext() {
            _container.UnbindAll();
            Object.Destroy(ProjectContext.Instance.gameObject);
        }

        private void UnloadDebugMenu() {
            DebugMenuCanvas debugMenu = Object.FindObjectOfType<DebugMenuCanvas>();
            if(debugMenu != null)
                Object.Destroy(debugMenu.gameObject);
        }

        private void LoadInitialScene() =>
            SceneManager.LoadScene(SceneNames.InitialScene.ToString());
    }
}
