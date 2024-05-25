using System;
using UnityEngine;

namespace Modules.GenerationSystem {
    [Serializable]
    public struct GenerationObjectData {
        public GameObject Prefab;
        public int Count;
    }
}