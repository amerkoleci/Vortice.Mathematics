// Copyright (c) Amer Koleci and contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

using System.Globalization;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Intrinsics;
using static Vortice.Mathematics.VectorUtilities;

namespace Vortice.Mathematics.PackedVector;

/// <summary>
/// Packed vector type containing four 8 bit unsigned normalized integer components.
/// </summary>
/// <remarks>Equivalent of XMUBYTEN4.</remarks>
[StructLayout(LayoutKind.Explicit)]
public readonly struct UByte4Normalized : IPackedVector<uint>, IEquatable<UByte4Normalized>
{
    [FieldOffset(0)]
    private readonly uint _packedValue;

    /// <summary>
    /// The X component of the vector.
    /// </summary>
    [FieldOffset(0)]
    public readonly byte X;

    /// <summary>
    /// The Y component of the vector.
    /// </summary>
    [FieldOffset(1)]
    public readonly byte Y;

    /// <summary>
    /// The Z component of the vector.
    /// </summary>
    [FieldOffset(2)]
    public readonly byte Z;

    /// <summary>
    /// The W component of the vector.
    /// </summary>
    [FieldOffset(3)]
    public readonly byte W;

    /// <summary>
    /// Initializes a new instance of the <see cref="UByte4Normalized"/> struct.
    /// </summary>
    /// <param name="packedValue">The packed value to assign.</param>
    public UByte4Normalized(uint packedValue)
    {
        _packedValue = packedValue;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="UByte4Normalized"/> struct.
    /// </summary>
    /// <param name="x">The x value.</param>
    /// <param name="y">The y value.</param>
    /// <param name="z">The z value.</param>
    /// <param name="w">The w value.</param>
    public UByte4Normalized(byte x, byte y, byte z, byte w)
    {
        X = x;
        Y = y;
        Z = z;
        W = w;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="UByte4Normalized"/> struct.
    /// </summary>
    /// <param name="x">The x value.</param>
    /// <param name="y">The y value.</param>
    /// <param name="z">The z value.</param>
    /// <param name="w">The w value.</param>
    public UByte4Normalized(float x, float y, float z, float w)
    {
        Vector128<float> result = Saturate(Vector128.Create(x, y, z, w));
        result = Vector128.Multiply(result, UByteMax);
        result = Truncate(result);

        X = (byte)result.GetX();
        Y = (byte)result.GetY();
        Z = (byte)result.GetZ();
        W = (byte)result.GetW();
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="UByte4Normalized"/> struct.
    /// </summary>
    /// <param name="vector">The <see cref="Vector4"/> containing X and Y value.</param>
    public UByte4Normalized(in Vector4 vector)
        : this(vector.X, vector.Y, vector.Z, vector.W)
    {
        
    }

    /// <summary>Constructs a vector from the given <see cref="ReadOnlySpan{Single}" />. The span must contain at least 3 elements.</summary>
    /// <param name="values">The span of elements to assign to the vector.</param>
    public UByte4Normalized(ReadOnlySpan<float> values)
    {
        if (values.Length < 4)
        {
            throw new ArgumentOutOfRangeException(nameof(values));
        }


        Vector128<float> result = Saturate(Vector128.Create(values));
        result = Vector128.Multiply(result, UByteMax);
        result = Truncate(result);

        X = (byte)result.GetX();
        Y = (byte)result.GetY();
        Z = (byte)result.GetZ();
        W = (byte)result.GetW();
    }

    /// <summary>
    /// Gets the packed value.
    /// </summary>
    public uint PackedValue => _packedValue;

    /// <summary>
    /// Expands the packed representation to a <see cref="Vector4"/>.
    /// </summary>
    public Vector4 ToVector4()
    {
        return new(X / 255.0f, Y / 255.0f, Z / 255.0f, W / 255.0f);
    }

    /// <inheritdoc/>
    public override bool Equals([NotNullWhen(true)] object? obj) => obj is UByte4Normalized other && Equals(other);

    /// <inheritdoc/>
    public bool Equals(UByte4Normalized other) => PackedValue.Equals(other.PackedValue);

    /// <summary>
    /// Compares two <see cref="UByte4Normalized"/> objects for equality.
    /// </summary>
    /// <param name="left">The <see cref="UByte4Normalized"/> on the left hand of the operand.</param>
    /// <param name="right">The <see cref="UByte4Normalized"/> on the right hand of the operand.</param>
    /// <returns>
    /// True if the current left is equal to the <paramref name="right"/> parameter; otherwise, false.
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator ==(UByte4Normalized left, UByte4Normalized right) => left.Equals(right);

    /// <summary>
    /// Compares two <see cref="UByte4Normalized"/> objects for inequality.
    /// </summary>
    /// <param name="left">The <see cref="UByte4Normalized"/> on the left hand of the operand.</param>
    /// <param name="right">The <see cref="UByte4Normalized"/> on the right hand of the operand.</param>
    /// <returns>
    /// True if the current left is unequal to the <paramref name="right"/> parameter; otherwise, false.
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator !=(UByte4Normalized left, UByte4Normalized right) => !left.Equals(right);

    /// <inheritdoc/>
    public override int GetHashCode() => PackedValue.GetHashCode();

    /// <inheritdoc/>
    public override string ToString() => PackedValue.ToString("X8", CultureInfo.InvariantCulture);
}
