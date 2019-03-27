// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace Vortice.Mathematics
{
    /// <summary>
    /// Defines a three-dimensional offset.
    /// </summary>
    [Serializable]
    [DataContract]
    [StructLayout(LayoutKind.Sequential)]
    public struct Offset3D : IEquatable<Offset3D>
    {
        /// <summary>
        /// The size in bytes of the <see cref="Offset3D"/> type.
        /// </summary>
        public static readonly int SizeInBytes = Unsafe.SizeOf<Offset3D>();

        /// <summary>
        /// Represents an empty <see cref="Offset3D"/>.
        /// </summary>
        public static readonly Offset3D Empty = new Offset3D();

        /// <summary>
        /// Gets or sets the x-coordinate of this <see cref="Offset3D"/>.
        /// </summary>
        public int X;

        /// <summary>
        /// Gets or sets the y-coordinate of this <see cref="Offset3D"/>.
        /// </summary>
        public int Y;

        /// <summary>
        /// Gets or sets the z-coordinate of this <see cref="Offset3D"/>.
        /// </summary>
        public int Z;

        /// <summary>
        /// Gets a value indicating whether this <see cref="Offset2D"/> is empty.
        /// </summary>
        public bool IsEmpty => Equals(Empty);

        /// <summary>
		/// Initializes a new instance of the <see cref="Offset3D"/> struct.
		/// </summary>
		/// <param name="x">The x-coordinate.</param>
		/// <param name="y">The y-coordinate.</param>
        /// <param name="z">The z-coordinate.</param>
		public Offset3D(int x, int y, int z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public void Deconstruct(out int x, out int y, out int z)
        {
            x = X;
            y = Y;
            z = Z;
        }

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
                int hashCode = X.GetHashCode();
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
