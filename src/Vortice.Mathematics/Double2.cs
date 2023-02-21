// Copyright (c) Amer Koleci and contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

using System.Diagnostics;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Vortice.Mathematics;

/// <summary>
/// Vector type containing two 64 bit floating point components.
/// </summary>
[DebuggerDisplay("X={X}, Y={Y}")]
public readonly struct Double2 : IEquatable<Double2>, IFormattable
{
    /// <summary>
    /// The X component of the vector.
    /// </summary>
    public readonly double X;

    /// <summary>
    /// The Y component of the vector.
    /// </summary>
    public readonly double Y;

    internal const int Count = 2;

    /// <summary>
    /// Initializes a new instance of the <see cref="Double2"/> struct.
    /// </summary>
    /// <param name="value">The value that will be assigned to all components.</param>
    public Double2(double value) : this(value, value)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Double2" /> struct.
    /// </summary>
    /// <param name="x">Initial value for the X component of the vector.</param>
    /// <param name="y">Initial value for the Y component of the vector.</param>
    public Double2(double x, double y)
    {
        X = x;
        Y = y;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Int2" /> struct.
    /// </summary>
    /// <param name="values">The span of elements to assign to the vector.</param>
    public Double2(ReadOnlySpan<double> values)
    {
        if (values.Length < 2)
        {
            throw new ArgumentOutOfRangeException(nameof(values), "There must be 2 uint values.");
        }

        this = Unsafe.ReadUnaligned<Double2>(ref Unsafe.As<double, byte>(ref MemoryMarshal.GetReference(values)));
    }

    /// <summary>
    /// A <see cref="Double2"/> with all of its components set to zero.
    /// </summary>
    public static Double2 Zero
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => default;
    }

    /// <summary>
    /// The X unit <see cref="Double2"/> (1, 0).
    /// </summary>
    public static Double2 UnitX
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => new(1.0, 0);
    }

    /// <summary>
    /// The Y unit <see cref="Double2"/> (0, 1).
    /// </summary>
    public static Double2 UnitY
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => new(0, 1);
    }

    /// <summary>
    /// A <see cref="Int2"/> with all of its components set to one.
    /// </summary>
    public static Int2 One
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => new(1, 1);
    }

    public readonly double this[int index] => GetElement(this, index);

    public void Deconstruct(out double x, out double y)
    {
        x = X;
        y = Y;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly void CopyTo(double[] array)
    {
        CopyTo(array, 0);
    }

    public readonly void CopyTo(double[] array, int index)
    {
        if (array is null)
        {
            throw new NullReferenceException(nameof(array));
        }

        if ((index < 0) || (index >= array.Length))
        {
            throw new ArgumentOutOfRangeException(nameof(index));
        }

        if ((array.Length - index) < 2)
        {
            throw new ArgumentOutOfRangeException(nameof(index));
        }

        array[index] = X;
        array[index + 1] = Y;
    }

    /// <summary>Copies the vector to the given <see cref="Span{T}" />.The length of the destination span must be at least 2.</summary>
    /// <param name="destination">The destination span which the values are copied into.</param>
    /// <exception cref="ArgumentException">If number of elements in source vector is greater than those available in destination span.</exception>
    public readonly void CopyTo(Span<double> destination)
    {
        if (destination.Length < 2)
        {
            throw new ArgumentOutOfRangeException(nameof(destination));
        }

        Unsafe.WriteUnaligned(ref Unsafe.As<double, byte>(ref MemoryMarshal.GetReference(destination)), this);
    }

    /// <summary>Attempts to copy the vector to the given <see cref="Span{Double}" />. The length of the destination span must be at least 2.</summary>
    /// <param name="destination">The destination span which the values are copied into.</param>
    /// <returns><see langword="true" /> if the source vector was successfully copied to <paramref name="destination" />. <see langword="false" /> if <paramref name="destination" /> is not large enough to hold the source vector.</returns>
    public readonly bool TryCopyTo(Span<double> destination)
    {
        if (destination.Length < 2)
        {
            return false;
        }

        Unsafe.WriteUnaligned(ref Unsafe.As<double, byte>(ref MemoryMarshal.GetReference(destination)), this);

        return true;
    }

    /// <summary>
    /// Creates a new <see cref="Double2"/> value with the same value for all its components.
    /// </summary>
    /// <param name="x">The value to use for the components of the new <see cref="Double2"/> instance.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static implicit operator Double2(double x) => new(x, x);

    /// <summary>
    /// Casts a <see cref="Double2"/> value to a <see cref="UInt2"/> one.
    /// </summary>
    /// <param name="xy">The input <see cref="Double2"/> value to cast.</param>
    public static explicit operator UInt2(Double2 xy) => new((uint)xy.X, (uint)xy.Y);

    /// <summary>
    /// Performs an explicit conversion from <see cref="Double2" /> to <see cref="Vector2"/>.
    /// </summary>
    /// <param name="xy">The value to convert.</param>
    /// <returns>The result of the conversion.</returns>
    public static explicit operator Vector2(Double2 xy) => new((float)xy.X, (float)xy.Y);

    /// <inheritdoc/>
    public override bool Equals(object? obj) => obj is Double2 value && Equals(value);

    /// <summary>
    /// Determines whether the specified <see cref="Double2"/> is equal to this instance.
    /// </summary>
    /// <param name="other">The <see cref="Double2"/> to compare with this instance.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool Equals(Double2 other)
    {
        return X == other.X
            && Y == other.Y;
    }

    /// <summary>
    /// Compares two <see cref="Double2"/> objects for equality.
    /// </summary>
    /// <param name="left">The <see cref="Double2"/> on the left hand of the operand.</param>
    /// <param name="right">The <see cref="Double2"/> on the right hand of the operand.</param>
    /// <returns>
    /// True if the current left is equal to the <paramref name="right"/> parameter; otherwise, false.
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator ==(Double2 left, Double2 right) => left.Equals(right);

    /// <summary>
    /// Compares two <see cref="Double2"/> objects for inequality.
    /// </summary>
    /// <param name="left">The <see cref="Double2"/> on the left hand of the operand.</param>
    /// <param name="right">The <see cref="Double2"/> on the right hand of the operand.</param>
    /// <returns>
    /// True if the current left is unequal to the <paramref name="right"/> parameter; otherwise, false.
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator !=(Double2 left, Double2 right) => !left.Equals(right);

    /// <inheritdoc/>
    public override int GetHashCode() => HashCode.Combine(X, Y);

    /// <inheritdoc />
    public override string ToString() => ToString(format: null, formatProvider: null);

    /// <inheritdoc />
    public string ToString(string? format, IFormatProvider? formatProvider)
        => $"{nameof(Double2)} {{ {nameof(X)} = {X.ToString(format, formatProvider)}, {nameof(Y)} = {Y.ToString(format, formatProvider)} }}";

    internal static double GetElement(Double2 vector, int index)
    {
        if (index >= Count)
        {
            throw new ArgumentOutOfRangeException(nameof(index));
        }

        return GetElementUnsafe(ref vector, index);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static double GetElementUnsafe(ref Double2 vector, int index)
    {
        Debug.Assert(index is >= 0 and < Count);

        return Unsafe.Add(ref Unsafe.As<Double2, double>(ref vector), index);
    }
}
