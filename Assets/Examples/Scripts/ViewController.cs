using System;
using BroWar.UI.Elements;
using BroWar.UI.Management;
using UnityEngine;

namespace Examples
{
    public class ViewController : MonoBehaviour
    {
        [SerializeField]
        private ViewsManager viewsManager;
        [SerializeField]
        private bool showOnStart = true;

        [EditorButton(nameof(Hide))]
        [EditorButton(nameof(Show))]
        [SerializeField, Line]
        private UiView targetView;

        private void Start()
        {
            if (showOnStart)
            {
                Show();
            }
        }

        public void Show()
        {
            viewsManager.Show(ViewType);
        }

        public void Hide()
        {
            viewsManager.Hide(ViewType);
        }

        private Type ViewType => targetView.GetType();
    }
}