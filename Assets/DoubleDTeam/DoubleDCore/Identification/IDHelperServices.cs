using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace DoubleDCore.Identification
{
    public static class IDHelperServices
    {
        private static bool HasDuplicateInProject(string id, IIdentifying client)
        {
            var allObjects = client is ScriptableObject
                ? GetIdentifyingScriptableObjects().ToList()
                : GetIdentifyingObjectsInScene().ToList();

            allObjects.Remove(client);

            var allID = allObjects.Select(o => o.ID);

            return allID.Contains(id);
        }

        private static IEnumerable<IIdentifying> GetIdentifyingObjectsInScene()
        {
            Scene scene = SceneManager.GetActiveScene();

            if (scene.isLoaded == false)
                return Array.Empty<IIdentifying>();

            var objects = new List<IIdentifying>();

            var rootObjects = scene.GetRootGameObjects();

            foreach (var rootObject in rootObjects)
                objects.AddRange(rootObject.GetComponentsInChildren<IIdentifying>());

            return objects.Where(o => o != null);
        }

        private static IEnumerable<IIdentifying> GetIdentifyingScriptableObjects()
        {
            var result = Resources.LoadAll<ScriptableObject>("")
                .OfType<IIdentifying>();

            return result;
        }

        public static string GetOrderID(string id, IIdentifying client, int order = 0)
        {
            string result = order == 0 ? "" : order.ToString();

            if (HasDuplicateInProject(id + result, client))
                return GetOrderID(id, client, order + 1);

            return result;
        }
    }
}