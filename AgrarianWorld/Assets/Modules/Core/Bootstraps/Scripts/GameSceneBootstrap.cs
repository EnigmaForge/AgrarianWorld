using UnityEngine;
using Zenject;

namespace Modules.Core.Bootstraps {
    public class GameSceneBootstrap : MonoBehaviour {
        [SerializeField] private SceneContext _sceneContext;
        
        private void Start() =>
            InitializeContext();

        private void InitializeContext() {
            SceneContext sceneContext = Instantiate(_sceneContext);
            sceneContext.Run();
        }
    }
}