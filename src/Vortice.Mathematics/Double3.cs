// Copyright (c) Amer Koleci and contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Vortice.Mathematics;

/// <summary>
/// Vector type containing three 64 bit floating point components.
/// </summary>
[DebuggerDisplay("X={X}, Y={Y}, Z={Z}")]
public struct Double3 : IEquatable<Double3>, IFormattable
{
    /// <summary>
    /// The X component of the vector.
    /// </summary>
    public double X;

    /// <summary>
    /// The Y component of the vector.
    /// </summary>
    public double Y;

    /// <summary>
    /// The Z component of the vector.
    /// </summary>
    public double Z;

    internal const int Count = 3;

    /// <summary>
    /// Initializes a new instance of the <see cref="Double3"/> struct.
    /// </summary>
    /// <param name="value">The value that will be assigned to all components.</param>
    public Double3(double value) : this(value, value, value)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Double3" /> struct.
    /// </summary>
    /// <param name="x">Initial value for the X component of the vector.</param>
    /// <param name="y">Initial value for the Y component of the vector.</param>
    /// <param name="z">Initial value for the Z component of the vector.</param>
    public Double3(double x, double y, double z)
    {
        X = x;
        Y = y;
        Z = z;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Double3" /> struct.
    /// </summary>
    /// <param name="xy">Initial value for the X and Y component of the vector.</param>
    /// <param name="z">Initial value for the Z component of the vector.</param>
    public Double3(Double2 xy, double z)
    {
        X = xy.X;
        Y = xy.Y;
        Z = z;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Double3" /> struct.
    /// </summary>
    /// <param name="values">The span of elements to assign to the vector.</param>
    public Double3(ReadOnlySpan<double> values)
    {
        if (values.Length < 3)
        {
            throw new ArgumentOutOfRangeException(nameof(values), "There must be 3 uint values.");
        }

        this = Unsafe.ReadUnaligned<Double3>(ref Unsafe.As<double, byte>(ref MemoryMarshal.GetReference(values)));
    }

    /// <summary>
    /// A <see cref="Double3"/> with all of its components set to zero.
    /// </summary>
    public static Double3 Zero
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => default;
    }

    /// <summary>
    /// The X unit <see cref="Double3"/> (1, 0, 0).
    /// </summary>
    public static Double3 UnitX
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => new(1, 0, 0);
    }

    /// <summary>
    /// The Y unit <see cref="Double3"/> (0, 1, 0).
    /// </summary>
    public static Double3 UnitY
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => new(0, 1, 0);
    }

    /// <summary>
    /// The Y unit <see cref="Double3"/> (0, 0, 1).
    /// </summary>
    public static Double3 UnitZ
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => new(0, 0, 1);
    }

    /// <summary>
    /// A <see cref="Int3"/> with all of its components set to one.
    /// </summary>
    public static Double3 One
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => new(1, 1, 1);
    }

    public double this[int index]
    {
        get => GetElement(this, index);
        set => this = WithElement(this, index, value);
    }

    public void Deconstruct(out double x, out double y, out double z)
    {
        x = X;
        y = Y;
        z = Z;
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
    public readonly void CopyTo(Span<double> destination)
    {
        if (destination.Length < 3)
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
        if (destination.Length < 3)
        {
            return false;
        }

        Unsafe.WriteUnaligned(ref Unsafe.As<double, byte>(ref MemoryMarshal.GetReference(destination)), this);
        return true;
    }

    /// <summary>
    /// Creates a new <see cref="Double3"/> value with the same value for all its components.
    /// </summary>
    /// <param name="x">The value to use for the components of the new <see cref="Double3"/> instance.</param>
    public static implicit operator Double3(double x) => new(x, x, x);

    /// <summary>
    /// Casts a <see cref="Double3"/> value to a <see cref="UInt3"/> one.
    /// </summary>
    /// <param name="xyz">The input <see cref="Double3"/> value to cast.</param>
    public static explicit operator UInt3(Double3 xyz) => new((uint)xyz.X, (uint)xyz.Y, (uint)xyz.Z);

    /// <summary>
    /// Casts a <see cref="Double3"/> value to a <see cref="Vector3"/> one.
    /// </summary>
    /// <param name="xyz">The input <see cref="Double3"/> value to cast.</param>
    public static implicit operator Vector3(Double3 xyz) => new((float)xyz.X, (float)xyz.Y, (float)xyz.Z);

    /// <inheritdoc/>
    public override bool Equals(object? obj) => obj is Double3 value && Equals(value);

    /// <summary>
    /// Determines whether the specified <see cref="Double3"/> is equal to this instance.
    /// </summary>
    /// <param name="other">The <see cref="Double3"/> to compare with this instance.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool Equals(Double3 other)
    {
        return X == other.X
            && Y == other.Y
            && Z == other.Z;
    }

    /// <summary>
    /// Compares two <see cref="Double3"/> objects for equality.
    /// </summary>
    /// <param name="left">The <see cref="Double3"/> on the left hand of the operand.</param>
    /// <param name="right">The <see cref="Double3"/> on the right hand of the operand.</param>
    /// <returns>
    /// True if the current left is equal to the <paramref name="right"/> parameter; otherwise, false.
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator ==(Double3 left, Double3 right) => left.Equals(right);

    /// <summary>
    /// Compares two <see cref="Double3"/> objects for inequality.
    /// </summary>
    /// <param name="left">The <see cref="Double3"/> on the left hand of the operand.</param>
    /// <param name="right">The <see cref="Double3"/> on the right hand of the operand.</param>
    /// <returns>
    /// True if the current left is unequal to the <paramref name="right"/> parameter; otherwise, false.
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator !=(Double3 left, Double3 right) => !left.Equals(right);

    /// <inheritdoc/>
    public override int GetHashCode() => HashCode.Combine(X, Y, Z);

    /// <inheritdoc />
    public override string ToString() => ToString(format: null, formatProvider: null);

    /// <inheritdoc />
    public string ToString(string? format, IFormatProvider? formatProvider)
        => $"{nameof(Double3)} {{ {nameof(X)} = {X.ToString(format, formatProvider)}, {nameof(Y)} = {Y.ToString(format, formatProvider)}, {nameof(Z)} = {Z.ToString(format, formatProvider)} }}";

    internal static double GetElement(Double3 vector, int index)
    {
        if (index >= Count)
        {
            throw new ArgumentOutOfRangeException(nameof(index));
        }

        return GetElementUnsafe(ref vector, index);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static double GetElementUnsafe(ref Double3 vector, int index)
    {
        Debug.Assert(index is >= 0 and < Count);

        return Unsafe.Add(ref Unsafe.As<Double3, double>(ref vector), index);
    }

    /// <summary>Sets the element at the specified index.</summary>
    /// <param name="vector">The vector of the element to get.</param>
    /// <param name="index">The index of the element to set.</param>
    /// <param name="value">The value of the element to set.</param>
    /// <exception cref="ArgumentOutOfRangeException"><paramref name="index" /> was less than zero or greater than the number of elements.</exception>
    internal static Double3 WithElement(Double3 vector, int index, double value)
    {
        if ((uint)index >= Count)
        {
            throw new ArgumentOutOfRangeException(nameof(index));
        }

        Double3 result = vector;
        SetElementUnsafe(ref result, index, value);
        return result;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static void SetElementUnsafe(ref Double3 vector, int index, double value)
    {
        Debug.Assert(index is >= 0 and < Count);

        Unsafe.Add(ref Unsafe.As<Double3, double>(ref vector), index) = value;
    }
}
