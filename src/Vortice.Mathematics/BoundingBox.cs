// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.
//
// -----------------------------------------------------------------------------
// Original code from SlimMath project. http://code.google.com/p/slimmath/
// Greetings to SlimDX Group. Original code published with the following license:
// -----------------------------------------------------------------------------
/*
* Copyright (c) 2007-2011 SlimDX Group
* 
* Permission is hereby granted, free of charge, to any person obtaining a copy
* of this software and associated documentation files (the "Software"), to deal
* in the Software without restriction, including without limitation the rights
* to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
* copies of the Software, and to permit persons to whom the Software is
* furnished to do so, subject to the following conditions:
* 
* The above copyright notice and this permission notice shall be included in
* all copies or substantial portions of the Software.
* 
* THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
* IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
* FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
* AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
* LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
* OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
* THE SOFTWARE.
*/

using System;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Vortice.Mathematics
{
    /// <summary>
    /// Defines an axis-aligned box-shaped 3D volume.
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public readonly struct BoundingBox : IEquatable<BoundingBox>, IFormattable
    {
        /// <summary>
        /// Specifies the total number of corners (8) in the BoundingBox.
        /// </summary>
        public const int CornerCount = 8;

        /// <summary>
        /// A <see cref="BoundingBox"/> which represents an empty space.
        /// </summary>
        public static readonly BoundingBox Empty = new BoundingBox(new Vector3(float.MaxValue), new Vector3(float.MinValue));

        /// <summary>
        /// Gets the center of this bouding box.
        /// </summary>
        public Vector3 Center => (Minimum + Maximum) / 2;

        /// <summary>
        /// Gets the extent of this bouding box.
        /// </summary>
        public Vector3 Extent => (Maximum - Minimum) / 2;

        /// <summary>
        /// Initializes a new instance of the <see cref="BoundingBox"/> struct.
        /// </summary>
        /// <param name="minimum">The minimum vertex of the bounding box.</param>
        /// <param name="maximum">The maximum vertex of the bounding box.</param>
        public BoundingBox(Vector3 minimum, Vector3 maximum)
        {
            Minimum = minimum;
            Maximum = maximum;
        }

        /// <summary>
        /// The minimum point of the box.
        /// </summary>
        public Vector3 Minimum { get; }

        /// <summary>
        /// The maximum point of the box.
        /// </summary>
        public Vector3 Maximum { get; }

        public static BoundingBox CreateFromPoints(Vector3[] points)
        {
            if (points == null)
            {
                throw new ArgumentNullException(nameof(points));
            }

            Vector3 min = new Vector3(float.MaxValue);
            Vector3 max = new Vector3(float.MinValue);

            for (int i = 0; i < points.Length; ++i)
            {
                min = Vector3.Min(min, points[i]);
                max = Vector3.Max(max, points[i]);
            }

            return new BoundingBox(min, max);
        }

        public static BoundingBox CreateFromSphere(in BoundingSphere sphere)
        {
            return new BoundingBox(
                new Vector3(sphere.Center.X - sphere.Radius, sphere.Center.Y - sphere.Radius, sphere.Center.Z - sphere.Radius),
                new Vector3(sphere.Center.X + sphere.Radius, sphere.Center.Y + sphere.Radius, sphere.Center.Z + sphere.Radius)
                );
        }

        public static BoundingBox CreateMerged(in BoundingBox original, in BoundingBox additional)
        {
            return new BoundingBox(
                Vector3.Min(original.Minimum, additional.Minimum),
                Vector3.Max(original.Maximum, additional.Maximum)
            );
        }

        /// <summary>
        /// Retrieves the eight corners of the bounding box.
        /// </summary>
        /// <returns>An array of points representing the eight corners of the bounding box.</returns>
        public Vector3[] GetCorners()
        {
            Vector3[] results = new Vector3[CornerCount];
            GetCorners(results);
            return results;
        }

        /// <summary>
        /// Retrieves the eight corners of the bounding box.
        /// </summary>
        /// <returns>An array of points representing the eight corners of the bounding box.</returns>
        public void GetCorners(Vector3[] corners)
        {
            if (corners == null)
            {
                throw new ArgumentNullException(nameof(corners));
            }

            if (corners.Length < CornerCount)
            {
                throw new ArgumentOutOfRangeException(nameof(corners), $"GetCorners need at least {CornerCount} elements to copy corners.");
            }

            corners[0] = new Vector3(Minimum.X, Maximum.Y, Maximum.Z);
            corners[1] = new Vector3(Maximum.X, Maximum.Y, Maximum.Z);
            corners[2] = new Vector3(Maximum.X, Minimum.Y, Maximum.Z);
            corners[3] = new Vector3(Minimum.X, Minimum.Y, Maximum.Z);
            corners[4] = new Vector3(Minimum.X, Maximum.Y, Minimum.Z);
            corners[5] = new Vector3(Maximum.X, Maximum.Y, Minimum.Z);
            corners[6] = new Vector3(Maximum.X, Minimum.Y, Minimum.Z);
            corners[7] = new Vector3(Minimum.X, Minimum.Y, Minimum.Z);
        }

        public ContainmentType Contains(in Vector3 point)
        {
            if (Minimum.X <= point.X && Maximum.X >= point.X &&
                Minimum.Y <= point.Y && Maximum.Y >= point.Y &&
                Minimum.Z <= point.Z && Maximum.Z >= point.Z)
            {
                return ContainmentType.Contains;
            }

            return ContainmentType.Disjoint;
        }

        public ContainmentType Contains(in BoundingBox box)
        {
            if (Maximum.X < box.Minimum.X || Minimum.X > box.Maximum.X)
                return ContainmentType.Disjoint;

            if (Maximum.Y < box.Minimum.Y || Minimum.Y > box.Maximum.Y)
                return ContainmentType.Disjoint;

            if (Maximum.Z < box.Minimum.Z || Minimum.Z > box.Maximum.Z)
                return ContainmentType.Disjoint;

            if (Minimum.X <= box.Minimum.X && (box.Maximum.X <= Maximum.X &&
                Minimum.Y <= box.Minimum.Y && box.Maximum.Y <= Maximum.Y) &&
                Minimum.Z <= box.Minimum.Z && box.Maximum.Z <= Maximum.Z)
            {
                return ContainmentType.Contains;
            }

            return ContainmentType.Intersects;
        }

        public ContainmentType Contains(in BoundingSphere sphere)
        {
            Vector3 vector = Vector3.Clamp(sphere.Center, Minimum, Maximum);
            float distance = Vector3.DistanceSquared(sphere.Center, vector);

            if (distance > sphere.Radius * sphere.Radius)
                return ContainmentType.Disjoint;

            if (((Minimum.X + sphere.Radius <= sphere.Center.X) && (sphere.Center.X <= Maximum.X - sphere.Radius) && (Maximum.X - Minimum.X > sphere.Radius)) &&
               ((Minimum.Y + sphere.Radius <= sphere.Center.Y) && (sphere.Center.Y <= Maximum.Y - sphere.Radius) && (Maximum.Y - Minimum.Y > sphere.Radius)) &&
               ((Minimum.Z + sphere.Radius <= sphere.Center.Z) && (sphere.Center.Z <= Maximum.Z - sphere.Radius) && (Maximum.Z - Minimum.Z > sphere.Radius)))
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
            if (Maximum.X < box.Minimum.X || Minimum.X > box.Maximum.X)
            {
                return false;
            }

            if (Maximum.Y < box.Minimum.Y || Minimum.Y > box.Maximum.Y)
            {
                return false;
            }

            if (Maximum.Z < box.Minimum.Z || Minimum.Z > box.Maximum.Z)
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
            Vector3 clampedVector = Vector3.Clamp(sphere.Center, Minimum, Maximum);
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
                if (ray.Position.X < Minimum.X || ray.Position.X > Maximum.X)
                {
                    return null;
                }
            }
            else
            {
                float inverse = 1.0f / ray.Direction.X;
                float t1 = (Minimum.X - ray.Position.X) * inverse;
                float t2 = (Maximum.X - ray.Position.X) * inverse;

                if (t1 > t2)
                {
                    float temp = t1;
                    t1 = t2;
                    t2 = temp;
                }

                distance = Math.Max(t1, distance);
                tmax = Math.Min(t2, tmax);

                if (distance > tmax)
                {
                    return null;
                }
            }

            if (MathHelper.IsZero(ray.Direction.Y))
            {
                if (ray.Position.Y < Minimum.Y || ray.Position.Y > Maximum.Y)
                {
                    return null;
                }
            }
            else
            {
                float inverse = 1.0f / ray.Direction.Y;
                float t1 = (Minimum.Y - ray.Position.Y) * inverse;
                float t2 = (Maximum.Y - ray.Position.Y) * inverse;

                if (t1 > t2)
                {
                    float temp = t1;
                    t1 = t2;
                    t2 = temp;
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
                if (ray.Position.Z < Minimum.Z || ray.Position.Z > Maximum.Z)
                {
                    return null;
                }
            }
            else
            {
                float inverse = 1.0f / ray.Direction.Z;
                float t1 = (Minimum.Z - ray.Position.Z) * inverse;
                float t2 = (Maximum.Z - ray.Position.Z) * inverse;

                if (t1 > t2)
                {
                    float temp = t1;
                    t1 = t2;
                    t2 = temp;
                }

                distance = Math.Max(t1, distance);
                tmax = Math.Min(t2, tmax);

                if (distance > tmax)
                {
                    return null;
                }
            }

            return distance;
        }

        public PlaneIntersectionType Intersects(ref Plane plane)
        {
            //Source: Real-Time Collision Detection by Christer Ericson
            //Reference: Page 161

            Vector3 min;
            Vector3 max;

            max.X = (plane.Normal.X >= 0.0f) ? Minimum.X : Maximum.X;
            max.Y = (plane.Normal.Y >= 0.0f) ? Minimum.Y : Maximum.Y;
            max.Z = (plane.Normal.Z >= 0.0f) ? Minimum.Z : Maximum.Z;
            min.X = (plane.Normal.X >= 0.0f) ? Maximum.X : Minimum.X;
            min.Y = (plane.Normal.Y >= 0.0f) ? Maximum.Y : Minimum.Y;
            min.Z = (plane.Normal.Z >= 0.0f) ? Maximum.Z : Minimum.Z;

            float distance = Vector3.Dot(plane.Normal, max);

            if (distance + plane.D > 0.0f)
                return PlaneIntersectionType.Front;

            distance = Vector3.Dot(plane.Normal, min);

            if (distance + plane.D < 0.0f)
                return PlaneIntersectionType.Back;

            return PlaneIntersectionType.Intersecting;
        }

        /// <inheritdoc/>
		public override bool Equals(object? obj) => obj is BoundingBox value && Equals(value);

        /// <summary>
        /// Determines whether the specified <see cref="BoundingBox"/> is equal to this instance.
        /// </summary>
        /// <param name="other">The <see cref="Int4"/> to compare with this instance.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Equals(BoundingBox other)
        {
            return Minimum.Equals(other.Minimum)
                && Maximum.Equals(other.Maximum);
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
        public static bool operator ==(BoundingBox left, BoundingBox right) => left.Equals(right);

        /// <summary>
        /// Compares two <see cref="BoundingBox"/> objects for inequality.
        /// </summary>
        /// <param name="left">The <see cref="BoundingBox"/> on the left hand of the operand.</param>
        /// <param name="right">The <see cref="BoundingBox"/> on the right hand of the operand.</param>
        /// <returns>
        /// True if the current left is unequal to the <paramref name="right"/> parameter; otherwise, false.
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator !=(BoundingBox left, BoundingBox right) => !left.Equals(right);

        /// <inheritdoc/>
		public override int GetHashCode()
        {
            HashCode hashCode = new HashCode();
            {
                hashCode.Add(Minimum);
                hashCode.Add(Maximum);
            }
            return hashCode.ToHashCode();
        }

        /// <inheritdoc />
        public override string ToString() => ToString(format: null, formatProvider: null);

        /// <inheritdoc />
        public string ToString(string? format, IFormatProvider? formatProvider)
        {
            return $"Minimum:{Minimum.ToString(format, formatProvider)} Maximum:{Maximum.ToString(format, formatProvider)}";
        }
    }
}
