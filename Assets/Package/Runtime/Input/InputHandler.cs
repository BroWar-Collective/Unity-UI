﻿using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.InputSystem.UI;

namespace BroWar.UI.Input
{
    /// <summary>
    /// Overlay used to wrap UI-based input features.
    /// Mainly focused on using internally the <see cref="InputSystemUIInputModule"/>.
    /// </summary>
    [DisallowMultipleComponent]
    [AddComponentMenu("BroWar/UI/Input/Input Handler")]
    public class InputHandler : UiHandlerBehaviour, IUiInputHandler
    {
        [SerializeField, NotNull]
        private InputSystemUIInputModule inputModule;

        public Vector2? PointerPosition
        {
            get
            {
                if (inputModule == null)
                {
                    return null;
                }

                var pointAction = inputModule.point;
                if (pointAction == null)
                {
                    return null;
                }

                return pointAction.action.ReadValue<Vector2>();
            }
        }

        public override void Prepare()
        {
            base.Prepare();
            Assert.IsNotNull(inputModule, "[UI][Input] Input module is null.");
        }
    }
}