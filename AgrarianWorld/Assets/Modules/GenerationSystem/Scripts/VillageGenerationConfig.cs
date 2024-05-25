using System.Collections.Generic;
using UnityEngine;

namespace Modules.GenerationSystem {
    [CreateAssetMenu(fileName = nameof(VillageGenerationConfig) + "_Default", menuName = "Configurations/GenerationSystem/" + nameof(VillageGenerationConfig))]
    public class VillageGenerationConfig : ScriptableObject {
        [field: SerializeField] public LSystemRule[] Rules { get; private set; }
        [field: SerializeField] public string RootSentence { get; private set; }
        [field: SerializeField, Min(1)] public int IterationsLimit { get; private set; }
        [field: SerializeField, Min(1)] public int Length { get; private set; }
        [field: SerializeField] public float Angle { get; private set; }
        [field: SerializeField] public List<Vector3> NeighborsDirections { get; private set; }
        [field: SerializeField] public GameObject RoadStraight { get; private set; }
        [field: SerializeField] public List<HouseSpawnData> Houses { get; private set; }
    }
}