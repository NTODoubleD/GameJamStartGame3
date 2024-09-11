using System;
using UnityEngine;

namespace DoubleDCore.Extensions
{
    public static class VectorExtensions
    {
        public static Vector3 ToVector3(this Vector2 vector, CoordinatePlane plane = CoordinatePlane.XY)
        {
            return plane switch
            {
                CoordinatePlane.XY => new Vector3(vector.x, vector.y, 0),
                CoordinatePlane.XZ => new Vector3(vector.x, 0, vector.y),
                CoordinatePlane.YZ => new Vector3(0, vector.x, vector.y),
                _ => throw new ArgumentOutOfRangeException(nameof(plane), plane, null)
            };
        }
    }

    public enum CoordinatePlane
    {
        XY = 0,
        XZ = 1,
        YZ = 2
    }
}