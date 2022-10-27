using UnityEngine;

namespace BroWar.UI
{
    [AddComponentMenu("BroWar/UI/Default Views Manager")]
    public class DefaultViewsManager : ViewsManager
    {
        [SerializeField]
        private Camera targetCamera;

        protected override void Awake()
        {
            base.Awake();
            if (targetCamera == null)
            {
                targetCamera = Camera.main;
                Debug.LogWarning("[UI] Target camera is no assigned.");
            }
        }
    }
}