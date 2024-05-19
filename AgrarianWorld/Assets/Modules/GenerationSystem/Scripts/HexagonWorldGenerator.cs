using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Modules.GenerationSystem {
    public class HexagonWorldGenerator : IHexagonWorldGenerator {
        private readonly HexagonGridGeneratorConfiguration _generationConfig;
        private BiomeDataHolder[,] _biomes;

        public HexagonWorldGenerator(HexagonGridGeneratorConfiguration generationConfig) =>
            _generationConfig = generationConfig;
        
        public void Generate(int seed) {
            Random.InitState(seed);

            Vector2Int mapSize = _generationConfig.MapSize;
            int biomeSize = _generationConfig.BiomeSize;
            List<BiomeData> tiles = _generationConfig.Biomes;
            int waterOffset = _generationConfig.WaterOffset;
            GameObject hexagonGridInstance = Object.Instantiate(_generationConfig.HexagonGridPrefab);
            if (hexagonGridInstance.TryGetComponent(out Grid hexagonGrid)) {
                GenerateTiles(hexagonGrid, mapSize, biomeSize, tiles, waterOffset);
                GenerateDecorations();
            }
        }

        private void GenerateTiles(Grid hexagonGrid, Vector2Int mapSize, int biomeSize, List<BiomeData> tiles, int waterOffset) {
            Vector2Int cellSize = mapSize / biomeSize;
           _biomes = GenerateBiomesData(cellSize, biomeSize, tiles, waterOffset);

            for (int i = 0; i < mapSize.x; i++) {
                for (int j = 0; j < mapSize.y; j++) {
                    int gridX = i / cellSize.x;
                    int gridY = j / cellSize.y;
                    float nearestDistance = Mathf.Infinity;
                    BiomeData biomeData = default;
                    Vector2Int biomeCoordinates = new Vector2Int();

                    for (int a = -1; a < 2; a++) {
                        for (int b = -1; b < 2; b++) {
                            int x = gridX + a;
                            int y = gridY + b;

                            if (x < 0 || y < 0 || x >= _generationConfig.BiomeSize || y >= _generationConfig.BiomeSize) 
                                continue;

                            float noise = Mathf.PerlinNoise(i * _generationConfig.NoiseScale, j * _generationConfig.NoiseScale) * _generationConfig.NoiseStrength;
                            Vector2 center = new Vector2(_biomes[x, y].Center.x + noise, _biomes[x, y].Center.y + noise);
                            float distance = Vector2.Distance(new Vector2(i, j), center);

                            if (distance < nearestDistance) {
                                nearestDistance = distance;
                                biomeData = _biomes[x, y].BiomeData;
                                biomeCoordinates.Set(x, y);
                            }
                        }
                    }

                    Vector3 position = hexagonGrid.CellToWorld(new Vector3Int(i, j, 0));
                    Object.Instantiate(biomeData!.TilePrefab, position, Quaternion.identity);
                    _biomes[biomeCoordinates.x, biomeCoordinates.y].TilePositions.Add(position);
                }
            }
        }

        private BiomeDataHolder[,] GenerateBiomesData(Vector2Int cellSize, int biomeSize, List<BiomeData> biomes, int waterOffset) {
            BiomeDataHolder[,] biomeHolders = new BiomeDataHolder[biomeSize, biomeSize];
            BiomeData waterBiome = biomes.Find(tile => tile.BiomeType == BiomeType.Water);

            for (int i = 0; i < biomeSize; i++) {
                for (int j = 0; j < biomeSize; j++) {
                    Vector2Int biomeCenter = new Vector2Int(i * cellSize.x + Random.Range(0, cellSize.x), j * cellSize.y + Random.Range(0, cellSize.y));
                    
                    BiomeData biomeData;
                    if (i < waterOffset || j < waterOffset || i >= biomeSize - waterOffset || j >= biomeSize - waterOffset)
                        biomeData = waterBiome;
                    else
                        biomeData = biomes.First(biome => Random.value <= biome.SpawnChance);

                    biomeHolders[i, j] = new BiomeDataHolder(biomeData, biomeCenter);
                }
            }

            return biomeHolders;
        }

        private void GenerateDecorations() {
            foreach (BiomeDataHolder biomeDataHolder in _biomes) {
                DecorationsData decorationsData = biomeDataHolder.BiomeData.Decorations;
                
                if (decorationsData.DecorationPrefabs is { Count: 0 } )
                    continue;

                float noiseScale = biomeDataHolder.BiomeData.Decorations.NoiseScale;
                float factor = 1f / (decorationsData.DecorationPrefabs.Count + 1);
                
                foreach (Vector3 position in biomeDataHolder.TilePositions) {
                    float xCoordinate = position.x * noiseScale;
                    float yCoordinate = position.z * noiseScale;
                    float perlinValue = Mathf.PerlinNoise(xCoordinate, yCoordinate);

                    GameObject prefabToSpawn = null;
                    for (int i = 0; i < decorationsData.DecorationPrefabs.Count; i++) {
                        if (perlinValue <= factor)
                            continue;
                        
                        if (perlinValue <= factor + factor * (i + 1)) {
                            prefabToSpawn = decorationsData.DecorationPrefabs[i];
                            break;
                        }
                    }

                    if (prefabToSpawn != null)
                        Object.Instantiate(prefabToSpawn, position, Quaternion.identity);
                }
            }
        }

        #if UNITY_EDITOR
        public Texture2D GenerateVoronoiDiagram(int seed) {
            Random.InitState(seed);
            
            Texture2D texture = new Texture2D(_generationConfig.MapSize.x, _generationConfig.MapSize.y);
            
            Vector2Int cellSize = _generationConfig.MapSize / _generationConfig.BiomeSize;
            BiomeDataHolder[,] biomes = GenerateBiomesData(cellSize, _generationConfig.BiomeSize, _generationConfig.Biomes, _generationConfig.WaterOffset);

            for (int i = 0; i < _generationConfig.MapSize.x; i++) {
                for (int j = 0; j < _generationConfig.MapSize.y; j++) {
                    int gridX = i / cellSize.x;
                    int gridY = j / cellSize.y;
                    float nearestDistance = Mathf.Infinity;
                    Color color = default;
                    
                    for (int a = -1; a < 2; a++) {
                        for (int b = -1; b < 2; b++) {
                            int x = gridX + a;
                            int y = gridY + b;

                            if (x < 0 || y < 0 || x >= _generationConfig.BiomeSize || y >= _generationConfig.BiomeSize) 
                                continue;

                            float noise = Mathf.PerlinNoise(i * _generationConfig.NoiseScale, j * _generationConfig.NoiseScale) * _generationConfig.NoiseStrength;
                            Vector2 center = new Vector2(biomes[x, y].Center.x + noise, biomes[x, y].Center.y + noise);
                            float distance = Vector2.Distance(new Vector2(i, j), center);

                            if (distance < nearestDistance) {
                                nearestDistance = distance;
                                color = biomes[x, y].BiomeData.Color;
                            }
                        }
                    }
                    
                    texture.SetPixel(i, j, color);
                }
            }
            
            texture.Apply();
            return texture;
        }
        #endif

        private class BiomeDataHolder {
            public BiomeData BiomeData { get; }
            public Vector2Int Center { get; }
            public List<Vector3> TilePositions { get; }

            internal BiomeDataHolder(BiomeData biomeData, Vector2Int center) {
                BiomeData = biomeData;
                Center = center;
                TilePositions = new();
            }
        }
    }
}