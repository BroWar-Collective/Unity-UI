using UnityEngine;

namespace BroWar.UI
{
    /// <summary>
    /// Base class for all UI-related objects.
    /// </summary>
    [DisallowMultipleComponent, RequireComponent(typeof(RectTransform))]
    public class UiObject : MonoBehaviour
    {
        public virtual void SetActive(bool value)
        {
            gameObject.SetActive(value);
        }

        public virtual void Show()
        {
            SetActive(true);
        }

        public virtual void Hide()
        {
            SetActive(false);
        }

        public bool IsActive => gameObject.activeSelf;
    }
}