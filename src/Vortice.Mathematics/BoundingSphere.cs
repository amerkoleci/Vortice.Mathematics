// Copyright (c) Amer Koleci and contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

// Copyright © Tanner Gooding and Contributors. Licensed under the MIT License (MIT). See License.md in the repository root for more information.

// This file includes code based on code from https://github.com/microsoft/DirectXMath
// The original code is Copyright © Microsoft. All rights reserved. Licensed under the MIT License (MIT).

using System.Numerics;
using System.Runtime.CompilerServices;

namespace Vortice.Mathematics;

/// <summary>
/// Defines an sphere in three dimensional space.
/// </summary>
public struct BoundingSphere : IEquatable<BoundingSphere>, IFormattable
{
    /// <summary>
    /// Gets a bounding sphere with zero radius.
    /// </summary>
    public static BoundingSphere Zero => new(Vector3.Zero, 0.0f);

    private Vector3 _center;
    private float _radius;

    /// <summary>
    /// Initializes a new instance of the <see cref="BoundingSphere"/> struct.
    /// </summary>
    /// <param name="center">The center of the sphere.</param>
    /// <param name="radius">The radius of the sphere.</param>
    public BoundingSphere(Vector3 center, float radius)
    {
        _center = center;
        _radius = radius;
    }

    /// <summary>
    /// Gets or sets the center of the bounding sphere.
    /// </summary>
    public Vector3 Center
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        readonly get => _center;
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        set => _center = value;
    }

    /// <summary>
    /// Gets or sets the radius of the bounding sphere.
    /// </summary>
    public float Radius
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        readonly get => _radius;
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        set => _radius = value;
    }

    public static BoundingSphere CreateFromPoints(Vector3[] points)
    {
        Span<Vector3> span = [.. points];
        return CreateFromPoints(span);
    }

    public static BoundingSphere CreateFromPoints(Span<Vector3> points)
    {
        Vector3 MinX, MaxX, MinY, MaxY, MinZ, MaxZ;
        MinX = MaxX = MinY = MaxY = MinZ = MaxZ = points[0];

        for (int i = 0; i < points.Length; ++i)
        {
            Vector3 point = points[i];
            if (point.X < MinX.X)
                MinX = point;

            if (point.X > MaxX.X)
                MaxX = point;

            if (point.Y < MinY.Y)
                MinY = point;

            if (point.Y > MaxY.Y)
                MaxY = point;

            if (point.Z < MinZ.Z)
                MinZ = point;

            if (point.Z > MaxZ.Z)
                MaxZ = point;
        }

        // Use the min/max pair that are farthest apart to form the initial sphere.
        Vector3 DeltaX = Vector3.Subtract(MaxX, MinX);
        Vector3 DeltaY = Vector3.Subtract(MaxY, MinY);
        Vector3 DeltaZ = Vector3.Subtract(MaxZ, MinZ);

        float DistX = DeltaX.Length();
        float DistY = DeltaY.Length();
        float DistZ = DeltaZ.Length();

        Vector3 center;
        float radius;

        if (DistX > DistY)
        {
            if (DistX > DistZ)
            {
                // Use min/max x.
                center = Vector3.Lerp(MaxX, MinX, 0.5f);
                radius = DistX * 0.5f;
            }
            else
            {
                // Use min/max z.
                center = Vector3.Lerp(MaxZ, MinZ, 0.5f);
                radius = DistZ * 0.5f;
            }
        }
        else // Y >= X
        {
            if (DistY > DistZ)
            {
                // Use min/max y.
                center = Vector3.Lerp(MaxY, MinY, 0.5f);
                radius = DistY * 0.5f;
            }
            else
            {
                // Use min/max z.
                center = Vector3.Lerp(MaxZ, MinZ, 0.5f);
                radius = DistZ * 0.5f;
            }
        }

        // Add any points not inside the sphere.
        for (int i = 0; i < points.Length; ++i)
        {
            Vector3 point = points[i];

            Vector3 Delta = Vector3.Subtract(point, center);
            float Dist = Delta.Length();

            if (Dist > radius)
            {
                // Adjust sphere to include the new point.
                radius = (radius + Dist) * 0.5f;
                center += (1.0f - (radius / Dist)) * Delta;
            }
        }

        return new BoundingSphere(center, radius);
    }

    /// <summary>
    /// Creates the smallest <see cref="BoundingSphere"/> that can contain a specified <see cref="BoundingBox"/>.
    /// </summary>
    /// <param name="box">The <see cref="BoundingBox"/> to create the <see cref="BoundingSphere"/> from.</param>
    /// <returns>The created <see cref="BoundingSphere"/>.</returns>
    public static BoundingSphere CreateFromBoundingBox(in BoundingBox box)
    {
        BoundingSphere result;

        result._center = Vector3.Lerp(box.Min, box.Max, 0.5f);
        result._radius = Vector3.Distance(box.Min, box.Max) * 0.5f;

        return result;
    }

    public static BoundingSphere CreateMerged(in BoundingSphere original, in BoundingSphere additional)
    {
        Vector3 center1 = original._center;
        float r1 = original._radius;

        Vector3 center2 = additional._center;
        float r2 = additional._radius;

        Vector3 V = Vector3.Subtract(center2, center1);

        float d = V.Length();

        if (r1 + r2 >= d)
        {
            if (r1 - r2 >= d)
            {
                return original;
            }
            else if (r2 - r1 >= d)
            {
                return additional;
            }
        }

        Vector3 N = Vector3.Divide(V, d);

        float t1 = MathF.Min(-r1, d - r2);
        float t2 = MathF.Max(r1, d + r2);
        float t_5 = (t2 - t1) * 0.5f;

        Vector3 NCenter = Vector3.Add(center1, N * (t_5 + t1));
        return new(NCenter, t_5);
    }

    /// <summary>
    /// Translates and scales given <see cref="BoundingSphere"/> using a given <see cref="Matrix4x4"/>.
    /// </summary>
    /// <param name="sphere">The source <see cref="BoundingSphere"/>.</param>
    /// <param name="transform">A transformation matrix that might include translation, rotation, or uniform scaling.</param>
    /// <returns>The transformed BoundingSphere.</returns>
    public static BoundingSphere Transform(in BoundingSphere sphere, in Matrix4x4 transform)
    {
        Transform(sphere, transform, out BoundingSphere result);
        return result;
    }

    /// <summary>
    /// Translates and scales given <see cref="BoundingSphere"/> using a given <see cref="Matrix4x4"/>.
    /// </summary>
    /// <param name="sphere">The source <see cref="BoundingSphere"/>.</param>
    /// <param name="transform">A transformation matrix that might include translation, rotation, or uniform scaling.</param>
    /// <param name="result">The transformed BoundingSphere.</param>
    public static void Transform(in BoundingSphere sphere, in Matrix4x4 transform, out BoundingSphere result)
    {
        Vector3 center = Vector3.Transform(sphere.Center, transform);

        float majorAxisLengthSquared = Math.Max(
           (transform.M11 * transform.M11) + (transform.M12 * transform.M12) + (transform.M13 * transform.M13), Math.Max(
           (transform.M21 * transform.M21) + (transform.M22 * transform.M22) + (transform.M23 * transform.M23),
           (transform.M31 * transform.M31) + (transform.M32 * transform.M32) + (transform.M33 * transform.M33)));

        float radius = sphere.Radius * (float)Math.Sqrt(majorAxisLengthSquared);
        result = new BoundingSphere(center, radius);
    }

    public readonly ContainmentType Contains(in Vector3 point)
    {
        if (Vector3.DistanceSquared(point, Center) <= Radius * Radius)
        {
            return ContainmentType.Contains;
        }

        return ContainmentType.Disjoint;
    }

    public readonly ContainmentType Contains(in BoundingBox box)
    {
        return box.Contains(this);
    }

    public readonly ContainmentType Contains(in BoundingSphere sphere)
    {
        float distance = Vector3.Distance(Center, sphere.Center);

        if (Radius + sphere.Radius < distance)
            return ContainmentType.Disjoint;

        if (Radius - sphere.Radius < distance)
            return ContainmentType.Intersects;

        return ContainmentType.Contains;
    }

    public readonly float? Intersects(in Ray ray)
    {
        //Source: Real-Time Collision Detection by Christer Ericson
        //Reference: Page 177

        Vector3 m = Vector3.Subtract(ray.Position, Center);

        float b = Vector3.Dot(m, ray.Direction);
        float c = Vector3.Dot(m, m) - (Radius * Radius);

        if (c > 0f && b > 0f)
        {
            return null;
        }

        float discriminant = b * b - c;

        if (discriminant < 0f)
        {
            return null;
        }

        float distance = -b - (float)Math.Sqrt(discriminant);

        if (distance < 0f)
            distance = 0f;

        return distance;
    }

    /// <summary>
    /// Checks whether the current <see cref="BoundingSphere"/> intersects with a specified <see cref="BoundingBox"/>.
    /// </summary>
    /// <param name="box">The <see cref="BoundingBox"/> to check for intersection with the current <see cref="BoundingSphere"/>.</param>
    /// <returns>True if intersects, false otherwise.</returns>
    public readonly bool Intersects(in BoundingBox box)
    {
        Vector3 clampedVector = Vector3.Clamp(Center, box.Min, box.Max);
        float distance = Vector3.DistanceSquared(Center, clampedVector);
        return distance <= Radius * Radius;
    }

    /// <summary>
    /// Checks whether the current <see cref="BoundingSphere"/> intersects with a specified <see cref="BoundingBox"/>.
    /// </summary>
    /// <param name="sphere">The <see cref="BoundingSphere"/> to check for intersection with the current <see cref="BoundingSphere"/>.</param>
    /// <returns>True if intersects, false otherwise.</returns>
    public readonly bool Intersects(in BoundingSphere sphere)
    {
        float radiisum = Radius + sphere.Radius;
        return Vector3.DistanceSquared(Center, sphere.Center) <= radiisum * radiisum;
    }

    /// <summary>
    /// Checks whether the current <see cref="BoundingSphere"/> intersects with a specified <see cref="BoundingBox"/>.
    /// </summary>
    /// <param name="plane">The <see cref="Plane"/> to check for intersection with the current <see cref="Plane"/>.</param>
    /// <returns>True if intersects, false otherwise.</returns>
    public readonly PlaneIntersectionType Intersects(in Plane plane)
    {
        //Source: Real-Time Collision Detection by Christer Ericson
        //Reference: Page 160

        float distance = Vector3.Dot(plane.Normal, Center);
        distance += plane.D;

        if (distance > Radius)
            return PlaneIntersectionType.Front;

        if (distance < -Radius)
            return PlaneIntersectionType.Back;

        return PlaneIntersectionType.Intersecting;
    }

    /// <inheritdoc/>
    public override readonly bool Equals(object? obj) => (obj is BoundingSphere other) && Equals(other);

    /// <summary>
    /// Determines whether the specified <see cref="BoundingSphere"/> is equal to this instance.
    /// </summary>
    /// <param name="other">The <see cref="Int4"/> to compare with this instance.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly bool Equals(BoundingSphere other)
    {
        return _center.Equals(other._center)
            && _radius.Equals(other._radius);
    }

    /// <summary>
    /// Compares two <see cref="BoundingSphere"/> objects for equality.
    /// </summary>
    /// <param name="left">The <see cref="BoundingSphere"/> on the left hand of the operand.</param>
    /// <param name="right">The <see cref="BoundingSphere"/> on the right hand of the operand.</param>
    /// <returns>
    /// True if the current left is equal to the <paramref name="right"/> parameter; otherwise, false.
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator ==(BoundingSphere left, BoundingSphere right)
    {
        return (left._center == right._center)
            && (left._radius == right._radius);
    }

    /// <summary>
    /// Compares two <see cref="BoundingSphere"/> objects for inequality.
    /// </summary>
    /// <param name="left">The <see cref="BoundingSphere"/> on the left hand of the operand.</param>
    /// <param name="right">The <see cref="BoundingSphere"/> on the right hand of the operand.</param>
    /// <returns>
    /// True if the current left is unequal to the <paramref name="right"/> parameter; otherwise, false.
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator !=(BoundingSphere left, BoundingSphere right)
    {
        return (left._center != right._center)
            || (left._radius != right._radius);
    }

    /// <inheritdoc/>
    public override int GetHashCode() => HashCode.Combine(_center, _radius);

    /// <inheritdoc />
    public override string ToString() => ToString(format: null, formatProvider: null);

    /// <inheritdoc />
    public readonly string ToString(string? format, IFormatProvider? formatProvider)
    {
        return $"{nameof(BoundingSphere)} {{ {nameof(Center)} = {_center.ToString(format, formatProvider)}, {nameof(Radius)} = {_radius.ToString(format, formatProvider)} }}";
    }
}
