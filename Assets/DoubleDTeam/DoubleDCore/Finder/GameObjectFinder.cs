using System.Collections.Generic;
using System.Diagnostics;
using DoubleDCore.Extensions;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace DoubleDCore.Finder
{
    public class GameObjectFinder : IGameObjectFinder
    {
        public TType[] Find<TType>()
        {
            Stopwatch s = new Stopwatch();
            s.Start();

            var result = new List<TType>();

            var scene = SceneManager.GetActiveScene();
            var rootObjects = scene.GetRootGameObjects();

            foreach (var gameObject in rootObjects)
                result.AddRange(gameObject.GetComponentsInChildren<TType>());

            s.Stop();

            UnityEngine.Debug.Log(
                $"{typeof(TType).Name} search took {s.ElapsedTicks / 10_000f} ms".Color(Color.gray));

            return result.ToArray();
        }
    }
}