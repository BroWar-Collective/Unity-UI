using System;
using UnityEngine;

namespace BroWar.UI.Common
{
    [AddComponentMenu("BroWar/UI/Common/UI Panel")]
    public class UiPanel : UiObject
    {
        [SerializeReference, ReferencePicker(TypeGrouping = TypeGrouping.ByFlatName)]
        private IShowHideHandler showHideHandler;

        public virtual void Show(bool immediately, Action onFinish = null)
        {
            if (showHideHandler == null)
            {
                base.Show();
                onFinish?.Invoke();
                return;
            }

            showHideHandler.Show(this, immediately, onFinish);
        }

        public virtual void Hide(bool immediately, Action onFinish = null)
        {
            if (showHideHandler == null)
            {
                base.Hide();
                onFinish?.Invoke();
                return;
            }

            showHideHandler.Hide(this, immediately, onFinish);
        }

        public override bool Shows => showHideHandler?.Shows ?? false;
        public override bool Hides => showHideHandler?.Hides ?? false;
    }
}