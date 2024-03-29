﻿using UnityEngine;

namespace BroWar.UI
{
    /// <summary>
    /// Base class for component-based <see cref="IUiHandler"/>s.
    /// </summary>
    public abstract class UiHandlerBehaviour : MonoBehaviour, IUiHandler
    {
        public virtual void Prepare()
        { }

        public virtual void Tick()
        { }

        public virtual void Dispose()
        { }

        public virtual bool IsTickable => false;
    }
}