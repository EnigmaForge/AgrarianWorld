using System;
using UnityEngine;

namespace Assets.Modules.DayNightCycle
{
    [Serializable]
    public class LightPresetSettings
    {
        [field: SerializeField] public Gradient AmbientColor { get; set; }
        [field: SerializeField] public Gradient DirectionalColor { get; set; }
        [field: SerializeField] public Gradient FogColor { get; set; }
    }
}