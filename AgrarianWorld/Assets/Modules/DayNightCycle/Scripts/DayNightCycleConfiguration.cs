using UnityEngine;

namespace DayNightCycle
{
    [CreateAssetMenu(fileName = "LightPresetDefault", menuName = "EnigmaForge/Day Night Cycle/Create Light Preset")]
    public class DayNightCycleConfiguration : ScriptableObject
    {
        public DayNightCycleSettings DayNightCycleSettings;
        public DayNightLightSettings LightSettings;
    }
}