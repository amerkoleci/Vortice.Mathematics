// Copyright (c) Amer Koleci and contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Diagnostics;

#if NET6_0_OR_GREATER
using System.Runtime.Intrinsics;
using static Vortice.Mathematics.VectorUtilities;
#endif

namespace Vortice.Mathematics;

/// <summary>
/// Represents a floating-point RGBA color.
/// </summary>
[Serializable]
[StructLayout(LayoutKind.Sequential, Pack = 4)]
public struct Color4 : IEquatable<Color4>, IFormattable
{
    /// <summary>
    /// The size of the <see cref="Color" /> type, in bytes.
    /// </summary>
    public static unsafe readonly int SizeInBytes = sizeof(Color);

#if NET6_0_OR_GREATER
    private Vector128<float> _value;
#endif

    /// <summary>
    /// Initializes a new instance of the <see cref="Color4"/> struct.
    /// </summary>
    /// <param name="value">The value that will be assigned to all components.</param>
    public Color4(float value)
    {
#if NET6_0_OR_GREATER
        _value = Vector128.Create(value, value, value, value);
#else
        A = R = G = B = value;
#endif
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Color4"/> struct.
    /// </summary>
    /// <param name="red">The red component of the color.</param>
    /// <param name="green">The green component of the color.</param>
    /// <param name="blue">The blue component of the color.</param>
    public Color4(float red, float green, float blue)
    {
#if NET6_0_OR_GREATER
        _value = Vector128.Create(red, green, blue, 1.0f);
#else
        R = red;
        G = green;
        B = blue;
        A = 1.0f;
#endif
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
#if NET6_0_OR_GREATER
        _value = Vector128.Create(red, green, blue, alpha);
#else
        R = red;
        G = green;
        B = blue;
        A = alpha;
#endif
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Color4"/> struct.
    /// </summary>
    /// <param name="value">The red, green, blue, and alpha components of the color.</param>
    public Color4(in Vector4 value)
    {
#if NET6_0_OR_GREATER
        _value = value.AsVector128();
#else
        R = value.X;
        G = value.Y;
        B = value.Z;
        A = value.W;
#endif
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Color4"/> struct.
    /// </summary>
    /// <param name="value">The red, green, and blue components of the color.</param>
    /// <param name="alpha">The alpha component of the color.</param>
    public Color4(in Vector3 value, float alpha)
    {
#if NET6_0_OR_GREATER
        _value = Vector128.Create(value.X, value.Y, value.Z, alpha);
#else
        R = value.X;
        G = value.Y;
        B = value.Z;
        A = alpha;
#endif
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Color4"/> struct.
    /// </summary>
    /// <param name="value">The red, green, and blue components of the color.</param>
    /// <param name="alpha">The alpha component of the color.</param>
    public Color4(in Color3 value, float alpha)
    {
#if NET6_0_OR_GREATER
        _value = Vector128.Create(value.R, value.G, value.B, alpha);
#else
        R = value.R;
        G = value.G;
        B = value.B;
        A = alpha;
#endif
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Color4"/> struct.
    /// </summary>
    /// <param name="color"><see cref="Color"/> used to initialize the color.</param>
    public Color4(in Color color)
    {
#if NET6_0_OR_GREATER
        _value = Vector128.Create(color.R / 255.0f, color.G / 255.0f, color.B / 255.0f, color.A / 255.0f);
#else
        R = color.R / 255.0f;
        G = color.G / 255.0f;
        B = color.B / 255.0f;
        A = color.A / 255.0f;
#endif
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Color4" /> struct.
    /// </summary>
    /// <param name="values">The span of elements to assign to the vector.</param>
    public Color4(ReadOnlySpan<float> values)
    {
        if (values.Length < 4)
        {
            throw new ArgumentOutOfRangeException(nameof(values), "There must be 4 uint values.");
        }

        this = Unsafe.ReadUnaligned<Color4>(ref Unsafe.As<float, byte>(ref MemoryMarshal.GetReference(values)));
    }

#if NET6_0_OR_GREATER
    /// <summary>
    /// Initializes a new instance of the <see cref="Color" /> struct.
    /// </summary>
    /// <param name="value">The value of the vector.</param>
    public Color4(Vector128<float> value)
    {
        _value = value;
    }

    /// <summary>
    /// Red component of the color.
    /// </summary>
    public readonly float R
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get
        {
            return _value.GetX();
        }
    }

    /// <summary>
    /// Green component of the color.
    /// </summary>
    public readonly float G
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get
        {
            return _value.GetY();
        }
    }

    /// <summary>
    /// Blue component of the color.
    /// </summary>
    public readonly float B
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get
        {
            return _value.GetZ();
        }
    }

    /// <summary>
    /// Alpha component of the color.
    /// </summary>
    public readonly float A
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get
        {
            return _value.GetW();
        }
    }
#else
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
#endif

    public float this[int index]
    {
        readonly get => GetElement(this, index);
        set => this = WithElement(this, index, value);
    }

    /// <summary>
    /// Converts the color into a packed integer.
    /// </summary>
    /// <returns>A packed integer containing all four color components.</returns>
    public int ToBgra()
    {
        uint a = (uint)(A * 255.0f) & 255;
        uint r = (uint)(R * 255.0f) & 255;
        uint g = (uint)(G * 255.0f) & 255;
        uint b = (uint)(B * 255.0f) & 255;

        uint value = b;
        value |= g << 8;
        value |= r << 16;
        value |= a << 24;

        return (int)value;
    }

    /// <summary>
    /// Converts the color into a packed integer.
    /// </summary>
    /// <returns>A packed integer containing all four color components.</returns>
    public void ToBgra(out byte r, out byte g, out byte b, out byte a)
    {
        b = (byte)(B * 255.0f);
        g = (byte)(G * 255.0f);
        r = (byte)(R * 255.0f);
        a = (byte)(A * 255.0f);
    }

    /// <summary>
    /// Converts the color into a packed integer.
    /// </summary>
    /// <returns>A packed integer containing all four color components.</returns>
    public int ToRgba()
    {
        uint r = (uint)(R * 255.0f) & 255;
        uint g = (uint)(G * 255.0f) & 255;
        uint b = (uint)(B * 255.0f) & 255;
        uint a = (uint)(A * 255.0f) & 255;

        uint value = r;
        value |= g << 8;
        value |= b << 16;
        value |= a << 24;

        return (int)value;
    }

    /// <summary>
    /// Converts the color into a packed integer.
    /// </summary>
    /// <returns>A packed integer containing all four color components.</returns>
    public void ToRgba(out byte r, out byte g, out byte b, out byte a)
    {
        r = (byte)(R * 255.0f);
        g = (byte)(G * 255.0f);
        b = (byte)(B * 255.0f);
        a = (byte)(A * 255.0f);
    }

    /// <summary>
    /// Determines the negative RGB color value of a color.
    /// </summary>
    /// <param name="color">The color value</param>
    /// <returns>The negative color</returns>
    public static Color4 Negative(in Color4 color)
    {
        return new Color4(1.0f - color.R, 1.0f - color.G, 1.0f - color.B, color.A);
    }

    /// <summary>
    /// Blends two colors by multiplying corresponding components together.
    /// </summary>
    /// <param name="color1">The first color.</param>
    /// <param name="color2">The second color.</param>
    public static Color4 Modulate(in Color4 color1, in Color4 color2)
    {
        return Multiply(color1, color2);
    }

    /// <summary>
    /// Restricts a value to be within a specified range.
    /// </summary>
    /// <param name="value">The value to clamp.</param>
    /// <param name="min">The minimum value.</param>
    /// <param name="max">The maximum value.</param>
    /// <returns>The clamped value.</returns>
    public static Color4 Clamp(in Color4 value, in Color4 min, in Color4 max)
    {
        float alpha = value.A;
        alpha = (alpha > max.A) ? max.A : alpha;
        alpha = (alpha < min.A) ? min.A : alpha;

        float red = value.R;
        red = (red > max.R) ? max.R : red;
        red = (red < min.R) ? min.R : red;

        float green = value.G;
        green = (green > max.G) ? max.G : green;
        green = (green < min.G) ? min.G : green;

        float blue = value.B;
        blue = (blue > max.B) ? max.B : blue;
        blue = (blue < min.B) ? min.B : blue;

        return new Color4(red, green, blue, alpha);
    }

    /// <summary>
    /// Lerps between color source and destination by a supplied amount
    /// </summary>
    /// <param name="start"><see cref="Color4"/> is the initial color</param>
    /// <param name="end"><see cref="Color4"/> is the target color</param>
    /// <param name="amount">Value between 0 and 1 indicating the weight of <paramref name="end"/>.</param>
    public static Color4 Lerp(in Color4 start, in Color4 end, float amount)
    {
        return new(
            MathHelper.Lerp(start.R, end.R, amount),
            MathHelper.Lerp(start.G, end.G, amount),
            MathHelper.Lerp(start.B, end.B, amount),
            MathHelper.Lerp(start.A, end.A, amount)
            );
    }

    /// <summary>
    /// Performs a cubic interpolation between two colors.
    /// </summary>
    /// <param name="start">Start color.</param>
    /// <param name="end">End color.</param>
    /// <param name="amount">Value between 0 and 1 indicating the weight of <paramref name="end"/>.</param>
    /// <returns>When the method completes, contains the cubic interpolation of the two colors.</returns>
    public static Color4 SmoothStep(in Color4 start, in Color4 end, float amount)
    {
        amount = MathHelper.SmoothStep(amount);
        return Lerp(start, end, amount);
    }

    /// <summary>
    /// Returns a color containing the smallest components of the specified colors.
    /// </summary>
    /// <param name="left">The first source color.</param>
    /// <param name="right">The second source color.</param>
    /// <returns>When the method completes, contains an new color composed of the largest components of the source colors.</returns>
    public static Color4 Max(in Color4 left, in Color4 right)
    {
        return new(
            (left.R > right.R) ? left.R : right.G,
            (left.G > right.G) ? left.G : right.G,
            (left.B > right.B) ? left.B : right.B,
            (left.A > right.A) ? left.A : right.A
            );
    }

    /// <summary>
    /// Returns a color containing the smallest components of the specified colors.
    /// </summary>
    /// <param name="left">The first source color.</param>
    /// <param name="right">The second source color.</param>
    /// <returns>When the method completes, contains an new color composed of the smallest components of the source colors.</returns>
    public static Color4 Min(in Color4 left, in Color4 right)
    {
        return new(
            (left.R < right.R) ? left.R : right.R,
            (left.G < right.G) ? left.G : right.G,
            (left.B < right.B) ? left.B : right.B,
            (left.A < right.A) ? left.A : right.A
            );
    }

    /// <summary>
    /// Computes the premultiplied value of the provided color.
    /// </summary>
    /// <param name="value">The non-premultiplied value.</param>
    /// <returns>The premultiplied result.</returns>
    public static Color4 Premultiply(in Color4 value)
    {
        return new(
            value.R * value.A,
            value.G * value.A,
            value.B * value.A,
            value.A
            );
    }

    /// <summary>
    /// Converts this color from linear space to sRGB space.
    /// </summary>
    /// <returns>A <see cref="Color4"/> in sRGB space.</returns>
    public Color4 ToSRgb()
    {
        return new(MathHelper.LinearToSRgb(R), MathHelper.LinearToSRgb(G), MathHelper.LinearToSRgb(B), A);
    }

    /// <summary>
    /// Converts this color from sRGB space to linear space.
    /// </summary>
    /// <returns>A Color in linear space.</returns>
    public Color4 ToLinear()
    {
        return new(MathHelper.SRgbToLinear(R), MathHelper.SRgbToLinear(G), MathHelper.SRgbToLinear(B), A);
    }

    public void Deconstruct(out float red, out float green, out float blue, out float alpha)
    {
        red = R;
        green = G;
        blue = B;
        alpha = A;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly void CopyTo(float[] array)
    {
        CopyTo(array, 0);
    }

    public readonly void CopyTo(float[] array, int index)
    {
        if (array is null)
        {
            throw new NullReferenceException(nameof(array));
        }

        if ((index < 0) || (index >= array.Length))
        {
            throw new ArgumentOutOfRangeException(nameof(index));
        }

        if ((array.Length - index) < 4)
        {
            throw new ArgumentOutOfRangeException(nameof(index));
        }

        array[index] = R;
        array[index + 1] = G;
        array[index + 2] = B;
        array[index + 3] = A;
    }

    /// <summary>Copies the vector to the given <see cref="Span{T}" />.The length of the destination span must be at least 2.</summary>
    /// <param name="destination">The destination span which the values are copied into.</param>
    /// <exception cref="ArgumentException">If number of elements in source vector is greater than those available in destination span.</exception>
    public readonly void CopyTo(Span<float> destination)
    {
        if (destination.Length < 4)
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
        if (destination.Length < 4)
        {
            return false;
        }

        Unsafe.WriteUnaligned(ref Unsafe.As<float, byte>(ref MemoryMarshal.GetReference(destination)), this);
        return true;
    }

    /// <summary>
    /// Returns a new color whose values are the product of each pair of elements in two specified colors.
    /// </summary>
    /// <param name="left">The first color.</param>
    /// <param name="right">The second color.</param>
    /// <returns>The element-wise product vector.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Color4 Multiply(Color4 left, Color4 right)
    {
#if NET6_0_OR_GREATER
        Vector128<float> result = VectorUtilities.Multiply(left._value, right._value);
        return new Color4(result);
#else
        return new Color4(left.R * right.R, left.G * right.G, left.B * right.B, left.A * right.A);
#endif
    }

    /// <summary>Multiplies a color by a specified scalar.</summary>
    /// <param name="left">The color to multiply.</param>
    /// <param name="right">The scalar value.</param>
    /// <returns>The scaled color.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Color4 Multiply(Color4 left, float right)
    {
#if NET6_0_OR_GREATER
        Vector128<float> result = VectorUtilities.Multiply(left._value, right);
        return new Color4(result);
#else
        return new Color4(left.R * right, left.G * right, left.B * right, left.A * right);
#endif
    }

    /// <summary>Multiplies a scalar value by a specified color.</summary>
    /// <param name="left">The scaled value.</param>
    /// <param name="right">The color.</param>
    /// <returns>The scaled color.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Color4 Multiply(float left, Color4 right)
    {
#if NET6_0_OR_GREATER
        Vector128<float> result = VectorUtilities.Multiply(right._value, left);
        return new Color4(result);
#else
        return new Color4(left * right.R, left * right.G, left * right.B, left * right.A);
#endif
    }

    /// <summary>
    /// Converts the color to <see cref="Vector3"/>.
    /// </summary>
    /// <returns>An instance of <see cref="Vector3"/> with R, G, B component.</returns>
    public Vector3 ToVector3() => new(R, G, B);

    /// <summary>
    /// Converts the color to <see cref="Vector4"/>
    /// </summary>
    /// <returns>An instance of <see cref="Vector4"/> with R, G, B, A component.</returns>
    public Vector4 ToVector4() => new(R, G, B, A);

    /// <summary>
    /// Performs an explicit conversion from <see cref="Color4"/> to <see cref="Vector3"/>.
    /// </summary>
    /// <param name="value">The value.</param>
    /// <returns>The result of the conversion.</returns>
    public static explicit operator Vector3(Color4 value) => new(value.R, value.G, value.B);

    /// <summary>
    /// Performs an implicit conversion from <see cref="Color4"/> to <see cref="Vector4"/>.
    /// </summary>
    /// <param name="value">The value.</param>
    /// <returns>The result of the conversion.</returns>
    public static implicit operator Vector4(Color4 value) => new(value.R, value.G, value.B, value.A);

    /// <summary>
    /// Performs an explicit conversion from <see cref="Vector3"/> to <see cref="Color4"/>.
    /// </summary>
    /// <param name="value">The value.</param>
    /// <returns>The result of the conversion.</returns>
    public static explicit operator Color4(Vector3 value) => new(value.X, value.Y, value.Z, 1.0f);

    /// <summary>
    /// Performs an explicit conversion from <see cref="Vector4"/> to <see cref="Color4"/>.
    /// </summary>
    /// <param name="value">The value.</param>
    /// <returns>The result of the conversion.</returns>
    public static explicit operator Color4(Vector4 value) => new(value.X, value.Y, value.Z, value.W);

    /// <inheritdoc />
    public override bool Equals(object? obj) => obj is Color4 other && Equals(other);

    /// <inheritdoc />
    public bool Equals(Color4 other) => this == other;

    /// <summary>
    /// Adds two colors.
    /// </summary>
    /// <param name="left">The first color to add.</param>
    /// <param name="right">The second color to add.</param>
    /// <returns>The sum of the two colors.</returns>
    public static Color4 operator +(Color4 left, Color4 right)
    {
        return new(left.R + right.R, left.G + right.G, left.B + right.B, left.A + right.A);
    }

    /// <summary>
    /// Assert a color (return it unchanged).
    /// </summary>
    /// <param name="value">The color to assert (unchanged).</param>
    /// <returns>The asserted (unchanged) color.</returns>
    public static Color4 operator +(Color4 value) => value;

    /// <summary>
    /// Subtracts two colors.
    /// </summary>
    /// <param name="left">The first color to subtract.</param>
    /// <param name="right">The second color to subtract.</param>
    /// <returns>The difference of the two colors.</returns>
    public static Color4 operator -(Color4 left, Color4 right)
    {
        return new Color4(left.R - right.R, left.G - right.G, left.B - right.B, left.A - right.A);
    }

    /// <summary>
    /// Negates a color.
    /// </summary>
    /// <param name="value">The color to negate.</param>
    /// <returns>A negated color.</returns>
    public static Color4 operator -(Color4 value)
    {
        return new Color4(-value.R, -value.G, -value.B, -value.A);
    }

    /// <summary>Computes the product of a color and a float.</summary>
    /// <param name="left">The vector to multiply by <paramref name="right" />.</param>
    /// <param name="right">The float which is used to multiply <paramref name="left" />.</param>
    /// <returns>The product of <paramref name="left" /> multipled by <paramref name="right" />.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Color4 operator *(float left, Color4 right)
    {
        return Multiply(left, right);
    }

    /// <summary>Computes the product of a color and a float.</summary>
    /// <param name="left">The vector to multiply by <paramref name="right" />.</param>
    /// <param name="right">The float which is used to multiply <paramref name="left" />.</param>
    /// <returns>The product of <paramref name="left" /> multipled by <paramref name="right" />.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Color4 operator *(Color4 left, float right)
    {
#if NET6_0_OR_GREATER
        Vector128<float> result = VectorUtilities.Multiply(left._value, right);
        return new Color4(result);
#else
        return new Color4(left.R * right, left.G * right, left.B * right, left.A * right);
#endif
    }

    /// <summary>Computes the product of two colors.</summary>
    /// <param name="left">The color to multiply by <paramref name="right" />.</param>
    /// <param name="right">The color which is used to multiply <paramref name="left" />.</param>
    /// <returns>The product of <paramref name="left" /> multipled by <paramref name="right" />.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Color4 operator *(Color4 left, Color4 right)
    {
#if NET6_0_OR_GREATER
        Vector128<float> result = VectorUtilities.Multiply(left._value, right._value);
        return new Color4(result);
#else
        return new Color4(left.R * right.R, left.G * right.G, left.B * right.B, left.A * right.A);
#endif
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
    public static bool operator ==(Color4 left, Color4 right)
    {
#if NET6_0_OR_GREATER
        return CompareEqualAll(left._value, right._value);
#else
        return left.A == right.A && left.R == right.R && left.G == right.G && left.B == right.B;
#endif
    }

    /// <summary>
    /// Compares two <see cref="Color4"/> objects for inequality.
    /// </summary>
    /// <param name="left">The <see cref="Color4"/> on the left hand of the operand.</param>
    /// <param name="right">The <see cref="Color4"/> on the right hand of the operand.</param>
    /// <returns>
    /// True if the current left is unequal to the <paramref name="right"/> parameter; otherwise, false.
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator !=(Color4 left, Color4 right)
    {
#if NET6_0_OR_GREATER
        return CompareNotEqualAny(left._value, right._value);
#else
        return !left.Equals(right);
#endif
    }

    /// <inheritdoc/>
    public override int GetHashCode() => HashCode.Combine(R, G, B, A);

    /// <inheritdoc/>
    public override string ToString() => ToString(format: null, formatProvider: null);

    /// <inheritdoc />
    public string ToString(string? format, IFormatProvider? formatProvider)
    {
        string? separator = NumberFormatInfo.GetInstance(formatProvider).NumberGroupSeparator;

        return new StringBuilder(9 + separator.Length * 3)
            .Append('<')
            .Append(R.ToString(format, formatProvider))
            .Append(separator)
            .Append(' ')
            .Append(G.ToString(format, formatProvider))
            .Append(separator)
            .Append(' ')
            .Append(B.ToString(format, formatProvider))
            .Append(separator)
            .Append(' ')
            .Append(A.ToString(format, formatProvider))
            .Append('>')
            .ToString();
    }

    internal const int Count = 4;

    internal static float GetElement(Color4 vector, int index)
    {
        if (index >= Count)
        {
            throw new ArgumentOutOfRangeException(nameof(index));
        }

        return GetElementUnsafe(ref vector, index);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static float GetElementUnsafe(ref Color4 vector, int index)
    {
        Debug.Assert(index is >= 0 and < Count);

        return Unsafe.Add(ref Unsafe.As<Color4, float>(ref vector), index);
    }

    /// <summary>Sets the element at the specified index.</summary>
    /// <param name="vector">The vector of the element to get.</param>
    /// <param name="index">The index of the element to set.</param>
    /// <param name="value">The value of the element to set.</param>
    /// <exception cref="ArgumentOutOfRangeException"><paramref name="index" /> was less than zero or greater than the number of elements.</exception>
    internal static Color4 WithElement(Color4 vector, int index, float value)
    {
        if ((uint)index >= Count)
        {
            throw new ArgumentOutOfRangeException(nameof(index));
        }

        Color4 result = vector;
        SetElementUnsafe(ref result, index, value);
        return result;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static void SetElementUnsafe(ref Color4 vector, int index, float value)
    {
        Debug.Assert(index is >= 0 and < Count);

        Unsafe.Add(ref Unsafe.As<Color4, float>(ref vector), index) = value;
    }
}
