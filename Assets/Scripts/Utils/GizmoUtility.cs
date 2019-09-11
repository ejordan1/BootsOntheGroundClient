using FixMath.NET;
using UnityEngine;

namespace Utils
{
    public static class GizmoUtility
    {
        public static void DrawArrow(Vector3 start, Vector3 end, Color color)
        {
            Gizmos.color = color;
            Gizmos.DrawLine(start, end);
            var heading = MathExtensions.HeadingTo(start, end);
            var left = Quaternion.Euler(0, heading - 160f, 0);
            var right = Quaternion.Euler(0, heading + 160f, 0);
            Gizmos.DrawLine(end, end + left * Vector3.forward * 0.1f);
            Gizmos.DrawLine(end, end + right * Vector3.forward * 0.1f);
        }

        public static void DrawCircle(Vector3 position, float range, Color color)
        {
#if UNITY_EDITOR
            var points = MathExtensions.CreateFlatCircle(position, range, 20);
            Gizmos.color = color;
            var prevPoint = points[points.Length - 1];
            foreach (var point in points)
            {
                Gizmos.DrawLine(prevPoint, point);
                prevPoint = point;
            }
#endif
        }
    }
}