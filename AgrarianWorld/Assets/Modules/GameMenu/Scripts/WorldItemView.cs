using Modules.ViewsModule;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Modules.GameMenu {
    public class WorldItemView : ViewBehaviour {
        [SerializeField] private Image _worldImage;
        [SerializeField] private TMP_Text _worldName;
        [SerializeField] private TMP_Text _lastOpenDate;

        public void SetWorldImage(Sprite worldImage) =>
            _worldImage.sprite = worldImage;
        
        public void SetWorldName(string worldName) =>
            _worldName.text = worldName;
        
        public void SetLastOpenDate(string lastOpenDate) =>
            _lastOpenDate.text = "Last open: " + lastOpenDate;
    }
}