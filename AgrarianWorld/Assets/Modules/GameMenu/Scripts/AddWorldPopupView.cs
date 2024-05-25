using System;
using System.Collections.Generic;
using Modules.ViewsModule;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Modules.GameMenu {
    public class AddWorldPopupView : ViewBehaviour {
        private const int WORLD_NAME_LENGTH = 32;
        private const int SEED_LENGTH = 6;
        [SerializeField] private Button _closeWindowButton;
        [SerializeField] private Button _createWorldButton;
        [SerializeField] private Button _cancelButton;
        [SerializeField] private TMP_Dropdown _worldTypeDropdown;
        [SerializeField] private TMP_InputField _worldNameInputField;
        [SerializeField] private TMP_InputField _seedInputField;

        public event Action OnClickCloseWindowButton;
        public event Action OnClickCreateWorldButton;
        public event Action<string> OnChangedWorldName;
        public event Action<int> OnChangedSeed;
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

        private void OnChangedWorldNameInvoke(string newWorldName) {
            if (newWorldName.Length > WORLD_NAME_LENGTH)
                _worldNameInputField.text = newWorldName[..WORLD_NAME_LENGTH];
            
            SetAddButtonInteractable(!string.IsNullOrEmpty(_worldNameInputField.text));
            OnChangedWorldName?.Invoke(newWorldName);
        }

        private void OnChangedSeedInvoke(string newSeed) {
            if (newSeed.Length > SEED_LENGTH)
                _seedInputField.text = newSeed[..SEED_LENGTH];
            
            bool isNumber = int.TryParse(newSeed, out int intValue);
            SetAddButtonInteractable(newSeed.Length == SEED_LENGTH && isNumber);
            if (isNumber) 
                OnChangedSeed?.Invoke(intValue);
        }

        private void OnChangedWorldTypeInvoke(int newWorldTypeIndex) =>
            OnChangedWorldType?.Invoke(_worldTypeDropdown.options[newWorldTypeIndex].text);

        private void SetAddButtonInteractable(bool interactable) =>
            _createWorldButton.interactable = interactable;

        public void SetDropdownItems(List<string> items) {
            _worldTypeDropdown.options.Clear();
            _worldTypeDropdown.AddOptions(items);
        }

        public void SetWorldName(string worldName) =>
            _worldNameInputField.text = worldName;

        public void SetSeedValue(int seed) {
            string result = string.Empty;
            int length = seed.ToString().Length - SEED_LENGTH;

            if (length < 0)
                for (int i = 0; i < -length; i++) 
                    result += "0";
            result += seed;

            _seedInputField.text = result;
        }

        public void SetWorldType(int worldTypeIndex) =>
            _worldTypeDropdown.value = worldTypeIndex;
    }
}