using System;
using System.Collections.Generic;
using UnityEngine;

namespace BroWar.UI.Common
{
    [Serializable]
    public class UiContentRefresher
    {
        [SerializeReference, ReferencePicker(typeof(IContentRefreshAction), TypeGrouping.ByFlatName)]
        private IContentRefreshAction[] actions;

        public IReadOnlyList<IContentRefreshAction> Actions => actions;

        public void Refresh()
        {
            for (int i = 0; i < Actions.Count; i++)
            {
                IContentRefreshAction action = Actions[i];
                action.Perform();
            }
        }
    }
}