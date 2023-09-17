using SceneLoader;
using StateMachine;
using Zenject;

namespace EntryPoint
{
    public class GameInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo(typeof(StateFactory)).AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo(typeof(AsyncSceneLoader)).AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo(typeof(GameStateMachine)).AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo(typeof(LoadGameSceneState)).AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo(typeof(ActiveGameSceneState)).AsSingle().NonLazy();
        }
    }
}