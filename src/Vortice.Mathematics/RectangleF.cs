// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace Vortice.Mathematics
{
    /// <summary>
    /// Defines a 2D floating-point rectangle.
    /// </summary>
    [Serializable]
    [DataContract]
    [StructLayout(LayoutKind.Sequential)]
    public struct RectangleF : IEquatable<RectangleF>
    {
        /// <summary>
        /// The size in bytes of the <see cref="RectangleF"/> type.
        /// </summary>
        public static readonly int SizeInBytes = Unsafe.SizeOf<RectangleF>();

        /// <summary>
        /// Returns a <see cref="RectangleF"/> with all of its values set to zero.
        /// </summary>
        public static readonly RectangleF Empty = new RectangleF();

        /// <summary>
        /// Specifies the x-coordinate of the rectangle.
        /// </summary>
        public float X;

        /// <summary>
        /// Specifies the y-coordinate of the rectangle.
        /// </summary>
        public float Y;

        /// <summary>
        /// Specifies the width of the rectangle.
        /// </summary>
        public float Width;

        /// <summary>
        /// Specifies the height of the rectangle.
        /// </summary>
        public float Height;

        /// <summary>
		/// Gets the x-coordinate of the left side of the rectangle.
		/// </summary>
		public float Left
        {
            get => X;
            set => X = value;
        }

        /// <summary>
        /// Gets the y-coordinate of the top of the rectangle.
        /// </summary>
        public float Top
        {
            get => Y;
            set => Y = value;
        }

        /// <summary>
        /// Gets the x-coordinate of the right side of the rectangle.
        /// </summary>
        public float Right
        {
            get => X + Width;
            set => Width = value - X;
        }

        /// <summary>
        /// Gets the y-coordinate of the bottom of the rectangle.
        /// </summary>
        public float Bottom
        {
            get => Y + Height;
            set => Height = value - Y;
        }

        /// <summary>
        /// Gets a value indicating whether this <see cref="RectangleF"/> is empty.
        /// </summary>
        public bool IsEmpty => Equals(Empty);

        /// <summary>
		/// Gets the aspect ratio of the rectangle.
		/// </summary>
		public float AspectRatio => Width / Height;

        /// <summary>
        /// Gets or sets the upper-left value of the <see cref="RectangleF"/>.
        /// </summary>
        public PointF Location
        {
            get => new PointF(X, Y);
        }

        /// <summary>
		/// Gets or sets the size of this <see cref="RectangleF"/>.
		/// </summary>
		public SizeF Size
        {
            get => new SizeF(Width, Height);
        }

        /// <summary>
		/// Initializes a new instance of the <see cref="RectangleF"/> struct.
		/// </summary>
		/// <param name="x">The x-coordinate of the rectangle.</param>
		/// <param name="y">The y-coordinate of the rectangle.</param>
		/// <param name="width">Width of the rectangle.</param>
		/// <param name="height">Height of the rectangle.</param>
		public RectangleF(float x, float y, float width, float height)
        {
            X = x;
            Y = y;
            Width = width;
            Height = height;
        }

        /// <summary>
		/// Initializes a new instance of the <see cref="RectangleF"/> struct.
		/// </summary>
		/// <param name="width">Width of the rectangle.</param>
        /// <param name="height">Height of the rectangle.</param>
		public RectangleF(float width, float height)
        {
            X = 0.0f;
            Y = 0.0f;
            Width = width;
            Height = height;
        }

        public void Deconstruct(out float x, out float y, out float width, out float height)
        {
            x = X;
            y = Y;
            width = Width;
            height = Height;
        }

        /// <inheritdoc/>
		public override bool Equals(object obj) => obj is RectangleF rect && Equals(ref rect);

        /// <summary>
        /// Determines whether the specified <see cref="RectangleF"/> is equal to this instance.
        /// </summary>
        /// <param name="other">The <see cref="RectangleF"/> to compare with this instance.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Equals(RectangleF other) => Equals(ref other);

        /// <summary>
		/// Determines whether the specified <see cref="RectangleF"/> is equal to this instance.
		/// </summary>
		/// <param name="other">The <see cref="RectangleF"/> to compare with this instance.</param>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Equals(ref RectangleF other)
        {
            return X.Equals(other.X)
                && Y.Equals(other.Y)
                && Width.Equals(other.Width)
                && Height.Equals(other.Height);
        }

        /// <summary>
        /// Compares two <see cref="RectangleF"/> objects for equality.
        /// </summary>
        /// <param name="left">The <see cref="RectangleF"/> on the left hand of the operand.</param>
        /// <param name="right">The <see cref="RectangleF"/> on the right hand of the operand.</param>
        /// <returns>
        /// True if the current left is equal to the <paramref name="right"/> parameter; otherwise, false.
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator ==(RectangleF left, RectangleF right) => left.Equals(ref right);

        /// <summary>
        /// Compares two <see cref="RectangleF"/> objects for inequality.
        /// </summary>
        /// <param name="left">The <see cref="RectangleF"/> on the left hand of the operand.</param>
        /// <param name="right">The <see cref="RectangleF"/> on the right hand of the operand.</param>
        /// <returns>
        /// True if the current left is unequal to the <paramref name="right"/> parameter; otherwise, false.
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator !=(RectangleF left, RectangleF right) => !left.Equals(ref right);

        /// <inheritdoc/>
		public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = X.GetHashCode();
                hashCode = (hashCode * 397) ^ Y.GetHashCode();
                hashCode = (hashCode * 397) ^ Width.GetHashCode();
                hashCode = (hashCode * 397) ^ Height.GetHashCode();
                return hashCode;
            }
        }

        /// <inheritdoc/>
        public override string ToString()
        {
            return $"{nameof(X)}: {X}, {nameof(Y)}: {Y}, {nameof(Width)}: {Width}, {nameof(Height)}: {Height}";
        }
    }
}
