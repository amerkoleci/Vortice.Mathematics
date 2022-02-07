// Copyright (c) Amer Koleci and contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using static Vortice.Mathematics.Vector2Utilities;

namespace Vortice.Mathematics.PackedVector;

/// <summary>
/// Packed vector type containing two 16-bit unsigned normalized integer components.
/// </summary>
/// <remarks>Equivalent of XMUSHORTN2.</remarks>
[StructLayout(LayoutKind.Explicit)]
public readonly struct UShortN2 : IPackedVector<uint>, IEquatable<UShortN2>
{
    [FieldOffset(0)]
    private readonly uint _packedValue;

    /// <summary>
    /// The X component of the vector.
    /// </summary>
    [FieldOffset(0)]
    public readonly ushort X;

    /// <summary>
    /// The Y component of the vector.
    /// </summary>
    [FieldOffset(2)]
    public readonly ushort Y;

    /// <summary>
    /// Initializes a new instance of the <see cref="UShortN2"/> struct.
    /// </summary>
    /// <param name="packedValue">The packed value to assign.</param>
    public UShortN2(uint packedValue)
    {
        Unsafe.SkipInit(out this);

        _packedValue = packedValue;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="UShortN2"/> struct.
    /// </summary>
    /// <param name="x">The x value.</param>
    /// <param name="y">The y value.</param>
    public UShortN2(ushort x, ushort y)
    {
        Unsafe.SkipInit(out this);

        X = x;
        Y = y;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="UShortN2"/> struct.
    /// </summary>
    /// <param name="x">The x value.</param>
    /// <param name="y">The y value.</param>
    public UShortN2(float x, float y)
    {
        Unsafe.SkipInit(out this);

        Vector2 vector = Saturate(new Vector2(x, y));
        vector = MultiplyAdd(vector, UShortMax, OneHalf);
        vector = Truncate(vector);

        X = (ushort)vector.X;
        Y = (ushort)vector.Y;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="UShortN2"/> struct.
    /// </summary>
    /// <param name="vector">The <see cref="Vector2"/> containing X and Y value.</param>
    public UShortN2(Vector2 vector)
        : this(vector.X, vector.Y)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="UShortN2"/> struct.
    /// </summary>
    /// <param name="vector">The <see cref="Vector4"/> containing X and Y value.</param>
    public UShortN2(Vector4 vector)
        : this(vector.X, vector.Y)
    {
    }

    /// <summary>
    /// Gets the packed value.
    /// </summary>
    public uint PackedValue => _packedValue;

    /// <summary>
    /// Expands the packed representation to a <see cref="Vector2"/>.
    /// </summary>
    public Vector2 ToVector2() => new((float)X / 65535.0f, (float)Y / 65535.0f);

    Vector4 IPackedVector.ToVector4()
    {
        Vector2 vector = ToVector2();
        return new Vector4(vector.X, vector.Y, 0.0f, 1.0f);
    }

    /// <inheritdoc/>
    public override bool Equals(object? obj) => obj is UShortN2 other && Equals(other);

    /// <inheritdoc/>
    public bool Equals(UShortN2 other) => PackedValue.Equals(other.PackedValue);

    /// <summary>
    /// Compares two <see cref="UShortN2"/> objects for equality.
    /// </summary>
    /// <param name="left">The <see cref="UShortN2"/> on the left hand of the operand.</param>
    /// <param name="right">The <see cref="UShortN2"/> on the right hand of the operand.</param>
    /// <returns>
    /// True if the current left is equal to the <paramref name="right"/> parameter; otherwise, false.
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator ==(UShortN2 left, UShortN2 right) => left.Equals(right);

    /// <summary>
    /// Compares two <see cref="UShortN2"/> objects for inequality.
    /// </summary>
    /// <param name="left">The <see cref="UShortN2"/> on the left hand of the operand.</param>
    /// <param name="right">The <see cref="UShortN2"/> on the right hand of the operand.</param>
    /// <returns>
    /// True if the current left is unequal to the <paramref name="right"/> parameter; otherwise, false.
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator !=(UShortN2 left, UShortN2 right) => !left.Equals(right);

    /// <inheritdoc/>
    public override int GetHashCode() => PackedValue.GetHashCode();

    /// <inheritdoc/>
    public override string ToString() => PackedValue.ToString("X8", CultureInfo.InvariantCulture);
}
