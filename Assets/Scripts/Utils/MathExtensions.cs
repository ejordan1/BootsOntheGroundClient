using Data;
using FixMath.NET;
using UnityEngine;

namespace Utils
{
    public static class MathExtensions
    {
        public static WorldPosition FromHeading(float heading)
        {
            return new WorldPosition((Fix64) Mathf.Sin(heading * Mathf.Deg2Rad), (Fix64) Mathf.Cos(heading * Mathf.Deg2Rad));
        }

        public static float HeadingTo(Vector3 one, Vector3 two)
        {
            var deltaVector = (two - one).normalized;
            return Mathf.Atan2(deltaVector.x, deltaVector.z) * Mathf.Rad2Deg;
        }

        public static Vector3[] CreateFlatCircle(Vector3 center, float radius, int smoothing = 10, float angleOffset = 0)
        {
            var angleStep = Mathf.PI * 2 / smoothing;
            var shape = new Vector3[smoothing + 1];
            for (var i = 0; i < smoothing + 1; i++)
            {
                shape[i] = new Vector3(Mathf.Cos(angleStep * i + angleOffset) * radius + center.x, center.y,
                    Mathf.Sin(angleStep * i + angleOffset) * radius + center.z);
            }
            return shape;
        }
    }
}