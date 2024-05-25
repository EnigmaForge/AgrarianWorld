using System;
using UnityEngine;

namespace Modules.GenerationSystem {
    [Serializable]
    public struct HouseSpawnData {
        public GameObject HousePrefab;
        [Range(0f, 1f)] public float SpawnChance;
    }
}