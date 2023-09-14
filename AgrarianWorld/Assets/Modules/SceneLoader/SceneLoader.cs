using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SceneLoader
{
    public class AsyncSceneLoader : ISceneLoader
    {
        public string CurrentScene { get; set; }

        public AsyncSceneLoader()
        {
            CurrentScene = SceneManager.GetActiveScene().name;
        }

        public void LoadScene(string name, Action onLoaded = null)
        {
            AsyncOperation loadScene = SceneManager.LoadSceneAsync(name);
            loadScene.completed += _ => {
                CurrentScene = name;
                onLoaded?.Invoke(); 
            };
        }

        public void UnloadScene(string name, Action onUnloaded = null)
        {
            AsyncOperation unloadScene = SceneManager.UnloadSceneAsync(name);
            unloadScene.completed += _ => onUnloaded?.Invoke();
        }
    }
}