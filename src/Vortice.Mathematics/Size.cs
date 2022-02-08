// Copyright (c) Amer Koleci and contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.CompilerServices;

namespace Vortice.Mathematics;

[DebuggerDisplay("Width={Width}, Height={Height}")]
public struct Size : IEquatable<Size>, IFormattable
{
    /// <summary>
    /// A <see cref="Size"/> with all of its components set to zero.
    /// </summary>
    public static readonly Size Empty = default;

    public Size(float size = 0)
    {
        if (float.IsNaN(size))
            throw new ArgumentException("NaN is not a valid value for size");

        Width = size;
        Height = size;
    }

    /// <summary>
    /// Initializes a new instance of <see cref="Size"/> structure.
    /// </summary>
    /// <param name="width">The width component of the size.</param>
    /// <param name="height">The height component of the size.</param>
    public Size(float width, float height)
    {
        if (float.IsNaN(width))
            throw new ArgumentException("NaN is not a valid value for width");
        if (float.IsNaN(height))
            throw new ArgumentException("NaN is not a valid value for height");

        Width = width;
        Height = height;
    }

    /// <summary>
    /// Initializes a new instance of <see cref="Size"/> structure.
    /// </summary>
    /// <param name="vector">The vector.</param>
    public Size(Vector2 vector)
    {
        if (float.IsNaN(vector.X))
            throw new ArgumentException("NaN is not a valid value for width");
        if (float.IsNaN(vector.Y))
            throw new ArgumentException("NaN is not a valid value for height");

        Width = vector.X;
        Height = vector.Y;
    }

    /// <summary>
    /// The width of the size.
    /// </summary>
    public float Width { get; set; }

    /// <summary>
    /// The height of the size.
    /// </summary>
    public float Height { get; set; }

    /// <summary>
    /// Gets a value indicating whether this <see cref="Size"/> is empty.
    /// </summary>
    [EditorBrowsable(EditorBrowsableState.Never)]
    public readonly bool IsEmpty => Width == 0 && Height == 0;

    public void Deconstruct(out float width, out float height)
    {
        width = Width;
        height = Height;
    }

    /// <summary>
    /// Performs an explicit conversion from <see cref="Size"/> to <see cref="Vector2"/>.
    /// </summary>
    /// <param name="size">The value.</param>
    /// <returns>The result of the conversion.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static explicit operator Vector2(Size size) => new(size.Width, size.Height);

    /// <summary>
    /// Performs an implicit conversion from <see cref="SizeI"/> to <see cref="Size"/>.
    /// </summary>
    /// <param name="size">The value.</param>
    /// <returns>The result of the conversion.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static implicit operator Size(SizeI size) => new(size.Width, size.Height);

    public static Size operator +(Size size1, Size size2)
    {
        return new(size1.Width + size2.Width, size1.Height + size2.Height);
    }

    public static Size operator -(Size size1, Size size2)
    {
        return new Size(size1.Width - size2.Width, size1.Height - size2.Height);
    }

    public static Size operator *(Size size, float value)
    {
        return new Size(size.Width * value, size.Height * value);
    }

    public static Size operator /(Size size, float value)
    {
        return new Size(size.Width / value, size.Height / value);
    }

    public readonly Vector2 ToVector2() => new(Width, Height);

    public static bool TryParse(string value, out Size size)
    {
        if (!string.IsNullOrEmpty(value))
        {
            string[] wh = value.Split(',');
            if (wh.Length == 2
                && float.TryParse(wh[0], NumberStyles.Number, CultureInfo.InvariantCulture, out float w)
                && float.TryParse(wh[1], NumberStyles.Number, CultureInfo.InvariantCulture, out float h))
            {
                size = new Size(w, h);
                return true;
            }
        }

        size = default;
        return false;
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
    public static bool operator !=(Size left, Size right) => (left.Width != right.Width) || (left.Height != right.Height);

    /// <inheritdoc/>
    public override bool Equals(object? obj) => obj is Size other && Equals(other);

    /// <inheritdoc/>
    public bool Equals(Size other)
    {
        return Width.Equals(other.Width) && Height.Equals(other.Height);
    }

    /// <inheritdoc/>
    public override int GetHashCode() => HashCode.Combine(Width, Height);

    /// <inheritdoc />
    public override string ToString() => ToString(format: null, formatProvider: null);

    /// <inheritdoc />
    public string ToString(string? format, IFormatProvider? formatProvider)
        => $"{nameof(Size)} {{ {nameof(Width)} = {Width.ToString(format, formatProvider)}, {nameof(Height)} = {Height.ToString(format, formatProvider)} }}";
}
