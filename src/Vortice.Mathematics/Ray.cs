// Copyright (c) Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.
// This file includes code based on code from https://github.com/microsoft/DirectXMath
// The original code is Copyright © Microsoft. All rights reserved. Licensed under the MIT License (MIT).

using System.Diagnostics.CodeAnalysis;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Vortice.Mathematics;

/// <summary>
/// Defines a ray.
/// </summary>
[StructLayout(LayoutKind.Sequential, Pack = 4)]
public struct Ray : IEquatable<Ray>, IFormattable
{
    private const float RayEpsilon = 1e-20f;

    /// <summary>
    /// Initializes a new instance of the <see cref="Ray"/> struct.
    /// </summary>
    /// <param name="position">The position in three dimensional space of the origin of the ray.</param>
    /// <param name="direction">The normalized direction of the ray.</param>
    public Ray(in Vector3 position, in Vector3 direction)
    {
        Position = position;
        Direction = direction;
    }

    /// <summary>
    /// The position in three dimensional space where the ray starts.
    /// </summary>
    public Vector3 Position;

    /// <summary>
    /// The normalized direction in which the ray points.
    /// </summary>
    public Vector3 Direction;

    /// <summary>
    /// Checks whether the current <see cref="Ray"/> intersects with a specified <see cref="Vector3"/>.
    /// </summary>
    /// <param name="point">Point to test ray intersection</param>
    /// <returns></returns>
    public readonly bool Intersects(in Vector3 point)
    {
        //Source: RayIntersectsSphere
        //Reference: None

        Vector3 m = Vector3.Subtract(Position, point);

        //Same thing as RayIntersectsSphere except that the radius of the sphere (point)
        //is the epsilon for zero.
        float b = Vector3.Dot(m, Direction);
        float c = Vector3.Dot(m, m) - MathHelper.ZeroTolerance;

        if (c > 0f && b > 0f)
            return false;

        float discriminant = b * b - c;

        if (discriminant < 0f)
            return false;

        return true;
    }

    /// <summary>
    /// Checks whether the current <see cref="Ray"/> intersects with a specified <see cref="BoundingSphere"/>.
    /// </summary>
    /// <param name="sphere">The <see cref="BoundingSphere"/> to check for intersection with the current <see cref="Ray"/>.</param>
    /// <returns>Distance value if intersects, null otherwise.</returns>
    public readonly float? Intersects(in BoundingSphere sphere) => sphere.Intersects(in this);

    /// <summary>
    /// Checks whether the current <see cref="Ray"/> intersects with a specified <see cref="BoundingBox"/>.
    /// </summary>
    /// <param name="box">The <see cref="BoundingBox"/> to check for intersection with the current <see cref="Ray"/>.</param>
    /// <param name="result">Distance of normalised vector to intersection if >= 0 </param>
    /// <returns>bool returns true if intersection with plane</returns>
    public readonly bool Intersects(in BoundingBox box, out float result)
    {
        float? rs = box.Intersects(this);

        result = rs == null ? -1 : (float)rs;

        return result >= 0;
    }

    /// <summary>
    /// Checks whether the current <see cref="Ray"/> intersects with a specified <see cref="BoundingBox"/>.
    /// </summary>
    /// <param name="box">The <see cref="BoundingBox"/> to check for intersection with the current <see cref="Ray"/>.</param>
    /// <returns>Distance value if intersects, null otherwise.</returns>
    public readonly float? Intersects(in BoundingBox box) => box.Intersects(in this);

    /// <summary>
    /// Checks whether the current <see cref="Ray"/> intersects with a specified <see cref="Plane"/>.
    /// </summary>
    /// <param name="plane">The <see cref="Plane"/> to check for intersection with the current <see cref="Ray"/>.</param>
    /// <returns>Distance value if intersects, null otherwise.</returns>
    public readonly float? Intersects(in Plane plane)
    {
        //Source: Real-Time Collision Detection by Christer Ericson
        //Reference: Page 175

        float direction = Vector3.Dot(plane.Normal, Direction);

        if (Math.Abs(direction) < MathHelper.ZeroTolerance)
        {
            return null;
        }

        float position = Vector3.Dot(plane.Normal, Position);
        float distance = (-plane.D - position) / direction;

        if (distance < 0f)
        {
            if (distance < -MathHelper.ZeroTolerance)
            {
                return null;
            }

            distance = 0f;
        }

        return distance;
    }

    /// <summary>
    /// Checks whether the current <see cref="Ray"/> intersects with a specified <see cref="Plane"/>.
    /// </summary>
    /// <param name="plane">The <see cref="Plane"/> to check for intersection with the current <see cref="Ray"/>.</param>
    /// <param name="result">Distance of normalised vector to intersection if >= 0 </param>
    /// <returns>bool returns true if intersection with plane</returns>
    public readonly bool Intersects(in Plane plane, out float result)
    {
        float? rs = Intersects(plane);
        result = rs == null ? -1 : (float)rs;

        return result >= 0;
    }

    /// <inheritdoc/>
    public override readonly bool Equals([NotNullWhen(true)] object? obj) => obj is Ray value && Equals(value);

    /// <summary>
    /// Determines whether the specified <see cref="Ray"/> is equal to this instance.
    /// </summary>
    /// <param name="other">The <see cref="Int4"/> to compare with this instance.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly bool Equals(Ray other)
    {
        return Position.Equals(other.Position) && Direction.Equals(other.Direction);
    }

    /// <summary>
    /// Compares two <see cref="Ray"/> objects for equality.
    /// </summary>
    /// <param name="left">The <see cref="Ray"/> on the left hand of the operand.</param>
    /// <param name="right">The <see cref="Ray"/> on the right hand of the operand.</param>
    /// <returns>
    /// True if the current left is equal to the <paramref name="right"/> parameter; otherwise, false.
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator ==(Ray left, Ray right) => left.Equals(right);

    /// <summary>
    /// Determines if there is an intersection between the current object and a triangle.
    /// </summary>
    /// <param name="vertex1">Triangle Corner 1</param>
    /// <param name="vertex2">Triangle Corner 2</param>
    /// <param name="vertex3">Triangle Corner 3</param>
    /// <param name="distance">
    /// When the method completes, contains the distance of the intersection, or 0 if there was no intersection.</param>
    /// <returns>Whether the two objects intersected.</returns>
    public readonly bool Intersects(in Vector3 vertex1, in Vector3 vertex2, in Vector3 vertex3, out float distance)
    {
        //-----------------------------------------------------------------------------
        // Compute the intersection of a ray (Origin, Direction) with a triangle
        // (V0, V1, V2).  Return true if there is an intersection and also set *pDist
        // to the distance along the ray to the intersection.
        //
        // The algorithm is based on Moller, Tomas and Trumbore, "Fast, Minimum Storage
        // Ray-Triangle Intersection", Journal of Graphics Tools, vol. 2, no. 1,
        // pp 21-28, 1997.
        //-----------------------------------------------------------------------------

        Vector3 edge1 = Vector3.Subtract(vertex2, vertex1);
        Vector3 edge2 = Vector3.Subtract(vertex3, vertex1);

        // p = Direction ^ e2;
        Vector3 directionCrossEdge2 = Vector3.Cross(Direction, edge2);

        // det = e1 * p;
        float determinant = Vector3.Dot(edge1, directionCrossEdge2);

        //If the ray is parallel to the triangle plane, there is no collision.
        //This also means that we are not culling, the ray may hit both the
        //back and the front of the triangle.
        if (determinant > -RayEpsilon && determinant < RayEpsilon)
        {
            distance = 0f;
            return false;
        }

        float inverseDeterminant = 1.0f / determinant;

        // Calculate the U parameter of the intersection point.
        Vector3 distanceVector;
        distanceVector.X = Position.X - vertex1.X;
        distanceVector.Y = Position.Y - vertex1.Y;
        distanceVector.Z = Position.Z - vertex1.Z;

        float triangleU;
        triangleU = (distanceVector.X * directionCrossEdge2.X) + (distanceVector.Y * directionCrossEdge2.Y) + (distanceVector.Z * directionCrossEdge2.Z);
        triangleU *= inverseDeterminant;

        // Make sure it is inside the triangle.
        if (triangleU < 0.0f || triangleU > 1.0f)
        {
            distance = 0.0f;
            return false;
        }

        // Calculate the V parameter of the intersection point.
        Vector3 distanceCrossEdge1;
        distanceCrossEdge1.X = (distanceVector.Y * edge1.Z) - (distanceVector.Z * edge1.Y);
        distanceCrossEdge1.Y = (distanceVector.Z * edge1.X) - (distanceVector.X * edge1.Z);
        distanceCrossEdge1.Z = (distanceVector.X * edge1.Y) - (distanceVector.Y * edge1.X);

        float triangleV;
        triangleV = ((Direction.X * distanceCrossEdge1.X) + (Direction.Y * distanceCrossEdge1.Y)) + (Direction.Z * distanceCrossEdge1.Z);
        triangleV *= inverseDeterminant;

        // Make sure it is inside the triangle.
        if (triangleV < 0.0f || triangleU + triangleV > 1.0f)
        {
            distance = 0.0f;
            return false;
        }

        // Compute the distance along the ray to the triangle.
        float rayDistance = (edge2.X * distanceCrossEdge1.X) + (edge2.Y * distanceCrossEdge1.Y) + (edge2.Z * distanceCrossEdge1.Z);
        rayDistance *= inverseDeterminant;

        //Is the triangle behind the ray origin?
        if (rayDistance < 0.0f)
        {
            distance = 0.0f;
            return false;
        }

        distance = rayDistance;
        return true;
    }

    /// <summary>
    /// Determines if there is an intersection between the current object and a triangle.
    /// </summary>
    /// <param name="vertex1">Triangle Corner 1</param>
    /// <param name="vertex2">Triangle Corner 2</param>
    /// <param name="vertex3">Triangle Corner 3</param>
    /// <param name="point">Intersection point if boolean returns true, of Vector3.Zero if false.</param>
    /// <returns></returns>
    public readonly bool Intersects(in Vector3 vertex1, in Vector3 vertex2, in Vector3 vertex3, out Vector3 point)
    {
        if (!Intersects(in vertex1, in vertex2, in vertex3, out float distance))
        {
            point = Vector3.Zero;
            return false;
        }

        point = Position + (Direction * distance);
        return true;
    }

    /// <summary>
    /// Compares two <see cref="Ray"/> objects for inequality.
    /// </summary>
    /// <param name="left">The <see cref="Ray"/> on the left hand of the operand.</param>
    /// <param name="right">The <see cref="Ray"/> on the right hand of the operand.</param>
    /// <returns>
    /// True if the current left is unequal to the <paramref name="right"/> parameter; otherwise, false.
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator !=(Ray left, Ray right) => !left.Equals(right);

    /// <inheritdoc/>
	public override readonly int GetHashCode()
    {
        var hashCode = new HashCode();
        {
            hashCode.Add(Position);
            hashCode.Add(Direction);
        }
        return hashCode.ToHashCode();
    }

    /// <inheritdoc />
    public override string ToString() => ToString(format: null, formatProvider: null);

    /// <inheritdoc />
    public readonly string ToString(string? format, IFormatProvider? formatProvider)
    {
        return $"Position:{Position.ToString(format, formatProvider)} Direction:{Direction.ToString(format, formatProvider)}";
    }
}
