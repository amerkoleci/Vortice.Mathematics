// Copyright (c) Amer Koleci and contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

using System.Numerics;

namespace Vortice.Mathematics;

/// <summary>
/// Defines extension methods for types in the numerics namespace.
/// </summary>
public static class NumericsExtensions
{
    public static Vector3 ToEuler(this in Quaternion rotation)
    {
        float xx = rotation.X * rotation.X;
        float yy = rotation.Y * rotation.Y;
        float zz = rotation.Z * rotation.Z;

        float m31 = 2.0f * rotation.X * rotation.Z + 2.0f * rotation.Y * rotation.W;
        float m32 = 2.0f * rotation.Y * rotation.Z - 2.0f * rotation.X * rotation.W;
        float m33 = 1.0f - 2.0f * xx - 2.0f * yy;

        float cy = MathF.Sqrt(m33 * m33 + m31 * m31);
        float cx = MathF.Atan2(-m32, cy);
        if (cy > 16.0f * float.Epsilon)
        {
            float m12 = 2.0f * rotation.X * rotation.Y + 2.0f * rotation.Z * rotation.W;
            float m22 = 1.0f - 2.0f * xx - 2.0f * zz;

            return new Vector3(cx, MathF.Atan2(m31, m33), MathF.Atan2(m12, m22));
        }
        else
        {
            float m11 = 1.0f - 2.0f * yy - 2.0f * zz;
            float m21 = 2.0f * rotation.X * rotation.Y - 2.0f * rotation.Z * rotation.W;

            return new Vector3(cx, 0.0f, MathF.Atan2(-m21, m11));
        }
    }

    public static Quaternion FromEuler(this in Vector3 value)
    {
        Quaternion rotation;

        Vector3 halfAngles = value * 0.5f;

        float fSinX = MathF.Sin(halfAngles.X);
        float fCosX = MathF.Cos(halfAngles.X);
        float fSinY = MathF.Sin(halfAngles.Y);
        float fCosY = MathF.Cos(halfAngles.Y);
        float fSinZ = MathF.Sin(halfAngles.Z);
        float fCosZ = MathF.Cos(halfAngles.Z);

        float fCosXY = fCosX * fCosY;
        float fSinXY = fSinX * fSinY;

        rotation.X = fSinX * fCosY * fCosZ - fSinZ * fSinY * fCosX;
        rotation.Y = fSinY * fCosX * fCosZ + fSinZ * fSinX * fCosY;
        rotation.Z = fSinZ * fCosXY - fSinXY * fCosZ;
        rotation.W = fCosZ * fCosXY + fSinXY * fSinZ;

        return rotation;
    }

    /// <summary>
    /// Defines a plane using a point and a normal.  Missing from System.Numeric.Plane
    /// </summary>
    /// <param name="pointOnPlane"><see cref="Vector3"/> is a point of a defined plane</param>
    /// <param name="planeNormal"><see cref="Vector3"/> is normal of the defined plane</param>
    public static Plane CreatePlane(in Vector3 pointOnPlane, in Vector3 planeNormal)
    {
        return new(planeNormal, -Vector3.Dot(planeNormal, pointOnPlane));
    }

    /// <summary>
    /// Defines a plane using a point and a normal.  Missing from System.Numeric.Plane
    /// </summary>
    /// <param name="pointOnPlane"><see cref="Vector3"/> is a point of a defined plane</param>
    /// <param name="planeNormal"><see cref="Vector3"/> is normal of the defined plane</param>
    /// <param name="result">A new <see cref="Plane"/> is defined based upon the input</param> 
    public static void CreatePlane(in Vector3 pointOnPlane, in Vector3 planeNormal, out Plane result)
    {
        result = new(planeNormal, -Vector3.Dot(planeNormal, pointOnPlane));
    }

    /// <summary>
    /// Creates a plane of unit length.
    /// </summary>
    /// <param name="normalX">The X component of the normal.</param>
    /// <param name="normalY">The Y component of the normal.</param>
    /// <param name="normalZ">The Z component of the normal.</param>
    /// <param name="planeD">The distance of the plane along its normal from the origin.</param>
    /// <param name="result">When the method completes, contains the normalized plane.</param>
    public static void NormalizePlane(float normalX, float normalY, float normalZ, float planeD, out Plane result)
    {
        float magnitude = 1.0f / MathF.Sqrt((normalX * normalX) + (normalY * normalY) + (normalZ * normalZ));

        result.Normal.X = normalX * magnitude;
        result.Normal.Y = normalY * magnitude;
        result.Normal.Z = normalZ * magnitude;
        result.D = planeD * magnitude;
    }

    public static Vector4 GetRow(in this Matrix4x4 matrix, int row) => new(matrix[row, 0], matrix[row, 1], matrix[row, 2], matrix[row, 3]);

    public static Vector4 GetColumn(in this Matrix4x4 matrix, int column) => new(matrix[0, column], matrix[1, column], matrix[2, column], matrix[3, column]);

    public static void SetRow(ref this Matrix4x4 matrix, int row, Vector4 value)
    {
        matrix[row, 0] = value.X;
        matrix[row, 1] = value.Y;
        matrix[row, 2] = value.Z;
        matrix[row, 3] = value.W;
    }

    public static void SetColumn(ref this Matrix4x4 matrix, int column, Vector4 value)
    {
        matrix[0, column] = value.X;
        matrix[1, column] = value.Y;
        matrix[2, column] = value.Z;
        matrix[3, column] = value.W;
    }


    public static void Deconstruct(this in Matrix4x4 matrix, out Vector3 scale, out Quaternion rotation, out Vector3 translation)
    {
        Matrix4x4.Decompose(matrix, out scale, out rotation, out translation);
    }
}
