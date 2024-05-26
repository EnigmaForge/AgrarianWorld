using Modules.GameMenu;
using Modules.GenerationSystem.Installers;
using UnityEngine;
using Zenject;

namespace Modules.Core.Bootstraps.Installers {
    [CreateAssetMenu(fileName = nameof(GameContextScriptableObjectInstaller), menuName = "GameInstallers/Contexts/" + nameof(GameContextScriptableObjectInstaller))]
    public class GameContextScriptableObjectInstaller : ScriptableObjectInstaller {
        [SerializeField] private PauseMenuWindow _pauseMenuWindow;
        
        public override void InstallBindings() {
            GenerationSystemInstaller.Install(Container);
            
            Container.Bind<PauseMenuWindow>()
                     .FromComponentInNewPrefab(_pauseMenuWindow)
                     .AsSingle()
                     .NonLazy();
            
            Container.BindInterfacesTo<PauseMenuController>()
                     .AsSingle();
        }
    }
}