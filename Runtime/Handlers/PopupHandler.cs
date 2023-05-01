using System;
using UnityEngine;

namespace BroWar.UI.Handlers
{
    [Serializable]
    public class PopupHandler : IUiHandler
    {
        public void Dispose()
        {
            throw new System.NotImplementedException();
        }

        public void Prepare()
        {
            throw new System.NotImplementedException();
        }

        public void Tick()
        {
            throw new System.NotImplementedException();
        }

        public bool IsTickable => false;
    }
}