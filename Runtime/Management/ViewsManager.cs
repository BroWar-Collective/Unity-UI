using UnityEngine;

namespace BroWar.UI.Management
{
    //TODO: rethink the name
    [AddComponentMenu("BroWar/UI/Views Manager")]
    public class ViewsManager : ViewsManagerBase
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