using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace Modules.GameMenu {
    public class HoverHandler : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {
        public UnityEvent OnHover;
        public UnityEvent OnExitHover;
        
        public void OnPointerEnter(PointerEventData eventData) =>
            OnHover?.Invoke();

        public void OnPointerExit(PointerEventData eventData) =>
            OnExitHover?.Invoke();
    }
}