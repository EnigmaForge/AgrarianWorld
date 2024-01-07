#if UNITY_EDITOR
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace SceneLoaderModule {
    internal class SceneNamesEnumGenerator {
        private const string ENUM_DIRECTORY_PATH = "Assets/Modules/SceneLoaderModule";
        private const string ENUM_NAME = "SceneNames";
        
        [MenuItem("Generate/Generate Scene Names Enum")]
        private static void GenerateSceneNamesEnum() {
            string[] scenePaths = EditorBuildSettings.scenes.Select(scene => scene.path).ToArray();

            string enumNamespace = Path.GetFileName(ENUM_DIRECTORY_PATH);
            string enumScriptContent = "namespace " + enumNamespace + " { \n    public enum " + ENUM_NAME + " {\n";

            for (int i = 0; i < scenePaths.Length; i++) {
                string sceneName = Path.GetFileNameWithoutExtension(scenePaths[i]);
                enumScriptContent += $"        {sceneName} = {i},\n";
            }

            enumScriptContent += "    }\n}";

            SaveSceneNamesEnum(enumScriptContent);
        }

        private static void SaveSceneNamesEnum(string enumScriptContent) {
            string enumFilePath = Path.Combine(ENUM_DIRECTORY_PATH, ENUM_NAME) + ".cs";
            File.WriteAllText(enumFilePath, enumScriptContent);
            Debug.Log("Scene Names enum generated at: " + enumFilePath);
        }
    }
}
#endif