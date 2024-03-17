using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Modules.Core.SceneLoader {
    public class AsyncSceneLoader : ISceneLoader {
        private Action _startCallback;
        private Action _completeCallback;
        
        public ISceneLoader OnStart(Action callback) {
            _startCallback = callback;
            return this;
        }

        public ISceneLoader OnComplete(Action callback) {
            _completeCallback = callback;
            return this;
        }

        public void Load(string sceneName, LoadSceneMode loadSceneMode, bool makeActive = false) {
            SceneLoaderActions sceneLoaderActions = new SceneLoaderActions(_startCallback, _completeCallback);
            sceneLoaderActions.OnPreLoad();
            
            AsyncOperation loadSceneOperation = SceneManager.LoadSceneAsync(sceneName, loadSceneMode);
            
            loadSceneOperation.completed += _ => {
                if (makeActive)
                    SceneManager.SetActiveScene(SceneManager.GetSceneByName(sceneName));
                
                sceneLoaderActions.OnPostLoad();
            };
        }

        public void Unload(string sceneName) {
            SceneLoaderActions sceneLoaderActions = new SceneLoaderActions(_startCallback, _completeCallback);
            sceneLoaderActions.OnPreLoad();
            AsyncOperation unloadSceneOperation = SceneManager.UnloadSceneAsync(sceneName);
            unloadSceneOperation.completed += _ => sceneLoaderActions.OnPostLoad();
        }
    }
}