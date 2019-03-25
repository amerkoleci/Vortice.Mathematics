// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using System;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace Vortice.Mathematics
{
    /// <summary>
    /// Represents a three dimensional mathematical int vector.
    /// </summary>
    [Serializable]
    [DataContract]
    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct IntVector3 : IEquatable<IntVector3>
    {
        /// <summary>
        /// The size in bytes of the <see cref="IntVector3"/> type.
        /// </summary>
        public static readonly int SizeInBytes = Unsafe.SizeOf<IntVector3>();

        /// <summary>
        /// A <see cref="IntVector3"/> with all of its components set to zero.
        /// </summary>
        public static readonly IntVector3 Zero = new IntVector3();

        /// <summary>
        /// The X unit <see cref="IntVector2"/> (1, 0, 0).
        /// </summary>
        public static readonly IntVector3 UnitX = new IntVector3(1, 0, 0);

        /// <summary>
        /// The Y unit <see cref="IntVector3"/> (0, 1, 0).
        /// </summary>
        public static readonly IntVector3 UnitY = new IntVector3(0, 1, 0);

        /// <summary>
        /// The Y unit <see cref="IntVector3"/> (0, 0, 1).
        /// </summary>
        public static readonly IntVector3 UnitZ = new IntVector3(0, 0, 1);

        /// <summary>
        /// A <see cref="IntVector3"/> with all of its components set to one.
        /// </summary>
        public static readonly IntVector3 One = new IntVector3(1, 1, 1);

        /// <summary>
        /// The X component of the vector.
        /// </summary>
        public int X;

        /// <summary>
        /// The Y component of the vector.
        /// </summary>
        public int Y;

        /// <summary>
        /// The Z component of the vector.
        /// </summary>
        public int Z;

        /// <summary>
        /// Initializes a new instance of the <see cref="IntVector3"/> struct.
        /// </summary>
        /// <param name="value">The value that will be assigned to all components.</param>
        public IntVector3(int value)
        {
            X = Y = Z = value;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="IntVector3" /> struct.
        /// </summary>
        /// <param name="x">Initial value for the X component of the vector.</param>
        /// <param name="y">Initial value for the Y component of the vector.</param>
        /// <param name="z">Initial value for the Z component of the vector.</param>
        public IntVector3(int x, int y, int z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        /// <summary>
        /// Performs an explicit conversion from <see cref="IntVector4" /> to <see cref="IntVector2" />.
        /// </summary>
        /// <param name="value">The value to convert.</param>
        /// <returns>The result of the conversion.</returns>
        public static explicit operator IntVector2(IntVector3 value) => new IntVector2(value.X, value.Y);

        /// <summary>
        /// Performs an explicit conversion from <see cref="IntVector3" /> to <see cref="Vector2"/>.
        /// </summary>
        /// <param name="value">The value to convert.</param>
        /// <returns>The result of the conversion.</returns>
        public static explicit operator Vector2(IntVector3 value) => new Vector2(value.X, value.Y);

        /// <summary>
        /// Performs an explicit conversion from <see cref="IntVector3" /> to <see cref="Vector3"/>.
        /// </summary>
        /// <param name="value">The value to convert.</param>
        /// <returns>The result of the conversion.</returns>
        public static explicit operator Vector3(IntVector3 value) => new Vector3(value.X, value.Y, value.Z);

        public void Deconstruct(out int x, out int y, out int z)
        {
            x = X;
            y = Y;
            z = Z;
        }

        /// <inheritdoc/>
		public override bool Equals(object obj) => obj is IntVector3 value && Equals(ref value);

        /// <summary>
        /// Determines whether the specified <see cref="IntVector3"/> is equal to this instance.
        /// </summary>
        /// <param name="other">The <see cref="IntVector3"/> to compare with this instance.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Equals(IntVector3 other) => Equals(ref other);

        /// <summary>
		/// Determines whether the specified <see cref="IntVector3"/> is equal to this instance.
		/// </summary>
		/// <param name="other">The <see cref="IntVector3"/> to compare with this instance.</param>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Equals(ref IntVector3 other)
        {
            return X == other.X
                && Y == other.Y
                && Z == other.Z;
        }

        /// <summary>
        /// Compares two <see cref="IntVector3"/> objects for equality.
        /// </summary>
        /// <param name="left">The <see cref="IntVector3"/> on the left hand of the operand.</param>
        /// <param name="right">The <see cref="IntVector3"/> on the right hand of the operand.</param>
        /// <returns>
        /// True if the current left is equal to the <paramref name="right"/> parameter; otherwise, false.
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator ==(IntVector3 left, IntVector3 right) => left.Equals(ref right);

        /// <summary>
        /// Compares two <see cref="IntVector3"/> objects for inequality.
        /// </summary>
        /// <param name="left">The <see cref="IntVector3"/> on the left hand of the operand.</param>
        /// <param name="right">The <see cref="IntVector3"/> on the right hand of the operand.</param>
        /// <returns>
        /// True if the current left is unequal to the <paramref name="right"/> parameter; otherwise, false.
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator !=(IntVector3 left, IntVector3 right) => !left.Equals(ref right);

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
            return $"{nameof(X)}: {X}, {nameof(Y)}: {Y}, {nameof(Z)}: {Z}";
        }
    }
}
