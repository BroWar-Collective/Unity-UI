using System.Collections.Generic;
using Toolbox.Editor;
using UnityEditor;
using UnityEngine;

namespace BroWar.Editor.UI
{
    using BroWar.UI.Elements;
    using BroWar.UI.Handlers;

    [CustomEditor(typeof(ViewsHandler))]
    public class ViewsHandlerEditor : ToolboxEditor
    {
        private void DrawActiveViews(IReadOnlyList<UiView> views)
        {
            using (new EditorGUILayout.VerticalScope(EditorStyles.helpBox))
            {
                EditorGUILayout.LabelField("Active Views", EditorStyles.boldLabel);
                if (views.Count == 0)
                {
                    EditorGUILayout.LabelField("<none>");
                    return;
                }

                foreach (var view in views)
                {
                    DrawActiveView(view);
                }
            }
        }

        private void DrawActiveView(UiView view)
        {
            using (new EditorGUILayout.HorizontalScope())
            {
                using (new EditorGUI.DisabledScope(true))
                {
                    EditorGUILayout.ObjectField(view, typeof(UiView), true);
                }

                if (GUILayout.Button("Hide"))
                {
                    Handler.Hide(view);
                }
            }
        }

        public override void DrawCustomInspector()
        {
            base.DrawCustomInspector();
            var handler = target as ViewsHandler;
            if (!handler.IsInitialized)
            {
                return;
            }

            var activeViews = new List<UiView>(handler.ActiveViews);
            DrawActiveViews(activeViews);
        }

        public override bool RequiresConstantRepaint()
        {
            return Handler.IsInitialized;
        }

        private ViewsHandler Handler => target as ViewsHandler;
    }
}