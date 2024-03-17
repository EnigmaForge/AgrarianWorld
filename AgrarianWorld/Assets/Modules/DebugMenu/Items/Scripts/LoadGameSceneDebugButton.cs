using Modules.Core.SceneLoader;
using UnityEngine.SceneManagement;
using Zenject;

namespace Modules.DebugMenu.Items {
    public class LoadGameSceneDebugButton : DebugButtonBehaviour {
        private ISceneLoader _sceneLoader;

        private void Start() =>
            _sceneLoader = ProjectContext.Instance.Container.Resolve<ISceneLoader>();

        public override void OnClick() {
            _sceneLoader.OnComplete(UnloadOtherScenes)
                        .Load(SceneNames.GameScene.ToString(), LoadSceneMode.Additive, true);
        }

        private void UnloadOtherScenes() {
            int sceneCount = SceneManager.sceneCount;
            for (int i = 0; i < sceneCount; i++) {
                Scene scene = SceneManager.GetSceneAt(i);
                if (scene.name != SceneNames.GameScene.ToString())
                    _sceneLoader.Unload(scene.name);
            }
        }
    }
}