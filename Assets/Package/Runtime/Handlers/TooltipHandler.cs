using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Object = UnityEngine.Object;

namespace BroWar.UI.Handlers
{
    using BroWar.UI.Tooltip;

    /// <summary>
    /// <see cref="IUiHandler"/> responsible for showing and updating tooltips.
    /// </summary>
    [Serializable]
    public class TooltipHandler : IUiHandler, ITooltipHandler
    {
        private readonly Dictionary<Object, TooltipBehaviour> tooltipsByPrefabs = new Dictionary<Object, TooltipBehaviour>();

        [SerializeField]
        private Canvas canvas;

        [Line]
        [SerializeField, NotNull]
        private TooltipBehaviour tooltipPrefab;
        [SerializeField, Disable]
        [Tooltip("Tooltip instance created in runtime.")]
        private TooltipBehaviour tooltip;

        private Vector2 ScreenPosition
        {
            get
            {
                //TODO: better way to handle input?
                //TODO: none mouse position
                return Mouse.current.position.ReadValue();
            }
        }

        public void Prepare()
        { }

        public void Tick()
        {
            if (!IsTooltipActive)
            {
                return;
            }

            tooltip.UpdatePosition(ScreenPosition);
        }

        public void Dispose()
        {
            HideTooltip();
        }

        //TODO: separate method to set content and separate to show tooltip
        public void ShowTooltip(string contentText, in TooltipData data)
        {
            tooltip.UpdatePositionAndData(ScreenPosition, in data);
            tooltip.UpdateContent(contentText);
            tooltip.Show();
        }

        //TODO: refactor
        public TooltipBehaviour ShowTooltip(string contentText, in TooltipData data, TooltipBehaviour tooltipPrefab)
        {
            if (tooltipPrefab == null)
            {
                tooltipPrefab = this.tooltipPrefab;
            }

            if (!tooltipsByPrefabs.TryGetValue(tooltipPrefab, out var instance))
            {
                instance = Object.Instantiate(tooltipPrefab, CanvasParent);
                instance.name = tooltipPrefab.name;
                tooltipsByPrefabs[tooltipPrefab] = instance;
            }

            instance.UpdatePositionAndData(ScreenPosition, in data);
            instance.UpdateContent(contentText);
            instance.Show();
            tooltip = instance;
            return instance;
        }

        public void HideTooltip()
        {
            if (tooltip != null && tooltip.IsActive)
            {
                tooltip.Hide();
            }
        }

        private Transform CanvasParent => canvas.transform;

        public bool IsTickable => IsTooltipActive;
        public bool IsTooltipActive => tooltip != null && tooltip.IsActive;
    }
}