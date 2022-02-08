// Copyright (c) Amer Koleci and contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Vortice.Mathematics;

/// <summary>
/// Vector type containing four 32 bit unsigned integer components.
/// </summary>
[DebuggerDisplay("X={X}, Y={Y}, Z={Z}, W={W}")]
public struct UInt4 : IEquatable<UInt4>, IFormattable
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

    /// <summary>
    /// The W component of the vector.
    /// </summary>
    public uint W;

    internal const int Count = 4;

    /// <summary>
    /// Initializes a new instance of the <see cref="UInt4"/> struct.
    /// </summary>
    /// <param name="value">The value that will be assigned to all components.</param>
    public UInt4(uint value) : this(value, value, value, value)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="UInt4" /> struct.
    /// </summary>
    /// <param name="x">Initial value for the X component of the vector.</param>
    /// <param name="y">Initial value for the Y component of the vector.</param>
    /// <param name="z">Initial value for the Z component of the vector.</param>
    /// <param name="w">Initial value for the W component of the vector.</param>
    public UInt4(uint x, uint y, uint z, uint w)
    {
        X = x;
        Y = y;
        Z = z;
        W = w; 
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="UInt4" /> struct.
    /// </summary>
    /// <param name="value">A <see cref="UInt2"/> containing the values with which to initialize the X and Y components.</param>
    /// <param name="z">Initial value for the Z component of the vector.</param>
    /// <param name="w">Initial value for the W component of the vector.</param>
    public UInt4(UInt2 value, uint z, uint w)
    {
        X = value.X;
        Y = value.Y;
        Z = z;
        W = w;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="UInt4" /> struct.
    /// </summary>
    /// <param name="value">A <see cref="UInt3"/> containing the values with which to initialize the X, Y, and Z components.</param>
    /// <param name="w">Initial value for the W component of the vector.</param>
    public UInt4(UInt3 value, uint w)
    {
        X = value.X;
        Y = value.Y;
        Z = value.Z;
        W = w;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="UInt4" /> struct.
    /// </summary>
    /// <param name="values">The span of elements to assign to the vector.</param>
    public UInt4(ReadOnlySpan<uint> values)
    {
        if (values.Length < 4)
        {
            throw new ArgumentOutOfRangeException(nameof(values), "There must be 4 uint values.");
        }

        this = Unsafe.ReadUnaligned<UInt4>(ref Unsafe.As<uint, byte>(ref MemoryMarshal.GetReference(values)));
    }

    /// <summary>
    /// A <see cref="UInt4"/> with all of its components set to zero.
    /// </summary>
    public static UInt4 Zero
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => default;
    }

    /// <summary>
    /// The X unit <see cref="UInt4"/> (1, 0, 0, 0).
    /// </summary>
    public static UInt4 UnitX
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => new(1, 0, 0, 0);
    }

    /// <summary>
    /// The Y unit <see cref="UInt4"/> (0, 1, 0, 0).
    /// </summary>
    public static UInt4 UnitY
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => new(0, 1, 0, 0);
    }

    /// <summary>
    /// The Y unit <see cref="UInt4"/> (0, 0, 1, 0).
    /// </summary>
    public static UInt4 UnitZ
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => new(0, 0, 1, 0);
    }

    /// <summary>
    /// The Y unit <see cref="UInt4"/> (0, 0, 0, 1).
    /// </summary>
    public static UInt4 UnitW
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => new(0, 0, 0, 1);
    }

    /// <summary>
    /// A <see cref="UInt4"/> with all of its components set to one.
    /// </summary>
    public static UInt4 One
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => new(1, 1, 1, 1);
    }

    public uint this[int index]
    {
        get => GetElement(this, index);
        set => this = WithElement(this, index, value);
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

        if ((array.Length - index) < 4)
        {
            throw new ArgumentOutOfRangeException(nameof(index));
        }

        array[index] = X;
        array[index + 1] = Y;
        array[index + 2] = Z;
        array[index + 3] = W;
    }

    /// <summary>Copies the vector to the given <see cref="Span{T}" />.The length of the destination span must be at least 2.</summary>
    /// <param name="destination">The destination span which the values are copied into.</param>
    /// <exception cref="ArgumentException">If number of elements in source vector is greater than those available in destination span.</exception>
    public readonly void CopyTo(Span<uint> destination)
    {
        if (destination.Length < 4)
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
        if (destination.Length < 4)
        {
            return false;
        }

        Unsafe.WriteUnaligned(ref Unsafe.As<uint, byte>(ref MemoryMarshal.GetReference(destination)), this);
        return true;
    }

    /// <summary>
    /// Creates a new <see cref="UInt4"/> value with the same value for all its components.
    /// </summary>
    /// <param name="x">The value to use for the components of the new <see cref="UInt4"/> instance.</param>
    public static implicit operator UInt4(uint x) => new(x, x, x, x);

    /// <summary>
    /// Casts a <see cref="UInt4"/> value to a <see cref="Int4"/> one.
    /// </summary>
    /// <param name="xyzw">The input <see cref="UInt4"/> value to cast.</param>
    public static explicit operator Int4(UInt4 xyzw) => new((int)xyzw.X, (int)xyzw.Y, (int)xyzw.Z, (int)xyzw.W);

    /// <summary>
    /// Casts a <see cref="UInt4"/> value to a <see cref="Vector4"/> one.
    /// </summary>
    /// <param name="xyzw">The input <see cref="UInt4"/> value to cast.</param>
    public static implicit operator Vector4(UInt4 xyzw) => new(xyzw.X, xyzw.Y, xyzw.Z, xyzw.W);

    /// <inheritdoc/>
    public override bool Equals(object? obj) => obj is UInt4 value && Equals(value);

    /// <summary>
    /// Determines whether the specified <see cref="UInt4"/> is equal to this instance.
    /// </summary>
    /// <param name="other">The <see cref="UInt4"/> to compare with this instance.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool Equals(UInt4 other)
    {
        return X == other.X
            && Y == other.Y
            && Z == other.Z
            && W == other.W;
    }

    /// <summary>
    /// Compares two <see cref="UInt4"/> objects for equality.
    /// </summary>
    /// <param name="left">The <see cref="UInt4"/> on the left hand of the operand.</param>
    /// <param name="right">The <see cref="UInt4"/> on the right hand of the operand.</param>
    /// <returns>
    /// True if the current left is equal to the <paramref name="right"/> parameter; otherwise, false.
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator ==(UInt4 left, UInt4 right) => left.Equals(right);

    /// <summary>
    /// Compares two <see cref="UInt4"/> objects for inequality.
    /// </summary>
    /// <param name="left">The <see cref="UInt4"/> on the left hand of the operand.</param>
    /// <param name="right">The <see cref="UInt4"/> on the right hand of the operand.</param>
    /// <returns>
    /// True if the current left is unequal to the <paramref name="right"/> parameter; otherwise, false.
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator !=(UInt4 left, UInt4 right) => !left.Equals(right);

    /// <inheritdoc/>
    public override int GetHashCode() => HashCode.Combine(X, Y, Z, W);

    /// <inheritdoc />
    public override string ToString() => ToString(format: null, formatProvider: null);

    /// <inheritdoc />
    public string ToString(string? format, IFormatProvider? formatProvider)
        => $"{nameof(UInt4)} {{ {nameof(X)} = {X.ToString(format, formatProvider)}, {nameof(Y)} = {Y.ToString(format, formatProvider)}, {nameof(Z)} = {Z.ToString(format, formatProvider)}, {nameof(W)} = {W.ToString(format, formatProvider)} }}";

    internal static uint GetElement(UInt4 vector, int index)
    {
        if ((uint)index >= Count)
        {
            throw new ArgumentOutOfRangeException(nameof(index));
        }

        return GetElementUnsafe(ref vector, index);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static uint GetElementUnsafe(ref UInt4 vector, int index)
    {
        Debug.Assert(index is >= 0 and < Count);

        return Unsafe.Add(ref Unsafe.As<UInt4, uint>(ref vector), index);
    }

    /// <summary>Sets the element at the specified index.</summary>
    /// <param name="vector">The vector of the element to get.</param>
    /// <param name="index">The index of the element to set.</param>
    /// <param name="value">The value of the element to set.</param>
    /// <exception cref="ArgumentOutOfRangeException"><paramref name="index" /> was less than zero or greater than the number of elements.</exception>
    internal static UInt4 WithElement(UInt4 vector, int index, uint value)
    {
        if ((uint)index >= Count)
        {
            throw new ArgumentOutOfRangeException(nameof(index));
        }

        UInt4 result = vector;
        SetElementUnsafe(ref result, index, value);
        return result;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static void SetElementUnsafe(ref UInt4 vector, int index, uint value)
    {
        Debug.Assert(index is >= 0 and < Count);

        Unsafe.Add(ref Unsafe.As<UInt4, uint>(ref vector), index) = value;
    }
}
