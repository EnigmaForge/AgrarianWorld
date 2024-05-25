using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Modules.GenerationSystem {
    public class HexagonWorldGenerator : IHexagonWorldGenerator {
        private readonly HexagonGridGeneratorConfiguration _generationConfig;
        private readonly VillageGenerationModel _villageGenerationModel;
        private readonly VillageGenerationConfig _villageGenerationConfig;
        private readonly LSystemSentenceGenerator _lSystemSentenceGenerator;
        private BiomeDataHolder[,] _biomes;
        private Grid _hexagonGrid;

        public HexagonWorldGenerator(HexagonGridGeneratorConfiguration generationConfig, LSystemSentenceGenerator lSystemSentenceGenerator, VillageGenerationModel villageGenerationModel, VillageGenerationConfig villageGenerationConfig) {
            _generationConfig = generationConfig;
            _lSystemSentenceGenerator = lSystemSentenceGenerator;
            _villageGenerationConfig = villageGenerationConfig;
            _villageGenerationModel = villageGenerationModel;
        }

        public void Generate(int seed) {
            Random.InitState(seed);

            Vector2Int mapSize = _generationConfig.MapSize;
            int biomeSize = _generationConfig.BiomeSize;
            List<BiomeData> tiles = _generationConfig.Biomes;
            int waterOffset = _generationConfig.WaterOffset;
            GameObject hexagonGridInstance = Object.Instantiate(_generationConfig.HexagonGridPrefab);
            if (hexagonGridInstance.TryGetComponent(out Grid hexagonGrid)) {
                _hexagonGrid = hexagonGrid;
                GenerateTiles(hexagonGrid, mapSize, biomeSize, tiles, waterOffset);
                
                _lSystemSentenceGenerator.Generate(seed);
                Vector2Int structureSize = GenerateLSystem(_villageGenerationModel.Sentence, _villageGenerationConfig.Angle, _villageGenerationConfig.Length);
                if(TryGetStructureCenterInBiome(BiomeType.Grass, structureSize, out Vector3 villageCenter)) {
                    _villageGenerationModel.VillageCenter = villageCenter;
                    for (int roadIndex = 0; roadIndex < _villageGenerationModel.RoadPositions.Count; roadIndex++)
                        _villageGenerationModel.RoadPositions[roadIndex] += villageCenter;

                    GenerateRoads();
                    GenerateHouses();
                }
                
                GenerateDecorations();
            }
        }

        private Vector2Int GenerateLSystem(string lSystemSentence, float angle, int length) {
            Vector2Int areaSize = new Vector2Int();
            
            Stack<LSystemAgentParameters> savePoints = new Stack<LSystemAgentParameters>(); 
            Vector3 currentPosition = Vector3.zero;
            Vector3 direction = Vector3.forward;
            float minX = currentPosition.x, maxX = currentPosition.x;
            float minZ = currentPosition.z, maxZ = currentPosition.z;

            foreach (char letter in lSystemSentence) {
                LSystemCommand lSystemCommand = (LSystemCommand)letter;

                switch (lSystemCommand) {
                    case LSystemCommand.Save:
                        savePoints.Push(new LSystemAgentParameters {
                            Position = currentPosition, 
                            Direction = direction, 
                            Length = length
                        });
                        break;
                    case LSystemCommand.Load:
                        LSystemAgentParameters agentParameter = savePoints.Pop();
                        currentPosition = agentParameter.Position;
                        direction = agentParameter.Direction;
                        length = agentParameter.Length;
                        break;
                    case LSystemCommand.Draw:
                        WriteRoadPositions(currentPosition, Vector3Int.RoundToInt(direction), length);
                        currentPosition += direction * length;
                        minX = Mathf.Min(minX, currentPosition.x);
                        maxX = Mathf.Max(maxX, currentPosition.x);
                        minZ = Mathf.Min(minZ, currentPosition.z);
                        maxZ = Mathf.Max(maxZ, currentPosition.z);
                        length -= 1;
                        break;
                    case LSystemCommand.TurnRight:
                        direction = Quaternion.AngleAxis(angle, Vector3.up) * direction;
                        break;
                    case LSystemCommand.TurnLeft:
                        direction = Quaternion.AngleAxis(-angle, Vector3.up) * direction;
                        break;
                }
            }
            
            areaSize.x = Mathf.RoundToInt(maxX - minX);
            areaSize.y = Mathf.RoundToInt(maxZ - minZ);
            return areaSize;
        }

        private void WriteRoadPositions(Vector3 startPosition, Vector3Int direction, int length) {
            for (int i = 0; i < length; i++) {
                Vector3Int roadPosition = Vector3Int.RoundToInt(startPosition + direction * i);
                roadPosition.Set(roadPosition.x, roadPosition.z, roadPosition.y);
                Vector3 roadPositionInWorld = _hexagonGrid.CellToWorld(roadPosition);
                if(_villageGenerationModel.RoadPositions.Contains(roadPositionInWorld))
                    continue;
                
                _villageGenerationModel.RoadPositions.Add(roadPositionInWorld);
            }
        }

        private void GenerateRoads() {
            foreach (Vector3 position in _villageGenerationModel.RoadPositions)
                Object.Instantiate(_villageGenerationConfig.RoadStraight, position, Quaternion.identity);
        }

        private void GenerateHouses() {
            foreach (Vector3 position in _villageGenerationModel.RoadPositions)
                GenerateHousesInNeighbors(position);
        }

        private void GenerateHousesInNeighbors(Vector3 position) {
            Vector3 currentPosition = _hexagonGrid.WorldToCell(position);
            
            List<Vector3> notAvailablePositions = new List<Vector3>();
            notAvailablePositions.AddRange(_villageGenerationModel.HousePositions);
            notAvailablePositions.AddRange(_villageGenerationModel.RoadPositions);
            foreach (BiomeDataHolder biomeDataHolder in _biomes) {
                if(biomeDataHolder.BiomeData.BiomeType == BiomeType.Water)
                    notAvailablePositions.AddRange(biomeDataHolder.TilePositions);
            }
            List<Vector3Int> notAvailableGridPositions = new List<Vector3Int>();
            foreach (Vector3 worldPosition in notAvailablePositions)
                notAvailableGridPositions.Add(_hexagonGrid.WorldToCell(worldPosition));
            
            foreach (Vector3 direction in _villageGenerationConfig.NeighborsDirections) {
                Vector3 normalizedDirection = direction.normalized;
                Vector3Int neighborPosition = Vector3Int.RoundToInt(currentPosition + normalizedDirection);
                if (notAvailableGridPositions.Contains(neighborPosition) is false) {
                    Vector3 housePosition = _hexagonGrid.CellToWorld(neighborPosition);
                    _villageGenerationModel.HousePositions.Add(housePosition);
                    GameObject housePrefab = null;
                    float randomValue = Random.value;
                    foreach (HouseSpawnData houseSpawnData in _villageGenerationConfig.Houses) {
                        if (randomValue <= houseSpawnData.SpawnChance) {
                            housePrefab = houseSpawnData.HousePrefab;
                            break;
                        }
                    }
                    
                    float angle = Mathf.Atan2(normalizedDirection.y, -normalizedDirection.x) * Mathf.Rad2Deg;
                    Quaternion rotation = Quaternion.Euler(0, angle, 0); 
                    Object.Instantiate(housePrefab, housePosition, rotation);
                }
            }
        }

        private bool TryGetStructureCenterInBiome(BiomeType biomeType, Vector2Int structureSize, out Vector3 structureCenter) {
            List<Vector3> allPositions = new List<Vector3>();
            foreach (BiomeDataHolder biomeDataHolder in _biomes)
                if (biomeDataHolder.BiomeData.BiomeType == biomeType)
                    allPositions.AddRange(biomeDataHolder.TilePositions);

            if (FindStructureCenter(allPositions, structureSize, out Vector3 center)) {
                structureCenter = center;
                return true;
            }

            structureCenter = default;
            return false;
        }

        private void GenerateTiles(Grid hexagonGrid, Vector2Int mapSize, int biomeSize, List<BiomeData> tiles, int waterOffset) {
            Vector2Int cellSize = mapSize / biomeSize;
            _biomes = GenerateBiomesData(cellSize, biomeSize, tiles, waterOffset);

            for (var i = 0; i < mapSize.x; i++) {
                for (var j = 0; j < mapSize.y; j++) {
                    int gridX = i / cellSize.x;
                    int gridY = j / cellSize.y;
                    float nearestDistance = Mathf.Infinity;
                    BiomeData biomeData = default;
                    var biomeCoordinates = new Vector2Int();

                    for (int a = -1; a < 2; a++) {
                        for (int b = -1; b < 2; b++) {
                            int x = gridX + a;
                            int y = gridY + b;

                            if (x < 0 || y < 0 || x >= _generationConfig.BiomeSize || y >= _generationConfig.BiomeSize)
                                continue;

                            float noise = Mathf.PerlinNoise(i * _generationConfig.NoiseScale, j * _generationConfig.NoiseScale) *
                                          _generationConfig.NoiseStrength;

                            var center = new Vector2(_biomes[x, y].Center.x + noise, _biomes[x, y].Center.y + noise);
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
                    _biomes[biomeCoordinates.x, biomeCoordinates.y]
                        .TilePositions.Add(position);
                }
            }
        }

        private BiomeDataHolder[,] GenerateBiomesData(Vector2Int cellSize, int biomeSize, List<BiomeData> biomes, int waterOffset) {
            var biomeHolders = new BiomeDataHolder[biomeSize, biomeSize];
            BiomeData waterBiome = biomes.Find(tile => tile.BiomeType == BiomeType.Water);

            for (var i = 0; i < biomeSize; i++) {
                for (var j = 0; j < biomeSize; j++) {
                    var biomeCenter = new Vector2Int(i * cellSize.x + Random.Range(0, cellSize.x), j * cellSize.y + Random.Range(0, cellSize.y));

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
        
        private bool FindStructureCenter(List<Vector3> positions, Vector2Int squareSize, out Vector3 structureCenter) {
            structureCenter = default;

            List<Vector3Int> intPositions = new List<Vector3Int>();
            foreach (Vector3 position in positions)
                intPositions.Add(new Vector3Int((int)position.x, (int)position.y, (int)position.z));

            for (var i = 0; i < intPositions.Count; i++) {
                Vector3Int position = intPositions[i];
                Vector3Int leftBottomCorner = position;
                Vector3Int leftTopCorner = new Vector3Int(position.x, position.y, position.z + squareSize.y);
                Vector3Int rightBottomCorner = new Vector3Int(position.x + squareSize.x, position.y, position.z);
                Vector3Int rightTopCorner = new Vector3Int(position.x + squareSize.x, position.y, position.z + squareSize.y);

                if (intPositions.Contains(leftBottomCorner) && 
                    intPositions.Contains(leftTopCorner) && 
                    intPositions.Contains(rightBottomCorner) && 
                    intPositions.Contains(rightTopCorner)) {
            
                    if (AllPositionsWithinSquareArePresent(intPositions, leftBottomCorner, squareSize)) {
                        structureCenter = new Vector3(positions[i].x + squareSize.x / 2f, positions[i].y, positions[i].z + squareSize.y / 2f);
                        Vector3Int gridPosition = _hexagonGrid.WorldToCell(structureCenter);
                        structureCenter = _hexagonGrid.CellToWorld(gridPosition);
                        return true;
                    }
                }
            }

            return false;
        }

        private bool AllPositionsWithinSquareArePresent(List<Vector3Int> positions, Vector3Int startPosition, Vector2Int squareSize) {
            for (int x = 0; x <= squareSize.x; x++) {
                for (int z = 0; z <= squareSize.y; z++) {
                    Vector3Int checkPosition = new Vector3Int(startPosition.x + x, startPosition.y, startPosition.z + z);
                    if (!positions.Contains(checkPosition)) {
                        return false;
                    }
                }
            }
            return true;
        }


        private void GenerateDecorations() {
            List<Vector3> structureElementPositions = new List<Vector3>();
            structureElementPositions.AddRange(_villageGenerationModel.HousePositions);
            structureElementPositions.AddRange(_villageGenerationModel.RoadPositions);
            List<Vector3Int> structureElementCellPositions = new List<Vector3Int>();
            foreach (Vector3 worldPosition in structureElementPositions)
                structureElementCellPositions.Add(_hexagonGrid.WorldToCell(worldPosition));
            
            foreach (BiomeDataHolder biomeDataHolder in _biomes) {
                DecorationsData decorationsData = biomeDataHolder.BiomeData.Decorations;

                if (decorationsData.DecorationPrefabs is { Count: 0 })
                    continue;

                float noiseScale = biomeDataHolder.BiomeData.Decorations.NoiseScale;
                float factor = 1f / (decorationsData.DecorationPrefabs.Count + 1);

                foreach (Vector3 position in biomeDataHolder.TilePositions) {
                    if (structureElementCellPositions.Contains(_hexagonGrid.WorldToCell(position)))
                        continue;
                    
                    float xCoordinate = position.x * noiseScale;
                    float yCoordinate = position.z * noiseScale;
                    float perlinValue = Mathf.PerlinNoise(xCoordinate, yCoordinate);

                    GameObject prefabToSpawn = null;
                    for (var i = 0; i < decorationsData.DecorationPrefabs.Count; i++) {
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

        private class BiomeDataHolder {
            public BiomeData BiomeData { get; }
            public Vector2Int Center { get; }
            public List<Vector3> TilePositions { get; }

            internal BiomeDataHolder(BiomeData biomeData, Vector2Int center) {
                BiomeData = biomeData;
                Center = center;
                TilePositions = new List<Vector3>();
            }
        }
    }
}