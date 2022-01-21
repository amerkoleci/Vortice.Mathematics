// Copyright (c) Amer Koleci and contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace Vortice.Mathematics;

[DebuggerDisplay("X={X}, Y={Y}, Width={Width}, Height={Height}")]
public struct RectangleI : IEquatable<RectangleI>, IFormattable
{
    /// <summary>
    /// A <see cref="RectangleI"/> with all of its components set to zero.
    /// </summary>
    public static readonly RectangleI Empty = default;

    public RectangleI(int x, int y, int width, int height)
    {
        X = x;
        Y = y;
        Width = width;
        Height = height;
    }

    public RectangleI(PointI location, SizeI size)
        : this(location.X, location.Y, size.Width, size.Height)
    {
    }

    /// <summary>
    /// Initializes a new instance of <see cref="RectangleI"/> structure.
    /// </summary>
    /// <param name="width">The width component of the size.</param>
    /// <param name="height">The height component of the size.</param>
    public RectangleI(int width, int height)
        : this(0, 0, width, height)
    {
    }

    /// <summary>
    /// The X of the rectangle.
    /// </summary>
    public int X { get; set; }

    /// <summary>
    /// The Y of the rectangle.
    /// </summary>
    public int Y { get; set; }

    /// <summary>
    /// The width of the rectangle.
    /// </summary>
    public int Width { get; set; }

    /// <summary>
    /// The height of the rectangle.
    /// </summary>
    public int Height { get; set; }

    /// <summary>
    /// Gets a value indicating whether this <see cref="RectangleI"/> is empty.
    /// </summary>
    [EditorBrowsable(EditorBrowsableState.Never)]
    public readonly bool IsEmpty => (Width <= 0) || (Height <= 0);

    public int Top
    {
        get => Y;
        set => Y = value;
    }

    public int Bottom
    {
        get => Y + Height;
        set => Height = value - Y;
    }

    public int Right
    {
        get => X + Width;
        set => Width = value - X;
    }

    public int Left
    {
        get => X;
        set => X = value;
    }

    public SizeI Size
    {
        get => new(Width, Height);
        set
        {
            Width = value.Width;
            Height = value.Height;
        }
    }

    public PointI Location
    {
        get => new(X, Y);
        set
        {
            X = value.X;
            Y = value.Y;
        }
    }

    public PointI Center => new(X + Width / 2, Y + Height / 2);

    public void Deconstruct(out int x, out int y, out int width, out int height)
    {
        x = X;
        y = Y;
        width = Width;
        height = Height;
    }

    public static RectangleI FromLTRB(int left, int top, int right, int bottom)
    {
        return new RectangleI(left, top, right - left, bottom - top);
    }

    public bool Contains(RectangleI rect)
    {
        return X <= rect.X && Right >= rect.Right && Y <= rect.Y && Bottom >= rect.Bottom;
    }

    public bool Contains(PointI pt)
    {
        return Contains(pt.X, pt.Y);
    }

    public bool Contains(int x, int y)
    {
        return (x >= Left) && (x < Right) && (y >= Top) && (y < Bottom);
    }

    public bool IntersectsWith(RectangleI r)
    {
        return !((Left >= r.Right) || (Right <= r.Left) || (Top >= r.Bottom) || (Bottom <= r.Top));
    }

    /// <summary>
    /// Compares two <see cref="RectangleI"/> objects for equality.
    /// </summary>
    /// <param name="left">The <see cref="RectangleI"/> on the left hand of the operand.</param>
    /// <param name="right">The <see cref="RectangleI"/> on the right hand of the operand.</param>
    /// <returns>
    /// True if the current left is equal to the <paramref name="right"/> parameter; otherwise, false.
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator ==(RectangleI left, RectangleI right)
    {
        return left.X == right.X && left.Y == right.Y && left.Width == right.Width && left.Height == right.Height;
    }

    /// <summary>
    /// Compares two <see cref="RectangleI"/> objects for inequality.
    /// </summary>
    /// <param name="left">The <see cref="RectangleI"/> on the left hand of the operand.</param>
    /// <param name="right">The <see cref="RectangleI"/> on the right hand of the operand.</param>
    /// <returns>
    /// True if the current left is unequal to the <paramref name="right"/> parameter; otherwise, false.
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator !=(RectangleI left, RectangleI right)
    {
        return (left.X != right.X) || (left.Y != right.Y) || (left.Width != right.Width) || (left.Height != right.Height);
    }

    /// <inheritdoc/>
    public override bool Equals(object? obj) => obj is RectangleI other && Equals(other);

    /// <inheritdoc/>
    public bool Equals(RectangleI other)
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
        => $"{nameof(RectangleI)} {{ {nameof(X)} = {X.ToString(format, formatProvider)}, {{ {nameof(Y)} = {Y.ToString(format, formatProvider)}, {{ {nameof(Width)} = {Width.ToString(format, formatProvider)}, {nameof(Height)} = {Height.ToString(format, formatProvider)} }}";
}
