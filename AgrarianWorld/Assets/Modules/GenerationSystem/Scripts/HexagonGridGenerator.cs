using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Modules.GenerationSystem {
    public class HexagonGridGenerator : IHexagonGridGenerator {
        private readonly HexagonGridGeneratorConfiguration _generationConfig;
        private readonly Dictionary<TileType, List<Vector3Int>> _tileCellPositions = new();

        public HexagonGridGenerator(HexagonGridGeneratorConfiguration generationConfig) =>
            _generationConfig = generationConfig;

        public void Generate(int seed) {
            Random.InitState(seed);
            
            Vector2Int mapSize = _generationConfig.MapSize;
            float[,] heights = GenerateHeights(mapSize, _generationConfig.NoiseScale, _generationConfig.Octaves, 
                                               _generationConfig.Lacunarity, _generationConfig.Persistence, 
                                               _generationConfig.VignetteIntensity, _generationConfig.Smoothness, seed);
            FillPositions(mapSize, heights);
            CreateTiles();
        }

        private void FillPositions(Vector2Int mapSize, float[,] heights) {
            for (int x = 0; x < mapSize.x; x++) {
                for (int y = 0; y < mapSize.y; y++) {
                    TileType tileType = GetTileTypeByHeight(heights[x, y]);
                    if(_tileCellPositions.TryGetValue(tileType, out List<Vector3Int> position))
                        position.Add(new(x, y, 0));
                    else
                        _tileCellPositions.Add(tileType, new List<Vector3Int> {
                            new(x, y, 0)
                        });
                }
            }
        }

        private void CreateTiles() {
            GameObject hexagonGridInstance = Object.Instantiate(_generationConfig.HexagonGridPrefab);
            if (hexagonGridInstance.TryGetComponent(out Grid hexagonGrid))
                GenerateMap(hexagonGrid, _tileCellPositions);
        }

        private void GenerateMap(Grid hexagonGrid, Dictionary<TileType, List<Vector3Int>> tileCellPositions) {
            foreach (KeyValuePair<TileType, List<Vector3Int>> tileCells in tileCellPositions) {
                foreach (Vector3Int gridPosition in tileCells.Value) {
                    Vector3 position = hexagonGrid.CellToWorld(gridPosition);
                    Object.Instantiate(GetTilePrefabByType(tileCells.Key), position, Quaternion.identity);
                }
            }
        }

        private TileType GetTileTypeByHeight(float height) =>
            _generationConfig.Tiles.First(tileData => height <= tileData.MaxHeight).TileType;

        private GameObject GetTilePrefabByType(TileType tileType) =>
            _generationConfig.Tiles.First(tileData => tileData.TileType == tileType).TilePrefab;

        public float[,] GenerateHeights(Vector2Int mapSize, float scale, int octaves, float lacunarity, float persistence, float vignetteIntensity = 0f, float smoothness = 0, int seed = 0) {
            float[,] heights = new float[mapSize.x, mapSize.y];

            float maxNoiseHeight = float.MinValue;
            float minNoiseHeight = float.MaxValue;

            for (int x = 0; x < mapSize.x; x++) {
                for (int y = 0; y < mapSize.y; y++) {
                    float xCoordinate = (float)(x + seed) / mapSize.x * scale;
                    float yCoordinate = (float)(y + seed) / mapSize.y * scale;
                    float noiseHeight = Mathf.PerlinNoise(xCoordinate, yCoordinate);
                    
                    float amplitude = 1;
                    float frequency = 1;

                    for (int octaveIndex = 0; octaveIndex < octaves; octaveIndex++) {
                        xCoordinate = (float)(x + seed) / mapSize.x * frequency;
                        yCoordinate = (float)(y + seed) / mapSize.y * frequency;
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
            if (vignetteIntensity > 0)
                ApplyVignette(mapSize, heights, vignetteIntensity, smoothness);

            return heights;
        }

        private static void NormalizeHeights(Vector2Int mapSize, float[,] heights, float minNoiseHeight, float maxNoiseHeight) {
            for (int x = 0; x < mapSize.x; x++)
                for (int y = 0; y < mapSize.y; y++)
                    heights[x, y] = Mathf.InverseLerp(minNoiseHeight, maxNoiseHeight, heights[x, y]);
        }

        private void ApplyVignette(Vector2 mapSize, float[,] heights, float vignetteIntensity, float smoothness) {
            float centerX = mapSize.x / 2f;
            float centerY = mapSize.y / 2f;
            float maxDistance = Mathf.Sqrt(centerX * centerX + centerY * centerY);

            for (int x = 0; x < mapSize.x; x++) {
                for (int y = 0; y < mapSize.y; y++) {
                    float distanceToCenter = Mathf.Sqrt((x - centerX) * (x - centerX) + (y - centerY) * (y - centerY));
                    float normalizedDistance = distanceToCenter / maxDistance;
                    float vignetteFactor = Mathf.Clamp01(1f - normalizedDistance * vignetteIntensity);

                    if(vignetteFactor > 0)
                        vignetteFactor = 1f - (1f - vignetteFactor) * smoothness;

                    heights[x, y] *= vignetteFactor;
                }
            }
        }
    }
}