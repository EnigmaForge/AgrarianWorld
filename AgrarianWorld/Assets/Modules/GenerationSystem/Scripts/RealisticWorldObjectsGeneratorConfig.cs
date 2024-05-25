using System.Collections.Generic;
using UnityEngine;

namespace Modules.GenerationSystem {
    [CreateAssetMenu(fileName = nameof(RealisticWorldObjectsGeneratorConfig) + "_Default", menuName = "Configurations/GenerationSystem/" + nameof(RealisticWorldObjectsGeneratorConfig))]
    public class RealisticWorldObjectsGeneratorConfig : ScriptableObject {
        [field: SerializeField] public List<GenerationObjectData> GenerationObjects { get; private set; }
        [field: SerializeField] public float MinHeight { get; private set; }
        [field: SerializeField] public float MaxHeight { get; private set; }
        [field: SerializeField] public float Range { get; private set; }
    }
}