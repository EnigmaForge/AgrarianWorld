using UnityEditor;
using UnityEngine;

namespace Modules.SavingSystem.Editor {
    public class SavingSystemHelper {
        [MenuItem("SavingSystemHelper/Open Saves Folder")]
        public static void OpenSavesFolder() {
            string pathToOpen = Application.persistentDataPath + "/Saves/";
            System.Diagnostics.Process.Start(pathToOpen);
        } 
    }
}
