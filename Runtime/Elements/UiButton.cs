using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace BroWar.UI.Elements
{
    /// <summary>
    /// Custom overlay on <see cref="Button"/>.
    /// </summary>
    [RequireComponent(typeof(Button))]
    public class UiButton : UiObject
    {
        [SerializeField, NotNull]
        private Button button;
        [SerializeField]
        private Image icon;
        [SerializeField]
        private TextMeshProUGUI label;

        public event Action OnClicked;

        public bool HasText => label != null;

        public string Text
        {
            get => label.text;
            set => label.text = value;
        }

        public Sprite Icon
        {
            get => icon.sprite;
            set => icon.sprite = value;
        }

        protected override void Awake()
        {
            base.Awake();
            if (button == null)
            {
                button = GetComponent<Button>();
            }

            button.onClick.AddListener(OnClick);
        }

        private void OnClick()
        {
            OnClicked?.Invoke();
        }
    }
}