using System;
using System.Collections.Generic;

namespace TimerModule {
    public class TimerSystem : ITimerSystem {
        private readonly Dictionary<TimerGroup, TimersHolder> _timerGroups = new();
        private readonly ITimersFactory _timersFactory;

        public TimerSystem(ITimersFactory timersFactory) =>
            _timersFactory = timersFactory;

        public void CreateTimer(TimerConfiguration configuration) {
            if (GroupContainsTimer(configuration.TimerName, configuration.TimerGroup))
                throw new Exception($"{configuration.TimerName} timer already exist in {configuration.TimerGroup} group");
            
            ITimer timer = _timersFactory.Create(configuration);
            
            if (_timerGroups.TryGetValue(configuration.TimerGroup, out TimersHolder timerGroup)) {
                timerGroup.Timers.Add(configuration.TimerName, timer);
            }
            else {
                TimersHolder timersHolder = new TimersHolder();
                timersHolder.Timers.Add(configuration.TimerName, timer);
                _timerGroups.Add(configuration.TimerGroup, timersHolder);
            }
        }

        public void StartTimer(TimerName timerName, TimerGroup timerGroup) {
            if (GroupContainsTimer(timerName, timerGroup) is false)
                throw new ArgumentException($"{timerName} timer is not exist in {timerGroup} group");
            
            _timerGroups[timerGroup].Timers[timerName].Start();
        }

        public void PauseTimer(TimerName timerName, TimerGroup timerGroup) {
            if (GroupContainsTimer(timerName, timerGroup) is false)
                throw new ArgumentException($"{timerName} timer is not exist in {timerGroup} group");
            
            _timerGroups[timerGroup].Timers[timerName].Pause();
        }

        public void StopTimer(TimerName timerName, TimerGroup timerGroup) {
            if (GroupContainsTimer(timerName, timerGroup) is false)
                throw new ArgumentException($"{timerName} timer is not exist in {timerGroup} group");
            
            _timerGroups[timerGroup].Timers[timerName].Stop();
        }

        public void KillTimer(TimerName timerName, TimerGroup timerGroup) {
            if (GroupContainsTimer(timerName, timerGroup) is false)
                throw new ArgumentException($"{timerName} timer is not exist in {timerGroup} group");
            
            _timerGroups[timerGroup].Timers[timerName].Cleanup();
            _timerGroups[timerGroup].Timers.Remove(timerName);
        }

        public void StartTimerGroup(TimerGroup timerGroup) {
            if (GroupIsEmpty(timerGroup) is false)
                return;

            foreach (ITimer timer in _timerGroups[timerGroup].Timers.Values)
                timer.Start();
        }

        public void PauseTimerGroup(TimerGroup timerGroup) {
            if (GroupIsEmpty(timerGroup) is false)
                return;
            
            foreach (ITimer timer in _timerGroups[timerGroup].Timers.Values)
                timer.Pause();
        }

        public void StopTimerGroup(TimerGroup timerGroup) {
            if (GroupIsEmpty(timerGroup) is false)
                return;
            
            foreach (ITimer timer in _timerGroups[timerGroup].Timers.Values)
                timer.Stop();
        }

        public void KillTimerGroup(TimerGroup timerGroup) {
            if (GroupIsEmpty(timerGroup) is false)
                return;

            foreach (ITimer timer in _timerGroups[timerGroup].Timers.Values)
                timer.Cleanup();
            
            _timerGroups[timerGroup].Timers.Clear();
        }

        private bool GroupIsEmpty(TimerGroup timerGroup) =>
            _timerGroups.ContainsKey(timerGroup) is false || _timerGroups[timerGroup].IsEmpty();

        private bool GroupContainsTimer(TimerName timerName, TimerGroup timerGroup) =>
            GroupIsEmpty(timerGroup) is false && _timerGroups[timerGroup].Timers.ContainsKey(timerName);
    }
}