using System.Collections.Generic;
using UnityEngine;

namespace Modules.GenerationSystem {
    [CreateAssetMenu(fileName = nameof(HexagonGridGeneratorConfiguration) + "_Default", menuName = "Configurations/GenerationSystem/" + nameof(HexagonGridGeneratorConfiguration))]
    public class HexagonGridGeneratorConfiguration : ScriptableObject {
        [field: SerializeField, Range(0, 999999)] public int Seed { get; private set; }
        [field: SerializeField] public Vector2Int MapSize { get; private set; }
        [field: SerializeField, Header("Noise")] public float NoiseScale { get; private set; }
        [field: SerializeField] public int Octaves { get; private set; }
        [field: SerializeField] public float Lacunarity { get; private set; }
        [field: SerializeField] public float Persistence { get; private set; }
        [field: SerializeField, Header("Vignette")] public float VignetteIntensity { get; private set; }
        [field: SerializeField, Range(0f, 1f)] public float Smoothness { get; private set; }
        [field: SerializeField, Header("Resources")] public GameObject HexagonGridPrefab { get; private set; }
        [field: SerializeField] public List<TileData> Tiles { get; private set; }
    }
}