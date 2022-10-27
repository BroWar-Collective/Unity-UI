using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace BroWar.UI
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

        public bool HasLabel => label != null;

        public string Label
        {
            get => label.text;
            set => label.text = value;
        }

        private void Awake()
        {
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