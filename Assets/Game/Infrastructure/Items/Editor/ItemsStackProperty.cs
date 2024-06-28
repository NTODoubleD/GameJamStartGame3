using UnityEditor;
using UnityEngine;

namespace Game.Infrastructure.Items
{
    [CustomPropertyDrawer(typeof(ItemsStack))]
    public class ItemsStackProperty : PropertyDrawer
    {
        private const float IntWidth = 50f;
        private const float Spacing = 10f;

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            var itemInfo = property.FindPropertyRelative("_itemInfo");
            var amount = property.FindPropertyRelative("_amount");

            EditorGUI.BeginProperty(position, label, property);

            Rect intFieldPosition = new Rect(position.xMax - IntWidth, position.y, IntWidth, position.height);

            Rect labelRect = new Rect(position.x, position.y, (intFieldPosition.x + Spacing) / 4, position.height);

            float fieldWidth = intFieldPosition.x - Spacing - labelRect.xMax - Spacing;

            Rect fieldPosition = new Rect(labelRect.xMax + Spacing, position.y, fieldWidth, position.height);

            EditorGUI.PrefixLabel(labelRect, label);
            EditorGUI.PropertyField(fieldPosition, itemInfo, GUIContent.none);

            amount.intValue = EditorGUI.IntField(intFieldPosition, amount.intValue);

            if (amount.intValue < 1)
                amount.intValue = 1;

            EditorGUI.EndProperty();
        }
    }
}