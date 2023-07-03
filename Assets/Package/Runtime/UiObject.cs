using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace BroWar.UI
{
    /// <summary>
    /// Base class for all UI-related objects.
    /// Contains basic API for hiding/showing.
    /// </summary>
    [DisallowMultipleComponent, RequireComponent(typeof(RectTransform))]
    public abstract class UiObject : MonoBehaviour, IActivityTarget
    {
        [SerializeReference, ReferencePicker(TypeGrouping = TypeGrouping.ByFlatName)]
        [FormerlySerializedAs("showHideHandler")]
        private IActivityHandler activityHandler;

        private RectTransform rectTransform;

        public event Action OnShow;
        public event Action OnHide;

        protected virtual void Awake()
        { }

        protected virtual void OnDestroy()
        {
            ResetActivity();
        }

        protected virtual void OnValidate()
        {
            ResetActivity();
        }

        protected void ResetActivity()
        {
            activityHandler?.Dispose();
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

        public virtual void Show(bool immediately, Action onFinish = null)
        {
            if (activityHandler == null)
            {
                Show();
                onFinish?.Invoke();
                return;
            }

            activityHandler.Show(this, immediately, onFinish);
        }

        public virtual void Hide()
        {
            SetActive(false);
        }

        public virtual void Hide(bool immediately, Action onFinish = null)
        {
            if (activityHandler == null)
            {
                Hide();
                onFinish?.Invoke();
                return;
            }

            activityHandler.Hide(this, immediately, onFinish);
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
        public virtual bool Shows => activityHandler?.Shows ?? false;
        /// <summary>
        /// Indicates if <see cref="UiObject"/> is during the hiding operation.
        /// Valid only for time-based operations, otherwise always <see langword="false"/>.
        /// </summary>
        public virtual bool Hides => activityHandler?.Hides ?? false;

        /// <summary>
        /// Indicates if <see cref="UiObject"/> is going to be activated or deactivated but in time.
        /// </summary>
        public bool IsActivityChanging => Shows || Hides;

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