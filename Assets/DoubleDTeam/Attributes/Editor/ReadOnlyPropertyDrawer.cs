using UnityEditor;
using UnityEngine;

namespace DoubleDTeam.Attributes.Editor
{
    [CustomPropertyDrawer(typeof(ReadOnlyPropertyAttribute))]
    public class ReadOnlyPropertyDrawer : PropertyDrawer
    {
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label) =>
            EditorGUI.GetPropertyHeight(property, label, true);

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            GUI.enabled = false;
            EditorGUI.PropertyField(position, property, label, true);
            GUI.enabled = true;
        }
    }
}