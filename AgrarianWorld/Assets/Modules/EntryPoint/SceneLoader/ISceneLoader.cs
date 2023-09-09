using System;

namespace EntryPoint.SceneLoader
{
    public interface ISceneLoader
    {
        void LoadScene(string name, Action onLoaded = null);
        void UnloadScene(string name, Action onUnloaded = null);
    }
}