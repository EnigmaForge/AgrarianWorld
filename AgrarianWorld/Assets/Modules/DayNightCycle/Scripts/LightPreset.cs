using UnityEngine;

namespace Assets.Modules.DayNightCycle
{
    [CreateAssetMenu(fileName = "LightPresetDefault", menuName = "EnigmaForge/Day Night Cycle/Create Light Preset")]
    public class LightPreset : ScriptableObject
    {
        public LightPresetSettings Settings;
    }
}