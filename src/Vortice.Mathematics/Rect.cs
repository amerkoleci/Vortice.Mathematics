// Copyright (c) Amer Koleci and contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace Vortice.Mathematics;

[DebuggerDisplay("X={X}, Y={Y}, Width={Width}, Height={Height}")]
public struct Rect : IEquatable<Rect>, IFormattable
{
    /// <summary>
    /// A <see cref="Rect"/> with all of its components set to zero.
    /// </summary>
    public static readonly Rect Empty = default;

    public Rect(float x, float y, float width, float height)
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

    public Rect(in Vector2 location, Size size)
        : this(location.X, location.Y, size.Width, size.Height)
    {
    }

    /// <summary>
    /// Initializes a new instance of <see cref="Rect"/> structure.
    /// </summary>
    /// <param name="width">The width component of the size.</param>
    /// <param name="height">The height component of the size.</param>
    public Rect(float width, float height)
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
    /// Gets a value indicating whether this <see cref="Rect"/> is empty.
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

    public Vector2 Location
    {
        get => new(X, Y);
        set
        {
            X = value.X;
            Y = value.Y;
        }
    }

    public Vector2 Center => new(X + Width / 2, Y + Height / 2);

    public void Deconstruct(out float x, out float y, out float width, out float height)
    {
        x = X;
        y = Y;
        width = Width;
        height = Height;
    }

    public static Rect FromLTRB(float left, float top, float right, float bottom)
    {
        return new Rect(left, top, right - left, bottom - top);
    }

    public bool Contains(float x, float y)
    {
        return (x >= Left) && (x < Right) && (y >= Top) && (y < Bottom);
    }

    public bool Contains(in Vector2 vector)
    {
        return Contains(vector.X, vector.Y);
    }

    public bool Contains(in Rect rect)
    {
        return X <= rect.X && Right >= rect.Right && Y <= rect.Y && Bottom >= rect.Bottom;
    }

    public void Offset(float offsetX, float offsetY)
    {
        X += offsetX;
        Y += offsetY;
    }

    public void Offset(Vector2 offset)
    {
        X += offset.X;
        Y += offset.Y;
    }

    public void Inflate(float horizAmount, float vertAmount)
    {
        X -= horizAmount;
        Y -= vertAmount;
        Width += horizAmount;
        Height += vertAmount;
    }

    public readonly bool IntersectsWith(Rect rect)
    {
        return (rect.X < (X + Width)) && (X < (rect.X + rect.Width)) && (rect.Y < (Y + Height)) && (Y < (rect.Y + rect.Height));
    }

    public static Rect Intersect(Rect ra, Rect rb)
    {
        float righta = ra.X + ra.Width;
        float rightb = rb.X + rb.Width;

        float bottoma = ra.Y + ra.Height;
        float bottomb = rb.Y + rb.Height;

        float maxX = ra.X > rb.X ? ra.X : rb.X;
        float maxY = ra.Y > rb.Y ? ra.Y : rb.Y;

        float minRight = righta < rightb ? righta : rightb;
        float minBottom = bottoma < bottomb ? bottoma : bottomb;

        if ((minRight > maxX) && (minBottom > maxY))
        {
            return new(maxX, maxY, minRight - maxX, minBottom - maxY);
        }

        return default;
    }

    public static Rect Union(Rect ra, Rect rb)
    {
        float righta = ra.X + ra.Width;
        float rightb = rb.X + rb.Width;

        float bottoma = ra.Y + ra.Height;
        float bottomb = rb.Y + rb.Height;

        float minX = ra.X < rb.X ? ra.X : rb.X;
        float minY = ra.Y < rb.Y ? ra.Y : rb.Y;

        float maxRight = righta > rightb ? righta : rightb;
        float maxBottom = bottoma > bottomb ? bottoma : bottomb;

        return new Rect(minX, minY, maxRight - minX, maxBottom - minY);
    }

    /// <summary>
    /// Compares two <see cref="Rect"/> objects for equality.
    /// </summary>
    /// <param name="left">The <see cref="Rect"/> on the left hand of the operand.</param>
    /// <param name="right">The <see cref="Rect"/> on the right hand of the operand.</param>
    /// <returns>
    /// True if the current left is equal to the <paramref name="right"/> parameter; otherwise, false.
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator ==(Rect left, Rect right)
    {
        return left.X == right.X && left.Y == right.Y && left.Width == right.Width && left.Height == right.Height;
    }

    /// <summary>
    /// Compares two <see cref="Rect"/> objects for inequality.
    /// </summary>
    /// <param name="left">The <see cref="Rect"/> on the left hand of the operand.</param>
    /// <param name="right">The <see cref="Rect"/> on the right hand of the operand.</param>
    /// <returns>
    /// True if the current left is unequal to the <paramref name="right"/> parameter; otherwise, false.
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator !=(Rect left, Rect right)
    {
        return (left.X != right.X) || (left.Y != right.Y) || (left.Width != right.Width) || (left.Height != right.Height);
    }

    /// <inheritdoc/>
    public override bool Equals(object? obj) => obj is Rect other && Equals(other);

    /// <inheritdoc/>
    public bool Equals(Rect other)
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
        => $"{nameof(Rect)} {{ {nameof(X)} = {X.ToString(format, formatProvider)}, {{ {nameof(Y)} = {Y.ToString(format, formatProvider)}, {{ {nameof(Width)} = {Width.ToString(format, formatProvider)}, {nameof(Height)} = {Height.ToString(format, formatProvider)} }}";
}
