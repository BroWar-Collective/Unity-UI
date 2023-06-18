using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace BroWar.UI.Common
{
    [AddComponentMenu("BroWar/UI/Common/UI Panel")]
    public class UiPanel : UiObject
    {
        [SerializeReference, ReferencePicker(TypeGrouping = TypeGrouping.ByFlatName)]
        [FormerlySerializedAs("showHideHandler")]
        private IActivityHandler activityHandler;

        public override void Show(bool immediately, Action onFinish = null)
        {
            if (activityHandler == null)
            {
                base.Show(immediately, onFinish);
                return;
            }

            activityHandler.Show(this, immediately, onFinish);
        }

        public override void Hide(bool immediately, Action onFinish = null)
        {
            if (activityHandler == null)
            {
                base.Hide(immediately, onFinish);
                return;
            }

            activityHandler.Hide(this, immediately, onFinish);
        }
    }
}