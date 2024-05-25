using System.Collections.Generic;
using UnityEngine;

namespace Modules.GenerationSystem {
    public class VillageGenerationModel {
        public string Sentence { get; set; }
        public Vector3 VillageCenter { get; set; }
        public List<Vector3> RoadPositions { get; set; } = new();
        public List<Vector3> HousePositions { get; set; } = new();
    }
}