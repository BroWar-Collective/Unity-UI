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
    [AddComponentMenu("BroWar/UI/Elements/UI Button")]
    public class UiButton : UiObject
    {
        [SerializeField, NotNull]
        private Button button;
        [SerializeField]
        private Image labelIcon;
        [SerializeField]
        private TextMeshProUGUI labelText;

        public event Action OnClicked;

        public bool HasText => labelText != null;

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