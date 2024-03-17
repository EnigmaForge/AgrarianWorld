using UnityEngine;
using Zenject;

namespace Modules.Core.Bootstraps {
    public class MenuSceneBootstrap : BootstrapBehaviour {
        [SerializeField] private SceneContext _sceneContext;
        
        protected override void Bootstrap() {
            InitializeContext();
        }

        private void InitializeContext() {
            SceneContext sceneContext = Instantiate(_sceneContext);
            sceneContext.Run();
        }
    }
}