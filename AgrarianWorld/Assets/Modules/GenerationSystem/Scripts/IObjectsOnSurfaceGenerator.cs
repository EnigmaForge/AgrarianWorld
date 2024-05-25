using System.Collections.Generic;
using UnityEngine;

namespace Modules.GenerationSystem {
    public interface IObjectsOnSurfaceGenerator : IObjectsGenerator {
        public IObjectsOnSurfaceGenerator SetGenerationPoints(List<Vector3> generationPoints);
        public IObjectsOnSurfaceGenerator SetGenerationRange(float minHeight, float maxHeight, float range, Vector3 center);
    }
}