// Copyright (c) Amer Koleci and contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;

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
public readonly struct Color4 : IEquatable<Color4>, IFormattable
{
#if NET6_0_OR_GREATER
    private readonly Vector128<float> _value;
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
    public Color4(Vector4 value)
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
    public Color4(Vector3 value, float alpha)
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
    public Color4(Color3 value, float alpha)
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
    public Color4(Color color)
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
    public float R
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
    public float G
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
    public float B
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
    public float A
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
    /// Lerps between color source and destination by a supplied amount
    /// </summary>
    /// <param name="start"><see cref="Color4"/> is the initial color</param>
    /// <param name="end"><see cref="Color4"/> is the target color</param>
    /// <param name="amount">Value between 0 and 1 indicating the weight of <paramref name="end"/>.</param>
    public static Color4 Lerp(in Color4 start, in Color4 end, float amount)
    {
        return new Color4(
            MathHelper.Lerp(start.R, end.R, amount),
            MathHelper.Lerp(start.G, end.G, amount),
            MathHelper.Lerp(start.B, end.B, amount),
            MathHelper.Lerp(start.A, end.A, amount)
            );
    }

    /// <summary>
    /// Lerps between color source and destination by a supplied amount
    /// </summary>
    /// <param name="start"><see cref="Color4"/> is the initial color</param>
    /// <param name="end"><see cref="Color4"/> is the target color</param>
    /// <param name="amount">Value between 0 and 1 indicating the weight of <paramref name="end"/>.</param>
    /// <param name="result">return <see cref="Color4"/> of the lerp value</param>
    public static void Lerp(in Color4 start, in Color4 end, float amount, out Color4 result)
    {
        result = new Color4(
            MathHelper.Lerp(start.R, end.R, amount),
            MathHelper.Lerp(start.G, end.G, amount),
            MathHelper.Lerp(start.B, end.B, amount),
            MathHelper.Lerp(start.A, end.A, amount)
            );
    }

    /// <summary>
    /// Converts the color to <see cref="Vector3"/>.
    /// </summary>
    /// <returns>An instance of <see cref="Vector3"/> with R, G, B component.</returns>
    public Vector3 ToVector3() => new Vector3(R, G, B);

    /// <summary>
    /// Converts the color to <see cref="Vector4"/>
    /// </summary>
    /// <returns>An instance of <see cref="Vector4"/> with R, G, B, A component.</returns>
    public Vector4 ToVector4() => new Vector4(R, G, B, A);

    /// <summary>
    /// Creates an array containing the elements of the color.
    /// </summary>
    /// <returns>A four-element array containing the components of the color.</returns>
    public float[] ToArray() => new[] { R, G, B, A };

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

    /// <inheritdoc />
    public override bool Equals(object? obj) => obj is Color4 other && Equals(other);

    /// <inheritdoc />
    public bool Equals(Color4 other) => this == other;

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
}
