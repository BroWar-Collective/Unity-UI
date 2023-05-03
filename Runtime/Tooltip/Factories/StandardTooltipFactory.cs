using System;
using System.Collections.Generic;
using UnityEngine;

namespace BroWar.UI.Tooltip.Factories
{
    using BroWar.Common;
    using BroWar.Common.Patterns;

    [Serializable]
    public class StandardTooltipFactory : ITooltipFactory
    {
        private readonly Dictionary<int, NativeObjectPool<TooltipBehaviour>> poolsByIds
            = new Dictionary<int, NativeObjectPool<TooltipBehaviour>>();

        [SerializeField]
        private Transform parent;
        [SerializeField, NotNull]
        private TooltipBehaviour defaultPrefab;

        private void CachePrefab(TooltipBehaviour prefab)
        {
            CachePrefab(prefab, parent);
        }

        private void CachePrefab(TooltipBehaviour prefab, Transform parent)
        {
            if (prefab == null)
            {
                LogHandler.Log($"[UI][Tooltip] Given prefab is invalid.", LogType.Error);
                return;
            }

            var id = prefab.GetInstanceID();
            if (poolsByIds.ContainsKey(id))
            {
                LogHandler.Log($"[UI][Tooltip] Prefab with ID: '{id}' is already cached.", LogType.Warning);
                return;
            }

            var pool = new NativeObjectPool<TooltipBehaviour>(prefab, parent);
            poolsByIds.Add(id, pool);
            pool.FillPool(0);
        }

        public TooltipBehaviour Create()
        {
            return Create<TooltipBehaviour>(null);
        }

        public T Create<T>(TooltipBehaviour prefab) where T : TooltipBehaviour
        {
            if (prefab == null)
            {
                if (defaultPrefab == null)
                {
                    LogHandler.Log("[UI][Tooltip] Default prefab not available.", LogType.Error);
                    return null;
                }

                prefab = defaultPrefab;
            }

            var id = prefab.GetInstanceID();
            if (poolsByIds.TryGetValue(id, out var pool))
            {
                var instance = pool.Get() as T;
                if (instance != null)
                {
                    instance.InstanceId = id;
#if UNITY_EDITOR
                    instance.name = prefab.name;
#endif
                }
                else
                {
                    LogHandler.Log($"[UI][Tooltip] Cannot create tooltip of type: {typeof(T)}.", LogType.Error);
                }

                return instance;
            }

            CachePrefab(prefab);
            return Create<T>(prefab);
        }

        public virtual void Dispose(TooltipBehaviour target)
        {
            if (target == null)
            {
                return;
            }

            var id = target.InstanceId;
            if (!poolsByIds.TryGetValue(id, out var pool))
            {
                LogHandler.Log($"[UI][Tooltip] Cannot dispose tooltip with ID: '{id}'.", LogType.Error);
                return;
            }

            target.InstanceId = -1;
            pool.Release(target);
        }

        public bool IsInitialized { get; private set; }
    }
}