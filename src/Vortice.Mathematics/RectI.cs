// Copyright (c) Amer Koleci and contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace Vortice.Mathematics;

[DebuggerDisplay("X={X}, Y={Y}, Width={Width}, Height={Height}")]
public struct RectI : IEquatable<RectI>, IFormattable
{
    /// <summary>
    /// A <see cref="RectI"/> with all of its components set to zero.
    /// </summary>
    public static readonly RectI Empty = default;

    public RectI(int x, int y, int width, int height)
    {
        X = x;
        Y = y;
        Width = width;
        Height = height;
    }

    public RectI(Int2 location, SizeI size)
        : this(location.X, location.Y, size.Width, size.Height)
    {
    }

    /// <summary>
    /// Initializes a new instance of <see cref="RectI"/> structure.
    /// </summary>
    /// <param name="width">The width component of the size.</param>
    /// <param name="height">The height component of the size.</param>
    public RectI(int width, int height)
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
    /// Gets a value indicating whether this <see cref="RectI"/> is empty.
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

    public Int2 Location
    {
        get => new(X, Y);
        set
        {
            X = value.X;
            Y = value.Y;
        }
    }

    public Int2 Center => new(X + Width / 2, Y + Height / 2);

    public void Deconstruct(out int x, out int y, out int width, out int height)
    {
        x = X;
        y = Y;
        width = Width;
        height = Height;
    }

    public static RectI FromLTRB(int left, int top, int right, int bottom)
    {
        return new RectI(left, top, right - left, bottom - top);
    }

    public bool Contains(int x, int y)
    {
        return (x >= Left) && (x < Right) && (y >= Top) && (y < Bottom);
    }

    public bool Contains(Int2 point)
    {
        return Contains(point.X, point.Y);
    }

    public bool Contains(RectI rect)
    {
        return X <= rect.X && Right >= rect.Right && Y <= rect.Y && Bottom >= rect.Bottom;
    }

    public void Offset(int offsetX, int offsetY)
    {
        X += offsetX;
        Y += offsetY;
    }

    public void Inflate(int horizAmount, int vertAmount)
    {
        X -= horizAmount;
        Y -= vertAmount;
        Width += horizAmount;
        Height += vertAmount;
    }

    public readonly bool IntersectsWith(RectI rect)
    {
        return (rect.X < (X + Width)) && (X < (rect.X + rect.Width)) && (rect.Y < (Y + Height)) && (Y < (rect.Y + rect.Height));
    }

    public static RectI Intersect(RectI ra, RectI rb)
    {
        int righta = ra.X + ra.Width;
        int rightb = rb.X + rb.Width;

        int bottoma = ra.Y + ra.Height;
        int bottomb = rb.Y + rb.Height;

        int maxX = ra.X > rb.X ? ra.X : rb.X;
        int maxY = ra.Y > rb.Y ? ra.Y : rb.Y;

        int minRight = righta < rightb ? righta : rightb;
        int minBottom = bottoma < bottomb ? bottoma : bottomb;

        if ((minRight > maxX) && (minBottom > maxY))
        {
            return new(maxX, maxY, minRight - maxX, minBottom - maxY);
        }

        return default;
    }

    public static RectI Union(RectI ra, RectI rb)
    {
        int righta = ra.X + ra.Width;
        int rightb = rb.X + rb.Width;

        int bottoma = ra.Y + ra.Height;
        int bottomb = rb.Y + rb.Height;

        int minX = ra.X < rb.X ? ra.X : rb.X;
        int minY = ra.Y < rb.Y ? ra.Y : rb.Y;

        int maxRight = righta > rightb ? righta : rightb;
        int maxBottom = bottoma > bottomb ? bottoma : bottomb;

        return new(minX, minY, maxRight - minX, maxBottom - minY);
    }

    /// <summary>
    /// Compares two <see cref="RectI"/> objects for equality.
    /// </summary>
    /// <param name="left">The <see cref="RectI"/> on the left hand of the operand.</param>
    /// <param name="right">The <see cref="RectI"/> on the right hand of the operand.</param>
    /// <returns>
    /// True if the current left is equal to the <paramref name="right"/> parameter; otherwise, false.
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator ==(RectI left, RectI right)
    {
        return left.X == right.X && left.Y == right.Y && left.Width == right.Width && left.Height == right.Height;
    }

    /// <summary>
    /// Compares two <see cref="RectI"/> objects for inequality.
    /// </summary>
    /// <param name="left">The <see cref="RectI"/> on the left hand of the operand.</param>
    /// <param name="right">The <see cref="RectI"/> on the right hand of the operand.</param>
    /// <returns>
    /// True if the current left is unequal to the <paramref name="right"/> parameter; otherwise, false.
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator !=(RectI left, RectI right)
    {
        return (left.X != right.X) || (left.Y != right.Y) || (left.Width != right.Width) || (left.Height != right.Height);
    }

    /// <inheritdoc/>
    public override bool Equals(object? obj) => obj is RectI other && Equals(other);

    /// <inheritdoc/>
    public bool Equals(RectI other)
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
        => $"{nameof(RectI)} {{ {nameof(X)} = {X.ToString(format, formatProvider)}, {{ {nameof(Y)} = {Y.ToString(format, formatProvider)}, {{ {nameof(Width)} = {Width.ToString(format, formatProvider)}, {nameof(Height)} = {Height.ToString(format, formatProvider)} }}";
}
