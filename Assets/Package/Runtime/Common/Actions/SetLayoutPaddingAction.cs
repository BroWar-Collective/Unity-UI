using System;
using UnityEngine;
using UnityEngine.UI;

namespace BroWar.UI.Common.Actions
{
    [Serializable]
    public class SetLayoutPaddingAction : IContentRefreshAction
    {
        [SerializeField]
        private LayoutGroup target;
        [SerializeField]
        private RectOffset padding;

        public void Perform()
        {
            target.padding = padding;
        }
    }
}