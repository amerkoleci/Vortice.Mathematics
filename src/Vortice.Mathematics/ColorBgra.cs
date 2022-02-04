// Copyright (c) Amer Koleci and contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Vortice.Mathematics.PackedVector;

#if NET6_0_OR_GREATER
using System.Runtime.Intrinsics;
using System.Runtime.Intrinsics.Arm;
using System.Runtime.Intrinsics.X86;
using static Vortice.Mathematics.VectorUtilities;
#endif

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
    /// <param name="packedValue">The packed value to assign.</param>
    public ColorBgra(uint packedValue) 
    {
        Unsafe.SkipInit(out this);

        _packedValue = packedValue;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ColorBgra"/> struct.
    /// </summary>
    /// <param name="value">The value that will be assigned to all components.</param>
    public ColorBgra(byte value)
        : this(value, value, value, value)
    {
    }

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
    /// <param name="r">The red component of the color.</param>
    /// <param name="g">The green component of the color.</param>
    /// <param name="b">The blue component of the color.</param>
    /// <param name="a">The alpha component of the color.</param>
    public ColorBgra(byte r, byte g, byte b, byte a = 255) 
    {
        Unsafe.SkipInit(out this);

        R = r;
        G = g;
        B = b;
        A = a;
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
        Unsafe.SkipInit(out this);

#if NET6_0_OR_GREATER && TODO
        unsafe
        {
            if (Sse2.IsSupported)
            {
                // Set <0 to 0
                Vector128<float> result = Sse.Max(Vector128.Create(red, green, blue, alpha), Vector128<float>.Zero);
                // Set>1 to 1
                result = Sse.Min(result, One);
                // Convert to 0-255
                result = Sse.Multiply(result, UByteMax);
                // Shuffle RGBA to ARGB
                result = Sse.Shuffle(result, result, Shuffle(3, 0, 1, 2));

                // Convert to int
                Vector128<int> vInt = Sse2.ConvertToVector128Int32(result);
                // Mash to shorts
                Vector128<short> vShort = Sse2.PackSignedSaturate(vInt, vInt);
                // Mash to bytes
                Vector128<byte> vByte = Sse2.PackUnsignedSaturate(vShort, vShort);

                vInt = Vector128.Create(vByte.ToScalar(), vByte.GetElement(1), vByte.GetElement(2), vByte.GetElement(3));

                uint packedValue = default;
                Sse.StoreScalar((float*)&packedValue, Sse2.ConvertToVector128Single(vInt));
                _packedValue = packedValue;
            }
        }
#endif

        Vector4 N = Vector4Utilities.Saturate(new Vector4(red, green, blue, alpha));
        N = Vector4.Multiply(N, Vector4Utilities.UByteMax);
        N = Vector4Utilities.Round(N);
        _packedValue = ((uint)N.W << 24) | ((uint)N.X << 16) | ((uint)N.Y << 8) | (uint)N.Z;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ColorBgra"/> struct.
    /// </summary>
    /// <param name="vector">The red, green, and blue components of the color.</param>
    /// <param name="alpha">The alpha component of the color.</param>
    public ColorBgra(Vector3 vector, float alpha) : this()
    {
        R = PackHelpers.ToByte(vector.X);
        G = PackHelpers.ToByte(vector.Y);
        B = PackHelpers.ToByte(vector.Z);
        A = PackHelpers.ToByte(alpha);
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ColorBgra"/> struct.
    /// </summary>
    /// <param name="vector">A three-component color.</param>
    public ColorBgra(Vector3 vector) : this()
    {
        R = PackHelpers.ToByte(vector.X);
        G = PackHelpers.ToByte(vector.Y);
        B = PackHelpers.ToByte(vector.Z);
        A = 255;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ColorBgra"/> struct.
    /// </summary>
    /// <param name="vector">A four-component color.</param>
    public ColorBgra(Vector4 vector) : this()
    {
        R = PackHelpers.ToByte(vector.X);
        G = PackHelpers.ToByte(vector.Y);
        B = PackHelpers.ToByte(vector.Z);
        A = PackHelpers.ToByte(vector.W);
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
