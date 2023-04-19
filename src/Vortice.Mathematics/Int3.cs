// Copyright (c) Amer Koleci and contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

using System.Diagnostics;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Vortice.Mathematics;

/// <summary>
/// Vector type containing three 32 bit signed integer components.
/// </summary>
[DebuggerDisplay("X={X}, Y={Y}, Z={Z}")]
public struct Int3 : IEquatable<Int3>, IFormattable
{
    /// <summary>
    /// The X component of the vector.
    /// </summary>
    public int X;

    /// <summary>
    /// The Y component of the vector.
    /// </summary>
    public int Y;

    /// <summary>
    /// The Z component of the vector.
    /// </summary>
    public int Z;

    internal const int Count = 3;

    /// <summary>
    /// Initializes a new instance of the <see cref="Int3"/> struct.
    /// </summary>
    /// <param name="value">The value that will be assigned to all components.</param>
    public Int3(int value) : this(value, value, value)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Int3" /> struct.
    /// </summary>
    /// <param name="x">Initial value for the X component of the vector.</param>
    /// <param name="y">Initial value for the Y component of the vector.</param>
    /// <param name="z">Initial value for the Z component of the vector.</param>
    public Int3(int x, int y, int z)
    {
        X = x;
        Y = y;
        Z = z;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Int3" /> struct.
    /// </summary>
    /// <param name="xy">Initial value for the X and Y component of the vector.</param>
    /// <param name="z">Initial value for the Z component of the vector.</param>
    public Int3(in Int2 xy, int z)
    {
        X = xy.X;
        Y = xy.Y;
        Z = z;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Int3" /> struct.
    /// </summary>
    /// <param name="values">The span of elements to assign to the vector.</param>
    public Int3(ReadOnlySpan<int> values)
    {
        if (values.Length < 3)
        {
            throw new ArgumentOutOfRangeException(nameof(values), "There must be 3 uint values.");
        }

        this = Unsafe.ReadUnaligned<Int3>(ref Unsafe.As<int, byte>(ref MemoryMarshal.GetReference(values)));
    }

    /// <summary>
    /// A <see cref="Int3"/> with all of its components set to zero.
    /// </summary>
    public static Int3 Zero
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => default;
    }

    /// <summary>
    /// The X unit <see cref="Int3"/> (1, 0, 0).
    /// </summary>
    public static Int3 UnitX
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => new(1, 0, 0);
    }

    /// <summary>
    /// The Y unit <see cref="Int3"/> (0, 1, 0).
    /// </summary>
    public static Int3 UnitY
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => new(0, 1, 0);
    }

    /// <summary>
    /// The Y unit <see cref="Int3"/> (0, 0, 1).
    /// </summary>
    public static Int3 UnitZ
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => new(0, 0, 1);
    }

    /// <summary>
    /// A <see cref="Int3"/> with all of its components set to one.
    /// </summary>
    public static Int3 One
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => new(1, 1, 1);
    }

    /// <summary>Gets or sets the element at the specified index.</summary>
    /// <param name="index">The index of the element to get or set.</param>
    /// <returns>The the element at <paramref name="index" />.</returns>
    /// <exception cref="ArgumentOutOfRangeException"><paramref name="index" /> was less than zero or greater than the number of elements.</exception>
    public int this[int index]
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        readonly get => this.GetElement(index);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        set => this = this.WithElement(index, value);
    }

    public void Deconstruct(out int x, out int y, out int z)
    {
        x = X;
        y = Y;
        z = Z;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly void CopyTo(int[] array)
    {
        CopyTo(array, 0);
    }

    public readonly void CopyTo(int[] array, int index)
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
    public readonly void CopyTo(Span<int> destination)
    {
        if (destination.Length < 3)
        {
            throw new ArgumentOutOfRangeException(nameof(destination));
        }

        Unsafe.WriteUnaligned(ref Unsafe.As<int, byte>(ref MemoryMarshal.GetReference(destination)), this);
    }

    /// <summary>Attempts to copy the vector to the given <see cref="Span{Int32}" />. The length of the destination span must be at least 2.</summary>
    /// <param name="destination">The destination span which the values are copied into.</param>
    /// <returns><see langword="true" /> if the source vector was successfully copied to <paramref name="destination" />. <see langword="false" /> if <paramref name="destination" /> is not large enough to hold the source vector.</returns>
    public readonly bool TryCopyTo(Span<int> destination)
    {
        if (destination.Length < 3)
        {
            return false;
        }

        Unsafe.WriteUnaligned(ref Unsafe.As<int, byte>(ref MemoryMarshal.GetReference(destination)), this);
        return true;
    }

    /// <summary>
    /// Creates a new <see cref="Int3"/> value with the same value for all its components.
    /// </summary>
    /// <param name="x">The value to use for the components of the new <see cref="Int3"/> instance.</param>
    public static implicit operator Int3(int x) => new(x, x, x);

    /// <summary>
    /// Casts a <see cref="Int3"/> value to a <see cref="UInt3"/> one.
    /// </summary>
    /// <param name="xyz">The input <see cref="Int3"/> value to cast.</param>
    public static explicit operator UInt3(Int3 xyz) => new((uint)xyz.X, (uint)xyz.Y, (uint)xyz.Z);

    /// <summary>
    /// Casts a <see cref="Int3"/> value to a <see cref="Vector3"/> one.
    /// </summary>
    /// <param name="xyz">The input <see cref="Int3"/> value to cast.</param>
    public static implicit operator Vector3(Int3 xyz) => new(xyz.X, xyz.Y, xyz.Z);

    /// <inheritdoc/>
    public override bool Equals(object? obj) => obj is Int3 value && Equals(value);

    /// <summary>
    /// Determines whether the specified <see cref="Int3"/> is equal to this instance.
    /// </summary>
    /// <param name="other">The <see cref="Int3"/> to compare with this instance.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool Equals(Int3 other)
    {
        return X == other.X
            && Y == other.Y
            && Z == other.Z;
    }

    /// <summary>
    /// Compares two <see cref="Int3"/> objects for equality.
    /// </summary>
    /// <param name="left">The <see cref="Int3"/> on the left hand of the operand.</param>
    /// <param name="right">The <see cref="Int3"/> on the right hand of the operand.</param>
    /// <returns>
    /// True if the current left is equal to the <paramref name="right"/> parameter; otherwise, false.
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator ==(Int3 left, Int3 right) => left.Equals(right);

    /// <summary>
    /// Compares two <see cref="Int3"/> objects for inequality.
    /// </summary>
    /// <param name="left">The <see cref="Int3"/> on the left hand of the operand.</param>
    /// <param name="right">The <see cref="Int3"/> on the right hand of the operand.</param>
    /// <returns>
    /// True if the current left is unequal to the <paramref name="right"/> parameter; otherwise, false.
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator !=(Int3 left, Int3 right) => !left.Equals(right);

    /// <inheritdoc/>
    public override int GetHashCode() => HashCode.Combine(X, Y, Z);

    /// <inheritdoc />
    public override string ToString() => ToString(format: null, formatProvider: null);

    /// <inheritdoc />
    public string ToString(string? format, IFormatProvider? formatProvider)
        => $"{nameof(Int3)} {{ {nameof(X)} = {X.ToString(format, formatProvider)}, {nameof(Y)} = {Y.ToString(format, formatProvider)}, {nameof(Z)} = {Z.ToString(format, formatProvider)} }}";
}
