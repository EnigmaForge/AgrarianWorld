using Modules.GenerationSystem.Installers;
using UnityEngine;
using Zenject;

namespace Modules.Core.Bootstraps.Installers {
    [CreateAssetMenu(fileName = nameof(GameContextScriptableObjectInstaller), menuName = "GameInstallers/Contexts/" + nameof(GameContextScriptableObjectInstaller))]
    public class GameContextScriptableObjectInstaller : ScriptableObjectInstaller {
        public override void InstallBindings() {
            GenerationSystemInstaller.Install(Container);
        }
    }
}