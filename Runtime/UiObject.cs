using System;
using UnityEngine;

namespace BroWar.UI
{
    /// <summary>
    /// Base class for all UI-related objects.
    /// Contains basic API for hiding/showing.
    /// </summary>
    [DisallowMultipleComponent, RequireComponent(typeof(RectTransform))]
    public abstract class UiObject : MonoBehaviour, IHidableObject
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

        public virtual void Show(bool immediately, Action onFinish = null)
        {
            Show();
            onFinish?.Invoke();
        }

        public virtual void Hide()
        {
            SetActive(false);
        }

        public virtual void Hide(bool immediately, Action onFinish = null)
        {
            Hide();
            onFinish?.Invoke();
        }

        /// <summary>
        /// Indicates if <see cref="UiObject"/> is safe to show.
        /// </summary>
        public virtual bool CanShow()
        {
            return !IsActive || Hides;
        }

        /// <summary>
        /// Indicates if <see cref="UiObject"/> is safe to hide.
        /// </summary>
        public virtual bool CanHide()
        {
            return IsActive && !Hides;
        }

        /// <summary>
        /// Indicates if <see cref="UiObject"/> is during the showing operation.
        /// alid only for time-based operations, otherwise always <see langword="false"/>.
        /// </summary>
        public virtual bool Shows => false;
        /// <summary>
        /// Indicates if <see cref="UiObject"/> is during the hiding operation.
        /// Valid only for time-based operations, otherwise always <see langword="false"/>.
        /// </summary>
        public virtual bool Hides => false;

        public bool IsActive => gameObject.activeSelf;

        public RectTransform RectTransform
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
    }
}