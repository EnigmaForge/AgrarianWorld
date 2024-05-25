using System;
using System.Collections.Generic;
using UnityEngine;

namespace Modules.GenerationSystem {
    [Serializable]
    public class DecorationsData {
        [field: SerializeField] public List<GameObject> DecorationPrefabs { get; private set; }
        [field: SerializeField] public float NoiseScale { get; private set; }
    }
}