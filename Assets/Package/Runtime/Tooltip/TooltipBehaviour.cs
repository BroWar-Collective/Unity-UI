using TMPro;
using UnityEngine;

namespace BroWar.UI.Tooltip
{
    [AddComponentMenu("BroWar/UI/Tooltip/Tooltip Behaviour")]
    public class TooltipBehaviour : UiObject
    {
        [SerializeField]
        private TextMeshProUGUI contentText;
    }
}