using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace BroWar.UI.Common
{
    /// <summary>
    /// Custom overlay on <see cref="Button"/>.
    /// </summary>
    [RequireComponent(typeof(Button))]
    [AddComponentMenu("BroWar/UI/Common/UI Button")]
    public class UiButton : UiObject
    {
        [Title("Content")]
        [SerializeField]
        private Image labelIcon;
        [SerializeField]
        private TextMeshProUGUI labelText;

        private Button button;

        public event Action OnClicked;

        private void Awake()
        {
            Button.onClick.AddListener(OnClick);
        }

        protected virtual void OnClick()
        {
            OnClicked?.Invoke();
        }

        private Button Button
        {
            get
            {
                if (button == null)
                {
                    button = GetComponent<Button>();
                }

                return button;
            }
        }

        /// <summary>
        /// Indicates if <see cref="Button"/> has associated label component.
        /// </summary>
        public bool HasText => labelText != null;

        /// <summary>
        /// Indicates if <see cref="Button"/> is interactable.
        /// </summary>
        public bool IsInteractable
        {
            get => Button.interactable;
            set => Button.interactable = true;
        }

        public string Text
        {
            get => labelText.text;
            set => labelText.text = value;
        }

        public Sprite Icon
        {
            get => labelIcon.sprite;
            set => labelIcon.sprite = value;
        }
    }
}