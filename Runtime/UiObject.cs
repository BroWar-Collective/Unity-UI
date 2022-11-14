using System;
using UnityEngine;

namespace BroWar.UI
{
    /// <summary>
    /// Base class for all UI-related objects.
    /// </summary>
    [DisallowMultipleComponent, RequireComponent(typeof(RectTransform))]
    public abstract class UiObject : MonoBehaviour
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
            if (value)
            {
                OnShow?.Invoke();
            }
            else
            {
                OnHide?.Invoke();
            }
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