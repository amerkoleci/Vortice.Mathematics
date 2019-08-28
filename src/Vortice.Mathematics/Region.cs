// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using System;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Vortice.Mathematics
{
    /// <summary>
    /// Represents a 3D region.
    /// </summary>
    [Serializable]
    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct Region : IEquatable<Region>, IFormattable
    {
        /// <summary>
        /// A <see cref="Region"/> with all of its components set to zero.
        /// </summary>
        public static readonly Region Zero = new Region();

        /// <summary>
        /// The original of the region.
        /// </summary>
        public Offset3D Origin;

        /// <summary>
        /// The size of the region.
        /// </summary>
        public Extent3D Size;

        /// <summary>
        /// Initializes a new instance of the <see cref="Offset3D"/> struct as 1D.
        /// </summary>
        /// <param name="x">The x coordinate of one corner of a 1D data.</param>
        /// <param name="width">The width of a 1D data.</param>
        public Region(int x, int width)
        {
            Origin = new Offset3D(x, 0, 0);
            Size = new Extent3D(width, 0, 0);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Offset3D"/> struct as 2D.
        /// </summary>
        /// <param name="x">The x coordinate of one corner of a 2D data.</param>
        /// <param name="y">The y coordinate of one corner of a 2D data.</param>
        /// <param name="width">The width of a 2D data.</param>
        /// <param name="height">The depth of a 2D data.</param>
        public Region(int x, int y, int width, int height)
        {
            Origin = new Offset3D(x, y, 0);
            Size = new Extent3D(width, height, 0);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Offset3D"/> struct as 3D.
        /// </summary>
        /// <param name="x">The x coordinate of one corner of a 3D data.</param>
        /// <param name="y">The y coordinate of one corner of a 3D data.</param>
        /// <param name="z">The z coordinate of one corner of a 3D data.</param>
        /// <param name="width">The width of a 3D data.</param>
        /// <param name="height">The height of a 3D data.</param>
        /// <param name="depth">The depth of a 3D data.</param>
        public Region(int x, int y, int z, int width, int height, int depth)
        {
            Origin = new Offset3D(x, y, z);
            Size = new Extent3D(width, height, depth);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Offset3D"/> struct.
        /// </summary>
        /// <param name="origin">The original value of the region.</param>
        /// <param name="size">The size value of the region.</param>
        public Region(Offset3D origin, Extent3D size)
        {
            Origin = origin;
            Size = size;
        }

        /// <inheritdoc/>
		public override bool Equals(object obj) => obj is Region value && Equals(ref value);

        /// <summary>
        /// Determines whether the specified <see cref="Region"/> is equal to this instance.
        /// </summary>
        /// <param name="other">The <see cref="Offset3D"/> to compare with this instance.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Equals(Region other) => Equals(ref other);

        /// <summary>
		/// Determines whether the specified <see cref="Region"/> is equal to this instance.
		/// </summary>
		/// <param name="other">The <see cref="Offset3D"/> to compare with this instance.</param>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Equals(ref Region other)
        {
            return Origin.Equals(ref other.Origin)
                && Size.Equals(ref other.Size);
        }

        /// <summary>
        /// Compares two <see cref="Region"/> objects for equality.
        /// </summary>
        /// <param name="left">The <see cref="Region"/> on the left hand of the operand.</param>
        /// <param name="right">The <see cref="Region"/> on the right hand of the operand.</param>
        /// <returns>
        /// True if the current left is equal to the <paramref name="right"/> parameter; otherwise, false.
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator ==(Region left, Region right) => left.Equals(ref right);

        /// <summary>
        /// Compares two <see cref="Region"/> objects for inequality.
        /// </summary>
        /// <param name="left">The <see cref="Region"/> on the left hand of the operand.</param>
        /// <param name="right">The <see cref="Region"/> on the right hand of the operand.</param>
        /// <returns>
        /// True if the current left is unequal to the <paramref name="right"/> parameter; otherwise, false.
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator !=(Region left, Region right) => !left.Equals(ref right);

        /// <inheritdoc/>
		public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = Origin.GetHashCode();
                hashCode = (hashCode * 397) ^ Size.GetHashCode();
                return hashCode;
            }
        }

        /// <inheritdoc/>
        public override string ToString()
        {
            return $"{nameof(Region)}({Origin}, {Size})";
        }

        public string ToString(string format, IFormatProvider formatProvider)
        {
            return $"{nameof(Region)}({Origin.ToString(format, formatProvider)}, {Size.ToString(format, formatProvider)})";
        }
    }
}
