using System;

namespace Modules.Core.SceneLoader {
    public sealed class SceneLoaderActions {
        public event Action PreLoad;
        public event Action PostLoad;

        public SceneLoaderActions(Action onPreLoad, Action onPostLoad) {
            PreLoad = onPreLoad;
            PostLoad = onPostLoad;
        }

        public void OnPreLoad() =>
            PreLoad?.Invoke();

        public void OnPostLoad() =>
            PostLoad?.Invoke();
    }
}