using BroWar.UI;
using UnityEngine;

namespace Examples
{
    public class ShowViewOnStart : MonoBehaviour
    {
        [SerializeField]
        private ViewsManager viewsManager;
        [SerializeField]
        private UiView prewarmedView;

        private void Start()
        {
            viewsManager.Show(prewarmedView.GetType());
        }
    }
}