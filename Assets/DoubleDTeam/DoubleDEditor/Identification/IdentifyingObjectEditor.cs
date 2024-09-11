using DoubleDCore.Identification;
using UnityEditor;
using UnityEngine;

namespace DoubleDEditor.Identification
{
    public class IdentifyingObjectEditor : Editor
    {
        private Object _behaviour;

        private void OnEnable()
        {
            _behaviour = target;
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            if (_behaviour is not IIdentifying identifying)
                return;

            if (GUILayout.Button("Generate ID"))
            {
                if (EditorUtility.DisplayDialog("Achtung!!!",
                        "Are you sure you want to generate ID?", "Yes", "No") == false)
                    return;

                Undo.RecordObject(_behaviour, "Change ID");

                identifying.SetIdentifier(identifying.GenerateIdentifier());

                EditorUtility.SetDirty(_behaviour);
            }
        }
    }
}