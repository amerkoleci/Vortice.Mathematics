// Copyright (c) Amer Koleci and contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

using System.ComponentModel;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Vortice.Mathematics;

/// <summary>
/// Stores an ordered pair of integer numbers describing the width, height and depth of a rectangle.
/// </summary>
[StructLayout(LayoutKind.Sequential, Pack = 4)]
public record struct Size3
{
    /// <summary>
    /// A <see cref="Size3"/> with all of its components set to zero.
    /// </summary>
    public static Size3 Empty => default;

    /// <summary>
    /// A special valued <see cref="Size"/>.
    /// </summary>
    public static Size3 WholeSize => new(~0, ~0, ~0);

    /// <summary>
    /// The width component of the extent.
    /// </summary>
    public readonly int Width;

    /// <summary>
    /// The height component of the extent.
    /// </summary>
    public readonly int Height;

    /// <summary>
    /// The depth component of the extent.
    /// </summary>
    public readonly int Depth;

    /// <summary>
    /// Initializes a new instance of <see cref="Size3"/> structure.
    /// </summary>
    /// <param name="width">The width component of the extent.</param>
    /// <param name="height">The height component of the extent.</param>
    /// <param name="depth">The depth component of the extent.</param>
    public Size3(int width, int height, int depth)
    {
        Width = width;
        Height = height;
        Depth = depth;
    }

    /// <summary>
    /// Initializes a new instance of <see cref="Size3"/> structure.
    /// </summary>
    /// <param name="size">The width and height component of the extent.</param>
    /// <param name="depth">The depth component of the extent.</param>
    public Size3(in Size size, int depth)
    {
        Width = size.Width;
        Height = size.Height;
        Depth = depth;
    }

    /// <summary>
    /// Gets a value indicating whether this <see cref="Size3"/> is empty.
    /// </summary>
    [EditorBrowsable(EditorBrowsableState.Never)]
    public readonly bool IsEmpty => this == Empty;

    /// <summary>
    /// Deconstructs this size into three integers.
    /// </summary>
    /// <param name="width">The out value for the width.</param>
    /// <param name="height">The out value for the height.</param>
    /// <param name="depth">The out value for the depth.</param>
    public readonly void Deconstruct(out int width, out int height, out int depth)
    {
        width = Width;
        height = Height;
        depth = Depth;
    }
}
