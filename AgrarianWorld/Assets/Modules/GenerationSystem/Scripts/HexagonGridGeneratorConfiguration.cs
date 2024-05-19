using System.Collections.Generic;
using UnityEngine;

namespace Modules.GenerationSystem {
    [CreateAssetMenu(fileName = nameof(HexagonGridGeneratorConfiguration) + "_Default", menuName = "Configurations/GenerationSystem/" + nameof(HexagonGridGeneratorConfiguration))]
    public class HexagonGridGeneratorConfiguration : ScriptableObject {
        [field: SerializeField] public Vector2Int MapSize { get; private set; }
        [field: SerializeField, Range(1, 20)] public int BiomeSize { get; private set; }
        [field: SerializeField, Header("Noise")] public float NoiseScale { get; private set; }
        [field: SerializeField] public float NoiseStrength { get; private set; }
        [field: SerializeField]  public int WaterOffset { get; private set; }
        [field: SerializeField, Header("Resources")] public GameObject HexagonGridPrefab { get; private set; }
        [field: SerializeField] public List<BiomeData> Biomes { get; private set; }
    }
}