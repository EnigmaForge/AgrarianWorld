using UnityEngine;

namespace Modules.GenerationSystem {
    public class RealisticTerrainGenerator : ITerrainGenerator {
        private readonly RealisticTerrainGenerationConfig _generationConfig;

        public RealisticTerrainGenerator(RealisticTerrainGenerationConfig realisticTerrainGenerationConfig) =>
            _generationConfig = realisticTerrainGenerationConfig;

        public void Generate(int seed) {
            Random.InitState(seed);
            
            Terrain terrain = Object.Instantiate(_generationConfig.TerrainPrefab);
            terrain.terrainData = GenerateTerrainData();
            if (terrain.TryGetComponent(out TerrainCollider terrainCollider))
                terrainCollider.terrainData = terrain.terrainData;
        }
        
        private TerrainData GenerateTerrainData() {
            int resolution = (int)_generationConfig.HeightmapResolution;
            TerrainData terrainData = new TerrainData {
                name = "Generated Data", 
                heightmapResolution = resolution + 1, 
                size = new Vector3(resolution, _generationConfig.HeightmapDepth, resolution)
            };
            terrainData.SetHeights(0, 0, GenerateHeights());
            
            return terrainData;
        }

        private float[,] GenerateHeights() {
            int resolution = (int)_generationConfig.HeightmapResolution;
            float scaledResolution = resolution * _generationConfig.NoiseScale;
            float[,] heights = new float[resolution, resolution];
            for (int x = 0; x < resolution; x++) {
                for (int y = 0; y < resolution; y++) {
                    float xCoordinate = x / scaledResolution;
                    float yCoordinate = y / scaledResolution;
                    heights[x, y] = Mathf.PerlinNoise(xCoordinate, yCoordinate);
                }
            }
    
            return heights;
        }
    }
}