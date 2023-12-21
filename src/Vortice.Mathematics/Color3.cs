// Copyright (c) Amer Koleci and contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

using System.Globalization;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;

namespace Vortice.Mathematics;

/// <summary>
/// Represents a floating-point RGB color.
/// </summary>
[StructLayout(LayoutKind.Sequential, Pack = 4)]
public struct Color3 : IEquatable<Color3>, IFormattable
{
    /// <summary>
    /// Red component of the color.
    /// </summary>
    public float R;

    /// <summary>
    /// Green component of the color.
    /// </summary>
    public float G;

    /// <summary>
    /// Blue component of the color.
    /// </summary>
    public float B;

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

    public readonly void Deconstruct(out float r, out float g, out float b)
    {
        r = R;
        g = G;
        b = B;
    }

    /// <summary>
    /// Lerps between color source and destination by a supplied amount
    /// </summary>
    /// <param name="start"><see cref="Color3"/> is the initial color</param>
    /// <param name="end"><see cref="Color3"/> is the target color</param>
    /// <param name="amount">Value between 0 and 1 indicating the weight of <paramref name="end"/>.</param>
    public static Color3 Lerp(in Color3 start, in Color3 end, float amount)
    {
        return new Color3(
            MathHelper.Lerp(start.R, end.R, amount),
            MathHelper.Lerp(start.G, end.G, amount),
            MathHelper.Lerp(start.B, end.B, amount)
            );
    }

    /// <summary>
    /// Lerps between color source and destination by a supplied amount
    /// </summary>
    /// <param name="start"><see cref="Color3"/> is the initial color</param>
    /// <param name="end"><see cref="Color3"/> is the target color</param>
    /// <param name="amount">Value between 0 and 1 indicating the weight of <paramref name="end"/>.</param>
    /// <param name="result">return <see cref="Color3"/> of the lerp value</param>
    public static void Lerp(in Color3 start, in Color3 end, float amount, out Color3 result)
    {
        result = new(
            MathHelper.Lerp(start.R, end.R, amount),
            MathHelper.Lerp(start.G, end.G, amount),
            MathHelper.Lerp(start.B, end.B, amount)
            );
    }

    /// <summary>
    /// Converts the color into a three component vector.
    /// </summary>
    /// <returns>A three component vector containing the red, green, and blue components of the color.</returns>
    public readonly Vector3 ToVector3() => new(R, G, B);

    /// <summary>
    /// Creates an array containing the elements of the color.
    /// </summary>
    /// <returns>A four-element array containing the components of the color.</returns>
    public readonly float[] ToArray() => new[] { R, G, B };

    /// <inheritdoc/>
    public override readonly bool Equals(object? obj) => obj is Color3 value && Equals(ref value);

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
    public readonly bool Equals(ref Color3 other)
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
