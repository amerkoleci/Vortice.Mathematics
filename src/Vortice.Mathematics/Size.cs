// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using System;
using System.ComponentModel;
using System.Numerics;

namespace Vortice.Mathematics
{
    /// <summary>
    /// Stores an ordered pair of floating-point numbers describing the width and height of a rectangle.
    /// </summary>
    public struct Size : IEquatable<Size>
    {
        /// <summary>
        /// Represents a <see cref="Size"/> that has Width and Height values set to zero.
        /// </summary>
        public static readonly Size Empty = default;

        /// <summary>
        /// Initializes a new instance of the <see cref="Size" /> struct.
        /// </summary>
        /// <param name="width">The width of the size.</param>
        /// <param name="height">The height of the size.</param>
        public Size(float width, float height)
        {
            Width = width;
            Height = height;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Size"/> struct from the given <see cref="Point"/>.
        /// </summary>
        /// <param name="point">The point.</param>
        public Size(Point point)
        {
            Width = point.X;
            Height = point.Y;
        }

        /// <summary>
        /// Gets or sets the width of this <see cref="Size"/>.
        /// </summary>
        public float Width { get; set; }

        /// <summary>
        /// Gets or sets the height of this <see cref="Size"/>.
        /// </summary>
        public float Height { get; set; }

        /// <summary>
        /// Gets a value indicating whether this <see cref="Size"/> is empty.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public readonly bool IsEmpty => this == Empty;

        /// <summary>
        /// Deconstructs this size into two floats.
        /// </summary>
        /// <param name="width">The out value for the width.</param>
        /// <param name="height">The out value for the height.</param>
        public void Deconstruct(out float width, out float height)
        {
            width = Width;
            height = Height;
        }

        /// <summary>
        /// Performs an implicit conversion from <see cre ="Size"/> to <see cref="Vector2" />.
        /// </summary>
        /// <param name="value">The value to convert.</param>
        /// <returns>The result of the conversion.</returns>
        public static implicit operator Vector2(Size value) => new Vector2(value.Width, value.Height);

        /// <summary>
        /// Performs an implicit conversion from <see cre ="Size"/> to <see cref="System.Drawing.SizeF" />.
        /// </summary>
        /// <param name="value">The value to convert.</param>
        /// <returns>The result of the conversion.</returns>
        public static implicit operator System.Drawing.SizeF(Size value) => new System.Drawing.SizeF(value.Width, value.Height);

        /// <summary>
        /// Performs an explicit conversion from <see cref="Size"/> to <see cref="Point"/>.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The result of the conversion.</returns>
        public static explicit operator Point(Size value) => new Point(value.Width, value.Height);

        /// <summary>
        /// Compares two <see cref="Size"/> objects for equality.
        /// </summary>
        /// <param name="left">The <see cref="Size"/> on the left hand of the operand.</param>
        /// <param name="right">The <see cref="Size"/> on the right hand of the operand.</param>
        /// <returns>
        /// True if the current left is equal to the <paramref name="right"/> parameter; otherwise, false.
        /// </returns>
        public static bool operator ==(Size left, Size right) => left.Equals(right);

        /// <summary>
        /// Compares two <see cref="Size"/> objects for inequality.
        /// </summary>
        /// <param name="left">The <see cref="Size"/> on the left hand of the operand.</param>
        /// <param name="right">The <see cref="Size"/> on the right hand of the operand.</param>
        /// <returns>
        /// True if the current left is unequal to the <paramref name="right"/> parameter; otherwise, false.
        /// </returns>
        public static bool operator !=(Size left, Size right) => !left.Equals(right);

        /// <inheritdoc/>
		public override int GetHashCode() => HashCode.Combine(Width, Height);

        /// <inheritdoc/>
        public override string ToString() => $"{{Width={Width}, Height={Height}}}";

        /// <inheritdoc/>
        public override bool Equals(object obj) => obj is Size other && Equals(other);

        /// <inheritdoc/>
        public bool Equals(Size other) => Width.Equals(other.Width) && Height.Equals(other.Height);
    }
}
