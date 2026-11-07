using UnityEngine;
using Verse;

namespace Izzyssentials;

public static class DrawHelper
{
    public static void DrawRectangle(RectangleRequest r)
    {
        var vector = r.preRotationOffset.RotatedBy(r.rotation.AsAngle);
        r.center += new Vector3(vector.x, 0f, vector.y);

        var s = new Vector3(r.size.x, 1f, r.size.y);
        var matrix = default(Matrix4x4);
        var pos = r.center + (Vector3.up * 0.01f);

        matrix.SetTRS(pos, r.rotation.AsQuat, s);
        Graphics.DrawMesh(MeshPool.plane10, matrix, r.mat, 0);
    }

    public struct FillableBarRequest
    {
        public Vector3 center;
        public Vector2 size;
        public float fillPercent;
        public Material mat;
        public Material matback;
        public Rot4 rotation;
        public Vector2 preRotationOffset;
    }

    public struct RectangleRequest
    {
        public Vector3 center;
        public Vector2 size;
        public Material mat;
        public Rot4 rotation;
        public Vector2 preRotationOffset;
    }
}