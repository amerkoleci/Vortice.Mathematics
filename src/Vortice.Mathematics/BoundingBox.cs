// Copyright (c) Amer Koleci and contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

// Copyright © Tanner Gooding and Contributors. Licensed under the MIT License (MIT). See License.md in the repository root for more information.

// This file includes code based on code from https://github.com/microsoft/DirectXMath
// The original code is Copyright © Microsoft. All rights reserved. Licensed under the MIT License (MIT).

using System.Numerics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Vortice.Mathematics;

/// <summary>
/// Defines an axis-aligned box-shaped 3D volume.
/// </summary>
[StructLayout(LayoutKind.Sequential, Pack = 4)]
public struct BoundingBox : IEquatable<BoundingBox>, IFormattable
{
    /// <summary>
    /// Specifies the total number of corners (8) in the BoundingBox.
    /// </summary>
    public const int CornerCount = 8;

    /// <summary>
    /// A <see cref="BoundingBox"/> which represents an empty space.
    /// </summary>
    public static BoundingBox Zero => new(Vector3.Zero, Vector3.Zero);

    private Vector3 _min;
    private Vector3 _max;

    /// <summary>
    /// Initializes a new instance of the <see cref="BoundingBox"/> struct.
    /// </summary>
    /// <param name="min">The minimum vertex of the bounding box.</param>
    /// <param name="max">The maximum vertex of the bounding box.</param>
    public BoundingBox(Vector3 min, Vector3 max)
    {
        _min = min;
        _max = max;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="BoundingBox"/> struct.
    /// </summary>
    /// <param name="sphere">The <see cref="BoundingSphere"/> to initialize from.</param>
    public BoundingBox(in BoundingSphere sphere)
    {
        _min = new Vector3(sphere.Center.X - sphere.Radius, sphere.Center.Y - sphere.Radius, sphere.Center.Z - sphere.Radius);
        _max = new Vector3(sphere.Center.X + sphere.Radius, sphere.Center.Y + sphere.Radius, sphere.Center.Z + sphere.Radius);
    }

    /// <summary>
    /// The minimum point of the box.
    /// </summary>
    public Vector3 Min
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        readonly get => _min;
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        set => _min = value;
    }

    /// <summary>
    /// The maximum point of the box.
    /// </summary>
    public Vector3 Max
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        readonly get => _max;
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        set => _max = value;
    }

    /// <summary>
    /// Gets the center of this bouding box.
    /// </summary>
    public readonly Vector3 Center
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => (_min + _max) / 2;
    }

    /// <summary>
    /// Gets the extent of this bouding box.
    /// </summary>
    public readonly Vector3 Extent
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => (_max - _min) / 2;
    }

    /// <summary>
    /// Gets size  of this bouding box.
    /// </summary>
    public readonly Vector3 Size => _max - _min;

    /// <summary>
    /// Gets the width of the bounding box.
    /// </summary>
    public readonly float Width
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => Extent.X * 2.0f;
    }

    /// <summary>
    /// Gets the height of the bounding box.
    /// </summary>
    public readonly float Height
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => Extent.Y * 2.0f;

    }

    /// <summary>
    /// Gets the depth of the bounding box.
    /// </summary>
    public readonly float Depth
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => Extent.Z * 2.0f;
    }

    /// <summary>
    /// Gets the volume of the bounding box.
    /// </summary>
    public readonly float Volume
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get
        {
            Vector3 sides = _max - _min;
            return sides.X * sides.Y * sides.Z;
        }
    }

    /// <summary>
    /// Get the perimeter length.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly float GetPerimeter()
    {
        Vector3 sides = _max - _min;
        return 4 * (sides.X + sides.Y + sides.Z);
    }

    /// <summary>
    /// Get the surface area.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly float GetSurfaceArea()
    {
        Vector3 sides = _max - _min;
        return 2 * (sides.X * sides.Y + sides.X * sides.Z + sides.Y * sides.Z);
    }

    public static BoundingBox CreateFromPoints(Vector3[] points)
    {
        ReadOnlySpan<Vector3> span = points.AsSpan();
        return CreateFromPoints(span);
    }

    public static BoundingBox CreateFromPoints(ReadOnlySpan<Vector3> points)
    {
        Vector3 min = new(float.MaxValue);
        Vector3 max = new(float.MinValue);

        for (int i = 0; i < points.Length; ++i)
        {
            min = Vector3.Min(min, points[i]);
            max = Vector3.Max(max, points[i]);
        }

        return new BoundingBox(min, max);
    }

    public static BoundingBox CreateMerged(in BoundingBox original, in BoundingBox additional)
    {
        return new BoundingBox(
            Vector3.Min(original.Min, additional.Min),
            Vector3.Max(original.Max, additional.Max)
        );
    }

    /// <summary>
    /// Transforms given <see cref="BoundingBox"/> using a given <see cref="Matrix4x4"/>.
    /// </summary>
    /// <param name="box">The source <see cref="BoundingBox"/>.</param>
    /// <param name="transform">A transformation matrix that might include translation, rotation, or uniform scaling.</param>
    /// <returns>The transformed BoundingBox.</returns>
    public static BoundingBox Transform(in BoundingBox box, in Matrix4x4 transform)
    {
        Transform(box, transform, out BoundingBox result);
        return result;
    }

    /// <summary>
    /// Transforms given <see cref="BoundingBox"/> using a given <see cref="Matrix4x4"/>.
    /// </summary>
    /// <param name="box">The source <see cref="BoundingBox"/>.</param>
    /// <param name="transform">A transformation matrix that might include translation, rotation, or uniform scaling.</param>
    /// <param name="result">The transformed BoundingBox.</param>
    public static void Transform(in BoundingBox box, in Matrix4x4 transform, out BoundingBox result)
    {
        Vector3 newCenter = Vector3.Transform(box.Center, transform);
        Vector3 oldEdge = box.Size * 0.5f;

        Vector3 newEdge = new(
            MathHelper.Abs(transform.M11) * oldEdge.X + MathHelper.Abs(transform.M12) * oldEdge.Y + MathHelper.Abs(transform.M13) * oldEdge.Z,
            MathHelper.Abs(transform.M21) * oldEdge.X + MathHelper.Abs(transform.M22) * oldEdge.Y + MathHelper.Abs(transform.M23) * oldEdge.Z,
            MathHelper.Abs(transform.M31) * oldEdge.X + MathHelper.Abs(transform.M32) * oldEdge.Y + MathHelper.Abs(transform.M33) * oldEdge.Z
        ); 

        result = new(newCenter - newEdge, newCenter + newEdge);
    }

    /// <summary>
    /// Retrieves the eight corners of the bounding box.
    /// </summary>
    /// <returns>An array of points representing the eight corners of the bounding box.</returns>
    public readonly Vector3[] GetCorners()
    {
        Vector3[] results = new Vector3[CornerCount];
        GetCorners(results);
        return results;
    }

    /// <summary>
    /// Retrieves the eight corners of the bounding box.
    /// </summary>
    /// <returns>An array of points representing the eight corners of the bounding box.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly void GetCorners(Vector3[] corners)
    {
        ArgumentNullException.ThrowIfNull(corners);

        if (corners.Length < CornerCount)
        {
            throw new ArgumentOutOfRangeException(nameof(corners), $"GetCorners need at least {CornerCount} elements to copy corners.");
        }

        corners[0] = new Vector3(_min.X, _max.Y, _max.Z);
        corners[1] = new Vector3(_max.X, _max.Y, _max.Z);
        corners[2] = new Vector3(_max.X, _min.Y, _max.Z);
        corners[3] = new Vector3(_min.X, _min.Y, _max.Z);
        corners[4] = new Vector3(_min.X, _max.Y, _min.Z);
        corners[5] = new Vector3(_max.X, _max.Y, _min.Z);
        corners[6] = new Vector3(_max.X, _min.Y, _min.Z);
        corners[7] = new Vector3(_min.X, _min.Y, _min.Z);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly void GetCorners(Span<Vector3> corners)
    {
        if (corners.Length < CornerCount)
        {
            throw new ArgumentOutOfRangeException(nameof(corners), $"GetCorners need at least {CornerCount} elements to copy corners.");
        }

        corners[0] = new Vector3(_min.X, _max.Y, _max.Z);
        corners[1] = new Vector3(_max.X, _max.Y, _max.Z);
        corners[2] = new Vector3(_max.X, _min.Y, _max.Z);
        corners[3] = new Vector3(_min.X, _min.Y, _max.Z);
        corners[4] = new Vector3(_min.X, _max.Y, _min.Z);
        corners[5] = new Vector3(_max.X, _max.Y, _min.Z);
        corners[6] = new Vector3(_max.X, _min.Y, _min.Z);
        corners[7] = new Vector3(_min.X, _min.Y, _min.Z);
    }

    public ContainmentType Contains(in Vector3 point)
    {
        if (Min.X <= point.X && Max.X >= point.X &&
            Min.Y <= point.Y && Max.Y >= point.Y &&
            Min.Z <= point.Z && Max.Z >= point.Z)
        {
            return ContainmentType.Contains;
        }

        return ContainmentType.Disjoint;
    }

    public ContainmentType Contains(in BoundingBox box)
    {
        if (Max.X < box.Min.X || Min.X > box.Max.X)
            return ContainmentType.Disjoint;

        if (Max.Y < box.Min.Y || Min.Y > box.Max.Y)
            return ContainmentType.Disjoint;

        if (Max.Z < box.Min.Z || Min.Z > box.Max.Z)
            return ContainmentType.Disjoint;

        if (Min.X <= box.Min.X && (box.Max.X <= Max.X &&
            Min.Y <= box.Min.Y && box.Max.Y <= Max.Y) &&
            Min.Z <= box.Min.Z && box.Max.Z <= Max.Z)
        {
            return ContainmentType.Contains;
        }

        return ContainmentType.Intersects;
    }

    public ContainmentType Contains(in BoundingSphere sphere)
    {
        Vector3 vector = Vector3.Clamp(sphere.Center, Min, Max);
        float distance = Vector3.DistanceSquared(sphere.Center, vector);

        if (distance > sphere.Radius * sphere.Radius)
            return ContainmentType.Disjoint;

        if (((Min.X + sphere.Radius <= sphere.Center.X) && (sphere.Center.X <= Max.X - sphere.Radius) && (Max.X - Min.X > sphere.Radius)) &&
           ((Min.Y + sphere.Radius <= sphere.Center.Y) && (sphere.Center.Y <= Max.Y - sphere.Radius) && (Max.Y - Min.Y > sphere.Radius)) &&
           ((Min.Z + sphere.Radius <= sphere.Center.Z) && (sphere.Center.Z <= Max.Z - sphere.Radius) && (Max.Z - Min.Z > sphere.Radius)))
        {
            return ContainmentType.Contains;
        }

        return ContainmentType.Intersects;
    }

    /// <summary>
    /// Checks whether the current <see cref="BoundingBox"/> intersects with a specified <see cref="BoundingBox"/>.
    /// </summary>
    /// <param name="box">The other <see cref="BoundingBox"/> to check.</param>
    /// <returns>True if intersects, false otherwise.</returns>
    public bool Intersects(in BoundingBox box)
    {
        if (Max.X < box.Min.X || Min.X > box.Max.X)
        {
            return false;
        }

        if (Max.Y < box.Min.Y || Min.Y > box.Max.Y)
        {
            return false;
        }

        if (Max.Z < box.Min.Z || Min.Z > box.Max.Z)
        {
            return false;
        }

        return true;
    }

    /// <summary>
    /// Checks whether the current <see cref="BoundingBox"/> intersects with a specified <see cref="BoundingSphere"/>.
    /// </summary>
    /// <param name="sphere">The <see cref="BoundingSphere"/> to check for intersection with the current <see cref="BoundingBox"/>.</param>
    /// <returns>True if intersects, false otherwise.</returns>
    public bool Intersects(in BoundingSphere sphere)
    {
        Vector3 clampedVector = Vector3.Clamp(sphere.Center, Min, Max);
        float distance = Vector3.DistanceSquared(sphere.Center, clampedVector);
        return distance <= sphere.Radius * sphere.Radius;
    }

    /// <summary>
    /// Checks whether the current <see cref="BoundingBox"/> intersects with a specified <see cref="Ray"/>.
    /// </summary>
    /// <param name="ray">The <see cref="Ray"/> to check for intersection with the current <see cref="BoundingBox"/>.</param>
    /// <returns>Distance value if intersects, null otherwise.</returns>
    public float? Intersects(in Ray ray)
    {
        //Source: Real-Time Collision Detection by Christer Ericson
        //Reference: Page 179

        float distance = 0.0f;
        float tmax = float.MaxValue;

        if (MathHelper.IsZero(ray.Direction.X))
        {
            if (ray.Position.X < Min.X || ray.Position.X > Max.X)
            {
                return null;
            }
        }
        else
        {
            float inverse = 1.0f / ray.Direction.X;
            float t1 = (Min.X - ray.Position.X) * inverse;
            float t2 = (Max.X - ray.Position.X) * inverse;

            if (t1 > t2)
            {
                (t2, t1) = (t1, t2);
            }

            distance = MathHelper.Max(t1, distance);
            tmax = MathHelper.Min(t2, tmax);

            if (distance > tmax)
            {
                return null;
            }
        }

        if (MathHelper.IsZero(ray.Direction.Y))
        {
            if (ray.Position.Y < Min.Y || ray.Position.Y > Max.Y)
            {
                return null;
            }
        }
        else
        {
            float inverse = 1.0f / ray.Direction.Y;
            float t1 = (Min.Y - ray.Position.Y) * inverse;
            float t2 = (Max.Y - ray.Position.Y) * inverse;

            if (t1 > t2)
            {
                (t2, t1) = (t1, t2);
            }

            distance = Math.Max(t1, distance);
            tmax = Math.Min(t2, tmax);

            if (distance > tmax)
            {
                return null;
            }
        }

        if (MathHelper.IsZero(ray.Direction.Z))
        {
            if (ray.Position.Z < Min.Z || ray.Position.Z > Max.Z)
            {
                return null;
            }
        }
        else
        {
            float inverse = 1.0f / ray.Direction.Z;
            float t1 = (Min.Z - ray.Position.Z) * inverse;
            float t2 = (Max.Z - ray.Position.Z) * inverse;

            if (t1 > t2)
            {
                (t2, t1) = (t1, t2);
            }

            distance = MathHelper.Max(t1, distance);
            tmax = MathHelper.Min(t2, tmax);

            if (distance > tmax)
            {
                return null;
            }
        }

        return distance;
    }

    public PlaneIntersectionType Intersects(in Plane plane)
    {
        //Source: Real-Time Collision Detection by Christer Ericson
        //Reference: Page 161

        Vector3 min;
        Vector3 max;

        max.X = (plane.Normal.X >= 0.0f) ? Min.X : Max.X;
        max.Y = (plane.Normal.Y >= 0.0f) ? Min.Y : Max.Y;
        max.Z = (plane.Normal.Z >= 0.0f) ? Min.Z : Max.Z;
        min.X = (plane.Normal.X >= 0.0f) ? Max.X : Min.X;
        min.Y = (plane.Normal.Y >= 0.0f) ? Max.Y : Min.Y;
        min.Z = (plane.Normal.Z >= 0.0f) ? Max.Z : Min.Z;

        float distance = Vector3.Dot(plane.Normal, max);

        if (distance + plane.D > 0.0f)
            return PlaneIntersectionType.Front;

        distance = Vector3.Dot(plane.Normal, min);

        if (distance + plane.D < 0.0f)
            return PlaneIntersectionType.Back;

        return PlaneIntersectionType.Intersecting;
    }

    /// <inheritdoc/>
    public override readonly bool Equals(object? obj) => (obj is BoundingBox other) && Equals(other);

    /// <summary>
    /// Determines whether the specified <see cref="BoundingBox"/> is equal to this instance.
    /// </summary>
    /// <param name="other">The <see cref="Int4"/> to compare with this instance.</param>
    public readonly bool Equals(BoundingBox other)
    {
        return _min.Equals(other._min)
            && _max.Equals(other._max);
    }

    /// <summary>
    /// Compares two <see cref="BoundingBox"/> objects for equality.
    /// </summary>
    /// <param name="left">The <see cref="BoundingBox"/> on the left hand of the operand.</param>
    /// <param name="right">The <see cref="BoundingBox"/> on the right hand of the operand.</param>
    /// <returns>
    /// True if the current left is equal to the <paramref name="right"/> parameter; otherwise, false.
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator ==(BoundingBox left, BoundingBox right)
    {
        return (left._min == right._min)
            && (left._max == right._max);
    }

    /// <summary>
    /// Compares two <see cref="BoundingBox"/> objects for inequality.
    /// </summary>
    /// <param name="left">The <see cref="BoundingBox"/> on the left hand of the operand.</param>
    /// <param name="right">The <see cref="BoundingBox"/> on the right hand of the operand.</param>
    /// <returns>
    /// True if the current left is unequal to the <paramref name="right"/> parameter; otherwise, false.
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator !=(BoundingBox left, BoundingBox right)
    {
        return (left._min != right._min)
            || (left._max != right._max);
    }

    /// <inheritdoc/>
    public override readonly int GetHashCode() => HashCode.Combine(_min, _max);

    /// <inheritdoc />
    public override string ToString() => ToString(format: null, formatProvider: null);

    /// <inheritdoc />
    public string ToString(string? format, IFormatProvider? formatProvider)
    {
        return $"{nameof(BoundingBox)} {{ {nameof(Min)} = {Min.ToString(format, formatProvider)}, {nameof(Max)} = {Max.ToString(format, formatProvider)} }}";
    }
}
