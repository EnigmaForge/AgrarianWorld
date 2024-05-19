using System;
using UnityEngine;

namespace Modules.GenerationSystem {
    [Serializable]
    public class BiomeData {
        [field: SerializeField] public BiomeType BiomeType { get; private set; }
        [field: SerializeField] public GameObject TilePrefab { get; private set; }
        [field: SerializeField] public float SpawnChance { get; private set; }
        [field: SerializeField] public Color Color { get; private set; }
    }
}