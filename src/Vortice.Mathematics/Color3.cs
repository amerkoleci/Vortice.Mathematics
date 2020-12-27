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
    /// Represents a floating-point RGB color.
    /// </summary>
    [Serializable]
    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public readonly struct Color3 : IEquatable<Color3>, IFormattable
    {
        /// <summary>
        /// Static declaration of white
        /// </summary>
        public static readonly Color3 White = new Color3(1);

        /// <summary>
        /// Static declaration of black
        /// </summary>
        public static readonly Color3 Black = new Color3(0);

        /// <summary>
        /// Initializes a new instance of the <see cref="Color3"/> struct.
        /// </summary>
        /// <param name="value">The value that will be assigned to all components.</param>
        public Color3(float value)
        {
            R = G = B = value;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Color3"/> struct.
        /// </summary>
        /// <param name="red">The red component of the color.</param>
        /// <param name="green">The green component of the color.</param>
        /// <param name="blue">The blue component of the color.</param>
        public Color3(float red, float green, float blue)
        {
            R = red;
            G = green;
            B = blue;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Color3"/> struct.
        /// </summary>
        /// <param name="value">The red, green, and blue components of the color.</param>
        public Color3(Vector3 value)
        {
            R = value.X;
            G = value.Y;
            B = value.Z;
        }

        /// <summary>
        /// Red component of the color.
        /// </summary>
        public float R { get; }

        /// <summary>
        /// Green component of the color.
        /// </summary>
        public float G { get; }

        /// <summary>
        /// Blue component of the color.
        /// </summary>
        public float B { get; }

        public void Deconstruct(out float r, out float g, out float b)
        {
            r = R;
            g = G;
            b = B;
        }

        /// <summary>
        /// Converts the color into a three component vector.
        /// </summary>
        /// <returns>A three component vector containing the red, green, and blue components of the color.</returns>
        public Vector3 ToVector3() => new Vector3(R, G, B);

        /// <summary>
        /// Creates an array containing the elements of the color.
        /// </summary>
        /// <returns>A four-element array containing the components of the color.</returns>
        public float[] ToArray() => new[] { R, G, B };

        /// <inheritdoc/>
		public override bool Equals(object? obj) => obj is Color3 value && Equals(ref value);

        /// <summary>
        /// Determines whether the specified <see cref="Color3"/> is equal to this instance.
        /// </summary>
        /// <param name="other">The <see cref="Color3"/> to compare with this instance.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Equals(Color3 other) => Equals(ref other);

        /// <summary>
		/// Determines whether the specified <see cref="Color3"/> is equal to this instance.
		/// </summary>
		/// <param name="other">The <see cref="Color3"/> to compare with this instance.</param>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Equals(ref Color3 other)
        {
            return R.Equals(other.R)
                && G.Equals(other.G)
                && B.Equals(other.B);
        }

        /// <summary>
        /// Compares two <see cref="Color3"/> objects for equality.
        /// </summary>
        /// <param name="left">The <see cref="Color3"/> on the left hand of the operand.</param>
        /// <param name="right">The <see cref="Color3"/> on the right hand of the operand.</param>
        /// <returns>
        /// True if the current left is equal to the <paramref name="right"/> parameter; otherwise, false.
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator ==(Color3 left, Color3 right) => left.Equals(ref right);

        /// <summary>
        /// Compares two <see cref="Color3"/> objects for inequality.
        /// </summary>
        /// <param name="left">The <see cref="Color3"/> on the left hand of the operand.</param>
        /// <param name="right">The <see cref="Color3"/> on the right hand of the operand.</param>
        /// <returns>
        /// True if the current left is unequal to the <paramref name="right"/> parameter; otherwise, false.
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator !=(Color3 left, Color3 right) => !left.Equals(ref right);

        /// <inheritdoc/>
        public override int GetHashCode()
        {
            var hashCode = new HashCode();
            {
                hashCode.Add(R);
                hashCode.Add(G);
                hashCode.Add(B);
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
                .Append(R.ToString(format, formatProvider)).Append(separator).Append(' ')
                .Append(G.ToString(format, formatProvider)).Append(separator).Append(' ')
                .Append(B.ToString(format, formatProvider))
                .Append('>')
                .ToString();
        }
    }
}
