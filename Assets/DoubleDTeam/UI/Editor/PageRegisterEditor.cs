using System.Reflection;
using DoubleDTeam.UI.Initializers;
using UnityEditor;
using UnityEngine;

namespace DoubleDTeam.UI.Editor
{
    [CustomEditor(typeof(PageRegister))]
    public class PageRegisterEditor : UnityEditor.Editor
    {
        private PageRegister _pageRegister;

        private readonly FieldInfo _pagesField =
            typeof(PageRegister).GetField("_monoPages", BindingFlags.NonPublic | BindingFlags.Instance);

        private void OnEnable()
        {
            _pageRegister = target as PageRegister;
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            if (GUILayout.Button("Add all pages"))
            {
                var pages = FindObjectsByType<MonoPage>(FindObjectsInactive.Include, FindObjectsSortMode.None);
                _pagesField.SetValue(_pageRegister, pages);

                EditorUtility.SetDirty(_pageRegister);
                AssetDatabase.SaveAssets();
            }
        }
    }
}