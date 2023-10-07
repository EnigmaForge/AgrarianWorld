using System;
using EntryPoint;
using GameUpdater;
using UnityEngine;
using Zenject;

namespace DayNightCycle
{
    public class DayNightCycleManager : MonoBehaviour
    {
        private const float MAX_HOURS_IN_DAY = 24f;

        private Light _sun;
        private DayNightCycleSettings _dayNightCycleSettings;
        private DayNightLightSettings _dayNightLightSettings;
        private IUpdater _updater;
        private float _timeOfDay;

        [Inject]
        public void Construct(IUpdater updater)
        {
            _updater = updater;
            DayNightCycleConfiguration configuration = Resources.Load<DayNightCycleConfiguration>(ResourcesPath.DayNightCycleConfiguration);
            _dayNightCycleSettings = configuration.DayNightCycleSettings;
            _dayNightLightSettings = configuration.LightSettings;
        }

        public void Start()
        {
            _sun = RenderSettings.sun;
            _dayNightCycleSettings.StartTime %= MAX_HOURS_IN_DAY;
            _timeOfDay = Mathf.Lerp(0f, _dayNightCycleSettings.Duration, _dayNightCycleSettings.StartTime / MAX_HOURS_IN_DAY);
            
            Subscription();
        }

        public void Destroy() => 
            UnSubscription();

        private void Subscription() => 
            _updater.OnUpdate += UpdateCycle;

        private void UnSubscription() => 
            _updater.OnUpdate -= UpdateCycle;

        private void UpdateCycle()
        {
            UpdateTimeOfDay();
            UpdateLight(_timeOfDay / _dayNightCycleSettings.Duration);
        }

        private void UpdateLight(float timePercent)
        {
            RenderSettings.ambientLight = _dayNightLightSettings.AmbientColor.Evaluate(timePercent);
            RenderSettings.fogColor = _dayNightLightSettings.FogColor.Evaluate(timePercent);
            RenderSettings.fogColor = _dayNightLightSettings.FogColor.Evaluate(timePercent);

            if (_sun == null)
                return;
            
            _sun.color = _dayNightLightSettings.DirectionalColor.Evaluate(timePercent);
            _sun.transform.localRotation = Quaternion.Euler(timePercent * 360f - 90f, 170f, 0f);
        }

        private void UpdateTimeOfDay()
        {
            _timeOfDay += Time.deltaTime;
            _timeOfDay %= _dayNightCycleSettings.Duration;
        }
    }
}
