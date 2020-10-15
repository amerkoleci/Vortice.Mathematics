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
    /// Represents a floating-point RGBA color.
    /// </summary>
    [Serializable]
    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public readonly struct Color4 : IEquatable<Color4>, IFormattable
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Color4"/> struct.
        /// </summary>
        /// <param name="value">The value that will be assigned to all components.</param>
        public Color4(float value)
        {
            A = R = G = B = value;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Color4"/> struct.
        /// </summary>
        /// <param name="red">The red component of the color.</param>
        /// <param name="green">The green component of the color.</param>
        /// <param name="blue">The blue component of the color.</param>
        public Color4(float red, float green, float blue)
        {
            R = red;
            G = green;
            B = blue;
            A = 1.0f;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Color4"/> struct.
        /// </summary>
        /// <param name="red">The red component of the color.</param>
        /// <param name="green">The green component of the color.</param>
        /// <param name="blue">The blue component of the color.</param>
        /// <param name="alpha">The alpha component of the color.</param>
        public Color4(float red, float green, float blue, float alpha)
        {
            R = red;
            G = green;
            B = blue;
            A = alpha;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Color4"/> struct.
        /// </summary>
        /// <param name="value">The red, green, blue, and alpha components of the color.</param>
        public Color4(Vector4 value)
        {
            R = value.X;
            G = value.Y;
            B = value.Z;
            A = value.W;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Color4"/> struct.
        /// </summary>
        /// <param name="value">The red, green, and blue components of the color.</param>
        /// <param name="alpha">The alpha component of the color.</param>
        public Color4(Vector3 value, float alpha)
        {
            R = value.X;
            G = value.Y;
            B = value.Z;
            A = alpha;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Color4"/> struct.
        /// </summary>
        /// <param name="color"><see cref="Color"/> used to initialize the color.</param>
        public Color4(Color color)
        {
            R = color.R / 255.0f;
            G = color.G / 255.0f;
            B = color.B / 255.0f;
            A = color.A / 255.0f;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Color4"/> struct.
        /// </summary>
        /// <param name="color"><see cref="System.Drawing.Color"/> used to initialize the color.</param>
        public Color4(System.Drawing.Color color)
        {
            R = color.R / 255.0f;
            G = color.G / 255.0f;
            B = color.B / 255.0f;
            A = color.A / 255.0f;
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

        /// <summary>
        /// Alpha component of the color.
        /// </summary>
        public float A { get; }

        public void Deconstruct(out float r, out float g, out float b, out float a)
        {
            r = R;
            g = G;
            b = B;
            a = A;
        }

        /// <summary>
        /// Converts the color into a packed integer.
        /// </summary>
        /// <returns>A packed integer containing all four color components.</returns>
        public void ToBgra(out byte r, out byte g, out byte b, out byte a)
        {
            r = (byte)(R * 255.0f);
            g = (byte)(G * 255.0f);
            b = (byte)(B * 255.0f);
            a = (byte)(A * 255.0f);
        }

        /// <summary>
        /// Converts the color to <see cref="Vector3"/>.
        /// </summary>
        /// <returns>An instance of <see cref="Vector3"/> with R, G, B component.</returns>
        public Vector3 ToVector3()
        {
            return new Vector3(R, G, B);
        }

        /// <summary>
        /// Converts the color to <see cref="Vector4"/>
        /// </summary>
        /// <returns>An instance of <see cref="Vector4"/> with R, G, B, A component.</returns>
        public Vector4 ToVector4()
        {
            return new Vector4(R, G, B, A);
        }

        /// <summary>
        /// Performs an explicit conversion from <see cref="Color4"/> to <see cref="Vector3"/>.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The result of the conversion.</returns>
        public static explicit operator Vector3(Color4 value) => new Vector3(value.R, value.G, value.B);

        /// <summary>
        /// Performs an implicit conversion from <see cref="Color4"/> to <see cref="Vector4"/>.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The result of the conversion.</returns>
        public static implicit operator Vector4(Color4 value) => new Vector4(value.R, value.G, value.B, value.A);

        /// <summary>
        /// Performs an explicit conversion from <see cref="Vector3"/> to <see cref="Color4"/>.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The result of the conversion.</returns>
        public static explicit operator Color4(Vector3 value) => new Color4(value.X, value.Y, value.Z, 1.0f);

        /// <summary>
        /// Performs an explicit conversion from <see cref="Vector4"/> to <see cref="Color4"/>.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The result of the conversion.</returns>
        public static explicit operator Color4(Vector4 value) => new Color4(value.X, value.Y, value.Z, value.W);

        /// <summary>
        /// Performs an explicit conversion from <see cref="System.Drawing.Color"/> to <see cref="Color4"/>.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The result of the conversion.</returns>
        public static explicit operator Color4(System.Drawing.Color value) => new Color4(value);

        /// <summary>
        /// Performs an explicit conversion from <see cref="Vector4"/> to <see cref="Color4"/>.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The result of the conversion.</returns>
        public static explicit operator System.Drawing.Color(Color4 value)
        {
            value.ToBgra(out byte red, out byte green, out byte blue, out byte alpha);
            return System.Drawing.Color.FromArgb(alpha, red, green, blue);
        }

        /// <inheritdoc/>
		public override bool Equals(object? obj) => obj is Color4 value && Equals(value);

        /// <summary>
        /// Determines whether the specified <see cref="Color4"/> is equal to this instance.
        /// </summary>
        /// <param name="other">The <see cref="Color4"/> to compare with this instance.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Equals(Color4 other)
        {
            return R.Equals(other.R)
                && G.Equals(other.G)
                && B.Equals(other.B)
                && A.Equals(other.A);
        }

        /// <summary>
        /// Compares two <see cref="Color4"/> objects for equality.
        /// </summary>
        /// <param name="left">The <see cref="Color4"/> on the left hand of the operand.</param>
        /// <param name="right">The <see cref="Color4"/> on the right hand of the operand.</param>
        /// <returns>
        /// True if the current left is equal to the <paramref name="right"/> parameter; otherwise, false.
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator ==(Color4 left, Color4 right) => left.Equals(right);

        /// <summary>
        /// Compares two <see cref="Color4"/> objects for inequality.
        /// </summary>
        /// <param name="left">The <see cref="Color4"/> on the left hand of the operand.</param>
        /// <param name="right">The <see cref="Color4"/> on the right hand of the operand.</param>
        /// <returns>
        /// True if the current left is unequal to the <paramref name="right"/> parameter; otherwise, false.
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator !=(Color4 left, Color4 right) => !left.Equals(right);

        /// <inheritdoc/>
		public override int GetHashCode()
        {
            var hashCode = new HashCode();
            {
                hashCode.Add(R);
                hashCode.Add(G);
                hashCode.Add(B);
                hashCode.Add(A);
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
                .Append(B.ToString(format, formatProvider)).Append(separator).Append(' ')
                .Append(A.ToString(format, formatProvider))
                .Append('>')
                .ToString();
        }
    }
}
