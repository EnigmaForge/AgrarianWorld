using System;
using UnityEngine;

namespace DayNightCycle
{
    [Serializable]
    public class DayNightLightSettings
    {
        [field: SerializeField] public Gradient AmbientColor { get; set; }
        [field: SerializeField] public Gradient DirectionalColor { get; set; }
        [field: SerializeField] public Gradient FogColor { get; set; }
    }
}