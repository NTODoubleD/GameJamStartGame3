using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace DoubleDCore.Extensions
{
    public static class SceneManagerExtensions
    {
        public static IEnumerable<T> GetComponentsInScene<T>() where T : Component
        {
            Scene scene = SceneManager.GetActiveScene();

            if (scene.isLoaded == false)
                return Array.Empty<T>();

            return scene.GetRootGameObjects()
                .Select(g => g.GetComponent<T>())
                .Where(g => g != null);
        }
    }
}