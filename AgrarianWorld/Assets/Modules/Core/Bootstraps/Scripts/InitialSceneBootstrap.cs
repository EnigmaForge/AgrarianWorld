using UnityEngine;
using Zenject;

namespace Modules.Core.Bootstraps {
    public class InitialSceneBootstrap : MonoBehaviour {
        private void Awake() {
            InitializeContext();
            LoadFirstScene();
        }

        private void InitializeContext() =>
            ProjectContext.Instance.EnsureIsInitialized();

        private void LoadFirstScene() {
            
        }
    }
}
