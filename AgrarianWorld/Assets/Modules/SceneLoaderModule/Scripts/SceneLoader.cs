using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace SceneLoaderModule {
    public class SceneLoader : ISceneLoader, IInitializable {
        public void Initialize() =>
            LoadInitialScene();

        private void LoadInitialScene() {
            if (IsInitialScene() is false)
                Load(SceneNames.InitialScene);
        }

        private bool IsInitialScene() =>
            SceneManager.GetActiveScene().name == SceneNames.InitialScene.ToString();

        public void Load(SceneNames sceneName, LoadSceneMode loadSceneMode = LoadSceneMode.Single, Action onStart = null, Action onComplete = null) {
            onStart?.Invoke();
            AsyncOperation loadSceneOperation = SceneManager.LoadSceneAsync(sceneName.ToString(), loadSceneMode);
            loadSceneOperation.completed += _ => {
                onComplete?.Invoke();
            };
        }

        public void Unload(SceneNames sceneName, UnloadSceneOptions unloadSceneOptions = UnloadSceneOptions.None, Action onStart = null, Action onComplete = null) {
            onStart?.Invoke();
            AsyncOperation unloadSceneOperation = SceneManager.UnloadSceneAsync(sceneName.ToString(), unloadSceneOptions);
            unloadSceneOperation.completed += _ => {
                onComplete?.Invoke();
            };
        }
    }
}