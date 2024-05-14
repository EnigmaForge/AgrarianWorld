using System;
using UnityEngine;

namespace Modules.GenerationSystem {
    [Serializable]
    public class TileData {
        [field: SerializeField] public TileType TileType { get; private set; }
        [field: SerializeField] public GameObject TilePrefab { get; private set; }
        [field: SerializeField] public float MaxHeight { get; private set; }
    }
}