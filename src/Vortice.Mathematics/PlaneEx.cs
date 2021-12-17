// Copyright (c) Amer Koleci and contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

using System.Numerics;

namespace Vortice.Mathematics;

/// <summary>
/// Represents extensions methods for <see cref="Plane"/>.
/// </summary>
public static class PlaneEx
{
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
