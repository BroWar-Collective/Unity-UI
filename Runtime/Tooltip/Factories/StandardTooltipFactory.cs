using System;
using System.Collections.Generic;
using UnityEngine;

namespace BroWar.UI.Tooltip.Factories
{
    using BroWar.Common.Factories;
    using BroWar.Common.Patterns;

    [Serializable]
    public class StandardTooltipFactory : ITooltipFactory
    {
        private readonly Dictionary<TooltipBehaviour, NativeObjectPool<TooltipBehaviour>> poolsByPrefabs = new Dictionary<TooltipBehaviour, NativeObjectPool<TooltipBehaviour>>();

        [SerializeField]
        private Transform parent;
        [SerializeField, NotNull, ReorderableList(HasLabels = false)]
        [Tooltip("Default tooltip prefab.")]
        private TooltipBehaviour[] tooltipPrefabs;

        private void CachePrefabs()
        {
            CachePrefabs(tooltipPrefabs, parent);
        }

        private void CachePrefabs(IReadOnlyList<TooltipBehaviour> prefabs, Transform parent)
        {
            foreach (var prefab in prefabs)
            {
                //CachePrefab(prefab, parent, 0);
            }
        }

        private void CachePrefab(TooltipBehaviour prefab, Transform parent)
        {
            if (prefab == null)
            {
                //Debug.LogError($"[Factories] Given prefab is invalid.");
                return;
            }

            var type = prefab.GetType();
            if (poolsByPrefabs.TryGetValue(prefab, out _))
            {
                //Debug.LogError($"[Factories] Prefab with type {type} is already cached.");
                return;
            }

            var pool = new NativeObjectPool<TooltipBehaviour>(prefab, parent);
            poolsByPrefabs.Add(prefab, pool);
            pool.FillPool(0);
        }

        public void Initialize()
        {
            CachePrefabs();
            IsInitialized = true;
        }

        public bool IsTooltipSupported<T>() where T : TooltipBehaviour
        {
            return true;
            //return 
        }

        public virtual T Create<T>(T prefab) where T : TooltipBehaviour
        {
            if (poolsByPrefabs.TryGetValue(prefab, out var pool))
            {
                return pool.Get() as T;
            }

            //Debug.LogError($"[Factories] Cannot create instance for type - {type}.");
            return null;
        }

        public virtual void Dispose(TooltipBehaviour target)
        {
            if (!target)
            {
                return;
            }

            //if (poolsByPrefabs.TryGetValue(prefab, out var pool))
            //{
            //    pool.Release(target);
            //}
        }

        public bool IsInitialized { get; private set; }
    }
}