﻿using Toolbox.Editor;
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
            const string contextsPropertyName = "contexts";
            IgnoreProperty(contextsPropertyName);
            var listProperty = serializedObject.FindProperty(contextsPropertyName);

            contextsList = new ToolboxEditorList(listProperty)
            {
                drawElementCallback = (rect, index, isActive, isFocused) =>
                {
                    using (new EditorGUILayout.VerticalScope(Style.viewContextBackgroundStyle))
                    {
                        var ctxProperty = listProperty.GetArrayElementAtIndex(index);
                        var viewProperty = ctxProperty.FindPropertyRelative("view");
                        var viewReference = viewProperty.objectReferenceValue as UiView;

                        var label = CreateContextLabel(viewReference);
                        ToolboxEditorGui.DrawToolboxProperty(ctxProperty, label);
                        if (!ctxProperty.isExpanded)
                        {
                            return;
                        }

                        EditorGUI.indentLevel++;
                        var handler = Handler;
                        DrawContextTools(handler, viewReference);
                        EditorGUI.indentLevel--;
                    }
                }
            };
        }

        private void DrawContexts()
        {
            serializedObject.Update();
            contextsList.DoList();
            serializedObject.ApplyModifiedProperties();
        }

        private GUIContent CreateContextLabel(UiView view)
        {
            if (view == null)
            {
                return null;
            }

            var text = $"{(view.IsActive ? "[a] " : string.Empty)}{view.name}";
            return new GUIContent(text);
        }

        private void DrawHandlerTools(ViewsHandler handler)
        {
            using (new EditorGUILayout.VerticalScope(EditorStyles.helpBox))
            {
                EditorGUILayout.LabelField("Tools", EditorStyles.boldLabel);
                if (GUILayout.Button("Hide All"))
                {
                    handler.HideAll();
                }
            }
        }

        private void DrawContextTools(ViewsHandler handler, UiView view)
        {
            if (!handler.IsInitialized || view == null)
            {
                return;
            }

            using (new EditorGUI.DisabledScope(true))
            {
                EditorGUILayout.Toggle("Is Active", view.IsActive);
            }

            using (new EditorGUILayout.HorizontalScope())
            {
                if (GUILayout.Button("Show"))
                {
                    handler.Show(view);
                }

                if (GUILayout.Button("Hide"))
                {
                    handler.Hide(view);
                }
            }
        }

        public override void DrawCustomInspector()
        {
            base.DrawCustomInspector();
            DrawContexts();

            var handler = Handler;
            if (!handler.IsInitialized)
            {
                return;
            }

            DrawHandlerTools(handler);
        }

        public override bool RequiresConstantRepaint()
        {
            return Handler.IsInitialized;
        }

        private ViewsHandler Handler => target as ViewsHandler;

        private static class Style
        {
            internal static readonly GUIStyle viewContextBackgroundStyle = new GUIStyle(EditorStyles.helpBox)
            {
                padding = new RectOffset(16, 0, 0, 0)
            };
        }
    }
}