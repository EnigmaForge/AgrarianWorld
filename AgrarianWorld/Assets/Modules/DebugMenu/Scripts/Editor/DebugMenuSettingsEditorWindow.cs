using UnityEditor;
using UnityEngine;

namespace Modules.DebugMenu.Editor {
    public class DebugMenuSettingsEditorWindow : EditorWindow {
        private const string DEFINE_SYMBOLS = "INCLUDE_DEBUG_MENU_IN_BUILD";
        private BuildTargetGroup _currentBuildTargetGroup;
        private bool _includeInBuild;

        [MenuItem("Debug Menu/Settings")]
        public static void ShowWindow() {
            var window = GetWindow<DebugMenuSettingsEditorWindow>("Debug Menu Settings");
            window.Show();
        }

        private void OnEnable() {
            _currentBuildTargetGroup = BuildPipeline.GetBuildTargetGroup(EditorUserBuildSettings.activeBuildTarget);
            _includeInBuild = PlayerSettings.GetScriptingDefineSymbolsForGroup(_currentBuildTargetGroup).Contains(DEFINE_SYMBOLS);
        }

        private void OnGUI() {
            _includeInBuild = EditorGUILayout.Toggle("Include In Build", _includeInBuild);
            EditorGUILayout.LabelField($"This option adds new define symbols ({DEFINE_SYMBOLS}) for current platform", EditorStyles.helpBox);

            if (GUI.changed)
                UpdateScriptingDefineSymbols();
        }

        private void UpdateScriptingDefineSymbols() {
            string defineSymbols = PlayerSettings.GetScriptingDefineSymbolsForGroup(_currentBuildTargetGroup);

            if (_includeInBuild) {
                if (defineSymbols.Contains(DEFINE_SYMBOLS))
                    return;
                
                if (string.IsNullOrEmpty(defineSymbols) is false)
                    defineSymbols += ";";

                defineSymbols += DEFINE_SYMBOLS;
            }
            else {
                defineSymbols = defineSymbols.Replace(DEFINE_SYMBOLS, "");
                defineSymbols = defineSymbols.Replace(";;", ";");
            }

            PlayerSettings.SetScriptingDefineSymbolsForGroup(_currentBuildTargetGroup, defineSymbols);
        }
    }
}