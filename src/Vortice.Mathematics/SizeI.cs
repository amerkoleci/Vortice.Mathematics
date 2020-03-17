// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using System;
using System.ComponentModel;
using System.Drawing;
using System.Numerics;
using System.Runtime.CompilerServices;

namespace Vortice.Mathematics
{
    /// <summary>
    /// Stores an ordered pair of integer numbers describing the width and height of a rectangle.
    /// </summary>
    public struct SizeI : IEquatable<SizeI>
    {
        /// <summary>
        /// Represents a <see cref="SizeI"/> that has Width and Height values set to zero.
        /// </summary>
        public static readonly SizeI Empty = default;

        /// <summary>
        /// Initializes a new instance of the <see cref="SizeI" /> struct.
        /// </summary>
        /// <param name="width">The width of the size.</param>
        /// <param name="height">The height of the size.</param>
        public SizeI(int width, int height)
        {
            Width = width;
            Height = height;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SizeI"/> struct from the given <see cref="PointI"/>.
        /// </summary>
        /// <param name="point">The point.</param>
        public SizeI(PointI point)
        {
            Width = point.X;
            Height = point.Y;
        }

        /// <summary>
        /// Gets or sets the width of this <see cref="SizeI"/>.
        /// </summary>
        public int Width { get; set; }

        /// <summary>
        /// Gets or sets the height of this <see cref="SizeI"/>.
        /// </summary>
        public int Height { get; set; }

        /// <summary>
        /// Gets a value indicating whether this <see cref="SizeI"/> is empty.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public readonly bool IsEmpty => this == Empty;

        /// <summary>
        /// Deconstructs this size into two integers.
        /// </summary>
        /// <param name="width">The out value for the width.</param>
        /// <param name="height">The out value for the height.</param>
        public void Deconstruct(out int width, out int height)
        {
            width = Width;
            height = Height;
        }

        /// <summary>
        /// Performs an implicit conversion from <see cre ="SizeI"/> to <see cref="System.Drawing.Size" />.
        /// </summary>
        /// <param name="value">The value to convert.</param>
        /// <returns>The result of the conversion.</returns>
        public static implicit operator System.Drawing.Size(SizeI value) => new System.Drawing.Size(value.Width, value.Height);

        /// <summary>
        /// Performs an explicit conversion from <see cref="SizeI"/> to <see cref="PointI"/>.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The result of the conversion.</returns>
        public static explicit operator PointI(SizeI value) => new PointI(value.Width, value.Height);

        /// <summary>
        /// Compares two <see cref="SizeI"/> objects for equality.
        /// </summary>
        /// <param name="left">The <see cref="SizeI"/> on the left hand of the operand.</param>
        /// <param name="right">The <see cref="SizeI"/> on the right hand of the operand.</param>
        /// <returns>
        /// True if the current left is equal to the <paramref name="right"/> parameter; otherwise, false.
        /// </returns>
        public static bool operator ==(SizeI left, SizeI right) => left.Equals(right);

        /// <summary>
        /// Compares two <see cref="SizeI"/> objects for inequality.
        /// </summary>
        /// <param name="left">The <see cref="SizeI"/> on the left hand of the operand.</param>
        /// <param name="right">The <see cref="SizeI"/> on the right hand of the operand.</param>
        /// <returns>
        /// True if the current left is unequal to the <paramref name="right"/> parameter; otherwise, false.
        /// </returns>
        public static bool operator !=(SizeI left, SizeI right) => !left.Equals(right);

        /// <inheritdoc/>
		public override int GetHashCode() => HashCode.Combine(Width, Height);

        /// <inheritdoc/>
        public override string ToString() => $"{{Width={Width}, Height={Height}}}";

        /// <inheritdoc/>
        public override bool Equals(object obj) => obj is SizeI other && Equals(other);

        /// <inheritdoc/>
        public bool Equals(SizeI other) => Width == other.Width && Height == other.Height;
    }
}
