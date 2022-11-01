using System;
using UnityEngine;

namespace BroWar.UI
{
    /// <summary>
    /// Base class for all UI-related objects.
    /// </summary>
    [DisallowMultipleComponent, RequireComponent(typeof(RectTransform))]
    public class UiObject : MonoBehaviour
    {
        protected RectTransform rectTransform;

        public event Action OnShow;
        public event Action OnHide;

        protected virtual void Awake()
        {
            rectTransform = GetComponent<RectTransform>();
        }

        public virtual void SetActive(bool value)
        {
            gameObject.SetActive(value);
        }

        public virtual void Show()
        {
            SetActive(true);
            OnShow?.Invoke();
        }

        public virtual void Hide()
        {
            SetActive(false);
            OnHide?.Invoke();
        }

        public bool IsActive => gameObject.activeSelf;
    }
}