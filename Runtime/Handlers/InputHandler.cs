using UnityEngine;
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
    }
}