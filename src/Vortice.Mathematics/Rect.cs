// Copyright (c) Amer Koleci and contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace Vortice.Mathematics;

[Serializable]
[DataContract]
[StructLayout(LayoutKind.Sequential)]
public struct Rect : IEquatable<Rect>
{
    /// <summary>
    /// A <see cref="Rect"/> with all of its components set to zero.
    /// </summary>
    public static Rect Empty => default;

    /// <summary>
    /// The x-coordinate of the rectangle.
    /// </summary>
    public float X;

    /// <summary>
    /// The y-coordinate of the rectangle.
    /// </summary>
    public float Y;

    /// <summary>
    /// The width of the rectangle.
    /// </summary>
    public float Width;

    /// <summary>
    /// The height of the rectangle.
    /// </summary>
    public float Height;

    /// <summary>
    /// Initializes a new instance of the <see cref='Rect'/> struct with the specified size.
    /// </summary>
    public Rect(float width, float height)
    {
        X = 0.0f;
        Y = 0.0f;
        Width = width;
        Height = height;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref='Rect'/> struct with the specified location and size.
    /// </summary>
    public Rect(float x, float y, float width, float height)
    {
        X = x;
        Y = y;
        Width = width;
        Height = height;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref='Rect'/> struct with the specified size.
    /// </summary>
    public Rect(in Vector2 size)
    {
        X = 0.0f;
        Y = 0.0f;
        Width = size.X;
        Height = size.Y;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref='Rect'/> struct with the specified location and size.
    /// </summary>
    public Rect(in Vector2 location, in Size size)
    {
        X = location.X;
        Y = location.Y;
        Width = size.Width;
        Height = size.Height;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref='Rect'/> struct from the specified <see cref="Vector4"/>.
    /// </summary>
    public Rect(in Vector4 vector)
    {
        X = vector.X;
        Y = vector.Y;
        Width = vector.Z;
        Height = vector.W;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref='Rect'/> struct from the specified <see cref="System.Drawing.RectangleF"/>.
    /// </summary>
    public Rect(in System.Drawing.RectangleF rect)
    {
        X = rect.X;
        Y = rect.Y;
        Width = rect.Width;
        Height = rect.Height;
    }

    /// <summary>
    /// Creates a new <see cref="Vector4"/> from this <see cref="Rect"/>.
    /// </summary>
    public readonly Vector4 ToVector4() => new(X, Y, Width, Height);

    /// <summary>
    /// Converts the specified <see cref="Rect"/> to a <see cref="Vector4"/>.
    /// </summary>
    public static explicit operator Vector4(in Rect rectangle) => rectangle.ToVector4();

    /// <summary>
    /// Converts the specified <see cref="Vector4"/> to a <see cref="Rect"/>.
    /// </summary>
    public static explicit operator Rect(in Vector4 vector) => new(vector);

    /// <summary>
    /// Creates a new <see cref='Rect'/> with the specified location and size.
    /// </summary>
    public static Rect FromLTRB(float left, float top, float right, float bottom) => new(left, top, right - left, bottom - top);

    [IgnoreDataMember]
    public Vector2 Position
    {
        readonly get => new(X, Y);
        set
        {
            X = value.X;
            Y = value.Y;
        }
    }

    [IgnoreDataMember]
    public Size Size
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
    /// <see cref='Rect'/> .
    /// </summary>
    [IgnoreDataMember]
    public float Left
    {
        readonly get => X;
        set => X = value;
    }

    /// <summary>
    /// Gets the y-coordinate of the upper-left corner of the rectangular region defined by this
    /// <see cref='Rect'/>.
    /// </summary>
    [IgnoreDataMember]
    public float Top
    {
        readonly get => Y;
        set => Y = value;
    }

    /// <summary>
    /// Gets the x-coordinate of the lower-right corner of the rectangular region defined by this
    /// <see cref='Rect'/>.
    /// </summary>
    [IgnoreDataMember]
    public float Right
    {
        readonly get => X + Width;
        set => X = value - Width;
    }

    /// <summary>
    /// Gets the y-coordinate of the lower-right corner of the rectangular region defined by this
    /// <see cref='Rect'/>.
    /// </summary>
    [IgnoreDataMember]
    public float Bottom
    {
        readonly get => Y + Height;
        set => Y = value - Height;
    }

    [IgnoreDataMember]
    public float CenterX
    {
        readonly get => X + Width / 2;
        set => X = value - Width / 2;
    }

    [IgnoreDataMember]
    public float CenterY
    {
        readonly get => Y + Height / 2;
        set => Y = value - Height / 2;
    }

    [IgnoreDataMember]
    public Vector2 TopLeft
    {
        readonly get => new(Left, Top);
        set
        {
            Left = value.X;
            Top = value.Y;
        }
    }

    [IgnoreDataMember]
    public Vector2 TopCenter
    {
        readonly get => new(CenterX, Top);
        set
        {
            CenterX = value.X;
            Top = value.Y;
        }
    }

    [IgnoreDataMember]
    public Vector2 TopRight
    {
        readonly get => new(Right, Top);
        set
        {
            Right = value.X;
            Top = value.Y;
        }
    }

    [IgnoreDataMember]
    public Vector2 CenterLeft
    {
        readonly get => new(Left, CenterY);
        set
        {
            Left = value.X;
            CenterY = value.Y;
        }
    }

    [IgnoreDataMember]
    public Vector2 Center
    {
        readonly get => new(CenterX, CenterY);
        set
        {
            CenterX = value.X;
            CenterY = value.Y;
        }
    }

    [IgnoreDataMember]
    public Vector2 CenterRight
    {
        readonly get => new(Right, CenterY);
        set
        {
            Right = value.X;
            CenterY = value.Y;
        }
    }

    [IgnoreDataMember]
    public Vector2 BottomLeft
    {
        readonly get => new(Left, Bottom);
        set
        {
            Left = value.X;
            Bottom = value.Y;
        }
    }

    [IgnoreDataMember]
    public Vector2 BottomCenter
    {
        readonly get => new(CenterX, Bottom);
        set
        {
            CenterX = value.X;
            Bottom = value.Y;
        }
    }

    [IgnoreDataMember]
    public Vector2 BottomRight
    {
        readonly get => new(Right, Bottom);
        set
        {
            Right = value.X;
            Bottom = value.Y;
        }
    }

    /// <summary>
    /// Gets a value indicating whether this <see cref="Rect"/> is empty.
    /// </summary>
    [EditorBrowsable(EditorBrowsableState.Never)]
    public readonly bool IsEmpty => (Width <= 0) || (Height <= 0);

    /// <summary>
    /// Adjusts the location of this rectangle by the specified amount.
    /// </summary>
    public void Offset(in Vector2 pos) => Offset(pos.X, pos.Y);

    /// <summary>
    /// Adjusts the location of this rectangle by the specified amount.
    /// </summary>
    public void Offset(float x, float y)
    {
        X += x;
        Y += y;
    }

    /// <summary>
    /// Inflates this <see cref='Rect'/> by the specified amount.
    /// </summary>
    public void Inflate(float x, float y)
    {
        X -= x;
        Y -= y;
        Width += 2 * x;
        Height += 2 * y;
    }

    /// <summary>
    /// Inflates this <see cref='Rect'/> by the specified amount.
    /// </summary>
    public void Inflate(in Size size) => Inflate(size.Width, size.Height);

    /// <summary>
    /// Determines if the specified point is contained within the rectangular region defined by this
    /// <see cref='Rect'/> .
    /// </summary>
    public readonly bool Contains(float x, float y) => X <= x && x < X + Width && Y <= y && y < Y + Height;

    /// <summary>
    /// Determines if the specified point is contained within the rectangular region defined by this
    /// <see cref='Rect'/> .
    /// </summary>
    public readonly bool Contains(in Vector2 pt) => Contains(pt.X, pt.Y);

    /// <summary>
    /// Determines if the rectangular region represented by <paramref name="rect"/> is entirely contained within
    /// the rectangular region represented by this <see cref='Rect'/> .
    /// </summary>
    public readonly bool Contains(in Rect rect) => (X <= rect.X) && (rect.X + rect.Width <= X + Width) && (Y <= rect.Y) && (rect.Y + rect.Height <= Y + Height);

    /// <summary>
    /// Determines if the rectangular region represented by <paramref name="rect"/> is entirely contained within
    /// the rectangular region represented by this <see cref='Rect'/> .
    /// </summary>
    public readonly bool Contains(in System.Drawing.RectangleF rect) => (X <= rect.X) && (rect.X + rect.Width <= X + Width) && (Y <= rect.Y) && (rect.Y + rect.Height <= Y + Height);

    /// <summary>
    /// Creates a Rectangle that represents the intersection between this Rectangle and rect.
    /// </summary>
    public void Intersect(in Rect rect)
    {
        Rect result = Intersect(rect, this);

        X = result.X;
        Y = result.Y;
        Width = result.Width;
        Height = result.Height;
    }

    /// <summary>
    /// Creates a rectangle that represents the intersection between a and b. If there is no intersection, an
    /// empty rectangle is returned.
    /// </summary>
    public static Rect Intersect(in Rect a, in Rect b)
    {
        float x1 = MathF.Max(a.X, b.X);
        float x2 = MathF.Min(a.X + a.Width, b.X + b.Width);
        float y1 = MathF.Max(a.Y, b.Y);
        float y2 = MathF.Min(a.Y + a.Height, b.Y + b.Height);

        if (x2 >= x1 && y2 >= y1)
        {
            return new(x1, y1, x2 - x1, y2 - y1);
        }

        return Empty;
    }

    /// <summary>
    /// Determines if this rectangle intersects with rect.
    /// </summary>
    public readonly bool IntersectsWith(in Rect rect) => (rect.X < X + Width) && (X < rect.X + rect.Width) && (rect.Y < Y + Height) && (Y < rect.Y + rect.Height);

    /// <summary>
    /// Determines if this rectangle intersects with rect.
    /// </summary>
    public readonly bool IntersectsWith(in System.Drawing.RectangleF rect) => (rect.X < X + Width) && (X < rect.X + rect.Width) && (rect.Y < Y + Height) && (Y < rect.Y + rect.Height);

    /// <summary>
    /// Creates a rectangle that represents the union between a and b.
    /// </summary>
    public static Rect Union(in Rect a, in Rect b)
    {
        float x1 = MathF.Min(a.X, b.X);
        float x2 = MathF.Max(a.X + a.Width, b.X + b.Width);
        float y1 = MathF.Min(a.Y, b.Y);
        float y2 = MathF.Max(a.Y + a.Height, b.Y + b.Height);

        return new (x1, y1, x2 - x1, y2 - y1);
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
    public static bool operator ==(Rect left, Rect right) =>
        left.X == right.X && left.Y == right.Y && left.Width == right.Width && left.Height == right.Height;

    /// <summary>
    /// Compares two <see cref="Rect"/> objects for inequality.
    /// </summary>
    /// <param name="left">The <see cref="Rect"/> on the left hand of the operand.</param>
    /// <param name="right">The <see cref="Rect"/> on the right hand of the operand.</param>
    /// <returns>
    /// True if the current left is unequal to the <paramref name="right"/> parameter; otherwise, false.
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator !=(Rect left, Rect right) => !(left == right);

    /// <summary>
    /// Converts the specified <see cref='RectI'/> to a <see cref='Rect'/>.
    /// </summary>
    public static implicit operator Rect(in RectI rect) => new(rect.X, rect.Y, rect.Width, rect.Height);

    /// <summary>
    /// Performs an explicit conversion from <see cref="System.Drawing.RectangleF"/> to <see cref="Rect"/>.
    /// </summary>
    /// <param name="value">The value.</param>
    /// <returns>The result of the conversion.</returns>
    public static explicit operator Rect(in System.Drawing.RectangleF value) => new(in value);

    /// <summary>
    /// Performs an explicit conversion from <see cref="Rect"/> to <see cref="System.Drawing.RectangleF"/>.
    /// </summary>
    /// <param name="value">The value.</param>
    /// <returns>The result of the conversion.</returns>
    public static explicit operator System.Drawing.RectangleF(in Rect value) => new(value.X, value.Y, value.Width, value.Height);

    /// <inheritdoc/>
    public override readonly bool Equals([NotNullWhen(true)] object? obj) => obj is Rect value && Equals(value);

    public readonly bool Equals(Rect other) => this == other;

    /// <summary>
    /// Gets the hash code for this <see cref='Rect'/>.
    /// </summary>
    public override readonly int GetHashCode() => HashCode.Combine(X, Y, Width, Height);

    //// <inheritdoc />
    public override readonly string ToString() => $"{{X={X},Y={Y},Width={Width},Height={Height}}}";
}
