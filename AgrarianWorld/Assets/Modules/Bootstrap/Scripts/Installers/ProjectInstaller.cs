using GameUpdatersModule;
using SceneLoaderModule;
using StateMachine;
using TimerModule;
using UnityEngine;
using Zenject;

namespace Bootstrap.Installers {
    [CreateAssetMenu(fileName = nameof(ProjectInstaller), menuName = "Enigma Forge/Global Installers/" + nameof(ProjectInstaller))]
    public class ProjectInstaller : ScriptableObjectInstaller {
        public override void InstallBindings() {
            BindUpdaters();
            BindSceneLoader();
            BindStateMachines();
            BindTimerSystem();
        }

        private void BindUpdaters() {
            Container.Bind<IGameUpdater>()
                     .To<GameUpdater>()
                     .FromNewComponentOnNewGameObject()
                     .AsSingle()
                     .NonLazy();
            
            Container.Bind<ITimersUpdater>()
                     .To<TimersUpdater>()
                     .FromNewComponentOnNewGameObject()
                     .AsSingle()
                     .NonLazy();
        }

        private void BindSceneLoader() {
            Container.BindInterfacesAndSelfTo<SceneLoader>()
                     .AsSingle()
                     .NonLazy();
        }

        private void BindStateMachines() {
            Container.BindInterfacesAndSelfTo<GameStateMachine>()
                     .AsSingle()
                     .NonLazy();
        }

        private void BindTimerSystem() {
            Container.Bind<ITimersFactory>()
                     .To<TimersFactory>()
                     .AsSingle();

            Container.Bind<ITimersManager>()
                     .To<TimersManager>()
                     .AsSingle();
        }
    }
}