using Modules.Core.SceneLoader;
using UnityEngine.SceneManagement;
using Zenject;
using Object = UnityEngine.Object;

namespace Modules.Core.GameReloadModule {
    public static class GameReloadService {
        #if UNITY_EDITOR
        [UnityEditor.InitializeOnLoadMethod]
        private static void SubscribeOnPlayModeStateChanged() =>
            UnityEditor.EditorApplication.playModeStateChanged += OnPlayModeStateChanged;

        private static void OnPlayModeStateChanged(UnityEditor.PlayModeStateChange playModeStateChange) {
            if (playModeStateChange != UnityEditor.PlayModeStateChange.EnteredPlayMode)
                return;
            
            if (SceneManager.GetActiveScene().name == SceneNames.InitialScene.ToString())
                return;

            LoadInitialScene();
        }
        #endif
        
        public static void Reload() {
            UnloadProjectContext();
            LoadInitialScene();
        }

        private static void UnloadProjectContext() {
            ProjectContext.Instance.Container.UnbindAll();
            Object.Destroy(ProjectContext.Instance.gameObject);
        }

        private static void LoadInitialScene() =>
            SceneManager.LoadScene(SceneNames.InitialScene.ToString());
    }
}
