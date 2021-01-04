// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

// Implementation is based on, see below
// ImageSharp: https://github.com/SixLabors/ImageSharp/blob/master/LICENSE
// SkiaSharp: https://github.com/mono/SkiaSharp/blob/master/LICENSE.md

using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Vortice.Mathematics
{
    /// <summary>
    /// Stores an ordered pair of integer numbers describing the width and height of a rectangle.
    /// </summary>
    public struct Size : IEquatable<Size>
    {
        /// <summary>
        /// Represents a <see cref="Size"/> that has Width and Height values set to zero.
        /// </summary>
        public static readonly Size Empty = default;

        /// <summary>
        /// A special valued <see cref="Size"/>.
        /// </summary>
        public static readonly Size WholeSize = new Size(~0, ~0);

        /// <summary>
        /// Initializes a new instance of the <see cref="Size" /> struct.
        /// </summary>
        /// <param name="width">The width of the size.</param>
        /// <param name="height">The height of the size.</param>
        public Size(int width, int height)
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
        public int Width { readonly get; set; }

        /// <summary>
        /// Gets or sets the height of this <see cref="Size"/>.
        /// </summary>
        public int Height { readonly get; set; }

        /// <summary>
        /// Gets a value indicating whether this <see cref="Size"/> is empty.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public readonly bool IsEmpty => this == Empty;

        public readonly Point ToPoint() => new Point(Width, Height);

        public static Size Add(Size left, Size right) => left + right;
        public static Size Subtract(Size left, Size right) => left - right;

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
        /// Returns a size whose elements are the maximum of each of the pairs of elements in the two source sizes.
        /// </summary>
        /// <param name="value1">The first source size.</param>
        /// <param name="value2">The second source size.</param>
        /// <returns>The maximized size.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Size Max(in Size value1, in Size value2)
        {
            return new Size(
                (value1.Width > value2.Width) ? value1.Width : value2.Width,
                (value1.Height > value2.Height) ? value1.Height : value2.Height
            );
        }

        /// <summary>
        /// Returns a vector size elements are the minimum of each of the pairs of elements in the two source sizes.
        /// </summary>
        /// <param name="value1">The first source size.</param>
        /// <param name="value2">The second source size.</param>
        /// <returns>The minimized size.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Size Min(in Size value1, in Size value2)
        {
            return new Size(
                (value1.Width < value2.Width) ? value1.Width : value2.Width,
                (value1.Height < value2.Height) ? value1.Height : value2.Height
            );
        }

        public static Size operator +(Size left, Size right) =>
            new Size(left.Width + right.Width, left.Height + right.Height);

        public static Size operator -(Size left, Size right) =>
            new Size(left.Width - right.Width, left.Height - right.Height);

        /// <summary>
        /// Performs an implicit conversion from <see cref="Size"/> to <see cref="System.Drawing.Size" />.
        /// </summary>
        /// <param name="value">The value to convert.</param>
        /// <returns>The result of the conversion.</returns>
        public static implicit operator System.Drawing.Size(Size value) => new System.Drawing.Size(value.Width, value.Height);

        /// <summary>
        /// Performs an implicit conversion from <see cref="System.Drawing.Size" /> to <see cref="Size"/>.
        /// </summary>
        /// <param name="value">The value to convert.</param>
        /// <returns>The result of the conversion.</returns>
        public static implicit operator Size(System.Drawing.Size value) => new Size(value.Width, value.Height);

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
        public static bool operator ==(Size left, Size right) => left.Width == right.Width && left.Height == right.Height;

        /// <summary>
        /// Compares two <see cref="Size"/> objects for inequality.
        /// </summary>
        /// <param name="left">The <see cref="Size"/> on the left hand of the operand.</param>
        /// <param name="right">The <see cref="Size"/> on the right hand of the operand.</param>
        /// <returns>
        /// True if the current left is unequal to the <paramref name="right"/> parameter; otherwise, false.
        /// </returns>
        public static bool operator !=(Size left, Size right) => (left.Width != right.Width) || (left.Height != right.Height);

        /// <inheritdoc/>
        public override bool Equals(object? obj) => (obj is Size other) && Equals(other);

        /// <inheritdoc/>
        public bool Equals(Size other) => this == other;

        /// <inheritdoc/>
        public override int GetHashCode()
        {
            var hashCode = new HashCode();
            {
                hashCode.Add(Width);
                hashCode.Add(Height);
            }
            return hashCode.ToHashCode();
        }

        /// <inheritdoc/>
        public override readonly string ToString() => $"{{{nameof(Width)}={Width},{nameof(Height)}={Height}}}";
    }
}
