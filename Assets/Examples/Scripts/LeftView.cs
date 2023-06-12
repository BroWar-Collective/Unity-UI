using System.Collections.Generic;
using BroWar.UI.Common;
using BroWar.UI.Views;
using UnityEngine;

namespace Examples
{
    public class LeftView : ScreenSpaceView
    {
        [Title("Content")]

        [SerializeField, ReorderableList(elementLabel: "Panel")]
        [Tooltip("Nested, optional, UI panels maintained by this view.")]
        private List<UiPanel> nestedPanels = new List<UiPanel>();

        protected override void OnInitialize(ViewData data)
        {
            base.OnInitialize(data);
            foreach (var panel in nestedPanels)
            {
                RegisterPanel(panel);
            }
        }
    }
}