using System;
using UnityEngine.SceneManagement;

namespace Modules.Core.SceneLoader {
    public interface ISceneLoader {
        public ISceneLoader OnStart(Action callback);
        public ISceneLoader OnComplete(Action callback);
        public void Load(string sceneName, LoadSceneMode loadSceneMode, bool makeActive = false);
        public void Unload(string sceneName);
    }
}