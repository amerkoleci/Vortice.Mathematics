// Copyright (c) Amer Koleci and contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

using System.Numerics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Intrinsics;
using Vortice.Mathematics.PackedVector;
using static Vortice.Mathematics.VectorUtilities;

namespace Vortice.Mathematics;

/// <summary>
/// Represents a 32-bit BGRA color (4 bytes).
/// </summary>
[StructLayout(LayoutKind.Explicit)]
public readonly struct ColorBgra : IPackedVector<uint>, IEquatable<ColorBgra>
{
    [FieldOffset(0)]
    private readonly uint _packedValue;

    /// <summary>
    /// The blue component of the color.
    /// </summary>
    [FieldOffset(0)]
    public readonly byte B;

    /// <summary>
    /// The green component of the color.
    /// </summary>
    [FieldOffset(1)]
    public readonly byte G;

    /// <summary>
    /// The red component of the color.
    /// </summary>
    [FieldOffset(2)]
    public readonly byte R;

    /// <summary>
    /// The alpha component of the color.
    /// </summary>
    [FieldOffset(3)]
    public readonly byte A;

    /// <summary>
    /// Gets packed value.
    /// </summary>
    public uint PackedValue => _packedValue;

    /// <summary>
    /// Initializes a new instance of the <see cref="ColorBgra"/> struct.
    /// </summary>
    /// <param name="value">The value that will be assigned to all components.</param>
    public ColorBgra(float value)
        : this(value, value, value, value)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ColorBgra"/> struct.
    /// </summary>
    /// <param name="red">The red component of the color.</param>
    /// <param name="green">The green component of the color.</param>
    /// <param name="blue">The blue component of the color.</param>
    /// <param name="alpha">The alpha component of the color.</param>
    public ColorBgra(int red, int green, int blue, byte alpha = 255)
    {
        R = PackHelpers.ToByte(red);
        G = PackHelpers.ToByte(green);
        B = PackHelpers.ToByte(blue);
        A = PackHelpers.ToByte(alpha);
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ColorBgra"/> struct.
    /// </summary>
    /// <param name="red">Red component.</param>
    /// <param name="green">Green component.</param>
    /// <param name="blue">Blue component.</param>
    /// <param name="alpha">Alpha component.</param>
    public ColorBgra(float red, float green, float blue, float alpha = 1.0f)
    {
        Vector128<float> vector = Vector128.Create(red, green, blue, alpha);
        Vector128<float> N = Saturate(vector);
        N = Vector128.Multiply(N, UByteMax);
        N = Round(N);

        _packedValue = ((uint)N.GetW() << 24) | ((uint)N.GetX() << 16) | ((uint)N.GetY() << 8) | (uint)N.GetZ();
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ColorBgra"/> struct.
    /// </summary>
    /// <param name="vector">The red, green, and blue components of the color.</param>
    /// <param name="alpha">The alpha component of the color.</param>
    public ColorBgra(Vector3 vector, float alpha = 1.0f)
         : this(vector.X, vector.Y, vector.Z, alpha)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ColorBgra"/> struct.
    /// </summary>
    /// <param name="vector">A four-component color.</param>
    public ColorBgra(Vector4 vector)
        : this(vector.X, vector.Y, vector.Z, vector.W)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ColorBgra"/> struct.
    /// </summary>
    /// <param name="bgra">A packed integer containing all four color components in BGRA order.</param>
    public ColorBgra(uint bgra)
    {
        A = (byte)((bgra >> 24) & 255);
        R = (byte)((bgra >> 16) & 255);
        G = (byte)((bgra >> 8) & 255);
        B = (byte)(bgra & 255);
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ColorBgra"/> struct.
    /// </summary>
    /// <param name="bgra">A packed integer containing all four color components in BGRA.</param>
    public ColorBgra(int bgra)
    {
        A = (byte)((bgra >> 24) & 255);
        R = (byte)((bgra >> 16) & 255);
        G = (byte)((bgra >> 8) & 255);
        B = (byte)(bgra & 255);
    }

    /// <summary>
    /// Converts the color into a packed integer.
    /// </summary>
    /// <returns>A packed integer containing all four color components.</returns>
    public int ToBgra()
    {
        int value = B;
        value |= G << 8;
        value |= R << 16;
        value |= A << 24;

        return value;
    }

    /// <summary>
    /// Converts the color into a packed integer.
    /// </summary>
    /// <returns>A packed integer containing all four color components.</returns>
    public int ToRgba()
    {
        int value = R;
        value |= G << 8;
        value |= B << 16;
        value |= A << 24;

        return value;
    }

    /// <summary>
    /// Converts the color into a three component vector.
    /// </summary>
    /// <returns>A three component vector containing the red, green, and blue components of the color.</returns>
    public Vector3 ToVector3()
    {
        return new Vector3(R / 255.0f, G / 255.0f, B / 255.0f);
    }

    /// <summary>
    /// Converts the color into a three component color.
    /// </summary>
    /// <returns>A three component color containing the red, green, and blue components of the color.</returns>
    public Color3 ToColor3()
    {
        return new Color3(R / 255.0f, G / 255.0f, B / 255.0f);
    }

    /// <summary>
    /// Gets a four-component vector representation for this object.
    /// </summary>
    public Vector4 ToVector4()
    {
        return new Vector4(R / 255.0f, G / 255.0f, B / 255.0f, A / 255.0f);
    }

    /// <summary>
    /// Convert this instance to a <see cref="Color4"/>
    /// </summary>
    public Color4 ToColor4()
    {
        return new Color4(R / 255.0f, G / 255.0f, B / 255.0f, A / 255.0f);
    }

    /// <summary>
    /// Converts the color from a packed BGRA integer.
    /// </summary>
    /// <param name="color">A packed integer containing all four color components in BGRA order</param>
    /// <returns>A color.</returns>
    public static ColorBgra FromBgra(int color) => new(color);

    /// <summary>
    /// Converts the color from a packed BGRA integer.
    /// </summary>
    /// <param name="color">A packed integer containing all four color components in BGRA order</param>
    /// <returns>A color.</returns>
    public static ColorBgra FromBgra(uint color) => new ColorBgra(color);

    /// <summary>
    /// Converts the color from a packed RGBA integer.
    /// </summary>
    /// <param name="color">A packed integer containing all four color components in RGBA order</param>
    /// <returns>A color.</returns>
    public static ColorBgra FromRgba(int color)
    {
        return new((byte)(color & 255), (byte)((color >> 8) & 255), (byte)((color >> 16) & 255), (byte)((color >> 24) & 255));
    }

    /// <summary>
    /// Converts the color from a packed RGBA integer.
    /// </summary>
    /// <param name="color">A packed integer containing all four color components in RGBA order</param>
    /// <returns>A color.</returns>
    public static ColorBgra FromRgba(uint color)
    {
        return FromRgba(unchecked((int)color));
    }

    /// <summary>
    /// Performs an implicit conversion from <see cref="ColorBgra"/> to <see cref="Color4"/>.
    /// </summary>
    /// <param name="value">The value.</param>
    /// <returns>The result of the conversion.</returns>
    public static implicit operator Color4(ColorBgra value) => value.ToColor4();

    /// <summary>
    /// Performs an explicit conversion from <see cref="Vector3"/> to <see cref="ColorBgra"/>.
    /// </summary>
    /// <param name="value">The value.</param>
    /// <returns>The result of the conversion.</returns>
    public static explicit operator ColorBgra(Vector3 value) => new(value.X, value.Y, value.Z, 1.0f);

    /// <summary>
    /// Performs an explicit conversion from <see cref="Vector4"/> to <see cref="ColorBgra"/>.
    /// </summary>
    /// <param name="value">The value.</param>
    /// <returns>The result of the conversion.</returns>
    public static explicit operator ColorBgra(Vector4 value) => new(value.X, value.Y, value.Z, value.W);

    /// <summary>
    /// Performs an explicit conversion from <see cref="Color4"/> to <see cref="ColorBgra"/>.
    /// </summary>
    /// <param name="value">The value.</param>
    /// <returns>The result of the conversion.</returns>
    public static explicit operator ColorBgra(Color4 value) => new(value.R, value.G, value.B, value.A);

    /// <inheritdoc/>
    public override bool Equals(object? obj) => obj is ColorBgra color && Equals(color);

    /// <summary>
    /// Determines whether the specified <see cref="ColorBgra"/> is equal to this instance.
    /// </summary>
    /// <param name="other">The <see cref="ColorBgra"/> to compare with this instance.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool Equals(ColorBgra other)
    {
        return R == other.R
            && G == other.G
            && B == other.B
            && A == other.A;
    }

    /// <summary>
    /// Compares two <see cref="ColorBgra"/> objects for equality.
    /// </summary>
    /// <param name="left">The <see cref="ColorBgra"/> on the left hand of the operand.</param>
    /// <param name="right">The <see cref="ColorBgra"/> on the right hand of the operand.</param>
    /// <returns>
    /// True if the current left is equal to the <paramref name="right"/> parameter; otherwise, false.
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator ==(ColorBgra left, ColorBgra right) => left.Equals(right);

    /// <summary>
    /// Compares two <see cref="ColorBgra"/> objects for inequality.
    /// </summary>
    /// <param name="left">The <see cref="ColorBgra"/> on the left hand of the operand.</param>
    /// <param name="right">The <see cref="ColorBgra"/> on the right hand of the operand.</param>
    /// <returns>
    /// True if the current left is unequal to the <paramref name="right"/> parameter; otherwise, false.
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator !=(ColorBgra left, ColorBgra right) => !left.Equals(right);

    /// <inheritdoc/>
    public override int GetHashCode() => PackedValue.GetHashCode();

    /// <inheritdoc/>
    public override string ToString()
    {
        return $"A={A}, R={R}, G={G}, B={B}";
    }
}
