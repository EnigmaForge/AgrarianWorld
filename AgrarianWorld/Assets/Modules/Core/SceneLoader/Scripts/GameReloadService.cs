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
            _container.UnbindAll();
            Object.Destroy(ProjectContext.Instance.gameObject);
            SceneManager.LoadScene(SceneNames.InitialScene.ToString());
        }
    }
}
