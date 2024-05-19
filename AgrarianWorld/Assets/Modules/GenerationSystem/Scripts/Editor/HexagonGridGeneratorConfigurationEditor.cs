using UnityEditor;
using UnityEngine;

namespace Modules.GenerationSystem.Editor {
    [CustomEditor(typeof(HexagonGridGeneratorConfiguration))]
    public class HexagonGridGeneratorConfigurationEditor : UnityEditor.Editor {
        private Texture2D _generatedTexture;
        private int _seed;

        public override void OnInspectorGUI() {
            HexagonGridGeneratorConfiguration config = (HexagonGridGeneratorConfiguration)target;
            base.OnInspectorGUI();
            
            GUILayout.Label("Debug", EditorStyles.boldLabel);

            _seed = EditorGUILayout.IntSlider("Seed", _seed, 0, 999999);
            
            if (GUILayout.Button("Generate Map")) {
                HexagonWorldGenerator generator = new HexagonWorldGenerator(config);
                _generatedTexture = generator.GenerateVoronoiDiagram(_seed);
            }
            
            if (_generatedTexture != null)
                GUILayout.Label(_generatedTexture, GUILayout.Width(128), GUILayout.Height(128));
        }
    }
}