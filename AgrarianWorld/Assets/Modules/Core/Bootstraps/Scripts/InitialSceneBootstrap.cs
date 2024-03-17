using Modules.Core.SceneLoader;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace Modules.Core.Bootstraps {
    public class InitialSceneBootstrap : MonoBehaviour {
        [SerializeField] private GameObject _debugMenuPrefab;
        
        private void Start() {
            InitializeContext();
            InitializeDebugMenu();
            LoadFirstScene();
        }

        private void InitializeContext() =>
            ProjectContext.Instance.EnsureIsInitialized();

        private void InitializeDebugMenu() {
            GameObject debugMenu = Instantiate(_debugMenuPrefab);
            DontDestroyOnLoad(debugMenu);
        }

        private void LoadFirstScene() {
            ISceneLoader sceneLoader = ProjectContext.Instance.Container.Resolve<ISceneLoader>();
            sceneLoader.Load(SceneNames.MenuScene.ToString(), LoadSceneMode.Single);
        }
    }
}
