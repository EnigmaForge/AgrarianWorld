using UnityEngine;

namespace Modules.GenerationSystem {
    [CreateAssetMenu(fileName = nameof(RealisticTerrainGenerationConfig) + "_Default", menuName = "Configurations/GenerationSystem/" + nameof(RealisticTerrainGenerationConfig))]
    public class RealisticTerrainGenerationConfig : ScriptableObject {
        [field: SerializeField] public HeightmapResolutionType HeightmapResolution { get; private set; }
        [field: SerializeField] public int HeightmapDepth { get; private set; }
        [field: SerializeField] public float NoiseScale { get; private set; }
        [field: SerializeField] public Terrain TerrainPrefab { get; private set; }
    }
}