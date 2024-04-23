using UnityEngine;
using Zenject;

namespace Modules.Core.Bootstraps {
    public class MenuSceneBootstrap : MonoBehaviour {
        [SerializeField] private SceneContext _sceneContext;
        [SerializeField] private GameObject _menuCanvas;
        
        private void Start() {
            InitializeContext();
            LoadUI();
        }

        private void InitializeContext() {
            SceneContext sceneContext = Instantiate(_sceneContext);
            sceneContext.Run();
        }

        private void LoadUI() =>
            Instantiate(_menuCanvas);
    }
}