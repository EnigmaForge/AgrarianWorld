using Modules.GameMenu;
using Modules.GenerationSystem;
using UnityEngine;
using Zenject;

namespace Modules.Core.Installers {
    [CreateAssetMenu(fileName = nameof(ConfigurationsInstaller), menuName = "GameInstallers/" + nameof(ConfigurationsInstaller))]
    public class ConfigurationsInstaller : ScriptableObjectInstaller {
        [SerializeField] private WorldsListConfiguration _worldsListConfiguration;
        [SerializeField] private RealisticTerrainGenerationConfig _realisticTerrainGenerationConfig;
        [SerializeField] private RealisticWorldObjectsGeneratorConfig _realisticWorldObjectsGeneratorConfig;
        [SerializeField] private HexagonGridGeneratorConfiguration _hexagonGridGeneratorConfiguration;
        [SerializeField] private VillageGenerationConfig _villageGenerationConfig;
        
        public override void InstallBindings() {
            Container.Bind<WorldsListConfiguration>()
                     .FromInstance(_worldsListConfiguration)
                     .AsSingle();
            
            Container.Bind<RealisticTerrainGenerationConfig>()
                     .FromInstance(_realisticTerrainGenerationConfig)
                     .AsSingle();
            
            Container.Bind<RealisticWorldObjectsGeneratorConfig>()
                     .FromInstance(_realisticWorldObjectsGeneratorConfig)
                     .AsSingle();
            
            Container.Bind<HexagonGridGeneratorConfiguration>()
                     .FromInstance(_hexagonGridGeneratorConfiguration)
                     .AsSingle();
            
            Container.Bind<VillageGenerationConfig>()
                     .FromInstance(_villageGenerationConfig)
                     .AsSingle();
        }
    }
}