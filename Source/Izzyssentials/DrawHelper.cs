﻿using UnityEngine;
using Verse;

namespace Izzyssentials
{
    public class DrawHelper
    {
        public static void DrawFillableBar(FillableBarRequest r)
        {
            var vector = r.preRotationOffset.RotatedBy(r.rotation.AsAngle);

            r.center += new Vector3(vector.x, 0f, vector.y);

            var s2 = new Vector3(r.size.x, 1f, r.size.y);
            var matrix2 = default(Matrix4x4);
            var pos2 = r.center + (Vector3.up * 0.009f);

            matrix2.SetTRS(pos2, r.rotation.AsQuat, s2);
            Graphics.DrawMesh(MeshPool.plane10, matrix2, r.matback, 0);


            if (!(r.fillPercent > 0.001f))
            {
                return;
            }

            var s = new Vector3(r.size.x * r.fillPercent, 1f, r.size.y);
            var matrix = default(Matrix4x4);
            var pos = r.center + (Vector3.up * 0.01f);

            if (r.rotation == Rot4.West) //North
            {
                pos.z = pos.z - (r.size.x * 0.5f);
                pos.z = pos.z + (0.5f * r.size.x * r.fillPercent);
            }

            if (r.rotation == Rot4.North) //East
            {
                pos.x = pos.x - (r.size.x * 0.5f);
                pos.x = pos.x + (0.5f * r.size.x * r.fillPercent);
            }

            if (r.rotation == Rot4.East) //South
            {
                pos.z = pos.z + (r.size.x * 0.5f);
                pos.z = pos.z - (0.5f * r.size.x * r.fillPercent);
            }

            if (r.rotation == Rot4.South) //West
            {
                pos.x = pos.x + (r.size.x * 0.5f);
                pos.x = pos.x - (0.5f * r.size.x * r.fillPercent);
            }

            matrix.SetTRS(pos, r.rotation.AsQuat, s);
            Graphics.DrawMesh(MeshPool.plane10, matrix, r.mat, 0);
        }

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
}