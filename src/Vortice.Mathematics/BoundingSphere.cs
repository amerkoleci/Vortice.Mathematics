// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using System;
using System.Globalization;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;

namespace Vortice.Mathematics
{
    /// <summary>
    /// Defines an sphere in three dimensional space.
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public readonly struct BoundingSphere : IEquatable<BoundingSphere>, IFormattable
    {
        /// <summary>
        /// An empty bounding sphere (Center = 0 and Radius = 0).
        /// </summary>
        public static readonly BoundingSphere Empty = new BoundingSphere();

        /// <summary>
        /// Initializes a new instance of the <see cref="BoundingSphere"/> struct.
        /// </summary>
        /// <param name="center">The center of the sphere.</param>
        /// <param name="radius">The radius of the sphere.</param>
        public BoundingSphere(Vector3 center, float radius)
        {
            Center = center;
            Radius = radius;
        }

        /// <summary>
        /// The center point of the sphere.
        /// </summary>
        public Vector3 Center { get; }

        /// <summary>
        /// The radious of the sphere.
        /// </summary>
        public float Radius { get; }

        /// <summary>
        /// Creates the smallest <see cref="BoundingSphere"/> that can contain a specified <see cref="BoundingBox"/>.
        /// </summary>
        /// <param name="box">The <see cref="BoundingBox"/> to create the <see cref="BoundingSphere"/> from.</param>
        /// <param name="result">The created <see cref="BoundingSphere"/>.</param>
        public static void CreateFromBoundingBox(in BoundingBox box, out BoundingSphere result)
        {
            result = new BoundingSphere(
                Vector3.Lerp(box.Minimum, box.Maximum, 0.5f),
                Vector3.Distance(box.Minimum, box.Maximum) * 0.5f
                );
        }

        /// <summary>
        /// Creates the smallest <see cref="BoundingSphere"/> that can contain a specified <see cref="BoundingBox"/>.
        /// </summary>
        /// <param name="box">The <see cref="BoundingBox"/> to create the <see cref="BoundingSphere"/> from.</param>
        /// <returns>The created <see cref="BoundingSphere"/>.</returns>
        public static BoundingSphere CreateFromBoundingBox(in BoundingBox box)
        {
            CreateFromBoundingBox(box, out BoundingSphere result);
            return result;
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

        /// <summary>
        /// Checks whether the current <see cref="BoundingSphere"/> intersects with a specified <see cref="BoundingBox"/>.
        /// </summary>
        /// <param name="box">The <see cref="BoundingBox"/> to check for intersection with the current <see cref="BoundingSphere"/>.</param>
        /// <returns>True if intersects, false otherwise.</returns>
        public bool Intersects(in BoundingBox box)
        {
            Vector3 clampedVector = Vector3.Clamp(Center, box.Minimum, box.Maximum);
            float distance = Vector3.DistanceSquared(Center, clampedVector);
            return distance <= Radius * Radius;
        }

        /// <inheritdoc/>
		public override bool Equals(object? obj) => obj is BoundingSphere value && Equals(ref value);

        /// <summary>
        /// Determines whether the specified <see cref="BoundingSphere"/> is equal to this instance.
        /// </summary>
        /// <param name="other">The <see cref="Int4"/> to compare with this instance.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Equals(BoundingSphere other) => Equals(ref other);

        /// <summary>
		/// Determines whether the specified <see cref="BoundingSphere"/> is equal to this instance.
		/// </summary>
		/// <param name="other">The <see cref="BoundingSphere"/> to compare with this instance.</param>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Equals(ref BoundingSphere other)
        {
            return Center.Equals(other.Center)
                && Radius == other.Radius;
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
        public static bool operator ==(BoundingSphere left, BoundingSphere right) => left.Equals(ref right);

        /// <summary>
        /// Compares two <see cref="BoundingSphere"/> objects for inequality.
        /// </summary>
        /// <param name="left">The <see cref="BoundingSphere"/> on the left hand of the operand.</param>
        /// <param name="right">The <see cref="BoundingSphere"/> on the right hand of the operand.</param>
        /// <returns>
        /// True if the current left is unequal to the <paramref name="right"/> parameter; otherwise, false.
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator !=(BoundingSphere left, BoundingSphere right) => !left.Equals(ref right);

        /// <inheritdoc/>
		public override int GetHashCode()
        {
            var hashCode = new HashCode();
            {
                hashCode.Add(Center);
                hashCode.Add(Radius);
            }
            return hashCode.ToHashCode();
        }

        /// <inheritdoc />
        public override string ToString() => ToString(format: null, formatProvider: null);

        /// <inheritdoc />
        public string ToString(string? format, IFormatProvider? formatProvider)
        {
            string? separator = NumberFormatInfo.GetInstance(formatProvider).NumberGroupSeparator;

            return new StringBuilder()
                .Append('<')
                .Append(Center.ToString(format, formatProvider)).Append(separator).Append(' ')
                .Append(Radius.ToString(format, formatProvider))
                .Append('>')
                .ToString();
        }
    }
}
