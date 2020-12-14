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

        /// <inheritdoc/>
		public override bool Equals(object? obj) => obj is BoundingBox value && Equals(ref value);

        /// <summary>
        /// Determines whether the specified <see cref="BoundingBox"/> is equal to this instance.
        /// </summary>
        /// <param name="other">The <see cref="Int4"/> to compare with this instance.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Equals(BoundingBox other) => Equals(ref other);

        /// <summary>
		/// Determines whether the specified <see cref="BoundingBox"/> is equal to this instance.
		/// </summary>
		/// <param name="other">The <see cref="BoundingBox"/> to compare with this instance.</param>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Equals(ref BoundingBox other)
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
        public static bool operator ==(BoundingBox left, BoundingBox right) => left.Equals(ref right);

        /// <summary>
        /// Compares two <see cref="BoundingBox"/> objects for inequality.
        /// </summary>
        /// <param name="left">The <see cref="BoundingBox"/> on the left hand of the operand.</param>
        /// <param name="right">The <see cref="BoundingBox"/> on the right hand of the operand.</param>
        /// <returns>
        /// True if the current left is unequal to the <paramref name="right"/> parameter; otherwise, false.
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator !=(BoundingBox left, BoundingBox right) => !left.Equals(ref right);

        /// <inheritdoc/>
		public override int GetHashCode()
        {
            var hashCode = new HashCode();
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
            var separator = NumberFormatInfo.GetInstance(formatProvider).NumberGroupSeparator;

            return new StringBuilder(5 + separator.Length)
                .Append('<')
                .Append(Minimum.ToString(format, formatProvider))
                .Append(separator)
                .Append(' ')
                .Append(Maximum.ToString(format, formatProvider))
                .Append('>')
                .ToString();
        }
    }
}
