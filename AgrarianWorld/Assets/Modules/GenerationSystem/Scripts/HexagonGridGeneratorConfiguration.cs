using UnityEngine;

namespace Modules.GenerationSystem {
    [CreateAssetMenu(fileName = nameof(HexagonGridGeneratorConfiguration) + "_Default", menuName = "Configurations/GenerationSystem/" + nameof(HexagonGridGeneratorConfiguration))]
    public class HexagonGridGeneratorConfiguration : ScriptableObject {
        [field: SerializeField] public Vector2Int MapSize { get; private set; }
        [field: SerializeField, Header("Noise")] public float NoiseScale { get; private set; }
        [field: SerializeField] public int Octaves { get; private set; }
        [field: SerializeField] public float Lacunarity { get; private set; }
        [field: SerializeField] public float Persistence { get; private set; }
        [field: SerializeField, Header("Vignette")] public float VignetteIntensity { get; private set; }
        [field: SerializeField, Header("Resources")] public GameObject HexagonGridPrefab { get; private set; }
        [field: SerializeField] public GameObject GrassHexagonPrefab { get; private set; }
    }
}