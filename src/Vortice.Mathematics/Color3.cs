// Copyright (c) Amer Koleci and contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

using System.Diagnostics;
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
public readonly struct Color3 : IEquatable<Color3>, IFormattable
{
    /// <summary>
    /// Red component of the color.
    /// </summary>
    public readonly float R;

    /// <summary>
    /// Green component of the color.
    /// </summary>
    public readonly float G;

    /// <summary>
    /// Blue component of the color.
    /// </summary>
    public readonly float B;

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
    public Color3(in Vector3 value)
    {
        R = value.X;
        G = value.Y;
        B = value.Z;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Color3"/> struct.
    /// </summary>
    /// <param name="rgb">A packed integer containing all three color components.
    /// The alpha component is ignored.</param>
    public Color3(int rgb)
    {
        B = ((rgb >> 16) & 255) / 255.0f;
        G = ((rgb >> 8) & 255) / 255.0f;
        R = (rgb & 255) / 255.0f;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Color3"/> struct.
    /// </summary>
    /// <param name="rgb">A packed unsigned integer containing all three color components.
    /// The alpha component is ignored.</param>
    public Color3(uint rgb)
    {
        B = ((rgb >> 16) & 255) / 255.0f;
        G = ((rgb >> 8) & 255) / 255.0f;
        R = (rgb & 255) / 255.0f;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Color3" /> struct.
    /// </summary>
    /// <param name="values">The span of elements to assign to the vector.</param>
    public Color3(ReadOnlySpan<float> values)
    {
        if (values.Length < 3)
        {
            throw new ArgumentOutOfRangeException(nameof(values), "There must be 3 uint values.");
        }

        this = Unsafe.ReadUnaligned<Color3>(ref Unsafe.As<float, byte>(ref MemoryMarshal.GetReference(values)));
    }

    /// <summary>Gets or sets the element at the specified index.</summary>
    /// <param name="index">The index of the element to get or set.</param>
    /// <returns>The the element at <paramref name="index" />.</returns>
    /// <exception cref="ArgumentOutOfRangeException"><paramref name="index" /> was less than zero or greater than the number of elements.</exception>
    public float this[int index]
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get
        {
            if (index >= 3)
            {
                throw new ArgumentOutOfRangeException(nameof(index));
            }

            ref float address = ref Unsafe.AsRef(in R);
            return Unsafe.Add(ref address, index);
        }
    }

    public readonly void Deconstruct(out float r, out float g, out float b)
    {
        r = R;
        g = G;
        b = B;
    }



    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly void CopyTo(float[] array)
    {
        if (array.Length < 3)
        {
            throw new ArgumentOutOfRangeException(nameof(array));
        }

        Unsafe.WriteUnaligned(ref Unsafe.As<float, byte>(ref array[0]), this);
    }

    public readonly void CopyTo(float[] array, int index)
    {
        if ((index < 0) || (index >= array.Length))
        {
            throw new ArgumentOutOfRangeException(nameof(index));
        }

        if ((array.Length - index) < 3)
        {
            throw new ArgumentOutOfRangeException(nameof(index));
        }

        Unsafe.WriteUnaligned(ref Unsafe.As<float, byte>(ref array[index]), this);
    }

    /// <summary>Copies the vector to the given <see cref="Span{T}" />.The length of the destination span must be at least 2.</summary>
    /// <param name="destination">The destination span which the values are copied into.</param>
    /// <exception cref="ArgumentException">If number of elements in source vector is greater than those available in destination span.</exception>
    public readonly void CopyTo(Span<float> destination)
    {
        if (destination.Length < 3)
        {
            throw new ArgumentOutOfRangeException(nameof(destination));
        }

        Unsafe.WriteUnaligned(ref Unsafe.As<float, byte>(ref MemoryMarshal.GetReference(destination)), this);
    }

    /// <summary>Attempts to copy the vector to the given <see cref="Span{Int32}" />. The length of the destination span must be at least 2.</summary>
    /// <param name="destination">The destination span which the values are copied into.</param>
    /// <returns><see langword="true" /> if the source vector was successfully copied to <paramref name="destination" />. <see langword="false" /> if <paramref name="destination" /> is not large enough to hold the source vector.</returns>
    public readonly bool TryCopyTo(Span<float> destination)
    {
        if (destination.Length < 3)
        {
            return false;
        }

        Unsafe.WriteUnaligned(ref Unsafe.As<float, byte>(ref MemoryMarshal.GetReference(destination)), this);
        return true;
    }

    /// <summary>
    /// Converts this color from sRGB space to linear space.
    /// </summary>
    /// <returns>A <see cref="Color3"/> in linear space.</returns>
    public Color3 ToLinear()
    {
        return new(MathHelper.SRgbToLinear(R), MathHelper.SRgbToLinear(G), MathHelper.SRgbToLinear(B));
    }

    /// <summary>
    /// Converts this color from linear space to sRGB space.
    /// </summary>
    /// <returns>A <see cref="Color3"/> in sRGB space.</returns>
    public Color3 ToSRgb()
    {
        return new(MathHelper.LinearToSRgb(R), MathHelper.LinearToSRgb(G), MathHelper.LinearToSRgb(B));
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
    /// Performs an explicit conversion from <see cref="Color3"/> to <see cref="Vector3"/>.
    /// </summary>
    /// <param name="value">The value.</param>
    /// <returns>The result of the conversion.</returns>
    public static explicit operator Vector3(in Color3 value) => new(value.R, value.G, value.B);

    /// <summary>
    /// Performs an explicit conversion from <see cref="Vector3"/> to <see cref="Color3"/>.
    /// </summary>
    /// <param name="value">The value.</param>
    /// <returns>The result of the conversion.</returns>
    public static explicit operator Color3(in Vector3 value) => new(value);

    /// <inheritdoc/>
    public override readonly bool Equals(object? obj) => obj is Color3 value && Equals(value);

    /// <summary>
    /// Determines whether the specified <see cref="Color3"/> is equal to this instance.
    /// </summary>
    /// <param name="other">The <see cref="Color3"/> to compare with this instance.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool Equals(Color3 other) => R.Equals(other.R) && G.Equals(other.G) && B.Equals(other.B);

    /// <summary>
    /// Compares two <see cref="Color3"/> objects for equality.
    /// </summary>
    /// <param name="left">The <see cref="Color3"/> on the left hand of the operand.</param>
    /// <param name="right">The <see cref="Color3"/> on the right hand of the operand.</param>
    /// <returns>
    /// True if the current left is equal to the <paramref name="right"/> parameter; otherwise, false.
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator ==(Color3 left, Color3 right) => left.Equals(right);

    /// <summary>
    /// Compares two <see cref="Color3"/> objects for inequality.
    /// </summary>
    /// <param name="left">The <see cref="Color3"/> on the left hand of the operand.</param>
    /// <param name="right">The <see cref="Color3"/> on the right hand of the operand.</param>
    /// <returns>
    /// True if the current left is unequal to the <paramref name="right"/> parameter; otherwise, false.
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator !=(Color3 left, Color3 right) => !left.Equals(right);

    /// <inheritdoc/>
    public override int GetHashCode() => HashCode.Combine(R, G, B);

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
