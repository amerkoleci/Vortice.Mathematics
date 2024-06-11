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
/// Packed vector type containing four 16-bit signed normalized integer components.
/// </summary>
/// <remarks>Equivalent of XMSHORTN4.</remarks>
[StructLayout(LayoutKind.Explicit)]
public readonly struct Short4Normalized : IPackedVector<ulong>, IEquatable<Short4Normalized>
{
    [FieldOffset(0)]
    private readonly ulong _packedValue;

    /// <summary>
    /// The X component of the vector.
    /// </summary>
    [FieldOffset(0)]
    public readonly short X;

    /// <summary>
    /// The Y component of the vector.
    /// </summary>
    [FieldOffset(2)]
    public readonly short Y;

    /// <summary>
    /// The Z component of the vector.
    /// </summary>
    [FieldOffset(4)]
    public readonly short Z;

    /// <summary>
    /// The W component of the vector.
    /// </summary>
    [FieldOffset(6)]
    public readonly short W;

    /// <summary>
    /// Initializes a new instance of the <see cref="Short4Normalized"/> struct.
    /// </summary>
    /// <param name="packedValue">The packed value to assign.</param>
    public Short4Normalized(ulong packedValue)
    {
        _packedValue = packedValue;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Short4Normalized"/> struct.
    /// </summary>
    /// <param name="x">The x value.</param>
    /// <param name="y">The y value.</param>
    /// <param name="z">The z value.</param>
    /// <param name="w">The w value.</param>
    public Short4Normalized(short x, short y, short z, short w)
    {
        X = x;
        Y = y;
        Z = z;
        W = w;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Short4Normalized"/> struct.
    /// </summary>
    /// <param name="x">The x value.</param>
    /// <param name="y">The y value.</param>
    /// <param name="z">The z value.</param>
    /// <param name="w">The w value.</param>
    public Short4Normalized(float x, float y, float z, float w)
    {
        Vector128<float> vector = Clamp(Vector128.Create(x, y, z, w), NegativeOne, One);
        vector = Vector128.Multiply(vector, ShortMax);
        vector = Round(vector);

        X = (short)vector.GetX();
        Y = (short)vector.GetY();
        Z = (short)vector.GetZ();
        W = (short)vector.GetW();
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Short4Normalized"/> struct.
    /// </summary>
    /// <param name="vector">The <see cref="Vector4"/> containing X, Y, Z and W value.</param>
    public Short4Normalized(in Vector4 vector)
        : this(vector.X, vector.Y, vector.Z, vector.W)
    {
    }

    /// <summary>
    /// Constructs a vector from the given <see cref="ReadOnlySpan{Single}" />. The span must contain at least 3 elements.
    /// </summary>
    /// <param name="values">The span of elements to assign to the vector.</param>
    public Short4Normalized(ReadOnlySpan<float> values)
    {
        Vector128<float> vector = Clamp(Vector128.Create(values), NegativeOne, One);
        vector = Vector128.Multiply(vector, ShortMax);
        vector = Round(vector);

        X = (short)vector.GetX();
        Y = (short)vector.GetY();
        Z = (short)vector.GetZ();
        W = (short)vector.GetW();
    }

    /// <summary>
    /// Gets the packed value.
    /// </summary>
    public ulong PackedValue => _packedValue;

    /// <summary>
    /// Expands the packed representation to a <see cref="Vector4"/>.
    /// </summary>
    public Vector4 ToVector4() => new(
        (X == -32768) ? -1.0f : X * (1.0f / 32767.0f),
        (Y == -32768) ? -1.0f : Y * (1.0f / 32767.0f),
        (Z == -32768) ? -1.0f : Z * (1.0f / 32767.0f),
        (W == -32768) ? -1.0f : W * (1.0f / 32767.0f)
        );

    /// <inheritdoc/>
    public override bool Equals([NotNullWhen(true)] object? obj) => obj is Short4Normalized other && Equals(other);

    /// <inheritdoc/>
    public bool Equals(Short4Normalized other) => PackedValue.Equals(other.PackedValue);

    /// <summary>
    /// Compares two <see cref="Short4Normalized"/> objects for equality.
    /// </summary>
    /// <param name="left">The <see cref="Short4Normalized"/> on the left hand of the operand.</param>
    /// <param name="right">The <see cref="Short4Normalized"/> on the right hand of the operand.</param>
    /// <returns>
    /// True if the current left is equal to the <paramref name="right"/> parameter; otherwise, false.
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator ==(Short4Normalized left, Short4Normalized right) => left.Equals(right);

    /// <summary>
    /// Compares two <see cref="Short4Normalized"/> objects for inequality.
    /// </summary>
    /// <param name="left">The <see cref="Short4Normalized"/> on the left hand of the operand.</param>
    /// <param name="right">The <see cref="Short4Normalized"/> on the right hand of the operand.</param>
    /// <returns>
    /// True if the current left is unequal to the <paramref name="right"/> parameter; otherwise, false.
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator !=(Short4Normalized left, Short4Normalized right) => !left.Equals(right);

    /// <inheritdoc/>
    public override int GetHashCode() => PackedValue.GetHashCode();

    /// <inheritdoc/>
    public override string ToString() => PackedValue.ToString("X16", CultureInfo.InvariantCulture);
}
