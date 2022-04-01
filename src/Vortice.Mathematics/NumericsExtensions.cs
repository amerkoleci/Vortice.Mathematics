// Copyright (c) Amer Koleci and contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

namespace Vortice.Mathematics;

/// <summary>
/// Defines extension methods for types in the numerics namespace.
/// </summary>
public static class NumericsExtensions
{
    public static Vector3 ToEuler(this in Quaternion rotation)
    {
        Vector3 rotationEuler;

        float xx = rotation.X * rotation.X;
        float yy = rotation.Y * rotation.Y;
        float zz = rotation.Z * rotation.Z;
        float xy = rotation.X * rotation.Y;
        float zw = rotation.Z * rotation.W;
        float zx = rotation.Z * rotation.X;
        float yw = rotation.Y * rotation.W;
        float yz = rotation.Y * rotation.Z;
        float xw = rotation.X * rotation.W;

        rotationEuler.Y = (float)Math.Asin(2.0f * (yw - zx));
        double test = Math.Cos(rotationEuler.Y);

        if (test > 1e-6f)
        {
            rotationEuler.Z = (float)Math.Atan2(2.0f * (xy + zw), 1.0f - (2.0f * (yy + zz)));
            rotationEuler.X = (float)Math.Atan2(2.0f * (yz + xw), 1.0f - (2.0f * (yy + xx)));
        }
        else
        {
            rotationEuler.Z = (float)Math.Atan2(2.0f * (zw - xy), 2.0f * (zx + yw));
            rotationEuler.X = 0.0f;
        }

        return rotationEuler;
    }

    public static Quaternion FromEuler(this in Vector3 value)
    {
        Quaternion rotation;

        var halfAngles = value * 0.5f;

        var fSinX = (float)Math.Sin(halfAngles.X);
        var fCosX = (float)Math.Cos(halfAngles.X);
        var fSinY = (float)Math.Sin(halfAngles.Y);
        var fCosY = (float)Math.Cos(halfAngles.Y);
        var fSinZ = (float)Math.Sin(halfAngles.Z);
        var fCosZ = (float)Math.Cos(halfAngles.Z);

        var fCosXY = fCosX * fCosY;
        var fSinXY = fSinX * fSinY;

        rotation.X = fSinX * fCosY * fCosZ - fSinZ * fSinY * fCosX;
        rotation.Y = fSinY * fCosX * fCosZ + fSinZ * fSinX * fCosY;
        rotation.Z = fSinZ * fCosXY - fSinXY * fCosZ;
        rotation.W = fCosZ * fCosXY + fSinXY * fSinZ;

        return rotation;
    }

    /// <summary>
    /// Defines a plane using a point and a normal.  Missing from System.Numeric.Plane
    /// </summary>
    /// <param name="pointonplane"><see cref="Vector3"/> is a point of a defined plane</param>
    /// <param name="planenormal"><see cref="Vector3"/> is normal of the defined plane</param>
    public static Plane CreatePlane(Vector3 pointonplane, Vector3 planenormal)
    {
        return new Plane(
            planenormal,
            -Vector3.Dot(planenormal, pointonplane));
    }

    /// <summary>
    /// Defines a plane using a point and a normal.  Missing from System.Numeric.Plane
    /// </summary>
    /// <param name="pointonplane"><see cref="Vector3"/> is a point of a defined plane</param>
    /// <param name="planenormal"><see cref="Vector3"/> is normal of the defined plane</param>
    /// <param name="result">A new <see cref="Plane"/> is defined based upon the input</param> 
    public static void CreatePlane(Vector3 pointonplane, Vector3 planenormal, out Plane result)
    {
        result = new Plane(
            pointonplane,
            -Vector3.Dot(planenormal, pointonplane)
            );
    }
}
