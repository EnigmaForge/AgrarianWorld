using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace EntryPoint.SceneLoader
{
    public class AsyncSceneLoader : ISceneLoader
    {
        public void LoadScene(string name, Action onLoaded = null)
        {
            AsyncOperation loadScene = SceneManager.LoadSceneAsync(name);
            loadScene.completed += _ => onLoaded?.Invoke();
        }

        public void UnloadScene(string name, Action onUnloaded = null)
        {
            AsyncOperation unloadScene = SceneManager.UnloadSceneAsync(name);
            unloadScene.completed += _ => onUnloaded?.Invoke();
        }
    }
}