using UnityEditor;
using UnityEngine;

namespace Modules.Core.SceneLoader.Editor {
    public class SceneNamesGenerator {
        private const string NAMESPACE = "namespace Modules.Core.SceneLoader";
        private const string CLASS_NAME = "SceneNames";
        
        [MenuItem("Generators/Generate Scene Names")]
        public static void GenerateSceneNames() {
            EditorBuildSettingsScene[] scenes = EditorBuildSettings.scenes;

            string content = "// generated file\n\n" + NAMESPACE + " {\n\tpublic enum " + CLASS_NAME + " {\n";

            for (int i = 0; i < scenes.Length; i++) {
                string sceneName = System.IO.Path.GetFileNameWithoutExtension(scenes[i].path);
                content += $"\t\t{sceneName} = {i},\n";
            }

            content += "\t}\n}";
            
            System.IO.File.WriteAllText(Application.dataPath + "/Modules/Core/SceneLoader/Scripts/" + CLASS_NAME + ".cs", content);
            AssetDatabase.Refresh();
            Debug.Log("SceneNames enum generated successfully.");
        }
    }
}