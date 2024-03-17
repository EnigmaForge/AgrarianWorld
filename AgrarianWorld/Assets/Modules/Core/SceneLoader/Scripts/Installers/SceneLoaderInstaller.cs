using Zenject;

namespace Modules.Core.SceneLoader.Installers {
    public class SceneLoaderInstaller : Installer<SceneLoaderInstaller> {
        public override void InstallBindings() {
            Container.BindInterfacesAndSelfTo<GameReloadService>()
                     .AsSingle()
                     .NonLazy();

            Container.BindInterfacesTo<AsyncSceneLoader>()
                     .AsSingle();
        }
    }
}