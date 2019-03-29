// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace Vortice.Mathematics
{
    /// <summary>
    /// Defines a 2D floating point.
    /// </summary>
    [Serializable]
    [DataContract]
    [StructLayout(LayoutKind.Sequential)]
    public struct PointF : IEquatable<PointF>
    {
        /// <summary>
        /// The size in bytes of the <see cref="PointF"/> type.
        /// </summary>
        public static readonly int SizeInBytes = Unsafe.SizeOf<PointF>();

        /// <summary>
        /// Represents a <see cref="PointF"/> that has X and Y values set to zero.
        /// </summary>
        public static readonly PointF Empty = new PointF();

        /// <summary>
        /// Gets or sets the x-coordinate of this <see cref="PointF"/>.
        /// </summary>
        public float X;

        /// <summary>
        /// Gets or sets the y-coordinate of this <see cref="PointF"/>.
        /// </summary>
        public float Y;

        /// <summary>
        /// Gets a value indicating whether this <see cref="PointF"/> is empty.
        /// </summary>
        public bool IsEmpty => Equals(Empty);

        /// <summary>
		/// Initializes a new instance of the <see cref="PointF"/> struct.
		/// </summary>
		/// <param name="x">The x-coordinate.</param>
		/// <param name="y">The y-coordinate.</param>
		public PointF(float x, float y)
        {
            X = x;
            Y = y;
        }

        public void Deconstruct(out float x, out float y)
        {
            x = X;
            y = Y;
        }

        /// <inheritdoc/>
		public override bool Equals(object obj) => obj is PointF point && Equals(ref point);

        /// <summary>
        /// Determines whether the specified <see cref="PointF"/> is equal to this instance.
        /// </summary>
        /// <param name="other">The <see cref="PointF"/> to compare with this instance.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Equals(PointF other) => Equals(ref other);

        /// <summary>
		/// Determines whether the specified <see cref="PointF"/> is equal to this instance.
		/// </summary>
		/// <param name="other">The <see cref="PointF"/> to compare with this instance.</param>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Equals(ref PointF other)
        {
            return X == other.X
                && Y == other.Y;
        }

        /// <summary>
        /// Compares two <see cref="PointF"/> objects for equality.
        /// </summary>
        /// <param name="left">The <see cref="PointF"/> on the left hand of the operand.</param>
        /// <param name="right">The <see cref="PointF"/> on the right hand of the operand.</param>
        /// <returns>
        /// True if the current left is equal to the <paramref name="right"/> parameter; otherwise, false.
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator ==(PointF left, PointF right) => left.Equals(ref right);

        /// <summary>
        /// Compares two <see cref="PointF"/> objects for inequality.
        /// </summary>
        /// <param name="left">The <see cref="PointF"/> on the left hand of the operand.</param>
        /// <param name="right">The <see cref="PointF"/> on the right hand of the operand.</param>
        /// <returns>
        /// True if the current left is unequal to the <paramref name="right"/> parameter; otherwise, false.
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator !=(PointF left, PointF right) => !left.Equals(ref right);

        /// <inheritdoc/>
		public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = X.GetHashCode();
                hashCode = (hashCode * 397) ^ Y.GetHashCode();
                return hashCode;
            }
        }

        /// <inheritdoc/>
        public override string ToString()
        {
            return $"PointF [ X={X}, Y={Y} ]";
        }
    }
}
