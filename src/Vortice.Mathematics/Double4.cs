// Copyright (c) Amer Koleci and contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

using System.Diagnostics;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Vortice.Mathematics;

/// <summary>
/// Vector type containing four 64 bit floating point components..
/// </summary>
[DebuggerDisplay("X={X}, Y={Y}, Z={Z}, W={W}")]
public struct Double4 : IEquatable<Double4>, IFormattable
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

    /// <summary>
    /// The W component of the vector.
    /// </summary>
    public double W;

    internal const int Count = 4;

    /// <summary>
    /// Initializes a new instance of the <see cref="Double4"/> struct.
    /// </summary>
    /// <param name="value">The value that will be assigned to all components.</param>
    public Double4(double value) : this(value, value, value, value)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Double4" /> struct.
    /// </summary>
    /// <param name="value">A <see cref="Double2"/> containing the values with which to initialize the X and Y components.</param>
    /// <param name="z">Initial value for the Z component of the vector.</param>
    /// <param name="w">Initial value for the W component of the vector.</param>
    public Double4(Double2 value, double z, double w)
    {
        X = value.X;
        Y = value.Y;
        Z = z;
        W = w;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Double4" /> struct.
    /// </summary>
    /// <param name="value">A <see cref="Double3"/> containing the values with which to initialize the X, Y, and Z components.</param>
    /// <param name="w">Initial value for the W component of the vector.</param>
    public Double4(Double3 value, double w)
    {
        X = value.X;
        Y = value.Y;
        Z = value.Z;
        W = w;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Double4" /> struct.
    /// </summary>
    /// <param name="x">Initial value for the X component of the vector.</param>
    /// <param name="y">Initial value for the Y component of the vector.</param>
    /// <param name="z">Initial value for the Z component of the vector.</param>
    /// <param name="w">Initial value for the W component of the vector.</param>
    public Double4(double x, double y, double z, double w)
    {
        X = x;
        Y = y;
        Z = z;
        W = w;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Double4" /> struct.
    /// </summary>
    /// <param name="values">The span of elements to assign to the vector.</param>
    public Double4(ReadOnlySpan<double> values)
    {
        if (values.Length < 4)
        {
            throw new ArgumentOutOfRangeException(nameof(values), "There must be 4 uint values.");
        }

        this = Unsafe.ReadUnaligned<Double4>(ref Unsafe.As<double, byte>(ref MemoryMarshal.GetReference(values)));
    }

    /// <summary>
    /// A <see cref="Double4"/> with all of its components set to zero.
    /// </summary>
    public static Double4 Zero
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => default;
    }

    /// <summary>
    /// The X unit <see cref="Double4"/> (1, 0, 0, 0).
    /// </summary>
    public static Double4 UnitX
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => new(1, 0, 0, 0);
    }

    /// <summary>
    /// The Y unit <see cref="Double4"/> (0, 1, 0, 0).
    /// </summary>
    public static Double4 UnitY
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => new(0, 1, 0, 0);
    }

    /// <summary>
    /// The Y unit <see cref="Double4"/> (0, 0, 1, 0).
    /// </summary>
    public static Double4 UnitZ
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => new(0, 0, 1, 0);
    }

    /// <summary>
    /// The Y unit <see cref="Double4"/> (0, 0, 0, 1).
    /// </summary>
    public static Double4 UnitW
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => new(0, 0, 0, 1);
    }

    /// <summary>
    /// A <see cref="Double4"/> with all of its components set to one.
    /// </summary>
    public static Double4 One
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => new(1, 1, 1, 1);
    }

    /// <summary>Gets or sets the element at the specified index.</summary>
    /// <param name="index">The index of the element to get or set.</param>
    /// <returns>The the element at <paramref name="index" />.</returns>
    /// <exception cref="ArgumentOutOfRangeException"><paramref name="index" /> was less than zero or greater than the number of elements.</exception>
    public double this[int index]
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        readonly get => this.GetElement(index);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        set => this = this.WithElement(index, value);
    }

    public void Deconstruct(out double x, out double y, out double z, out double w)
    {
        x = X;
        y = Y;
        z = Z;
        w = W;
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
    public readonly void CopyTo(Span<double> destination)
    {
        if (destination.Length < 4)
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
        if (destination.Length < 4)
        {
            return false;
        }

        Unsafe.WriteUnaligned(ref Unsafe.As<double, byte>(ref MemoryMarshal.GetReference(destination)), this);
        return true;
    }

    /// <summary>
    /// Creates a new <see cref="Double4"/> value with the same value for all its components.
    /// </summary>
    /// <param name="x">The value to use for the components of the new <see cref="Int4"/> instance.</param>
    public static implicit operator Double4(int x) => new(x, x, x, x);

    /// <summary>
    /// Casts a <see cref="Double4"/> value to a <see cref="UInt4"/> one.
    /// </summary>
    /// <param name="xyzw">The input <see cref="Double4"/> value to cast.</param>
    public static explicit operator UInt4(Double4 xyzw) => new((uint)xyzw.X, (uint)xyzw.Y, (uint)xyzw.Z, (uint)xyzw.W);

    /// <summary>
    /// Performs an explicit conversion from <see cref="Double4" /> to <see cref="Vector4"/>.
    /// </summary>
    /// <param name = "value">The value to convert.</param>
    /// <returns>The result of the conversion.</returns>
    public static implicit operator Vector4(Double4 value) => new((float)value.X, (float)value.Y, (float)value.Z, (float)value.W);

    /// <inheritdoc/>
    public override bool Equals([NotNullWhen(true)] object? obj) => obj is Double4 value && Equals(value);

    /// <summary>
    /// Determines whether the specified <see cref="Double4"/> is equal to this instance.
    /// </summary>
    /// <param name="other">The <see cref="Double4"/> to compare with this instance.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly bool Equals(Double4 other)
    {
        return X == other.X
            && Y == other.Y
            && Z == other.Z
            && W == other.W;
    }

    /// <summary>
    /// Compares two <see cref="Double4"/> objects for equality.
    /// </summary>
    /// <param name="left">The <see cref="Double4"/> on the left hand of the operand.</param>
    /// <param name="right">The <see cref="Double4"/> on the right hand of the operand.</param>
    /// <returns>
    /// True if the current left is equal to the <paramref name="right"/> parameter; otherwise, false.
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator ==(Double4 left, Double4 right) => left.Equals(right);

    /// <summary>
    /// Compares two <see cref="Double4"/> objects for inequality.
    /// </summary>
    /// <param name="left">The <see cref="Double4"/> on the left hand of the operand.</param>
    /// <param name="right">The <see cref="Double4"/> on the right hand of the operand.</param>
    /// <returns>
    /// True if the current left is unequal to the <paramref name="right"/> parameter; otherwise, false.
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator !=(Double4 left, Double4 right) => !left.Equals(right);

    /// <inheritdoc/>
    public override readonly int GetHashCode() => HashCode.Combine(X, Y, Z, W);

    /// <inheritdoc />
    public override readonly string ToString() => ToString(format: null, formatProvider: null);

    /// <inheritdoc />
    public readonly string ToString(string? format, IFormatProvider? formatProvider)
        => $"{nameof(Double4)} {{ {nameof(X)} = {X.ToString(format, formatProvider)}, {nameof(Y)} = {Y.ToString(format, formatProvider)}, {nameof(Z)} = {Z.ToString(format, formatProvider)}, {nameof(W)} = {W.ToString(format, formatProvider)} }}";


    internal static double GetElement(Double4 vector, int index)
    {
        if (index >= Count)
        {
            throw new ArgumentOutOfRangeException(nameof(index));
        }

        return GetElementUnsafe(ref vector, index);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static double GetElementUnsafe(ref Double4 vector, int index)
    {
        Debug.Assert(index is >= 0 and < Count);

        return Unsafe.Add(ref Unsafe.As<Double4, double>(ref vector), index);
    }
}
