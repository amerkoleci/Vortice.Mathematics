// Copyright (c) Amer Koleci and contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Numerics;
using System.Runtime.CompilerServices;

namespace Vortice.Mathematics;

/// <summary>
/// Stores an ordered pair of floating-point numbers describing the width, height and depth of a rectangle.
/// </summary>
public struct Size : IEquatable<Size>
{
    /// <summary>
    /// A <see cref="Size"/> with all of its components set to zero.
    /// </summary>
    public static Size Empty => default;

    /// <summary>
    /// The width component of the size.
    /// </summary>
    public float Width;

    /// <summary>
    /// The height component of the size.
    /// </summary>
    public float Height;

    /// <summary>
    /// Initializes a new instance of <see cref="Size"/> structure.
    /// </summary>
    /// <param name="width">The width component of the size.</param>
    /// <param name="height">The height component of the size.</param>
    public Size(float width, float height)
    {
        Width = width;
        Height = height;
    }

    /// <summary>
    /// Initializes a new instance of <see cref="Size"/> structure.
    /// </summary>
    /// <param name="vector">The width/height vector.</param>
    public Size(in Vector2 vector)
    {
        Width = vector.X;
        Height = vector.Y;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref='Size'/> struct from the specified <see cref="System.Drawing.SizeF"/>.
    /// </summary>
    public Size(in System.Drawing.SizeF size)
    {
        Width = size.Width;
        Height = size.Height;
    }

    /// <summary>
    /// Gets a value indicating whether this <see cref="Size"/> is empty.
    /// </summary>
    [EditorBrowsable(EditorBrowsableState.Never)]
    public readonly bool IsEmpty => Width == 0 && Height == 0;

    /// <summary>
    /// Deconstructs this size into two float values.
    /// </summary>
    /// <param name="width">The out value for the width.</param>
    /// <param name="height">The out value for the height.</param>
    public readonly void Deconstruct(out float width, out float height)
    {
        width = Width;
        height = Height;
    }

    /// <summary>
    /// Creates a new <see cref="Vector2"/> from this <see cref="Size"/>.
    /// </summary>
    public readonly Vector2 ToVector2() => new(Width, Height);

    /// <summary>
    /// Creates a new <see cref="SizeI"/> from this <see cref="Size"/>.
    /// </summary>
    /// <returns></returns>
    public readonly SizeI ToSizeI() => SizeI.Truncate(this);

    /// <summary>
    /// Adds two sizes.
    /// </summary>
    /// <param name="size1">First size.</param>
    /// <param name="size2">Second size.</param>
    /// <returns></returns>
    public static Size Add(Size size1, Size size2) => new(size1.Width + size2.Width, size1.Height + size2.Height);

    /// <summary>
    /// Subtracts two sizes.
    /// </summary>
    /// <param name="size1">First size.</param>
    /// <param name="size2">Second size.</param>
    /// <returns></returns>
    public static Size Subtract(Size size1, Size size2) => new(size1.Width - size2.Width, size1.Height - size2.Height);

    /// <summary>
    /// Multiplies two sizes.
    /// </summary>
    /// <param name="size1">First size.</param>
    /// <param name="size2">Second size.</param>
    /// <returns></returns>
    public static Size Multiply(Size size1, Size size2) => new(size1.Width * size2.Width, size1.Height * size2.Height);

    /// <summary>
    /// Multiplies a size by a scalar value.
    /// </summary>
    /// <param name="size">First size.</param>
    /// <param name="scalar">Scalar value.</param>
    /// <returns></returns>
    public static Size Multiply(Size size, float scalar) => new(size.Width * scalar, size.Height * scalar);

    /// <summary>
    /// Divides two sizes.
    /// </summary>
    /// <param name="size1">First size.</param>
    /// <param name="size2">Second size.</param>
    /// <returns></returns>
    public static Size Divide(Size size1, Size size2) => new(size1.Width / size2.Width, size1.Height / size2.Height);

    /// <summary>
    /// Divides a size by a scalar value.
    /// </summary>
    /// <param name="size">Source size.</param>
    /// <param name="divider">The divider.</param>
    /// <returns></returns>
    public static Size Divide(Size size, float divider)
    {
        float invRhs = 1f / divider;
        return new(size.Width * invRhs, size.Height * invRhs);
    }

    /// <summary>
    /// Compares two <see cref="Size"/> objects for equality.
    /// </summary>
    /// <param name="left">The <see cref="Size"/> on the left hand of the operand.</param>
    /// <param name="right">The <see cref="Size"/> on the right hand of the operand.</param>
    /// <returns>
    /// True if the current left is equal to the <paramref name="right"/> parameter; otherwise, false.
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator ==(Size left, Size right) => left.Width == right.Width && left.Height == right.Height;

    /// <summary>
    /// Compares two <see cref="Size"/> objects for inequality.
    /// </summary>
    /// <param name="left">The <see cref="Size"/> on the left hand of the operand.</param>
    /// <param name="right">The <see cref="Size"/> on the right hand of the operand.</param>
    /// <returns>
    /// True if the current left is unequal to the <paramref name="right"/> parameter; otherwise, false.
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator !=(Size left, Size right) => !(left == right);

    /// <summary>
    /// Performs vector addition of two <see cref='Size'/>.
    /// </summary>
    public static Size operator +(Size size1, Size size2) => new(size1.Width + size2.Width, size1.Height + size2.Height);

    /// <summary>
    /// Contracts a <see cref='Size'/> by another <see cref='Size'/>
    /// </summary>
    public static Size operator -(Size size1, Size size2) => new(size1.Width - size2.Width, size1.Height - size2.Height);

    /// <summary>
    /// Multiplies <see cref="Size"/> by a <see cref="float"/> producing <see cref="Size"/>.
    /// </summary>
    /// <param name="left">Multiplier of type <see cref="float"/>.</param>
    /// <param name="right">Multiplicand of type <see cref="Size"/>.</param>
    /// <returns>Product of type <see cref="Size"/>.</returns>
    public static Size operator *(Size left, Size right) => Multiply(right, left);

    /// <summary>
    /// Multiplies <see cref="Size"/> by a <see cref="float"/> producing <see cref="Size"/>.
    /// </summary>
    /// <param name="left">Multiplier of type <see cref="float"/>.</param>
    /// <param name="right">Multiplicand of type <see cref="Size"/>.</param>
    /// <returns>Product of type <see cref="Size"/>.</returns>
    public static Size operator *(float left, Size right) => Multiply(right, left);

    /// <summary>
    /// Multiplies <see cref="Size"/> by a <see cref="float"/> producing <see cref="Size"/>.
    /// </summary>
    /// <param name="left">Multiplicand of type <see cref="Size"/>.</param>
    /// <param name="right">Multiplier of type <see cref="float"/>.</param>
    /// <returns>Product of type <see cref="Size"/>.</returns>
    public static Size operator *(Size left, float right) => Multiply(left, right);

    /// <summary>
    /// Divides <see cref="Size"/> by a <see cref="Size"/> producing <see cref="Size"/>.
    /// </summary>
    /// <param name="left">Dividend of type <see cref="Size"/>.</param>
    /// <param name="right">Divisor of type <see cref="Size"/>.</param>
    /// <returns>Result of type <see cref="Size"/>.</returns>
    public static Size operator /(Size left, Size right) => new(left.Width / right.Width, left.Height / right.Height);

    /// <summary>
    /// Divides <see cref="Size"/> by a <see cref="float"/> producing <see cref="Size"/>.
    /// </summary>
    /// <param name="left">Dividend of type <see cref="Size"/>.</param>
    /// <param name="right">Divisor of type <see cref="int"/>.</param>
    /// <returns>Result of type <see cref="Size"/>.</returns>
    public static Size operator /(Size left, float right) => new(left.Width / right, left.Height / right);

    /// <summary>
    /// Performs an explicit conversion from <see cref="System.Drawing.SizeF"/> to <see cref="Size"/>.
    /// </summary>
    /// <param name="value">The value.</param>
    /// <returns>The result of the conversion.</returns>
    public static explicit operator Size(in System.Drawing.SizeF value) => new(in value);

    /// <summary>
    /// Performs an explicit conversion from <see cref="Size"/> to <see cref="System.Drawing.SizeF"/>.
    /// </summary>
    /// <param name="value">The value.</param>
    /// <returns>The result of the conversion.</returns>
    public static explicit operator System.Drawing.SizeF(in Size value) => new(value.Width, value.Height);

    /// <summary>
    /// Converts the specified <see cref="Size"/> to a <see cref="Vector2"/>.
    /// </summary>
    public static explicit operator Vector2(in Size size) => size.ToVector2();

    /// <summary>
    /// Converts the specified <see cref="Vector2"/> to a <see cref="Size"/>.
    /// </summary>
    public static explicit operator Size(in Vector2 vector) => new(vector);

    /// <inheritdoc/>
    public override readonly bool Equals([NotNullWhen(true)] object? obj) => obj is Size value && Equals(value);

    public readonly bool Equals(Size other) => this == other;

    /// <inheritdoc/>
    public override readonly int GetHashCode() => HashCode.Combine(Width, Height);

    /// <inheritdoc />
    public override readonly string ToString() => $"{{Width={Width}, Height={Height}}}";
}
