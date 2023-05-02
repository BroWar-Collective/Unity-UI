using UnityEngine;

namespace BroWar.UI.Management
{
    //TODO: make it obsolete
    [AddComponentMenu("BroWar/UI/Views Manager")]
    public class ViewsManager : UiViewsManagerBase
    {
        [SerializeField, Line]
        private Camera targetCamera;

        private void Awake()
        {
            if (targetCamera == null)
            {
                targetCamera = Camera.main;
                Debug.LogWarning("[UI] Target camera is not assigned.");
            }

            Initialize(targetCamera);
        }
    }
}