using System.Linq;
using Modules.GameMenu;
using Modules.GenerationSystem;
using UnityEngine;
using Zenject;

namespace Modules.Core.Bootstraps {
    public class GameSceneBootstrap : BootstrapBehaviour {
        [SerializeField] private SceneContext _sceneContext;
        private SceneContext _sceneContextInstance;

        private void Start() {
            InitializeContext();
            InitializeWindows();
            GenerateWorld();
        }

        private void InitializeContext() {
            _sceneContextInstance = Instantiate(_sceneContext);
            _sceneContextInstance.Run();
        }

        private void GenerateWorld() {
            WorldsListModel worldsListModel = _sceneContextInstance.Container.Resolve<WorldsListModel>();
            WorldData worldData = worldsListModel.SelectedWorld;

            if (worldData is { WorldType: WorldType.Flat }) {
                IHexagonWorldGenerator hexagonWorldGenerator = _sceneContextInstance.Container.Resolve<HexagonWorldGenerator>();
                hexagonWorldGenerator.Generate(worldData.Seed);
            }
            else if (worldData is { WorldType: WorldType.Terrain }) {
                ITerrainGenerator terrainGenerator = _sceneContextInstance.Container.Resolve<RealisticTerrainGenerator>();
                terrainGenerator.Generate(worldData.Seed);

                RealisticWorldObjectsGeneratorConfig generationObjectsConfig = _sceneContextInstance.Container.Resolve<RealisticWorldObjectsGeneratorConfig>();
                TerrainSurfacePointsModel terrainSurfacePointsModel = _sceneContextInstance.Container.Resolve<TerrainSurfacePointsModel>();
                IObjectsOnSurfaceGenerator objectsGenerator = _sceneContextInstance.Container.Resolve<ObjectsOnSurfaceGenerator>();
                objectsGenerator.SetGenerationPoints(terrainSurfacePointsModel.Points)
                                .SetGenerationRange(generationObjectsConfig.MinHeight, generationObjectsConfig.MaxHeight, generationObjectsConfig.Range, terrainSurfacePointsModel.SurfaceCenter)
                                .SetObjects(generationObjectsConfig.GenerationObjects.ToHashSet())
                                .Generate(worldData.Seed);
            }
        }
        
        private void InitializeWindows() {
            PauseMenuWindow pauseMenuWindow = _sceneContextInstance.Container.Resolve<PauseMenuWindow>();
            pauseMenuWindow.SetActive(false);
        }
    }
}