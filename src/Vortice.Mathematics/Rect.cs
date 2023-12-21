// Copyright (c) Amer Koleci and contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

using System.ComponentModel;
using System.Numerics;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace Vortice.Mathematics;

[Serializable]
[DataContract]
[StructLayout(LayoutKind.Sequential)]
public record struct Rect
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
    public Rect(in Vector2 location, in Vector2 size)
    {
        X = location.X;
        Y = location.Y;
        Width = size.X;
        Height = size.Y;
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
    public Vector2 Size
    {
        readonly get => new(Width, Height);
        set
        {
            Width = value.X;
            Height = value.Y;
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
    /// Converts the specified <see cref='RectI'/> to a <see cref='Rect'/>.
    /// </summary>
    public static implicit operator Rect(in RectI rect) => new Rect(rect.X, rect.Y, rect.Width, rect.Height); 

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
}
