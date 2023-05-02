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
        private readonly Dictionary<string, NativeObjectPool<TooltipBehaviour>> poolsByIds
            = new Dictionary<string, NativeObjectPool<TooltipBehaviour>>();

        [SerializeField]
        private Transform parent;
        [SerializeField, NotNull]
        [Tooltip("TODO")]
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

            var id = GenerateId(prefab);
            if (poolsByIds.TryGetValue(id, out _))
            {
                LogHandler.Log($"[UI][Tooltip] Prefab with ID: '{id}' is already cached.", LogType.Warning);
                return;
            }

            var pool = new NativeObjectPool<TooltipBehaviour>(prefab, parent);
            poolsByIds.Add(id, pool);
            pool.FillPool(0);
        }

        private string GenerateId(TooltipBehaviour prefab)
        {
            //TODO: temporary
            return prefab.GetInstanceID().ToString();
        }

        public TooltipBehaviour Create()
        {
            if (defaultPrefab == null)
            {
                return null;
            }

            return Create(defaultPrefab);
        }

        //TODO: refactor
        public T Create<T>(T prefab) where T : TooltipBehaviour
        {
            if (prefab == null)
            {
                return Create() as T;
            }

            var id = GenerateId(prefab);
            if (poolsByIds.TryGetValue(id, out var pool))
            {
                var instance = pool.Get() as T;
                if (instance != null)
                {
                    instance.Id = id;
                }

                return instance;
            }

            CachePrefab(prefab);
            return Create(prefab);
        }

        public virtual void Dispose(TooltipBehaviour target)
        {
            if (target == null)
            {
                return;
            }

            var id = target.Id;
            if (!poolsByIds.TryGetValue(id, out var pool))
            {
                LogHandler.Log($"[UI][Tooltip] Cannot dispose tooltip with ID: '{id}'.", LogType.Error);
                return;
            }

            target.Id = null;
            pool.Release(target);
        }

        public bool IsInitialized { get; private set; }
    }
}