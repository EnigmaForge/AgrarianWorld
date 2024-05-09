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
            GenerateWorld();
        }

        private void InitializeContext() {
            _sceneContextInstance = Instantiate(_sceneContext);
            _sceneContextInstance.Run();
        }

        private void GenerateWorld() {
            WorldsListModel worldsListModel = _sceneContextInstance.Container.Resolve<WorldsListModel>();
            WorldData worldData = worldsListModel.SelectedWorld;

            if (worldData is { WorldType: WorldType.Realistic }) {
                ITerrainGenerator terrainGenerator = _sceneContextInstance.Container.Resolve<ITerrainGenerator>();
                terrainGenerator.Generate(worldData.Seed);
            }
        }
    }
}