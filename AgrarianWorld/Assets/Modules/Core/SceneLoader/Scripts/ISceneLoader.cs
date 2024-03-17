using System;
using UnityEngine.SceneManagement;

namespace Modules.Core.SceneLoader {
    public interface ISceneLoader {
        public void OnPreLoad(Action callback);
        public void OnPostLoad(Action callback);
        public void Load(string sceneName, LoadSceneMode loadSceneMode, bool makeActive = false);
        public void Unload(string sceneName);
    }
}