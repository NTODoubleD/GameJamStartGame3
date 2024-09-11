using System;
using System.Reflection;
using DoubleDCore.Attributes;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace DoubleDEditor.Attributes
{
    [CustomPropertyDrawer(typeof(ButtonPropertyAttribute))]
    public class ButtonPropertyDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            string methodName = (attribute as ButtonPropertyAttribute).MethodName;

            Object target = property.serializedObject.targetObject;

            Type type = target.GetType();
            MethodInfo method = type.GetMethod(methodName);

            if (method == null)
            {
                EditorGUILayout.HelpBox("Method could not be found. Is it public?", MessageType.Error);
                return;
            }

            if (method.GetParameters().Length > 0)
            {
                EditorGUILayout.HelpBox("Method cannot have parameters!!!", MessageType.Error);
                return;
            }

            if (GUI.Button(position, method.Name))
            {
                method.Invoke(target, null);
            }
        }
    }
}