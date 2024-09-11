using System.Linq;
using System.Reflection;
using DoubleDCore.TranslationTools;
using UnityEditor;
using UnityEngine;

namespace DoubleDEditor.TranslationTools
{
    //[CustomPropertyDrawer(typeof(TranslatedText))]
    public class TranslatedTextDrawer : PropertyDrawer
    {
        private const float Spacing = 10f;
        private const float LanguageLabelWidth = 20f;
        private const float TextAreaHeight = 40f;
        private const float StandardHeight = 18f;
        private const float Margin = 18f;

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            var fieldInfos = GetFieldInfos();

            Rect labelRect = GetLabelRect(position);

            Rect[] languageWidgets = GetWidgetsRects(position);

            EditorGUI.BeginProperty(position, label, property);

            EditorGUI.PrefixLabel(labelRect, label);

            for (int i = 0; i < languageWidgets.Length; i++)
            {
                Rect widgetReact = languageWidgets[i];

                Rect languageLabelRect =
                    new Rect(widgetReact.x + Margin, widgetReact.y, LanguageLabelWidth, StandardHeight);

                EditorGUI.LabelField(languageLabelRect, fieldInfos[i].Name.Trim('_').ToUpper());

                Rect textAreaReact = new Rect(languageLabelRect.xMax + Spacing, widgetReact.y,
                    position.width - languageLabelRect.xMax + Spacing, TextAreaHeight);

                var value = property.FindPropertyRelative(fieldInfos[i].Name);
                value.stringValue = EditorGUI.TextArea(textAreaReact, value.stringValue);
            }

            EditorGUI.EndProperty();
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            float totalHeight = StandardHeight + Spacing +
                                GetWidgetsRects(new Rect()).Length * (TextAreaHeight + Spacing);
            return totalHeight;
        }

        private Rect[] GetWidgetsRects(Rect position)
        {
            var fieldInfos = GetFieldInfos();

            Rect labelRect = GetLabelRect(position);

            Rect[] languageWidgets = new Rect[fieldInfos.Length];

            for (int i = 0; i < languageWidgets.Length; i++)
            {
                languageWidgets[i] =
                    new Rect(position.x, (TextAreaHeight + Spacing) * i + position.y + labelRect.height + Spacing,
                        position.width, TextAreaHeight);
            }

            return languageWidgets;
        }

        private static FieldInfo[] GetFieldInfos()
        {
            return typeof(TranslatedText)
                .GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance)
                .Where(f => f.FieldType == typeof(string)).ToArray();
        }

        private Rect GetLabelRect(Rect position)
        {
            return new Rect(position.x, position.y, position.width, StandardHeight);
        }
    }
}