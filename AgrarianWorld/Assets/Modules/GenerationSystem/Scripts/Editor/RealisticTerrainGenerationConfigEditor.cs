using UnityEditor;
using UnityEngine;

namespace Modules.GenerationSystem.Editor {
    [CustomEditor(typeof(RealisticTerrainGenerationConfig))]
    public class RealisticTerrainGenerationConfigEditor : UnityEditor.Editor {
        private const int RESOLUTION = 128;
        private int _seed;
        private Texture2D _perlinTexture;

        public override void OnInspectorGUI() {
            base.OnInspectorGUI();

            RealisticTerrainGenerationConfig config = (RealisticTerrainGenerationConfig)target;
            EditorGUILayout.Space();
            
            GeneratePerlinTexture(config);
            GUILayout.Label("Debug", EditorStyles.boldLabel);
            _seed = EditorGUILayout.IntSlider("Seed", _seed, 0, 999999);
            GUILayout.Label(_perlinTexture, GUILayout.Width(RESOLUTION), GUILayout.Height(RESOLUTION));
        }
        private void GeneratePerlinTexture(RealisticTerrainGenerationConfig config) {
            Random.InitState(0);

            RealisticTerrainGenerator terrainGenerator = new RealisticTerrainGenerator(config, null);
            float[,] heights = terrainGenerator.GenerateHeightsByNoise(RESOLUTION, _seed);
            _perlinTexture = CreateTexture(heights, RESOLUTION, RESOLUTION);
        }

        private Texture2D CreateTexture(float[,] heights, int width, int height) {
            Texture2D texture = new Texture2D(width, height);

            for (int x = 0; x < width; x++) {
                for (int y = 0; y < height; y++) {
                    float heightValue = heights[x, y];
                    var color = new Color(heightValue, heightValue, heightValue);
                    texture.SetPixel(x, y, color);
                }
            }

            texture.Apply();
            return texture;
        }
    }
}