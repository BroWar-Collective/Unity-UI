using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.UI;
using Object = UnityEngine.Object;
using UnityEngine.EventSystems;

namespace BroWar.UI.Handlers
{
    using BroWar.Common;
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
        [SerializeField]
        private InputSystemUIInputModule inputModule;

        [Line]
        [SerializeField, SerializeReference, ReferencePicker(TypeGrouping = TypeGrouping.ByFlatName)]
        private ITooltipFactory factory;

        [SpaceArea]
        [SerializeField, Disable]
        [Tooltip("Currently active tooltip instance.")]
        private TooltipBehaviour activeTooltip;

        private TooltipBehaviour ActiveTooltip
        {
            get => activeTooltip;
            set
            {
                HideTooltip();
                activeTooltip = value;
            }
        }

        private Vector2 ScreenPosition
        {
            get
            {
                //TODO: UI-based interface to handle input
                //TODO: get it from the EventSystem
                var pointInputReference = inputModule.point;
                return pointInputReference.action.ReadValue<Vector2>();
            }
        }

        private void ShowTooltip(TooltipBehaviour tooltip)
        {
            if (tooltip == null)
            {
                LogHandler.Log("[UI] Cannot show tooltip. Instance is null.", LogType.Error);
                return;
            }

            ActiveTooltip = tooltip;
            ActiveTooltip.Show();
        }

        public void Prepare()
        { }

        public void Tick()
        {
            if (!IsTooltipActive)
            {
                return;
            }

            activeTooltip.UpdatePosition(ScreenPosition);
        }

        public void Dispose()
        {
            HideTooltip();
        }

        public TooltipBehaviour ShowTooltip(string contentText, in TooltipData data)
        {
            //return ShowTooltip(contentText, in data, tooltipPrefab);
            return null;
        }

        //TODO: refactor
        public TooltipBehaviour ShowTooltip(string contentText, in TooltipData data, TooltipBehaviour tooltipPrefab)
        {
            //if (tooltipPrefab == null)
            //{
            //    tooltipPrefab = this.tooltipPrefab;
            //}

            //TODO: move it to a factory
            if (!tooltipsByPrefabs.TryGetValue(tooltipPrefab, out var instance))
            {
                instance = Object.Instantiate(tooltipPrefab, CanvasParent);
                instance.name = tooltipPrefab.name;
                tooltipsByPrefabs[tooltipPrefab] = instance;
            }

            instance.UpdatePositionAndData(ScreenPosition, in data);
            instance.UpdateContent(contentText);
            ShowTooltip(instance);
            return instance;
        }

        public void HideTooltip()
        {
            if (!IsTooltipActive)
            {
                return;
            }

            ActiveTooltip.Hide();
            ActiveTooltip = null;
        }

        private Transform CanvasParent => canvas.transform;

        bool IUiHandler.IsTickable => IsTooltipActive;

        public bool IsTooltipActive => ActiveTooltip != null && ActiveTooltip.IsActive;
    }
}