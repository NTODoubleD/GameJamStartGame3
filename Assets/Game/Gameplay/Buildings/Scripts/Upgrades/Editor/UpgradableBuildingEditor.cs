using UnityEditor;
using UnityEngine;

namespace Game.Gameplay.Buildings
{
    [CustomEditor(typeof(UpgradableBuildingMock))]
    public class UpgradableBuildingEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            UpgradableBuildingMock targetComponent = (UpgradableBuildingMock)target;

            if (GUILayout.Button("Add TownHall Condition"))
                targetComponent.AddTownHallCondition();

            if (GUILayout.Button("Add Resources Condition"))
                targetComponent.AddResourcesCondition();
        }
    }
}
