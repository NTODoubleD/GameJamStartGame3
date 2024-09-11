using UnityEngine;

namespace DoubleDCore.Extensions
{
    public static class GameObjectExtensions
    {
        public static GameObject SetTransform(this GameObject gameObject,
            Vector3 position)
        {
            gameObject.transform.position = position;
            return gameObject;
        }

        public static GameObject SetTransform(this GameObject gameObject,
            Vector3 position, Quaternion rotation)
        {
            gameObject.transform.position = position;
            gameObject.transform.rotation = rotation;

            return gameObject;
        }

        public static GameObject SetTransform(this GameObject gameObject,
            Vector3 position, Quaternion rotation, Transform parent)
        {
            gameObject.transform.position = position;
            gameObject.transform.rotation = rotation;
            gameObject.transform.SetParent(parent);

            return gameObject;
        }

        public static GameObject SetTransform(this GameObject gameObject,
            Vector3 position, Quaternion rotation, Vector3 scale, Transform parent)
        {
            gameObject.transform.position = position;
            gameObject.transform.rotation = rotation;
            gameObject.transform.localScale = scale;
            gameObject.transform.SetParent(parent);

            return gameObject;
        }
    }
}