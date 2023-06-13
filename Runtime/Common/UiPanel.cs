﻿using System;
using UnityEngine;

namespace BroWar.UI.Common
{
    [AddComponentMenu("BroWar/UI/Common/UI Panel")]
    public class UiPanel : UiObject
    {
        //TODO: rename to ActivityHandler
        [SerializeReference, ReferencePicker(TypeGrouping = TypeGrouping.ByFlatName)]
        private IActivityHandler showHideHandler;

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
    }
}