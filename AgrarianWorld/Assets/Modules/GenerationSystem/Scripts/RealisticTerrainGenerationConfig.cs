using UnityEngine;

namespace Modules.GenerationSystem {
    [CreateAssetMenu(fileName = nameof(RealisticTerrainGenerationConfig) + "_Default", menuName = "Configurations/GenerationSystem/" + nameof(RealisticTerrainGenerationConfig))]
    public class RealisticTerrainGenerationConfig : ScriptableObject {
        [field: SerializeField] public HeightmapResolutionType HeightmapResolution { get; private set; }
        [field: SerializeField] public Vector3 TerrainSize { get; private set; }
        [field: SerializeField, Header("Noise")] public float NoiseScale { get; private set; }
        [field: SerializeField] public int Octaves { get; private set; }
        [field: SerializeField] public float Lacunarity { get; private set; }
        [field: SerializeField] public float Persistence { get; private set; }
        [field: SerializeField, Header("Erosion")] public int ErosionIterations { get; private set; }
        [field: SerializeField] public float ErosionStrength { get; private set; }
        [field: SerializeField, Header("Vignette")] public float VignetteIntensity { get; private set; }
        [field: SerializeField, Header("Resources")] public Terrain TerrainPrefab { get; private set; }
        [field: SerializeField] public Material TerrainMaterial { get; private set; }
    }
}