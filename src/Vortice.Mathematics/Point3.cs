// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using System;
using System.ComponentModel;
using System.Numerics;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;
using System.Globalization;
using System.Text;

namespace Vortice.Mathematics
{
    /// <summary>
    /// Represents a three-dimensional offset.
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public readonly struct Point3 : IEquatable<Point3>, IFormattable
    {
        /// <summary>
        /// A <see cref="Point3"/> with all of its components set to zero.
        /// </summary>
        public static readonly Point3 Empty = default;

        /// <summary>
        /// The X unit <see cref="Point3"/> (1, 0, 0).
        /// </summary>
        public static readonly Point3 UnitX = new Point3(1, 0, 0);

        /// <summary>
        /// The Y unit <see cref="Point3"/> (0, 1, 0).
        /// </summary>
        public static readonly Point3 UnitY = new Point3(0, 1, 0);

        /// <summary>
        /// The Z unit <see cref="Point3"/> (0, 0, 1).
        /// </summary>
        public static readonly Point3 UnitZ = new Point3(0, 0, 1);

        /// <summary>
        /// A <see cref="Point3"/> with all of its components set to one.
        /// </summary>
        public static readonly Point3 One = new Point3(1, 1, 1);

        /// <summary>
        /// Initializes a new instance of the <see cref="Point3"/> struct.
        /// </summary>
        /// <param name="value">The value that will be assigned to all components.</param>
        public Point3(int value)
        {
            X = value;
            Y = value;
            Z = value;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Point3" /> struct.
        /// </summary>
        /// <param name="x">Initial value for the X component of the offset.</param>
        /// <param name="y">Initial value for the Y component of the offset.</param>
        /// <param name="z">Initial value for the Z component of the offset.</param>
        public Point3(int x, int y, int z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Point3"/> struct.
        /// </summary>
        /// <param name="value">A vector containing the values with which to initialize the X and Y components.</param>
        public Point3(Vector2 value)
        {
            X = (int)value.X;
            Y = (int)value.Y;
            Z = 0;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Point3"/> struct.
        /// </summary>
        /// <param name="value">A vector containing the values with which to initialize the X and Y components.</param>
        public Point3(Vector3 value)
        {
            X = (int)value.X;
            Y = (int)value.Y;
            Z = (int)value.Z;
        }

        /// <summary>
        /// Gets the x-coordinate of this <see cref="Point3"/>.
        /// </summary>
        public int X { get; }

        /// <summary>
        /// Gets the y-coordinate of this <see cref="Point3"/>.
        /// </summary>
        public int Y { get; }

        /// <summary>
        /// Gets the z-coordinate of this <see cref="Point3"/>.
        /// </summary>
        public int Z { get; }

        /// <summary>
        /// Gets a value indicating whether this <see cref="Point"/> is empty.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public readonly bool IsEmpty => this == Empty;

        /// <summary>
        /// Deconstructs this point into three integers.
        /// </summary>
        /// <param name="x">The out value for X.</param>
        /// <param name="y">The out value for Y.</param>
        /// <param name="z">The out value for Z.</param>
        public void Deconstruct(out int x, out int y, out int z)
        {
            x = X;
            y = Y;
            z = Z;
        }

        /// <summary>
        /// Performs an explicit conversion from <see cref="Point3"/> to <see cref="Vector3" />.
        /// </summary>
        /// <param name="value">The value to convert.</param>
        /// <returns>The result of the conversion.</returns>
        public static explicit operator Vector3(Point3 value) => new Vector3(value.X, value.Y, value.Z);

        /// <summary>
        /// Performs an explicit conversion from <see cref="Point3"/> to <see cref="Vector4"/>.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The result of the conversion.</returns>
        public static explicit operator Vector4(Point3 value) => new Vector4(value.X, value.Y, value.Z, 0);

        /// <summary>
        /// Compares two <see cref="Point3"/> objects for equality.
        /// </summary>
        /// <param name="left">The <see cref="Point3"/> on the left hand of the operand.</param>
        /// <param name="right">The <see cref="Point3"/> on the right hand of the operand.</param>
        /// <returns>
        /// True if the current left is equal to the <paramref name="right"/> parameter; otherwise, false.
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator ==(Point3 left, Point3 right) => left.Equals(right);

        /// <summary>
        /// Compares two <see cref="Point3"/> objects for inequality.
        /// </summary>
        /// <param name="left">The <see cref="Point3"/> on the left hand of the operand.</param>
        /// <param name="right">The <see cref="Point3"/> on the right hand of the operand.</param>
        /// <returns>
        /// True if the current left is unequal to the <paramref name="right"/> parameter; otherwise, false.
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator !=(Point3 left, Point3 right) => !left.Equals(right);

        /// <inheritdoc/>
        public override bool Equals(object? obj) => obj is Point3 other && Equals(other);

        /// <inheritdoc/>
        public bool Equals(Point3 other) => X == other.X && Y == other.Y && Z == other.Z;


        /// <inheritdoc/>
        public override int GetHashCode()
        {
            var hashCode = new HashCode();
            {
                hashCode.Add(X);
                hashCode.Add(Y);
                hashCode.Add(Z);
            }
            return hashCode.ToHashCode();
        }

        /// <inheritdoc/>
        public override string ToString() => ToString(format: null, formatProvider: null);

        /// <inheritdoc />
        public string ToString(string? format, IFormatProvider? formatProvider)
        {
            string? separator = NumberFormatInfo.GetInstance(formatProvider).NumberGroupSeparator;

            return new StringBuilder()
                .Append('<')
                .Append(X.ToString(format, formatProvider)).Append(separator).Append(' ')
                .Append(Y.ToString(format, formatProvider)).Append(separator).Append(' ')
                .Append(Z.ToString(format, formatProvider))
                .Append('>')
                .ToString();
        }
    }
}
