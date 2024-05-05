using System;
using Modules.ViewsModule;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Modules.GameMenu {
    public class WorldItemView : ViewBehaviour, IPointerEnterHandler, IPointerExitHandler {
        [SerializeField] private Button _openWorldButton;
        [SerializeField] private Image _backgroundImage;
        [SerializeField] private Image _worldImage;
        [SerializeField] private TMP_Text _worldName;
        [SerializeField] private TMP_Text _lastOpenDate;
        private bool _selected;
        private int _index;

        public event Action<int> OnClickOpenWorldButton;

        private void OnEnable() =>
            _openWorldButton.onClick.AddListener(OnClickOpenWorldButtonInvoke);

        private void OnDisable() =>
            _openWorldButton.onClick.RemoveListener(OnClickOpenWorldButtonInvoke);

        private void OnClickOpenWorldButtonInvoke() =>
            OnClickOpenWorldButton?.Invoke(_index);

        public void SetWorldImage(Sprite worldImage) =>
            _worldImage.sprite = worldImage;
        
        public void SetWorldName(string worldName) =>
            _worldName.text = worldName;
        
        public void SetLastOpenDate(string lastOpenDate) =>
            _lastOpenDate.text = "Last open: " + lastOpenDate;

        public void SetSelected(bool selected) {
            _selected = selected;
            _backgroundImage.enabled = selected;
        }

        public void OnPointerEnter(PointerEventData eventData) {
            if (!_selected)
                _backgroundImage.enabled = true;
        }

        public void OnPointerExit(PointerEventData eventData) {
            if (!_selected)
                _backgroundImage.enabled = false;
        }

        public void SetIndex(int index) =>
            _index = index;
    }
}