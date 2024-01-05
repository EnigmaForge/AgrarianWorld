using UnityEngine;
using Zenject;

namespace Core.Installers {
    [CreateAssetMenu(fileName = "ConfigsInstaller", menuName = "Enigma Forge/Installers/ConfigsInstaller")]
    public class ConfigsInstaller : ScriptableObjectInstaller<ConfigsInstaller> {
        public override void InstallBindings() {
            
        }
    }
}