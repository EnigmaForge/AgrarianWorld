using Modules.GameMenu;
using Modules.GenerationSystem;
using UnityEngine;
using Zenject;

namespace Modules.Core.Installers {
    [CreateAssetMenu(fileName = nameof(ConfigurationsInstaller), menuName = "GameInstallers/" + nameof(ConfigurationsInstaller))]
    public class ConfigurationsInstaller : ScriptableObjectInstaller {
        [SerializeField] private WorldsListConfiguration _worldsListConfiguration;
        [SerializeField] private RealisticTerrainGenerationConfig _realisticTerrainGenerationConfig;
        
        public override void InstallBindings() {
            Container.Bind<WorldsListConfiguration>()
                     .FromInstance(_worldsListConfiguration)
                     .AsSingle();
            
            Container.Bind<RealisticTerrainGenerationConfig>()
                     .FromInstance(_realisticTerrainGenerationConfig)
                     .AsSingle();
        }
    }
}