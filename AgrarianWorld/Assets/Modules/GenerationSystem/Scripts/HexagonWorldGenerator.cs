using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Modules.GenerationSystem {
    public class HexagonWorldGenerator : IHexagonWorldGenerator {
        private readonly HexagonGridGeneratorConfiguration _generationConfig;
        
        public HexagonWorldGenerator(HexagonGridGeneratorConfiguration generationConfig) =>
            _generationConfig = generationConfig;
        
        public void Generate(int seed) {
            Random.InitState(seed);

            Vector2Int mapSize = _generationConfig.MapSize;
            int biomeSize = _generationConfig.BiomeSize;
            List<BiomeData> tiles = _generationConfig.Biomes;
            int waterOffset = _generationConfig.WaterOffset;
            GameObject hexagonGridInstance = Object.Instantiate(_generationConfig.HexagonGridPrefab);
            if (hexagonGridInstance.TryGetComponent(out Grid hexagonGrid))
                GenerateTiles(hexagonGrid, mapSize, biomeSize, tiles, waterOffset);
        }

        private void GenerateTiles(Grid hexagonGrid, Vector2Int mapSize, int biomeSize, List<BiomeData> tiles, int waterOffset) {
            Vector2Int cellSize = mapSize / biomeSize;
            BiomeDataHolder[,] biomes = GenerateBiomesData(cellSize, biomeSize, tiles, waterOffset);

            for (int i = 0; i < mapSize.x; i++) {
                for (int j = 0; j < mapSize.y; j++) {
                    int gridX = i / cellSize.x;
                    int gridY = j / cellSize.y;
                    float nearestDistance = Mathf.Infinity;
                    BiomeData biomeData = default;

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
                                biomeData = biomes[x, y].BiomeData;
                            }
                        }
                    }

                    Vector3 position = hexagonGrid.CellToWorld(new Vector3Int(i, j, 0));
                    Object.Instantiate(biomeData!.TilePrefab, position, Quaternion.identity);
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

            internal BiomeDataHolder(BiomeData biomeData, Vector2Int center) {
                BiomeData = biomeData;
                Center = center;
            }
        }
    }
}