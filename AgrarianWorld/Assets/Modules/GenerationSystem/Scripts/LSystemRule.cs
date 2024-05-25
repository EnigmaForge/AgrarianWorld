using UnityEngine;

namespace Modules.GenerationSystem {
    [CreateAssetMenu(fileName = nameof(LSystemRule) + "_Default", menuName = "Configurations/GenerationSystem/" + nameof(LSystemRule))]
    public class LSystemRule : ScriptableObject {
        [field: SerializeField] public string Letter { get; private set; }
        [field: SerializeField] private string[] Results { get; set; }

        public string GetResult() =>
            Results[Random.Range(0, Results.Length)];
    }
}