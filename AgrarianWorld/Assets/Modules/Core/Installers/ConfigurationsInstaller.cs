using Modules.GameMenu;
using UnityEngine;
using Zenject;

namespace Modules.Core.Installers {
    [CreateAssetMenu(fileName = nameof(ConfigurationsInstaller), menuName = "GameInstallers/" + nameof(ConfigurationsInstaller))]
    public class ConfigurationsInstaller : ScriptableObjectInstaller {
        [SerializeField] private WorldsListConfiguration _worldsListConfiguration;
        
        public override void InstallBindings() {
            Container.Bind<WorldsListConfiguration>()
                     .FromInstance(_worldsListConfiguration)
                     .AsSingle();
        }
    }
}