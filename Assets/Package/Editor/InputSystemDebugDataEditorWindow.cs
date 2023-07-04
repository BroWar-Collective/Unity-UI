using System;
using System.Collections;
using System.Reflection;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem.UI;

namespace BroWar.Editor.UI
{
    public class InputSystemDebugDataEditorWindow : EditorWindow
    {
        private static readonly Type inputModuleType = typeof(InputSystemUIInputModule);
        private static readonly Type pointerModelType = inputModuleType.Assembly.GetType("UnityEngine.InputSystem.UI.PointerModel");
        private static readonly FieldInfo pointerStatesFieldInfo = inputModuleType.GetField("m_PointerStates", BindingFlags.NonPublic | BindingFlags.Instance);
        private static readonly FieldInfo eventDataFieldInfo = pointerModelType?.GetField("eventData");
        private static readonly PropertyInfo screenPositionFieldInfo = pointerModelType?.GetProperty("screenPosition");

        [MenuItem("Tools/BroWar/UI/Input System Debug Data", false, priority = 201)]
        public static void Initialize()
        {
            GetWindow(typeof(InputSystemDebugDataEditorWindow), false, "Input System Debug Data");
        }

        private void Update()
        {
            Repaint();
        }

        private void OnGUI()
        {
            var eventSystem = EventSystem.current;
            if (eventSystem == null)
            {
                EditorGUILayout.LabelField("Debug Info not available", EditorStyles.boldLabel);
                return;
            }

            var inputModule = eventSystem.currentInputModule;
            if (inputModule == null)
            {
                EditorGUILayout.LabelField("Debug Info not available", EditorStyles.boldLabel);
                return;
            }

            if (inputModule is InputSystemUIInputModule uiInputModule)
            {
                using (new EditorGUILayout.VerticalScope(EditorStyles.helpBox))
                {
                    EditorGUILayout.LabelField("Debug Info", EditorStyles.boldLabel);
                    EditorGUILayout.Space();
                    DrawDebugInfo(uiInputModule);
                }
            }
        }

        private void DrawDebugInfo(InputSystemUIInputModule inputModule)
        {
            var pointerStates = pointerStatesFieldInfo.GetValue(inputModule);
            if (pointerStates is IEnumerable pointerStatesCollection)
            {
                foreach (var pointerModel in pointerStatesCollection)
                {
                    var eventData = eventDataFieldInfo.GetValue(pointerModel);
                    if (eventData == null)
                    {
                        continue;
                    }

                    var screenPosition = screenPositionFieldInfo.GetValue(pointerModel);
                    EditorGUILayout.LabelField("<B>Pointer:</b> " + screenPosition.ToString(), Style.debugLabelStyle);
                    var eventDataContent = new GUIContent(eventData.ToString());
                    var size = Style.debugLabelStyle.CalcSize(eventDataContent);
                    EditorGUILayout.LabelField(eventData.ToString(), Style.debugLabelStyle, GUILayout.MinHeight(size.y));
                }
            }
        }

        private static class Style
        {
            internal static readonly GUIStyle debugLabelStyle = new GUIStyle(EditorStyles.label)
            {
                richText = true
            };
        }
    }
}