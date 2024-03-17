using System;
using Modules.Core.SceneLoader;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace Modules.Core.Bootstraps {
    public class InitialSceneBootstrap : MonoBehaviour {
        private void Start() {
            InitializeContext();
            LoadFirstScene();
        }

        private void InitializeContext() =>
            ProjectContext.Instance.EnsureIsInitialized();

        private void LoadFirstScene() {
            ISceneLoader sceneLoader = ProjectContext.Instance.Container.Resolve<ISceneLoader>();
            sceneLoader.Load(SceneNames.MenuScene.ToString(), LoadSceneMode.Single);
        }
    }
}
