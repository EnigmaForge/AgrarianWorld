using Modules.GameMenu.Installer;
using UnityEngine;
using Zenject;

namespace Modules.Core.Bootstraps.Installers {
    [CreateAssetMenu(fileName = nameof(GameMenuContextScriptableObjectInstaller), menuName = "GameInstallers/Contexts/" + nameof(GameMenuContextScriptableObjectInstaller))]
    public class GameMenuContextScriptableObjectInstaller : ScriptableObjectInstaller {
        public override void InstallBindings() {
            GameMenuWindowsInstaller.Install(Container);
        }
    }
}