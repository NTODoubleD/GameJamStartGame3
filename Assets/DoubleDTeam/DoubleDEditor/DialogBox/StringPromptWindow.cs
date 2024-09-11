using UnityEditor;
using UnityEngine;

//TODO: StringPromptWindow feature
namespace DoubleDEditor.DialogBox
{
    public class StringPromptWindow : EditorWindow
    {
        public static void ShowWindow(string title)
        {
            GetWindow<StringPromptWindow>(title, true);
        }

        private void OnGUI()
        {
            GUILayout.Label("Введите ваше имя", EditorStyles.boldLabel);
            // playerName = EditorGUILayout.TextField("Имя:", playerName);

            if (GUILayout.Button("Ок"))
            {
                Close();
            }
        }
    }
}