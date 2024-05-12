using Zenject;

namespace Modules.GenerationSystem.Installers {
    public class GenerationSystemInstaller : Installer<GenerationSystemInstaller> {
        public override void InstallBindings() {
            Container.BindInterfacesAndSelfTo<RealisticTerrainGenerator>()
                     .AsSingle();
            
            Container.BindInterfacesAndSelfTo<ObjectsOnSurfaceGenerator>()
                     .AsSingle();
        }
    }
}