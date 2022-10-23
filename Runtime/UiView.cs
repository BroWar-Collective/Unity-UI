using UnityEngine;

namespace BroWar.UI
{
    public class UiView : UiPanel
    {
        [SerializeField]
        private UiPanel[] panels;

        public override void Hide()
        {
            base.Hide();
            foreach (var panel in panels)
            {
                panel.Show();
            }
        }

        public override void Show()
        {
            base.Show();
            foreach (var panel in panels)
            {
                panel.Hide();
            }
        }
    }
}