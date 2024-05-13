using System.Collections.Generic;
using UnityEngine;

namespace Modules.GenerationSystem {
    public class HexagonGridGenerator : IHexagonGridGenerator {
        private readonly HexagonGridGeneratorConfiguration _generationConfig;

        public HexagonGridGenerator(HexagonGridGeneratorConfiguration generationConfig) =>
            _generationConfig = generationConfig;

        public void Generate(int seed) {
            Random.InitState(seed);
            
            Vector2Int mapSize = _generationConfig.MapSize;
            float[,] heights = GenerateHeights(mapSize, _generationConfig.NoiseScale, _generationConfig.Octaves, _generationConfig.Lacunarity, _generationConfig.Persistence, _generationConfig.VignetteIntensity);
            List<Vector3Int> grassPositions = new();
            for (int x = 0; x < mapSize.x; x++)
                for (int y = 0; y < mapSize.y; y++)
                    if (heights[x, y] >= 0.5f)
                        grassPositions.Add(new Vector3Int(x, y, 0));

            GameObject hexagonGridInstance = Object.Instantiate(_generationConfig.HexagonGridPrefab);
            if (hexagonGridInstance.TryGetComponent(out Grid hexagonGrid))
                GenerateMap(hexagonGrid, grassPositions);
        }

        private void GenerateMap(Grid hexagonGrid, List<Vector3Int> grassPositions) {
            foreach (Vector3Int cellPosition in grassPositions) {
                Vector3 grassPosition = hexagonGrid.CellToWorld(cellPosition);
                Object.Instantiate(_generationConfig.GrassHexagonPrefab, grassPosition, Quaternion.identity);
            }
        }

        public float[,] GenerateHeights(Vector2Int mapSize, float scale, int octaves, float lacunarity, float persistence, float vignetteIntensity = 0f) {
            float[,] heights = new float[mapSize.x, mapSize.y];

            float maxNoiseHeight = float.MinValue;
            float minNoiseHeight = float.MaxValue;

            for (int x = 0; x < mapSize.x; x++) {
                for (int y = 0; y < mapSize.y; y++) {
                    float xCoordinate = (float)x / mapSize.x * scale;
                    float yCoordinate = (float)y / mapSize.y * scale;
                    float noiseHeight = Mathf.PerlinNoise(xCoordinate, yCoordinate);
                    
                    float amplitude = 1;
                    float frequency = 1;

                    for (int octaveIndex = 0; octaveIndex < octaves; octaveIndex++) {
                        xCoordinate = (float)x / mapSize.x * scale * frequency;
                        yCoordinate = (float)y / mapSize.y * scale * frequency;
                        float octaveNoise = Mathf.PerlinNoise(xCoordinate, yCoordinate);
                        noiseHeight += octaveNoise * amplitude;

                        amplitude *= persistence;
                        frequency *= lacunarity;
                    }

                    if (noiseHeight > maxNoiseHeight)
                        maxNoiseHeight = noiseHeight;
                    else if (noiseHeight < minNoiseHeight)
                        minNoiseHeight = noiseHeight;
                    
                    heights[x, y] = noiseHeight;
                }
            }

            NormalizeHeights(mapSize, heights, minNoiseHeight, maxNoiseHeight);
            ApplyVignette(mapSize, heights, vignetteIntensity);

            return heights;
        }

        private static void NormalizeHeights(Vector2Int mapSize, float[,] heights, float minNoiseHeight, float maxNoiseHeight) {
            for (int x = 0; x < mapSize.x; x++)
                for (int y = 0; y < mapSize.y; y++)
                    heights[x, y] = Mathf.InverseLerp(minNoiseHeight, maxNoiseHeight, heights[x, y]);
        }

        private void ApplyVignette(Vector2 mapSize, float[,] heights, float vignetteIntensity) {
            float centerX = mapSize.x / 2f;
            float centerY = mapSize.y / 2f;
            float maxDistance = Mathf.Sqrt(centerX * centerX + centerY * centerY);

            for (int x = 0; x < mapSize.x; x++) {
                for (int y = 0; y < mapSize.y; y++) {
                    float distanceToCenter = Mathf.Sqrt((x - centerX) * (x - centerX) + (y - centerY) * (y - centerY));
                    float normalizedDistance = distanceToCenter / maxDistance;
                    float vignetteFactor = Mathf.Clamp01(1 - normalizedDistance * vignetteIntensity);
                    heights[x, y] *= vignetteFactor;
                }
            }
        }
    }
}