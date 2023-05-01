using System;
using UnityEngine;
using UnityEngine.InputSystem.UI;

namespace BroWar.UI.Handlers
{
    /// <summary>
    /// Overlay used to wrap UI-based input features.
    /// Mainly focused on using internally the <see cref="InputSystemUIInputModule"/>.
    /// </summary>
    [Serializable]
    public class InputHandler : IUiHandler
    {
        [SerializeField]
        private InputSystemUIInputModule inputModule;

        public void Prepare()
        {
            throw new System.NotImplementedException();
        }

        public void Tick()
        {
            throw new System.NotImplementedException();
        }

        public void Dispose()
        {
            throw new System.NotImplementedException();
        }

        public bool IsTickable => throw new System.NotImplementedException();
    }
}