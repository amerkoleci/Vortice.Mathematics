// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using System;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Vortice.Mathematics
{
    /// <summary>
    /// Represents a three-dimensional offset.
    /// </summary>
    [Serializable]
    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct Offset3D : IEquatable<Offset3D>, IFormattable
    {
        /// <summary>
        /// The size of the <see cref="Offset3D"/> type, in bytes.
        /// </summary>
        public static readonly int SizeInBytes = Unsafe.SizeOf<Offset3D>();

        /// <summary>
        /// A <see cref="Offset3D"/> with all of its components set to zero.
        /// </summary>
        public static readonly Offset3D Zero = new Offset3D();

        /// <summary>
        /// The X unit <see cref="Offset3D"/> (1, 0, 0).
        /// </summary>
        public static readonly Offset3D UnitX = new Offset3D(1, 0, 0);

        /// <summary>
        /// The Y unit <see cref="Offset3D"/> (0, 1, 0).
        /// </summary>
        public static readonly Offset3D UnitY = new Offset3D(0, 1, 0);

        /// <summary>
        /// The Z unit <see cref="Offset3D"/> (0, 0, 1).
        /// </summary>
        public static readonly Offset3D UnitZ = new Offset3D(0, 0, 1);

        /// <summary>
        /// A <see cref="Offset3D"/> with all of its components set to one.
        /// </summary>
        public static readonly Offset3D One = new Offset3D(1, 1, 1);

        /// <summary>
        /// The X component of the offset.
        /// </summary>
        public int X;

        /// <summary>
        /// The Y component of the offset.
        /// </summary>
        public int Y;

        /// <summary>
        /// The Z component of the offset.
        /// </summary>
        public int Z;

        /// <summary>
        /// Initializes a new instance of the <see cref="Offset3D"/> struct.
        /// </summary>
        /// <param name="value">The value that will be assigned to all components.</param>
        public Offset3D(int value)
        {
            X = Y = Z = value;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Offset3D" /> struct.
        /// </summary>
        /// <param name="x">Initial value for the X component of the offset.</param>
        /// <param name="y">Initial value for the Y component of the offset.</param>
        /// <param name="z">Initial value for the Z component of the offset.</param>
        public Offset3D(int x, int y, int z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Offset3D"/> struct.
        /// </summary>
        /// <param name="value">A vector containing the values with which to initialize the X and Y components.</param>
        public Offset3D(Vector2 value)
        {
            X = (int)value.X;
            Y = (int)value.Y;
            Z = 0;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Offset3D"/> struct.
        /// </summary>
        /// <param name="value">A vector containing the values with which to initialize the X and Y components.</param>
        public Offset3D(Vector3 value)
        {
            X = (int)value.X;
            Y = (int)value.Y;
            Z = (int)value.Z;
        }

        public void Deconstruct(out int x, out int y, out int z)
        {
            x = X;
            y = Y;
            z = Z;
        }

        /// <summary>
        /// Creates an array containing the elements of the offset.
        /// </summary>
        /// <returns>A three-element array containing the components of the offset.</returns>
        public int[] ToArray() => new int[] { X, Y, Z };

        /// <summary>
        /// Performs an explicit conversion from <see cre ="Offset3D"/> to <see cref="Vector3" />.
        /// </summary>
        /// <param name="value">The value to convert.</param>
        /// <returns>The result of the conversion.</returns>
        public static explicit operator Vector3(Offset3D value) => new Vector3(value.X, value.Y, value.Z);

        /// <summary>
        /// Performs an explicit conversion from <see cref="Offset3D"/> to <see cref="Vector4"/>.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The result of the conversion.</returns>
        public static explicit operator Vector4(Offset3D value) => new Vector4(value.X, value.Y, value.Z, 0);

        /// <inheritdoc/>
		public override bool Equals(object obj) => obj is Offset3D value && Equals(ref value);

        /// <summary>
        /// Determines whether the specified <see cref="Offset3D"/> is equal to this instance.
        /// </summary>
        /// <param name="other">The <see cref="Offset3D"/> to compare with this instance.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Equals(Offset3D other) => Equals(ref other);

        /// <summary>
		/// Determines whether the specified <see cref="Offset3D"/> is equal to this instance.
		/// </summary>
		/// <param name="other">The <see cref="Offset3D"/> to compare with this instance.</param>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Equals(ref Offset3D other)
        {
            return X == other.X
                && Y == other.Y
                && Z == other.Z;
        }

        /// <summary>
        /// Compares two <see cref="Offset3D"/> objects for equality.
        /// </summary>
        /// <param name="left">The <see cref="Offset3D"/> on the left hand of the operand.</param>
        /// <param name="right">The <see cref="Offset3D"/> on the right hand of the operand.</param>
        /// <returns>
        /// True if the current left is equal to the <paramref name="right"/> parameter; otherwise, false.
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator ==(Offset3D left, Offset3D right) => left.Equals(ref right);

        /// <summary>
        /// Compares two <see cref="Offset3D"/> objects for inequality.
        /// </summary>
        /// <param name="left">The <see cref="Offset3D"/> on the left hand of the operand.</param>
        /// <param name="right">The <see cref="Offset3D"/> on the right hand of the operand.</param>
        /// <returns>
        /// True if the current left is unequal to the <paramref name="right"/> parameter; otherwise, false.
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator !=(Offset3D left, Offset3D right) => !left.Equals(ref right);

        /// <inheritdoc/>
		public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = X.GetHashCode();
                hashCode = (hashCode * 397) ^ Y.GetHashCode();
                hashCode = (hashCode * 397) ^ Z.GetHashCode();
                return hashCode;
            }
        }

        /// <inheritdoc/>
        public override string ToString()
        {
            return $"{nameof(Offset3D)}({X}, {Y}, {Z})";
        }

        public string ToString(string format, IFormatProvider formatProvider)
        {
            return $"{nameof(Offset3D)}({X.ToString(format, formatProvider)}, {Y.ToString(format, formatProvider)}, {Z.ToString(format, formatProvider)})";
        }
    }
}
