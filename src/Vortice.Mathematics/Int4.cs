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
    /// Represents a four dimensional mathematical int vector.
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public readonly struct Int4 : IEquatable<Int4>, IFormattable
    {
        /// <summary>
        /// A <see cref="Int4"/> with all of its components set to zero.
        /// </summary>
        public static readonly Int4 Zero = new Int4();

        /// <summary>
        /// The X unit <see cref="Int4"/> (1, 0, 0, 0).
        /// </summary>
        public static readonly Int4 UnitX = new Int4(1, 0, 0, 0);

        /// <summary>
        /// The Y unit <see cref="Int4"/> (0, 1, 0, 0).
        /// </summary>
        public static readonly Int4 UnitY = new Int4(0, 1, 0, 0);

        /// <summary>
        /// The Y unit <see cref="Int4"/> (0, 0, 1, 0).
        /// </summary>
        public static readonly Int4 UnitZ = new Int4(0, 0, 1, 0);

        /// <summary>
        /// The W unit <see cref="Int4"/> (0, 0, 0, 1).
        /// </summary>
        public static readonly Int4 UnitW = new Int4(0, 0, 0, 1);

        /// <summary>
        /// A <see cref="Int4"/> with all of its components set to one.
        /// </summary>
        public static readonly Int4 One = new Int4(1, 1, 1, 1);

        /// <summary>
        /// The X component of the vector.
        /// </summary>
        public int X { get; }

        /// <summary>
        /// The Y component of the vector.
        /// </summary>
        public int Y { get; }

        /// <summary>
        /// The Z component of the vector.
        /// </summary>
        public int Z { get; }

        /// <summary>
        /// The W component of the vector.
        /// </summary>
        public int W { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Int4"/> struct.
        /// </summary>
        /// <param name="value">The value that will be assigned to all components.</param>
        public Int4(int value)
        {
            X = Y = Z = W = value;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Int4" /> struct.
        /// </summary>
        /// <param name="x">Initial value for the X component of the vector.</param>
        /// <param name="y">Initial value for the Y component of the vector.</param>
        /// <param name="z">Initial value for the Z component of the vector.</param>
        /// <param name="w">Initial value for the Z component of the vector.</param>
        public Int4(int x, int y, int z, int w)
        {
            X = x;
            Y = y;
            Z = z;
            W = w;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Int4" /> struct.
        /// </summary>
        /// <param name="value">A <see cref="Int3"/> containing the values with which to initialize the X, Y, and Z components.</param>
        /// <param name="w">Initial value for the W component of the vector.</param>
        public Int4(Int3 value, int w)
        {
            X = value.X;
            Y = value.Y;
            Z = value.Z;
            W = w;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Int4" /> struct.
        /// </summary>
        /// <param name="value">A <see cref="Point"/> containing the values with which to initialize the X and Y components.</param>
        /// <param name="z">Initial value for the Z component of the vector.</param>
        /// <param name="w">Initial value for the W component of the vector.</param>
        public Int4(Point value, int z, int w)
        {
            X = value.X;
            Y = value.Y;
            Z = z;
            W = w;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Int4" /> struct.
        /// </summary>
        /// <param name="values">
        /// The values to assign to the X, Y, Z, and W components of the vector. This must be an array with four elements.
        /// </param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="values" /> is <c>null</c>.</exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown when <paramref name="values" /> contains more or less than four elements.
        /// </exception>
        public Int4(int[] values)
        {
            if (values == null)
            {
                throw new ArgumentNullException(nameof(values));
            }

            if (values.Length != 3 && values.Length != 4)
            {
                throw new ArgumentOutOfRangeException(nameof(values), "There must be 3 or 4 int values.");
            }

            X = values[0];
            Y = values[1];
            Z = values[2];
            W = values.Length >= 4 ? values[3] : 1;
        }

        public void Deconstruct(out int x, out int y, out int z, out int w)
        {
            x = X;
            y = Y;
            z = Z;
            w = W;
        }

        /// <summary>
        /// Creates an array containing the elements of the vector.
        /// </summary>
        /// <returns>A four-element array containing the components of the vector.</returns>
        public int[] ToArray() => new[] { X, Y, Z, W };

        /// <summary>
        /// Performs an explicit conversion from <see cref="Int4" /> to <see cref="Point" />.
        /// </summary>
        /// <param name="value">The value to convert.</param>
        /// <returns>The result of the conversion.</returns>
        public static explicit operator Point(Int4 value) => new Point(value.X, value.Y);

        /// <summary>
        /// Performs an explicit conversion from <see cref="Int4" /> to <see cref="Int3" />.
        /// </summary>
        /// <param name="value">The value to convert.</param>
        /// <returns>The result of the conversion.</returns>
        public static explicit operator Int3(Int4 value) => new Int3(value.X, value.Y, value.Z);

        /// <summary>
        /// Performs an explicit conversion from <see cref="Int4" /> to <see cref="Vector2"/>.
        /// </summary>
        /// <param name = "value">The value to convert.</param>
        /// <returns>The result of the conversion.</returns>
        public static explicit operator Vector2(Int4 value) => new Vector2(value.X, value.Y);

        /// <summary>
        /// Performs an explicit conversion from <see cref="Int4" /> to <see cref="Vector3"/>.
        /// </summary>
        /// <param name = "value">The value to convert.</param>
        /// <returns>The result of the conversion.</returns>
        public static explicit operator Vector3(Int4 value) => new Vector3(value.X, value.Y, value.Z);

        /// <summary>
        /// Performs an explicit conversion from <see cref="Int4" /> to <see cref="Vector4"/>.
        /// </summary>
        /// <param name = "value">The value to convert.</param>
        /// <returns>The result of the conversion.</returns>
        public static explicit operator Vector4(Int4 value) => new Vector4(value.X, value.Y, value.Z, value.W);

        /// <inheritdoc/>
		public override bool Equals(object? obj) => obj is Int4 value && Equals(ref value);

        /// <summary>
        /// Determines whether the specified <see cref="Int4"/> is equal to this instance.
        /// </summary>
        /// <param name="other">The <see cref="Int4"/> to compare with this instance.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Equals(Int4 other) => Equals(ref other);

        /// <summary>
		/// Determines whether the specified <see cref="Int4"/> is equal to this instance.
		/// </summary>
		/// <param name="other">The <see cref="Int4"/> to compare with this instance.</param>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Equals(ref Int4 other)
        {
            return X == other.X
                && Y == other.Y
                && Z == other.Z
                && W == other.W;
        }

        /// <summary>
        /// Compares two <see cref="Int4"/> objects for equality.
        /// </summary>
        /// <param name="left">The <see cref="Int4"/> on the left hand of the operand.</param>
        /// <param name="right">The <see cref="Int4"/> on the right hand of the operand.</param>
        /// <returns>
        /// True if the current left is equal to the <paramref name="right"/> parameter; otherwise, false.
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator ==(Int4 left, Int4 right) => left.Equals(ref right);

        /// <summary>
        /// Compares two <see cref="Int4"/> objects for inequality.
        /// </summary>
        /// <param name="left">The <see cref="Int4"/> on the left hand of the operand.</param>
        /// <param name="right">The <see cref="Int4"/> on the right hand of the operand.</param>
        /// <returns>
        /// True if the current left is unequal to the <paramref name="right"/> parameter; otherwise, false.
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator !=(Int4 left, Int4 right) => !left.Equals(ref right);

        /// <inheritdoc/>
		public override int GetHashCode()
        {
            var hashCode = new HashCode();
            {
                hashCode.Add(X);
                hashCode.Add(Y);
                hashCode.Add(Z);
                hashCode.Add(W);
            }
            return hashCode.ToHashCode();
        }

        /// <inheritdoc/>
        public override string ToString()
        {
            return $"{nameof(Int4)}({X}, {Y}, {Z}, {W})";
        }

        public string ToString(string? format, IFormatProvider? formatProvider)
        {
            string? separator = NumberFormatInfo.GetInstance(formatProvider).NumberGroupSeparator;

            return new StringBuilder()
                .Append('<')
                .Append(X.ToString(format, formatProvider)).Append(separator).Append(' ')
                .Append(Y.ToString(format, formatProvider)).Append(separator).Append(' ')
                .Append(Z.ToString(format, formatProvider)).Append(separator).Append(' ')
                .Append(W.ToString(format, formatProvider))
                .Append('>')
                .ToString();
        }
    }
}
