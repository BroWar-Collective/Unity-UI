using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.InputSystem.UI;

namespace BroWar.UI.Handlers
{
    using BroWar.UI.Input;

    /// <summary>
    /// Overlay used to wrap UI-based input features.
    /// Mainly focused on using internally the <see cref="InputSystemUIInputModule"/>.
    /// </summary>
    [DisallowMultipleComponent]
    [AddComponentMenu("BroWar/UI/Handlers/Input Handler")]
    public class InputHandler : UiHandlerBehaviour, IUiInputHandler
    {
        [SerializeField]
        private InputSystemUIInputModule inputModule;

        public Vector2 PointPosition
        {
            get
            {
                if (inputModule == null)
                {
                    return Vector2.zero;
                }

                var pointAction = inputModule.point;
                if (pointAction == null)
                {
                    return Vector2.zero;
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