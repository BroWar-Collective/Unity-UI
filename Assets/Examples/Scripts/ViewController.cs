using BroWar.UI.Elements;
using BroWar.UI.Management;
using UnityEngine;
using Zenject;

namespace Examples
{
    public class ViewController : MonoBehaviour
    {
        [SerializeField]
        private bool showOnStart = true;

        [EditorButton(nameof(Hide))]
        [EditorButton(nameof(Show))]
        [SerializeField, TypeConstraint(typeof(UiView))]
        private SerializedType viewType;

        private IUiViewsHandler viewsHandler;

        private void Start()
        {
            if (showOnStart)
            {
                Show();
            }
        }

        [Inject]
        internal void Inject(IUiViewsHandler viewsHandler)
        {
            this.viewsHandler = viewsHandler;
        }

        public void Show()
        {
            viewsHandler?.Show(viewType);
        }

        public void Hide()
        {
            viewsHandler?.Hide(viewType);
        }
    }
}