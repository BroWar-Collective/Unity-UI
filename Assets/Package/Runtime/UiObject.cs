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
        private RectTransform rectTransform;

        public event Action OnShow;
        public event Action OnHide;

        protected virtual void Awake()
        { }

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

        protected RectTransform RectTransform
        {
            get
            {
                if (rectTransform == null)
                {
                    rectTransform = GetComponent<RectTransform>();
                }

                return rectTransform;
            }
        }

        public bool IsActive => gameObject.activeSelf;
    }
}