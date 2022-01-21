// Copyright (c) Amer Koleci and contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace Vortice.Mathematics;

[DebuggerDisplay("X={X}, Y={Y}, Width={Width}, Height={Height}")]
public struct Rectangle : IEquatable<Rectangle>, IFormattable
{
    /// <summary>
    /// A <see cref="Rectangle"/> with all of its components set to zero.
    /// </summary>
    public static readonly Rectangle Empty = default;

    public Rectangle(float x, float y, float width, float height)
    {
        if (float.IsNaN(width))
            throw new ArgumentException("NaN is not a valid value for width");
        if (float.IsNaN(height))
            throw new ArgumentException("NaN is not a valid value for height");

        X = x;
        Y = y;
        Width = width;
        Height = height;
    }

    public Rectangle(Point location, Size size)
        : this(location.X, location.Y, size.Width, size.Height)
    {
    }

    /// <summary>
    /// Initializes a new instance of <see cref="Rectangle"/> structure.
    /// </summary>
    /// <param name="width">The width component of the size.</param>
    /// <param name="height">The height component of the size.</param>
    public Rectangle(float width, float height)
        : this(0.0f, 0.0f, width, height)
    {
    }

    /// <summary>
    /// The X of the rectangle.
    /// </summary>
    public float X { get; set; }

    /// <summary>
    /// The Y of the rectangle.
    /// </summary>
    public float Y { get; set; }

    /// <summary>
    /// The width of the rectangle.
    /// </summary>
    public float Width { get; set; }

    /// <summary>
    /// The height of the rectangle.
    /// </summary>
    public float Height { get; set; }

    /// <summary>
    /// Gets a value indicating whether this <see cref="Rectangle"/> is empty.
    /// </summary>
    [EditorBrowsable(EditorBrowsableState.Never)]
    public readonly bool IsEmpty => (Width <= 0) || (Height <= 0);

    public float Top
    {
        get => Y;
        set => Y = value;
    }

    public float Bottom
    {
        get => Y + Height;
        set => Height = value - Y;
    }

    public float Right
    {
        get => X + Width;
        set => Width = value - X;
    }

    public float Left
    {
        get => X;
        set => X = value;
    }

    public Size Size
    {
        get => new(Width, Height);
        set
        {
            Width = value.Width;
            Height = value.Height;
        }
    }

    public Point Location
    {
        get => new Point(X, Y);
        set
        {
            X = value.X;
            Y = value.Y;
        }
    }

    public Point Center => new(X + Width / 2, Y + Height / 2);

    public void Deconstruct(out float x, out float y, out float width, out float height)
    {
        x = X;
        y = Y;
        width = Width;
        height = Height;
    }

    public static Rectangle FromLTRB(float left, float top, float right, float bottom)
    {
        return new Rectangle(left, top, right - left, bottom - top);
    }

    public bool Contains(Rectangle rect)
    {
        return X <= rect.X && Right >= rect.Right && Y <= rect.Y && Bottom >= rect.Bottom;
    }

    public bool Contains(Point pt)
    {
        return Contains(pt.X, pt.Y);
    }

    public bool Contains(float x, float y)
    {
        return (x >= Left) && (x < Right) && (y >= Top) && (y < Bottom);
    }

    public bool IntersectsWith(Rectangle r)
    {
        return !((Left >= r.Right) || (Right <= r.Left) || (Top >= r.Bottom) || (Bottom <= r.Top));
    }

    /// <summary>
    /// Compares two <see cref="Rectangle"/> objects for equality.
    /// </summary>
    /// <param name="left">The <see cref="Rectangle"/> on the left hand of the operand.</param>
    /// <param name="right">The <see cref="Rectangle"/> on the right hand of the operand.</param>
    /// <returns>
    /// True if the current left is equal to the <paramref name="right"/> parameter; otherwise, false.
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator ==(Rectangle left, Rectangle right)
    {
        return left.X == right.X && left.Y == right.Y && left.Width == right.Width && left.Height == right.Height;
    }

    /// <summary>
    /// Compares two <see cref="Rectangle"/> objects for inequality.
    /// </summary>
    /// <param name="left">The <see cref="Rectangle"/> on the left hand of the operand.</param>
    /// <param name="right">The <see cref="Rectangle"/> on the right hand of the operand.</param>
    /// <returns>
    /// True if the current left is unequal to the <paramref name="right"/> parameter; otherwise, false.
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator !=(Rectangle left, Rectangle right)
    {
        return (left.X != right.X) || (left.Y != right.Y) || (left.Width != right.Width) || (left.Height != right.Height);
    }

    /// <inheritdoc/>
    public override bool Equals(object? obj) => obj is Rectangle other && Equals(other);

    /// <inheritdoc/>
    public bool Equals(Rectangle other)
    {
        return
            X.Equals(other.X) &&
            Y.Equals(other.Y) &&
            Width.Equals(other.Width) &&
            Height.Equals(other.Height);
    }

    /// <inheritdoc/>
    public override int GetHashCode() => HashCode.Combine(X, Y, Width, Height);

    /// <inheritdoc />
    public override string ToString() => ToString(format: null, formatProvider: null);

    /// <inheritdoc />
    public string ToString(string? format, IFormatProvider? formatProvider)
        => $"{nameof(Rectangle)} {{ {nameof(X)} = {X.ToString(format, formatProvider)}, {{ {nameof(Y)} = {Y.ToString(format, formatProvider)}, {{ {nameof(Width)} = {Width.ToString(format, formatProvider)}, {nameof(Height)} = {Height.ToString(format, formatProvider)} }}";
}
