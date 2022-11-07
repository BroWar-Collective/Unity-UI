using UnityEngine;

namespace BroWar.UI
{
    public abstract class ComponentBasedHandler : MonoBehaviour, IUiFeatureHandler
    {
        public virtual void Dispose()
        { }

        public virtual void Prepare()
        { }

        public virtual void Tick()
        { }

        public abstract bool IsTickable { get; }
    }
}