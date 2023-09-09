using EntryPoint.StateMachine;
using UnityEngine;

namespace EntryPoint
{
    public class GameBootstrapper : MonoBehaviour
    {
        private IStateMachine _gameStateMachine;

        private void Awake()
        {
            _gameStateMachine = new GameStateMachine();
            _gameStateMachine.Enter<BootstrapState>();

            DontDestroyOnLoad(this);
        }
    }
}