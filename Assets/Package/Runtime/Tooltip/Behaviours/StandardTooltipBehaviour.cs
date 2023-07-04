using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace BroWar.UI.Tooltip.Behaviours
{
    [AddComponentMenu("BroWar/UI/Tooltip/Behaviours/Tooltip Behaviour (Default)")]
    public class StandardTooltipBehaviour : TooltipBehaviour
    {
        [SerializeField, NotNull]
        private TextMeshProUGUI contentText;
        [SerializeField, NotNull]
        private LayoutGroup contentGroup;

        public void UpdateContent(string content)
        {
            contentText.SetText(content);
        }

        protected override void OnDataUpdate(TooltipData data)
        {
            base.OnDataUpdate(data);
            contentGroup.childAlignment = data.childAlignment;
        }
    }
}