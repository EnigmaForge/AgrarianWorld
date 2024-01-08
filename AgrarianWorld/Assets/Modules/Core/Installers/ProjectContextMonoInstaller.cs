using GameUpdater;
using SceneLoaderModule;
using StateMachine;
using UnityEngine;
using Zenject;

namespace Core.Installers {
    public class ProjectContextMonoInstaller : MonoInstaller {
        [SerializeField] private Updater _gameUpdater;
        
        public override void InstallBindings() {
            Container.Bind<IUpdater>()
                .To<Updater>()
                .FromInstance(_gameUpdater)
                .AsSingle()
                .NonLazy();

            Container.BindInterfacesAndSelfTo<SceneLoader>()
                .AsSingle()
                .NonLazy();

            Container.BindInterfacesAndSelfTo<GameStateMachine>()
                .AsSingle()
                .NonLazy();
        }
    }
}