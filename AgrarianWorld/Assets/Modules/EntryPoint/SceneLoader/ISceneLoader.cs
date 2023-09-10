﻿using System;

namespace EntryPoint.SceneLoader
{
    public interface ISceneLoader
    {
        public string CurrentScene { get; set; }

        public void LoadScene(string name, Action onLoaded = null);
        public void UnloadScene(string name, Action onUnloaded = null);
    }
}