using System.Collections.Generic;
using Toolbox.Editor;
using Toolbox.Editor.Internal;
using UnityEditor;
using UnityEngine;

namespace BroWar.Editor.UI
{
    using BroWar.UI.Elements;
    using BroWar.UI.Views;

    [CustomEditor(typeof(ViewsHandler))]
    public class ViewsHandlerEditor : ToolboxEditor
    {
        private ToolboxEditorList contextsList;

        private void OnEnable()
        {
            var viewsProperty = serializedObject.FindProperty("contexts");
            contextsList = new ToolboxEditorList(viewsProperty)
            {
                drawElementCallback = (rect, index, isActive, isFocused) =>
                {
                    var property = viewsProperty.GetArrayElementAtIndex(index);
                    ToolboxEditorGui.DrawToolboxProperty(property);

                    var handler = target as ViewsHandler;
                    if (!handler.IsInitialized)
                    {
                        return;
                    }

                    var viewProperty = property.FindPropertyRelative("view");
                    var view = viewProperty.objectReferenceValue as UiView;
                    if (view == null)
                    {
                        return;
                    }

                    EditorGUILayout.Toggle("Is Active", view.IsActive);
                    if (GUILayout.Button("Hide"))
                    {
                        handler.Hide(view);
                    }

                    if (GUILayout.Button("Show"))
                    {
                        handler.Show(view);
                    }
                }
            };
        }

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
            }
        }

        public override void DrawCustomInspector()
        {
            base.DrawCustomInspector();
            serializedObject.Update();
            contextsList.DoList();
            serializedObject.ApplyModifiedProperties();

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