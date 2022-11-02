using UnityEngine;

namespace BroWar.UI.Management
{
    [AddComponentMenu("BroWar/UI/Default Views Manager")]
    public class DefaultViewsManager : ViewsManager
    {
        [SerializeField, Line]
        private Camera targetCamera;
        [SerializeField, SerializeReference, ReferencePicker]
        private IUiFeatureHandler[] handlers;

        private void Awake()
        {
            if (targetCamera == null)
            {
                targetCamera = Camera.main;
                Debug.LogWarning("[UI] Target camera is no assigned.");
            }

            Initialize(targetCamera, new IUiFeatureHandler[] { });
        }
    }
}