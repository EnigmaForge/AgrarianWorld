using GameUpdater;
using SceneLoader;
using StateMachine;
using UnityEngine;
using Zenject;

namespace EntryPoint.Installers
{
    public class GameInstaller : MonoInstaller
    {
        [SerializeField] private Updater _updater;

        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo(typeof(Updater)).FromInstance(_updater).AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo(typeof(StateFactory)).AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo(typeof(AsyncSceneLoader)).AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo(typeof(GameStateMachine)).AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo(typeof(LoadGameSceneState)).AsSingle().NonLazy();
            Container.Bind<DayNightCycleFactoryBase>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo(typeof(ActiveGameSceneState)).AsSingle().NonLazy();
        }
    }
}