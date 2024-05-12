using System.Collections.Generic;
using UnityEngine;

namespace Modules.GenerationSystem {
    public class TerrainSurfacePointsModel {
        private List<Vector3> _points;

        public List<Vector3> Points {
            get =>
                new(_points);
            set =>
                _points = value;
        }
        
        public Vector3 SurfaceCenter { get; set; }
    }
}