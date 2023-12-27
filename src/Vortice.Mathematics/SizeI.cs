// Copyright (c) Amer Koleci and contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

using System.ComponentModel;

namespace Vortice.Mathematics;

/// <summary>
/// Stores an ordered pair of integer numbers describing the width, height and depth of a rectangle.
/// </summary>
public record struct SizeI
{
    /// <summary>
    /// A <see cref="SizeI"/> with all of its components set to zero.
    /// </summary>
    public static SizeI Empty => default;

    /// <summary>
    /// The width component of the size.
    /// </summary>
    public int Width;

    /// <summary>
    /// The height component of the size.
    /// </summary>
    public int Height;

    /// <summary>
    /// Initializes a new instance of <see cref="SizeI"/> structure.
    /// </summary>
    /// <param name="width">The width component of the size.</param>
    /// <param name="height">The height component of the size.</param>
    public SizeI(int width, int height)
    {
        Width = width;
        Height = height;
    }

    /// <summary>
    /// Initializes a new instance of <see cref="SizeI"/> structure.
    /// </summary>
    /// <param name="vector">The width/height vector.</param>
    public SizeI(in Int2 vector)
    {
        Width = vector.X;
        Height = vector.Y;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref='SizeI'/> struct from the specified <see cref="System.Drawing.Size"/>.
    /// </summary>
    public SizeI(in System.Drawing.Size size)
    {
        Width = size.Width;
        Height = size.Height;
    }

    /// <summary>
    /// Gets a value indicating whether this <see cref="SizeI"/> is empty.
    /// </summary>
    [EditorBrowsable(EditorBrowsableState.Never)]
    public readonly bool IsEmpty => this == Empty;

    /// <summary>
    /// Deconstructs this size into three integers.
    /// </summary>
    /// <param name="width">The out value for the width.</param>
    /// <param name="height">The out value for the height.</param>
    public readonly void Deconstruct(out int width, out int height)
    {
        width = Width;
        height = Height;
    }

    /// <summary>
    /// Performs an explicit conversion from <see cref="System.Drawing.Size"/> to <see cref="SizeI"/>.
    /// </summary>
    /// <param name="value">The value.</param>
    /// <returns>The result of the conversion.</returns>
    public static explicit operator SizeI(in System.Drawing.Size value) => new(in value);

    /// <summary>
    /// Performs an explicit conversion from <see cref="SizeI"/> to <see cref="System.Drawing.Size"/>.
    /// </summary>
    /// <param name="value">The value.</param>
    /// <returns>The result of the conversion.</returns>
    public static explicit operator System.Drawing.Size(in SizeI value) => new(value.Width, value.Height);
}
