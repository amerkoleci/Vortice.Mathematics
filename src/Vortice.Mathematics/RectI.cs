// Copyright (c) Amer Koleci and contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace Vortice.Mathematics;

[Serializable]
[DataContract]
[StructLayout(LayoutKind.Sequential)]
public struct RectI : IEquatable<RectI>
{
    /// <summary>
    /// A <see cref="RectI"/> with all of its components set to zero.
    /// </summary>
    public static RectI Empty => default;

    /// <summary>
    /// The x-coordinate of the rectangle.
    /// </summary>
    public int X;

    /// <summary>
    /// The y-coordinate of the rectangle.
    /// </summary>
    public int Y;

    /// <summary>
    /// The width of the rectangle.
    /// </summary>
    public int Width;

    /// <summary>
    /// The height of the rectangle.
    /// </summary>
    public int Height;

    /// <summary>
    /// Initializes a new instance of the <see cref='RectI'/> struct with the specified size.
    /// </summary>
    public RectI(int width, int height)
    {
        X = 0;
        Y = 0;
        Width = width;
        Height = height;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref='RectI'/> struct with the specified location and size.
    /// </summary>
    public RectI(int x, int y, int width, int height)
    {
        X = x;
        Y = y;
        Width = width;
        Height = height;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref='RectI'/> struct with the specified size.
    /// </summary>
    public RectI(in SizeI size)
    {
        X = 0;
        Y = 0;
        Width = size.Width;
        Height = size.Height;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref='RectI'/> struct with the specified location and size.
    /// </summary>
    public RectI(in Int2 location, in SizeI size)
    {
        X = location.X;
        Y = location.Y;
        Width = size.Width;
        Height = size.Height;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref='RectI'/> struct from the specified <see cref="System.Drawing.Rectangle"/>.
    /// </summary>
    public RectI(in System.Drawing.Rectangle rect)
    {
        X = rect.X;
        Y = rect.Y;
        Width = rect.Width;
        Height = rect.Height;
    }

    /// <summary>
    /// Creates a new <see cref='RectI'/> with the specified location and size.
    /// </summary>
    public static RectI FromLTRB(int left, int top, int right, int bottom) => new(left, top, unchecked(right - left), unchecked(bottom - top));

    [IgnoreDataMember]
    public Int2 Position
    {
        readonly get => new(X, Y);
        set
        {
            X = value.X;
            Y = value.Y;
        }
    }

    [IgnoreDataMember]
    public SizeI Size
    {
        readonly get => new(Width, Height);
        set
        {
            Width = value.Width;
            Height = value.Height;
        }
    }

    /// <summary>
    /// Gets the x-coordinate of the upper-left corner of the rectangular region defined by this
    /// <see cref='RectI'/> .
    /// </summary>
    [IgnoreDataMember]
    public int Left
    {
        readonly get => X;
        set => X = value;
    }

    /// <summary>
    /// Gets the y-coordinate of the upper-left corner of the rectangular region defined by this
    /// <see cref='RectI'/>.
    /// </summary>
    [IgnoreDataMember]
    public int Top
    {
        readonly get => Y;
        set => Y = value;
    }

    /// <summary>
    /// Gets the x-coordinate of the lower-right corner of the rectangular region defined by this
    /// <see cref='RectI'/>.
    /// </summary>
    [IgnoreDataMember]
    public int Right
    {
        readonly get => X + Width;
        set => X = value - Width;
    }

    /// <summary>
    /// Gets the y-coordinate of the lower-right corner of the rectangular region defined by this
    /// <see cref='RectI'/>.
    /// </summary>
    [IgnoreDataMember]
    public int Bottom
    {
        readonly get => Y + Height;
        set => Y = value - Height;
    }

    /// <summary>
    /// Gets a value indicating whether this <see cref="Rect"/> is empty.
    /// </summary>
    [EditorBrowsable(EditorBrowsableState.Never)]
    public readonly bool IsEmpty => Height == 0 && Width == 0 && X == 0 && Y == 0;


    /// <summary>
    /// Adjusts the location of this rectangle by the specified amount.
    /// </summary>
    public void Offset(in Int2 pos) => Offset(pos.X, pos.Y);

    /// <summary>
    /// Adjusts the location of this rectangle by the specified amount.
    /// </summary>
    public void Offset(int x, int y)
    {
        unchecked
        {
            X += x;
            Y += y;
        }
    }

    /// <summary>
    /// Inflates this <see cref='RectI'/> by the specified amount.
    /// </summary>
    public void Inflate(int width, int height)
    {
        unchecked
        {
            X -= width;
            Y -= height;

            Width += 2 * width;
            Height += 2 * height;
        }
    }

    /// <summary>
    /// Inflates this <see cref='RectI'/> by the specified amount.
    /// </summary>
    public void Inflate(in SizeI size) => Inflate(size.Width, size.Height);

    /// <summary>
    /// Determines if the specified point is contained within the rectangular region defined by this
    /// <see cref='RectI'/> .
    /// </summary>
    public readonly bool Contains(int x, int y) => X <= x && x < X + Width && Y <= y && y < Y + Height;

    /// <summary>
    /// Determines if the specified point is contained within the rectangular region defined by this
    /// <see cref='RectI'/> .
    /// </summary>
    public readonly bool Contains(in Int2 pt) => Contains(pt.X, pt.Y);

    /// <summary>
    /// Determines if the rectangular region represented by <paramref name="rect"/> is entirely contained within the
    /// rectangular region represented by this <see cref='RectI'/> .
    /// </summary>
    public readonly bool Contains(in RectI rect) =>
        (X <= rect.X) && (rect.X + rect.Width <= X + Width) &&
        (Y <= rect.Y) && (rect.Y + rect.Height <= Y + Height);

    /// <summary>
    /// Determines if the rectangular region represented by <paramref name="rect"/> is entirely contained within the
    /// rectangular region represented by this <see cref='RectI'/> .
    /// </summary>
    public readonly bool Contains(in System.Drawing.Rectangle rect) =>
        (X <= rect.X) && (rect.X + rect.Width <= X + Width) &&
        (Y <= rect.Y) && (rect.Y + rect.Height <= Y + Height);

    /// <summary>
    /// Creates a Rectangle that represents the intersection between this Rectangle and rect.
    /// </summary>
    public void Intersect(in RectI rect)
    {
        RectI result = Intersect(rect, this);

        X = result.X;
        Y = result.Y;
        Width = result.Width;
        Height = result.Height;
    }

    /// <summary>
    /// Creates a rectangle that represents the intersection between a and b. If there is no intersection, an
    /// empty rectangle is returned.
    /// </summary>
    public static RectI Intersect(in RectI a, in RectI b)
    {
        int x1 = Math.Max(a.X, b.X);
        int x2 = Math.Min(a.X + a.Width, b.X + b.Width);
        int y1 = Math.Max(a.Y, b.Y);
        int y2 = Math.Min(a.Y + a.Height, b.Y + b.Height);

        if (x2 >= x1 && y2 >= y1)
        {
            return new(x1, y1, x2 - x1, y2 - y1);
        }

        return Empty;
    }

    /// <summary>
    /// Determines if this rectangle intersects with rect.
    /// </summary>
    public readonly bool IntersectsWith(in RectI rect) =>
        (rect.X < X + Width) && (X < rect.X + rect.Width) &&
        (rect.Y < Y + Height) && (Y < rect.Y + rect.Height);

    /// <summary>
    /// Determines if this rectangle intersects with rect.
    /// </summary>
    public readonly bool IntersectsWith(in System.Drawing.Rectangle rect) =>
        (rect.X < X + Width) && (X < rect.X + rect.Width) &&
        (rect.Y < Y + Height) && (Y < rect.Y + rect.Height);

    /// <summary>
    /// Creates a rectangle that represents the union between a and b.
    /// </summary>
    public static RectI Union(in RectI a, in RectI b)
    {
        int x1 = Math.Min(a.X, b.X);
        int x2 = Math.Max(a.X + a.Width, b.X + b.Width);
        int y1 = Math.Min(a.Y, b.Y);
        int y2 = Math.Max(a.Y + a.Height, b.Y + b.Height);

        return new(x1, y1, x2 - x1, y2 - y1);
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
    public static bool operator ==(RectI left, RectI right) =>
        left.X == right.X && left.Y == right.Y && left.Width == right.Width && left.Height == right.Height;

    /// <summary>
    /// Compares two <see cref="RectI"/> objects for inequality.
    /// </summary>
    /// <param name="left">The <see cref="RectI"/> on the left hand of the operand.</param>
    /// <param name="right">The <see cref="RectI"/> on the right hand of the operand.</param>
    /// <returns>
    /// True if the current left is unequal to the <paramref name="right"/> parameter; otherwise, false.
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator !=(RectI left, RectI right) => !(left == right);

    /// <summary>
    /// Performs an explicit conversion from <see cref="System.Drawing.Rectangle"/> to <see cref="RectI"/>.
    /// </summary>
    /// <param name="value">The value.</param>
    /// <returns>The result of the conversion.</returns>
    public static explicit operator RectI(in System.Drawing.Rectangle value) => new(in value);

    /// <summary>
    /// Performs an explicit conversion from <see cref="RectI"/> to <see cref="System.Drawing.Rectangle"/>.
    /// </summary>
    /// <param name="value">The value.</param>
    /// <returns>The result of the conversion.</returns>
    public static explicit operator System.Drawing.Rectangle(in RectI value) => new(value.X, value.Y, value.Width, value.Height);

    /// <inheritdoc/>
    public override readonly bool Equals([NotNullWhen(true)] object? obj) => obj is RectI value && Equals(value);

    public readonly bool Equals(RectI other) => this == other;

    /// <summary>
    /// Gets the hash code for this <see cref='RectI'/>.
    /// </summary>
    public override readonly int GetHashCode() => HashCode.Combine(X, Y, Width, Height);

    //// <inheritdoc />
    public override readonly string ToString() => $"{{X={X},Y={Y},Width={Width},Height={Height}}}";
}
