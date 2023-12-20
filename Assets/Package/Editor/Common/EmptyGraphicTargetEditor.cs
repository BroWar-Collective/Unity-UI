using Toolbox.Editor;
using UnityEditor;

namespace BroWar.UI.Editor.Common
{
    using BroWar.UI.Common;

    [CustomEditor(typeof(EmptyGraphicTarget))]
    public class EmptyGraphicTargetEditor : ToolboxEditor
    {
        public override void DrawCustomInspector()
        { }
    }
}