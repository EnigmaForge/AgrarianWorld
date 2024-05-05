using System;
using Modules.ViewsModule;
using UnityEngine;
using UnityEngine.UI;

namespace Modules.GameMenu {
    public class OpenWorldView : ViewBehaviour {
        [SerializeField] private Button _closeWindowButton;
        [SerializeField] private Button _openWorldButton;
        [SerializeField] private Button _showAddWorldWindowButton;
        [SerializeField] private Button _removeWorldButton;

        public event Action OnClickCloseWindowButton;
        public event Action OnClickOpenWorldButton;
        public event Action OnClickShowAddWorldWindowButton;
        public event Action OnClickRemoveWorldButton;

        private void OnEnable() {
            _closeWindowButton.onClick.AddListener(OnClickCloseWindowInvoke);
            _openWorldButton.onClick.AddListener(OnClickOpenWorldButtonInvoke);
            _showAddWorldWindowButton.onClick.AddListener(OnClickShowAddWorldWindowButtonInvoke);
            _removeWorldButton.onClick.AddListener(OnClickRemoveWorldButtonInvoke);
        }

        private void OnDisable() {
            _closeWindowButton.onClick.RemoveListener(OnClickCloseWindowInvoke);
            _openWorldButton.onClick.RemoveListener(OnClickOpenWorldButtonInvoke);
            _showAddWorldWindowButton.onClick.RemoveListener(OnClickShowAddWorldWindowButtonInvoke);
            _removeWorldButton.onClick.RemoveListener(OnClickRemoveWorldButtonInvoke);
        }

        private void OnClickCloseWindowInvoke() =>
            OnClickCloseWindowButton?.Invoke();

        private void OnClickOpenWorldButtonInvoke() =>
            OnClickOpenWorldButton?.Invoke();

        private void OnClickShowAddWorldWindowButtonInvoke() =>
            OnClickShowAddWorldWindowButton?.Invoke();

        private void OnClickRemoveWorldButtonInvoke() =>
            OnClickRemoveWorldButton?.Invoke();

        public void SetInteractableOpenButton(bool interactable) =>
            _openWorldButton.interactable = interactable;

        public void SetInteractableAddButton(bool interactable) =>
            _showAddWorldWindowButton.interactable = interactable;

        public void SetInteractableRemoveButton(bool interactable) =>
            _removeWorldButton.interactable = interactable;
    }
}