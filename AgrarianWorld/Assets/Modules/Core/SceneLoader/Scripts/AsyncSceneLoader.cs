using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Modules.Core.SceneLoader {
    public class AsyncSceneLoader : ISceneLoader {
        private Action _preLoadCallback;
        private Action _postLoadCallback;
        
        public void OnPreLoad(Action callback) =>
            _preLoadCallback = callback;

        public void OnPostLoad(Action callback) =>
            _postLoadCallback = callback;

        public void Load(string sceneName, LoadSceneMode loadSceneMode, bool makeActive = false) {
            SceneLoaderActions sceneLoaderActions = new SceneLoaderActions(_preLoadCallback, _postLoadCallback);
            sceneLoaderActions.OnPreLoad();
            
            AsyncOperation loadSceneOperation = SceneManager.LoadSceneAsync(sceneName, loadSceneMode);
            
            loadSceneOperation.completed += _ => {
                if (makeActive)
                    SceneManager.SetActiveScene(SceneManager.GetSceneByName(sceneName));
                
                sceneLoaderActions.OnPostLoad();
            };
        }

        public void Unload(string sceneName) {
            SceneLoaderActions sceneLoaderActions = new SceneLoaderActions(_preLoadCallback, _postLoadCallback);
            sceneLoaderActions.OnPreLoad();
            AsyncOperation unloadSceneOperation = SceneManager.UnloadSceneAsync(sceneName);
            unloadSceneOperation.completed += _ => sceneLoaderActions.OnPostLoad();
        }
    }
}