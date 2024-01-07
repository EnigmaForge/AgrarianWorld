using System;
using UnityEngine.SceneManagement;

namespace SceneLoaderModule {
    public interface ISceneLoader {
        public void Load(SceneNames sceneName, LoadSceneMode loadSceneMode = LoadSceneMode.Single, Action onStart = null, Action onComplete = null);
        public void Unload(SceneNames sceneName, UnloadSceneOptions unloadSceneOptions = UnloadSceneOptions.None, Action onStart = null, Action onComplete = null);
    }
}

