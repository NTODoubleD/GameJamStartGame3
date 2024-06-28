using System.Collections.Generic;
using System.Reflection;
using DoubleDTeam.Containers.Base;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

namespace DoubleDTeam.Containers.Initializers.Editor.Base
{
    public class ModuleRegisterEditor : UnityEditor.Editor
    {
        private ModuleRegister _initializer;

        private readonly FieldInfo _fieldInfo = typeof(ModuleRegister).GetField("_initializeObjects",
            BindingFlags.Instance | BindingFlags.NonPublic);

        private void OnEnable()
        {
            _initializer = (ModuleRegister)target;
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            if (GUILayout.Button("Add all initializing object"))
            {
                AddAllModules();

                if (!GUI.changed)
                    return;

                EditorUtility.SetDirty(_initializer.gameObject);
                EditorSceneManager.MarkSceneDirty(_initializer.gameObject.scene);
            }
        }

        private void AddAllModules()
        {
            var initializeObjects =
                FindObjectsByType<InitializeObject>(FindObjectsInactive.Include, FindObjectsSortMode.None);

            var objects = (List<InitializeObject>)_fieldInfo.GetValue(_initializer);

            foreach (var initializeObject in initializeObjects)
            {
                if (objects.Contains(initializeObject))
                    continue;

                objects.Add(initializeObject);
            }

            objects.RemoveAll(o => o == null);
        }
    }
}