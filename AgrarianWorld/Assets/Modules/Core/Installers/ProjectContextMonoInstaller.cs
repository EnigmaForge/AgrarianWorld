using GameUpdater;
using UnityEngine;
using Zenject;

namespace Core.Installers {
    public class ProjectContextMonoInstaller : MonoInstaller {
        [SerializeField] private Updater _gameUpdater;
        
        public override void InstallBindings() {
            Container.Bind<IUpdater>()
                .To<Updater>()
                .FromInstance(_gameUpdater)
                .NonLazy();
        }
    }
}