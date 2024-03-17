using Modules.Core.SceneLoader;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Modules.Core.Bootstraps {
    public abstract class BootstrapBehaviour : MonoBehaviour {
        private void Awake() =>
            LoadInitialScene();

        private void Start() =>
            Bootstrap();

        private void LoadInitialScene() {
            string initialSceneName = SceneNames.InitialScene.ToString();
            if (SceneManager.GetActiveScene().name != initialSceneName)
                SceneManager.LoadScene(initialSceneName);
        }

        protected abstract void Bootstrap();
    }
}