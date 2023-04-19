// Copyright (c) Amer Koleci and contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

using System.Diagnostics;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Vortice.Mathematics;

/// <summary>
/// Vector type containing three 32 bit unsigned integer components.
/// </summary>
[DebuggerDisplay("X={X}, Y={Y}, Z={Z}")]
public struct UInt3 : IEquatable<UInt3>, IFormattable
{
    /// <summary>
    /// The X component of the vector.
    /// </summary>
    public uint X;

    /// <summary>
    /// The Y component of the vector.
    /// </summary>
    public uint Y;

    /// <summary>
    /// The Z component of the vector.
    /// </summary>
    public uint Z;

    internal const int Count = 3;

    /// <summary>
    /// Initializes a new instance of the <see cref="UInt3"/> struct.
    /// </summary>
    /// <param name="value">The value that will be assigned to all components.</param>
    public UInt3(uint value)
        : this(value, value, value)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="UInt3" /> struct.
    /// </summary>
    /// <param name="x">Initial value for the X component of the vector.</param>
    /// <param name="y">Initial value for the Y component of the vector.</param>
    /// <param name="z">Initial value for the Z component of the vector.</param>
    public UInt3(uint x, uint y, uint z)
    {
        X = x;
        Y = y;
        Z = z;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="UInt3" /> struct.
    /// </summary>
    /// <param name="xy">Initial value for the X and Y component of the vector.</param>
    /// <param name="z">Initial value for the Z component of the vector.</param>
    public UInt3(in UInt2 xy, uint z)
    {
        X = xy.X;
        Y = xy.Y;
        Z = z;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="UInt3" /> struct.
    /// </summary>
    /// <param name="values">The span of elements to assign to the vector.</param>
    public UInt3(ReadOnlySpan<uint> values)
    {
        if (values.Length < 3)
        {
            throw new ArgumentOutOfRangeException(nameof(values), "There must be 3 uint values.");
        }

        this = Unsafe.ReadUnaligned<UInt3>(ref Unsafe.As<uint, byte>(ref MemoryMarshal.GetReference(values)));
    }

    /// <summary>
    /// A <see cref="UInt3"/> with all of its components set to zero.
    /// </summary>
    public static UInt3 Zero
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => default;
    }

    /// <summary>
    /// The X unit <see cref="UInt3"/> (1, 0, 0).
    /// </summary>
    public static UInt3 UnitX
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => new(1, 0, 0);
    }

    /// <summary>
    /// The Y unit <see cref="UInt3"/> (0, 1, 0).
    /// </summary>
    public static UInt3 UnitY
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => new(0, 1, 0);
    }

    /// <summary>
    /// The Y unit <see cref="UInt3"/> (0, 0, 1).
    /// </summary>
    public static UInt3 UnitZ
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => new(0, 0, 1);
    }

    /// <summary>
    /// A <see cref="UInt3"/> with all of its components set to one.
    /// </summary>
    public static UInt3 One
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => new(1, 1, 1);
    }

    /// <summary>Gets or sets the element at the specified index.</summary>
    /// <param name="index">The index of the element to get or set.</param>
    /// <returns>The the element at <paramref name="index" />.</returns>
    /// <exception cref="ArgumentOutOfRangeException"><paramref name="index" /> was less than zero or greater than the number of elements.</exception>
    public uint this[int index]
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        readonly get => this.GetElement(index);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        set => this = this.WithElement(index, value);
    }

    public void Deconstruct(out uint x, out uint y, out uint z)
    {
        x = X;
        y = Y;
        z = Z;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly void CopyTo(uint[] array)
    {
        CopyTo(array, 0);
    }

    public readonly void CopyTo(uint[] array, int index)
    {
        if (array is null)
        {
            throw new NullReferenceException(nameof(array));
        }

        if ((index < 0) || (index >= array.Length))
        {
            throw new ArgumentOutOfRangeException(nameof(index));
        }

        if ((array.Length - index) < 3)
        {
            throw new ArgumentOutOfRangeException(nameof(index));
        }

        array[index] = X;
        array[index + 1] = Y;
        array[index + 2] = Z;
    }

    /// <summary>Copies the vector to the given <see cref="Span{T}" />.The length of the destination span must be at least 2.</summary>
    /// <param name="destination">The destination span which the values are copied into.</param>
    /// <exception cref="ArgumentException">If number of elements in source vector is greater than those available in destination span.</exception>
    public readonly void CopyTo(Span<uint> destination)
    {
        if (destination.Length < 3)
        {
            throw new ArgumentOutOfRangeException(nameof(destination));
        }

        Unsafe.WriteUnaligned(ref Unsafe.As<uint, byte>(ref MemoryMarshal.GetReference(destination)), this);
    }

    /// <summary>Attempts to copy the vector to the given <see cref="Span{Int32}" />. The length of the destination span must be at least 2.</summary>
    /// <param name="destination">The destination span which the values are copied into.</param>
    /// <returns><see langword="true" /> if the source vector was successfully copied to <paramref name="destination" />. <see langword="false" /> if <paramref name="destination" /> is not large enough to hold the source vector.</returns>
    public readonly bool TryCopyTo(Span<uint> destination)
    {
        if (destination.Length < 3)
        {
            return false;
        }

        Unsafe.WriteUnaligned(ref Unsafe.As<uint, byte>(ref MemoryMarshal.GetReference(destination)), this);
        return true;
    }

    /// <summary>
    /// Creates a new <see cref="UInt3"/> value with the same value for all its components.
    /// </summary>
    /// <param name="x">The value to use for the components of the new <see cref="UInt3"/> instance.</param>
    public static implicit operator UInt3(uint x) => new(x, x, x);

    /// <summary>
    /// Casts a <see cref="UInt3"/> value to a <see cref="Int3"/> one.
    /// </summary>
    /// <param name="xyz">The input <see cref="UInt3"/> value to cast.</param>
    public static explicit operator Int3(UInt3 xyz) => new Int3((int)xyz.X, (int)xyz.Y, (int)xyz.Z);

    /// <summary>
    /// Casts a <see cref="UInt3"/> value to a <see cref="Vector3"/> one.
    /// </summary>
    /// <param name="xyz">The input <see cref="UInt3"/> value to cast.</param>
    public static implicit operator Vector3(UInt3 xyz) => new(xyz.X, xyz.Y, xyz.Z);

    /// <inheritdoc/>
    public override bool Equals(object? obj) => obj is UInt3 value && Equals(value);

    /// <summary>
    /// Determines whether the specified <see cref="UInt3"/> is equal to this instance.
    /// </summary>
    /// <param name="other">The <see cref="UInt3"/> to compare with this instance.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool Equals(UInt3 other)
    {
        return X == other.X
            && Y == other.Y
            && Z == other.Z;
    }

    /// <summary>
    /// Compares two <see cref="UInt3"/> objects for equality.
    /// </summary>
    /// <param name="left">The <see cref="UInt3"/> on the left hand of the operand.</param>
    /// <param name="right">The <see cref="UInt3"/> on the right hand of the operand.</param>
    /// <returns>
    /// True if the current left is equal to the <paramref name="right"/> parameter; otherwise, false.
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator ==(UInt3 left, UInt3 right) => left.Equals(right);

    /// <summary>
    /// Compares two <see cref="UInt3"/> objects for inequality.
    /// </summary>
    /// <param name="left">The <see cref="UInt3"/> on the left hand of the operand.</param>
    /// <param name="right">The <see cref="UInt3"/> on the right hand of the operand.</param>
    /// <returns>
    /// True if the current left is unequal to the <paramref name="right"/> parameter; otherwise, false.
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator !=(UInt3 left, UInt3 right) => !left.Equals(right);

    /// <inheritdoc/>
    public override int GetHashCode() => HashCode.Combine(X, Y, Z);

    /// <inheritdoc />
    public override string ToString() => ToString(format: null, formatProvider: null);

    /// <inheritdoc />
    public string ToString(string? format, IFormatProvider? formatProvider)
        => $"{nameof(UInt3)} {{ {nameof(X)} = {X.ToString(format, formatProvider)}, {nameof(Y)} = {Y.ToString(format, formatProvider)}, {nameof(Z)} = {Z.ToString(format, formatProvider)} }}";
}
