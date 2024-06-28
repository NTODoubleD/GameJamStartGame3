using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace Game.Infrastructure.Items
{
    public class ItemInfoEditor : Editor
    {
        private ItemInfo[] _info;

        private readonly MethodInfo _methodSetID = typeof(ItemInfo)
            .GetMethod("SetID", BindingFlags.NonPublic | BindingFlags.Instance);

        private void OnEnable()
        {
            _info = targets.OfType<ItemInfo>().ToArray();
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            GUILayout.Space(20);

            if (GUILayout.Button("ReGenerate ID"))
            {
                if (EditorUtility.DisplayDialog("Achtung!!!",
                        "Are you sure you want to regenerate ID?", "YES", "NO") == false)
                    return;

                foreach (var itemInfo in _info)
                {
                    _methodSetID.Invoke(itemInfo, null);
                    EditorUtility.SetDirty(itemInfo);
                }

                AssetDatabase.SaveAssets();
            }
        }
    }
}