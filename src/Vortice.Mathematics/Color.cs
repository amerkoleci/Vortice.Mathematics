// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace Vortice.Mathematics
{
    /// <summary>
    /// Represents a 32-bit color (4 bytes) in the form of RGBA (in byte order: R, G, B, A).
    /// </summary>
    [Serializable]
    [DataContract]
    [StructLayout(LayoutKind.Sequential)]
    public struct Color : IEquatable<Color>
    {
        /// <summary>
		/// The total size, in bytes, of an <see cref="Color"/> value.
		/// </summary>
		public static readonly int SizeInBytes = 4;

        /// <summary>
        /// The red component of the color.
        /// </summary>
        public byte R;

        /// <summary>
        /// The green component of the color.
        /// </summary>
        public byte G;

        /// <summary>
        /// The blue component of the color.
        /// </summary>
        public byte B;

        /// <summary>
        /// The alpha component of the color.
        /// </summary>
        public byte A;

        /// <summary>
		/// Initializes a new instance of the <see cref="Color"/> struct.
		/// </summary>
		/// <param name="r">The red component of the color.</param>
        /// <param name="g">The green component of the color.</param>
        /// <param name="b">The blue component of the color.</param>
        /// <param name="a">The alpha component of the color.</param>
		public Color(byte r, byte g, byte b, byte a)
        {
            R = r;
            G = g;
            B = b;
            A = a;
        }

        public void Deconstruct(out byte red, out byte green, out byte blue, out byte alpha)
        {
            red = R;
            green = G;
            blue = B;
            alpha = A;
        }

        /// <summary>
        /// Performs an implicit conversion from <see cref="Color"/> to <see cref="System.Drawing.Color"/>.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The result of the conversion.</returns>
        public static implicit operator System.Drawing.Color(Color value)
        {
            return System.Drawing.Color.FromArgb(value.A, value.R, value.G, value.B);
        }

        /// <summary>
        /// Performs an implicit conversion from <see cref="System.Drawing.Color"/> to <see cref="Color"/>.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The result of the conversion.</returns>
        public static implicit operator Color(System.Drawing.Color value)
        {
            return new Color(value.R, value.G, value.B, value.A);
        }

        /// <inheritdoc/>
		public override bool Equals(object obj) => obj is Color color && Equals(ref color);

        /// <summary>
        /// Determines whether the specified <see cref="Color"/> is equal to this instance.
        /// </summary>
        /// <param name="other">The <see cref="Color"/> to compare with this instance.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Equals(Color other) => Equals(ref other);

        /// <summary>
		/// Determines whether the specified <see cref="Color"/> is equal to this instance.
		/// </summary>
		/// <param name="other">The <see cref="Color"/> to compare with this instance.</param>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Equals(ref Color other)
        {
            return R == other.R 
                && G == other.G 
                && B == other.B 
                && A == other.A;
        }

        /// <summary>
        /// Compares two <see cref="Color"/> objects for equality.
        /// </summary>
        /// <param name="left">The <see cref="Color"/> on the left hand of the operand.</param>
        /// <param name="right">The <see cref="Color"/> on the right hand of the operand.</param>
        /// <returns>
        /// True if the current left is equal to the <paramref name="right"/> parameter; otherwise, false.
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator ==(Color left, Color right) => left.Equals(ref right);

        /// <summary>
        /// Compares two <see cref="Color"/> objects for inequality.
        /// </summary>
        /// <param name="left">The <see cref="Color"/> on the left hand of the operand.</param>
        /// <param name="right">The <see cref="Color"/> on the right hand of the operand.</param>
        /// <returns>
        /// True if the current left is unequal to the <paramref name="right"/> parameter; otherwise, false.
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator !=(Color left, Color right) => !left.Equals(ref right);

        /// <inheritdoc/>
		public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = R.GetHashCode();
                hashCode = (hashCode * 397) ^ G.GetHashCode();
                hashCode = (hashCode * 397) ^ B.GetHashCode();
                hashCode = (hashCode * 397) ^ A.GetHashCode();
                return hashCode;
            }
        }

        /// <inheritdoc/>
        public override string ToString()
        {
            return $"R={R}, G={G}, B={B}, A={A}";
        }
    }
}
