// Copyright (c) Amer Koleci and contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Vortice.Mathematics;

/// <summary>
/// Vector type containing four 32 bit signed integer components.
/// </summary>
[DebuggerDisplay("X={X}, Y={Y}, Z={Z}, W={W}")]
public readonly struct Int4 : IEquatable<Int4>, IFormattable
{
    /// <summary>
    /// The X component of the vector.
    /// </summary>
    public readonly int X;

    /// <summary>
    /// The Y component of the vector.
    /// </summary>
    public readonly int Y;

    /// <summary>
    /// The Z component of the vector.
    /// </summary>
    public readonly int Z;

    /// <summary>
    /// The W component of the vector.
    /// </summary>
    public readonly int W;

    internal const int Count = 4;

    /// <summary>
    /// Initializes a new instance of the <see cref="Int4"/> struct.
    /// </summary>
    /// <param name="value">The value that will be assigned to all components.</param>
    public Int4(int value) : this(value, value, value, value)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Int4" /> struct.
    /// </summary>
    /// <param name="value">A <see cref="Int2"/> containing the values with which to initialize the X and Y components.</param>
    /// <param name="z">Initial value for the Z component of the vector.</param>
    /// <param name="w">Initial value for the W component of the vector.</param>
    public Int4(Int2 value, int z, int w)
    {
        X = value.X;
        Y = value.Y;
        Z = z;
        W = w;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Int4" /> struct.
    /// </summary>
    /// <param name="value">A <see cref="Int3"/> containing the values with which to initialize the X, Y, and Z components.</param>
    /// <param name="w">Initial value for the W component of the vector.</param>
    public Int4(Int3 value, int w)
    {
        X = value.X;
        Y = value.Y;
        Z = value.Z;
        W = w;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Int4" /> struct.
    /// </summary>
    /// <param name="x">Initial value for the X component of the vector.</param>
    /// <param name="y">Initial value for the Y component of the vector.</param>
    /// <param name="z">Initial value for the Z component of the vector.</param>
    /// <param name="w">Initial value for the W component of the vector.</param>
    public Int4(int x, int y, int z, int w)
    {
        X = x;
        Y = y;
        Z = z;
        W = w;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Int4" /> struct.
    /// </summary>
    /// <param name="values">The span of elements to assign to the vector.</param>
    public Int4(ReadOnlySpan<int> values)
    {
        if (values.Length < 4)
        {
            throw new ArgumentOutOfRangeException(nameof(values), "There must be 4 uint values.");
        }

        this = Unsafe.ReadUnaligned<Int4>(ref Unsafe.As<int, byte>(ref MemoryMarshal.GetReference(values)));
    }

    /// <summary>
    /// A <see cref="Int4"/> with all of its components set to zero.
    /// </summary>
    public static Int4 Zero
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => default;
    }

    /// <summary>
    /// The X unit <see cref="Int4"/> (1, 0, 0, 0).
    /// </summary>
    public static Int4 UnitX
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => new(1, 0, 0, 0);
    }

    /// <summary>
    /// The Y unit <see cref="Int4"/> (0, 1, 0, 0).
    /// </summary>
    public static Int4 UnitY
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => new(0, 1, 0, 0);
    }

    /// <summary>
    /// The Y unit <see cref="Int4"/> (0, 0, 1, 0).
    /// </summary>
    public static Int4 UnitZ
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => new(0, 0, 1, 0);
    }

    /// <summary>
    /// The Y unit <see cref="Int4"/> (0, 0, 0, 1).
    /// </summary>
    public static Int4 UnitW
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => new(0, 0, 0, 1);
    }

    /// <summary>
    /// A <see cref="Int4"/> with all of its components set to one.
    /// </summary>
    public static Int4 One
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => new(1, 1, 1, 1);
    }

    public readonly int this[int index] => GetElement(this, index);

    public void Deconstruct(out int x, out int y, out int z, out int w)
    {
        x = X;
        y = Y;
        z = Z;
        w = W;
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
    public readonly void CopyTo(Span<int> destination)
    {
        if (destination.Length < 4)
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
        if (destination.Length < 4)
        {
            return false;
        }

        Unsafe.WriteUnaligned(ref Unsafe.As<int, byte>(ref MemoryMarshal.GetReference(destination)), this);
        return true;
    }

    /// <summary>
    /// Creates a new <see cref="Int4"/> value with the same value for all its components.
    /// </summary>
    /// <param name="x">The value to use for the components of the new <see cref="Int4"/> instance.</param>
    public static implicit operator Int4(int x) => new(x, x, x, x);

    /// <summary>
    /// Casts a <see cref="Int4"/> value to a <see cref="UInt4"/> one.
    /// </summary>
    /// <param name="xyzw">The input <see cref="Int4"/> value to cast.</param>
    public static explicit operator UInt4(Int4 xyzw) => new((uint)xyzw.X, (uint)xyzw.Y, (uint)xyzw.Z, (uint)xyzw.W);  

    /// <summary>
    /// Performs an explicit conversion from <see cref="Int4" /> to <see cref="Vector4"/>.
    /// </summary>
    /// <param name = "value">The value to convert.</param>
    /// <returns>The result of the conversion.</returns>
    public static implicit operator Vector4(Int4 value) => new(value.X, value.Y, value.Z, value.W);

    /// <summary>
    /// Performs an explicit conversion from <see cref="Int4" /> to <see cref="Int3" />.
    /// </summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>The result of the conversion.</returns>
    public static explicit operator Int3(Int4 value) => new(value.X, value.Y, value.Z);

    /// <summary>
    /// Performs an explicit conversion from <see cref="Int4" /> to <see cref="Vector2"/>.
    /// </summary>
    /// <param name = "value">The value to convert.</param>
    /// <returns>The result of the conversion.</returns>
    public static explicit operator Vector2(Int4 value) => new(value.X, value.Y);

    /// <summary>
    /// Performs an explicit conversion from <see cref="Int4" /> to <see cref="Vector3"/>.
    /// </summary>
    /// <param name = "value">The value to convert.</param>
    /// <returns>The result of the conversion.</returns>
    public static explicit operator Vector3(Int4 value) => new(value.X, value.Y, value.Z);

    /// <inheritdoc/>
    public override bool Equals(object? obj) => obj is Int4 value && Equals(value);

    /// <summary>
    /// Determines whether the specified <see cref="Int4"/> is equal to this instance.
    /// </summary>
    /// <param name="other">The <see cref="Int4"/> to compare with this instance.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool Equals(Int4 other)
    {
        return X == other.X
            && Y == other.Y
            && Z == other.Z
            && W == other.W;
    }

    /// <summary>
    /// Compares two <see cref="Int4"/> objects for equality.
    /// </summary>
    /// <param name="left">The <see cref="Int4"/> on the left hand of the operand.</param>
    /// <param name="right">The <see cref="Int4"/> on the right hand of the operand.</param>
    /// <returns>
    /// True if the current left is equal to the <paramref name="right"/> parameter; otherwise, false.
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator ==(Int4 left, Int4 right) => left.Equals(right);

    /// <summary>
    /// Compares two <see cref="Int4"/> objects for inequality.
    /// </summary>
    /// <param name="left">The <see cref="Int4"/> on the left hand of the operand.</param>
    /// <param name="right">The <see cref="Int4"/> on the right hand of the operand.</param>
    /// <returns>
    /// True if the current left is unequal to the <paramref name="right"/> parameter; otherwise, false.
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator !=(Int4 left, Int4 right) => !left.Equals(right);

    /// <inheritdoc/>
    public override int GetHashCode() => HashCode.Combine(X, Y, Z, W);

    /// <inheritdoc />
    public override string ToString() => ToString(format: null, formatProvider: null);

    /// <inheritdoc />
    public string ToString(string? format, IFormatProvider? formatProvider)
        => $"{nameof(Int4)} {{ {nameof(X)} = {X.ToString(format, formatProvider)}, {nameof(Y)} = {Y.ToString(format, formatProvider)}, {nameof(Z)} = {Z.ToString(format, formatProvider)}, {nameof(W)} = {W.ToString(format, formatProvider)} }}";


    internal static int GetElement(Int4 vector, int index)
    {
        if (index >= Count)
        {
            throw new ArgumentOutOfRangeException(nameof(index));
        }

        return GetElementUnsafe(ref vector, index);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static int GetElementUnsafe(ref Int4 vector, int index)
    {
        Debug.Assert(index is >= 0 and < Count);

        return Unsafe.Add(ref Unsafe.As<Int4, int>(ref vector), index);
    }
}
