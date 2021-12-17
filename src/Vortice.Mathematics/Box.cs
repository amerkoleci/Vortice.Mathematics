// Copyright (c) Amer Koleci and contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

using System;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;

namespace Vortice.Mathematics
{
    /// <summary>
    /// Represents a 3D box.
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public readonly struct Box : IEquatable<Box>, IFormattable
    {
        /// <summary>
        /// A <see cref="Box"/> with all of its components set to zero.
        /// </summary>
        public static readonly Box Zero = new Box();

        /// <summary>
        /// Initializes a new instance of <see cref="Box"/> structure.
        /// </summary>
        /// <param name="left">Left coordinates</param>
        /// <param name="top">Top coordinates</param>
        /// <param name="front">Front coordinate.</param>
        /// <param name="right"></param>
        /// <param name="bottom"></param>
        /// <param name="back"></param>
        public Box(int left, int top, int front, int right, int bottom, int back)
        {
            Left = left;
            Top = top;
            Front = front;
            Right = right;
            Bottom = bottom;
            Back = back;
        }

        /// <summary>
        /// Initializes a new instance of <see cref="Box"/> structure.
        /// </summary>
        /// <param name="offset">The <see cref="Point3"/> containing Left, Top and Front coordiantes.</param>
        /// <param name="extent">The <see cref="Size3"/> containing Width, Height and Depth.</param>
        public Box(in Point3 offset, in Size3 extent)
        {
            Left = offset.X;
            Top = offset.Y;
            Front = offset.Z;
            Right = offset.X + extent.Width;
            Bottom = offset.Y + extent.Height;
            Back = offset.Z + extent.Depth;
        }

        /// <summary>
        /// The x position of the left hand side of the box.
        /// </summary>
        public int Left { get; }

        /// <summary>
        /// The y position of the top of the box.
        /// </summary>
        public int Top { get; }

        /// <summary>
        /// The z position of the front of the box.
        /// </summary>
        public int Front { get; }

        /// <summary>
        /// The x position of the right hand side of the box, plus 1. This means that Right - Left equals the width of the box.
        /// </summary>
        public int Right { get; }

        /// <summary>
        /// The y position of the bottom of the box, plus 1. This means that top - bottom equals the height of the box.
        /// </summary>
        public int Bottom { get; }

        /// <summary>
        /// The z position of the back of the box, plus 1. This means that front - back equals the depth of the box.
        /// </summary>
        public int Back { get; }

        /// <summary>
        /// The width of the box (equals to Right - Left).
        /// </summary>
        public int Width => unchecked(Right - Left);

        /// <summary>
        /// The height of the box (equals to Top - Bottom).
        /// </summary>
        public int Height => unchecked(Top - Bottom);

        /// <summary>
        /// The depth of the box (equals to Front - Back).
        /// </summary>
        public int Depth => unchecked(Front - Back);

        /// <inheritdoc/>
		public override bool Equals(object? obj) => obj is Box value && Equals(ref value);

        /// <summary>
        /// Determines whether the specified <see cref="Box"/> is equal to this instance.
        /// </summary>
        /// <param name="other">The <see cref="Box"/> to compare with this instance.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Equals(Box other) => Equals(ref other);

        /// <summary>
		/// Determines whether the specified <see cref="Box"/> is equal to this instance.
		/// </summary>
		/// <param name="other">The <see cref="Box"/> to compare with this instance.</param>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Equals(ref Box other)
        {
            return Left == other.Left
                && Top == other.Top
                && Front == other.Front
                && Right == other.Right
                && Bottom == other.Bottom
                && Back == other.Back;
        }

        /// <summary>
        /// Compares two <see cref="Box"/> objects for equality.
        /// </summary>
        /// <param name="left">The <see cref="Box"/> on the left hand of the operand.</param>
        /// <param name="right">The <see cref="Box"/> on the right hand of the operand.</param>
        /// <returns>
        /// True if the current left is equal to the <paramref name="right"/> parameter; otherwise, false.
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator ==(Box left, Box right) => left.Equals(ref right);

        /// <summary>
        /// Compares two <see cref="Box"/> objects for inequality.
        /// </summary>
        /// <param name="left">The <see cref="Box"/> on the left hand of the operand.</param>
        /// <param name="right">The <see cref="Box"/> on the right hand of the operand.</param>
        /// <returns>
        /// True if the current left is unequal to the <paramref name="right"/> parameter; otherwise, false.
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator !=(Box left, Box right) => !left.Equals(ref right);

        /// <inheritdoc/>
		public override int GetHashCode()
        {
            var hashCode = new HashCode();
            {
                hashCode.Add(Left);
                hashCode.Add(Top);
                hashCode.Add(Front);
                hashCode.Add(Right);
                hashCode.Add(Bottom);
                hashCode.Add(Back);
            }
            return hashCode.ToHashCode();
        }

        /// <inheritdoc/>
        public override string ToString() => ToString(format: null, formatProvider: null);

        /// <inheritdoc />
        public string ToString(string? format, IFormatProvider? formatProvider)
        {
            var separator = NumberFormatInfo.GetInstance(formatProvider).NumberGroupSeparator;

            return new StringBuilder()
                .Append('<')
                .Append(Left.ToString(format, formatProvider)).Append(separator).Append(' ')
                .Append(Top.ToString(format, formatProvider)).Append(separator).Append(' ')
                .Append(Front.ToString(format, formatProvider)).Append(separator).Append(' ')
                .Append(Right.ToString(format, formatProvider)).Append(separator).Append(' ')
                .Append(Bottom.ToString(format, formatProvider)).Append(separator).Append(' ')
                .Append(Back.ToString(format, formatProvider))
                .Append('>')
                .ToString();
        }
    }
}
