using GameUpdatersModule;
using SceneLoaderModule;
using StateMachine;
using UnityEngine;
using Zenject;

namespace Bootstrap.Installers {
    [CreateAssetMenu(fileName = nameof(ProjectInstaller), menuName = "Enigma Forge/Global Installers/" + nameof(ProjectInstaller))]
    public class ProjectInstaller : ScriptableObjectInstaller {
        public override void InstallBindings() {
            Container.Bind<IGameUpdater>()
                     .To<GameUpdater>()
                     .FromNewComponentOnNewGameObject()
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