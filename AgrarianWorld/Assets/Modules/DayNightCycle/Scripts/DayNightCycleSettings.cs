using System;
using UnityEngine;

namespace DayNightCycle
{
    [Serializable]
    public class DayNightCycleSettings
    {
        [field: SerializeField, Min(1)] public float Duration;
        [field: SerializeField, Range(0, 24)] public float StartTime;
    }
}