using UnityEngine;
using Zenject;

namespace Bootstrap.Installers {
    [CreateAssetMenu(fileName = nameof(DataInstaller), menuName = "Enigma Forge/Global Installers/" + nameof(DataInstaller))]
    public class DataInstaller : ScriptableObjectInstaller {
        public override void InstallBindings() { }
    }
}