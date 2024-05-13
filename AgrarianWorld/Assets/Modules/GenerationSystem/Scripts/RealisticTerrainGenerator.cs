using System.Collections.Generic;
using UnityEngine;

namespace Modules.GenerationSystem {
    public class RealisticTerrainGenerator : ITerrainGenerator {
        private readonly RealisticTerrainGenerationConfig _generationConfig;
        private readonly TerrainSurfacePointsModel _terrainSurfacePointsModel;

        public RealisticTerrainGenerator(RealisticTerrainGenerationConfig realisticTerrainGenerationConfig, TerrainSurfacePointsModel terrainSurfacePointsModel) {
            _generationConfig = realisticTerrainGenerationConfig;
            _terrainSurfacePointsModel = terrainSurfacePointsModel;
        }

        public void Generate(int seed) {
            Random.InitState(seed);

            Terrain terrain = Object.Instantiate(_generationConfig.TerrainPrefab);
            terrain.heightmapPixelError = 1;
            terrain.materialTemplate = _generationConfig.TerrainMaterial;
            terrain.terrainData = GenerateTerrainData();
            if (terrain.TryGetComponent(out TerrainCollider terrainCollider))
                terrainCollider.terrainData = terrain.terrainData;

            _terrainSurfacePointsModel.SurfaceCenter = terrain.transform.position + terrain.terrainData.size / 2f;
            _terrainSurfacePointsModel.Points = GetTerrainPoints(terrain);
        }

        private List<Vector3> GetTerrainPoints(Terrain terrain) {
            List<Vector3> points = new();
            
            TerrainData terrainData = terrain.terrainData;
            Vector3 terrainSize = terrainData.size;
            Vector3 terrainPosition = terrain.transform.position;

            for (float x = 0; x < terrainSize.x; x ++) {
                for (float z = 0; z < terrainSize.z; z ++) {
                    float y = terrain.SampleHeight(new Vector3(x, 0, z) + terrainPosition);
                    points.Add(new Vector3(x, y, z) + terrainPosition);
                }
            }

            return points;
        }

        private TerrainData GenerateTerrainData() {
            int resolution = (int)_generationConfig.HeightmapResolution;
            TerrainData terrainData = new TerrainData {
                name = "Generated Data", 
                heightmapResolution = resolution + 1, 
                size = _generationConfig.TerrainSize,
            };

            float[,] heights = GenerateHeightsByNoise(resolution);
            ApplyErosion((int)_generationConfig.HeightmapResolution, heights);
            terrainData.SetHeights(0, 0, heights);
            
            return terrainData;
        }

        public float[,] GenerateHeightsByNoise(int resolution) {
            float scale = _generationConfig.NoiseScale;
            float scaledResolution = resolution * scale;
            float[,] heights = new float[resolution, resolution];
            float maxNoiseHeight = float.MinValue;
            float minNoiseHeight = float.MaxValue;

            maxNoiseHeight = GenerateNoiseHeights(resolution, scaledResolution, maxNoiseHeight, heights, ref minNoiseHeight);
            NormalizeHeights(maxNoiseHeight, minNoiseHeight, resolution, heights);
            AddVignette(resolution, heights);
            
            return heights;
        }

        private float GenerateNoiseHeights(int resolution, float scaledResolution, float maxNoiseHeight, float[,] heights, ref float minNoiseHeight) {
            for (int x = 0; x < resolution; x++) {
                for (int y = 0; y < resolution; y++) {
                    float amplitude = 1;
                    float frequency = 1;
                    float noiseHeight = 0;

                    for (int octave = 0; octave < _generationConfig.Octaves; octave++) {
                        float xCoordinate = x / scaledResolution * frequency;
                        float yCoordinate = y / scaledResolution * frequency;
                        float perlinValue = Mathf.PerlinNoise(xCoordinate, yCoordinate) * 2 - 1;
                        noiseHeight += perlinValue * amplitude;

                        amplitude *= _generationConfig.Persistence;
                        frequency *= _generationConfig.Lacunarity;
                    }

                    if (noiseHeight > maxNoiseHeight) {
                        maxNoiseHeight = noiseHeight;
                    } else if (noiseHeight < minNoiseHeight) {
                        minNoiseHeight = noiseHeight;
                    }

                    heights[x, y] = noiseHeight;
                }
            }

            return maxNoiseHeight;
        }

        private void NormalizeHeights(float maxNoiseHeight, float minNoiseHeight, int resolution, float[,] heights) {
            if (maxNoiseHeight != minNoiseHeight) {
                for (int x = 0; x < resolution; x++) {
                    for (int y = 0; y < resolution; y++) {
                        heights[x, y] = Mathf.InverseLerp(minNoiseHeight, maxNoiseHeight, heights[x, y]);
                    }
                }
            }
        }

        private void AddVignette(int resolution, float[,] heights) {
            float center = resolution / 2f;
            float maxDistance = Mathf.Sqrt(center * center + center * center);

            for (int x = 0; x < resolution; x++) {
                for (int y = 0; y < resolution; y++) {
                    float distanceToCenter = Mathf.Sqrt((x - center) * (x - center) + (y - center) * (y - center));
                    float normalizedDistance = distanceToCenter / maxDistance;
                    float vignetteFactor = Mathf.Clamp01(1 - normalizedDistance * _generationConfig.VignetteIntensity);
                    heights[x, y] *= vignetteFactor;
                }
            }
        }

        private void ApplyErosion(int resolution, float[,] heights) {
            for (int i = 0; i < _generationConfig.ErosionIterations; i++) {
                for (int x = 0; x < resolution; x++) {
                    for (int y = 0; y < resolution; y++) {
                        float centerHeight = heights[x, y];
                        float minNeighbourHeight = centerHeight;

                        for (int dx = -1; dx <= 1; dx++) {
                            for (int dy = -1; dy <= 1; dy++) {
                                if (x + dx >= 0 && x + dx < resolution && y + dy >= 0 && y + dy < resolution) {
                                    minNeighbourHeight = Mathf.Min(minNeighbourHeight, heights[x + dx, y + dy]);
                                }
                            }
                        }

                        heights[x, y] -= _generationConfig.ErosionStrength * (centerHeight - minNeighbourHeight);
                    }
                }
            }
        }
    }
}