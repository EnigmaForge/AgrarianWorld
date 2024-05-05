using System;
using System.Collections.Generic;
using Modules.ViewsModule;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Modules.GameMenu {
    public class AddWorldPopupView : ViewBehaviour {
        [SerializeField] private Button _closeWindowButton;
        [SerializeField] private Button _createWorldButton;
        [SerializeField] private Button _cancelButton;
        [SerializeField] private TMP_Dropdown _worldTypeDropdown;
        [SerializeField] private TMP_InputField _worldNameInputField;
        [SerializeField] private TMP_InputField _seedInputField;

        public event Action OnClickCloseWindowButton;
        public event Action OnClickCreateWorldButton;
        public event Action<string> OnChangedWorldName;
        public event Action<string> OnChangedSeed;
        public event Action<string> OnChangedWorldType;

        private void OnEnable() {
            _closeWindowButton.onClick.AddListener(OnClickCloseWindowInvoke);
            _cancelButton.onClick.AddListener(OnClickCloseWindowInvoke);
            _createWorldButton.onClick.AddListener(OnClickCreateWorldInvoke);
            _worldNameInputField.onValueChanged.AddListener(OnChangedWorldNameInvoke);
            _seedInputField.onValueChanged.AddListener(OnChangedSeedInvoke);
            _worldTypeDropdown.onValueChanged.AddListener(OnChangedWorldTypeInvoke);
        }

        private void OnDisable() {
            _closeWindowButton.onClick.RemoveListener(OnClickCloseWindowInvoke);
            _cancelButton.onClick.RemoveListener(OnClickCloseWindowInvoke);
            _createWorldButton.onClick.RemoveListener(OnClickCreateWorldInvoke);
            _worldNameInputField.onValueChanged.RemoveListener(OnChangedWorldNameInvoke);
            _seedInputField.onValueChanged.RemoveListener(OnChangedSeedInvoke);
            _worldTypeDropdown.onValueChanged.RemoveListener(OnChangedWorldTypeInvoke);
        }

        private void OnClickCloseWindowInvoke() =>
            OnClickCloseWindowButton?.Invoke();

        private void OnClickCreateWorldInvoke() =>
            OnClickCreateWorldButton?.Invoke();

        private void OnChangedWorldNameInvoke(string newWorldName) =>
            OnChangedWorldName?.Invoke(newWorldName);

        private void OnChangedSeedInvoke(string newSeed) =>
            OnChangedSeed?.Invoke(newSeed);

        private void OnChangedWorldTypeInvoke(int newWorldTypeIndex) =>
            OnChangedWorldType?.Invoke(_worldTypeDropdown.options[newWorldTypeIndex].text);

        public void SetDropdownItems(List<string> items) {
            _worldTypeDropdown.options.Clear();
            _worldTypeDropdown.AddOptions(items);
        }
    }
}