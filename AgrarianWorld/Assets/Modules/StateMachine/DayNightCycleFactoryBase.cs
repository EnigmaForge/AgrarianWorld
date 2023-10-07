using EntryPoint;
using UnityEngine;
using Zenject;

namespace StateMachine
{
    public class DayNightCycleFactoryBase
    {
        private readonly DiContainer _diContainer;

        protected DayNightCycleFactoryBase(DiContainer diContainer) => 
            _diContainer = diContainer;

        public void Create()
        {
            GameObject dayNightCyclePrefab = Resources.Load<GameObject>(ResourcesPath.DayNightCyclePrefab);
            _diContainer.InstantiatePrefab(dayNightCyclePrefab);
        }
    }
}