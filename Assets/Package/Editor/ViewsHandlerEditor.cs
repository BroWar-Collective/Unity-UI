using System.Collections.Generic;
using Toolbox.Editor;
using Toolbox.Editor.Internal;
using UnityEditor;
using UnityEngine;

namespace BroWar.Editor.UI
{
    using BroWar.UI.Elements;
    using BroWar.UI.Handlers;

    [CustomEditor(typeof(ViewsHandler))]
    public class ViewsHandlerEditor : ToolboxEditor
    {
        private ToolboxEditorList viewsList;

        private void OnEnable()
        {
            var viewsProperty = serializedObject.FindProperty("views");
            viewsList = new ToolboxEditorList(viewsProperty)
            {
                drawElementCallback = (rect, index, isActive, isFocused) =>
                {
                    var property = viewsProperty.GetArrayElementAtIndex(index);
                    ToolboxEditorGui.DrawToolboxProperty(property, GUIContent.none);

                    var handler = target as ViewsHandler;
                    if (!handler.IsInitialized)
                    {
                        return;
                    }

                    var view = property.objectReferenceValue as UiView;
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

                if (GUILayout.Button("Hide"))
                {
                    Handler.Hide(view);
                }
            }
        }

        public override void DrawCustomInspector()
        {
            base.DrawCustomInspector();
            serializedObject.Update();
            viewsList.DoList();
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