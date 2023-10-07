using StateMachine;
using UnityEngine;
using Zenject;

namespace EntryPoint
{
    public class GameStartup : MonoBehaviour
    {
        private IStateMachine _stateMachine;

        [Inject]
        public void Construct(IStateMachine stateMachine)
        {
            _stateMachine = stateMachine;
        }

        private void Start()
        {
            _stateMachine.ChangeState<LoadGameSceneState>();
        }
    }
}