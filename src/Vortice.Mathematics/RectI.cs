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
public record struct RectI
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
    public RectI(in Int2 size)
    {
        X = 0;
        Y = 0;
        Width = size.X;
        Height = size.Y;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref='RectI'/> struct with the specified location and size.
    /// </summary>
    public RectI(in Int2 location, in Int2 size)
    {
        X = location.X;
        Y = location.Y;
        Width = size.X;
        Height = size.Y;
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
    public Int2 Size
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
}
