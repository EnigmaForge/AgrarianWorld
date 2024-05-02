using UnityEngine;

namespace Modules.ViewsModule {
    public abstract class ViewBehaviour : MonoBehaviour {
        public void SetActive(bool value) =>
            gameObject.SetActive(value);
    }
}