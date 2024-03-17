using UnityEngine;
using Zenject;

namespace Modules.Core.Installers {
    [CreateAssetMenu(fileName = nameof(ProjectContextInstaller), menuName = "Installers/" + nameof(ProjectContextInstaller))]
    public class ProjectContextInstaller : ScriptableObjectInstaller<ProjectContextInstaller> {
        public override void InstallBindings() {
            
        }
    }
}