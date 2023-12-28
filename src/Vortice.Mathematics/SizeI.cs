// Copyright (c) Amer Koleci and contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace Vortice.Mathematics;

/// <summary>
/// Stores an ordered pair of integer numbers describing the width, height and depth of a rectangle.
/// </summary>
public struct SizeI : IEquatable<SizeI>
{
    /// <summary>
    /// A <see cref="SizeI"/> with all of its components set to zero.
    /// </summary>
    public static SizeI Empty => default;

    /// <summary>
    /// The width component of the size.
    /// </summary>
    public int Width;

    /// <summary>
    /// The height component of the size.
    /// </summary>
    public int Height;

    /// <summary>
    /// Initializes a new instance of <see cref="SizeI"/> structure.
    /// </summary>
    /// <param name="width">The width component of the size.</param>
    /// <param name="height">The height component of the size.</param>
    public SizeI(int width, int height)
    {
        Width = width;
        Height = height;
    }

    /// <summary>
    /// Initializes a new instance of <see cref="SizeI"/> structure.
    /// </summary>
    /// <param name="vector">The width/height vector.</param>
    public SizeI(in Int2 vector)
    {
        Width = vector.X;
        Height = vector.Y;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref='SizeI'/> struct from the specified <see cref="System.Drawing.Size"/>.
    /// </summary>
    public SizeI(in System.Drawing.Size size)
    {
        Width = size.Width;
        Height = size.Height;
    }

    /// <summary>
    /// Gets a value indicating whether this <see cref="SizeI"/> is empty.
    /// </summary>
    [EditorBrowsable(EditorBrowsableState.Never)]
    public readonly bool IsEmpty => Width == 0 && Height == 0;

    /// <summary>
    /// Deconstructs this size into two integers.
    /// </summary>
    /// <param name="width">The out value for the width.</param>
    /// <param name="height">The out value for the height.</param>
    public readonly void Deconstruct(out int width, out int height)
    {
        width = Width;
        height = Height;
    }

    /// <summary>
    /// Adds two sizes.
    /// </summary>
    /// <param name="size1">First size.</param>
    /// <param name="size2">Second size.</param>
    /// <returns></returns>
    public static SizeI Add(SizeI size1, SizeI size2) => new(size1.Width + size2.Width, size1.Height + size2.Height);

    /// <summary>
    /// Subtracts two sizes.
    /// </summary>
    /// <param name="size1">First size.</param>
    /// <param name="size2">Second size.</param>
    /// <returns></returns>
    public static SizeI Subtract(SizeI size1, SizeI size2) => new(size1.Width - size2.Width, size1.Height - size2.Height);

    /// <summary>
    /// Multiplies two sizes.
    /// </summary>
    /// <param name="size1">First size.</param>
    /// <param name="size2">Second size.</param>
    /// <returns></returns>
    public static SizeI Multiply(SizeI size1, SizeI size2) => new(size1.Width * size2.Width, size1.Height * size2.Height);

    /// <summary>
    /// Multiplies a size by a scalar value.
    /// </summary>
    /// <param name="size">First size.</param>
    /// <param name="multiplier">Multiplier value.</param>
    /// <returns></returns>
    public static SizeI Multiply(SizeI size, int multiplier) => new(size.Width * multiplier, size.Height * multiplier);

    /// <summary>
    /// Multiplies a size by a scalar value.
    /// </summary>
    /// <param name="size">First size.</param>
    /// <param name="multiplier">Multiplier value.</param>
    /// <returns></returns>
    public static Size Multiply(SizeI size, float multiplier) => new(size.Width * multiplier, size.Height * multiplier);

    /// <summary>
    /// Divides two sizes.
    /// </summary>
    /// <param name="size1">First size.</param>
    /// <param name="size2">Second size.</param>
    /// <returns></returns>
    public static SizeI Divide(SizeI size1, SizeI size2) => new(size1.Width / size2.Width, size1.Height / size2.Height);

    /// <summary>
    /// Converts a <see cref="Size"/> to a <see cref="SizeI"/> by performing a truncate operation on all the coordinates.
    /// </summary>
    public static SizeI Truncate(in Size value) => new(unchecked((int)value.Width), unchecked((int)value.Height));

    /// <summary>
    /// Converts a <see cref="Size"/> to a <see cref="SizeI"/> by performing a round operation on all the coordinates.
    /// </summary>
    public static SizeI Round(in Size value) => new(unchecked((int)Math.Round(value.Width)), unchecked((int)Math.Round(value.Height)));

    /// <summary>
    /// Compares two <see cref="SizeI"/> objects for equality.
    /// </summary>
    /// <param name="left">The <see cref="SizeI"/> on the left hand of the operand.</param>
    /// <param name="right">The <see cref="SizeI"/> on the right hand of the operand.</param>
    /// <returns>
    /// True if the current left is equal to the <paramref name="right"/> parameter; otherwise, false.
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator ==(SizeI left, SizeI right) => left.Width == right.Width && left.Height == right.Height;

    /// <summary>
    /// Compares two <see cref="SizeI"/> objects for inequality.
    /// </summary>
    /// <param name="left">The <see cref="SizeI"/> on the left hand of the operand.</param>
    /// <param name="right">The <see cref="SizeI"/> on the right hand of the operand.</param>
    /// <returns>
    /// True if the current left is unequal to the <paramref name="right"/> parameter; otherwise, false.
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator !=(SizeI left, SizeI right) => !(left == right);

    /// <summary>
    /// Performs vector addition of two <see cref='SizeI'/> objects.
    /// </summary>
    public static SizeI operator +(SizeI sz1, SizeI sz2) => Add(sz1, sz2);

    /// <summary>
    /// Contracts a <see cref='SizeI'/> by another <see cref='SizeI'/>
    /// </summary>
    public static SizeI operator -(SizeI sz1, SizeI sz2) => Subtract(sz1, sz2);

    /// <summary>
    /// Multiplies a <see cref="SizeI"/> by an <see cref="int"/> producing <see cref="SizeI"/>.
    /// </summary>
    /// <param name="left">Multiplier of type <see cref="int"/>.</param>
    /// <param name="right">Multiplicand of type <see cref="SizeI"/>.</param>
    /// <returns>Product of type <see cref="SizeI"/>.</returns>
    public static SizeI operator *(int left, SizeI right) => Multiply(right, left);

    /// <summary>
    /// Multiplies <see cref="SizeI"/> by an <see cref="int"/> producing <see cref="SizeI"/>.
    /// </summary>
    /// <param name="left">Multiplicand of type <see cref="SizeI"/>.</param>
    /// <param name="right">Multiplier of type <see cref="int"/>.</param>
    /// <returns>Product of type <see cref="SizeI"/>.</returns>
    public static SizeI operator *(SizeI left, int right) => Multiply(left, right);

    /// <summary>
    /// Divides <see cref="SizeI"/> by an <see cref="int"/> producing <see cref="SizeI"/>.
    /// </summary>
    /// <param name="left">Dividend of type <see cref="SizeI"/>.</param>
    /// <param name="right">Divisor of type <see cref="int"/>.</param>
    /// <returns>Result of type <see cref="SizeI"/>.</returns>
    public static SizeI operator /(SizeI left, int right) => new(unchecked(left.Width / right), unchecked(left.Height / right));

    /// <summary>
    /// Multiplies <see cref="SizeI"/> by a <see cref="float"/> producing <see cref="Size"/>.
    /// </summary>
    /// <param name="left">Multiplier of type <see cref="float"/>.</param>
    /// <param name="right">Multiplicand of type <see cref="SizeI"/>.</param>
    /// <returns>Product of type <see cref="Size"/>.</returns>
    public static Size operator *(float left, SizeI right) => Multiply(right, left);

    /// <summary>
    /// Multiplies <see cref="SizeI"/> by a <see cref="float"/> producing <see cref="Size"/>.
    /// </summary>
    /// <param name="left">Multiplicand of type <see cref="SizeI"/>.</param>
    /// <param name="right">Multiplier of type <see cref="float"/>.</param>
    /// <returns>Product of type <see cref="Size"/>.</returns>
    public static Size operator *(SizeI left, float right) => Multiply(left, right);

    /// <summary>
    /// Divides <see cref="SizeI"/> by a <see cref="float"/> producing <see cref="Size"/>.
    /// </summary>
    /// <param name="left">Dividend of type <see cref="SizeI"/>.</param>
    /// <param name="right">Divisor of type <see cref="float"/>.</param>
    /// <returns>Result of type <see cref="Size"/>.</returns>
    public static Size operator /(SizeI left, float right) => new(left.Width / right, left.Height / right);

    /// <summary>
    /// Converts the specified <see cref='SizeI'/> to a <see cref='Size'/>.
    /// </summary>
    public static implicit operator Size(SizeI size) => new Size(size.Width, size.Height);

    /// <summary>
    /// Performs an explicit conversion from <see cref="System.Drawing.Size"/> to <see cref="SizeI"/>.
    /// </summary>
    /// <param name="value">The value.</param>
    /// <returns>The result of the conversion.</returns>
    public static explicit operator SizeI(in System.Drawing.Size value) => new(in value);

    /// <summary>
    /// Performs an explicit conversion from <see cref="SizeI"/> to <see cref="System.Drawing.Size"/>.
    /// </summary>
    /// <param name="value">The value.</param>
    /// <returns>The result of the conversion.</returns>
    public static explicit operator System.Drawing.Size(in SizeI value) => new(value.Width, value.Height);

    /// <summary>
    /// Converts the specified <see cref='SizeI'/> to a <see cref='Int2'/>.
    /// </summary>
    public static explicit operator Int2(SizeI size) => new(size.Width, size.Height);


    //// <inheritdoc/>
    public override readonly bool Equals([NotNullWhen(true)] object? obj) => obj is SizeI value && Equals(value);

    public readonly bool Equals(SizeI other) => this == other;

    /// <inheritdoc/>
    public override readonly int GetHashCode() => HashCode.Combine(Width, Height);

    /// <inheritdoc />
    public override readonly string ToString() => $"{{Width={Width}, Height={Height}}}";
}
